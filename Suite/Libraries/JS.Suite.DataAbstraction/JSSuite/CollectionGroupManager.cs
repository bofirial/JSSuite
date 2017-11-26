using JS.Core.Foundation.BaseClasses;
using JS.Core.Foundation.Data;
using JS.Core.Foundation.ErrorHandling;
using JS.Core.Foundation.Helpers;
using JS.Suite.DataAbstraction.JSSuite.Generated;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Suite.DataAbstraction.JSSuite
{
    /// <summary>
    /// Collection Group Manager
    /// </summary>
    public class CollectionGroupManager : CollectionGroupManager_Generated<CollectionGroupManager>
    {
        /// <summary>
        /// Pagings the select asynchronous.
        /// </summary>
        /// <param name="connectionInfo">The connection information.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public async Task<PagedResult<CollectionGroup>> PagingSelectAsync(IConnectionInfo connectionInfo, CollectionGroup filter)
        {
            PagedResult<CollectionGroup> pagedResult = new PagedResult<CollectionGroup>()
            {
                Results = new List<CollectionGroup>()
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

                    string selectQuery = String.Format("SELECT cg.CollectionGroupId as CollectionGroupId, cg.Name as Name, cg.Note as Note, jsu.Name as JSUserName, COUNT(ci.CollectionItemId) as NumberOfCollectionItems FROM {0} cg WITH (NOLOCK)", EntityHelper.Current.GetTableName(filter));

                    selectQuery += " JOIN CollectionGroupJSUser cgjsu ON cgjsu.CollectionGroupId = cg.CollectionGroupId";
                    selectQuery += " LEFT JOIN CollectionItem ci ON ci.CollectionGroupId = cg.CollectionGroupId";
                    selectQuery += " LEFT JOIN JSUser jsu ON jsu.JSUserId = cgjsu.JSUserId";

                    int skipNo = (queryContext.PageNumber - 1) * queryContext.PageSize;

                    IEnumerable<string> modifiedColumns = filter.GetModifiedColumns();

                    string whereFilter = String.Empty;

                    if (modifiedColumns.Count() > 0)
                    {
                        whereFilter = String.Format(" WHERE cgjsu.JSUserId = {0}", filter.JSUserId); //GetEntityFilter<ApplicationLog>(filter, modifiedColumns, sqlParameters, connectionInfo)

                        selectQuery += whereFilter;
                    }

                    if (String.IsNullOrEmpty(queryContext.SortBy))
                    {
                        queryContext.SortBy = EntityHelper.Current.GetPrimaryKeyColumn(filter);
                        queryContext.SortDirection = SortOrder.Ascending;
                    }

                    selectQuery += String.Format(" GROUP BY cg.CollectionGroupId, cg.Name, cg.Note, jsu.Name");

                    selectQuery += String.Format(" ORDER BY cg.{0} {1}", queryContext.SortBy, SQLHelper.Current.GetSortDirection(queryContext.SortDirection));

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
                            pagedResult.Results.Add(EntityHelper.Current.PopulateFromDataReader<CollectionGroup>(reader, connectionInfo));
                        }
                    }

                    command.Parameters.Clear();
                    reader.Close();

                    string selectCountQuery = String.Format("SELECT COUNT(cg.{0}) FROM {1} cg", EntityHelper.Current.GetPrimaryKeyColumn(filter), EntityHelper.Current.GetTableName(filter));

                    selectCountQuery += " JOIN CollectionGroupJSUser cgjsu ON cgjsu.CollectionGroupId = cg.CollectionGroupId";

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
