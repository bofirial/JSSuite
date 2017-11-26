using JS.Core.Foundation.BaseClasses;
using JS.Core.Foundation.Constants;
using JS.Core.Foundation.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JS.Core.Foundation.ExtensionMethods;
using JS.Core.Foundation.ErrorHandling;
using JS.Core.Foundation.Messaging;
using System.Reflection;

namespace JS.Core.Foundation.Data
{
    /// <summary>
    /// Base Class for (DAL) Entity Managers
    /// </summary>
    public abstract class EntityManagerBase<TManager> : SingletonBase<TManager> where TManager : class
    {

        #region Public Functions

        /// <summary>
        /// Name of the Database
        /// </summary>
        public abstract string DatabaseName { get; }

        /// <summary>
        /// Populates the Connection Info With the Connection Information for the current Database
        /// </summary>
        /// <param name="connectionInfo"></param>
        public virtual void PopulateConnectionInfo(IConnectionInfo connectionInfo)
        {
            connectionInfo.ConnectionString = ConfigurationHelper.Current.GetConnectionString(DatabaseName);
        }

        #endregion

        #region Protected Functions

        /// <summary>
        /// Internal Select Function
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="connectionInfo"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected async Task<List<TEntity>> SelectInternal<TEntity>(IConnectionInfo connectionInfo, TEntity entity)
            where TEntity : IEntity, new()
        {
            List<TEntity> entities = new List<TEntity>();
            SqlDataReader reader = null;

            try
            {
                PopulateConnectionInfo(connectionInfo);

                SqlConnection connection = new SqlConnection(connectionInfo.ConnectionString);

                using (connection)
                {
                    List<SqlParameter> sqlParameters = new List<SqlParameter>();

                    string selectQuery = String.Format("SELECT {0} FROM dbo.{1} WITH (NOLOCK)", String.Join(", ", EntityHelper.Current.GetAllColumns(entity)), EntityHelper.Current.GetTableName(entity));

                    IEnumerable<string> modifiedColumns = entity.GetModifiedColumns();

                    if (modifiedColumns.Count() > 0)
                    {
                        selectQuery += String.Format(" WHERE {0}", GetEntityFilter<TEntity>(entity, modifiedColumns, sqlParameters, connectionInfo));
                    }

                    SqlCommand command = new SqlCommand(selectQuery, connection);

                    await connection.OpenAsync();

                    command.Parameters.AddRange(sqlParameters.ToArray());

                    TraceSQLStatement(selectQuery, sqlParameters);

                    reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            entities.Add(EntityHelper.Current.PopulateFromDataReader<TEntity>(reader, connectionInfo));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new SQLException("SQL Data Access Error", String.Format("There was an error with the {0} SQL Statement for the {1} Table.", "Select", EntityHelper.Current.GetTableName(entity)), e);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }

            return entities;
        }

        /// <summary>
        /// Internal Insert Function
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="connectionInfo"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected async Task<IProcessResult> InsertInternal<TEntity>(IConnectionInfo connectionInfo, TEntity entity)
            where TEntity : IEntity, new()
        {
            SqlDataReader reader = null;

            try
            {
                PopulateConnectionInfo(connectionInfo);

                entity.InsertedBy = connectionInfo.UserId;
                entity.InsertedOn = DateTimeHelper.Current.GetLocalNow(connectionInfo);
                entity.UpdatedBy = connectionInfo.UserId;
                entity.UpdatedOn = DateTimeHelper.Current.GetLocalNow(connectionInfo);

                SqlConnection connection = new SqlConnection(connectionInfo.ConnectionString);

                List<SqlParameter> sqlParameters = new List<SqlParameter>();

                List<string> columnNames = EntityHelper.Current.GetAllColumns(entity, true).ToList();
                List<string> columnValues = new List<string>();

                string primaryKeyColumnName = EntityHelper.Current.GetPrimaryKeyColumn(entity);

                foreach (var columnName in columnNames)
                {
                    object columnValue = ReflectionHelper.Current.GetPropertyValue(entity, columnName);

                    columnValues.Add(String.Format("@{0}", columnName));

                    SQLHelper.Current.AddSQLParameter(sqlParameters, columnName, columnValue, connectionInfo);
                }

                using (connection)
                {
                    string insertQuery = String.Format("INSERT INTO {0} ({1}) OUTPUT INSERTED.{2} VALUES ({3})", EntityHelper.Current.GetTableName(entity),
                        String.Join(", ", columnNames), primaryKeyColumnName, String.Join(", ", columnValues));

                    SqlCommand command = new SqlCommand(insertQuery, connection);

                    connection.Open();

                    command.Parameters.AddRange(sqlParameters.ToArray());

                    TraceSQLStatement(insertQuery, sqlParameters);

                    reader = await command.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        ReflectionHelper.Current.SetPropertyValue(entity, primaryKeyColumnName, reader[primaryKeyColumnName]);
                    }

                    entity.StartTrackingChanges();
                }
            }
            catch (Exception e)
            {
                throw new SQLException("SQL Data Access Error", String.Format("There was an error with the {0} SQL Statement for the {1} Table.", "Insert", EntityHelper.Current.GetTableName(entity)), e);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }

            return new ProcessResult(ResultCodes.Success);
        }

        /// <summary>
        /// Internal Update Function.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="connectionInfo">The connection information.</param>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        protected async Task<IProcessResult> UpdateInternal<TEntity>(IConnectionInfo connectionInfo, TEntity entity)
            where TEntity : IEntity, new()
        {
            SqlDataReader reader = null;

            try
            {
                PopulateConnectionInfo(connectionInfo);

                entity.UpdatedBy = connectionInfo.UserId;
                entity.UpdatedOn = DateTime.UtcNow;

                SqlConnection connection = new SqlConnection(connectionInfo.ConnectionString);

                string primaryKeyColumnName = EntityHelper.Current.GetPrimaryKeyColumn(entity);
                int primaryKeyColumnValue = (int)ReflectionHelper.Current.GetPropertyValue(entity, primaryKeyColumnName);

                Dictionary<string, object> modifiedColumns = new Dictionary<string, object>();

                List<SqlParameter> sqlParameters = new List<SqlParameter>();

                foreach (var columnName in entity.GetModifiedColumns())
                {
                    object columnValue = ReflectionHelper.Current.GetPropertyValue(entity, columnName);

                    if (columnName != primaryKeyColumnName)
                    {
                        modifiedColumns.Add(columnName, String.Format("@{0}", columnName));

                        SQLHelper.Current.AddSQLParameter(sqlParameters, columnName, columnValue, connectionInfo);
                    }
                }

                using (connection)
                {
                    string updateQuery = String.Format("UPDATE {0} SET {1} WHERE {2} = {3}",
                        EntityHelper.Current.GetTableName(entity),
                        String.Join(", ", modifiedColumns.Select(kvp => String.Format("{0} = {1}", kvp.Key, kvp.Value))),
                        primaryKeyColumnName,
                        primaryKeyColumnValue);

                    SqlCommand command = new SqlCommand(updateQuery, connection);

                    connection.Open();

                    command.Parameters.AddRange(sqlParameters.ToArray());

                    TraceSQLStatement(updateQuery, sqlParameters);

                    reader = await command.ExecuteReaderAsync();

                    entity.StartTrackingChanges();
                }
            }
            catch (Exception e)
            {
                throw new SQLException("SQL Data Access Error", String.Format("There was an error with the {0} SQL Statement for the {1} Table.", "Update", EntityHelper.Current.GetTableName(entity)), e);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }

            return new ProcessResult(ResultCodes.Success);
        }

        /// <summary>
        /// Internal Delete Function
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="connectionInfo">The connection information.</param>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        protected async Task<IProcessResult> DeleteInternal<TEntity>(IConnectionInfo connectionInfo, TEntity entity)
            where TEntity : IEntity, new()
        {
            SqlDataReader reader = null;

            try
            {
                PopulateConnectionInfo(connectionInfo);

                SqlConnection connection = new SqlConnection(connectionInfo.ConnectionString);

                List<SqlParameter> sqlParameters = new List<SqlParameter>();

                string primaryKeyColumnName = EntityHelper.Current.GetPrimaryKeyColumn(entity);
                int primaryKeyColumnValue = (int)ReflectionHelper.Current.GetPropertyValue(entity, primaryKeyColumnName);

                using (connection)
                {
                    string deleteQuery = String.Format("DELETE FROM {0} WHERE {1} = {2}",
                        EntityHelper.Current.GetTableName(entity),
                        primaryKeyColumnName,
                        String.Format("@{0}", primaryKeyColumnName));

                    SQLHelper.Current.AddSQLParameter(sqlParameters, primaryKeyColumnName, primaryKeyColumnValue, connectionInfo);

                    SqlCommand command = new SqlCommand(deleteQuery, connection);

                    connection.Open();

                    command.Parameters.AddRange(sqlParameters.ToArray());

                    TraceSQLStatement(deleteQuery, sqlParameters);

                    reader = await command.ExecuteReaderAsync();
                }
            }
            catch (Exception e)
            {
                throw new SQLException("SQL Data Access Error", String.Format("There was an error with the {0} SQL Statement for the {1} Table.", "Delete", EntityHelper.Current.GetTableName(entity)), e);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }

            return new ProcessResult(ResultCodes.Success);
        }

        #endregion

        #region Private Functions

        private static void TraceSQLStatement(string query, List<SqlParameter> sqlParameters)
        {
            AppTraceHelper.Current.TraceMessage(TraceLevels.Debug,
                String.Format("Executing SQL Statement: \"{0}\" with Parameters: {1}",
                query,
                String.Join(", ", sqlParameters.Select(s => String.Format("{0} = {1}", s.ParameterName, s.SqlValue)))), "SQL Database Access");
        }

        protected static string GetEntityFilter<TEntity>(TEntity entity, IEnumerable<string> modifiedColumns, List<SqlParameter> sqlParameters, IConnectionInfo connectionInfo) where TEntity : IEntity, new()
        {
            return String.Join(" AND ", modifiedColumns.Select(c => GetFilterItem<TEntity>(entity, c, sqlParameters, connectionInfo)));
        }

        protected static string GetFilterItem<TEntity>(TEntity entity, string columnName, List<SqlParameter> sqlParameters, IConnectionInfo connectionInfo) where TEntity : IEntity, new()
        {
            object columnValue = ReflectionHelper.Current.GetPropertyValue(entity, columnName);
            string variableName = columnName;

            string format = "{0} = @{1}";

            PropertyInfo propInfo = ReflectionHelper.Current.GetProperty(entity, columnName);

            DBLikeComparisonAttribute dbLikeComparisonAttribute = ReflectionHelper.Current.GetAttribute<DBLikeComparisonAttribute>(propInfo);

            if (dbLikeComparisonAttribute != null)
            {
                format = "{0} like @{1}";

                string colVal = columnValue.ToString();

                #region Remove SQL "Like" Query Special Characters Except '%'

                //http://msdn.microsoft.com/en-us/library/ms179859.aspx

                colVal = colVal.Replace("_", String.Empty);
                colVal = colVal.Replace("[", String.Empty);
                colVal = colVal.Replace("]", String.Empty);
                colVal = colVal.Replace("^", String.Empty);

                #endregion

                if (String.IsNullOrEmpty(dbLikeComparisonAttribute.Wildcard) || dbLikeComparisonAttribute.Wildcard != "%")
                {
                    colVal = colVal.Replace("%", String.Empty);
                }

                if (!String.IsNullOrEmpty(dbLikeComparisonAttribute.Wildcard))
                {
                    colVal = colVal.Replace(dbLikeComparisonAttribute.Wildcard, "%");
                }

                string valueFormat = "{0}";

                if (dbLikeComparisonAttribute is DBContainsAttribute)
                {
                    valueFormat = "%{0}%";
                }
                else if (dbLikeComparisonAttribute is DBStartsWithAttribute)
                {
                    valueFormat = "{0}%";
                }
                else if (dbLikeComparisonAttribute is DBEndsWithAttribute)
                {
                    valueFormat = "%{0}";
                }

                columnValue = String.Format(valueFormat, colVal);
            }

            //if (columnValue != null && columnValue.ToString().Contains('%'))
            //{
            //    format = "{0} like @{1}";
            //}

            if (columnName.EndsWith("_From"))
            {
                columnName = columnName.Remove(columnName.Length - 5);
                format = "{0} >= @{1}";
            }

            if (columnName.EndsWith("_To"))
            {
                columnName = columnName.Remove(columnName.Length - 3);
                format = "{0} < @{1}";
            }

            SQLHelper.Current.AddSQLParameter(sqlParameters, variableName, columnValue, connectionInfo);

            return String.Format(format, columnName, variableName);
        }

        #endregion
    }
}
