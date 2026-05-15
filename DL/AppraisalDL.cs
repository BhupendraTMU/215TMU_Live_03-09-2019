using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Utility;
namespace DL
{
    public class AppraisalDL : DataUtility
    {
        public AppraisalDL()
        { }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
        public DataTable GetPersonalDetails(string UserID ,string Acadmicyear)
        {
            string procName = "sp_GetAppraisalPersonalDetails";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName, UserID, Acadmicyear);
            return dt;
        }
        public DataTable GetTeachingEngagements(String UserID, String ODDEVEN)
        {
            string procName = "sp_GetTeachingEngagements"; 
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName,UserID,ODDEVEN);
            return dt;
        }
        public DataTable GetResults(String UserID)
        {
            string procName = "sp_GetResultsforAppraisal";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName, UserID);
            return dt;
        }
        public string SaveProjectAndDissertation(string Level, string TitleOfProj, string NameOfStudent, string Particulars,string UserId,string AcademicSession)
        {
            string ReturnID;
            OpenConnection();
            string[] ParamName = new string[6];
            object[] ParamValue = new object[6];

            ParamValue[0] = Level;
            ParamName[0] = "Level";
            ParamValue[1] = TitleOfProj;
            ParamName[1] = "TitleOfProj";
            ParamValue[2] = NameOfStudent;
            ParamName[2] = "NameOfStudent";
            ParamValue[3] = Particulars;
            ParamName[3] = "Particulars";
            ParamValue[4] = UserId;
            ParamName[4] = "UserId";
            ParamValue[5] = AcademicSession;
            ParamName[5] = "AcademicSession";
            

            try
            {

                ReturnID = ExecScalarCmdText("[proc_insertAppraisal_ProjectAndDissertation]", ParamName, ParamValue);

            }
            catch
            {
                ReturnID = "Something went wrong Try Again....";
            }
            finally
            {
                CloseConnection();

            }
            return ReturnID;
        }
        public DataTable GetProjectAndDissertation(string UserId, string AcademicSession)
        {
            string procName = "proc_GetAppraisal_ProjectAndDissertation";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName, UserId, AcademicSession);
            return dt;
        }
        public DataTable DeleteProjectAndDissertation(int ID, string UserId)
        {
            string procName = "proc_DeleteAppraisal_ProjectAndDissertation";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName, ID, UserId);
            return dt;
        }
        public string SaveManagementAndInstitutional(string DepartmentLevel, string Committee, string RoleAndResponsibility, string UserId, string AcademicSession)
        {
            string ReturnID;
            OpenConnection();
            string[] ParamName = new string[5];
            object[] ParamValue = new object[5];

            ParamValue[0] = DepartmentLevel;
            ParamName[0] = "DepartmentLevel";
            ParamValue[1] = Committee;
            ParamName[1] = "Committee";
            ParamValue[2] = RoleAndResponsibility;
            ParamName[2] = "RoleAndResponsibility";           
            ParamValue[3] = UserId;
            ParamName[3] = "UserId";
            ParamValue[4] = AcademicSession;
            ParamName[4] = "AcademicSession";
            try
            {
                ReturnID = ExecScalarCmdText("[proc_insertAppraisal_ManagementAndInstitutional]", ParamName, ParamValue);
            }
            catch
            {
                ReturnID = "Something went wrong Try Again....";
            }
            finally
            {
                CloseConnection();

            }
            return ReturnID;
        }
        public DataTable GetManagementAndInstitutional(string UserId, string AcademicSession)
        {
            string procName = "proc_GetAppraisal_ManagementAndInstitutional";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName, UserId, AcademicSession);
            return dt;
        }
        public DataTable DeleteManagementAndInstitutional(int ID, string UserId)
        {
            string procName = "proc_DeleteAppraisal_ManagementAndInstitutional";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName, ID, UserId);
            return dt;
        }

        public string SavePhDResearch(string NameOfStudent, string RegistrationOfYear, string ThesisTitle, string OtherSupervisor, string CompletedOngoing, string UserId, string AcademicSession)
        {
            string ReturnID;
            OpenConnection();
            string[] ParamName = new string[7];
            object[] ParamValue = new object[7];

            ParamValue[0] = NameOfStudent;
            ParamName[0] = "NameOfStudent";
            ParamValue[1] = RegistrationOfYear;
            ParamName[1] = "RegistrationOfYear";
            ParamValue[2] = ThesisTitle;
            ParamName[2] = "ThesisTitle";
            ParamValue[3] = OtherSupervisor;
            ParamName[3] = "OtherSupervisor";
            ParamValue[4] = CompletedOngoing;
            ParamName[4] = "CompletedOngoing";
            ParamValue[5] = UserId;
            ParamName[5] = "UserId";
            ParamValue[6] = AcademicSession;
            ParamName[6] = "AcademicSession";
            try
            {
                ReturnID = ExecScalarCmdText("[proc_insertAppraisal_PhDResearch]", ParamName, ParamValue);
            }
            catch
            {
                ReturnID = "Something went wrong Try Again....";
            }
            finally
            {
                CloseConnection();

            }
            return ReturnID;
        }
        public DataTable GetPhDResearch(string UserId, string AcademicSession)
        {
            string procName = "proc_GetAppraisal_PhDResearch";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName, UserId, AcademicSession);
            return dt;
        }
        public DataTable DeletePhDResearch(int ID, string UserId)
        {
            string procName = "proc_DeleteAppraisal_PhDResearch";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName, ID, UserId);
            return dt;
        }

        

        public string SaveRefereedJournalPaper(string TitleOfPaper, string JournalName, string ConferenceName, string ConferencePlace, string VolNo, string Year, string PageNo, string UserId, string AcademicSession)
        {
            string ReturnID;
            OpenConnection();
            string[] ParamName = new string[9];
            object[] ParamValue = new object[9];

            ParamValue[0] = TitleOfPaper;
            ParamName[0] = "TitleOfPaper";
            ParamValue[1] = JournalName;
            ParamName[1] = "JournalName";
            ParamValue[2] = ConferenceName;
            ParamName[2] = "ConferenceName";
            ParamValue[3] = ConferencePlace;
            ParamName[3] = "ConferencePlace";
            ParamValue[4] = VolNo;
            ParamName[4] = "VolNo";
            ParamValue[5] = Year;
            ParamName[5] = "Year";
            ParamValue[6] = PageNo;
            ParamName[6] = "PageNo";
            ParamValue[7] = UserId;
            ParamName[7] = "UserId";
            ParamValue[8] = AcademicSession;
            ParamName[8] = "AcademicSession";
            try
            {
                ReturnID = ExecScalarCmdText("[proc_insertAppraisal_RefereedJournalPaper]", ParamName, ParamValue);
            }
            catch
            {
                ReturnID = "Something went wrong Try Again....";
            }
            finally
            {
                CloseConnection();

            }
            return ReturnID;
        }
        public DataTable GetRefereedJournalPaper(string UserId, string AcademicSession)
        {
            string procName = "proc_GetAppraisal_RefereedJournalPaper";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName, UserId, AcademicSession);
            return dt;
        }
        public DataTable DeleteRefereedJournalPaper(int ID, string UserId)
        {
            string procName = "proc_DeleteAppraisal_RefereedJournalPaper";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName, ID, UserId);
            return dt;
        }

        public string SaveAuthors(string AuthorsName, string Title, string Publisher, string VolNo, string Year, string PageNo, string UserId, string AcademicSession)
        {
            string ReturnID;
            OpenConnection();
            string[] ParamName = new string[8];
            object[] ParamValue = new object[8];

            ParamValue[0] = AuthorsName;
            ParamName[0] = "AuthorsName";
            ParamValue[1] = Title;
            ParamName[1] = "Title";
            ParamValue[2] = Publisher;
            ParamName[2] = "Publisher";            
            ParamValue[3] = VolNo;
            ParamName[3] = "VolNo";
            ParamValue[4] = Year;
            ParamName[4] = "Year";
            ParamValue[5] = PageNo;
            ParamName[5] = "PageNo";
            ParamValue[6] = UserId;
            ParamName[6] = "UserId";
            ParamValue[7] = AcademicSession;
            ParamName[7] = "AcademicSession";
            try
            {
                ReturnID = ExecScalarCmdText("[proc_insertAppraisal_Authors]", ParamName, ParamValue);
            }
            catch
            {
                ReturnID = "Something went wrong Try Again....";
            }
            finally
            {
                CloseConnection();

            }
            return ReturnID;
        }
        public DataTable GetAuthors(string UserId, string AcademicSession)
        {
            string procName = "proc_GetAppraisal_Authors";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName, UserId, AcademicSession);
            return dt;
        }
        public DataTable DeleteAuthors(int ID, string UserId)
        {
            string procName = "proc_DeleteAppraisal_Authors";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName, ID, UserId);
            return dt;
        }

        public string SaveTechReport(string ReportTitle, string Particulars, string Authors, string Remarks, string UserId, string AcademicSession)
        {
            string ReturnID;
            OpenConnection();
            string[] ParamName = new string[6];
            object[] ParamValue = new object[6];

            ParamValue[0] = ReportTitle;
            ParamName[0] = "ReportTitle";
            ParamValue[1] = Particulars;
            ParamName[1] = "Particulars";
            ParamValue[2] = Authors;
            ParamName[2] = "Authors";
            ParamValue[3] = Remarks;
            ParamName[3] = "Remarks";            
            ParamValue[4] = UserId;
            ParamName[4] = "UserId";
            ParamValue[5] = AcademicSession;
            ParamName[5] = "AcademicSession";
            try
            {
                ReturnID = ExecScalarCmdText("[proc_insertAppraisal_TechReport]", ParamName, ParamValue);
            }
            catch
            {
                ReturnID = "Something went wrong Try Again....";
            }
            finally
            {
                CloseConnection();

            }
            return ReturnID;
        }
        public DataTable GetTechReport(string UserId, string AcademicSession)
        {
            string procName = "proc_GetAppraisal_TechReport";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName, UserId, AcademicSession);
            return dt;
        }
        public DataTable DeleteTechReport(int ID, string UserId)
        {
            string procName = "proc_DeleteAppraisal_TechReport";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName, ID, UserId);
            return dt;
        }

        public string SaveSponsorConsultancyProjects(string ProjectTitle, string FundingAgency, string FinancialOutlay, string YearStartAndTotalPeriod, string NameOfPI, string CurrentStatus, string SponsorConsultancyType, string UserId, string AcademicSession)
        {
            string ReturnID;
           // OpenConnection();
            string[] ParamName = new string[9];
            object[] ParamValue = new object[9];

            ParamValue[0] = ProjectTitle;
            ParamName[0] = "ProjectTitle";
            ParamValue[1] = FundingAgency;
            ParamName[1] = "FundingAgency";
            ParamValue[2] = FinancialOutlay;
            ParamName[2] = "FinancialOutlay";
            ParamValue[3] = YearStartAndTotalPeriod;
            ParamName[3] = "YearStartAndTotalPeriod";
            ParamValue[4] = NameOfPI;
            ParamName[4] = "NameOfPI";
            ParamValue[5] = CurrentStatus;
            ParamName[5] = "CurrentStatus";
            ParamValue[6] = SponsorConsultancyType;
            ParamName[6] = "SponsorConsultancyType";
            ParamValue[7] = UserId;
            ParamName[7] = "UserId";
            ParamValue[8] = AcademicSession;
            ParamName[8] = "AcademicSession";
            try
            {
                ReturnID = ExecScalarCmdText("[proc_insertAppraisal_SponsorConsultancyProjects]", ParamName, ParamValue);
            }
            catch
            {
                ReturnID = "Something went wrong Try Again....";
            }
            finally
            {
                CloseConnection();

            }
            return ReturnID;
        }
        public DataTable GetSponsorConsultancyProjects(string UserId, string AcademicSession, string SponsorConsultancyType)
        {
            string procName = "proc_GetAppraisal_SponsorConsultancyProjects";
            DataUtility objDut = new DataUtility();
            string[] ParamName = new string[3];
            object[] ParamValue = new object[3];
            ParamValue[0] = UserId;
            ParamName[0] = "UserId";
            ParamValue[1] = AcademicSession;
            ParamName[1] = "AcademicSession";
            ParamValue[2] = SponsorConsultancyType;
            ParamName[2] = "SponsorConsultancyType";
            DataTable dt = objDut.GetDataTableCmdParams(procName, ParamName, ParamValue);
            return dt;
        }
        public DataTable DeleteSponsorConsultancyProjects(int ID, string UserId)
        {
            string procName = "proc_DeleteAppraisal_SponsorConsultancyProjects";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName, ID, UserId);
            return dt;
        }


        public string SaveContinuingEducation(string LectureTitle, string DatePlaceProgramme, string OtherInformation,string UserId, string AcademicSession)
        {
            string ReturnID;
            // OpenConnection();
            string[] ParamName = new string[5];
            object[] ParamValue = new object[5];

            ParamValue[0] = LectureTitle;
            ParamName[0] = "LectureTitle";
            ParamValue[1] = DatePlaceProgramme;
            ParamName[1] = "DatePlaceProgramme";
            ParamValue[2] = OtherInformation;
            ParamName[2] = "OtherInformation";
            ParamValue[3] = UserId;
            ParamName[3] = "UserId";
            ParamValue[4] = AcademicSession;
            ParamName[4] = "AcademicSession";
            try
            {
                ReturnID = ExecScalarCmdText("[proc_insertAppraisal_ContinuingEducation]", ParamName, ParamValue);
            }
            catch
            {
                ReturnID = "Something went wrong Try Again....";
            }
            finally
            {
                CloseConnection();

            }
            return ReturnID;
        }
        public DataTable GetContinuingEducation(string UserId, string AcademicSession)
        {
            string procName = "proc_GetAppraisal_ContinuingEducation";
            DataUtility objDut = new DataUtility();
            string[] ParamName = new string[2];
            object[] ParamValue = new object[2];
            ParamValue[0] = UserId;
            ParamName[0] = "UserId";
            ParamValue[1] = AcademicSession;
            ParamName[1] = "AcademicSession";            
            DataTable dt = objDut.GetDataTableCmdParams(procName, ParamName, ParamValue);
            return dt;
        }
        public DataTable DeleteContinuingEducation(int ID, string UserId)
        {
            string procName = "proc_DeleteAppraisal_ContinuingEducation";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName, ID, UserId);
            return dt;
        }

        public string SaveMiscellaneous(string Description, string Type, string UserId, string AcademicSession)
        {
            string ReturnID;
            // OpenConnection();
            string[] ParamName = new string[4];
            object[] ParamValue = new object[4];

            ParamValue[0] = Description;
            ParamName[0] = "Description";
            ParamValue[1] = Type;
            ParamName[1] = "Type";            
            ParamValue[2] = UserId;
            ParamName[2] = "UserId";
            ParamValue[3] = AcademicSession;
            ParamName[3] = "AcademicSession";
            try
            {
                ReturnID = ExecScalarCmdText("[proc_insertAppraisal_Miscellaneous]", ParamName, ParamValue);
            }
            catch
            {
                ReturnID = "Something went wrong Try Again....";
            }
            finally
            {
                CloseConnection();

            }
            return ReturnID;
        }
        public DataTable GetMiscellaneous(string Type,string UserId, string AcademicSession)
        {
            string procName = "proc_GetAppraisal_Miscellaneous";
            DataUtility objDut = new DataUtility();
            string[] ParamName = new string[3];
            object[] ParamValue = new object[3];
            ParamValue[0] = UserId;
            ParamName[0] = "UserId";
            ParamValue[1] = AcademicSession;
            ParamName[1] = "AcademicSession";
            ParamValue[2] = Type;
            ParamName[2] = "Type";
            DataTable dt = objDut.GetDataTableCmdParams(procName, ParamName, ParamValue);
            return dt;
        }
        public DataTable DeleteMiscellaneous(int ID, string UserId)
        {
            string procName = "proc_DeleteAppraisal_Miscellaneous";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName, ID, UserId);
            return dt;
        }

        public string SaveCourseConferenceSeminar(string Name,string PlaceAndSponsoredBy,string Dates, string Type, string UserId, string AcademicSession)
        {
            string ReturnID;
            // OpenConnection();
            string[] ParamName = new string[6];
            object[] ParamValue = new object[6];

            ParamValue[0] = Name;
            ParamName[0] = "Name";
            ParamValue[1] = PlaceAndSponsoredBy;
            ParamName[1] = "PlaceAndSponsoredBy";
            ParamValue[2] = Dates;
            ParamName[2] = "Dates"; 
            ParamValue[3] = Type;
            ParamName[3] = "Type";
            ParamValue[4] = UserId;
            ParamName[4] = "UserId";
            ParamValue[5] = AcademicSession;
            ParamName[5] = "AcademicSession";
            try
            {
                ReturnID = ExecScalarCmdText("[proc_insertAppraisal_CourseConferenceSeminar]", ParamName, ParamValue);
            }
            catch
            {
                ReturnID = "Something went wrong Try Again....";
            }
            finally
            {
                CloseConnection();

            }
            return ReturnID;
        }
        public DataTable GetCourseConferenceSeminar(string Type, string UserId, string AcademicSession)
        {
            string procName = "proc_GetAppraisal_CourseConferenceSeminar";
            DataUtility objDut = new DataUtility();
            string[] ParamName = new string[3];
            object[] ParamValue = new object[3];
            ParamValue[0] = UserId;
            ParamName[0] = "UserId";
            ParamValue[1] = AcademicSession;
            ParamName[1] = "AcademicSession";
            ParamValue[2] = Type;
            ParamName[2] = "Type";
            DataTable dt = objDut.GetDataTableCmdParams(procName, ParamName, ParamValue);
            return dt;
        }
        public DataTable DeleteCourseConferenceSeminar(int ID, string UserId)
        {
            string procName = "proc_DeleteAppraisal_CourseConferenceSeminar";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName, ID, UserId);
            return dt;
        }

        public DataTable AssessmentScore(string UserId, string AcademicSession)
        {
            string procName = "[proc_GetAppraisal_AssessmentScore]";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName, UserId, AcademicSession);
            return dt;
        }
        public DataTable AssessmentRanking(string UserId, string AcademicSession)
        {
            //string procName = "[proc_GetAppraisal_StudentAssessment]";
            string procName = "[proc_GetAppraisal_AssessmentRanking]";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName, UserId, AcademicSession);
            return dt;
        }


        public DataTable AcademicYear(string procName)
        {

            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc1(procName);
            return dt;
        }

    }

}
