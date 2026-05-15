using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Utility;

namespace DL
{
  public class ChartDL:DataUtilityTrn
    {
      public ChartDL() { }

      public DataTable GetChartType()
      {
          string procName = "proc_GetchartTypeDdl";
          DataUtility objDut = new DataUtility();
          DataTable dt = objDut.GetDataTableProc(procName);
          return dt;

      }

      public DataTable GetChartDetailsByChartType(string ChartType)
      {
          string procName = "proc_GetChartDetailsByChartType";
          DataUtility objDut = new DataUtility();
          DataTable dt = objDut.GetDataTableProc(procName, ChartType);
          return dt;

      }

    }
}
