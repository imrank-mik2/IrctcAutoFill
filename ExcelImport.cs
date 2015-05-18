using System.Data;
using System.Data.OleDb;
using System.Globalization;

namespace AutoFillerAdvance
{
    class ExcelImport
    {
        #region Redundant
        //public static DataTable DtTbl;
        //public static DataTable Exceldata(string filePath)
        //{
        //    DataTable dtexcel = new DataTable();
        //    bool hasHeaders = false;
        //    string hdr = hasHeaders ? "Yes" : "No";
        //    string strConn;
        //    if (filePath.Substring(filePath.LastIndexOf('.')).ToLower() == ".xlsx")
        //        strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0;HDR=" + hdr + ";IMEX=0\"";
        //    else
        //        strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=" + hdr + ";IMEX=0\"";
        //    OleDbConnection conn = new OleDbConnection(strConn);
        //    conn.Open();
        //    DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
        //    //Looping Total Sheet of Xl File
        //    /*foreach (DataRow schemaRow in schemaTable.Rows)
        //    {
        //    }*/
        //    //Looping a first Sheet of Xl File
        //    DataRow schemaRow = schemaTable.Rows[0];
        //    string sheet = schemaRow["TABLE_NAME"].ToString();
        //    if (!sheet.EndsWith("_"))
        //    {
        //        string query = "SELECT  * FROM [Sheet1$]";
        //        OleDbDataAdapter daexcel = new OleDbDataAdapter(query, conn);
        //        dtexcel.Locale = CultureInfo.CurrentCulture;
        //        daexcel.Fill(dtexcel);
        //    }

        //    conn.Close();
        //    return dtexcel;

        //} 
        #endregion

        public static DataSet DsExcel=new DataSet();
        public static DataSet ExcelData(string filePath)
        {
            //DataSet dsExcel = new DataSet();
            bool hasHeaders = false;
            string hdr = hasHeaders ? "Yes" : "No";
            string strConn;
            if (filePath.Substring(filePath.LastIndexOf('.')).ToLower() == ".xlsx")
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0;HDR=" + hdr + ";IMEX=0\"";
            else
                strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=" + hdr + ";IMEX=0\"";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            //Looping Total Sheet of Xl File
            int i = 1;
            foreach (DataRow schemaRow in schemaTable.Rows)
            {
                string sheet = schemaRow["TABLE_NAME"].ToString();
                if (!sheet.EndsWith("_"))
                {
                    DataTable dtexcel = new DataTable();
                    string query = "SELECT  * FROM [Sheet" + i + "$]";
                    OleDbDataAdapter daexcel = new OleDbDataAdapter(query, conn);
                    dtexcel.Locale = CultureInfo.CurrentCulture;
                    daexcel.Fill(dtexcel);
                    DsExcel.Tables.Add(dtexcel);

                }
                i++;
            }
            //Looping a first Sheet of Xl File
            //DataRow schemaRow = schemaTable.Rows[0];
            
            conn.Close();
            return DsExcel;
        }

    }
}
