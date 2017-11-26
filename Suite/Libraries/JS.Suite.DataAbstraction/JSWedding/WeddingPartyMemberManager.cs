using JS.Core.Foundation.BaseClasses;
using JS.Core.Foundation.Data;
using JS.Core.Foundation.ErrorHandling;
using JS.Core.Foundation.Helpers;
using JS.Suite.DataAbstraction.JSWedding.Generated;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Suite.DataAbstraction.JSWedding
{
    /// <summary>
    /// WeddingPartyMember Manager
    /// </summary>
    public class WeddingPartyMemberManager : WeddingPartyMemberManager_Generated<WeddingPartyMemberManager>
    {
        /// <summary>
        /// Select With Summary Partners Async
        /// </summary>
        /// <param name="connectionInfo">The connection information.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public async Task<List<WeddingPartyMember>> SelectWithSummaryPartnersAsync(IConnectionInfo connectionInfo, WeddingPartyMember filter)
        {
            //TODO: Make this a custom Stored Proc

            List<WeddingPartyMember> entities = new List<WeddingPartyMember>();
            SqlDataReader reader = null;

            try
            {
                PopulateConnectionInfo(connectionInfo);

                SqlConnection connection = new SqlConnection(connectionInfo.ConnectionString);

                using (connection)
                {
                    List<SqlParameter> sqlParameters = new List<SqlParameter>();

                    string selectQuery = String.Format("SELECT {0}, sp.NameFirst as 'SummaryPartner.NameFirst', sp.NameLast as 'SummaryPartner.NameLast', sp.UDF1 as 'SummaryPartner.UDF1', sp.UDF2 as 'SummaryPartner.UDF2', sp.UDF3 as 'SummaryPartner.UDF3'FROM WeddingPartyMember wpm", String.Join(", ", EntityHelper.Current.GetAllColumns(filter).Select(s => String.Format("wpm.{0}", s))), EntityHelper.Current.GetTableName(filter));

                    selectQuery += String.Format(" LEFT JOIN WeddingPartyMember sp ON sp.WeddingPartyMemberId = wpm.SummaryPartnerId");

                    IEnumerable<string> modifiedColumns = filter.GetModifiedColumns();

                    if (modifiedColumns.Count() > 0)
                    {
                        //HACK We need a way to pass in a table prefix to each column in the filter.  Only 1 column needed here.
                        selectQuery += String.Format(" WHERE wpm.{0}", GetEntityFilter<WeddingPartyMember>(filter, modifiedColumns, sqlParameters, connectionInfo));
                    }

                    SqlCommand command = new SqlCommand(selectQuery, connection);

                    await connection.OpenAsync();

                    command.Parameters.AddRange(sqlParameters.ToArray());

                    reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            entities.Add(EntityHelper.Current.PopulateFromDataReader<WeddingPartyMember>(reader, connectionInfo));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new CoreException("SQL Data Access Error", String.Format("There was an error with the {0} SQL Statement for the {1} Table.", "Select", EntityHelper.Current.GetTableName(filter)), e);
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
    }
}
