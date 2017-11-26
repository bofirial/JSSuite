using JS.Core.Foundation.Data;
using JS.Core.Foundation.Helpers;
using JS.Suite.Foundation.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.SqlServer.Server;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace JS.Suite.CodeGeneration
{
    public partial class JSTools : Form
    {
        List<string> excludedColumns = new List<string>();

        enum ParameterFormatTypes
        {
            ParameterList = 1,
            ParameterListExcludePK = 2,
            ParameterListOnlyPrimaryKey = 3,
            ParameterSelectList = 4,
            ParameterFilterList = 5,
            ParameterUpdate = 6,
            ParameterCommaSeperatedListExcludePK = 7,
            ParameterCommaSeperatedListWithAtExcludePK = 8, 
            ParameterListOnlyPrimaryKeyNoNull = 9,
            ParameterTableCreate = 10
        };

        enum DefaultTypes
        {
            BasedOnColumn = 1,
            NoDefaults = 2,
            AlwaysDefaultWithEqual = 3
        };

        public JSTools()
        {
            InitializeComponent();
            List<string> TableNames;

            ConnectionInfo connectionInfo = new ConnectionInfo();
            connectionInfo.ConnectionString = ConfigurationHelper.Current.GetConnectionString(Databases.JSSuite);

            TableNames = SQLReaderSingleColumn(connectionInfo, "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME NOT IN ('sysdiagrams') ORDER BY TABLE_NAME", "TABLE_NAME");

            foreach (string s in TableNames)
                CGT_TableCheckList.Items.Add(s);

            excludedColumns.Add("RowVersion");

            CGT_DatabaseComboBox.Items.Add(Databases.JSSuite);
            CGT_DatabaseComboBox.Items.Add(Databases.JSSupport);
            CGT_DatabaseComboBox.Items.Add(Databases.JSWedding);

            CGT_DatabaseComboBox.SelectedItem = Databases.JSSuite;

            CGT_SuiteFolderTextBox.Text = "C:\\SVNViews\\trunk\\Suite\\";

            RunScript(connectionInfo, "C:\\SVNViews\\trunk\\Suite\\Database\\JSSuite\\Build\\StoredProcedures\\Generated\\Application_select.sql");
            
        }
        
        private List<string> SQLReaderSingleColumn (IConnectionInfo connectionInfo, string query, string columnName)
        {
            SqlDataReader reader = null;
            SqlConnection connection = new SqlConnection(connectionInfo.ConnectionString);
            List<string> listStrings = new List<string>();

            using (connection)
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>();
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                command.Parameters.AddRange(sqlParameters.ToArray());
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        listStrings.Add(reader[columnName].ToString());
                    }
                }
            }
            return listStrings;
        }

        private void SqlReaderSchemaForTable(ConnectionInfo connectionInfo, string tableName, List<Column> columns)
        {
            SqlDataReader reader = null;
            SqlConnection connection = new SqlConnection(connectionInfo.ConnectionString);
            string MaxLength = "";

            string query = "SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + tableName + "' ORDER BY ORDINAL_POSITION";

            using (connection)
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>();
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                command.Parameters.AddRange(sqlParameters.ToArray());
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Column column = new Column();
                        
                        column.Name = reader["COLUMN_NAME"].ToString();
                        column.Type = reader["DATA_TYPE"].ToString();
                        column.IsNullableFlag = (reader["IS_NULLABLE"].ToString() == "YES" ? true : false);

                        MaxLength = reader["CHARACTER_MAXIMUM_LENGTH"].ToString();
                        if (MaxLength != "")
                            column.MaxLength = Convert.ToInt32(MaxLength);

                        columns.Add(column);
                    }
                }
            }
        }

        private void CodeGenButton_Click(object sender, EventArgs e) 
        {
            ConnectionInfo connectionInfo = new ConnectionInfo();
            connectionInfo.ConnectionString = ConfigurationHelper.Current.GetConnectionString(CGT_DatabaseComboBox.SelectedItem.ToString());

            if (CGT_TableCheckList.CheckedItems.Count > 0)
                FileGenerator(connectionInfo);

        }


        public void FileGenerator(ConnectionInfo connectionInfo)
        {
            string databaseFilePath = "Database\\" + CGT_DatabaseComboBox.SelectedItem.ToString();

            List<Column> columns = new List<Column>();
            foreach (string tableName in CGT_TableCheckList.CheckedItems)
            {
                SqlReaderSchemaForTable(connectionInfo, tableName, columns);

                // Stored Procedures
                writeFile(tableName + "_insert.sql", CGT_SuiteFolderTextBox.Text + databaseFilePath + "\\Build\\StoredProcedures\\Generated\\", TableName_Insert(columns, tableName));
                writeFile(tableName + "_update.sql", CGT_SuiteFolderTextBox.Text + databaseFilePath + "\\Build\\StoredProcedures\\Generated\\", TableName_Update(columns, tableName));
                writeFile(tableName + "_delete.sql", CGT_SuiteFolderTextBox.Text + databaseFilePath + "\\Build\\StoredProcedures\\Generated\\", TableName_Delete(columns, tableName));
                writeFile(tableName + "_select.sql", CGT_SuiteFolderTextBox.Text + databaseFilePath + "\\Build\\StoredProcedures\\Generated\\", TableName_Select(columns, tableName));

                // Tables
                writeFile(tableName + ".sql", CGT_SuiteFolderTextBox.Text + databaseFilePath + "\\Build\\Tables\\", TableName_Create(columns, tableName));
            }
        }


        #region Parameter Formatting

        private string ParameterReplace(
            List<Column> columns
            , string tableName
            , ParameterFormatTypes formatType)
        {
            string primaryTemplate;
            string secondaryTemplate;

            switch (formatType)
            {
                case ParameterFormatTypes.ParameterList:
                    primaryTemplate = "\t  @<<columnName>> <<columnType>> = null";
                    secondaryTemplate = "\r\n\t, @<<columnName>> <<columnType>> = null";
                    return ParameterFormat(columns, tableName, primaryTemplate, secondaryTemplate, true, true);
                case ParameterFormatTypes.ParameterListExcludePK:
                    primaryTemplate = "\t  @<<columnName>> <<columnType>> = null";
                    secondaryTemplate = "\r\n\t, @<<columnName>> <<columnType>> = null";
                    return ParameterFormat(columns, tableName, primaryTemplate, secondaryTemplate, false, true);
                case ParameterFormatTypes.ParameterListOnlyPrimaryKey:
                    primaryTemplate = "\t  @<<columnName>> <<columnType>> = null";
                    secondaryTemplate = "\r\n\t, @<<columnName>> <<columnType>> = null";
                    return ParameterFormat(columns, tableName, primaryTemplate, secondaryTemplate, true, false);
                case ParameterFormatTypes.ParameterListOnlyPrimaryKeyNoNull:
                    primaryTemplate = "\t  @<<columnName>> <<columnType>>";
                    secondaryTemplate = "\r\n\t, @<<columnName>> <<columnType>>";
                    return ParameterFormat(columns, tableName, primaryTemplate, secondaryTemplate, true, false);
                case ParameterFormatTypes.ParameterSelectList:
                    primaryTemplate = "     t0.<<columnName>>";
                    secondaryTemplate = "\r\n\t   , t0.<<columnName>>";
                    return ParameterFormat(columns, tableName, primaryTemplate, secondaryTemplate, true, true);
                case ParameterFormatTypes.ParameterFilterList:
                    primaryTemplate = "WHERE (@<<columnName>> IS NULL OR t0.<<columnName>> = @<<columnName>>)";
                    secondaryTemplate = "\r\n\tAND (@<<columnName>> IS NULL OR t0.<<columnName>> = @<<columnName>>)";
                    return ParameterFormat(columns, tableName, primaryTemplate, secondaryTemplate, true, true);
                case ParameterFormatTypes.ParameterUpdate:
                    primaryTemplate = "  <<columnName>> = ISNULL(@<<columnName>>, <<columnName>>)";
                    secondaryTemplate = "\r\n\t\t, <<columnName>> = ISNULL(@<<columnName>>, <<columnName>>)";
                    return ParameterFormat(columns, tableName, primaryTemplate, secondaryTemplate, false, true);
                case ParameterFormatTypes.ParameterCommaSeperatedListExcludePK:
                    primaryTemplate = "<<columnName>>";
                    secondaryTemplate = ", <<columnName>>";
                    return ParameterFormat(columns, tableName, primaryTemplate, secondaryTemplate, false, true);
                case ParameterFormatTypes.ParameterCommaSeperatedListWithAtExcludePK:
                    primaryTemplate = "@<<columnName>>";
                    secondaryTemplate = ", @<<columnName>>";
                    return ParameterFormat(columns, tableName, primaryTemplate, secondaryTemplate, false, true);
                case ParameterFormatTypes.ParameterTableCreate:
                    primaryTemplate = "\t\t  <<columnName>> <<columnType>> IDENTITY(100000, 1) <<columnNullable>>"; // We are working under the assumption the first column is always the Identity because that is our standard
                    secondaryTemplate = "\r\n\t\t, <<columnName>> <<columnType>> <<columnNullable>>";
                    return ParameterFormat(columns, tableName, primaryTemplate, secondaryTemplate, true, true);
                default:
                    return "";
            }
        }

        private string ParameterFormat(
            List<Column> columns
            , string tableName
            , string primaryTemplate
            , string secondaryTemplate
            , bool includePrimaryKeyFlag = true
            , bool includeNonPrimaryKeyColumnsFlag = true)
        {
            StringBuilder sb = new StringBuilder();

            string primaryKey = tableName + "Id";
            string parameter = "";

            List<string> excludedColumnsLocale = new List<string>();
            excludedColumnsLocale.AddRange(excludedColumns);

            if (!includePrimaryKeyFlag)
                excludedColumnsLocale.Add(primaryKey);

            for (int i = 0; i < columns.Count(); i++)
            {
                if (!excludedColumnsLocale.Contains(columns[i].Name))
                {
                    if (sb.Length == 0 && (includeNonPrimaryKeyColumnsFlag || columns[i].Name == primaryKey))
                        parameter = primaryTemplate;
                    else if (includeNonPrimaryKeyColumnsFlag || columns[i].Name == primaryKey)
                        parameter = secondaryTemplate;

                    if (parameter != "")
                    {
                        parameter = parameter.Replace("<<columnName>>", columns[i].Name);
                        parameter = parameter.Replace("<<columnType>>", columns[i].TypeWithLength);
                        parameter = parameter.Replace("<<columnNullable>>", columns[i].isNullableString);

                        sb.Append(parameter);
                        parameter = "";
                    }
                }
            }

            return sb.ToString();
        }

        #endregion

        #region IO
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="filepath">"c:\\CodeGenTest\\"</param>
        /// <param name="file"></param>
        private void writeFile(string fileName, string filepath, string file)
        {
            if (!Directory.Exists(filepath))
                Directory.CreateDirectory(filepath);

            StreamWriter writer = new StreamWriter(filepath + fileName);

            writer.WriteLine(file);
            writer.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="filepath">"c:\\CodeGenTest\\Templates\\"</param>
        /// <returns></returns>
        private string readFile(string fileName, string filepath)
        {
            StreamReader reader = new StreamReader(filepath + fileName + ".txt");
            StringBuilder sb = new StringBuilder();

            while (reader.Peek() != -1)
            {
                sb.AppendLine(reader.ReadLine());
            }

            return sb.ToString();
        }
        #endregion

        #region Execute Scripts

        private void RunScript(ConnectionInfo connectionInfo, String FileName)
        {
            FileInfo file = new FileInfo(FileName);
            string script = file.OpenText().ReadToEnd();
            
            SqlConnection connection = new SqlConnection(connectionInfo.ConnectionString);

            Server server = new Server(new ServerConnection(connection));
            server.ConnectionContext.ExecuteNonQuery(script);
        }

        #endregion

        private void DatabaseComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConnectionInfo connectionInfo = new ConnectionInfo();
            connectionInfo.ConnectionString = ConfigurationHelper.Current.GetConnectionString(CGT_DatabaseComboBox.SelectedItem.ToString());

            CGT_TableCheckList.Items.Clear();
            List<string> TableNames = SQLReaderSingleColumn(connectionInfo, "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME NOT IN ('sysdiagrams') ORDER BY TABLE_NAME", "TABLE_NAME");

            foreach (string s in TableNames)
                CGT_TableCheckList.Items.Add(s);
        }

        private void SelectAllTableCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < CGT_TableCheckList.Items.Count; i++)
            {
                CGT_TableCheckList.SetItemChecked(i, CGT_SelectAllTableCheckBox.Checked);
            }
        }


        private string TableName_Insert(List<Column> columns, string tableName)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(@"IF EXISTS(	");
            sb.AppendLine(@"	SELECT 1");
            sb.AppendLine(@"	FROM dbo.sysobjects");
            sb.AppendLine(@"	WHERE id = object_Id(N'dbo." + tableName + "_insert')");
            sb.AppendLine(@"	AND OBJECTPROPERTY(id, N'IsProcedure') = 1)");
            sb.AppendLine(@"		DROP PROCEDURE dbo." + tableName + "_insert");
            sb.AppendLine(@"GO");
            sb.AppendLine(@"CREATE PROCEDURE dbo." + tableName + "_insert");
            sb.AppendLine(ParameterReplace(columns, tableName, ParameterFormatTypes.ParameterListExcludePK));
            sb.AppendLine(@"AS");
            sb.AppendLine(@"BEGIN");
            sb.AppendLine(@"	SET NOCOUNT ON");
            sb.AppendLine(@"");
            sb.AppendLine(@"	INSERT INTO dbo." + tableName + " (" + ParameterReplace(columns, tableName, ParameterFormatTypes.ParameterCommaSeperatedListExcludePK) + ")");
            sb.AppendLine(@"	VALUES (" + ParameterReplace(columns, tableName, ParameterFormatTypes.ParameterCommaSeperatedListWithAtExcludePK) + ")");
            sb.AppendLine(@"END");
            sb.Append(@"GO");

            return sb.ToString();
        }
        private string TableName_Update(List<Column> columns, string tableName)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(@"IF EXISTS(	");
            sb.AppendLine(@"	SELECT 1");
            sb.AppendLine(@"	FROM dbo.sysobjects");
            sb.AppendLine(@"	WHERE id = object_Id(N'dbo." + tableName + "_update')");
            sb.AppendLine(@"	AND OBJECTPROPERTY(id, N'IsProcedure') = 1)");
            sb.AppendLine(@"		DROP PROCEDURE dbo." + tableName + "_update");
            sb.AppendLine(@"GO");
            sb.AppendLine(@"CREATE PROCEDURE dbo." + tableName + "_update");
            sb.AppendLine(ParameterReplace(columns, tableName, ParameterFormatTypes.ParameterList));
            sb.AppendLine(@"AS");
            sb.AppendLine(@"BEGIN");
            sb.AppendLine(@"	SET NOCOUNT ON");
            sb.AppendLine(@"");
            sb.AppendLine(@"	UPDATE dbo." + tableName);
            sb.AppendLine(@"	SET " + ParameterReplace(columns, tableName, ParameterFormatTypes.ParameterUpdate) + "");
            sb.AppendLine(@"	WHERE " + tableName + "Id = @" + tableName + "Id");
            sb.AppendLine(@"");
            sb.AppendLine(@"END");
            sb.Append(@"GO");

            return sb.ToString();
        }
        private string TableName_Select(List<Column> columns, string tableName)
        {
            StringBuilder sb = new StringBuilder();

            string s = ParameterReplace(columns, tableName, ParameterFormatTypes.ParameterCommaSeperatedListExcludePK);

            sb.AppendLine(@"IF EXISTS(	");
            sb.AppendLine(@"	SELECT 1");
            sb.AppendLine(@"	FROM dbo.sysobjects");
            sb.AppendLine(@"	WHERE id = object_Id(N'dbo." + tableName + "_select')");
            sb.AppendLine(@"	AND OBJECTPROPERTY(id, N'IsProcedure') = 1)");
            sb.AppendLine(@"		DROP PROCEDURE dbo." + tableName + "_select");
            sb.AppendLine(@"GO");
            sb.AppendLine(@"CREATE PROCEDURE dbo." + tableName + "_select");
            sb.AppendLine(ParameterReplace(columns, tableName, ParameterFormatTypes.ParameterList));
            sb.AppendLine(@"AS");
            sb.AppendLine(@"BEGIN");
            sb.AppendLine(@"	SET NOCOUNT ON");
            sb.AppendLine(@"");
            sb.AppendLine(@"	SELECT ");
            sb.AppendLine(@"	" + ParameterReplace(columns, tableName, ParameterFormatTypes.ParameterSelectList));
            sb.AppendLine(@"	FROM dbo." + tableName + " t0 WITH (NOLOCK)");
            sb.AppendLine(@"	" + ParameterReplace(columns, tableName, ParameterFormatTypes.ParameterFilterList));
            sb.AppendLine(@"END");
            sb.Append(@"GO");

            return sb.ToString();
        }
        private string TableName_Delete(List<Column> columns, string tableName)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(@"IF EXISTS(	");
            sb.AppendLine(@"	SELECT 1");
            sb.AppendLine(@"	FROM dbo.sysobjects");
            sb.AppendLine(@"	WHERE id = object_Id(N'dbo." + tableName + "_delete')");
            sb.AppendLine(@"	AND OBJECTPROPERTY(id, N'IsProcedure') = 1)");
            sb.AppendLine(@"		DROP PROCEDURE dbo." + tableName + "_delete");
            sb.AppendLine(@"GO");
            sb.AppendLine(@"CREATE PROCEDURE dbo." + tableName + "_delete");
            sb.AppendLine(ParameterReplace(columns, tableName, ParameterFormatTypes.ParameterListOnlyPrimaryKeyNoNull));
            sb.AppendLine(@"AS");
            sb.AppendLine(@"BEGIN");
            sb.AppendLine(@"	SET NOCOUNT ON");
            sb.AppendLine(@"");
            sb.AppendLine(@"	DELETE dbo." + tableName);
            sb.AppendLine(@"	FROM dbo." + tableName);
            sb.AppendLine(@"	WHERE " + tableName + "Id = @" + tableName + "Id");
            sb.AppendLine(@"");
            sb.AppendLine(@"END");
            sb.Append(@"GO");

            return sb.ToString();
        }

        private string TableName_Create(List<Column> columns, string tableName)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(@"IF NOT EXISTS (SELECT 1 FROM Information_Schema.Tables WHERE TABLE_NAME = '" + tableName + "')");
            sb.AppendLine(@"BEGIN");
            sb.AppendLine(@"	CREATE TABLE " + tableName);
            sb.AppendLine(@"	(");
            sb.AppendLine(ParameterReplace(columns, tableName, ParameterFormatTypes.ParameterTableCreate));
            sb.AppendLine(@"	)");
            sb.Append(@"END");

            return sb.ToString();
        }
    }

    public class Column
    {
        public string Name;
        public string Type;
        public bool IsNullableFlag;
        public int MaxLength;

        public string TypeWithLength
        {
            get
            {
                if (MaxLength > 0)
                    return Type + "(" + MaxLength + ")";
                else
                    return Type;
            }
        }

        public string isNullableString
        {
            get
            {
                if (IsNullableFlag)
                    return "null";
                else
                    return "not null";
            }
        }

    }
}
