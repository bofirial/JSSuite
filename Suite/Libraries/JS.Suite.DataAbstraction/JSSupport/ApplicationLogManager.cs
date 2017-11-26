using JS.Core.Foundation.BaseClasses;
using JS.Core.Foundation.Data;
using JS.Core.Foundation.ErrorHandling;
using JS.Core.Foundation.Helpers;
using JS.Core.Web.Mvc.Html;
using JS.Suite.DataAbstraction.JSSupport.Generated;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Suite.DataAbstraction.JSSupport
{
    /// <summary>
    /// ApplicationLog Manager
    /// </summary>
    public class ApplicationLogManager : ApplicationLogManager_Generated<ApplicationLogManager>
    {
        /// <summary>
        /// Pagings the select asynchronous.
        /// </summary>
        /// <param name="connectionInfo">The connection information.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        /// <exception cref="SQLException">SQL Data Access Error;There was an error with the Paging Select SQL Statement for the Application Log Table.</exception>
        public async Task<PagedResult<ApplicationLog>> PagingSelectAsync(IConnectionInfo connectionInfo, ApplicationLog filter)
        {
            PagedResult<ApplicationLog> pagedResult = new PagedResult<ApplicationLog>()
            {
                Results = new List<ApplicationLog>()
            };
            SqlDataReader reader = null;

            try
            {
                PopulateConnectionInfo(connectionInfo);

                SqlConnection connection = new SqlConnection(connectionInfo.ConnectionString);

                QueryContext queryContext = filter.QueryContext;

                if (queryContext == null)
                {
                    queryContext = new QueryContext();
                }

                pagedResult.PageNumber = queryContext.PageNumber;
                pagedResult.PageSize = queryContext.PageSize;

                using (connection)
                {
                    List<SqlParameter> sqlParameters = new List<SqlParameter>();

                    string selectQuery = String.Format("SELECT {0} FROM {1} WITH (NOLOCK)", String.Join(", ", EntityHelper.Current.GetAllColumns(filter)), EntityHelper.Current.GetTableName(filter));
                    
                    int skipNo = (queryContext.PageNumber - 1) * queryContext.PageSize;

                    IEnumerable<string> modifiedColumns = filter.GetModifiedColumns();

                    string whereFilter = String.Empty;

                    if (modifiedColumns.Count() > 0)
                    {
                        whereFilter = String.Format(" WHERE {0}", GetEntityFilter<ApplicationLog>(filter, modifiedColumns, sqlParameters, connectionInfo));

                        selectQuery += whereFilter;
                    }

                    if (String.IsNullOrEmpty(queryContext.SortBy))
                    {
                        queryContext.SortBy = EntityHelper.Current.GetPrimaryKeyColumn(filter);
                        queryContext.SortDirection = SortOrder.Ascending;
                    }

                    selectQuery += String.Format(" ORDER BY {0} {1}", queryContext.SortBy, SQLHelper.Current.GetSortDirection(queryContext.SortDirection));

                    selectQuery += String.Format(" OFFSET {0} ROWS", skipNo);

                    selectQuery += String.Format(" FETCH NEXT {0} ROWS ONLY", queryContext.PageSize);

                    SqlCommand command = new SqlCommand(selectQuery, connection);

                    await connection.OpenAsync();

                    command.Parameters.AddRange(sqlParameters.ToArray());

                    reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            pagedResult.Results.Add(EntityHelper.Current.PopulateFromDataReader<ApplicationLog>(reader, connectionInfo));
                        }
                    }

                    command.Parameters.Clear();
                    reader.Close();

                    string selectCountQuery = String.Format("SELECT COUNT({0}) FROM {1}", EntityHelper.Current.GetPrimaryKeyColumn(filter), EntityHelper.Current.GetTableName(filter));

                    selectCountQuery += whereFilter;

                    SqlCommand selectCountCommand = new SqlCommand(selectCountQuery, connection);

                    selectCountCommand.Parameters.AddRange(sqlParameters.ToArray());

                    reader = await selectCountCommand.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            pagedResult.TotalResults = (int)reader.GetValue(0);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new SQLException("SQL Data Access Error", "There was an error with the Paging Select SQL Statement for the Application Log Table.", e);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }

            return pagedResult;
        }

        /// <summary>
        /// Applications the log summary paging select asynchronous.
        /// </summary>
        /// <param name="connectionInfo">The connection information.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public async Task<PagedResult<ApplicationLogSummary>> ApplicationLogSummaryPagingSelectAsync(IConnectionInfo connectionInfo, ApplicationLogSummary filter)
        {
            PagedResult<ApplicationLogSummary> pagedResult = new PagedResult<ApplicationLogSummary>()
            {
                Results = new List<ApplicationLogSummary>()
            };
            SqlDataReader reader = null;

            try
            {
                PopulateConnectionInfo(connectionInfo);

                SqlConnection connection = new SqlConnection(connectionInfo.ConnectionString);

                QueryContext queryContext = filter.QueryContext;

                if (queryContext == null)
                {
                    queryContext = new QueryContext();
                }

                pagedResult.PageNumber = queryContext.PageNumber;
                pagedResult.PageSize = queryContext.PageSize;

                using (connection)
                {
                    List<SqlParameter> sqlParameters = new List<SqlParameter>();

                    string selectQuery = String.Format("SELECT Subject, MAX(MessageTypeId) AS MessageTypeId, COUNT(ApplicationId) as Count, MAX(InsertedOn) AS InsertedOn FROM ApplicationLog WITH (NOLOCK)");

                    int skipNo = (queryContext.PageNumber - 1) * queryContext.PageSize;

                    IEnumerable<string> modifiedColumns = filter.GetModifiedColumns();

                    string whereFilter = String.Empty;

                    if (modifiedColumns.Count() > 0)
                    {
                        whereFilter = String.Format(" WHERE {0}", GetEntityFilter<ApplicationLog>(filter, modifiedColumns, sqlParameters, connectionInfo));

                        selectQuery += whereFilter;
                    }

                    if (String.IsNullOrEmpty(queryContext.SortBy))
                    {
                        queryContext.SortBy = "Count";
                        queryContext.SortDirection = SortOrder.Ascending;
                    }

                    selectQuery += String.Format(" GROUP BY Subject");

                    selectQuery += String.Format(" ORDER BY {0} {1}", queryContext.SortBy, SQLHelper.Current.GetSortDirection(queryContext.SortDirection));

                    selectQuery += String.Format(" OFFSET {0} ROWS", skipNo);

                    selectQuery += String.Format(" FETCH NEXT {0} ROWS ONLY", queryContext.PageSize);

                    SqlCommand command = new SqlCommand(selectQuery, connection);

                    await connection.OpenAsync();

                    command.Parameters.AddRange(sqlParameters.ToArray());

                    reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            pagedResult.Results.Add(EntityHelper.Current.PopulateFromDataReader<ApplicationLogSummary>(reader, connectionInfo));
                        }
                    }

                    command.Parameters.Clear();
                    reader.Close();

                    string selectCountQuery = String.Format("SELECT COUNT(DISTINCT ISNULL(Subject, 'NULL')) FROM ApplicationLog");

                    selectCountQuery += whereFilter;

                    SqlCommand selectCountCommand = new SqlCommand(selectCountQuery, connection);

                    selectCountCommand.Parameters.AddRange(sqlParameters.ToArray());

                    reader = await selectCountCommand.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            pagedResult.TotalResults = (int)reader.GetValue(0);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new SQLException("SQL Data Access Error", "There was an error with the Paging Select SQL Statement for the Application Log Table.", e);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }

            return pagedResult;
        }
    }
}
