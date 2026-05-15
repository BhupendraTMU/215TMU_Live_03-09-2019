using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;
using PL;
using System.Data;


namespace DL
{
    public class PersonalInformationDL : DataUtility
    {
        public PersonalInformationDL()
        {
        }
        public List<PersonalInformationPL> GetPersonalInformation(String ID)
        {
            List<PersonalInformationPL> objPersonalInformationList = new List<PersonalInformationPL>();
            string procName = "proc_GetPersonalInformation";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName,ID);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                objPersonalInformationList.Add(new PersonalInformationPL(
                      dt.Rows[i][0].ToString(),
                      dt.Rows[i][1].ToString(),
                      dt.Rows[i][2].ToString(),
                      dt.Rows[i][3].ToString(),
                      dt.Rows[i][4].ToString(),
                      dt.Rows[i][5].ToString(),
                      dt.Rows[i][6].ToString(),
                      dt.Rows[i][7].ToString(),
                      dt.Rows[i][8].ToString(),
                      dt.Rows[i][9].ToString(),
                      dt.Rows[i][10].ToString(),
                      dt.Rows[i][11].ToString(),
                      dt.Rows[i][12].ToString(),
                      dt.Rows[i][13].ToString(),
                      dt.Rows[i][14].ToString(),
                      dt.Rows[i][15].ToString(),
                      dt.Rows[i][16].ToString(),
                      dt.Rows[i][17].ToString(),
                      dt.Rows[i][18].ToString(),
                      dt.Rows[i][19].ToString(),
                      dt.Rows[i][20].ToString(),
                      dt.Rows[i][21].ToString(),
                      dt.Rows[i][22].ToString(),
                      dt.Rows[i][23].ToString(),
                      dt.Rows[i][24].ToString(),
                      dt.Rows[i][25].ToString(),
                      dt.Rows[i][26].ToString()));                     
            }
            return objPersonalInformationList;
        }
    }
}
