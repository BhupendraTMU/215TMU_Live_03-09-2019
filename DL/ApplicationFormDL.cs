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
    public class ApplicationFormDL : DataUtility
    {
        public ApplicationFormDL()
        { }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
        public int insertGen_JournalLine(string EnquiryNo, decimal Amount, string ExternalDocumentNo, string JournalBatchName)
        {
            int result = 0;
            con.Open();
            SqlCommand cmd = new SqlCommand("proc_insertGen_JournalLine", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EnqNo", EnquiryNo);
            cmd.Parameters.AddWithValue("@Amount", Amount);
            cmd.Parameters.AddWithValue("@ExternalDocumentNo", ExternalDocumentNo);
            cmd.Parameters.AddWithValue("@JournalBatchName", JournalBatchName);
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;

        }
        public String GetNextApplicationNo()// Ex- 24 Feb 2015
        {
            string Query = "select [dbo].[NextApplicationNumber]()";
            DataUtility objDut = new DataUtility();
            String GetAge = objDut.ExecScalarCmdText(Query);
            return GetAge;
        }
        //District ---string SubReligion(05-08-2016)
        public string addRegistrationInformation(string No, string dateofSale, string enquiryNo, string Name, string Course, string category,
               int gender, string DOB, int Age, int months, string fathersName, string mothersNmae, string academicYear, string citizen,
            string preQualification, string nameOfPreviousInstitute, string medium, int hostelAcommodation, string quota, decimal applicationCost,
            string paymentMode, DateTime chequeDate, string chequeNo, string bankName, string aadharNo, string emailId, string facebookId, string fatherQualification,
            string FatherOccupation, decimal fatherAnnualIncome, string motherQualification, string motherOccupation, decimal motherAnnualIncome, string ApplicationNo, 
            int RegidanceStatus,int MaritalStatus,string MotherTounge,string Religion,int Disability,string Domicile,
            int Case,string PermAddress,string PermCity,string PermPincode,string PermState,int SameAsPermanentAddress,
            string CorresAddress,string CorresCity,string CorresPinCode,string CorresState,
            string ParentContact, string ParentEmail, byte[] StudentImage, string MobileNo, string SubReligion, string District, string CorresDistrict)
        {
            string ReturnID = "Error";
            con.Open();
            SqlCommand cmd = new SqlCommand("[dbo].[proc_insertRegistrationInformation]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@No", No);
            cmd.Parameters.AddWithValue("@dateofSale", dateofSale);
            cmd.Parameters.AddWithValue("@enquiryNo", enquiryNo);
            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@Course", Course);
            cmd.Parameters.AddWithValue("@category", category);
            cmd.Parameters.AddWithValue("@gender", gender);
            cmd.Parameters.AddWithValue("@DOB", DOB);
            cmd.Parameters.AddWithValue("@Age", Age);
            cmd.Parameters.AddWithValue("@months", months);
            cmd.Parameters.AddWithValue("@fathersName", fathersName);
            cmd.Parameters.AddWithValue("@mothersNmae", mothersNmae);
            cmd.Parameters.AddWithValue("@AcademicYear", academicYear);
            cmd.Parameters.AddWithValue("@citizen", citizen);
            cmd.Parameters.AddWithValue("@preQualification", preQualification);
            cmd.Parameters.AddWithValue("@nameOfPreviousInstitute", nameOfPreviousInstitute);
            cmd.Parameters.AddWithValue("@medium", medium);
            cmd.Parameters.AddWithValue("@hostelAcommodation", hostelAcommodation);
            cmd.Parameters.AddWithValue("@quota", quota);
            cmd.Parameters.AddWithValue("@applicationCost", applicationCost);
            cmd.Parameters.AddWithValue("@paymentMode", paymentMode);
            cmd.Parameters.AddWithValue("@chequeDate", chequeDate);
            cmd.Parameters.AddWithValue("@chequeNo", chequeNo);
            cmd.Parameters.AddWithValue("@bankName", bankName);
            cmd.Parameters.AddWithValue("@aadharNo", aadharNo);
            cmd.Parameters.AddWithValue("@emailID", emailId);
            cmd.Parameters.AddWithValue("@facebookId", facebookId);
            cmd.Parameters.AddWithValue("@fatherQualification", fatherQualification);
            cmd.Parameters.AddWithValue("@fatherOccupation", FatherOccupation);
            cmd.Parameters.AddWithValue("@fatherAnnualIncome", fatherAnnualIncome);
            cmd.Parameters.AddWithValue("@MotherQualification", motherQualification);
            cmd.Parameters.AddWithValue("@MotherOccupation", motherOccupation);
            cmd.Parameters.AddWithValue("@MotherAnnualIncome", motherAnnualIncome);
            cmd.Parameters.AddWithValue("@ApplicationNo", ApplicationNo);
            cmd.Parameters.AddWithValue("@ApplicantImage", StudentImage);
            //------
           //-- cmd.Parameters.AddWithValue("@Address4", StudentImage);

            cmd.Parameters.AddWithValue("@ResidentStatus", RegidanceStatus);
            cmd.Parameters.AddWithValue("@Address1", PermAddress);
            cmd.Parameters.AddWithValue("@City", PermCity);
            cmd.Parameters.AddWithValue("@State",PermState);
            cmd.Parameters.AddWithValue("@CountryCode", citizen);
            cmd.Parameters.AddWithValue("@PostCode", PermPincode);
             cmd.Parameters.AddWithValue("@CorCity", CorresCity);
             cmd.Parameters.AddWithValue("@CorState", CorresState);
             cmd.Parameters.AddWithValue("@CorCountryCode", citizen);
             cmd.Parameters.AddWithValue("@CorPostCode", CorresPinCode);
             cmd.Parameters.AddWithValue("@SameAsPermanentAddress", SameAsPermanentAddress);
             cmd.Parameters.AddWithValue("@Disability", Disability);
             cmd.Parameters.AddWithValue("@RecordofCriminalCase", Case);
             cmd.Parameters.AddWithValue("@MaritalStatus", MaritalStatus);
             cmd.Parameters.AddWithValue("@MotherTongue", MotherTounge);
             cmd.Parameters.AddWithValue("@ParentsPhoneNumber", ParentContact);
             cmd.Parameters.AddWithValue("@ParentsMobileNumber", ParentContact);
             cmd.Parameters.AddWithValue("@ParentsEmailId", ParentEmail);
             cmd.Parameters.AddWithValue("@Religion", Religion);
             cmd.Parameters.AddWithValue("@Address3", CorresAddress);
             cmd.Parameters.AddWithValue("@MobileNo", MobileNo);
             cmd.Parameters.AddWithValue("@SubReligion", SubReligion);
             cmd.Parameters.AddWithValue("@District", District);
             cmd.Parameters.AddWithValue("@CorDistrict", CorresDistrict);
             cmd.ExecuteNonQuery(); 
           
            // ReturnID=cmd.ExecuteNonQuery();
            con.Close();
            return "Success";
        }
        public DataTable GetPrequalificationDdl()
        {
            string procName = "proc_GetPrequalificationDdl"; //"proc_getprequalificationSubCode";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName);
            return dt;
        }
        public DataTable GetAcademicYearDdl()
        {
            string procName = "proc_GetSessionDdl";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName);
            return dt;
        }
        public String addQualificationInformation(DataTable dt, string EnquiryNo, string courseCode, string academicYear, string category, string medium)
        {
            string Result = "Error";
            if (con.State == ConnectionState.Closed)
                con.Open();
            // ============ Delete qualification and then add========Start===
            SqlCommand cmddelete = new SqlCommand("[dbo].[proc_DeleteApplicationQualification]", con);
            cmddelete.CommandType = CommandType.StoredProcedure;
            cmddelete.Parameters.AddWithValue("@EnqNo", EnquiryNo);
            try
            {
                cmddelete.ExecuteNonQuery();
                Result = "Success";
            }
            catch
            { 
                Result = "Error";
                return Result;
            }
            // ============ Delete qualification and then add=======END====
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Result = "Error";
               
                try
                {
                  //SqlCommand cmd = new SqlCommand("[dbo].[proc_insertApplicationMarkCOLLEGE]", con);
                    SqlCommand cmd = new SqlCommand("[dbo].[proc_insertApplPrequalMarkCOLLEGE]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EnqNo", EnquiryNo);
                    cmd.Parameters.AddWithValue("@code", dt.Rows[i]["Qualification"]);
                    cmd.Parameters.AddWithValue("@Description", dt.Rows[i]["Description"]);
                    cmd.Parameters.AddWithValue("@yearOfPassing", dt.Rows[i]["YearOfPassing"]);
                    cmd.Parameters.AddWithValue("@SchoolCollege", dt.Rows[i]["SchoolCollege"]);//new
                    cmd.Parameters.AddWithValue("@BoardUniversity", dt.Rows[i]["BoardUniversity"]);//new
                    cmd.Parameters.AddWithValue("@maximum", dt.Rows[i]["MaximumMarks"]);
                    cmd.Parameters.AddWithValue("@marksObtained", dt.Rows[i]["MarksObtained"]);
                    if (dt.Rows[i]["PMarks"].ToString() == "")
                    { dt.Rows[i]["PMarks"] = 0; }//new}
                    cmd.Parameters.AddWithValue("@PMarks", dt.Rows[i]["PMarks"]);//new}
                    cmd.Parameters.AddWithValue("@Grade", dt.Rows[i]["Grade"]);//new
                    cmd.Parameters.AddWithValue("@medium", dt.Rows[i]["MOI"]);//new
                    cmd.Parameters.AddWithValue("@DesciplineSubj", dt.Rows[i]["DesciplineSubj"]);                   
                    cmd.Parameters.AddWithValue("@courseCode", courseCode);
                    cmd.Parameters.AddWithValue("@academicYear", academicYear);
                    cmd.Parameters.AddWithValue("@category", category);
                    cmd.ExecuteNonQuery();
                    Result ="Success";
                }
                catch
                {
                    Result = "Error";
                }
            }
            con.Close();
            return Result;
        }

        //===================================Ashutosh==========================
        public string SaveAndGenerateEnquiryForApplicant(string Name, String Course, String ContactNo, String whatsappno)
        {

            string ReturnID;
            OpenConnection();
            string[] ParamName = new string[4];
            object[] ParamValue = new object[4];

            ParamValue[0] = Name;
            ParamName[0] = "Name";
            ParamValue[1] = Course;
            ParamName[1] = "Course";
            ParamValue[2] = ContactNo;
            ParamName[2] = "ContactNo";
            ParamValue[3] = whatsappno;
            ParamName[3] = "whatsappno";

            try
            {

                ReturnID = ExecScalarCmdText("[proc_insertApllicantEnquiryAndReturnEnquiryNo]", ParamName, ParamValue);

            }
            catch
            {
                // Note :-Message should be greater than 11 character because length 10 condition is used on Application.aspxpage
                ReturnID = "Something went wrong Try Again....";
            }
            finally
            {
                CloseConnection();

            }
            return ReturnID;



        }
        public string UpdateandGenerateOTPForApplicant(string No_, String ContactNo)
        {

            string ReturnOTP;
            OpenConnection();
            string[] ParamName = new string[2];
            object[] ParamValue = new object[2];

            ParamValue[0] = No_;
            ParamName[0] = "No_";
            ParamValue[1] = ContactNo;
            ParamName[1] = "ContactNo";

            try
            {

                ReturnOTP = ExecScalarCmdText("[proc_GenerateOTPagainstEnquiry]", ParamName, ParamValue);

            }
            catch
            {
                //Note :-Message should be greater than 11 character because length 10 condition is used on Application.aspxpage
                ReturnOTP = "Something went wrong Try Again....";
            }
            finally
            {
                CloseConnection();

            }
            return ReturnOTP;



        }
        public DataTable GetDataAfterValidateOTP(string No_, string ContactNo, string OTP)
        {
            string procName = "[proc_ValidateOTPagainstEnquiry]";
            string[] ParamName = new string[3];
            object[] ParamValue = new object[3];
            ParamValue[0] = No_;
            ParamName[0] = "No_";
            ParamValue[1] = ContactNo;
            ParamName[1] = "ContactNo";
            ParamValue[2] = OTP;
            ParamName[2] = "OTP";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableCmdParams(procName, ParamName, ParamValue);
            return dt;
        }

        public String UpdateEnquiry(string No_, string EnquiryDate, string EnquiryType, string EnquirySource, string NameoftheMedia, String EnquirerName,
                string ApplicantRelationship, String ApplicantName, DateTime DateofBirth, string Father_sName, string Mother_sName, string ApplicantStatus, string AcademicYear,
             string CourseCode, string UniversityInterested, int HostelAcommodation, string Prequalification, string NameofthePreviousInstitute, string CertificationAuthoriry,
             string MediumofInstruction, string Addressto, string Addressee, string Address1, string Address2, string City, string PostCode, string CountryCode, string EMailAddress,
             string MobileNumber, string PhoneNumber, int Gender, string State, string No_Series, string Address3, string ConvertedtoApplication, int Age, int Months, string UserID,
            string PortalID, string CollegeInterested, int Category, int Religion, int SubReligion, string Remarks_Feedback, int FeeType)
        {
            string ReturnID;
            OpenConnection();
            string[] ParamName = new string[45];
            object[] ParamValue = new object[45];

            ParamValue[0] = No_;
            ParamName[0] = "No_";
            ParamValue[1] = EnquiryDate;
            ParamName[1] = "EnquiryDate";
            ParamValue[2] = EnquiryType;
            ParamName[2] = "EnquiryType";
            ParamValue[3] = EnquirySource;
            ParamName[3] = "EnquirySource";
            ParamValue[4] = NameoftheMedia;
            ParamName[4] = "NameoftheMedia";
            ParamValue[5] = EnquirerName;
            ParamName[5] = "EnquirerName";
            ParamValue[6] = ApplicantRelationship;
            ParamName[6] = "ApplicantRelationship";
            ParamValue[7] = ApplicantName;
            ParamName[7] = "ApplicantName";
            ParamValue[8] = DateofBirth;
            ParamName[8] = "DateofBirth";
            ParamValue[9] = Father_sName;
            ParamName[9] = "Father_sName";
            ParamValue[10] = Mother_sName;
            ParamName[10] = "Mother_sName";
            ParamValue[11] = ApplicantStatus;
            ParamName[11] = "ApplicantStatus";

            ParamValue[12] = AcademicYear;
            ParamName[12] = "AcademicYear";
            ParamValue[13] = CourseCode;
            ParamName[13] = "CourseCode";
            ParamValue[14] = UniversityInterested;
            ParamName[14] = "UniversityInterested";
            ParamValue[15] = HostelAcommodation;
            ParamName[15] = "HostelAcommodation";
            ParamValue[16] = Prequalification;
            ParamName[16] = "Prequalification";
            ParamValue[17] = NameofthePreviousInstitute;
            ParamName[17] = "NameofthePreviousInstitute";
            ParamValue[18] = CertificationAuthoriry;
            ParamName[18] = "CertificationAuthoriry";
            ParamValue[19] = MediumofInstruction;
            ParamName[19] = "MediumofInstruction";
            ParamValue[20] = Addressto;
            ParamName[20] = "Addressto";
            ParamValue[21] = Addressee;
            ParamName[21] = "Addressee";
            ParamValue[22] = Address1;
            ParamName[22] = "Address1";
            ParamValue[23] = Address2;
            ParamName[23] = "Address2";
            ParamValue[24] = City;
            ParamName[24] = "City";
            ParamValue[25] = PostCode;
            ParamName[25] = "PostCode";
            ParamValue[26] = CountryCode;
            ParamName[26] = "CountryCode";
            ParamValue[27] = EMailAddress;
            ParamName[27] = "EMailAddress";
            ParamValue[28] = MobileNumber;
            ParamName[28] = "MobileNumber";
            ParamValue[29] = PhoneNumber;
            ParamName[29] = "PhoneNumber";
            ParamValue[30] = Gender;
            ParamName[30] = "Gender";
            ParamValue[31] = State;
            ParamName[31] = "State";
            ParamValue[32] = No_Series;
            ParamName[32] = "No_Series";
            ParamValue[33] = Address3;
            ParamName[33] = "Address3";
            ParamValue[34] = ConvertedtoApplication;
            ParamName[34] = "ConvertedtoApplication";
            ParamValue[35] = Age;
            ParamName[35] = "Age";
            ParamValue[36] = Months;
            ParamName[36] = "Months";
            ParamValue[37] = UserID;
            ParamName[37] = "UserID";
            ParamValue[38] = PortalID;
            ParamName[38] = "PortalID";
            ParamValue[39] = CollegeInterested;
            ParamName[39] = "CollegeInterested";
            ParamValue[40] = Category;
            ParamName[40] = "Category";
            ParamValue[41] = Religion;
            ParamName[41] = "Religion";
            ParamValue[42] = SubReligion;
            ParamName[42] = "SubReligion";
            ParamValue[43] = Remarks_Feedback;
            ParamName[43] = "Remarks_Feedback";
            ParamValue[44] = FeeType;
            ParamName[44] = "FeeType";
            try
            {
                ReturnID = ExecScalarCmdText("proc_SaveEnquiry", ParamName, ParamValue);
            }
            catch
            {

                ReturnID = "Error";// don't change message Error  because i have use a condition on Application page as if(Message<>"Error")
            }
            finally
            {
                CloseConnection();

            }
            return ReturnID;



        }

        public DataTable GetQualificationDataAfterValidateOTP(string No_)
        {
            string procName = "[proc_GetApplicantQualification]";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName, No_);
            return dt;
        }
        public DataTable GetApplicationNoByEnquiryId(string EnquiryNo)// Ex- 24 Feb 2015
        {
            string procName = "[proc_GetApplicantpaymentDetails]";
            DataUtility objDut = new DataUtility();
            DataTable dt = objDut.GetDataTableProc(procName, EnquiryNo);            
            return dt;            
        }
    }


}
