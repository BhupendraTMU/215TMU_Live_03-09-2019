using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for pms_connection
/// </summary>
public class pms_connection
{

    SqlConnection Conn = null;

    //OleDbConnection Connex = null;
    SqlCommand cmd;
    //OleDbCommand cmdex;
    public pms_connection()
    {

        try
        {


            Conn = new SqlConnection(ConfigurationManager.AppSettings["strportal"]);

        }
        catch (Exception ex)
        {

        }

    }

    public SqlConnection Con
    {
        get
        {
            return Conn;
        }
    }



    public void Connect()
    {

        if (Conn.State == ConnectionState.Closed)
         Conn.Open(); 
    }

    public void DisConnect()
    {
        Conn.Close();
    }



    public void SP_Tbl_PMS_Insert(string Academic_Year, string A1_Total_Numbers_B
      , string A1_API_Score_AxB
      , string A1_Finally_Obtained_PQ
      , string A1_AssessmentBy_RM
      , string B1_Total_Numbers_B
      , string B1_API_Score_AxB
      , string B1_Finally_Obtained_PQ
      , string B1_AssessmentBy_RM
      , string B2_Total_Numbers_B
      , string B2_API_Score_AxB
      , string B2_Finally_Obtained_PQ
      , string B2_AssessmentBy_RM
      , string B3_Total_Numbers_B
      , string B3_API_Score_AxB
      , string B3_Finally_Obtained_PQ
      , string B3_AssessmentBy_RM
      , string B4_Total_Numbers_B
      , string B4_API_Score_AxB
      , string B4_Finally_Obtained_PQ
      , string B4_AssessmentBy_RM
      , string B5_Total_Numbers_B
      , string B5_API_Score_AxB
      , string B5_Finally_Obtained_PQ
      , string B5_AssessmentBy_RM
      , string B6_Total_Numbers_B
      , string B6_API_Score_AxB
      , string B6_Finally_Obtained_PQ
      , string B6_AssessmentBy_RM
      , string C1_Total_Numbers_B
      , string C1_API_Score_AxB
      , string C1_Finally_Obtained_PQ
      , string C1_AssessmentBy_RM
      , string C2_Total_Numbers_B
      , string C2_API_Score_AxB
      , string C2_Finally_Obtained_PQ
      , string C2_AssessmentBy_RM
      , string D1_Total_Numbers_B
      , string D1_API_Score_AxB
      , string D1_Finally_Obtained_PQ
      , string D1_AssessmentBy_RM
      , string D2_Total_Numbers_B
      , string D2_API_Score_AxB
      , string D2_Finally_Obtained_PQ
      , string D2_AssessmentBy_RM
      , string D3_Total_Numbers_B
      , string D3_API_Score_AxB
      , string D3_Finally_Obtained_PQ
      , string D3_AssessmentBy_RM
      , string D4_Total_Numbers_B
      , string D4_API_Score_AxB
      , string D4_Finally_Obtained_PQ
      , string D4_AssessmentBy_RM
      , string D5_Total_Numbers_B
      , string D5_API_Score_AxB
      , string D5_Finally_Obtained_PQ
      , string D5_AssessmentBy_RM
      , string D6_Total_Numbers_B
      , string D6_API_Score_AxB
      , string D6_Finally_Obtained_PQ
      , string D6_AssessmentBy_RM
      , string E1_Total_Numbers_B
      , string E1_API_Score_AxB
      , string E1_Finally_Obtained_PQ
      , string E1_AssessmentBy_RM
      , string CriteriaA_API_Score_AxB_Total
      , string CriteriaA_Finally_Obtained_PQ_Total
      , string CriteriaA_AssessmentBy_RM_Total
      , string F1_Total_Numbers_B
      , string F1_API_Score_AxB
      , string F1_Finally_Obtained_PQ
      , string F1_AssessmentBy_RM
      , string F2_Total_Numbers_B
      , string F2_API_Score_AxB
      , string F2_Finally_Obtained_PQ
      , string F2_AssessmentBy_RM
      , string F3_Total_Numbers_B
      , string F3_API_Score_AxB
      , string F3_Finally_Obtained_PQ
      , string F3_AssessmentBy_RM
      , string F4_Total_Numbers_B
      , string F4_API_Score_AxB
      , string F4_Finally_Obtained_PQ
      , string F4_AssessmentBy_RM
      , string F5_Total_Numbers_B
      , string F5_API_Score_AxB
      , string F5_Finally_Obtained_PQ
      , string F5_AssessmentBy_RM
      , string F6_Total_Numbers_B
      , string F6_API_Score_AxB
      , string F6_Finally_Obtained_PQ
      , string F6_AssessmentBy_RM
      , string F7_Total_Numbers_B
      , string F7_API_Score_AxB
      , string F7_Finally_Obtained_PQ
      , string F7_AssessmentBy_RM
      , string G1_Total_Numbers_B
      , string G1_API_Score_AxB
      , string G1_Finally_Obtained_PQ
      , string G1_AssessmentBy_RM
      , string G2_Total_Numbers_B
      , string G2_API_Score_AxB
      , string G2_Finally_Obtained_PQ
      , string G2_AssessmentBy_RM
      , string G3_Total_Numbers_B
      , string G3_API_Score_AxB
      , string G3_Finally_Obtained_PQ
      , string G3_AssessmentBy_RM
      , string H1_Total_Numbers_B
      , string H1_API_Score_AxB
      , string H1_Finally_Obtained_PQ
      , string H1_AssessmentBy_RM
      , string H2_Total_Numbers_B
      , string H2_API_Score_AxB
      , string H2_Finally_Obtained_PQ
      , string H2_AssessmentBy_RM
      , string H3_Total_Numbers_B
      , string H3_API_Score_AxB
      , string H3_Finally_Obtained_PQ
      , string H3_AssessmentBy_RM
      , string H4_Total_Numbers_B
      , string H4_API_Score_AxB
      , string H4_Finally_Obtained_PQ
      , string H4_AssessmentBy_RM
      , string H5_Total_Numbers_B
      , string H5_API_Score_AxB
      , string H5_Finally_Obtained_PQ
      , string H5_AssessmentBy_RM
      , string H6_Total_Numbers_B
      , string H6_API_Score_AxB
      , string H6_Finally_Obtained_PQ
      , string H6_AssessmentBy_RM
      , string H7_Total_Numbers_B
      , string H7_API_Score_AxB
      , string H7_Finally_Obtained_PQ
      , string H7_AssessmentBy_RM
      , string H8_Total_Numbers_B
      , string H8_API_Score_AxB
      , string H8_Finally_Obtained_PQ
      , string H8_AssessmentBy_RM
      , string H9_Total_Numbers_B
      , string H9_API_Score_AxB
      , string H9_Finally_Obtained_PQ
      , string H9_AssessmentBy_RM
      , string I1_Total_Numbers_B
      , string I1_API_Score_AxB
      , string I1_Finally_Obtained_PQ
      , string I1_AssessmentBy_RM
      , string I2_Total_Numbers_B
      , string I2_API_Score_AxB
      , string I2_Finally_Obtained_PQ
      , string I2_AssessmentBy_RM
      , string I3_Total_Numbers_B
      , string I3_API_Score_AxB
      , string I3_Finally_Obtained_PQ
      , string I3_AssessmentBy_RM
      , string I4_Total_Numbers_B
      , string I4_API_Score_AxB
      , string I4_Finally_Obtained_PQ
      , string I4_AssessmentBy_RM
      , string I5_Total_Numbers_B
      , string I5_API_Score_AxB
      , string I5_Finally_Obtained_PQ
      , string I5_AssessmentBy_RM
      , string J1_Total_Numbers_B
      , string J1_API_Score_AxB
      , string J1_Finally_Obtained_PQ
      , string J1_AssessmentBy_RM
      , string J2_Total_Numbers_B
      , string J2_API_Score_AxB
      , string J2_Finally_Obtained_PQ
      , string J2_AssessmentBy_RM
      , string CriteriaB_API_Score_AxB_Total
      , string CriteriaB_Finally_Obtained_PQ_Total
      , string CriteriaB_AssessmentBy_RM_Total
      , string K1_Total_Numbers_B
      , string K1_API_Score_AxB
      , string K1_Finally_Obtained_PQ
      , string K1_AssessmentBy_RM
      , string K2_Total_Numbers_B
      , string K2_API_Score_AxB
      , string K2_Finally_Obtained_PQ
      , string K2_AssessmentBy_RM
      , string K3_Total_Numbers_B
      , string K3_API_Score_AxB
      , string K3_Finally_Obtained_PQ
      , string K3_AssessmentBy_RM
      , string K4_Total_Numbers_B
      , string K4_API_Score_AxB
      , string K4_Finally_Obtained_PQ
      , string K4_AssessmentBy_RM
      , string K5_Total_Numbers_B
      , string K5_API_Score_AxB
      , string K5_Finally_Obtained_PQ
      , string K5_AssessmentBy_RM
      , string K6_Total_Numbers_B
      , string K6_API_Score_AxB
      , string K6_Finally_Obtained_PQ
      , string K6_AssessmentBy_RM
      , string K7_Total_Numbers_B
      , string K7_API_Score_AxB
      , string K7_Finally_Obtained_PQ
      , string K7_AssessmentBy_RM
      , string K8_Total_Numbers_B
      , string K8_API_Score_AxB
      , string K8_Finally_Obtained_PQ
      , string K8_AssessmentBy_RM
      , string K9_Total_Numbers_B
      , string K9_API_Score_AxB
      , string K9_Finally_Obtained_PQ
      , string K9_AssessmentBy_RM
      , string K10_Total_Numbers_B
      , string K10_API_Score_AxB
      , string K10_Finally_Obtained_PQ
      , string K10_AssessmentBy_RM
      , string K11_Total_Numbers_B
      , string K11_API_Score_AxB
      , string K11_Finally_Obtained_PQ
      , string K11_AssessmentBy_RM
      , string L1_Total_Numbers_B
      , string L1_API_Score_AxB
      , string L1_Finally_Obtained_PQ
      , string L1_AssessmentBy_RM
      , string L2_Total_Numbers_B
      , string L2_API_Score_AxB
      , string L2_Finally_Obtained_PQ
      , string L2_AssessmentBy_RM
      , string L3_Total_Numbers_B
      , string L3_API_Score_AxB
      , string L3_Finally_Obtained_PQ
      , string L3_AssessmentBy_RM
      , string M1_Total_Numbers_B
      , string M1_API_Score_AxB
      , string M1_Finally_Obtained_PQ
      , string M1_AssessmentBy_RM
      , string M2_Total_Numbers_B
      , string M2_API_Score_AxB
      , string M2_Finally_Obtained_PQ
      , string M2_AssessmentBy_RM
      , string M3_Total_Numbers_B
      , string M3_API_Score_AxB
      , string M3_Finally_Obtained_PQ
      , string M3_AssessmentBy_RM
      , string M4_Total_Numbers_B
      , string M4_API_Score_AxB
      , string M4_Finally_Obtained_PQ
      , string M4_AssessmentBy_RM
      , string N1_Total_Numbers_B
      , string N1_API_Score_AxB
      , string N1_Finally_Obtained_PQ
      , string N1_AssessmentBy_RM
      , string N2_Total_Numbers_B
      , string N2_API_Score_AxB
      , string N2_Finally_Obtained_PQ
      , string N2_AssessmentBy_RM
      , string N3_Total_Numbers_B
      , string N3_API_Score_AxB
      , string N3_Finally_Obtained_PQ
      , string N3_AssessmentBy_RM
      , string CriteriaC_API_Score_AxB_Total
      , string CriteriaC_Finally_Obtained_PQ_Total
      , string CriteriaC_AssessmentBy_RM_Total
      , string O1_Total_Numbers_B
      , string O1_API_Score_AxB
      , string O1_Finally_Obtained_PQ
      , string O1_AssessmentBy_RM
      , string O2_Total_Numbers_B
      , string O2_API_Score_AxB
      , string O2_Finally_Obtained_PQ
      , string O2_AssessmentBy_RM
      , string O3_Total_Numbers_B
      , string O3_API_Score_AxB
      , string O3_Finally_Obtained_PQ
      , string O3_AssessmentBy_RM
      , string O4_Total_Numbers_B
      , string O4_API_Score_AxB
      , string O4_Finally_Obtained_PQ
      , string O4_AssessmentBy_RM
      , string O5_Total_Numbers_B
      , string O5_API_Score_AxB
      , string O5_Finally_Obtained_PQ
      , string O5_AssessmentBy_RM
      , string CriteriaD_API_Score_AxB_Total
      , string CriteriaD_Finally_Obtained_PQ_Total
      , string CriteriaD_AssessmentBy_RM_Total
      , string P1_ScoreGivenBy_RM
      , string P2_ScoreGivenBy_RM
      , string P3_ScoreGivenBy_RM
      , string P4_ScoreGivenBy_RM
      , string P5_ScoreGivenBy_RM
      , string P6_ScoreGivenBy_RM
      , string P7_ScoreGivenBy_RM
      , string P8_ScoreGivenBy_RM
      , string P9_ScoreGivenBy_RM
      , string P10_ScoreGivenBy_RM
      , string CriteriaE_ScoreGivenBy_RM_Total
      , string CriteriaF_FacultyFeedback_Percentage
      , string CriteriaF_FacultyFeedback_ObtainedMarks_Total
      , string APIScore_CriteriaA_Faculty_Total
      , string APIScore_CriteriaB_Faculty_Total
      , string APIScore_CriteriaC_Faculty_Total
      , string APIScore_CriteriaD_Faculty_Total
      , string APIScore_CriteriaE_RM_Total
      , string APIScore_CriteriaF_RM_Total
      , string APIScore_TotalObtained_ApiScore_Total
      , string APIScore_FacultyCateogory
      , string RM_Comments_A
      , string RM_Comments_B
      , string RM_Comments_C
      , string RM_Comments_D
      , string RM_Comments_E
      , string HR_MaxScore_Total
      , string HR_TotalObtainedScore_Total
      , string HR_FacultyCateogory
      , string HR_RequiredImprovement_txt1
      , string HR_RequiredImprovement_txt2
      , string HR_RequiredImprovement_txt3
      , string HR_RequiredImprovement_txt4
      , string HR_Recommendations_A
      , string HR_Recommendations_B
      , string HR_Recommendations_C
      , string HR_Recommendations_D
        , string HR_Recommendations_E
      , string Created_On, string Created_By_ID
      , string Created_By

      , string Modified_On
        , string Modified_By_ID
      , string Modified_By
      , string IsFaculty_Approval
      , string Faculty_Approval_ID
      , string IsAssessment_Approval
      , string Assessment_Approval_ID
      , string IsHR_Approval
      , string HR_Approval_ID
      , string IsVC_Approval
      , string VC_Approval_ID
      , string Faculty_Approval_On
      , string Assesment_Approval_On
      , string HR_Approval_On
      , string VC_Approval_On, string Status, string F1_2_Total_Numbers_B
      , string F1_2_API_Score_AxB
      , string F1_2_Finally_Obtained_PQ
      , string F1_2_AssessmentBy_RM
      , string F3_2_Total_Numbers_B
      , string F3_2_API_Score_AxB
      , string F3_2_Finally_Obtained_PQ
      , string F3_2_AssessmentBy_RM

      , string Faculty_Name
      , string Designation
      , string Employee_Code
      , string Department, string UserType, string ApplicableFor, string ID_For_Updated, string College, string New_Department, string Feed_Odd_Sem, string Feed_Even_Sem)
    {
        Connect();
        string sqlq = "SP_Tbl_PMS_Insert";
        cmd = new SqlCommand(sqlq, Conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Academic_Year", Academic_Year.Trim());
        cmd.Parameters.AddWithValue("@A1_Total_Numbers_B", A1_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@A1_API_Score_AxB", A1_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@A1_Finally_Obtained_PQ", A1_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@A1_AssessmentBy_RM", A1_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@B1_Total_Numbers_B", B1_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@B1_API_Score_AxB", B1_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@B1_Finally_Obtained_PQ", B1_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@B1_AssessmentBy_RM", B1_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@B2_Total_Numbers_B", B2_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@B2_API_Score_AxB", B2_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@B2_Finally_Obtained_PQ", B2_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@B2_AssessmentBy_RM", B2_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@B3_Total_Numbers_B", B3_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@B3_API_Score_AxB", B3_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@B3_Finally_Obtained_PQ", B3_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@B3_AssessmentBy_RM", B3_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@B4_Total_Numbers_B", B4_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@B4_API_Score_AxB", B4_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@B4_Finally_Obtained_PQ", B4_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@B4_AssessmentBy_RM", B4_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@B5_Total_Numbers_B", B5_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@B5_API_Score_AxB", B5_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@B5_Finally_Obtained_PQ", B5_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@B5_AssessmentBy_RM", B5_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@B6_Total_Numbers_B", B6_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@B6_API_Score_AxB", B6_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@B6_Finally_Obtained_PQ", B6_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@B6_AssessmentBy_RM", B6_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@C1_Total_Numbers_B", C1_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@C1_API_Score_AxB", C1_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@C1_Finally_Obtained_PQ", C1_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@C1_AssessmentBy_RM", C1_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@C2_Total_Numbers_B", C2_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@C2_API_Score_AxB", C2_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@C2_Finally_Obtained_PQ", C2_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@C2_AssessmentBy_RM", C2_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@D1_Total_Numbers_B", D1_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@D1_API_Score_AxB", D1_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@D1_Finally_Obtained_PQ", D1_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@D1_AssessmentBy_RM", D1_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@D2_Total_Numbers_B", D2_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@D2_API_Score_AxB", D2_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@D2_Finally_Obtained_PQ", D2_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@D2_AssessmentBy_RM", D2_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@D3_Total_Numbers_B", D3_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@D3_API_Score_AxB", D3_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@D3_Finally_Obtained_PQ", D3_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@D3_AssessmentBy_RM", D3_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@D4_Total_Numbers_B", D4_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@D4_API_Score_AxB", D4_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@D4_Finally_Obtained_PQ", D4_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@D4_AssessmentBy_RM", D4_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@D5_Total_Numbers_B", D5_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@D5_API_Score_AxB", D5_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@D5_Finally_Obtained_PQ", D5_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@D5_AssessmentBy_RM", D5_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@D6_Total_Numbers_B", D6_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@D6_API_Score_AxB", D6_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@D6_Finally_Obtained_PQ", D6_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@D6_AssessmentBy_RM", D6_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@E1_Total_Numbers_B", E1_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@E1_API_Score_AxB", E1_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@E1_Finally_Obtained_PQ", E1_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@E1_AssessmentBy_RM", E1_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@CriteriaA_API_Score_AxB_Total", CriteriaA_API_Score_AxB_Total.Trim());
        cmd.Parameters.AddWithValue("@CriteriaA_Finally_Obtained_PQ_Total", CriteriaA_Finally_Obtained_PQ_Total.Trim());
        cmd.Parameters.AddWithValue("@CriteriaA_AssessmentBy_RM_Total", CriteriaA_AssessmentBy_RM_Total.Trim());

        cmd.Parameters.AddWithValue("@F1_Total_Numbers_B", F1_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@F1_API_Score_AxB", F1_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@F1_Finally_Obtained_PQ", F1_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@F1_AssessmentBy_RM", F1_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@F2_Total_Numbers_B", F2_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@F2_API_Score_AxB", F2_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@F2_Finally_Obtained_PQ", F2_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@F2_AssessmentBy_RM", F2_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@F3_Total_Numbers_B", F3_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@F3_API_Score_AxB", F3_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@F3_Finally_Obtained_PQ", F3_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@F3_AssessmentBy_RM", F3_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@F4_Total_Numbers_B", F4_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@F4_API_Score_AxB", F4_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@F4_Finally_Obtained_PQ", F4_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@F4_AssessmentBy_RM", F4_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@F5_Total_Numbers_B", F5_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@F5_API_Score_AxB", F5_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@F5_Finally_Obtained_PQ", F5_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@F5_AssessmentBy_RM", F5_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@F6_Total_Numbers_B", F6_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@F6_API_Score_AxB", F6_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@F6_Finally_Obtained_PQ", F6_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@F6_AssessmentBy_RM", F6_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@F7_Total_Numbers_B", F7_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@F7_API_Score_AxB", F7_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@F7_Finally_Obtained_PQ", F7_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@F7_AssessmentBy_RM", F7_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@G1_Total_Numbers_B", G1_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@G1_API_Score_AxB", G1_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@G1_Finally_Obtained_PQ", G1_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@G1_AssessmentBy_RM", G1_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@G2_Total_Numbers_B", G2_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@G2_API_Score_AxB", G2_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@G2_Finally_Obtained_PQ", G2_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@G2_AssessmentBy_RM", G2_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@G3_Total_Numbers_B", G3_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@G3_API_Score_AxB", G3_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@G3_Finally_Obtained_PQ", G3_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@G3_AssessmentBy_RM", G3_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@H1_Total_Numbers_B", H1_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@H1_API_Score_AxB", H1_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@H1_Finally_Obtained_PQ", H1_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@H1_AssessmentBy_RM", H1_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@H2_Total_Numbers_B", H2_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@H2_API_Score_AxB", H2_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@H2_Finally_Obtained_PQ", H2_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@H2_AssessmentBy_RM", H2_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@H3_Total_Numbers_B", H3_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@H3_API_Score_AxB", H3_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@H3_Finally_Obtained_PQ", H3_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@H3_AssessmentBy_RM", H3_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@H4_Total_Numbers_B", H4_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@H4_API_Score_AxB", H4_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@H4_Finally_Obtained_PQ", H4_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@H4_AssessmentBy_RM", H4_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@H5_Total_Numbers_B", H5_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@H5_API_Score_AxB", H5_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@H5_Finally_Obtained_PQ", H5_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@H5_AssessmentBy_RM", H5_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@H6_Total_Numbers_B", H6_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@H6_API_Score_AxB", H6_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@H6_Finally_Obtained_PQ", H6_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@H6_AssessmentBy_RM", H6_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@H7_Total_Numbers_B", H7_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@H7_API_Score_AxB", H7_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@H7_Finally_Obtained_PQ", H7_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@H7_AssessmentBy_RM", H7_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@H8_Total_Numbers_B", H8_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@H8_API_Score_AxB", H8_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@H8_Finally_Obtained_PQ", H8_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@H8_AssessmentBy_RM", H8_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@H9_Total_Numbers_B", H9_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@H9_API_Score_AxB", H9_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@H9_Finally_Obtained_PQ", H9_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@H9_AssessmentBy_RM", H9_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@I1_Total_Numbers_B", I1_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@I1_API_Score_AxB", I1_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@I1_Finally_Obtained_PQ", I1_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@I1_AssessmentBy_RM", I1_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@I2_Total_Numbers_B", I2_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@I2_API_Score_AxB", I2_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@I2_Finally_Obtained_PQ", I2_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@I2_AssessmentBy_RM", I2_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@I3_Total_Numbers_B", I3_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@I3_API_Score_AxB", I3_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@I3_Finally_Obtained_PQ", I3_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@I3_AssessmentBy_RM", I3_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@I4_Total_Numbers_B", I4_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@I4_API_Score_AxB", I4_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@I4_Finally_Obtained_PQ", I4_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@I4_AssessmentBy_RM", I4_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@I5_Total_Numbers_B", I5_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@I5_API_Score_AxB", I5_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@I5_Finally_Obtained_PQ", I5_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@I5_AssessmentBy_RM", I5_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@J1_Total_Numbers_B", J1_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@J1_API_Score_AxB", J1_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@J1_Finally_Obtained_PQ", J1_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@J1_AssessmentBy_RM", J1_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@J2_Total_Numbers_B", J2_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@J2_API_Score_AxB", J2_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@J2_Finally_Obtained_PQ", J2_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@J2_AssessmentBy_RM", J2_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@CriteriaB_API_Score_AxB_Total", CriteriaB_API_Score_AxB_Total.Trim());
        cmd.Parameters.AddWithValue("@CriteriaB_Finally_Obtained_PQ_Total", CriteriaB_Finally_Obtained_PQ_Total.Trim());
        cmd.Parameters.AddWithValue("@CriteriaB_AssessmentBy_RM_Total", CriteriaB_AssessmentBy_RM_Total.Trim());

        cmd.Parameters.AddWithValue("@K1_Total_Numbers_B", K1_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@K1_API_Score_AxB", K1_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@K1_Finally_Obtained_PQ", K1_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@K1_AssessmentBy_RM", K1_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@K2_Total_Numbers_B", K2_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@K2_API_Score_AxB", K2_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@K2_Finally_Obtained_PQ", K2_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@K2_AssessmentBy_RM", K2_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@K3_Total_Numbers_B", K3_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@K3_API_Score_AxB", K3_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@K3_Finally_Obtained_PQ", K3_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@K3_AssessmentBy_RM", K3_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@K4_Total_Numbers_B", K4_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@K4_API_Score_AxB", K4_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@K4_Finally_Obtained_PQ", K4_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@K4_AssessmentBy_RM", K4_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@K5_Total_Numbers_B", K5_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@K5_API_Score_AxB", K5_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@K5_Finally_Obtained_PQ", K5_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@K5_AssessmentBy_RM", K5_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@K6_Total_Numbers_B", K6_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@K6_API_Score_AxB", K6_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@K6_Finally_Obtained_PQ", K6_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@K6_AssessmentBy_RM", K6_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@K7_Total_Numbers_B", K7_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@K7_API_Score_AxB", K7_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@K7_Finally_Obtained_PQ", K7_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@K7_AssessmentBy_RM", K7_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@K8_Total_Numbers_B", K8_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@K8_API_Score_AxB", K8_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@K8_Finally_Obtained_PQ", K8_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@K8_AssessmentBy_RM", K8_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@K9_Total_Numbers_B", K9_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@K9_API_Score_AxB", K9_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@K9_Finally_Obtained_PQ", K9_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@K9_AssessmentBy_RM", K9_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@K10_Total_Numbers_B", K10_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@K10_API_Score_AxB", K10_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@K10_Finally_Obtained_PQ", K10_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@K10_AssessmentBy_RM", K10_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@K11_Total_Numbers_B", K11_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@K11_API_Score_AxB", K11_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@K11_Finally_Obtained_PQ", K11_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@K11_AssessmentBy_RM", K11_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@L1_Total_Numbers_B", L1_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@L1_API_Score_AxB", L1_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@L1_Finally_Obtained_PQ", L1_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@L1_AssessmentBy_RM", L1_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@L2_Total_Numbers_B", L2_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@L2_API_Score_AxB", L2_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@L2_Finally_Obtained_PQ", L2_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@L2_AssessmentBy_RM", L2_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@L3_Total_Numbers_B", L3_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@L3_API_Score_AxB", L3_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@L3_Finally_Obtained_PQ", L3_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@L3_AssessmentBy_RM", L3_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@M1_Total_Numbers_B", M1_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@M1_API_Score_AxB", M1_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@M1_Finally_Obtained_PQ", M1_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@M1_AssessmentBy_RM", M1_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@M2_Total_Numbers_B", M2_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@M2_API_Score_AxB", M2_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@M2_Finally_Obtained_PQ", M2_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@M2_AssessmentBy_RM", M2_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@M3_Total_Numbers_B", M3_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@M3_API_Score_AxB", M3_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@M3_Finally_Obtained_PQ", M3_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@M3_AssessmentBy_RM", M3_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@M4_Total_Numbers_B", M4_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@M4_API_Score_AxB", M4_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@M4_Finally_Obtained_PQ", M4_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@M4_AssessmentBy_RM", M4_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@N1_Total_Numbers_B", N1_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@N1_API_Score_AxB", N1_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@N1_Finally_Obtained_PQ", N1_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@N1_AssessmentBy_RM", N1_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@N2_Total_Numbers_B", N2_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@N2_API_Score_AxB", N2_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@N2_Finally_Obtained_PQ", N2_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@N2_AssessmentBy_RM", N2_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@N3_Total_Numbers_B", N3_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@N3_API_Score_AxB", N3_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@N3_Finally_Obtained_PQ", N3_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@N3_AssessmentBy_RM", N3_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@CriteriaC_API_Score_AxB_Total", CriteriaC_API_Score_AxB_Total.Trim());
        cmd.Parameters.AddWithValue("@CriteriaC_Finally_Obtained_PQ_Total", CriteriaC_Finally_Obtained_PQ_Total.Trim());
        cmd.Parameters.AddWithValue("@CriteriaC_AssessmentBy_RM_Total", CriteriaC_AssessmentBy_RM_Total.Trim());

        cmd.Parameters.AddWithValue("@O1_Total_Numbers_B", O1_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@O1_API_Score_AxB", O1_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@O1_Finally_Obtained_PQ", O1_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@O1_AssessmentBy_RM", O1_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@O2_Total_Numbers_B", O2_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@O2_API_Score_AxB", O2_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@O2_Finally_Obtained_PQ", O2_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@O2_AssessmentBy_RM", O2_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@O3_Total_Numbers_B", O3_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@O3_API_Score_AxB", O3_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@O3_Finally_Obtained_PQ", O3_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@O3_AssessmentBy_RM", O3_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@O4_Total_Numbers_B", O4_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@O4_API_Score_AxB", O4_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@O4_Finally_Obtained_PQ", O4_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@O4_AssessmentBy_RM", O4_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@O5_Total_Numbers_B", O5_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@O5_API_Score_AxB", O5_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@O5_Finally_Obtained_PQ", O5_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@O5_AssessmentBy_RM", O5_AssessmentBy_RM.Trim());

        cmd.Parameters.AddWithValue("@CriteriaD_API_Score_AxB_Total", CriteriaD_API_Score_AxB_Total.Trim());
        cmd.Parameters.AddWithValue("@CriteriaD_Finally_Obtained_PQ_Total", CriteriaD_Finally_Obtained_PQ_Total.Trim());
        cmd.Parameters.AddWithValue("@CriteriaD_AssessmentBy_RM_Total", CriteriaD_AssessmentBy_RM_Total.Trim());

        cmd.Parameters.AddWithValue("@P1_ScoreGivenBy_RM", P1_ScoreGivenBy_RM.Trim());
        cmd.Parameters.AddWithValue("@P2_ScoreGivenBy_RM", P2_ScoreGivenBy_RM.Trim());
        cmd.Parameters.AddWithValue("@P3_ScoreGivenBy_RM", P3_ScoreGivenBy_RM.Trim());
        cmd.Parameters.AddWithValue("@P4_ScoreGivenBy_RM", P4_ScoreGivenBy_RM.Trim());
        cmd.Parameters.AddWithValue("@P5_ScoreGivenBy_RM", P5_ScoreGivenBy_RM.Trim());
        cmd.Parameters.AddWithValue("@P6_ScoreGivenBy_RM", P6_ScoreGivenBy_RM.Trim());
        cmd.Parameters.AddWithValue("@P7_ScoreGivenBy_RM", P7_ScoreGivenBy_RM.Trim());
        cmd.Parameters.AddWithValue("@P8_ScoreGivenBy_RM", P8_ScoreGivenBy_RM.Trim());
        cmd.Parameters.AddWithValue("@P9_ScoreGivenBy_RM", P9_ScoreGivenBy_RM.Trim());
        cmd.Parameters.AddWithValue("@P10_ScoreGivenBy_RM", P10_ScoreGivenBy_RM.Trim());

        cmd.Parameters.AddWithValue("@CriteriaE_ScoreGivenBy_RM_Total", CriteriaE_ScoreGivenBy_RM_Total.Trim());
        cmd.Parameters.AddWithValue("@CriteriaF_FacultyFeedback_Percentage", CriteriaF_FacultyFeedback_Percentage.Trim());
        cmd.Parameters.AddWithValue("@CriteriaF_FacultyFeedback_ObtainedMarks_Total", CriteriaF_FacultyFeedback_ObtainedMarks_Total.Trim());

        cmd.Parameters.AddWithValue("@APIScore_CriteriaA_Faculty_Total", APIScore_CriteriaA_Faculty_Total.Trim());
        cmd.Parameters.AddWithValue("@APIScore_CriteriaB_Faculty_Total", APIScore_CriteriaB_Faculty_Total.Trim());
        cmd.Parameters.AddWithValue("@APIScore_CriteriaC_Faculty_Total", APIScore_CriteriaC_Faculty_Total.Trim());
        cmd.Parameters.AddWithValue("@APIScore_CriteriaD_Faculty_Total", APIScore_CriteriaD_Faculty_Total.Trim());
        cmd.Parameters.AddWithValue("@APIScore_CriteriaE_RM_Total", APIScore_CriteriaE_RM_Total.Trim());
        cmd.Parameters.AddWithValue("@APIScore_CriteriaF_RM_Total", APIScore_CriteriaF_RM_Total.Trim());
        cmd.Parameters.AddWithValue("@APIScore_TotalObtained_ApiScore_Total", APIScore_TotalObtained_ApiScore_Total.Trim());
        cmd.Parameters.AddWithValue("@APIScore_FacultyCateogory", APIScore_FacultyCateogory.Trim());
        cmd.Parameters.AddWithValue("@RM_Comments_A", RM_Comments_A.Trim());
        cmd.Parameters.AddWithValue("@RM_Comments_B", RM_Comments_B.Trim());
        cmd.Parameters.AddWithValue("@RM_Comments_C", RM_Comments_C.Trim());
        cmd.Parameters.AddWithValue("@RM_Comments_D", RM_Comments_D.Trim());
        cmd.Parameters.AddWithValue("@RM_Comments_E", RM_Comments_E.Trim());

        cmd.Parameters.AddWithValue("@HR_MaxScore_Total", HR_MaxScore_Total.Trim());
        cmd.Parameters.AddWithValue("@HR_TotalObtainedScore_Total", HR_TotalObtainedScore_Total.Trim());
        cmd.Parameters.AddWithValue("@HR_FacultyCateogory", HR_FacultyCateogory.Trim());
        cmd.Parameters.AddWithValue("@HR_RequiredImprovement_txt1", HR_RequiredImprovement_txt1.Trim());
        cmd.Parameters.AddWithValue("@HR_RequiredImprovement_txt2", HR_RequiredImprovement_txt2.Trim());
        cmd.Parameters.AddWithValue("@HR_RequiredImprovement_txt3", HR_RequiredImprovement_txt3.Trim());
        cmd.Parameters.AddWithValue("@HR_RequiredImprovement_txt4", HR_RequiredImprovement_txt4.Trim());
        cmd.Parameters.AddWithValue("@HR_Recommendations_A", HR_Recommendations_A.Trim());
        cmd.Parameters.AddWithValue("@HR_Recommendations_B", HR_Recommendations_B.Trim());
        cmd.Parameters.AddWithValue("@HR_Recommendations_C", HR_Recommendations_C.Trim());
        cmd.Parameters.AddWithValue("@HR_Recommendations_D", HR_Recommendations_D.Trim());
        cmd.Parameters.AddWithValue("@HR_Recommendations_E", HR_Recommendations_E.Trim());
        cmd.Parameters.AddWithValue("@Created_On", Created_On.Trim());
        cmd.Parameters.AddWithValue("@Created_By_ID", Created_By_ID.Trim());
        cmd.Parameters.AddWithValue("@Created_By", Created_By.Trim());
        cmd.Parameters.AddWithValue("@Modified_On", Modified_On.Trim());
        cmd.Parameters.AddWithValue("@Modified_By_ID", Modified_By_ID.Trim());
        cmd.Parameters.AddWithValue("@Modified_By", Modified_By.Trim());
        cmd.Parameters.AddWithValue("@IsFaculty_Approval", IsFaculty_Approval.Trim());
        cmd.Parameters.AddWithValue("@Faculty_Approval_ID", Faculty_Approval_ID.Trim());
        cmd.Parameters.AddWithValue("@IsAssessment_Approval", IsAssessment_Approval.Trim());
        cmd.Parameters.AddWithValue("@Assessment_Approval_ID", Assessment_Approval_ID.Trim());
        cmd.Parameters.AddWithValue("@IsHR_Approval", IsHR_Approval.Trim());
        cmd.Parameters.AddWithValue("@HR_Approval_ID", HR_Approval_ID.Trim());
        cmd.Parameters.AddWithValue("@IsVC_Approval", IsVC_Approval.Trim());
        cmd.Parameters.AddWithValue("@VC_Approval_ID", VC_Approval_ID.Trim());
        cmd.Parameters.AddWithValue("@Faculty_Approval_On", Faculty_Approval_On.Trim());
        cmd.Parameters.AddWithValue("@Assesment_Approval_On", Assesment_Approval_On.Trim());
        cmd.Parameters.AddWithValue("@HR_Approval_On", HR_Approval_On.Trim());
        cmd.Parameters.AddWithValue("@VC_Approval_On", VC_Approval_On.Trim());
        cmd.Parameters.AddWithValue("@Status", Status.Trim());
        cmd.Parameters.AddWithValue("@F1_2_Total_Numbers_B", F1_2_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@F1_2_API_Score_AxB", F1_2_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@F1_2_Finally_Obtained_PQ", F1_2_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@F1_2_AssessmentBy_RM", F1_2_AssessmentBy_RM.Trim());
        cmd.Parameters.AddWithValue("@F3_2_Total_Numbers_B", F3_2_Total_Numbers_B.Trim());
        cmd.Parameters.AddWithValue("@F3_2_API_Score_AxB", F3_2_API_Score_AxB.Trim());
        cmd.Parameters.AddWithValue("@F3_2_Finally_Obtained_PQ", F3_2_Finally_Obtained_PQ.Trim());
        cmd.Parameters.AddWithValue("@F3_2_AssessmentBy_RM", F3_2_AssessmentBy_RM.Trim());
        cmd.Parameters.AddWithValue("@Faculty_Name", Faculty_Name.Trim());
        cmd.Parameters.AddWithValue("@Designation", Designation.Trim());
        cmd.Parameters.AddWithValue("@Employee_Code", Employee_Code.Trim());
        cmd.Parameters.AddWithValue("@Department", Department.Trim());
        cmd.Parameters.AddWithValue("@UserType", UserType.Trim());
        cmd.Parameters.AddWithValue("@ApplicableFor", ApplicableFor.Trim());

        cmd.Parameters.AddWithValue("@ID_For_Updated", ID_For_Updated.Trim());
        cmd.Parameters.AddWithValue("@College", College.Trim());
        cmd.Parameters.AddWithValue("@New_Department", New_Department.Trim());
        cmd.Parameters.AddWithValue("@Feed_Odd_Sem", Feed_Odd_Sem.Trim());
        cmd.Parameters.AddWithValue("@Feed_Even_Sem", Feed_Even_Sem.Trim());


        cmd.ExecuteNonQuery();
    }

    public SqlDataReader sp_Get_PMS_DataWithID(string ID)
    {
        Connect();
        cmd = new SqlCommand("sp_Get_PMS_DataWithID", Conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", ID.Trim());

        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }


    public SqlDataReader sp_GetAttachmentByApplicable_for_PMS(string Applicable_For, string EmployeeID, string AcademicYear)
    {
        Connect();
        cmd = new SqlCommand("sp_GetAttachmentByApplicable_for_PMS", Conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Applicable_For", Applicable_For.Trim());
        cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID.Trim());
        cmd.Parameters.AddWithValue("@AcademicYear", AcademicYear.Trim());

        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }
    public SqlDataReader sp_Get_PMS_All_Data(string AcademicYear, string FilterText, string loginID, string DepartmentID, string Status, string Department)
    {
        Connect();
        cmd = new SqlCommand("sp_Get_PMS_All_Data", Conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@AcademicYear", AcademicYear.Trim());
        cmd.Parameters.AddWithValue("@FilterText", FilterText.Trim());
        cmd.Parameters.AddWithValue("@loginID", loginID.Trim());
        cmd.Parameters.AddWithValue("@DepartmentID", DepartmentID.Trim());
        cmd.Parameters.AddWithValue("@Status", Status.Trim());
        cmd.Parameters.AddWithValue("@DepartmentName", Department.Trim());
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader sp_Get_PMS_Faculty_FeedBack_In_Perntage(string Faculty_Code, string AcademicYear)
    {
        Connect();
        cmd = new SqlCommand("sp_Get_PMS_Faculty_FeedBack_In_Perntage", Conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Faculty_Code", Faculty_Code.Trim());
        cmd.Parameters.AddWithValue("@AcademicYear", AcademicYear.Trim());

        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }
    public SqlDataReader SP_PMS_Login(string UserID, string Password)
    {
        Connect();
        cmd = new SqlCommand("SP_PMS_Login", Conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserID", UserID.Trim());
        cmd.Parameters.AddWithValue("@Password", Password.Trim());

        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader SP_PMS_Get_Department()
    {
        Connect();
        cmd = new SqlCommand("SP_PMS_Get_Department", Conn);
        cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Parameters.AddWithValue("@UserID", UserID.Trim());
        //cmd.Parameters.AddWithValue("@Password", Password.Trim());

        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }
    public SqlDataReader sp_PMS_Check_Filled_Or_Not(string AcademicYear, string EmployeeCode)
    {
        Connect();
        cmd = new SqlCommand("sp_PMS_Check_Filled_Or_Not", Conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@AcademicYear", AcademicYear.Trim());
        cmd.Parameters.AddWithValue("@EmployeeCode", EmployeeCode.Trim());

        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader sp_PMS_Check_Reporting_Manager(string EmployeeCode, string Login_ID)
    {
        Connect();
        cmd = new SqlCommand("sp_PMS_Check_Reporting_Manager", Conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@EmployeeCode", EmployeeCode.Trim());
        cmd.Parameters.AddWithValue("@Login_ID", Login_ID.Trim());

        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }



    public SqlDataReader SP_PMS_Count_Filled_Or_Not_Filed_Emaployee_Details(string Academic_Year, string Applicable_For)
    {
        Connect();
        cmd = new SqlCommand("SP_PMS_Count_Filled_Or_Not_Filed_Emaployee_Details", Conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Academic_Year", Academic_Year.Trim());
        cmd.Parameters.AddWithValue("@Applicable_For", Applicable_For.Trim());

        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader SP_PMS_Get_Filled_Or_Not_Filed_Emaployee_Details(string Academic_Year, string Applicable_For)
    {
        Connect();
        cmd = new SqlCommand("SP_PMS_Get_Filled_Or_Not_Filed_Emaployee_Details", Conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Academic_Year", Academic_Year.Trim());
        cmd.Parameters.AddWithValue("@Applicable_For", Applicable_For.Trim());

        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }
    public void SP_PMS_Send_For_Approval(string id, string Applicable_For, string login_ID, string Login_ID_Name, string Approval_Date)
    {

        Connect();
        string sqlq = "SP_PMS_Send_For_Approval";
        cmd = new SqlCommand(sqlq, Conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@id", id.Trim());
        cmd.Parameters.AddWithValue("@Applicable_For", Applicable_For.Trim());
        cmd.Parameters.AddWithValue("@login_ID", login_ID.Trim());
        cmd.Parameters.AddWithValue("@Login_ID_Name", Login_ID_Name.Trim());
        cmd.Parameters.AddWithValue("@Approval_Date", Approval_Date.Trim());
        cmd.ExecuteNonQuery();
    }
    public void sp_Delete_Attachment_PMS(string AutoNo)
    {

        Connect();
        string sqlq = "sp_Delete_Attachment_PMS";
        cmd = new SqlCommand(sqlq, Conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@AutoNo", AutoNo.Trim());
        cmd.ExecuteNonQuery();
    }
    public SqlDataReader SP_FA_MM_Get_Menters_Record(string Name_No, string AcademicYear, string Coursecode,string loginid)
    {
        Connect();
        cmd = new SqlCommand("SP_FA_MM_Get_Menters_Record", Conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Name_No", Name_No.Trim());
        cmd.Parameters.AddWithValue("@Academic_Year", AcademicYear.Trim());
        cmd.Parameters.AddWithValue("@Course_Code", Coursecode.Trim());
        cmd.Parameters.AddWithValue("@loginid", loginid.Trim());
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader SP_FA_MM_Get_Mentnee_Record(string Name_No, string AcademicYear, string Coursecode,string Semester,string Section)
    {
        Connect();
        cmd = new SqlCommand("SP_FA_MM_Get_Mentnee_Record", Conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Name_No", Name_No.Trim());
        cmd.Parameters.AddWithValue("@Academic_Year", AcademicYear.Trim());
        cmd.Parameters.AddWithValue("@Course_Code", Coursecode.Trim());
        cmd.Parameters.AddWithValue("@Semester", Semester.Trim());
        cmd.Parameters.AddWithValue("@Section", Section.Trim());
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }
    public SqlDataReader SP_FA_Get_CollegeCode_Of_StudentNo(string StudentNo)
    {
        Connect();
        cmd = new SqlCommand("SP_FA_Get_CollegeCode_Of_StudentNo", Conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@StudentNo", StudentNo.Trim());
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader SP_FA_MM_Get_CourseCode(string LoginID,string GlobalDimensioncode1)
    {
        Connect();
        SqlCommand cmd = new SqlCommand("SP_FA_MM_Get_CourseCode", Con)
        {
            CommandType = CommandType.StoredProcedure
        };

        cmd.Parameters.AddWithValue("@loginid", LoginID.Trim());
        cmd.Parameters.AddWithValue("@GlobalDimensioncode1", GlobalDimensioncode1.Trim());
        // Execute and return the SqlDataReader
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader SP_FA_MM_Get_Academic_Year()
    {
        Connect();
        cmd = new SqlCommand("SP_FA_MM_Get_Academic_Year", Con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;

    }
    public SqlDataReader SP_FA_Get_Section()
    {
        Connect();
        cmd = new SqlCommand("SP_FA_Get_Section", Con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;

    }

    public SqlDataReader SP_FA_MM_Get_Semester()
    {
        Connect();
        cmd = new SqlCommand("SP_FA_MM_Get_Semester", Con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;

    }

    public void SP_FA_MM_Insert_Mentnee_Record(string No, string StudentName, string DOB, string FatherName, 
        string MotherName, string MobileNo, string Mentee_ID, string Mentee_Name, string Academic_Year, 
        string Course_Code, string Allocated, string CreatedBy, string CreatedByName, string CreatedOn)
    {
        Connect();
        string sqlq = "SP_FA_MM_Insert_Mentnee_Record";
        cmd = new SqlCommand(sqlq, Conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@No", No.Trim());
        cmd.Parameters.AddWithValue("@StudentName", StudentName.Trim());
        cmd.Parameters.AddWithValue("@DOB", DOB.Trim());
        cmd.Parameters.AddWithValue("@FatherName", FatherName.Trim());
        cmd.Parameters.AddWithValue("@MotherName", MotherName.Trim());
        cmd.Parameters.AddWithValue("@MobileNo", MobileNo.Trim());
        cmd.Parameters.AddWithValue("@MentorId", Mentee_ID.Trim());
        cmd.Parameters.AddWithValue("@MentorName", Mentee_Name.Trim());
        cmd.Parameters.AddWithValue("@Academic_Year", Academic_Year.Trim());
        cmd.Parameters.AddWithValue("@Course_Code", Course_Code.Trim());
        cmd.Parameters.AddWithValue("@Allocated", Allocated.Trim());
        cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy.Trim());
        cmd.Parameters.AddWithValue("@CreatedByName", CreatedByName.Trim());
        cmd.Parameters.AddWithValue("@CreatedOn", CreatedOn.Trim());

        cmd.ExecuteNonQuery();

    }

    public SqlDataReader SP_GetData_MentorFor_Mentee(string AcademicYear, string Name_No, string Coursecode)
    {
        Connect();
        cmd = new SqlCommand("SP_GetData_MentorFor_Mentee", Con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader SP_GetData_MenterFor_Mentee(string StudentName, string CourseCode, string AcademicYear, string MentorID)
    {
        Connect();
        cmd = new SqlCommand("SP_FA_GetData_MenterFor_Mentee", Conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Student_Name", StudentName.Trim());
        cmd.Parameters.AddWithValue("@CourseCode", CourseCode.Trim());
        cmd.Parameters.AddWithValue("@Academic_Year", AcademicYear.Trim());
        cmd.Parameters.AddWithValue("@MentorID", MentorID.Trim());


        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public void SP_FA_Mentor_Mentee_MeetingRecord_Insert(string Semester, string Date, string Name_Student, string Issue_Discussed_Identified_ProblemS, string Provided_Advice_Solutions_ByMentor, string Summary_ActionTaken_Remark, string AutoNo,
        string Student_Id, string MentorId, string MentorName, string CreatedOn, string Course, string AcademicYear)
    {
        Connect();
        string sqlq = "SP_FA_Mentor_Mentee_MeetingRecord_Insert";
        cmd = new SqlCommand(sqlq, Conn);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@Semester", Semester.Trim().ToString());
        cmd.Parameters.AddWithValue("@Date", Date.Trim());
        cmd.Parameters.AddWithValue("@Name_Student", Name_Student.Trim());
        cmd.Parameters.AddWithValue("@Issue_Discussed_Identified_ProblemS", Issue_Discussed_Identified_ProblemS.Trim());
        cmd.Parameters.AddWithValue("@Provided_Advice_Solutions_ByMentor", Provided_Advice_Solutions_ByMentor.Trim());
        cmd.Parameters.AddWithValue("@Summary_ActionTaken_Remark", Summary_ActionTaken_Remark.Trim());
        cmd.Parameters.AddWithValue("@AutoNo", AutoNo.Trim());
        cmd.Parameters.AddWithValue("@Student_Id", Student_Id.Trim());
        cmd.Parameters.AddWithValue("@MentorId", MentorId.Trim());
        cmd.Parameters.AddWithValue("@MentorName", MentorName.Trim());
        cmd.Parameters.AddWithValue("@CreatedOn", System.DateTime.Now.ToString("dd MMM yyyy").Trim());
        cmd.Parameters.AddWithValue("@Course", Course.ToString().Trim());
        cmd.Parameters.AddWithValue("@AcademicYear", AcademicYear.ToString().Trim());





        cmd.ExecuteNonQuery();
    }


    public SqlDataReader SP_GetAllData_MM_Record(string AutoNo)
    {
        Connect();
        cmd = new SqlCommand("SP_GetAllData_MM_Record", Conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@AutoNo", AutoNo.Trim());


        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }


    public SqlDataReader SP_FA_MM_Remove_Mentnee_Record(string AutoNo)
    {
        Connect();
        string sqlq = "SP_FA_MM_Remove_Mentnee_Record";
        cmd = new SqlCommand(sqlq, Conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@AutoNo", AutoNo.Trim());
        
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;

    }

    public void SP_FA_DelteMM_Record(string SrNo, string AutoNo, string StudentName)
    {
        Connect();
        string sqlq = "SP_FA_DelteMM_Record ";
        cmd = new SqlCommand(sqlq, Conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@SrNo", SrNo.Trim());
        cmd.Parameters.AddWithValue("@AutoNo", AutoNo.Trim());
        cmd.Parameters.AddWithValue("@StudentName", StudentName.Trim());


        cmd.ExecuteNonQuery();       

    }

    public void SP_FA_MM_Edit_Record(string SrNo, string AutoNo,string Semester, string Date, string StudentName,
        string Issue_Discussed_Identified_ProblemS, string Provided_Advice_Solutions_ByMentor, string Summary_ActionTaken_Remark)
    {
        Connect();
        string sqlq = "SP_FA_MM_Edit_Record ";
        cmd = new SqlCommand(sqlq, Conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@SrNo", SrNo.Trim());
        cmd.Parameters.AddWithValue("@AutoNo", AutoNo.Trim());
        cmd.Parameters.AddWithValue("@Semester", Semester.Trim());
        cmd.Parameters.AddWithValue("@Date", Date.Trim());
        cmd.Parameters.AddWithValue("@Name_Student", StudentName.Trim());
        cmd.Parameters.AddWithValue("@Issue_Discussed_Identified_ProblemS", Issue_Discussed_Identified_ProblemS.Trim());
        cmd.Parameters.AddWithValue("@Provided_Advice_Solutions_ByMentor", Provided_Advice_Solutions_ByMentor.Trim());
        cmd.Parameters.AddWithValue("@Summary_ActionTaken_Remark", Summary_ActionTaken_Remark.Trim());
        cmd.ExecuteNonQuery();

    }

    public void SP_FA_CurricularActivities_Insert(string Semester, string Student_Name, string Activity_Name, string Date, string Event_Details, string Detail_EventOrganizer, string Level_C_U_S_N_I, string Certification_Position, string Type, string AutoNo, string StudentId, string MentorId, string MentorName, string CreatedOn, string Course, string AcademicYear)
    {
        Connect();
        string Sqlq = "SP_FA_CurricularActivities_Insert";
        cmd = new SqlCommand(Sqlq, Conn);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@Semester", Semester.Trim());
        cmd.Parameters.AddWithValue("@Student_Name", Student_Name.Trim());
        cmd.Parameters.AddWithValue("@Activity_Name", Activity_Name.Trim());
        cmd.Parameters.AddWithValue("@Date", Date.Trim());
        cmd.Parameters.AddWithValue("@Event_Details", Event_Details.Trim());
        cmd.Parameters.AddWithValue("@Detail_EventOrganizer", Detail_EventOrganizer.Trim());
        cmd.Parameters.AddWithValue("@Level_C_U_S_N_I", Level_C_U_S_N_I.Trim());
        cmd.Parameters.AddWithValue("@Certification_Position", Certification_Position.Trim());
        cmd.Parameters.AddWithValue("@Type", Type.Trim());
        cmd.Parameters.AddWithValue("@AutoNo", AutoNo.Trim());
        cmd.Parameters.AddWithValue("@StudentId", StudentId.Trim());
        cmd.Parameters.AddWithValue("@MentorId", MentorId.Trim());
        cmd.Parameters.AddWithValue("@MentorName", MentorName.Trim());
        cmd.Parameters.AddWithValue("@CreatedOn", System.DateTime.Now);
        cmd.Parameters.AddWithValue("@Course", Course.Trim());
        cmd.Parameters.AddWithValue("@AcademicYear", AcademicYear.Trim());

        cmd.ExecuteNonQuery();
    }


    public SqlDataReader SP_GetAllData_CurricularActivities(string MentorId, string Course, string AcademicYear, string EnrollNo)
    {
        Connect();
        cmd = new SqlCommand("SP_FA_Get_All_Activity_Records", Conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@MentorId", MentorId.Trim());
        cmd.Parameters.AddWithValue("@Course", Course.Trim());
        cmd.Parameters.AddWithValue("@AcademicYear", AcademicYear.Trim());
        cmd.Parameters.AddWithValue("@EnrollNo", EnrollNo.Trim());
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;

    }

    public void SP_FA_tbl_SpecialNeeds_Bymentee_Insert(string Semester, string Student_Name, string Date, string ProvideFacility_theMentee, string Remark, string AutoNo, string StudentId, string AcademicYear, string Course, string MentorId)
    {
        Connect();
        string Sqlq = "SP_FA_tbl_SpecialNeeds_Bymentee_Insert";
        cmd = new SqlCommand(Sqlq, Conn);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@Semester", Semester.Trim());
        cmd.Parameters.AddWithValue("@Date", Date.Trim());
        cmd.Parameters.AddWithValue("@Student_Name", Student_Name.Trim());
        cmd.Parameters.AddWithValue("@ProvideFacility_theMentee", ProvideFacility_theMentee.Trim());
        cmd.Parameters.AddWithValue("@Remark", Remark.Trim());
        cmd.Parameters.AddWithValue("@AutoNo", AutoNo.Trim());
        cmd.Parameters.AddWithValue("@StudentId", StudentId.Trim());
        cmd.Parameters.AddWithValue("@AcademicYear", AcademicYear.Trim());
        cmd.Parameters.AddWithValue("@Course", Course.Trim());
        cmd.Parameters.AddWithValue("@MentorId", MentorId.Trim());



        cmd.ExecuteNonQuery();
    }

    public SqlDataReader SP_GetAllData_SpecialNeeds_Bymentee(string AutoNo)
    {
        Connect();
        cmd = new SqlCommand("SP_GetAllData_SpecialNeeds_Bymentee", Conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@AutoNo", AutoNo.Trim());

        SqlDataReader dr = cmd.ExecuteReader();
        return dr;

    }

    public void SP_FA_CurricularActivities_Update(string Semester, string Student_Name, string Activity_Name, string Date,
        string Event_Details, string Detail_EventOrganizer, string Level_C_U_S_N_I, string Certification_Position,
        string Type, string Sr_No, string Auto_No)
    {
        Connect();
        string sqlq = "SP_FA_CurricularActivities_Update";
        cmd = new SqlCommand(sqlq, Conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Semester", Semester.Trim());
        cmd.Parameters.AddWithValue("@Student_Name", Student_Name.Trim());
        cmd.Parameters.AddWithValue("@Activity_Name", Activity_Name.Trim());
        cmd.Parameters.AddWithValue("@Date", Date.Trim());
        cmd.Parameters.AddWithValue("@Event_Details", Event_Details.Trim());
        cmd.Parameters.AddWithValue("@Detail_EventOrganizer", Detail_EventOrganizer.Trim());
        cmd.Parameters.AddWithValue("@Level_C_U_S_N_I", Level_C_U_S_N_I.Trim());
        cmd.Parameters.AddWithValue("@Certification_Position", Certification_Position.Trim());
        cmd.Parameters.AddWithValue("@Type", Type.Trim());
        cmd.Parameters.AddWithValue("@Sr_No", Sr_No.Trim());
        cmd.Parameters.AddWithValue("@Auto_No", Auto_No.Trim());

        cmd.ExecuteNonQuery();
    }

    public void SP_FA_CurricularActivites_Delete(string SrNo, string AutoNo, string StudentName)
    {
        Connect();
        string sqlq = "SP_FA_CurricularActivites_Delete ";
        cmd = new SqlCommand(sqlq, Conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@SrNo", SrNo.Trim());
        cmd.Parameters.AddWithValue("@AutoNo", AutoNo.Trim());
        cmd.Parameters.AddWithValue("@StudentName", StudentName.Trim());


        cmd.ExecuteNonQuery();

    }

    public void SP_FA_tbl_SpecialNeeds_Bymentee_Update(string Semester, string Student_Name, string Date,
        string ProvideFacility_theMentee, string Remark,
        string AutoNo, string SrNo)
    {
        Connect();
        string sqlq = "SP_FA_tbl_SpecialNeeds_Bymentee_Update";
        cmd = new SqlCommand(sqlq, Conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Semester", Semester.Trim());
        cmd.Parameters.AddWithValue("@Student_Name", Student_Name.Trim());
        cmd.Parameters.AddWithValue("@Date", Date.Trim());
        cmd.Parameters.AddWithValue("@ProvideFacility_theMentee", ProvideFacility_theMentee.Trim());
        cmd.Parameters.AddWithValue("@Remark", Remark.Trim());
        cmd.Parameters.AddWithValue("@AutoNo", AutoNo.Trim());
        cmd.Parameters.AddWithValue("@SrNo", SrNo.Trim());
        cmd.ExecuteNonQuery();
    }

    public void SP_FA_tbl_SpecialNeeds_Bymentee_Delete(string SrNo, string AutoNo, string StudentName)
    {
        Connect();
        string sqlq = "SP_FA_tbl_SpecialNeeds_Bymentee_Delete ";
        cmd = new SqlCommand(sqlq, Conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@SrNo", SrNo.Trim());
        cmd.Parameters.AddWithValue("@AutoNo", AutoNo.Trim());
        cmd.Parameters.AddWithValue("@StudentName", StudentName.Trim());


        cmd.ExecuteNonQuery();

    }

    public void SP_FA_StudentAssessment_MM_Insert(string AutoNO, string Semester, string Student_Name, string Date,
        string Regularity_Classrooms, string Performance_Study, string Participation_CurricularActivities, string Physical_Status
       , string Behaviour_Teachers_Students, string Mental_Status, string Regularity_Classrooms_Text, string Performance_Study_Text,
        string Participation_CurricularActivities_Text, string Physical_Status_Text, string Behaviour_Teachers_Students_Text,
        string Mental_Status_Text, string StudentId, string AcademicYear, string Course, string MentorId)
    {
        Connect();
        string Sqlq = "SP_FA_StudentAssessment_MM_Insert";
        cmd = new SqlCommand(Sqlq, Conn);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@AutoNO", AutoNO.Trim());
        cmd.Parameters.AddWithValue("@Semester", Semester.Trim());
        cmd.Parameters.AddWithValue("@Student_Name", Student_Name.Trim());
        cmd.Parameters.AddWithValue("@Date", Date.Trim());
        cmd.Parameters.AddWithValue("@Regularity_Classrooms", Regularity_Classrooms.Trim());
        cmd.Parameters.AddWithValue("@Performance_Study", Performance_Study.Trim());
        cmd.Parameters.AddWithValue("@Participation_CurricularActivities", Participation_CurricularActivities.Trim());
        cmd.Parameters.AddWithValue("@Physical_Status", Physical_Status.Trim());
        cmd.Parameters.AddWithValue("@Behaviour_Teachers_Students", Behaviour_Teachers_Students.Trim());
        cmd.Parameters.AddWithValue("@Mental_Status", Mental_Status.Trim());
        cmd.Parameters.AddWithValue("@Regularity_Classrooms_Text", Regularity_Classrooms_Text.Trim());
        cmd.Parameters.AddWithValue("@Performance_Study_Text", Performance_Study_Text.Trim());
        cmd.Parameters.AddWithValue("@Participation_CurricularActivities_Text", Participation_CurricularActivities_Text.Trim());
        cmd.Parameters.AddWithValue("@Physical_Status_Text", Physical_Status_Text.Trim());
        cmd.Parameters.AddWithValue("@Behaviour_Teachers_Students_Text", Behaviour_Teachers_Students_Text.Trim());
        cmd.Parameters.AddWithValue("@Mental_Status_Text", Mental_Status_Text.Trim());
        cmd.Parameters.AddWithValue("@StudentId", StudentId.Trim());
        cmd.Parameters.AddWithValue("@AcademicYear", AcademicYear.Trim());
        cmd.Parameters.AddWithValue("@Course", Course.Trim());
        cmd.Parameters.AddWithValue("@MentorId", MentorId.Trim());


        cmd.ExecuteNonQuery();
    }

    public SqlDataReader SP_FA_StudentAssessment_GetAllData(string AutoNo)
    {
        Connect();
        cmd = new SqlCommand("SP_FA_StudentAssessment_GetAllData", Conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@AutoNo", AutoNo.Trim());

        SqlDataReader dr = cmd.ExecuteReader();
        return dr;

    }
    public void SP_FA_StudentAssessment_MM_Delete(string SrNo, string AutoNo, string StudentName)
    {
        Connect();
        string sqlq = "SP_FA_StudentAssessment_MM_Delete ";
        cmd = new SqlCommand(sqlq, Conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@SrNo", SrNo.Trim());
        cmd.Parameters.AddWithValue("@AutoNo", AutoNo.Trim());
        cmd.Parameters.AddWithValue("@StudentName", StudentName.Trim());


        cmd.ExecuteNonQuery();

    }
    public void SP_FA_StudentAssessment_MM_Update(string AutoNo, string Semester, string Student_Name, string Date,
        string Regularity_Classrooms, string Performance_Study, string Participation_CurricularActivities, string Physical_Status
        ,string Behaviour_Teachers_Students, string Mental_Status, string Sr_No,
        string Regularity_Classrooms_Text, string Performance_Study_Text,
        string Participation_CurricularActivities_Text, string Physical_Status_Text, string Behaviour_Teachers_Students_Text,
        string Mental_Status_Text)
    {
        Connect();
        string sqlq = "SP_FA_StudentAssessment_MM_Update";
        cmd = new SqlCommand(sqlq, Conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@AutoNo", AutoNo.Trim());
        cmd.Parameters.AddWithValue("@Semester", Semester.Trim());
        cmd.Parameters.AddWithValue("@Student_Name", Student_Name.Trim());
        cmd.Parameters.AddWithValue("@Date", System.DateTime.Now);
        cmd.Parameters.AddWithValue("@Regularity_Classrooms", Regularity_Classrooms.Trim());
        cmd.Parameters.AddWithValue("@Performance_Study", Performance_Study.Trim());
        cmd.Parameters.AddWithValue("@Participation_CurricularActivities", Participation_CurricularActivities.Trim());
        cmd.Parameters.AddWithValue("@Physical_Status", Physical_Status.Trim());
        cmd.Parameters.AddWithValue("@Behaviour_Teachers_Students", Behaviour_Teachers_Students.Trim());
        cmd.Parameters.AddWithValue("@Mental_Status", Mental_Status.Trim());
        cmd.Parameters.AddWithValue("@Sr_No", Sr_No.Trim());
        cmd.Parameters.AddWithValue("@Regularity_Classrooms_Text", Regularity_Classrooms_Text.Trim());
        cmd.Parameters.AddWithValue("@Performance_Study_Text", Performance_Study_Text.Trim());
        cmd.Parameters.AddWithValue("@Participation_CurricularActivities_Text", Participation_CurricularActivities_Text.Trim());
        cmd.Parameters.AddWithValue("@Physical_Status_Text", Physical_Status_Text.Trim());
        cmd.Parameters.AddWithValue("@Behaviour_Teachers_Students_Text", Behaviour_Teachers_Students_Text.Trim());
        cmd.Parameters.AddWithValue("@Mental_Status_Text", Mental_Status_Text.Trim());

        cmd.ExecuteNonQuery();
    }

    public SqlDataReader sp_fa_student_profile(string id, string Name, string Age)
    {
        // Ensure the connection is open
        Connect(); // Assumes Connect() opens the SQL connection

        // Define stored procedure and connection
        string sqlconn = "sp_fa_student_profile";
        SqlCommand cmd = new SqlCommand(sqlconn, Conn); // Ensure connection object Conn is valid
        cmd.CommandType = CommandType.StoredProcedure;

        // Add parameters
        cmd.Parameters.AddWithValue("@id", id); // Include id parameter
        cmd.Parameters.AddWithValue("@Name", Name);
        cmd.Parameters.AddWithValue("@Age", Age);

        try
        {
            // Execute the stored procedure and return the SqlDataReader
            SqlDataReader dr = cmd.ExecuteReader();
            return dr; // Return the SqlDataReader

        }
        catch (Exception ex)
        {
            // Handle potential exceptions
            Console.WriteLine("Error: " + ex.Message);
            return null; // Return null in case of an error
        }

        // Note: No need for finally block to Disconnect() here because the caller should handle closing the connection.
    }
    public SqlDataReader sp_FA_MM_Count_Attachement(string StudentId, string AcademicYear, string Course, string Semester)
    {
        Connect();
        cmd = new SqlCommand("sp_FA_MM_Count_Attachement", Conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@StudentId", StudentId.Trim());
        cmd.Parameters.AddWithValue("@AcademicYear", AcademicYear.Trim());
        cmd.Parameters.AddWithValue("@Course", Course.Trim());
        cmd.Parameters.AddWithValue("@Semester", Semester.Trim());





        SqlDataReader dr = cmd.ExecuteReader();
        return dr;

    }

    public SqlDataReader SP_FA_MM_Delete_Attachment(string StudentId, string AcademicYear, string Course, string Semester)
    {
        Connect();
        cmd = new SqlCommand("SP_FA_MM_Delete_Attachment", Conn);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@StudentId", StudentId.Trim());
        cmd.Parameters.AddWithValue("@AcademicYear", AcademicYear.Trim());
        cmd.Parameters.AddWithValue("@Course", Course.Trim());
        cmd.Parameters.AddWithValue("@Semester", Semester.Trim());


        cmd.CommandType = CommandType.StoredProcedure;
       

        SqlDataReader dr = cmd.ExecuteReader();
        return dr;

    }

    public SqlDataReader SP_FA_Get_All_Meeting_Records(string MentorId, string Course, string AcademicYear, string EnrollNo)
    {
        Connect();
        cmd = new SqlCommand("SP_FA_Get_All_Meeting_Records", Conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@MentorId", MentorId.Trim());
        cmd.Parameters.AddWithValue("@Course", Course.Trim());
        cmd.Parameters.AddWithValue("@AcademicYear", AcademicYear.Trim());
        cmd.Parameters.AddWithValue("@EnrollNo", EnrollNo.Trim());


        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader SP_FA_Get_All_Meeting_Records_For_Student(string Course, string AcademicYear, string EnrollNo,string Semester)
    {
        Connect();
        cmd = new SqlCommand("SP_FA_Get_All_Meeting_Records_For_Student", Conn);
        cmd.CommandType = CommandType.StoredProcedure;
        
        cmd.Parameters.AddWithValue("@Course", Course.Trim());
        cmd.Parameters.AddWithValue("@AcademicYear", AcademicYear.Trim());
        cmd.Parameters.AddWithValue("@EnrollNo", EnrollNo.Trim());
        cmd.Parameters.AddWithValue("@Semester", Semester.Trim());


        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader SP_FA_Get_All_Activity_Records(string MentorId, string Course, string AcademicYear, string EnrollNo)
    {
        Connect();
        cmd = new SqlCommand("SP_FA_Get_All_Activity_Records", Conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@MentorId", MentorId.Trim());
        cmd.Parameters.AddWithValue("@Course", Course.Trim());
        cmd.Parameters.AddWithValue("@AcademicYear", AcademicYear.Trim());
        cmd.Parameters.AddWithValue("@EnrollNo", EnrollNo.Trim());


        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public SqlDataReader SP_FA_Get_Count_Mentee(string Academic_Year, string Course, string Applicable_For_Total, string Applicable_For_Pending,string Semester,string Section)
    {
        Connect();
        cmd = new SqlCommand("SP_FA_Get_Count_Mentee", Conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Academic_Year", Academic_Year.Trim());
        cmd.Parameters.AddWithValue("@Course", Course.Trim());
        cmd.Parameters.AddWithValue("@Applicable_For_Total", Applicable_For_Total.Trim());
        cmd.Parameters.AddWithValue("@Applicable_For_Pending", Applicable_For_Pending.Trim());
        cmd.Parameters.AddWithValue("@Semester", Semester.Trim());
        cmd.Parameters.AddWithValue("@Section", Section.Trim());



        SqlDataReader dr = cmd.ExecuteReader();
        return dr;

    }

    public SqlDataReader SP_FA_Get_Count_MenteeDetails(string Academic_Year, string Course, string Applicable_For,string Semester,string Section)
    {
        Connect();
        cmd = new SqlCommand("SP_FA_Get_Count_MenteeDetails", Conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Academic_Year", Academic_Year.Trim());
        cmd.Parameters.AddWithValue("@Course", Course.Trim());
        cmd.Parameters.AddWithValue("@Applicable_For", Applicable_For.Trim());
        cmd.Parameters.AddWithValue("@Semester", Semester.Trim());
        cmd.Parameters.AddWithValue("@Section", Section.Trim());

        SqlDataReader dr = cmd.ExecuteReader();
        return dr;

    }

    public void SP_FA_StudentStatus_Update(string SrNo, string AutoNo, string Name_Student, string ApplicableFor, string StudentRemarks)
    {
        Connect();
        string sqlq = "SP_FA_StudentStatus_Update ";
        cmd = new SqlCommand(sqlq, Conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@SrNo", SrNo.Trim());
        cmd.Parameters.AddWithValue("@AutoNo", AutoNo.Trim());
        cmd.Parameters.AddWithValue("@Name_Student", Name_Student.Trim());
        cmd.Parameters.AddWithValue("@ApplicableFor", ApplicableFor.Trim());
        cmd.Parameters.AddWithValue("@StudentRemarks", StudentRemarks.Trim());

        cmd.ExecuteNonQuery();

    }

    public SqlDataReader SP_FA_Get_Student_Result(string StudentNo, string Semester, string Years, string ProgramCode)
    {
        // Ensure the connection is open
        Connect(); // Assumes Connect() opens the SQL connection

        // Define stored procedure and connection
        string sqlconn = "SP_FA_Get_Student_Result";
        SqlCommand cmd = new SqlCommand(sqlconn, Conn); // Ensure connection object Conn is valid
        cmd.CommandType = CommandType.StoredProcedure;

        // Add parameters
        cmd.Parameters.AddWithValue("@StudentNo", StudentNo); // Include id parameter
        cmd.Parameters.AddWithValue("@Semester", Semester);
        cmd.Parameters.AddWithValue("@Years", Years);
        cmd.Parameters.AddWithValue("@ProgramCode", ProgramCode);


        //try
        //{
            // Execute the stored procedure and return the SqlDataReader
            SqlDataReader dr = cmd.ExecuteReader();
            return dr; // Return the SqlDataReader

        //}
        //catch (Exception ex)
        //{
        //    // Handle potential exceptions
        //    Console.WriteLine("Error: " + ex.Message);
        //    return null; // Return null in case of an error
        //}

        // Note: No need for finally block to Disconnect() here because the caller should handle closing the connection.
    }

    public SqlDataReader SP_FA_MM_Get_SpecialAchievements_Records(string MentorId, string Course, string AcademicYear, string Student_Id)
    {
        Connect();
        cmd = new SqlCommand("SP_FA_MM_Get_SpecialAchievements_Records", Conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@MentorId", MentorId.Trim());
        cmd.Parameters.AddWithValue("@Course", Course.Trim());
        cmd.Parameters.AddWithValue("@AcademicYear", AcademicYear.Trim());
        cmd.Parameters.AddWithValue("@Student_Id", Student_Id.Trim());


        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }


    public void SP_FA_MM_Insert_SpecialAchievements(string Semester, string Achievements, string Student_Name, string Date, string ActivityName, string DetailsOfEvent, string Inter_Intra_Uni, string Position, string Remarks, string AutoNo, string StudentId, string MentorId, string MentorName, string CreatedOn, string Course, string AcademicYear)
    {
        Connect();
        string Sqlq = "SP_FA_MM_Insert_SpecialAchievements";
        cmd = new SqlCommand(Sqlq, Conn);
        cmd.CommandType = CommandType.StoredProcedure;

        // Add parameters for the stored procedure
        cmd.Parameters.AddWithValue("@Semester", Semester.Trim());
        cmd.Parameters.AddWithValue("@Achievements", Achievements.Trim());
        cmd.Parameters.AddWithValue("@Name_Student", Student_Name.Trim());
        cmd.Parameters.AddWithValue("@Date", DateTime.Parse(Date.Trim()));
        cmd.Parameters.AddWithValue("@ActivityName", ActivityName.Trim());
        cmd.Parameters.AddWithValue("@DetailsOfEvent", DetailsOfEvent.Trim());
        cmd.Parameters.AddWithValue("@Inter_Intra_Uni", Inter_Intra_Uni.Trim());
        cmd.Parameters.AddWithValue("@Position", Position.Trim());
        cmd.Parameters.AddWithValue("@Remarks", Remarks.Trim());
        cmd.Parameters.AddWithValue("@AutoNo", AutoNo.Trim());
        cmd.Parameters.AddWithValue("@Student_Id", StudentId.Trim());
        cmd.Parameters.AddWithValue("@MentorId", MentorId.Trim());
        cmd.Parameters.AddWithValue("@MentorName", MentorName.Trim());
        cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Parse(DateTime.Now.ToString().Trim()));  // Ensure CreatedOn is in correct format
        cmd.Parameters.AddWithValue("@Course", Course.Trim());
        cmd.Parameters.AddWithValue("@AcademicYear", AcademicYear.Trim());

        // Execute the stored procedure
        cmd.ExecuteNonQuery();
    }

    public void SP_FA_MM_Delete_SpecialAchievements(string SrNo, string AutoNo, string Student_Id)
    {
        Connect();
        string sqlq = "SP_FA_MM_Delete_SpecialAchievements ";
        cmd = new SqlCommand(sqlq, Conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Sr_No", SrNo.Trim());
        cmd.Parameters.AddWithValue("@AutoNo", AutoNo.Trim());
        cmd.Parameters.AddWithValue("@Student_Id", Student_Id.Trim());


        cmd.ExecuteNonQuery();

    }

    public void SP_FA_MM_Update_SpecialAchievements(string Semester, string Achievements, string Student_Name, string Date, string ActivityName, string DetailsOfEvent, string Inter_Intra_Uni, string Position, string Remarks, string AutoNo, string Sr_No, string StudentId)
    {
        Connect();
        string Sqlq = "SP_FA_MM_Update_SpecialAchievements";
        cmd = new SqlCommand(Sqlq, Conn);
        cmd.CommandType = CommandType.StoredProcedure;

        // Add parameters for the stored procedure
        cmd.Parameters.AddWithValue("@Semester", Semester.Trim());
        cmd.Parameters.AddWithValue("@Achievements", Achievements.Trim());
        cmd.Parameters.AddWithValue("@Name_Student", Student_Name.Trim());
        cmd.Parameters.AddWithValue("@Date", DateTime.Parse(Date.Trim()));
        cmd.Parameters.AddWithValue("@ActivityName", ActivityName.Trim());
        cmd.Parameters.AddWithValue("@DetailsOfEvent", DetailsOfEvent.Trim());
        cmd.Parameters.AddWithValue("@Inter_Intra_Uni", Inter_Intra_Uni.Trim());
        cmd.Parameters.AddWithValue("@Position", Position.Trim());
        cmd.Parameters.AddWithValue("@Remarks", Remarks.Trim());
        cmd.Parameters.AddWithValue("@AutoNo", AutoNo.Trim());
        cmd.Parameters.AddWithValue("@Sr_No", Sr_No.Trim());
        cmd.Parameters.AddWithValue("@Student_Id", StudentId.Trim());


        // Execute the stored procedure
        cmd.ExecuteNonQuery();
    }


    public SqlDataReader SP_FA_MM_ViewConsolidateReport(string StudentId, string Semester, string AcademicYear, string CourseCode)
    {

        Connect();
        cmd = new SqlCommand("SP_FA_MM_ViewConsolidateReport", Conn);
        cmd.CommandType = CommandType.StoredProcedure;
       
        // Add parameters
        cmd.Parameters.AddWithValue("@StudentNo", StudentId); // Include id parameter
        cmd.Parameters.AddWithValue("@Semester", Semester);
        cmd.Parameters.AddWithValue("@Years", AcademicYear);
        cmd.Parameters.AddWithValue("@ProgramCode", CourseCode);


        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

}

