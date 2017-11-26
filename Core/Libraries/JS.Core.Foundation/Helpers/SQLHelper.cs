using JS.Core.Foundation.BaseClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JS.Core.Foundation.ExtensionMethods;
using JS.Core.Foundation.Data;

namespace JS.Core.Foundation.Helpers
{
    /// <summary>
    /// SQL Helper
    /// </summary>
    public class SQLHelper : SingletonBase<SQLHelper>
    {
        private static readonly Dictionary<Type, SqlDbType> TypeToSqlDbType
        = new Dictionary<Type, SqlDbType>
              {
                  {typeof (byte), SqlDbType.TinyInt},
                  {typeof (sbyte), SqlDbType.TinyInt},
                  {typeof (short), SqlDbType.SmallInt},
                  {typeof (ushort), SqlDbType.SmallInt},
                  {typeof (int), SqlDbType.Int},
                  {typeof (uint), SqlDbType.Int},
                  {typeof (long), SqlDbType.BigInt},
                  {typeof (ulong), SqlDbType.BigInt},
                  {typeof (float), SqlDbType.Real},
                  {typeof (double), SqlDbType.Float},
                  {typeof (decimal), SqlDbType.Decimal},
                  {typeof (bool), SqlDbType.Bit},
                  {typeof (string), SqlDbType.VarChar},
                  {typeof (char), SqlDbType.Char},
                  {typeof (Guid), SqlDbType.UniqueIdentifier},
                  {typeof (DateTime), SqlDbType.DateTime},
                  {typeof (DateTimeOffset), SqlDbType.DateTimeOffset},
                  {typeof (byte[]), SqlDbType.Binary},
                  {typeof (byte?), SqlDbType.TinyInt},
                  {typeof (sbyte?), SqlDbType.SmallInt},
                  {typeof (short?), SqlDbType.SmallInt},
                  {typeof (ushort?), SqlDbType.SmallInt},
                  {typeof (int?), SqlDbType.Int},
                  {typeof (uint?), SqlDbType.Int},
                  {typeof (long?), SqlDbType.BigInt},
                  {typeof (ulong?), SqlDbType.BigInt},
                  {typeof (float?), SqlDbType.Real},
                  {typeof (double?), SqlDbType.Float},
                  {typeof (decimal?), SqlDbType.Decimal},
                  {typeof (bool?), SqlDbType.Bit},
                  {typeof (char?), SqlDbType.VarChar},
                  {typeof (Guid?), SqlDbType.UniqueIdentifier},
                  {typeof (DateTime?), SqlDbType.DateTime},
                  {typeof (DateTimeOffset?), SqlDbType.DateTimeOffset}
              };
        
        /// <summary>
        /// Adds the SQL parameter.
        /// </summary>
        /// <param name="sqlParameters">The SQL parameters.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="columnValue">The column value.</param>
        /// <param name="connectionInfo">The connection information.</param>
        public void AddSQLParameter(List<SqlParameter> sqlParameters, string columnName, object columnValue, IConnectionInfo connectionInfo)
        {
            if (columnValue == null)
            {
                sqlParameters.Add(new SqlParameter(columnName, DBNull.Value));
                return;
            }

            if (columnValue is DateTime || columnValue is DateTime?)
            {
                columnValue = DateTimeHelper.Current.ConvertLocalToUtc(connectionInfo, (DateTime)columnValue);
            }

            SqlDbType sqlDbType = TypeToSqlDbType[columnValue.GetType()];

            SqlParameter parameter = new SqlParameter();

            parameter.ParameterName = columnName;
            parameter.SqlDbType = sqlDbType;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = columnValue;

            sqlParameters.Add(parameter);
        }

        /// <summary>
        /// Gets the sort direction.
        /// </summary>
        /// <param name="sortDir">The sort dir.</param>
        /// <returns></returns>
        public string GetSortDirection(SortOrder sortDir)
        {
            switch (sortDir)
            {
                case SortOrder.Descending:
                    return "DESC";
                case SortOrder.Ascending:
                case SortOrder.Unspecified:
                default:
                    return "ASC";
            }
        }
    }
}
