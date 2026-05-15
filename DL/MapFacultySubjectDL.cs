using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;
using System.Data;

namespace DL
{
    public class MapFacultySubjectDL:DataUtility
    {
         public MapFacultySubjectDL()
        { 

        }
         public DataTable GetMappedFacultySubject(string FacultyCode, string CollegeCode, string UserGroup)
        {
            string procName = "proc_GetMappedFacultySubject";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName,FacultyCode,CollegeCode,UserGroup);
            return dt;
        }
         public DataTable GetSearchEnquiryDdl()
         {
             string procName = "proc_SearchFacultySubjectDDL";
             DataUtility objDut = new DataUtility();
             DataTable dt = objDut.GetDataTableProc(procName);
             return dt;
         }
         public string SaveMapFacultySubject(string CourseCode, string SubjectCode, string FacultyCode, string UserId, string CollegeCode,string SemYear)
         {
             string ReturnID;
             OpenConnection();
             string[] ParamName = new string[6];
             object[] ParamValue = new object[6];

             ParamValue[0] = CourseCode;
             ParamName[0] = "CourseCode";
             ParamValue[1] = SubjectCode;
             ParamName[1] = "SubjectCode";
             ParamValue[2] = FacultyCode;
             ParamName[2] = "FacultyCode";
             ParamValue[3] = UserId;
             ParamName[3] = "UserId";
             ParamValue[4] = CollegeCode;
             ParamName[4] = "CollegeCode";
             ParamValue[5] = SemYear;
             ParamName[5] = "SemYear";

             try
             {
                 
                 ReturnID = ExecScalarCmdText("[proc_insertMapFacultySubject]", ParamName, ParamValue);
              
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
         
        public DataTable DeleteMappedFacultySubject(int ID,string UserId)
         {
             string procName = "proc_DeleteMappedFacultySubject";
             DataUtility objDut = new DataUtility();
             DataTable dt = objDut.GetDataTableProc(procName, ID,UserId);
             return dt;
         }
        public DataTable GetCourseDdl(string CollegeCode)
        {
            string procName = "[proc_GetCourseDdlByCollegeCode]";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName, CollegeCode);
            return dt;
        }
        public DataTable GetSubjectDdl(string CourseCode,string SemYear)
        {
            string procName = "proc_GetSubjectDdlTheoryLab";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName, CourseCode, SemYear);
            return dt;

        }
        public DataTable GetSemYear(string CourseCode)
        {
            string procName = "Sp_SemsterYear";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName, CourseCode);
            return dt;
        }
    }
}
