using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;

namespace ImageReport.Helper
{
    public class ExcelFile
    {
        //string FilePath;

        OleDbConnectionStringBuilder csbuilder;

        //----------------------------------
        public ExcelFile(string _filePath)
        {
            //FilePath = _filePath;

            csbuilder = new OleDbConnectionStringBuilder();

            csbuilder.Provider = "Microsoft.ACE.OLEDB.12.0";
            csbuilder.DataSource = _filePath;
            csbuilder.Add("Extended Properties", "Excel 12.0 Xml; HDR=YES");
        }

        //-----------------------------------------
        public DataSet GetDropDownList()
        {

            DataSet ds = new DataSet();

            using (OleDbConnection connection = new OleDbConnection(csbuilder.ConnectionString))
            {
                connection.Open();

                ds.Tables.Add(getTable(connection, @"SELECT item FROM [Observation$]"));

                ds.Tables.Add(getTable(connection, @"SELECT item FROM [Deficiency$]"));

                ds.Tables.Add(getTable(connection, @"SELECT item FROM [Recommendation$]"));

                connection.Close();
            }

            return ds;
        }


        //-----------------------------------------
        public bool InsertPickListItem(string tableName, string NewItem)
        {
             using (OleDbConnection connection = new OleDbConnection(csbuilder.ConnectionString))
             {
                using (OleDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO [" + tableName + "$] (item) " +
                                          "VALUES ('" + NewItem + "')";

                    connection.Open();

                    command.ExecuteNonQuery();
                }

                connection.Close();
             }

             return true;
        }


        //------------------------------------------------------
        private DataTable getTable(OleDbConnection connection, string sSQL)
        {
            DataTable dt = new DataTable();

            using (OleDbDataAdapter adapter = new OleDbDataAdapter(sSQL, connection))
            {
                adapter.Fill(dt);
            }

            return dt;
        }
    }       
}