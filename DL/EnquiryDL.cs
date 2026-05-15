using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;
using PL;
using System.Data;

namespace DL
{
    public class EnquiryDL : DataUtilityTrn
    {
        public EnquiryDL()
        { }
        public List<EnquiryPL> GetEnquiryDdl(string UserId)
        {
            List<EnquiryPL> objEnquiryList = new List<EnquiryPL>();
            string procName = "proc_GetEnquiryDdl";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName, UserId);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                objEnquiryList.Add(new EnquiryPL(
                      dt.Rows[i][0].ToString(),
                      dt.Rows[i][1].ToString()));
            }
            return objEnquiryList;
        }
        public List<EnquiryPL> GetEnquiryDdl()
        {
            List<EnquiryPL> objEnquiryList = new List<EnquiryPL>();
            string procName = "proc_GetEnquiryDdl";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                objEnquiryList.Add(new EnquiryPL(
                      dt.Rows[i][0].ToString(),
                      dt.Rows[i][1].ToString()));
            }
            return objEnquiryList;
        }
        public DataTable GetSessionDdl()
        {
            string procName = "proc_GetSessionDdl";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName);
            return dt;
        }
        public DataTable GetCourseDdl(string UserId)
        {
            string procName = "proc_GetCourseDdl";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName, UserId);
            return dt;
        }
        public DataTable GetCourseDdl() // Used on ->OnlineENquiry, Application Online,
        {
            string procName = "proc_GetCourseDdl";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName);
            return dt;
        }
        public DataTable GetCategoryDdl(string ReligionCode)
        {
            string procName = "proc_GetCategoryDdl";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName, ReligionCode);
            return dt;
        }
        public DataTable GetSourceDdl()
        {
            string procName = "proc_GetSourceDdl";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName);
            return dt;
        }
        public DataTable GetReligionDdl()
        {
            string procName = "proc_GetReligionDdl";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName);
            return dt;
        }
        public DataTable GetSubReligionDdl()
        {
            string procName = "proc_GetSubReligionDdl";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName);
            return dt;
        }
        public DataTable GetGenderDdl()
        {
            string procName = "proc_GetGenderDdl";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName);
            return dt;
        }
        public DataTable GetNationalityDdl()
        {
            string procName = "proc_GetNationalityDdl";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName);
            return dt;
        }
        public DataTable GetFeeTypeDdl()
        {
            string procName = "proc_GetFeeTypeDdl";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName);
            return dt;
        }
        public DataTable GetNameOfMediaDdl()
        {
            string procName = "proc_GetNameOfMediaDdl";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName);
            return dt;
        }
        public DataTable GetEnquiryTypeDdl()
        {
            string procName = "proc_GetEnquiryTypeDdl";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName);
            return dt;
        }

        public DataTable GetPrequalificationDdl()
        {
            string procName = "proc_GetPrequalificationDdl";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName);
            return dt;
        }

        public DataTable GetFollowUpStatusDdl()
        {
            string procName = "proc_GetFollowUpStatusDdl";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName);
            return dt;
        }
        public DataTable GetSearchEnquiryDdl()
        {
            string procName = "proc_SearchEnquiryDDL";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName);
            return dt;
        }
        public List<EnquiryPL> GetEnquiryDetails_ByENquiryId(String ID)
        {
            List<EnquiryPL> objEnquiryDetailList = new List<EnquiryPL>();
            string procName = "proc_GetEnquiryDetail_ByEnqID";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName, ID);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                objEnquiryDetailList.Add(new EnquiryPL(
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
                      Convert.ToInt32(dt.Rows[i][15].ToString()),//boolean
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
                      dt.Rows[i][26].ToString(),
                      dt.Rows[i][27].ToString(),
                      dt.Rows[i][28].ToString(),
                      dt.Rows[i][29].ToString(),
                      Convert.ToInt32(dt.Rows[i][30].ToString()),
                      dt.Rows[i][31].ToString(),
                      dt.Rows[i][32].ToString(),
                      dt.Rows[i][33].ToString(),
                      Convert.ToInt32(dt.Rows[i][34].ToString()), //boolean
                      Convert.ToInt32(dt.Rows[i][35].ToString()),
                      Convert.ToInt32(dt.Rows[i][36].ToString()),
                      dt.Rows[i][37].ToString(),
                      dt.Rows[i][38].ToString(),
                      dt.Rows[i][39].ToString(),
                      dt.Rows[i][40].ToString(),
                      dt.Rows[i][41].ToString(),
                      Convert.ToInt32(dt.Rows[i][42].ToString()),
                      dt.Rows[i][43].ToString(),
                      Convert.ToInt32(dt.Rows[i][44].ToString())));

            }
            return objEnquiryDetailList;
        }

        public string SaveEnquiry(List<EnquiryPL> objEnquiryList, string UserName)
        {
            string ReturnID;
            OpenConnection();
            string[] ParamName = new string[45];
            object[] ParamValue = new object[45];

            ParamValue[0] = objEnquiryList[0].No_;
            ParamName[0] = "No_";
            ParamValue[1] = objEnquiryList[0].EnquiryDate;
            ParamName[1] = "EnquiryDate";
            ParamValue[2] = objEnquiryList[0].EnquiryType;
            ParamName[2] = "EnquiryType";
            ParamValue[3] = objEnquiryList[0].EnquirySource;
            ParamName[3] = "EnquirySource";
            ParamValue[4] = objEnquiryList[0].NameoftheMedia;
            ParamName[4] = "NameoftheMedia";
            ParamValue[5] = objEnquiryList[0].EnquirerName;
            ParamName[5] = "EnquirerName";
            ParamValue[6] = objEnquiryList[0].ApplicantRelationship;
            ParamName[6] = "ApplicantRelationship";
            ParamValue[7] = objEnquiryList[0].ApplicantName;
            ParamName[7] = "ApplicantName";
            ParamValue[8] = objEnquiryList[0].DateofBirth;
            ParamName[8] = "DateofBirth";
            ParamValue[9] = objEnquiryList[0].Father_sName;
            ParamName[9] = "Father_sName";
            ParamValue[10] = objEnquiryList[0].Mother_sName;
            ParamName[10] = "Mother_sName";
            ParamValue[11] = objEnquiryList[0].ApplicantStatus;
            ParamName[11] = "ApplicantStatus";
            ParamValue[12] = objEnquiryList[0].AcademicYear;
            ParamName[12] = "AcademicYear";
            ParamValue[13] = objEnquiryList[0].CourseCode;
            ParamName[13] = "CourseCode";
            ParamValue[14] = objEnquiryList[0].UniversityInterested;
            ParamName[14] = "UniversityInterested";
            ParamValue[15] = objEnquiryList[0].HostelAcommodation;
            ParamName[15] = "HostelAcommodation";
            ParamValue[16] = objEnquiryList[0].Prequalification;
            ParamName[16] = "Prequalification";
            ParamValue[17] = objEnquiryList[0].NameofthePreviousInstitute;
            ParamName[17] = "NameofthePreviousInstitute";
            ParamValue[18] = objEnquiryList[0].CertificationAuthoriry;
            ParamName[18] = "CertificationAuthoriry";
            ParamValue[19] = objEnquiryList[0].MediumofInstruction;
            ParamName[19] = "MediumofInstruction";
            ParamValue[20] = objEnquiryList[0].Addressto;
            ParamName[20] = "Addressto";
            ParamValue[21] = objEnquiryList[0].Addressee;
            ParamName[21] = "Addressee";
            ParamValue[22] = objEnquiryList[0].Address1;
            ParamName[22] = "Address1";
            ParamValue[23] = objEnquiryList[0].Address2;
            ParamName[23] = "Address2";
            ParamValue[24] = objEnquiryList[0].City;
            ParamName[24] = "City";
            ParamValue[25] = objEnquiryList[0].PostCode;
            ParamName[25] = "PostCode";
            ParamValue[26] = objEnquiryList[0].CountryCode;
            ParamName[26] = "CountryCode";
            ParamValue[27] = objEnquiryList[0].EMailAddress;
            ParamName[27] = "EMailAddress";
            ParamValue[28] = objEnquiryList[0].MobileNumber;
            ParamName[28] = "MobileNumber";
            ParamValue[29] = objEnquiryList[0].PhoneNumber;
            ParamName[29] = "PhoneNumber";
            ParamValue[30] = objEnquiryList[0].Gender;
            ParamName[30] = "Gender";
            ParamValue[31] = objEnquiryList[0].State;
            ParamName[31] = "State";
            ParamValue[32] = objEnquiryList[0].No_Series;
            ParamName[32] = "No_Series";
            ParamValue[33] = objEnquiryList[0].Address3;
            ParamName[33] = "Address3";
            ParamValue[34] = objEnquiryList[0].ConvertedtoApplication;
            ParamName[34] = "ConvertedtoApplication";
            ParamValue[35] = objEnquiryList[0].Age;
            ParamName[35] = "Age";
            ParamValue[36] = objEnquiryList[0].Months;
            ParamName[36] = "Months";
            ParamValue[37] = objEnquiryList[0].UserID;
            ParamName[37] = "UserID";
            ParamValue[38] = objEnquiryList[0].PortalID;
            ParamName[38] = "PortalID";
            ParamValue[39] = objEnquiryList[0].CollegeInterested;
            ParamName[39] = "CollegeInterested";
            ParamValue[40] = objEnquiryList[0].Category;
            ParamName[40] = "Category";
            ParamValue[41] = objEnquiryList[0].Religion;
            ParamName[41] = "Religion";
            ParamValue[42] = objEnquiryList[0].SubReligion;
            ParamName[42] = "SubReligion";
            ParamValue[43] = objEnquiryList[0].Remarks_Feedback;
            ParamName[43] = "Remarks_Feedback";
            ParamValue[44] = objEnquiryList[0].FeeType;
            ParamName[44] = "FeeType";

            try
            {
                ReturnID = ExecScalarCmdParams("proc_SaveEnquiry", ParamName, ParamValue);
                CommitTransaction();
            }
            catch
            {
                RollBackTrn();
                ReturnID = "Error";
            }
            finally
            {
                CloseConnection();

            }
            return ReturnID;



        }
        public List<EnquiryPL> GetEnquiryList(string UserID)
        {
            List<EnquiryPL> objEnquiryList = new List<EnquiryPL>();
            string procName = "proc_GetEnquiryList";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName, UserID);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                objEnquiryList.Add(new EnquiryPL(
                      dt.Rows[i][0].ToString(),
                      dt.Rows[i][1].ToString(),
                      dt.Rows[i][2].ToString(),
                      dt.Rows[i][3].ToString(),
                      dt.Rows[i][4].ToString(),
                      dt.Rows[i][5].ToString(),
                      dt.Rows[i][6].ToString(),
                      dt.Rows[i][7].ToString(),
                      dt.Rows[i][8].ToString() == "01 Jan 1753" ? "" : dt.Rows[i][8].ToString(),
                      dt.Rows[i][9].ToString(),
                      dt.Rows[i][10].ToString(),
                      dt.Rows[i][11].ToString(),
                      dt.Rows[i][12].ToString(),
                      dt.Rows[i][13].ToString(),
                      dt.Rows[i][14].ToString(),
                      Convert.ToInt32(dt.Rows[i][15].ToString()),//boolean
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
                      dt.Rows[i][26].ToString(),
                      dt.Rows[i][27].ToString(),
                      dt.Rows[i][28].ToString(),
                      dt.Rows[i][29].ToString(),
                      Convert.ToInt32(dt.Rows[i][30].ToString()),
                      dt.Rows[i][31].ToString(),
                      dt.Rows[i][32].ToString(),
                      dt.Rows[i][33].ToString(),
                      Convert.ToInt32(dt.Rows[i][34].ToString()), //boolean
                      Convert.ToInt32(dt.Rows[i][35].ToString()),
                      Convert.ToInt32(dt.Rows[i][36].ToString()),
                      dt.Rows[i][37].ToString(),
                      dt.Rows[i][38].ToString(),
                      dt.Rows[i][39].ToString(),
                      dt.Rows[i][40].ToString(),
                      dt.Rows[i][41].ToString(),
                      Convert.ToInt32(dt.Rows[i][42].ToString()),
                      dt.Rows[i][43].ToString(),
                      Convert.ToInt32(dt.Rows[i][44].ToString())));
            }
            return objEnquiryList;
        }
        public List<EnquiryPL> GetFollowUpList_ByENquiryId(String ID)
        {
            List<EnquiryPL> objFollowUpList = new List<EnquiryPL>();
            string procName = "proc_GetFollowUp_ByEnqID";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName, ID);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                objFollowUpList.Add(new EnquiryPL(
                      dt.Rows[i]["No_"].ToString(),
                      Convert.ToInt32(dt.Rows[i]["Line No"].ToString()),
                      Convert.ToInt32(dt.Rows[i]["Follow Up Status"].ToString()),
                      dt.Rows[i]["Next Follow Up Date"].ToString(),
                      dt.Rows[i]["Remarks"].ToString(),
                      dt.Rows[i]["Follow Up Status Name"].ToString()));
            }
            return objFollowUpList;
        }
        public string SaveFollowUp(List<EnquiryPL> objFollowUpList, string UserName)
        {
            string Message;
            OpenConnection();
            string[] ParamName = new string[5];
            object[] ParamValue = new object[5];

            ParamValue[0] = objFollowUpList[0].No_;
            ParamName[0] = "No_";
            ParamValue[1] = objFollowUpList[0].LineNo;
            ParamName[1] = "LineNo";
            ParamValue[2] = objFollowUpList[0].FollowUpStatus;
            ParamName[2] = "FollowUpStatus";
            ParamValue[3] = objFollowUpList[0].NextFollowUpDate;
            ParamName[3] = "NextFollowUpDate";
            ParamValue[4] = objFollowUpList[0].Remarks;
            ParamName[4] = "Remarks";
            try
            {
                Message = ExecScalarCmdParams("proc_SaveFollowUp", ParamName, ParamValue);
                CommitTransaction();
            }
            catch
            {
                RollBackTrn();
                Message = "Error";
            }
            finally
            {
                CloseConnection();

            }
            return Message;



        }


        //===============================Pramod=====================
        public DataTable GetApplicationStatusDdl()
        {
            string procName = "proc_GetApplicationStatusDdl";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName);
            return dt;
        }
        public DataTable GetMediumInstructionDdl()
        {
            string procName = "proc_GetMediumInstructionDdl";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName);
            return dt;
        }

        public DataTable GetCourseAge(string CourseCode)
        {
            string procName = "proc_GetCourseAge";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName,CourseCode);
            return dt;
           
        }

        public DataTable GetCityDdl()
        {
            string procName = "proc_GetCityDdl";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName);
            return dt;

        }
        public DataTable GetSubjectDdl(string CourseCode)
        {
            string procName = "proc_GetSubjectDdl";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName,CourseCode);
            return dt;

        }
    }
}
