using System;
using System.Collections.Generic;
using System.Data;

namespace AutoFillerAdvance
{
    public static class ValidateStations
    {
        //public static List<string> ReturnMatchingStations(string searchstring)
        //{
        //    List<string> lsStations = new List<string>();
        //    DataTable dtSheet3 = new DataTable();
        //    dtSheet3 = ExcelImport.dsExcel.Tables[2];
        //    for (int i = 0; i < dtSheet3.Rows.Count; i++)
        //    {
        //        string currentRow = Convert.ToString(dtSheet3.Rows[i][0]);
        //        lsStations.Add(currentRow);
        //    }
        //    return lsStations;
        //}

        public static List<string> ReturnAllStations()
        {
            List<string> lsAllStations = new List<string>();
            DataTable dtSheet3 = new DataTable();
            dtSheet3 = ExcelImport.DsExcel.Tables[2];
            for (int i = 0; (i < dtSheet3.Rows.Count) ; i++)
            {
                string currentRow = Convert.ToString(dtSheet3.Rows[i][0]);
                lsAllStations.Add(currentRow);
            }
            return lsAllStations;
        }
    }
}
