using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DL;
using PL;
using System.Data;
using System.Data.SqlClient;
using Utility;




public partial class PersonalInformation : System.Web.UI.Page
{
    static string ID = string.Empty;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        GetPI();
    }
    public void GetPI()
    {
        ID = "STU000001";
         PersonalInformationDL objPersonalInformationDL = new PersonalInformationDL();
         List<PersonalInformationPL> objPersonalInformationList = new List<PersonalInformationPL>();
         objPersonalInformationList = objPersonalInformationDL.GetPersonalInformation(ID);
         if (objPersonalInformationList.Count > 0)
         {
             lblbName.Text = objPersonalInformationList[0].StudentName;
             lblEnrollmentNo.Text = objPersonalInformationList[0].EnrollmentNo_;
             lblbProgramme.Text = objPersonalInformationList[0].EnrollmentNo_;
             lblbYearSession.Text = objPersonalInformationList[0].AcademicYear;
             lblbDept.Text = objPersonalInformationList[0].Dist;
             lblbDOB.Text = objPersonalInformationList[0].DOB ;
             lblbBloodGroup.Text = objPersonalInformationList[0].Dist;
             lblbMobileNumber.Text = objPersonalInformationList[0].principalContNo;
             lblbEmailID.Text = objPersonalInformationList[0].ImpEmailId;
             lblbFatherName.Text = objPersonalInformationList[0].FathersName;
             lblbMotherName.Text = objPersonalInformationList[0].MothersName;
             lblbPAddressLine1.Text = objPersonalInformationList[0].StudAddress1;
             lblbPAddressLine2.Text = objPersonalInformationList[0].StudAddress2;
             lblbPDistt.Text = objPersonalInformationList[0].Dist;
             lblbPPin.Text = objPersonalInformationList[0].principalContNo;
             lblbPState.Text = objPersonalInformationList[0].State;
             lblbPCountry.Text = objPersonalInformationList[0].Country; 
             lblbBankName.Text = objPersonalInformationList[0].BankName;
             lblbBankAcNo.Text = objPersonalInformationList[0].BankAcNo;
             lblbCAddressLine1.Text = objPersonalInformationList[0].ImpAddress;
             lblbCAddressLine2.Text = objPersonalInformationList[0].ImpAddress;
             lblbCDistt.Text = objPersonalInformationList[0].Dist;
             lblbCPin.Text = objPersonalInformationList[0].principalContNo;
             lblbCState.Text = objPersonalInformationList[0].State;
             lblbCCountry.Text = objPersonalInformationList[0].Country;
             lblbDoctor.Text = objPersonalInformationList[0].Doctor;
             lblbDPhoneNumber.Text = objPersonalInformationList[0].Doctor;
             lblbPrincipalHOD.Text = objPersonalInformationList[0].PrincipalHOD;
             lblbPPhoneNumber.Text = objPersonalInformationList[0].principalContNo;
             lblbLocalGuardian.Text = objPersonalInformationList[0].LocalGuardian;
             lblbRelation.Text = objPersonalInformationList[0].Relation;
             lblbImpContactNumber.Text = objPersonalInformationList[0].ImpContactNo;
             lblbImpEmailID.Text = objPersonalInformationList[0].ImpEmailId;
             lblbImpAddress.Text = objPersonalInformationList[0].ImpAddress;
             lblbInCaseOf.Text = objPersonalInformationList[0].IncaseOf;
            // imgStudent.ImageUrl = "data:image/bmp;base64," +objPersonalInformationList[0].StudentImage;
           //  imgStudent.ImageUrl = "data:image/bmp;base64," + Convert.ToBase64String(objPersonalInformationList[0].StudentImage);
             //imgStudent.ImageUrl = "data:image/bmp;base64," + Convert.ToBase64String((byte[])objPersonalInformationList[0].StudentImage);
            // getimage();
             imgStudent.ImageUrl = "~/Handler.ashx?id=" + ID;
         }
         else
         { 
         }

    }
    private void getimage()
    {
        try
        {
            string sQuery = "select [Student Image] as StudentImage from [Ashoka University$Student - COLLEGE] where No_='STU000001'";           
           // SqlDataAdapter da = new SqlDataAdapter(sQuery, con.Con);
            DataSet ds = new DataSet();
            DataUtility objDut = new DataUtility();
            ds = (DataSet)objDut.GetDataSetText(sQuery);
           // da.Fill(ds, "tbl_Registration");
            //imgStudent.ImageUrl = "data:image/bmp;base64," + Convert.ToBase64String((byte[])ds.Tables[0].Rows[0]["StudentImage"]);


        }
        catch (Exception)
        {
            imgStudent.ImageUrl = "images/index.jpg";
        }
    }
}