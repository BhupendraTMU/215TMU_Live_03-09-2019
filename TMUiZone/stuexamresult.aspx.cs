using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
public partial class stuexamresult : System.Web.UI.Page
{
   TMUConnection con;
    string tblStudentExternal = "[Ashoka University$Student External Line - COL]";

    string tblStudentCollege = "[Ashoka University$Subject - COLLEGE]";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            con = new TMUConnection();
            if (!IsPostBack)
            {
                CalculateSGPA();
                ShowSGPA();
                Showsemester();
                showExamResult();
            }
        }
        catch (Exception)
        {
            Response.Redirect("Index.aspx");
        }
    }
    public void showExamResult()
    {

        SqlDataReader odr = con.ShowExamResult(tblStudentExternal,tblStudentCollege,Session["uid"].ToString(),ddsemester.SelectedItem.Text);
        DataTable Dt = new DataTable();
        Dt.Load(odr);
        grdCourse.DataSource = Dt;
        grdCourse.DataBind();
        odr.Close();
        con.DisConnect();
    }

    public void ShowSGPA()
    {

        SqlDataReader odr = con.ShowSGPA(Session["uid"].ToString());
        DataTable Dt = new DataTable();
        Dt.Load(odr);
        GridView1.DataSource = Dt;
        GridView1.DataBind();
        odr.Close();
        con.DisConnect();
    }

    public void Showsemester()
    {

        SqlDataReader dr = con.Show_SemesterwithStudentExternalLineCOL(tblStudentExternal, Session["uid"].ToString());
        // dr.Read();
        ddsemester.DataSource = dr;

        ddsemester.DataTextField = "Semester";


        ddsemester.DataBind();
        dr.Close();
        con.DisConnect();


    }

    decimal cpmGP = 0; decimal CGPAPlus = 0;
    public void CalculateSGPA()
    {
        con.Delete_tbl_Studentexamresult(Session["uid"].ToString());

        string sem = ""; string cp = ""; string GP = ""; decimal plusCP = 0; decimal PlusGP = 0; decimal Pluscgpas = 0; decimal c1; string backpaper = "";
        SqlDataAdapter da = new SqlDataAdapter("select distinct (Semester) as Semester from " + tblStudentExternal + " where [Student No_]='" + Session["uid"].ToString() + "'", con.Con);
            DataSet ds = new DataSet();
            da.Fill(ds, "dd");
            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {

                sem = ds.Tables[0].Rows[i]["Semester"].ToString();



                SqlDataAdapter dadata = new SqlDataAdapter("select [Cr Points],Points,Semester from " + tblStudentExternal + " where [Student No_]='" + Session["uid"].ToString() + "' and Semester='" + sem + "' and [Total Maximum]='100.00000000000000000000' ", con.Con);
                DataSet dsdata = new DataSet();
                dadata.Fill(dsdata, "dd");
                for (int j = 0; j <= dsdata.Tables[0].Rows.Count - 1; j++)
                {
                    cp = dsdata.Tables[0].Rows[j]["Cr Points"].ToString();
                    GP = dsdata.Tables[0].Rows[j]["Points"].ToString();
                    decimal cp1=Convert.ToDecimal(cp);
                    decimal GP1=Convert.ToDecimal(GP);
                    cpmGP = cp1 * GP1 + cpmGP;
                    plusCP = plusCP + cp1;

                }
                decimal SGPA = cpmGP / plusCP;
                string c = "";
                SqlDataAdapter daCGPA = new SqlDataAdapter("select [Cr Points],Points,Semester from " + tblStudentExternal + " where [Student No_]='" + Session["uid"].ToString() + "' and Semester='" + sem + "' and [Total Maximum]='100.00000000000000000000' ", con.Con);
                DataSet dsCGPA = new DataSet();
                daCGPA.Fill(dsCGPA, "dd");
                for (int p = 0; p <= dsCGPA.Tables[0].Rows.Count - 1; p++)
                {
                    c = dsCGPA.Tables[0].Rows[p]["Cr Points"].ToString();

                    decimal cp1 = Convert.ToDecimal(c);
                    Pluscgpas = Pluscgpas + cp1;
                    CGPAPlus = SGPA * cp1 + CGPAPlus;
                   

                }
                decimal finalCGPA = CGPAPlus / Pluscgpas;

                SqlDataReader dr = con.CountBackpaper(Session["uid"].ToString(), tblStudentExternal, sem);
                dr.Read();
                if (dr.HasRows)
                {
                    backpaper = dr["Result"].ToString();
                }
                dr.Close();
                con.Insert_tbl_Studentexamresult(Session["uid"].ToString(), sem, SGPA.ToString("00.00"), finalCGPA.ToString("00.00"), backpaper);

            }

    }
    protected void ddsemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        showExamResult();
    }
}