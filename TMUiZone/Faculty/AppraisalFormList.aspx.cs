using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

public partial class Faculty_pmsapproval : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["uid"] == null || string.IsNullOrEmpty(Session["uid"].ToString()))
        {
            //Response.Redirect("Default.aspx");
        }
        else
        {
            if (!IsPostBack)
            {
                bindAcademicYear();
                point();
                facultylist();
             


                // bindpms();
                // Checkpms();
            }
        }
            
            
    }
    public void bindAcademicYear()
    {
        SqlCommand cmd = new SqlCommand("[EDUCOLLEGELIVE-R2].dbo.proc_GetAcademicYearAppraial", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt1 = new DataTable();
        da.Fill(dt1);
        drpAcademic.DataSource = dt1;
        drpAcademic.DataTextField = "Details";
        drpAcademic.DataValueField = "No_";
        drpAcademic.DataBind();

    }

    public void HREnble()
    {
        txt7tpoint.Enabled = true;
        txt7flg.Enabled = true;
        txtp7r1.Enabled = true;
        txtp7r2.Enabled = true;
        txtp7r3.Enabled = true;

    }

    public void point()
    {
        SqlCommand cmd = new SqlCommand("proc_GetAcademicYearAppraial", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt1 = new DataTable();
        con1.Open();
        da.Fill(dt1);
        con1.Close();
        lblAcad.Text = dt1.Rows[0]["Details"].ToString();
    }

    private void facultylist(string sortExpression = null)
    {
        SqlCommand cmd = new SqlCommand("proc_Gettbl_PMSHODPrinciple", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Fid", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@AcadmicYear", drpAcademic.SelectedValue);
        cmd.Parameters.AddWithValue("@UserGroup", Session["UserGroup"]);
        cmd.Parameters.AddWithValue("@GlobalDimension", Session["GlobalDimension1Code"]);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
           using (DataTable dt = new DataTable())
                {
                    con.Open();
                    da.Fill(dt);
                    con.Close();
                    if (sortExpression != null)
                    {
                        DataView dv = dt.AsDataView();
                        this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";
 
                        dv.Sort = sortExpression + " " + this.SortDirection;
                        GrdExamList.DataSource = dv;
                    }
                    else
                    {
                        GrdExamList.DataSource = dt;
                    }
                    GrdExamList.DataBind();
                }
                    //GrdExamList.DataSource = dt;
        //GrdExamList.DataBind();
    }


    public void Checkpms()
    {
        SqlCommand cmd = new SqlCommand("proc_Gettbl_PMS", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Fid", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@AcadmicYear", drpAcademic.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt1 = new DataTable();
        con.Open();
        da.Fill(dt1);
        con.Close();
        if (dt1.Rows.Count > 0)
        {
            Enable();
            btnApproved.Visible = false;
            btntest.Visible = true;
            btnSave.Visible = false;
        }
        else
        {
            btnApproved.Visible = true;
            btnSave.Visible = true;
        }
    }

  
    
    
    public void Enable()
    {
txtct1.Enabled=false;
txtct2.Enabled=false;
txtct3.Enabled = true;
txtsf11.Enabled=false;
txtsf12.Enabled=false;
//txtsf13.Enabled = true;
txtsf21.Enabled=false;
txtsf22.Enabled=false;
//txtsf23.Enabled = true;
txtsf31.Enabled=false;
txtsf32.Enabled=false;
//txtsf33.Enabled = true;
txtsf41.Enabled=false;
txtsf42.Enabled=false;
//txtsf43.Enabled = true;
txtsf51.Enabled=false;
txtsf52.Enabled=false;
//txtsf53.Enabled = true;
txtsf61.Enabled=false;
txtsf62.Enabled=false;
//txtsf63.Enabled = true;
//txtsf66.Enabled = true;
//txtsf69.Enabled = true;
//txtsf72.Enabled = true;
//txtsf75.Enabled = true;
//txtsf78.Enabled = true;
//txtsf81.Enabled = true;



txtip11.Enabled=false;
txtip12.Enabled=false;
txtip13.Enabled = true;
txtip21.Enabled=false;
txtip22.Enabled=false;
txtip23.Enabled = true;
txtip31.Enabled=false;
txtip32.Enabled=false;
txtip33.Enabled = true;
txtNc11.Enabled=false;
txtNc12.Enabled=false;
txtNc13.Enabled = true;
txtNc21.Enabled=false;
txtNc22.Enabled=false;
txtNc23.Enabled = true;
txtNc31.Enabled=false;
txtNc32.Enabled=false;
txtNc33.Enabled = true;
txtNc36.Enabled = true;
txtNc39.Enabled = true;
txtNc42.Enabled = true;
txtcsm11.Enabled=false;
txtcsm12.Enabled=false;
txtcsm13.Enabled = true;
txtcsm21.Enabled=false;
txtcsm22.Enabled=false;
txtcsm23.Enabled = true;
txtpws11.Enabled=false;
txtpws12.Enabled=false;
txtpws13.Enabled = true;
txtpws21.Enabled=false;
txtpws22.Enabled=false;
txtpws23.Enabled = true;
txtpws31.Enabled=false;
txtpws32.Enabled=false;
txtpws33.Enabled = true;
txtpws36.Enabled = true;
txtpwA11.Enabled=false;
txtpwA12.Enabled=false;
txtpwA13.Enabled=false;
txtpp11.Enabled=false;
txtpp12.Enabled=false;
txtpp13.Enabled = true;
txtpp21.Enabled=false;
txtpp22.Enabled=false;
txtpp23.Enabled = true;
txtpp31.Enabled=false;
txtpp32.Enabled=false;
txtpp33.Enabled = true;
txtpp41.Enabled=false;
txtpp42.Enabled=false;
txtpp43.Enabled = true;
txtpp51.Enabled=false;
txtpp52.Enabled=false;
txtpp53.Enabled = true;
txtpp61.Enabled=false;
txtpp62.Enabled=false;
txtpp63.Enabled = true;
txtrs11.Enabled=false;
txtrs12.Enabled=false;
txtrs13.Enabled = true;
txtrs21.Enabled=false;
txtrs22.Enabled=false;
txtrs23.Enabled = true;
txtrs31.Enabled=false;
txtrs32.Enabled=false;
txtrs33.Enabled = true;
txtrs41.Enabled=false;
txtrs42.Enabled=false;
txtrs43.Enabled = true;
txtrpp11.Enabled=false;
txtrpp12.Enabled=false;
txtrpp13.Enabled = true;
txtrpp21.Enabled=false;
txtrpp22.Enabled=false;
txtrpp23.Enabled = true;
txtrpp31.Enabled=false;
txtrpp32.Enabled=false;
txtrpp33.Enabled = true;
txtrpro11.Enabled=false;
txtrpro12.Enabled=false;
txtrpro13.Enabled = true;
txtrpro21.Enabled=false;
txtrpro22.Enabled=false;
txtrpro23.Enabled = true;
txtrpro31.Enabled=false;
txtrpro32.Enabled=false;
txtrpro33.Enabled = true;
txtrpro41.Enabled=false;
txtrpro42.Enabled=false;
txtrpro43.Enabled = true;
txtrpro51.Enabled=false;
txtrpro52.Enabled=false;
txtrpro53.Enabled = true;
txtrpro56.Enabled = true;
txtrproT11.Enabled=false;
txtrproT12.Enabled=false;
txtrproT13.Enabled=false;
txtinb11.Enabled=false;
txtinb12.Enabled=false;
txtinb13.Enabled = true;
txtinb21.Enabled=false;
txtinb22.Enabled=false;
txtinb23.Enabled = true;
txtinb31.Enabled=false;
txtinb32.Enabled=false;
txtinb33.Enabled = true;
txtinb41.Enabled=false;
txtinb42.Enabled=false;
txtinb43.Enabled = true;
txtinb51.Enabled=false;
txtinb52.Enabled=false;
txtinb53.Enabled = true;
txtinb61.Enabled=false;
txtinb62.Enabled=false;
txtinb63.Enabled = true;
txtinb71.Enabled=false;
txtinb72.Enabled=false;
txtinb73.Enabled = true;

txtinb76.Enabled = true;

txtinb79.Enabled = true;

txtinbT11.Enabled=false;
txtinbT12.Enabled=false;
txtinbT13.Enabled=false;
txtsd11.Enabled=false;
txtsd12.Enabled=false;
txtsd13.Enabled = true;
txtsd21.Enabled=false;
txtsd22.Enabled=false;
txtsd23.Enabled = true;
txtsd31.Enabled=false;
txtsd32.Enabled=false;
txtsd33.Enabled = true;
txtsd41.Enabled=false;
txtsd42.Enabled=false;
txtsd43.Enabled = true;
txtsd51.Enabled=false;
txtsd52.Enabled=false;
txtsd53.Enabled = true;
txtsd61.Enabled=false;
txtsd62.Enabled=false;
txtsd63.Enabled = true;
txtsd71.Enabled=false;
txtsd72.Enabled=false;
txtsd73.Enabled = true;
txtsd76.Enabled = true;
txtsd79.Enabled = true;

txtsdT11.Enabled=false;
txtsdT12.Enabled=false;
txtsdT13.Enabled=false;
txtpcm11.Enabled=false;
txtpcm12.Enabled=false;
txtpcm13.Enabled = true;
txtpcm21.Enabled=false;
txtpcm22.Enabled=false;
txtpcm23.Enabled = true;
txtpcm31.Enabled=false;
txtpcm32.Enabled=false;
txtpcm33.Enabled = true;
txtpcm41.Enabled=false;
txtpcm42.Enabled=false;
txtpcm43.Enabled = true;
txtpcm51.Enabled=false;
txtpcm52.Enabled=false;
txtpcm53.Enabled = true;
txtpcm61.Enabled=false;
txtpcm62.Enabled=false;
txtpcm63.Enabled = true;
txtpcmT11.Enabled=false;
txtpcmT12.Enabled=false;
txtpcmT13.Enabled=false;
txtpiis11.Enabled=false;
txtpiis12.Enabled=false;
txtpiis13.Enabled = true;
txtpiis21.Enabled=false;
txtpiis22.Enabled=false;
txtpiis23.Enabled = true;
txtpiis31.Enabled=false;
txtpiis32.Enabled=false;
txtpiis33.Enabled = true;
txtpiis41.Enabled=false;
txtpiis42.Enabled=false;
txtpiis43.Enabled = true;
txtpiis51.Enabled=false;
txtpiis52.Enabled=false;
txtpiis53.Enabled = true;

txtpiis56.Enabled = true;

txtpiis59.Enabled = true;

txtpiisT11.Enabled=false;
txtpiisT12.Enabled=false;
txtpiisT13.Enabled=false;
txtssc11.Enabled=false;
txtssc12.Enabled=false;
txtssc13.Enabled = true;
txtssc21.Enabled=false;
txtssc22.Enabled=false;
txtssc23.Enabled = true;
txtssc31.Enabled=false;
txtssc32.Enabled=false;
txtssc33.Enabled = true;
txtssc36.Enabled = true;
txtssc39.Enabled = true;
txtsscT1.Enabled=false;
txtsscT2.Enabled=false;
txtsscT3.Enabled=false;
txtcsw11.Enabled=false;
txtcsw12.Enabled=false;
txtcsw13.Enabled = true;
txtcsw21.Enabled=false;
txtcsw22.Enabled=false;
txtcsw23.Enabled = true;
txtcsw31.Enabled=false;
txtcsw32.Enabled=false;
txtcsw33.Enabled = true;
txtcsw41.Enabled=false;
txtcsw42.Enabled=false;
txtcsw43.Enabled = true;
txtcsw51.Enabled=false;
txtcsw52.Enabled=false;
txtcsw53.Enabled = true;
txtcswT11.Enabled=false;
txtcswT12.Enabled=false;
txtcswT13.Enabled=false;
txtGGT11.Enabled=false;
txtGGT12.Enabled=false;
txtGGT13.Enabled=false;
txtp2a.Enabled = true;
txtp2b.Enabled = true;
txtp2c.Enabled = true;
txtp2d.Enabled = true;
txtp2e.Enabled = true;
datep2.Enabled = true;
SignAssp2.Enabled = true;
SignCos.Enabled = true;
txtp3a.Enabled = true;
txtp3b.Enabled = true;
txtp3c.Enabled = true;
txtp3d.Enabled = true;
txtp3e.Enabled = true;
txtp3f.Enabled = true;
txtp3g.Enabled = true;
txtp3h.Enabled = true;
txtp3i.Enabled = true;
txtp3j.Enabled = true;
txtp41point.Enabled = true;
txtp43point.Enabled = true;
txtp51point.Enabled = true;
txtp53point.Enabled = true;
txt6coments.Enabled = true;
txt7tpoint.Enabled=false;
txt7flg.Enabled=false;
txtp7r1.Enabled=false;
txtp7r2.Enabled=false;
txtp7r3.Enabled=false;

    }

    public void EnableF()
    {
        txtct1.Enabled = false;
        txtct2.Enabled = false;
        txtct3.Enabled = false;
        txtsf11.Enabled = false;
        txtsf12.Enabled = false;
        txtsf13.Enabled = false;
        txtsf21.Enabled = false;
        txtsf22.Enabled = false;
        txtsf23.Enabled = false;
        txtsf31.Enabled = false;
        txtsf32.Enabled = false;
        txtsf33.Enabled = false;
        txtsf41.Enabled = false;
        txtsf42.Enabled = false;
        txtsf43.Enabled = false;
        txtsf51.Enabled = false;
        txtsf52.Enabled = false;
        txtsf53.Enabled = false;
        txtsf61.Enabled = false;
        txtsf62.Enabled = false;
        txtsf63.Enabled = false;
        txtsf66.Enabled = false;
        txtsf69.Enabled = false;
        txtsf72.Enabled = false;
        txtsf75.Enabled = false;
        txtsf78.Enabled = false;
        txtsf81.Enabled = false;
        txtip11.Enabled = false;
        txtip12.Enabled = false;
        txtip13.Enabled = false;
        txtip21.Enabled = false;
        txtip22.Enabled = false;
        txtip23.Enabled = false;
        txtip31.Enabled = false;
        txtip32.Enabled = false;
        txtip33.Enabled = false;
        txtNc11.Enabled = false;
        txtNc12.Enabled = false;
        txtNc13.Enabled = false;
        txtNc21.Enabled = false;
        txtNc22.Enabled = false;
        txtNc23.Enabled = false;
        txtNc31.Enabled = false;
        txtNc32.Enabled = false;
        txtNc33.Enabled = false;
        txtNc36.Enabled = false;
        txtNc39.Enabled = false;
        txtNc42.Enabled = false;
        txtcsm11.Enabled = false;
        txtcsm12.Enabled = false;
        txtcsm13.Enabled = false;
        txtcsm21.Enabled = false;
        txtcsm22.Enabled = false;
        txtcsm23.Enabled = false;
        txtpws11.Enabled = false;
        txtpws12.Enabled = false;
        txtpws13.Enabled = false;
        txtpws21.Enabled = false;
        txtpws22.Enabled = false;
        txtpws23.Enabled = false;
        txtpws31.Enabled = false;
        txtpws32.Enabled = false;
        txtpws33.Enabled = false;
        txtpws36.Enabled = false;
        txtpwA11.Enabled = false;
        txtpwA12.Enabled = false;
        txtpwA13.Enabled = false;
        txtpp11.Enabled = false;
        txtpp12.Enabled = false;
        txtpp13.Enabled = false;
        txtpp21.Enabled = false;
        txtpp22.Enabled = false;
        txtpp23.Enabled = false;
        txtpp31.Enabled = false;
        txtpp32.Enabled = false;
        txtpp33.Enabled = false;
        txtpp41.Enabled = false;
        txtpp42.Enabled = false;
        txtpp43.Enabled = false;
        txtpp51.Enabled = false;
        txtpp52.Enabled = false;
        txtpp53.Enabled = false;
        txtpp61.Enabled = false;
        txtpp62.Enabled = false;
        txtpp63.Enabled = false;
        txtrs11.Enabled = false;
        txtrs12.Enabled = false;
        txtrs13.Enabled = false;
        txtrs21.Enabled = false;
        txtrs22.Enabled = false;
        txtrs23.Enabled = false;
        txtrs31.Enabled = false;
        txtrs32.Enabled = false;
        txtrs33.Enabled = false;
        txtrs41.Enabled = false;
        txtrs42.Enabled = false;
        txtrs43.Enabled = false;
        txtrpp11.Enabled = false;
        txtrpp12.Enabled = false;
        txtrpp13.Enabled = false;
        txtrpp21.Enabled = false;
        txtrpp22.Enabled = false;
        txtrpp23.Enabled = false;
        txtrpp31.Enabled = false;
        txtrpp32.Enabled = false;
        txtrpp33.Enabled = false;
        txtrpro11.Enabled = false;
        txtrpro12.Enabled = false;
        txtrpro13.Enabled = false;
        txtrpro21.Enabled = false;
        txtrpro22.Enabled = false;
        txtrpro23.Enabled = false;
        txtrpro31.Enabled = false;
        txtrpro32.Enabled = false;
        txtrpro33.Enabled = false;
        txtrpro41.Enabled = false;
        txtrpro42.Enabled = false;
        txtrpro43.Enabled = false;
        txtrpro51.Enabled = false;
        txtrpro52.Enabled = false;
        txtrpro53.Enabled = false;
        txtrpro56.Enabled = false;
        txtrproT11.Enabled = false;
        txtrproT12.Enabled = false;
        txtrproT13.Enabled = false;
        txtinb11.Enabled = false;
        txtinb12.Enabled = false;
        txtinb13.Enabled = false;
        txtinb21.Enabled = false;
        txtinb22.Enabled = false;
        txtinb23.Enabled = false;
        txtinb31.Enabled = false;
        txtinb32.Enabled = false;
        txtinb33.Enabled = false;
        txtinb41.Enabled = false;
        txtinb42.Enabled = false;
        txtinb43.Enabled = false;
        txtinb51.Enabled = false;
        txtinb52.Enabled = false;
        txtinb53.Enabled = false;
        txtinb61.Enabled = false;
        txtinb62.Enabled = false;
        txtinb63.Enabled = false;
        txtinb71.Enabled = false;
        txtinb72.Enabled = false;
        txtinb73.Enabled = false;
        txtinb76.Enabled = false;
        txtinb79.Enabled = false;
        txtinbT11.Enabled = false;
        txtinbT12.Enabled = false;
        txtinbT13.Enabled = false;
        txtsd11.Enabled = false;
        txtsd12.Enabled = false;
        txtsd13.Enabled = false;
        txtsd21.Enabled = false;
        txtsd22.Enabled = false;
        txtsd23.Enabled = false;
        txtsd31.Enabled = false;
        txtsd32.Enabled = false;
        txtsd33.Enabled = false;
        txtsd41.Enabled = false;
        txtsd42.Enabled = false;
        txtsd43.Enabled = false;
        txtsd51.Enabled = false;
        txtsd52.Enabled = false;
        txtsd53.Enabled = false;
        txtsd61.Enabled = false;
        txtsd62.Enabled = false;
        txtsd63.Enabled = false;
        txtsd71.Enabled = false;
        txtsd72.Enabled = false;
        txtsd73.Enabled = false;
        txtsd76.Enabled = false;
        txtsd79.Enabled = false;
        txtsdT11.Enabled = false;
        txtsdT12.Enabled = false;
        txtsdT13.Enabled = false;
        txtpcm11.Enabled = false;
        txtpcm12.Enabled = false;
        txtpcm13.Enabled = false;
        txtpcm21.Enabled = false;
        txtpcm22.Enabled = false;
        txtpcm23.Enabled = false;
        txtpcm31.Enabled = false;
        txtpcm32.Enabled = false;
        txtpcm33.Enabled = false;
        txtpcm41.Enabled = false;
        txtpcm42.Enabled = false;
        txtpcm43.Enabled = false;
        txtpcm51.Enabled = false;
        txtpcm52.Enabled = false;
        txtpcm53.Enabled = false;
        txtpcm61.Enabled = false;
        txtpcm62.Enabled = false;
        txtpcm63.Enabled = false;
        txtpcmT11.Enabled = false;
        txtpcmT12.Enabled = false;
        txtpcmT13.Enabled = false;
        txtpiis11.Enabled = false;
        txtpiis12.Enabled = false;
        txtpiis13.Enabled = false;
        txtpiis21.Enabled = false;
        txtpiis22.Enabled = false;
        txtpiis23.Enabled = false;
        txtpiis31.Enabled = false;
        txtpiis32.Enabled = false;
        txtpiis33.Enabled = false;
        txtpiis41.Enabled = false;
        txtpiis42.Enabled = false;
        txtpiis43.Enabled = false;
        txtpiis51.Enabled = false;
        txtpiis52.Enabled = false;
        txtpiis53.Enabled = false;
        txtpiis56.Enabled = false;
        txtpiis59.Enabled = false;
        txtpiisT11.Enabled = false;
        txtpiisT12.Enabled = false;
        txtpiisT13.Enabled = false;
        txtssc11.Enabled = false;
        txtssc12.Enabled = false;
        txtssc13.Enabled = false;
        txtssc21.Enabled = false;
        txtssc22.Enabled = false;
        txtssc23.Enabled = false;
        txtssc31.Enabled = false;
        txtssc32.Enabled = false;
        txtssc33.Enabled = false;
        txtssc36.Enabled = false;
        txtssc39.Enabled = false;
        txtsscT1.Enabled = false;
        txtsscT2.Enabled = false;
        txtsscT3.Enabled = false;
        txtcsw11.Enabled = false;
        txtcsw12.Enabled = false;
        txtcsw13.Enabled = false;
        txtcsw21.Enabled = false;
        txtcsw22.Enabled = false;
        txtcsw23.Enabled = false;
        txtcsw31.Enabled = false;
        txtcsw32.Enabled = false;
        txtcsw33.Enabled = false;
        txtcsw41.Enabled = false;
        txtcsw42.Enabled = false;
        txtcsw43.Enabled = false;
        txtcsw51.Enabled = false;
        txtcsw52.Enabled = false;
        txtcsw53.Enabled = false;
        txtcswT11.Enabled = false;
        txtcswT12.Enabled = false;
        txtcswT13.Enabled = false;
        txtGGT11.Enabled = false;
        txtGGT12.Enabled = false;
        txtGGT13.Enabled = false;
        txtp2a.Enabled = false;
        txtp2b.Enabled = false;
        txtp2c.Enabled = false;
        txtp2d.Enabled = false;
        txtp2e.Enabled = false;
        datep2.Enabled = false;
        SignAssp2.Enabled = false;
        SignCos.Enabled = false;
        txtp3a.Enabled = false;
        txtp3b.Enabled = false;
        txtp3c.Enabled = false;
        txtp3d.Enabled = false;
        txtp3e.Enabled = false;
        txtp3f.Enabled = false;
        txtp3g.Enabled = false;
        txtp3h.Enabled = false;
        txtp3i.Enabled = false;
        txtp3j.Enabled = false;
        txtp41point.Enabled = false;
        txtp43point.Enabled = false;
        txtp51point.Enabled = false;
        txtp53point.Enabled = false;
        txt6coments.Enabled = false;
        txt7tpoint.Enabled = false;
        txt7flg.Enabled = false;
        txtp7r1.Enabled = false;
        txtp7r2.Enabled = false;
        txtp7r3.Enabled = false;
    }
    
    
    public void submit()
    {
        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("PMSAproval", con);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@Fid", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Hid", FF.Value);
            // cmd.Parameters.AddWithValue("@Pid", "");
            cmd.Parameters.AddWithValue("@AcademicYear", drpAcademic.SelectedValue);

            cmd.Parameters.AddWithValue("@txtct1", txtct1.Text);
            cmd.Parameters.AddWithValue("@txtct2", txtct2.Text);
            cmd.Parameters.AddWithValue("@txtct3", txtct3.Text);
            cmd.Parameters.AddWithValue("@txtsf11", txtsf11.Text);
            cmd.Parameters.AddWithValue("@txtsf12", txtsf12.Text);
            cmd.Parameters.AddWithValue("@txtsf13", txtsf13.Text);
            cmd.Parameters.AddWithValue("@txtsf21", txtsf21.Text);
            cmd.Parameters.AddWithValue("@txtsf22", txtsf22.Text);
            cmd.Parameters.AddWithValue("@txtsf23", txtsf23.Text);
            cmd.Parameters.AddWithValue("@txtsf31", txtsf31.Text);
            cmd.Parameters.AddWithValue("@txtsf32", txtsf32.Text);
            cmd.Parameters.AddWithValue("@txtsf33", txtsf33.Text);
            cmd.Parameters.AddWithValue("@txtsf41", txtsf41.Text);
            cmd.Parameters.AddWithValue("@txtsf42", txtsf42.Text);
            cmd.Parameters.AddWithValue("@txtsf43", txtsf43.Text);
            cmd.Parameters.AddWithValue("@txtsf51", txtsf51.Text);
            cmd.Parameters.AddWithValue("@txtsf52", txtsf52.Text);
            cmd.Parameters.AddWithValue("@txtsf53", txtsf53.Text);
            cmd.Parameters.AddWithValue("@txtsf61", txtsf61.Text);
            cmd.Parameters.AddWithValue("@txtsf62", txtsf62.Text);
            cmd.Parameters.AddWithValue("@txtsf63", txtsf63.Text);

            cmd.Parameters.AddWithValue("@txtsf64", txtsf64.Text);
            cmd.Parameters.AddWithValue("@txtsf65", txtsf65.Text);
            cmd.Parameters.AddWithValue("@txtsf66", txtsf66.Text);
            cmd.Parameters.AddWithValue("@txtsf67", txtsf67.Text);
            cmd.Parameters.AddWithValue("@txtsf68", txtsf68.Text);
            cmd.Parameters.AddWithValue("@txtsf69", txtsf69.Text);
            cmd.Parameters.AddWithValue("@txtsf70", txtsf70.Text);
            cmd.Parameters.AddWithValue("@txtsf71", txtsf71.Text);
            cmd.Parameters.AddWithValue("@txtsf72", txtsf72.Text);
            cmd.Parameters.AddWithValue("@txtsf73", txtsf73.Text);
            cmd.Parameters.AddWithValue("@txtsf74", txtsf74.Text);
            cmd.Parameters.AddWithValue("@txtsf75", txtsf75.Text);
            cmd.Parameters.AddWithValue("@txtsf76", txtsf76.Text);
            cmd.Parameters.AddWithValue("@txtsf77", txtsf77.Text);
            cmd.Parameters.AddWithValue("@txtsf78", txtsf78.Text);
            cmd.Parameters.AddWithValue("@txtsf79", txtsf79.Text);
            cmd.Parameters.AddWithValue("@txtsf80", txtsf80.Text);
            cmd.Parameters.AddWithValue("@txtsf81", txtsf81.Text);











            cmd.Parameters.AddWithValue("@txtip11", txtip11.Text);
            cmd.Parameters.AddWithValue("@txtip12", txtip12.Text);
            cmd.Parameters.AddWithValue("@txtip13", txtip13.Text);
            cmd.Parameters.AddWithValue("@txtip21", txtip21.Text);
            cmd.Parameters.AddWithValue("@txtip22", txtip22.Text);
            cmd.Parameters.AddWithValue("@txtip23", txtip23.Text);
            cmd.Parameters.AddWithValue("@txtip31", txtip31.Text);
            cmd.Parameters.AddWithValue("@txtip32", txtip32.Text);
            cmd.Parameters.AddWithValue("@txtip33", txtip33.Text);
            cmd.Parameters.AddWithValue("@txtNc11", txtNc11.Text);
            cmd.Parameters.AddWithValue("@txtNc12", txtNc12.Text);
            cmd.Parameters.AddWithValue("@txtNc13", txtNc13.Text);
            cmd.Parameters.AddWithValue("@txtNc21", txtNc21.Text);
            cmd.Parameters.AddWithValue("@txtNc22", txtNc22.Text);
            cmd.Parameters.AddWithValue("@txtNc23", txtNc23.Text);
            cmd.Parameters.AddWithValue("@txtNc31", txtNc31.Text);
            cmd.Parameters.AddWithValue("@txtNc32", txtNc32.Text);
            cmd.Parameters.AddWithValue("@txtNc33", txtNc33.Text);

            cmd.Parameters.AddWithValue("@txtNc34", txtNc34.Text);
            cmd.Parameters.AddWithValue("@txtNc35", txtNc35.Text);
            cmd.Parameters.AddWithValue("@txtNc36", txtNc36.Text);
            cmd.Parameters.AddWithValue("@txtNc37", txtNc37.Text);
            cmd.Parameters.AddWithValue("@txtNc38", txtNc38.Text);
            cmd.Parameters.AddWithValue("@txtNc39", txtNc39.Text);
            cmd.Parameters.AddWithValue("@txtNc40", txtNc40.Text);
            cmd.Parameters.AddWithValue("@txtNc41", txtNc41.Text);
            cmd.Parameters.AddWithValue("@txtNc42", txtNc42.Text);





            cmd.Parameters.AddWithValue("@txtcsm11", txtcsm11.Text);
            cmd.Parameters.AddWithValue("@txtcsm12", txtcsm12.Text);
            cmd.Parameters.AddWithValue("@txtcsm13", txtcsm13.Text);
            cmd.Parameters.AddWithValue("@txtcsm21", txtcsm21.Text);
            cmd.Parameters.AddWithValue("@txtcsm22", txtcsm22.Text);
            cmd.Parameters.AddWithValue("@txtcsm23", txtcsm23.Text);
            cmd.Parameters.AddWithValue("@txtpws11", txtpws11.Text);
            cmd.Parameters.AddWithValue("@txtpws12", txtpws12.Text);
            cmd.Parameters.AddWithValue("@txtpws13", txtpws13.Text);
            cmd.Parameters.AddWithValue("@txtpws21", txtpws21.Text);
            cmd.Parameters.AddWithValue("@txtpws22", txtpws22.Text);
            cmd.Parameters.AddWithValue("@txtpws23", txtpws23.Text);
            cmd.Parameters.AddWithValue("@txtpws31", txtpws31.Text);
            cmd.Parameters.AddWithValue("@txtpws32", txtpws32.Text);
            cmd.Parameters.AddWithValue("@txtpws33", txtpws33.Text);

            cmd.Parameters.AddWithValue("@txtpws34", txtpws34.Text);
            cmd.Parameters.AddWithValue("@txtpws35", txtpws35.Text);
            cmd.Parameters.AddWithValue("@txtpws36", txtpws36.Text);



            cmd.Parameters.AddWithValue("@txtpwA11", txtpwA11.Text);
            cmd.Parameters.AddWithValue("@txtpwA12", txtpwA12.Text);
            cmd.Parameters.AddWithValue("@txtpwA13", txtpwA13.Text);
            cmd.Parameters.AddWithValue("@txtpp11", txtpp11.Text);
            cmd.Parameters.AddWithValue("@txtpp12", txtpp12.Text);
            cmd.Parameters.AddWithValue("@txtpp13", txtpp13.Text);
            cmd.Parameters.AddWithValue("@txtpp21", txtpp21.Text);
            cmd.Parameters.AddWithValue("@txtpp22", txtpp22.Text);
            cmd.Parameters.AddWithValue("@txtpp23", txtpp23.Text);
            cmd.Parameters.AddWithValue("@txtpp31", txtpp31.Text);
            cmd.Parameters.AddWithValue("@txtpp32", txtpp32.Text);
            cmd.Parameters.AddWithValue("@txtpp33", txtpp33.Text);
            cmd.Parameters.AddWithValue("@txtpp41", txtpp41.Text);
            cmd.Parameters.AddWithValue("@txtpp42", txtpp42.Text);
            cmd.Parameters.AddWithValue("@txtpp43", txtpp43.Text);
            cmd.Parameters.AddWithValue("@txtpp51", txtpp51.Text);
            cmd.Parameters.AddWithValue("@txtpp52", txtpp52.Text);
            cmd.Parameters.AddWithValue("@txtpp53", txtpp53.Text);
            cmd.Parameters.AddWithValue("@txtpp61", txtpp61.Text);
            cmd.Parameters.AddWithValue("@txtpp62", txtpp62.Text);
            cmd.Parameters.AddWithValue("@txtpp63", txtpp63.Text);
            cmd.Parameters.AddWithValue("@txtrs11", txtrs11.Text);
            cmd.Parameters.AddWithValue("@txtrs12", txtrs12.Text);
            cmd.Parameters.AddWithValue("@txtrs13", txtrs13.Text);
            cmd.Parameters.AddWithValue("@txtrs21", txtrs21.Text);
            cmd.Parameters.AddWithValue("@txtrs22", txtrs22.Text);
            cmd.Parameters.AddWithValue("@txtrs23", txtrs23.Text);
            cmd.Parameters.AddWithValue("@txtrs31", txtrs31.Text);
            cmd.Parameters.AddWithValue("@txtrs32", txtrs32.Text);
            cmd.Parameters.AddWithValue("@txtrs33", txtrs33.Text);
            cmd.Parameters.AddWithValue("@txtrs41", txtrs41.Text);
            cmd.Parameters.AddWithValue("@txtrs42", txtrs42.Text);
            cmd.Parameters.AddWithValue("@txtrs43", txtrs43.Text);
            cmd.Parameters.AddWithValue("@txtrpp11", txtrpp11.Text);
            cmd.Parameters.AddWithValue("@txtrpp12", txtrpp12.Text);
            cmd.Parameters.AddWithValue("@txtrpp13", txtrpp13.Text);
            cmd.Parameters.AddWithValue("@txtrpp21", txtrpp21.Text);
            cmd.Parameters.AddWithValue("@txtrpp22", txtrpp22.Text);
            cmd.Parameters.AddWithValue("@txtrpp23", txtrpp23.Text);
            cmd.Parameters.AddWithValue("@txtrpp31", txtrpp31.Text);
            cmd.Parameters.AddWithValue("@txtrpp32", txtrpp32.Text);
            cmd.Parameters.AddWithValue("@txtrpp33", txtrpp33.Text);
            cmd.Parameters.AddWithValue("@txtrpro11", txtrpro11.Text);
            cmd.Parameters.AddWithValue("@txtrpro12", txtrpro12.Text);
            cmd.Parameters.AddWithValue("@txtrpro13", txtrpro13.Text);
            cmd.Parameters.AddWithValue("@txtrpro21", txtrpro21.Text);
            cmd.Parameters.AddWithValue("@txtrpro22", txtrpro22.Text);
            cmd.Parameters.AddWithValue("@txtrpro23", txtrpro23.Text);
            cmd.Parameters.AddWithValue("@txtrpro31", txtrpro31.Text);
            cmd.Parameters.AddWithValue("@txtrpro32", txtrpro32.Text);
            cmd.Parameters.AddWithValue("@txtrpro33", txtrpro33.Text);
            cmd.Parameters.AddWithValue("@txtrpro41", txtrpro41.Text);
            cmd.Parameters.AddWithValue("@txtrpro42", txtrpro42.Text);
            cmd.Parameters.AddWithValue("@txtrpro43", txtrpro43.Text);
            cmd.Parameters.AddWithValue("@txtrpro51", txtrpro51.Text);
            cmd.Parameters.AddWithValue("@txtrpro52", txtrpro52.Text);
            cmd.Parameters.AddWithValue("@txtrpro53", txtrpro53.Text);

            cmd.Parameters.AddWithValue("@txtrpro54", txtrpro54.Text);
            cmd.Parameters.AddWithValue("@txtrpro55", txtrpro55.Text);
            cmd.Parameters.AddWithValue("@txtrpro56", txtrpro56.Text);





            cmd.Parameters.AddWithValue("@txtrproT11", txtrproT11.Text);
            cmd.Parameters.AddWithValue("@txtrproT12", txtrproT12.Text);
            cmd.Parameters.AddWithValue("@txtrproT13", txtrproT13.Text);
            cmd.Parameters.AddWithValue("@txtinb11", txtinb11.Text);
            cmd.Parameters.AddWithValue("@txtinb12", txtinb12.Text);
            cmd.Parameters.AddWithValue("@txtinb13", txtinb13.Text);
            cmd.Parameters.AddWithValue("@txtinb21", txtinb21.Text);
            cmd.Parameters.AddWithValue("@txtinb22", txtinb22.Text);
            cmd.Parameters.AddWithValue("@txtinb23", txtinb23.Text);
            cmd.Parameters.AddWithValue("@txtinb31", txtinb31.Text);
            cmd.Parameters.AddWithValue("@txtinb32", txtinb32.Text);
            cmd.Parameters.AddWithValue("@txtinb33", txtinb33.Text);
            cmd.Parameters.AddWithValue("@txtinb41", txtinb41.Text);
            cmd.Parameters.AddWithValue("@txtinb42", txtinb42.Text);
            cmd.Parameters.AddWithValue("@txtinb43", txtinb43.Text);
            cmd.Parameters.AddWithValue("@txtinb51", txtinb51.Text);
            cmd.Parameters.AddWithValue("@txtinb52", txtinb52.Text);
            cmd.Parameters.AddWithValue("@txtinb53", txtinb53.Text);
            cmd.Parameters.AddWithValue("@txtinb61", txtinb61.Text);
            cmd.Parameters.AddWithValue("@txtinb62", txtinb62.Text);
            cmd.Parameters.AddWithValue("@txtinb63", txtinb63.Text);
            cmd.Parameters.AddWithValue("@txtinb71", txtinb71.Text);
            cmd.Parameters.AddWithValue("@txtinb72", txtinb72.Text);
            cmd.Parameters.AddWithValue("@txtinb73", txtinb73.Text);

            cmd.Parameters.AddWithValue("@txtinb74", txtinb74.Text);
            cmd.Parameters.AddWithValue("@txtinb75", txtinb75.Text);
            cmd.Parameters.AddWithValue("@txtinb76", txtinb76.Text);
            cmd.Parameters.AddWithValue("@txtinb77", txtinb77.Text);
            cmd.Parameters.AddWithValue("@txtinb78", txtinb78.Text);
            cmd.Parameters.AddWithValue("@txtinb79", txtinb79.Text);



            cmd.Parameters.AddWithValue("@txtinbT11", txtinbT11.Text);
            cmd.Parameters.AddWithValue("@txtinbT12", txtinbT12.Text);
            cmd.Parameters.AddWithValue("@txtinbT13", txtinbT13.Text);
            cmd.Parameters.AddWithValue("@txtsd11", txtsd11.Text);
            cmd.Parameters.AddWithValue("@txtsd12", txtsd12.Text);
            cmd.Parameters.AddWithValue("@txtsd13", txtsd13.Text);
            cmd.Parameters.AddWithValue("@txtsd21", txtsd21.Text);
            cmd.Parameters.AddWithValue("@txtsd22", txtsd22.Text);
            cmd.Parameters.AddWithValue("@txtsd23", txtsd23.Text);
            cmd.Parameters.AddWithValue("@txtsd31", txtsd31.Text);
            cmd.Parameters.AddWithValue("@txtsd32", txtsd32.Text);
            cmd.Parameters.AddWithValue("@txtsd33", txtsd33.Text);
            cmd.Parameters.AddWithValue("@txtsd41", txtsd41.Text);
            cmd.Parameters.AddWithValue("@txtsd42", txtsd42.Text);
            cmd.Parameters.AddWithValue("@txtsd43", txtsd43.Text);
            cmd.Parameters.AddWithValue("@txtsd51", txtsd51.Text);
            cmd.Parameters.AddWithValue("@txtsd52", txtsd52.Text);
            cmd.Parameters.AddWithValue("@txtsd53", txtsd53.Text);
            cmd.Parameters.AddWithValue("@txtsd61", txtsd61.Text);
            cmd.Parameters.AddWithValue("@txtsd62", txtsd62.Text);
            cmd.Parameters.AddWithValue("@txtsd63", txtsd63.Text);
            cmd.Parameters.AddWithValue("@txtsd71", txtsd71.Text);
            cmd.Parameters.AddWithValue("@txtsd72", txtsd72.Text);
            cmd.Parameters.AddWithValue("@txtsd73", txtsd73.Text);

            cmd.Parameters.AddWithValue("@txtsd74", txtsd74.Text);
            cmd.Parameters.AddWithValue("@txtsd75", txtsd75.Text);
            cmd.Parameters.AddWithValue("@txtsd76", txtsd76.Text);
            cmd.Parameters.AddWithValue("@txtsd77", txtsd77.Text);
            cmd.Parameters.AddWithValue("@txtsd78", txtsd78.Text);
            cmd.Parameters.AddWithValue("@txtsd79", txtsd79.Text);




            cmd.Parameters.AddWithValue("@txtsdT11", txtsdT11.Text);
            cmd.Parameters.AddWithValue("@txtsdT12", txtsdT12.Text);
            cmd.Parameters.AddWithValue("@txtsdT13", txtsdT13.Text);
            cmd.Parameters.AddWithValue("@txtpcm11", txtpcm11.Text);
            cmd.Parameters.AddWithValue("@txtpcm12", txtpcm12.Text);
            cmd.Parameters.AddWithValue("@txtpcm13", txtpcm13.Text);
            cmd.Parameters.AddWithValue("@txtpcm21", txtpcm21.Text);
            cmd.Parameters.AddWithValue("@txtpcm22", txtpcm22.Text);
            cmd.Parameters.AddWithValue("@txtpcm23", txtpcm23.Text);
            cmd.Parameters.AddWithValue("@txtpcm31", txtpcm31.Text);
            cmd.Parameters.AddWithValue("@txtpcm32", txtpcm32.Text);
            cmd.Parameters.AddWithValue("@txtpcm33", txtpcm33.Text);
            cmd.Parameters.AddWithValue("@txtpcm41", txtpcm41.Text);
            cmd.Parameters.AddWithValue("@txtpcm42", txtpcm42.Text);
            cmd.Parameters.AddWithValue("@txtpcm43", txtpcm43.Text);
            cmd.Parameters.AddWithValue("@txtpcm51", txtpcm51.Text);
            cmd.Parameters.AddWithValue("@txtpcm52", txtpcm52.Text);
            cmd.Parameters.AddWithValue("@txtpcm53", txtpcm53.Text);
            cmd.Parameters.AddWithValue("@txtpcm61", txtpcm61.Text);
            cmd.Parameters.AddWithValue("@txtpcm62", txtpcm62.Text);
            cmd.Parameters.AddWithValue("@txtpcm63", txtpcm63.Text);
            cmd.Parameters.AddWithValue("@txtpcmT11", txtpcmT11.Text);
            cmd.Parameters.AddWithValue("@txtpcmT12", txtpcmT12.Text);
            cmd.Parameters.AddWithValue("@txtpcmT13", txtpcmT13.Text);
            cmd.Parameters.AddWithValue("@txtpiis11", txtpiis11.Text);
            cmd.Parameters.AddWithValue("@txtpiis12", txtpiis12.Text);
            cmd.Parameters.AddWithValue("@txtpiis13", txtpiis13.Text);
            cmd.Parameters.AddWithValue("@txtpiis21", txtpiis21.Text);
            cmd.Parameters.AddWithValue("@txtpiis22", txtpiis22.Text);
            cmd.Parameters.AddWithValue("@txtpiis23", txtpiis23.Text);
            cmd.Parameters.AddWithValue("@txtpiis31", txtpiis31.Text);
            cmd.Parameters.AddWithValue("@txtpiis32", txtpiis32.Text);
            cmd.Parameters.AddWithValue("@txtpiis33", txtpiis33.Text);
            cmd.Parameters.AddWithValue("@txtpiis41", txtpiis41.Text);
            cmd.Parameters.AddWithValue("@txtpiis42", txtpiis42.Text);
            cmd.Parameters.AddWithValue("@txtpiis43", txtpiis43.Text);
            cmd.Parameters.AddWithValue("@txtpiis51", txtpiis51.Text);
            cmd.Parameters.AddWithValue("@txtpiis52", txtpiis52.Text);
            cmd.Parameters.AddWithValue("@txtpiis53", txtpiis53.Text);

            cmd.Parameters.AddWithValue("@txtpiis54", txtpiis54.Text);
            cmd.Parameters.AddWithValue("@txtpiis55", txtpiis55.Text);
            cmd.Parameters.AddWithValue("@txtpiis56", txtpiis56.Text);
            cmd.Parameters.AddWithValue("@txtpiis57", txtpiis57.Text);
            cmd.Parameters.AddWithValue("@txtpiis58", txtpiis58.Text);
            cmd.Parameters.AddWithValue("@txtpiis59", txtpiis59.Text);



            cmd.Parameters.AddWithValue("@txtpiisT11", txtpiisT11.Text);
            cmd.Parameters.AddWithValue("@txtpiisT12", txtpiisT12.Text);
            cmd.Parameters.AddWithValue("@txtpiisT13", txtpiisT13.Text);
            cmd.Parameters.AddWithValue("@txtssc11", txtssc11.Text);
            cmd.Parameters.AddWithValue("@txtssc12", txtssc12.Text);
            cmd.Parameters.AddWithValue("@txtssc13", txtssc13.Text);
            cmd.Parameters.AddWithValue("@txtssc21", txtssc21.Text);
            cmd.Parameters.AddWithValue("@txtssc22", txtssc22.Text);
            cmd.Parameters.AddWithValue("@txtssc23", txtssc23.Text);
            cmd.Parameters.AddWithValue("@txtssc31", txtssc31.Text);
            cmd.Parameters.AddWithValue("@txtssc32", txtssc32.Text);
            cmd.Parameters.AddWithValue("@txtssc33", txtssc33.Text);

            cmd.Parameters.AddWithValue("@txtssc34", txtssc34.Text);
            cmd.Parameters.AddWithValue("@txtssc35", txtssc35.Text);
            cmd.Parameters.AddWithValue("@txtssc36", txtssc36.Text);
            cmd.Parameters.AddWithValue("@txtssc37", txtssc37.Text);
            cmd.Parameters.AddWithValue("@txtssc38", txtssc38.Text);
            cmd.Parameters.AddWithValue("@txtssc39", txtssc39.Text);



            cmd.Parameters.AddWithValue("@txtsscT1", txtsscT1.Text);
            cmd.Parameters.AddWithValue("@txtsscT2", txtsscT2.Text);
            cmd.Parameters.AddWithValue("@txtsscT3", txtsscT3.Text);
            cmd.Parameters.AddWithValue("@txtcsw11", txtcsw11.Text);
            cmd.Parameters.AddWithValue("@txtcsw12", txtcsw12.Text);
            cmd.Parameters.AddWithValue("@txtcsw13", txtcsw13.Text);
            cmd.Parameters.AddWithValue("@txtcsw21", txtcsw21.Text);
            cmd.Parameters.AddWithValue("@txtcsw22", txtcsw22.Text);
            cmd.Parameters.AddWithValue("@txtcsw23", txtcsw23.Text);
            cmd.Parameters.AddWithValue("@txtcsw31", txtcsw31.Text);
            cmd.Parameters.AddWithValue("@txtcsw32", txtcsw32.Text);
            cmd.Parameters.AddWithValue("@txtcsw33", txtcsw33.Text);
            cmd.Parameters.AddWithValue("@txtcsw41", txtcsw41.Text);
            cmd.Parameters.AddWithValue("@txtcsw42", txtcsw42.Text);
            cmd.Parameters.AddWithValue("@txtcsw43", txtcsw43.Text);
            cmd.Parameters.AddWithValue("@txtcsw51", txtcsw51.Text);
            cmd.Parameters.AddWithValue("@txtcsw52", txtcsw52.Text);
            cmd.Parameters.AddWithValue("@txtcsw53", txtcsw53.Text);
            cmd.Parameters.AddWithValue("@txtcswT11", txtcswT11.Text);
            cmd.Parameters.AddWithValue("@txtcswT12", txtcswT12.Text);
            cmd.Parameters.AddWithValue("@txtcswT13", txtcswT13.Text);
            cmd.Parameters.AddWithValue("@txtGGT11", txtGGT11.Text);
            cmd.Parameters.AddWithValue("@txtGGT12", txtGGT12.Text);
            cmd.Parameters.AddWithValue("@txtGGT13", txtGGT13.Text);



            cmd.Parameters.AddWithValue("@txtp2a", txtp2a.Text);
            cmd.Parameters.AddWithValue("@txtp2b", txtp2b.Text);
            cmd.Parameters.AddWithValue("@txtp2c", txtp2c.Text);
            cmd.Parameters.AddWithValue("@txtp2d", txtp2d.Text);
            cmd.Parameters.AddWithValue("@txtp2e", txtp2e.Text);
            cmd.Parameters.AddWithValue("@datep2", datep2.Text);
            cmd.Parameters.AddWithValue("@SignAssp2", SignAssp2.Text);
            cmd.Parameters.AddWithValue("@SignCos", SignCos.Text);
            cmd.Parameters.AddWithValue("@txtp3a", txtp3a.Text);
            cmd.Parameters.AddWithValue("@txtp3b", txtp3b.Text);
            cmd.Parameters.AddWithValue("@txtp3c", txtp3c.Text);
            cmd.Parameters.AddWithValue("@txtp3d", txtp3d.Text);
            cmd.Parameters.AddWithValue("@txtp3e", txtp3e.Text);
            cmd.Parameters.AddWithValue("@txtp3f", txtp3f.Text);
            cmd.Parameters.AddWithValue("@txtp3g", txtp3g.Text);
            cmd.Parameters.AddWithValue("@txtp3h", txtp3h.Text);
            cmd.Parameters.AddWithValue("@txtp3i", txtp3i.Text);
            cmd.Parameters.AddWithValue("@txtp3j", txtp3j.Text);
            cmd.Parameters.AddWithValue("@txtp41point", txtp41point.Text);
            cmd.Parameters.AddWithValue("@txtp43point", txtp43point.Text);
            cmd.Parameters.AddWithValue("@txtp51point", txtp51point.Text);
            cmd.Parameters.AddWithValue("@txtp53point", txtp53point.Text);
            cmd.Parameters.AddWithValue("@txt6coments", txt6coments.Text);
            cmd.Parameters.AddWithValue("@txt7tpoint", txt7tpoint.Text);
            cmd.Parameters.AddWithValue("@txt7flg", txt7flg.Text);
            cmd.Parameters.AddWithValue("@txtp7r1", txtp7r1.Text);
            cmd.Parameters.AddWithValue("@txtp7r2", txtp7r2.Text);
            cmd.Parameters.AddWithValue("@txtp7r3", txtp7r3.Text);
            if (Session["UserGroup"].ToString() == "HR")
            {
                cmd.Parameters.AddWithValue("@Status", "4");
            }
            else if (Session["UserGroup"].ToString() == "VC")
            {
                cmd.Parameters.AddWithValue("@Status", "5");
                cmd.Parameters.AddWithValue("@txtVC", txtVC.Text);

            }
            else if (Session["UserGroup"].ToString() == "HOD" || Session["UserGroup"].ToString() == "PRINCIPAL" || Session["UserGroup"].ToString() == "DEAN" || Session["uid"].ToString()=="TMU00215")
            {
                cmd.Parameters.AddWithValue("@Status", "3");
            }
            int i= cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('PMS  Form Submitted')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Try After Some Time')", true);
                return;
            }
        }
        catch
        {
        }
    }

    public void Save()
    {
        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("PMSAproval", con);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@Fid", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Hid", FF.Value);
            // cmd.Parameters.AddWithValue("@Pid", "");
            cmd.Parameters.AddWithValue("@AcademicYear", drpAcademic.SelectedValue);

            cmd.Parameters.AddWithValue("@txtct1", txtct1.Text);
            cmd.Parameters.AddWithValue("@txtct2", txtct2.Text);
            cmd.Parameters.AddWithValue("@txtct3", txtct3.Text);
            cmd.Parameters.AddWithValue("@txtsf11", txtsf11.Text);
            cmd.Parameters.AddWithValue("@txtsf12", txtsf12.Text);
            cmd.Parameters.AddWithValue("@txtsf13", txtsf13.Text);
            cmd.Parameters.AddWithValue("@txtsf21", txtsf21.Text);
            cmd.Parameters.AddWithValue("@txtsf22", txtsf22.Text);
            cmd.Parameters.AddWithValue("@txtsf23", txtsf23.Text);
            cmd.Parameters.AddWithValue("@txtsf31", txtsf31.Text);
            cmd.Parameters.AddWithValue("@txtsf32", txtsf32.Text);
            cmd.Parameters.AddWithValue("@txtsf33", txtsf33.Text);
            cmd.Parameters.AddWithValue("@txtsf41", txtsf41.Text);
            cmd.Parameters.AddWithValue("@txtsf42", txtsf42.Text);
            cmd.Parameters.AddWithValue("@txtsf43", txtsf43.Text);
            cmd.Parameters.AddWithValue("@txtsf51", txtsf51.Text);
            cmd.Parameters.AddWithValue("@txtsf52", txtsf52.Text);
            cmd.Parameters.AddWithValue("@txtsf53", txtsf53.Text);
            cmd.Parameters.AddWithValue("@txtsf61", txtsf61.Text);
            cmd.Parameters.AddWithValue("@txtsf62", txtsf62.Text);
            cmd.Parameters.AddWithValue("@txtsf63", txtsf63.Text);

            cmd.Parameters.AddWithValue("@txtsf64", txtsf64.Text);
            cmd.Parameters.AddWithValue("@txtsf65", txtsf65.Text);
            cmd.Parameters.AddWithValue("@txtsf66", txtsf66.Text);
            cmd.Parameters.AddWithValue("@txtsf67", txtsf67.Text);
            cmd.Parameters.AddWithValue("@txtsf68", txtsf68.Text);
            cmd.Parameters.AddWithValue("@txtsf69", txtsf69.Text);
            cmd.Parameters.AddWithValue("@txtsf70", txtsf70.Text);
            cmd.Parameters.AddWithValue("@txtsf71", txtsf71.Text);
            cmd.Parameters.AddWithValue("@txtsf72", txtsf72.Text);
            cmd.Parameters.AddWithValue("@txtsf73", txtsf73.Text);
            cmd.Parameters.AddWithValue("@txtsf74", txtsf74.Text);
            cmd.Parameters.AddWithValue("@txtsf75", txtsf75.Text);
            cmd.Parameters.AddWithValue("@txtsf76", txtsf76.Text);
            cmd.Parameters.AddWithValue("@txtsf77", txtsf77.Text);
            cmd.Parameters.AddWithValue("@txtsf78", txtsf78.Text);
            cmd.Parameters.AddWithValue("@txtsf79", txtsf79.Text);
            cmd.Parameters.AddWithValue("@txtsf80", txtsf80.Text);
            cmd.Parameters.AddWithValue("@txtsf81", txtsf81.Text);











            cmd.Parameters.AddWithValue("@txtip11", txtip11.Text);
            cmd.Parameters.AddWithValue("@txtip12", txtip12.Text);
            cmd.Parameters.AddWithValue("@txtip13", txtip13.Text);
            cmd.Parameters.AddWithValue("@txtip21", txtip21.Text);
            cmd.Parameters.AddWithValue("@txtip22", txtip22.Text);
            cmd.Parameters.AddWithValue("@txtip23", txtip23.Text);
            cmd.Parameters.AddWithValue("@txtip31", txtip31.Text);
            cmd.Parameters.AddWithValue("@txtip32", txtip32.Text);
            cmd.Parameters.AddWithValue("@txtip33", txtip33.Text);
            cmd.Parameters.AddWithValue("@txtNc11", txtNc11.Text);
            cmd.Parameters.AddWithValue("@txtNc12", txtNc12.Text);
            cmd.Parameters.AddWithValue("@txtNc13", txtNc13.Text);
            cmd.Parameters.AddWithValue("@txtNc21", txtNc21.Text);
            cmd.Parameters.AddWithValue("@txtNc22", txtNc22.Text);
            cmd.Parameters.AddWithValue("@txtNc23", txtNc23.Text);
            cmd.Parameters.AddWithValue("@txtNc31", txtNc31.Text);
            cmd.Parameters.AddWithValue("@txtNc32", txtNc32.Text);
            cmd.Parameters.AddWithValue("@txtNc33", txtNc33.Text);

            cmd.Parameters.AddWithValue("@txtNc34", txtNc34.Text);
            cmd.Parameters.AddWithValue("@txtNc35", txtNc35.Text);
            cmd.Parameters.AddWithValue("@txtNc36", txtNc36.Text);
            cmd.Parameters.AddWithValue("@txtNc37", txtNc37.Text);
            cmd.Parameters.AddWithValue("@txtNc38", txtNc38.Text);
            cmd.Parameters.AddWithValue("@txtNc39", txtNc39.Text);
            cmd.Parameters.AddWithValue("@txtNc40", txtNc40.Text);
            cmd.Parameters.AddWithValue("@txtNc41", txtNc41.Text);
            cmd.Parameters.AddWithValue("@txtNc42", txtNc42.Text);





            cmd.Parameters.AddWithValue("@txtcsm11", txtcsm11.Text);
            cmd.Parameters.AddWithValue("@txtcsm12", txtcsm12.Text);
            cmd.Parameters.AddWithValue("@txtcsm13", txtcsm13.Text);
            cmd.Parameters.AddWithValue("@txtcsm21", txtcsm21.Text);
            cmd.Parameters.AddWithValue("@txtcsm22", txtcsm22.Text);
            cmd.Parameters.AddWithValue("@txtcsm23", txtcsm23.Text);
            cmd.Parameters.AddWithValue("@txtpws11", txtpws11.Text);
            cmd.Parameters.AddWithValue("@txtpws12", txtpws12.Text);
            cmd.Parameters.AddWithValue("@txtpws13", txtpws13.Text);
            cmd.Parameters.AddWithValue("@txtpws21", txtpws21.Text);
            cmd.Parameters.AddWithValue("@txtpws22", txtpws22.Text);
            cmd.Parameters.AddWithValue("@txtpws23", txtpws23.Text);
            cmd.Parameters.AddWithValue("@txtpws31", txtpws31.Text);
            cmd.Parameters.AddWithValue("@txtpws32", txtpws32.Text);
            cmd.Parameters.AddWithValue("@txtpws33", txtpws33.Text);

            cmd.Parameters.AddWithValue("@txtpws34", txtpws34.Text);
            cmd.Parameters.AddWithValue("@txtpws35", txtpws35.Text);
            cmd.Parameters.AddWithValue("@txtpws36", txtpws36.Text);



            cmd.Parameters.AddWithValue("@txtpwA11", txtpwA11.Text);
            cmd.Parameters.AddWithValue("@txtpwA12", txtpwA12.Text);
            cmd.Parameters.AddWithValue("@txtpwA13", txtpwA13.Text);
            cmd.Parameters.AddWithValue("@txtpp11", txtpp11.Text);
            cmd.Parameters.AddWithValue("@txtpp12", txtpp12.Text);
            cmd.Parameters.AddWithValue("@txtpp13", txtpp13.Text);
            cmd.Parameters.AddWithValue("@txtpp21", txtpp21.Text);
            cmd.Parameters.AddWithValue("@txtpp22", txtpp22.Text);
            cmd.Parameters.AddWithValue("@txtpp23", txtpp23.Text);
            cmd.Parameters.AddWithValue("@txtpp31", txtpp31.Text);
            cmd.Parameters.AddWithValue("@txtpp32", txtpp32.Text);
            cmd.Parameters.AddWithValue("@txtpp33", txtpp33.Text);
            cmd.Parameters.AddWithValue("@txtpp41", txtpp41.Text);
            cmd.Parameters.AddWithValue("@txtpp42", txtpp42.Text);
            cmd.Parameters.AddWithValue("@txtpp43", txtpp43.Text);
            cmd.Parameters.AddWithValue("@txtpp51", txtpp51.Text);
            cmd.Parameters.AddWithValue("@txtpp52", txtpp52.Text);
            cmd.Parameters.AddWithValue("@txtpp53", txtpp53.Text);
            cmd.Parameters.AddWithValue("@txtpp61", txtpp61.Text);
            cmd.Parameters.AddWithValue("@txtpp62", txtpp62.Text);
            cmd.Parameters.AddWithValue("@txtpp63", txtpp63.Text);
            cmd.Parameters.AddWithValue("@txtrs11", txtrs11.Text);
            cmd.Parameters.AddWithValue("@txtrs12", txtrs12.Text);
            cmd.Parameters.AddWithValue("@txtrs13", txtrs13.Text);
            cmd.Parameters.AddWithValue("@txtrs21", txtrs21.Text);
            cmd.Parameters.AddWithValue("@txtrs22", txtrs22.Text);
            cmd.Parameters.AddWithValue("@txtrs23", txtrs23.Text);
            cmd.Parameters.AddWithValue("@txtrs31", txtrs31.Text);
            cmd.Parameters.AddWithValue("@txtrs32", txtrs32.Text);
            cmd.Parameters.AddWithValue("@txtrs33", txtrs33.Text);
            cmd.Parameters.AddWithValue("@txtrs41", txtrs41.Text);
            cmd.Parameters.AddWithValue("@txtrs42", txtrs42.Text);
            cmd.Parameters.AddWithValue("@txtrs43", txtrs43.Text);
            cmd.Parameters.AddWithValue("@txtrpp11", txtrpp11.Text);
            cmd.Parameters.AddWithValue("@txtrpp12", txtrpp12.Text);
            cmd.Parameters.AddWithValue("@txtrpp13", txtrpp13.Text);
            cmd.Parameters.AddWithValue("@txtrpp21", txtrpp21.Text);
            cmd.Parameters.AddWithValue("@txtrpp22", txtrpp22.Text);
            cmd.Parameters.AddWithValue("@txtrpp23", txtrpp23.Text);
            cmd.Parameters.AddWithValue("@txtrpp31", txtrpp31.Text);
            cmd.Parameters.AddWithValue("@txtrpp32", txtrpp32.Text);
            cmd.Parameters.AddWithValue("@txtrpp33", txtrpp33.Text);
            cmd.Parameters.AddWithValue("@txtrpro11", txtrpro11.Text);
            cmd.Parameters.AddWithValue("@txtrpro12", txtrpro12.Text);
            cmd.Parameters.AddWithValue("@txtrpro13", txtrpro13.Text);
            cmd.Parameters.AddWithValue("@txtrpro21", txtrpro21.Text);
            cmd.Parameters.AddWithValue("@txtrpro22", txtrpro22.Text);
            cmd.Parameters.AddWithValue("@txtrpro23", txtrpro23.Text);
            cmd.Parameters.AddWithValue("@txtrpro31", txtrpro31.Text);
            cmd.Parameters.AddWithValue("@txtrpro32", txtrpro32.Text);
            cmd.Parameters.AddWithValue("@txtrpro33", txtrpro33.Text);
            cmd.Parameters.AddWithValue("@txtrpro41", txtrpro41.Text);
            cmd.Parameters.AddWithValue("@txtrpro42", txtrpro42.Text);
            cmd.Parameters.AddWithValue("@txtrpro43", txtrpro43.Text);
            cmd.Parameters.AddWithValue("@txtrpro51", txtrpro51.Text);
            cmd.Parameters.AddWithValue("@txtrpro52", txtrpro52.Text);
            cmd.Parameters.AddWithValue("@txtrpro53", txtrpro53.Text);

            cmd.Parameters.AddWithValue("@txtrpro54", txtrpro54.Text);
            cmd.Parameters.AddWithValue("@txtrpro55", txtrpro55.Text);
            cmd.Parameters.AddWithValue("@txtrpro56", txtrpro56.Text);





            cmd.Parameters.AddWithValue("@txtrproT11", txtrproT11.Text);
            cmd.Parameters.AddWithValue("@txtrproT12", txtrproT12.Text);
            cmd.Parameters.AddWithValue("@txtrproT13", txtrproT13.Text);
            cmd.Parameters.AddWithValue("@txtinb11", txtinb11.Text);
            cmd.Parameters.AddWithValue("@txtinb12", txtinb12.Text);
            cmd.Parameters.AddWithValue("@txtinb13", txtinb13.Text);
            cmd.Parameters.AddWithValue("@txtinb21", txtinb21.Text);
            cmd.Parameters.AddWithValue("@txtinb22", txtinb22.Text);
            cmd.Parameters.AddWithValue("@txtinb23", txtinb23.Text);
            cmd.Parameters.AddWithValue("@txtinb31", txtinb31.Text);
            cmd.Parameters.AddWithValue("@txtinb32", txtinb32.Text);
            cmd.Parameters.AddWithValue("@txtinb33", txtinb33.Text);
            cmd.Parameters.AddWithValue("@txtinb41", txtinb41.Text);
            cmd.Parameters.AddWithValue("@txtinb42", txtinb42.Text);
            cmd.Parameters.AddWithValue("@txtinb43", txtinb43.Text);
            cmd.Parameters.AddWithValue("@txtinb51", txtinb51.Text);
            cmd.Parameters.AddWithValue("@txtinb52", txtinb52.Text);
            cmd.Parameters.AddWithValue("@txtinb53", txtinb53.Text);
            cmd.Parameters.AddWithValue("@txtinb61", txtinb61.Text);
            cmd.Parameters.AddWithValue("@txtinb62", txtinb62.Text);
            cmd.Parameters.AddWithValue("@txtinb63", txtinb63.Text);
            cmd.Parameters.AddWithValue("@txtinb71", txtinb71.Text);
            cmd.Parameters.AddWithValue("@txtinb72", txtinb72.Text);
            cmd.Parameters.AddWithValue("@txtinb73", txtinb73.Text);

            cmd.Parameters.AddWithValue("@txtinb74", txtinb74.Text);
            cmd.Parameters.AddWithValue("@txtinb75", txtinb75.Text);
            cmd.Parameters.AddWithValue("@txtinb76", txtinb76.Text);
            cmd.Parameters.AddWithValue("@txtinb77", txtinb77.Text);
            cmd.Parameters.AddWithValue("@txtinb78", txtinb78.Text);
            cmd.Parameters.AddWithValue("@txtinb79", txtinb79.Text);



            cmd.Parameters.AddWithValue("@txtinbT11", txtinbT11.Text);
            cmd.Parameters.AddWithValue("@txtinbT12", txtinbT12.Text);
            cmd.Parameters.AddWithValue("@txtinbT13", txtinbT13.Text);
            cmd.Parameters.AddWithValue("@txtsd11", txtsd11.Text);
            cmd.Parameters.AddWithValue("@txtsd12", txtsd12.Text);
            cmd.Parameters.AddWithValue("@txtsd13", txtsd13.Text);
            cmd.Parameters.AddWithValue("@txtsd21", txtsd21.Text);
            cmd.Parameters.AddWithValue("@txtsd22", txtsd22.Text);
            cmd.Parameters.AddWithValue("@txtsd23", txtsd23.Text);
            cmd.Parameters.AddWithValue("@txtsd31", txtsd31.Text);
            cmd.Parameters.AddWithValue("@txtsd32", txtsd32.Text);
            cmd.Parameters.AddWithValue("@txtsd33", txtsd33.Text);
            cmd.Parameters.AddWithValue("@txtsd41", txtsd41.Text);
            cmd.Parameters.AddWithValue("@txtsd42", txtsd42.Text);
            cmd.Parameters.AddWithValue("@txtsd43", txtsd43.Text);
            cmd.Parameters.AddWithValue("@txtsd51", txtsd51.Text);
            cmd.Parameters.AddWithValue("@txtsd52", txtsd52.Text);
            cmd.Parameters.AddWithValue("@txtsd53", txtsd53.Text);
            cmd.Parameters.AddWithValue("@txtsd61", txtsd61.Text);
            cmd.Parameters.AddWithValue("@txtsd62", txtsd62.Text);
            cmd.Parameters.AddWithValue("@txtsd63", txtsd63.Text);
            cmd.Parameters.AddWithValue("@txtsd71", txtsd71.Text);
            cmd.Parameters.AddWithValue("@txtsd72", txtsd72.Text);
            cmd.Parameters.AddWithValue("@txtsd73", txtsd73.Text);

            cmd.Parameters.AddWithValue("@txtsd74", txtsd74.Text);
            cmd.Parameters.AddWithValue("@txtsd75", txtsd75.Text);
            cmd.Parameters.AddWithValue("@txtsd76", txtsd76.Text);
            cmd.Parameters.AddWithValue("@txtsd77", txtsd77.Text);
            cmd.Parameters.AddWithValue("@txtsd78", txtsd78.Text);
            cmd.Parameters.AddWithValue("@txtsd79", txtsd79.Text);




            cmd.Parameters.AddWithValue("@txtsdT11", txtsdT11.Text);
            cmd.Parameters.AddWithValue("@txtsdT12", txtsdT12.Text);
            cmd.Parameters.AddWithValue("@txtsdT13", txtsdT13.Text);
            cmd.Parameters.AddWithValue("@txtpcm11", txtpcm11.Text);
            cmd.Parameters.AddWithValue("@txtpcm12", txtpcm12.Text);
            cmd.Parameters.AddWithValue("@txtpcm13", txtpcm13.Text);
            cmd.Parameters.AddWithValue("@txtpcm21", txtpcm21.Text);
            cmd.Parameters.AddWithValue("@txtpcm22", txtpcm22.Text);
            cmd.Parameters.AddWithValue("@txtpcm23", txtpcm23.Text);
            cmd.Parameters.AddWithValue("@txtpcm31", txtpcm31.Text);
            cmd.Parameters.AddWithValue("@txtpcm32", txtpcm32.Text);
            cmd.Parameters.AddWithValue("@txtpcm33", txtpcm33.Text);
            cmd.Parameters.AddWithValue("@txtpcm41", txtpcm41.Text);
            cmd.Parameters.AddWithValue("@txtpcm42", txtpcm42.Text);
            cmd.Parameters.AddWithValue("@txtpcm43", txtpcm43.Text);
            cmd.Parameters.AddWithValue("@txtpcm51", txtpcm51.Text);
            cmd.Parameters.AddWithValue("@txtpcm52", txtpcm52.Text);
            cmd.Parameters.AddWithValue("@txtpcm53", txtpcm53.Text);
            cmd.Parameters.AddWithValue("@txtpcm61", txtpcm61.Text);
            cmd.Parameters.AddWithValue("@txtpcm62", txtpcm62.Text);
            cmd.Parameters.AddWithValue("@txtpcm63", txtpcm63.Text);
            cmd.Parameters.AddWithValue("@txtpcmT11", txtpcmT11.Text);
            cmd.Parameters.AddWithValue("@txtpcmT12", txtpcmT12.Text);
            cmd.Parameters.AddWithValue("@txtpcmT13", txtpcmT13.Text);
            cmd.Parameters.AddWithValue("@txtpiis11", txtpiis11.Text);
            cmd.Parameters.AddWithValue("@txtpiis12", txtpiis12.Text);
            cmd.Parameters.AddWithValue("@txtpiis13", txtpiis13.Text);
            cmd.Parameters.AddWithValue("@txtpiis21", txtpiis21.Text);
            cmd.Parameters.AddWithValue("@txtpiis22", txtpiis22.Text);
            cmd.Parameters.AddWithValue("@txtpiis23", txtpiis23.Text);
            cmd.Parameters.AddWithValue("@txtpiis31", txtpiis31.Text);
            cmd.Parameters.AddWithValue("@txtpiis32", txtpiis32.Text);
            cmd.Parameters.AddWithValue("@txtpiis33", txtpiis33.Text);
            cmd.Parameters.AddWithValue("@txtpiis41", txtpiis41.Text);
            cmd.Parameters.AddWithValue("@txtpiis42", txtpiis42.Text);
            cmd.Parameters.AddWithValue("@txtpiis43", txtpiis43.Text);
            cmd.Parameters.AddWithValue("@txtpiis51", txtpiis51.Text);
            cmd.Parameters.AddWithValue("@txtpiis52", txtpiis52.Text);
            cmd.Parameters.AddWithValue("@txtpiis53", txtpiis53.Text);

            cmd.Parameters.AddWithValue("@txtpiis54", txtpiis54.Text);
            cmd.Parameters.AddWithValue("@txtpiis55", txtpiis55.Text);
            cmd.Parameters.AddWithValue("@txtpiis56", txtpiis56.Text);
            cmd.Parameters.AddWithValue("@txtpiis57", txtpiis57.Text);
            cmd.Parameters.AddWithValue("@txtpiis58", txtpiis58.Text);
            cmd.Parameters.AddWithValue("@txtpiis59", txtpiis59.Text);



            cmd.Parameters.AddWithValue("@txtpiisT11", txtpiisT11.Text);
            cmd.Parameters.AddWithValue("@txtpiisT12", txtpiisT12.Text);
            cmd.Parameters.AddWithValue("@txtpiisT13", txtpiisT13.Text);
            cmd.Parameters.AddWithValue("@txtssc11", txtssc11.Text);
            cmd.Parameters.AddWithValue("@txtssc12", txtssc12.Text);
            cmd.Parameters.AddWithValue("@txtssc13", txtssc13.Text);
            cmd.Parameters.AddWithValue("@txtssc21", txtssc21.Text);
            cmd.Parameters.AddWithValue("@txtssc22", txtssc22.Text);
            cmd.Parameters.AddWithValue("@txtssc23", txtssc23.Text);
            cmd.Parameters.AddWithValue("@txtssc31", txtssc31.Text);
            cmd.Parameters.AddWithValue("@txtssc32", txtssc32.Text);
            cmd.Parameters.AddWithValue("@txtssc33", txtssc33.Text);

            cmd.Parameters.AddWithValue("@txtssc34", txtssc34.Text);
            cmd.Parameters.AddWithValue("@txtssc35", txtssc35.Text);
            cmd.Parameters.AddWithValue("@txtssc36", txtssc36.Text);
            cmd.Parameters.AddWithValue("@txtssc37", txtssc37.Text);
            cmd.Parameters.AddWithValue("@txtssc38", txtssc38.Text);
            cmd.Parameters.AddWithValue("@txtssc39", txtssc39.Text);



            cmd.Parameters.AddWithValue("@txtsscT1", txtsscT1.Text);
            cmd.Parameters.AddWithValue("@txtsscT2", txtsscT2.Text);
            cmd.Parameters.AddWithValue("@txtsscT3", txtsscT3.Text);
            cmd.Parameters.AddWithValue("@txtcsw11", txtcsw11.Text);
            cmd.Parameters.AddWithValue("@txtcsw12", txtcsw12.Text);
            cmd.Parameters.AddWithValue("@txtcsw13", txtcsw13.Text);
            cmd.Parameters.AddWithValue("@txtcsw21", txtcsw21.Text);
            cmd.Parameters.AddWithValue("@txtcsw22", txtcsw22.Text);
            cmd.Parameters.AddWithValue("@txtcsw23", txtcsw23.Text);
            cmd.Parameters.AddWithValue("@txtcsw31", txtcsw31.Text);
            cmd.Parameters.AddWithValue("@txtcsw32", txtcsw32.Text);
            cmd.Parameters.AddWithValue("@txtcsw33", txtcsw33.Text);
            cmd.Parameters.AddWithValue("@txtcsw41", txtcsw41.Text);
            cmd.Parameters.AddWithValue("@txtcsw42", txtcsw42.Text);
            cmd.Parameters.AddWithValue("@txtcsw43", txtcsw43.Text);
            cmd.Parameters.AddWithValue("@txtcsw51", txtcsw51.Text);
            cmd.Parameters.AddWithValue("@txtcsw52", txtcsw52.Text);
            cmd.Parameters.AddWithValue("@txtcsw53", txtcsw53.Text);
            cmd.Parameters.AddWithValue("@txtcswT11", txtcswT11.Text);
            cmd.Parameters.AddWithValue("@txtcswT12", txtcswT12.Text);
            cmd.Parameters.AddWithValue("@txtcswT13", txtcswT13.Text);
            cmd.Parameters.AddWithValue("@txtGGT11", txtGGT11.Text);
            cmd.Parameters.AddWithValue("@txtGGT12", txtGGT12.Text);
            cmd.Parameters.AddWithValue("@txtGGT13", txtGGT13.Text);



            cmd.Parameters.AddWithValue("@txtp2a", txtp2a.Text);
            cmd.Parameters.AddWithValue("@txtp2b", txtp2b.Text);
            cmd.Parameters.AddWithValue("@txtp2c", txtp2c.Text);
            cmd.Parameters.AddWithValue("@txtp2d", txtp2d.Text);
            cmd.Parameters.AddWithValue("@txtp2e", txtp2e.Text);
            cmd.Parameters.AddWithValue("@datep2", datep2.Text);
            cmd.Parameters.AddWithValue("@SignAssp2", SignAssp2.Text);
            cmd.Parameters.AddWithValue("@SignCos", SignCos.Text);
            cmd.Parameters.AddWithValue("@txtp3a", txtp3a.Text);
            cmd.Parameters.AddWithValue("@txtp3b", txtp3b.Text);
            cmd.Parameters.AddWithValue("@txtp3c", txtp3c.Text);
            cmd.Parameters.AddWithValue("@txtp3d", txtp3d.Text);
            cmd.Parameters.AddWithValue("@txtp3e", txtp3e.Text);
            cmd.Parameters.AddWithValue("@txtp3f", txtp3f.Text);
            cmd.Parameters.AddWithValue("@txtp3g", txtp3g.Text);
            cmd.Parameters.AddWithValue("@txtp3h", txtp3h.Text);
            cmd.Parameters.AddWithValue("@txtp3i", txtp3i.Text);
            cmd.Parameters.AddWithValue("@txtp3j", txtp3j.Text);
            cmd.Parameters.AddWithValue("@txtp41point", txtp41point.Text);
            cmd.Parameters.AddWithValue("@txtp43point", txtp43point.Text);
            cmd.Parameters.AddWithValue("@txtp51point", txtp51point.Text);
            cmd.Parameters.AddWithValue("@txtp53point", txtp53point.Text);
            cmd.Parameters.AddWithValue("@txt6coments", txt6coments.Text);
            cmd.Parameters.AddWithValue("@txt7tpoint", txt7tpoint.Text);
            cmd.Parameters.AddWithValue("@txt7flg", txt7flg.Text);
            cmd.Parameters.AddWithValue("@txtp7r1", txtp7r1.Text);
            cmd.Parameters.AddWithValue("@txtp7r2", txtp7r2.Text);
            cmd.Parameters.AddWithValue("@txtp7r3", txtp7r3.Text);
            if (Session["UserGroup"].ToString() == "HR")
            {
                cmd.Parameters.AddWithValue("@Status", "4");
            }
            else if (Session["UserGroup"].ToString() == "VC")
            {
                cmd.Parameters.AddWithValue("@Status", "5");
                cmd.Parameters.AddWithValue("@txtVC", txtVC.Text);

            }
            else if (Session["UserGroup"].ToString() == "HOD" || Session["UserGroup"].ToString() == "PRINCIPAL" || Session["UserGroup"].ToString() == "DEAN" || Session["uid"].ToString()=="TMU00215")
            {
                cmd.Parameters.AddWithValue("@Status", "2");
            }
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('PMS  Form Saved')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Try After Some Time')", true);
                return;
            }
        }
        catch
        {
        }
    }
  
  protected void txtcsw53_TextChanged(object sender, EventArgs e)
    {


        if (Convert.ToInt32(txtcsw13.Text) + Convert.ToInt32(txtcsw23.Text) + Convert.ToInt32(txtcsw33.Text) + Convert.ToInt32(txtcsw43.Text) + Convert.ToInt32(txtcsw53.Text)  > 10)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Can not enter more than 10')", true);
            txtcsw13.Text = "";
            txtcsw23.Text = "";
            txtcsw33.Text = "";
            txtcsw43.Text = "";
            txtcsw53.Text = "";
            txtcsw13.BorderColor = System.Drawing.Color.Red;
            txtcsw23.BorderColor = System.Drawing.Color.Red;
            txtcsw33.BorderColor = System.Drawing.Color.Red;
            txtcsw43.BorderColor = System.Drawing.Color.Red;
            txtcsw53.BorderColor = System.Drawing.Color.Red;

          

            return;
        }

        if (txtcsw53.Text=="")
        {

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You could not fill blank')", true);
           
            return;
        }

        if (Convert.ToInt32(txtcsw53.Text) > 10)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Can not enter more than 10')", true);
            txtcsw53.Text = "";
            txtcsw53.Focus();
            txtcsw53.BorderColor=System.Drawing.Color.Red;
            return;
        }
         //GT3();
    }
 
    public void GT3()
    {


        Decimal A3;
        A3 = (Convert.ToDecimal(txtct3.Text = string.IsNullOrEmpty(txtct3.Text) ? "0" : txtct3.Text)
            //+ Convert.ToDecimal(txtsf13.Text = string.IsNullOrEmpty(txtsf13.Text) ? "0" : txtsf13.Text)
            //+ Convert.ToDecimal(txtsf23.Text = string.IsNullOrEmpty(txtsf23.Text) ? "0" : txtsf23.Text)
            //+ Convert.ToDecimal(txtsf33.Text = string.IsNullOrEmpty(txtsf33.Text) ? "0" : txtsf33.Text)
            //+ Convert.ToDecimal(txtsf43.Text = string.IsNullOrEmpty(txtsf43.Text) ? "0" : txtsf43.Text)
            //+ Convert.ToDecimal(txtsf53.Text = string.IsNullOrEmpty(txtsf53.Text) ? "0" : txtsf53.Text)
            //+ Convert.ToDecimal(txtsf63.Text = string.IsNullOrEmpty(txtsf63.Text) ? "0" : txtsf63.Text)
            //+ Convert.ToDecimal(txtsf66.Text = string.IsNullOrEmpty(txtsf66.Text) ? "0" : txtsf66.Text)
            //   + Convert.ToDecimal(txtsf69.Text = string.IsNullOrEmpty(txtsf69.Text) ? "0" : txtsf69.Text)
            //      + Convert.ToDecimal(txtsf72.Text = string.IsNullOrEmpty(txtsf72.Text) ? "0" : txtsf72.Text)
            //         + Convert.ToDecimal(txtsf75.Text = string.IsNullOrEmpty(txtsf75.Text) ? "0" : txtsf75.Text)
            //            + Convert.ToDecimal(txtsf78.Text = string.IsNullOrEmpty(txtsf78.Text) ? "0" : txtsf78.Text)
            //               + Convert.ToDecimal(txtsf81.Text = string.IsNullOrEmpty(txtsf81.Text) ? "0" : txtsf81.Text)

            + Convert.ToDecimal(txtip13.Text = string.IsNullOrEmpty(txtip13.Text) ? "0" : txtip13.Text)
            + Convert.ToDecimal(txtip23.Text = string.IsNullOrEmpty(txtip23.Text) ? "0" : txtip23.Text)
            + Convert.ToDecimal(txtip33.Text = string.IsNullOrEmpty(txtip33.Text) ? "0" : txtip33.Text)


            + Convert.ToDecimal(txtNc13.Text = string.IsNullOrEmpty(txtNc13.Text) ? "0" : txtNc13.Text)
            + Convert.ToDecimal(txtNc23.Text = string.IsNullOrEmpty(txtNc23.Text) ? "0" : txtNc23.Text)
            + Convert.ToDecimal(txtNc33.Text = string.IsNullOrEmpty(txtNc33.Text) ? "0" : txtNc33.Text)
            + Convert.ToDecimal(txtNc36.Text = string.IsNullOrEmpty(txtNc36.Text) ? "0" : txtNc36.Text)
            + Convert.ToDecimal(txtNc39.Text = string.IsNullOrEmpty(txtNc39.Text) ? "0" : txtNc39.Text)
            + Convert.ToDecimal(txtNc42.Text = string.IsNullOrEmpty(txtNc42.Text) ? "0" : txtNc42.Text)


            + Convert.ToDecimal(txtcsm13.Text = string.IsNullOrEmpty(txtcsm13.Text) ? "0" : txtcsm13.Text)
            + Convert.ToDecimal(txtcsm23.Text = string.IsNullOrEmpty(txtcsm23.Text) ? "0" : txtcsm23.Text)


            + Convert.ToDecimal(txtpws13.Text = string.IsNullOrEmpty(txtpws13.Text) ? "0" : txtpws13.Text)
            + Convert.ToDecimal(txtpws23.Text = string.IsNullOrEmpty(txtpws23.Text) ? "0" : txtpws23.Text)
            + Convert.ToDecimal(txtpws33.Text = string.IsNullOrEmpty(txtpws33.Text) ? "0" : txtpws33.Text)
            + Convert.ToDecimal(txtpws36.Text = string.IsNullOrEmpty(txtpws36.Text) ? "0" : txtpws36.Text)
            );
        txtpwA13.Text = Convert.ToString(A3);


        Decimal B3;

        B3 = (Convert.ToDecimal(txtpp13.Text = string.IsNullOrEmpty(txtpp13.Text) ? "0" : txtpp13.Text)
        + Convert.ToDecimal(txtpp23.Text = string.IsNullOrEmpty(txtpp23.Text) ? "0" : txtpp23.Text)
        + Convert.ToDecimal(txtpp33.Text = string.IsNullOrEmpty(txtpp33.Text) ? "0" : txtpp33.Text)
        + Convert.ToDecimal(txtpp43.Text = string.IsNullOrEmpty(txtpp43.Text) ? "0" : txtpp43.Text)
        + Convert.ToDecimal(txtpp53.Text = string.IsNullOrEmpty(txtpp53.Text) ? "0" : txtpp53.Text)
        + Convert.ToDecimal(txtpp63.Text = string.IsNullOrEmpty(txtpp63.Text) ? "0" : txtpp63.Text)

        + Convert.ToDecimal(txtrs13.Text = string.IsNullOrEmpty(txtrs13.Text) ? "0" : txtrs13.Text)
        + Convert.ToDecimal(txtrs23.Text = string.IsNullOrEmpty(txtrs23.Text) ? "0" : txtrs23.Text)
        + Convert.ToDecimal(txtrs33.Text = string.IsNullOrEmpty(txtrs33.Text) ? "0" : txtrs33.Text)
        + Convert.ToDecimal(txtrs43.Text = string.IsNullOrEmpty(txtrs43.Text) ? "0" : txtrs43.Text)



        + Convert.ToDecimal(txtrpp13.Text = string.IsNullOrEmpty(txtrpp13.Text) ? "0" : txtrpp13.Text)
       + Convert.ToDecimal(txtrpp23.Text = string.IsNullOrEmpty(txtrpp23.Text) ? "0" : txtrpp23.Text)
        + Convert.ToDecimal(txtrpp33.Text = string.IsNullOrEmpty(txtrpp33.Text) ? "0" : txtrpp33.Text)


           + Convert.ToDecimal(txtrpro13.Text = string.IsNullOrEmpty(txtrpro13.Text) ? "0" : txtrpro13.Text)
            + Convert.ToDecimal(txtrpro23.Text = string.IsNullOrEmpty(txtrpro23.Text) ? "0" : txtrpro23.Text)
             + Convert.ToDecimal(txtrpro33.Text = string.IsNullOrEmpty(txtrpro33.Text) ? "0" : txtrpro33.Text)
              + Convert.ToDecimal(txtrpro43.Text = string.IsNullOrEmpty(txtrpro43.Text) ? "0" : txtrpro43.Text)
               + Convert.ToDecimal(txtrpro53.Text = string.IsNullOrEmpty(txtrpro53.Text) ? "0" : txtrpro53.Text)
                + Convert.ToDecimal(txtrpro56.Text = string.IsNullOrEmpty(txtrpro56.Text) ? "0" : txtrpro56.Text)
               
               );


        txtrproT13.Text = Convert.ToString(B3);

        decimal C3;
        C3 = (Convert.ToDecimal(txtinb13.Text = string.IsNullOrEmpty(txtinb13.Text) ? "0" : txtinb13.Text)
            + Convert.ToDecimal(txtinb23.Text = string.IsNullOrEmpty(txtinb23.Text) ? "0" : txtinb23.Text)
            + Convert.ToDecimal(txtinb33.Text = string.IsNullOrEmpty(txtinb33.Text) ? "0" : txtinb33.Text)
            + Convert.ToDecimal(txtinb43.Text = string.IsNullOrEmpty(txtinb43.Text) ? "0" : txtinb43.Text)
             + Convert.ToDecimal(txtinb53.Text = string.IsNullOrEmpty(txtinb53.Text) ? "0" : txtinb53.Text)
              + Convert.ToDecimal(txtinb63.Text = string.IsNullOrEmpty(txtinb63.Text) ? "0" : txtinb63.Text)
               + Convert.ToDecimal(txtinb73.Text = string.IsNullOrEmpty(txtinb73.Text) ? "0" : txtinb73.Text)
               + Convert.ToDecimal(txtinb76.Text = string.IsNullOrEmpty(txtinb76.Text) ? "0" : txtinb76.Text)
               + Convert.ToDecimal(txtinb79.Text = string.IsNullOrEmpty(txtinb79.Text) ? "0" : txtinb79.Text)
               );
        txtinbT13.Text = Convert.ToString(C3);

        decimal D3;
        D3 = (Convert.ToDecimal(txtsd13.Text = string.IsNullOrEmpty(txtsd13.Text) ? "0" : txtsd13.Text)
           + Convert.ToDecimal(txtsd23.Text = string.IsNullOrEmpty(txtsd23.Text) ? "0" : txtsd23.Text)
           + Convert.ToDecimal(txtsd33.Text = string.IsNullOrEmpty(txtsd33.Text) ? "0" : txtsd33.Text)
            + Convert.ToDecimal(txtsd43.Text = string.IsNullOrEmpty(txtsd43.Text) ? "0" : txtsd43.Text)
             + Convert.ToDecimal(txtsd53.Text = string.IsNullOrEmpty(txtsd53.Text) ? "0" : txtsd53.Text)
              + Convert.ToDecimal(txtsd63.Text = string.IsNullOrEmpty(txtsd63.Text) ? "0" : txtsd63.Text)
               + Convert.ToDecimal(txtsd73.Text = string.IsNullOrEmpty(txtsd73.Text) ? "0" : txtsd73.Text)
               + Convert.ToDecimal(txtsd76.Text = string.IsNullOrEmpty(txtsd76.Text) ? "0" : txtsd76.Text)
               + Convert.ToDecimal(txtsd79.Text = string.IsNullOrEmpty(txtsd79.Text) ? "0" : txtsd79.Text)
               );
        txtsdT13.Text = Convert.ToString(D3);

      


        decimal E3;
        E3 = (Convert.ToDecimal(txtpcm13.Text = string.IsNullOrEmpty(txtpcm13.Text) ? "0" : txtpcm13.Text)
             + Convert.ToDecimal(txtpcm23.Text = string.IsNullOrEmpty(txtpcm23.Text) ? "0" : txtpcm23.Text)
             + Convert.ToDecimal(txtpcm33.Text = string.IsNullOrEmpty(txtpcm33.Text) ? "0" : txtpcm33.Text)
              + Convert.ToDecimal(txtpcm43.Text = string.IsNullOrEmpty(txtpcm43.Text) ? "0" : txtpcm43.Text)
               + Convert.ToDecimal(txtpcm53.Text = string.IsNullOrEmpty(txtpcm53.Text) ? "0" : txtpcm53.Text)
                + Convert.ToDecimal(txtpcm63.Text = string.IsNullOrEmpty(txtpcm63.Text) ? "0" : txtpcm63.Text)
                
                );

        txtpcmT13.Text = Convert.ToString(E3);

        decimal F3;
        F3 = (Convert.ToDecimal(txtpiis13.Text = string.IsNullOrEmpty(txtpiis13.Text) ? "0" : txtpiis13.Text)
             + Convert.ToDecimal(txtpiis23.Text = string.IsNullOrEmpty(txtpiis23.Text) ? "0" : txtpiis23.Text)
             + Convert.ToDecimal(txtpiis33.Text = string.IsNullOrEmpty(txtpiis33.Text) ? "0" : txtpiis33.Text)
              + Convert.ToDecimal(txtpiis43.Text = string.IsNullOrEmpty(txtpiis43.Text) ? "0" : txtpiis43.Text)
               + Convert.ToDecimal(txtpiis53.Text = string.IsNullOrEmpty(txtpiis53.Text) ? "0" : txtpiis53.Text)
                + Convert.ToDecimal(txtpiis56.Text = string.IsNullOrEmpty(txtpiis56.Text) ? "0" : txtpiis56.Text)
                 + Convert.ToDecimal(txtpiis59.Text = string.IsNullOrEmpty(txtpiis59.Text) ? "0" : txtpiis59.Text)
               );

        txtpiisT13.Text = Convert.ToString(F3);
        decimal G3;
        G3 = (
         Convert.ToDecimal(txtssc13.Text = string.IsNullOrEmpty(txtssc13.Text) ? "0" : txtssc13.Text)
             + Convert.ToDecimal(txtssc23.Text = string.IsNullOrEmpty(txtssc23.Text) ? "0" : txtssc23.Text)
             + Convert.ToDecimal(txtssc33.Text = string.IsNullOrEmpty(txtssc33.Text) ? "0" : txtssc33.Text)
              + Convert.ToDecimal(txtssc36.Text = string.IsNullOrEmpty(txtssc36.Text) ? "0" : txtssc36.Text)
               + Convert.ToDecimal(txtssc39.Text = string.IsNullOrEmpty(txtssc39.Text) ? "0" : txtssc39.Text)
             );

        txtsscT3.Text = Convert.ToString(G3);


        decimal H3;
        H3 = (Convert.ToDecimal(txtcsw13.Text = string.IsNullOrEmpty(txtcsw13.Text) ? "0" : txtcsw13.Text)
           + Convert.ToDecimal(txtcsw23.Text = string.IsNullOrEmpty(txtcsw23.Text) ? "0" : txtcsw23.Text)
           + Convert.ToDecimal(txtcsw33.Text = string.IsNullOrEmpty(txtcsw33.Text) ? "0" : txtcsw33.Text)
            + Convert.ToDecimal(txtcsw43.Text = string.IsNullOrEmpty(txtcsw43.Text) ? "0" : txtcsw43.Text)
             + Convert.ToDecimal(txtcsw53.Text = string.IsNullOrEmpty(txtcsw53.Text) ? "0" : txtcsw53.Text));
        txtcswT13.Text = Convert.ToString(H3);

        decimal GT3;
        GT3 = (Convert.ToDecimal(txtpwA13.Text)
         + Convert.ToDecimal(txtrproT13.Text)
         + Convert.ToDecimal(txtinbT13.Text)
         + Convert.ToDecimal(txtsdT13.Text)
         + Convert.ToDecimal(txtpcmT13.Text)
         + Convert.ToDecimal(txtpiisT13.Text)
         + Convert.ToDecimal(txtsscT3.Text)
         + Convert.ToDecimal(txtcswT13.Text));
        txtGGT13.Text = Convert.ToString(GT3);
    }

    public void part7()
    {
        
        Decimal A3;
        A3 = (Convert.ToDecimal(txtp3a.Text = string.IsNullOrEmpty(txtp3a.Text) ? "0" : txtp3a.Text)
            + Convert.ToDecimal(txtp3b.Text = string.IsNullOrEmpty(txtp3b.Text) ? "0" : txtp3b.Text)
            + Convert.ToDecimal(txtp3c.Text = string.IsNullOrEmpty(txtp3c.Text) ? "0" : txtp3c.Text)
            + Convert.ToDecimal(txtp3d.Text = string.IsNullOrEmpty(txtp3d.Text) ? "0" : txtp3d.Text)
            + Convert.ToDecimal(txtp3e.Text = string.IsNullOrEmpty(txtp3e.Text) ? "0" : txtp3e.Text)
            + Convert.ToDecimal(txtp3f.Text = string.IsNullOrEmpty(txtp3f.Text) ? "0" : txtp3f.Text)
            + Convert.ToDecimal(txtp3g.Text = string.IsNullOrEmpty(txtp3g.Text) ? "0" : txtp3g.Text)
            + Convert.ToDecimal(txtp3h.Text = string.IsNullOrEmpty(txtp3h.Text) ? "0" : txtp3h.Text)
            + Convert.ToDecimal(txtp3i.Text = string.IsNullOrEmpty(txtp3i.Text) ? "0" : txtp3i.Text)
            + Convert.ToDecimal(txtp3j.Text = string.IsNullOrEmpty(txtp3j.Text) ? "0" : txtp3j.Text)
            + Convert.ToDecimal(txtGGT13.Text = string.IsNullOrEmpty(txtGGT13.Text) ? "0" : txtGGT13.Text));
        txt7tpoint.Text = Convert.ToString(A3); 
       
    }

    protected void btnApproved_Click(object sender, EventArgs e)
    {

        if (txtct3.Text == "" ||


            //|| txtsf13.Text == "" ||
       
        //txtsf23.Text == "" ||
       
        //txtsf33.Text == "" ||
       
        //txtsf43.Text == "" ||
      
        //txtsf53.Text == "" ||

        
        //txtsf63.Text == "" ||
        //txtsf66.Text == "" ||
        //txtsf69.Text == "" ||
        //txtsf72.Text == "" ||
        //txtsf75.Text == "" ||
        //txtsf78.Text == "" ||
        //txtsf81.Text == "" ||

     
        txtip13.Text == "" ||
       
        txtip23.Text == "" ||
      
        txtip33.Text == "" ||


        txtNc13.Text == "" ||
   
        txtNc23.Text == "" ||
     
        txtNc33.Text == "" ||
        txtNc36.Text == "" ||
        txtNc39.Text == "" ||
        txtNc42.Text == "" ||

      
        txtcsm13.Text == "" ||
       
        txtcsm23.Text == "" ||
    
        txtpws13.Text == "" ||
      
        txtpws23.Text == "" ||
    
        txtpws33.Text == "" ||
        txtpws36.Text == "" ||
       
      
      
        txtpp13.Text == "" ||
       
        txtpp23.Text == "" ||
       
        txtpp33.Text == "" ||
       
        txtpp43.Text == "" ||
       
        txtpp53.Text == "" ||
      
        txtpp63.Text == "" ||

        
        txtrs13.Text == "" ||
      
        txtrs23.Text == "" ||
     
        txtrs33.Text == "" ||
      
        txtrs43.Text == "" ||
      
        txtrpp13.Text == "" ||
       
        txtrpp23.Text == "" ||
    
        txtrpp33.Text == "" ||
       
        txtrpro13.Text == "" ||
      
        txtrpro23.Text == "" ||
     
        txtrpro33.Text == "" ||
      
        txtrpro43.Text == "" ||
     
        txtrpro53.Text == "" ||
        txtrpro56.Text == "" ||
       
      
      
        txtinb13.Text == "" ||
       
        txtinb23.Text == "" ||
       
        txtinb33.Text == "" ||
      
        txtinb43.Text == "" ||
       
        txtinb53.Text == "" ||
      
        txtinb63.Text == "" ||
       
        txtinb73.Text == "" ||
        txtinb76.Text == "" ||
        txtinb79.Text == "" ||

      
       
        txtsd13.Text == "" ||
       
        txtsd23.Text == "" ||
      
        txtsd33.Text == "" ||
     
        txtsd43.Text == "" ||
       
        txtsd53.Text == "" ||
      
        txtsd63.Text == "" ||
       
        txtsd73.Text == "" ||
        txtsd76.Text == "" ||
        txtsd79.Text == "" ||
       
       
        txtpcm13.Text == "" ||
      
        txtpcm23.Text == "" ||
     
        txtpcm33.Text == "" ||
      
        txtpcm43.Text == "" ||
     
        txtpcm53.Text == "" ||
      
        txtpcm63.Text == "" ||
       
     
       
        txtpiis13.Text == "" ||
       
        txtpiis23.Text == "" ||
       
        txtpiis33.Text == "" ||
      
        txtpiis43.Text == "" ||
       
        txtpiis53.Text == "" ||
        txtpiis56.Text == "" ||
        txtpiis59.Text == "" ||
       
      
        txtssc13.Text == "" ||
       
        txtssc23.Text == "" ||
       
        txtssc33.Text == "" ||
        txtssc36.Text == "" ||
        txtssc39.Text == "" ||
      
       
        txtcsw13.Text == "" ||
       
        txtcsw23.Text == "" ||
      
        txtcsw33.Text == "" ||
        
        txtcsw43.Text == "" ||
        
        txtcsw53.Text == "" 
       
        
        //txtGGT13.Text == "" ||
        //txtp2a.Text == "" ||
        //txtp2b.Text == "" ||
        //txtp2c.Text == "" ||
        //txtp2d.Text == "" ||
        //txtp2e.Text == "" ||
        //datep2.Text == "" ||
        //SignAssp2.Text == "" ||
        //SignCos.Text == "" ||
        //txtp3a.Text == "" ||
        //txtp3b.Text == "" ||
        //txtp3c.Text == "" ||
        //txtp3d.Text == "" ||
        //txtp3e.Text == "" ||
        //txtp3f.Text == "" ||
        //txtp3g.Text == "" ||
        //txtp3h.Text == "" ||
        //txtp3i.Text == "" ||
        //txtp3j.Text == "" ||
        //txtp41point.Text == "" ||
        //txtp43point.Text == "" ||
        //txtp51point.Text == "" ||
        //txtp53point.Text == "" ||
        //txt6coments.Text == "" ||
        //txt7tpoint.Text == "" ||
        //txt7flg.Text == "" ||
        //txtp7r1.Text == "" ||
        //txtp7r2.Text == "" ||
        //txtp7r3.Text == "" 

        )
        {
             ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You could not fill blank')", true);
        }

        else
        {
            GT3();




        part7();
        submit();
        facultylist();
        DivForm.Visible = false;
        btnback.Visible = false;
        GrdExamList.Visible = true;
        }
    }
    
   
    protected void btnback_Click(object sender, EventArgs e)
    {
        DivForm.Visible = false;
        btnback.Visible = false;
        GrdExamList.Visible = true;
        grdDocument.Visible = false;
    }
   
    protected void lblview_Click(object sender, EventArgs e)
    {

        FF.Value = "";     
       
        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        int i = Convert.ToInt32(row.RowIndex);
        HiddenField HfStaffcode = (HiddenField)row.FindControl("HfStudentNo");
        HiddenField HfSName = (HiddenField)row.FindControl("hfFName");
        SqlCommand cmd = new SqlCommand("proc_Gettbl_PMS", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Fid", HfStaffcode.Value);
        cmd.Parameters.AddWithValue("@AcadmicYear", drpAcademic.SelectedValue);
        FF.Value = HfStaffcode.Value;
        Label1.Text = drpAcademic.SelectedValue;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Open();
        da.Fill(dt);
        con.Close();

        if (dt.Rows.Count > 0)
        {

            lblEmpName.Text ="Employee Code" +": "+HfSName.Value+"(" +HfStaffcode.Value+")";
            txtct1.Text = dt.Rows[0]["txtct1"].ToString();
            txtct2.Text = dt.Rows[0]["txtct2"].ToString();
            txtct3.Text = dt.Rows[0]["txtct3"].ToString();
            txtsf11.Text = dt.Rows[0]["txtsf11"].ToString();
            txtsf12.Text = dt.Rows[0]["txtsf12"].ToString();
            txtsf13.Text = dt.Rows[0]["txtsf13"].ToString();
            txtsf21.Text = dt.Rows[0]["txtsf21"].ToString();
            txtsf22.Text = dt.Rows[0]["txtsf22"].ToString();
            txtsf23.Text = dt.Rows[0]["txtsf23"].ToString();
            txtsf31.Text = dt.Rows[0]["txtsf31"].ToString();
            txtsf32.Text = dt.Rows[0]["txtsf32"].ToString();
            txtsf33.Text = dt.Rows[0]["txtsf33"].ToString();
            txtsf41.Text = dt.Rows[0]["txtsf41"].ToString();
            txtsf42.Text = dt.Rows[0]["txtsf42"].ToString();
            txtsf43.Text = dt.Rows[0]["txtsf43"].ToString();
            txtsf51.Text = dt.Rows[0]["txtsf51"].ToString();
            txtsf52.Text = dt.Rows[0]["txtsf52"].ToString();
            txtsf53.Text = dt.Rows[0]["txtsf53"].ToString();
            txtsf61.Text = dt.Rows[0]["txtsf61"].ToString();
            txtsf62.Text = dt.Rows[0]["txtsf62"].ToString();
            txtsf63.Text = dt.Rows[0]["txtsf63"].ToString();
           
            txtsf64.Text = dt.Rows[0]["txtsf64"].ToString();
            txtsf65.Text = dt.Rows[0]["txtsf65"].ToString();
            txtsf66.Text = dt.Rows[0]["txtsf66"].ToString();
            txtsf67.Text = dt.Rows[0]["txtsf67"].ToString();
            txtsf68.Text = dt.Rows[0]["txtsf68"].ToString();
            txtsf69.Text = dt.Rows[0]["txtsf69"].ToString();
            txtsf70.Text = dt.Rows[0]["txtsf70"].ToString();
            txtsf71.Text = dt.Rows[0]["txtsf71"].ToString();
            txtsf72.Text = dt.Rows[0]["txtsf72"].ToString();
            txtsf73.Text = dt.Rows[0]["txtsf73"].ToString();
            txtsf74.Text = dt.Rows[0]["txtsf74"].ToString();
            txtsf75.Text = dt.Rows[0]["txtsf75"].ToString();
            txtsf76.Text = dt.Rows[0]["txtsf76"].ToString();
            txtsf77.Text = dt.Rows[0]["txtsf77"].ToString();
            txtsf78.Text = dt.Rows[0]["txtsf78"].ToString();
            txtsf79.Text = dt.Rows[0]["txtsf79"].ToString();
            txtsf80.Text = dt.Rows[0]["txtsf80"].ToString();
            txtsf81.Text = dt.Rows[0]["txtsf81"].ToString();













            txtip11.Text = dt.Rows[0]["txtip11"].ToString();
            txtip12.Text = dt.Rows[0]["txtip12"].ToString();
            txtip13.Text = dt.Rows[0]["txtip13"].ToString();
            txtip21.Text = dt.Rows[0]["txtip21"].ToString();
            txtip22.Text = dt.Rows[0]["txtip22"].ToString();
            txtip23.Text = dt.Rows[0]["txtip23"].ToString();
            txtip31.Text = dt.Rows[0]["txtip31"].ToString();
            txtip32.Text = dt.Rows[0]["txtip32"].ToString();
            txtip33.Text = dt.Rows[0]["txtip33"].ToString();
            txtNc11.Text = dt.Rows[0]["txtNc11"].ToString();
            txtNc12.Text = dt.Rows[0]["txtNc12"].ToString();
            txtNc13.Text = dt.Rows[0]["txtNc13"].ToString();
            txtNc21.Text = dt.Rows[0]["txtNc21"].ToString();
            txtNc22.Text = dt.Rows[0]["txtNc22"].ToString();
            txtNc23.Text = dt.Rows[0]["txtNc23"].ToString();
            txtNc31.Text = dt.Rows[0]["txtNc31"].ToString();
            txtNc32.Text = dt.Rows[0]["txtNc32"].ToString();
            txtNc33.Text = dt.Rows[0]["txtNc33"].ToString();

            txtNc34.Text = dt.Rows[0]["txtNc34"].ToString();
            txtNc35.Text = dt.Rows[0]["txtNc35"].ToString();
            txtNc36.Text = dt.Rows[0]["txtNc36"].ToString();
            txtNc37.Text = dt.Rows[0]["txtNc37"].ToString();
            txtNc38.Text = dt.Rows[0]["txtNc38"].ToString();
            txtNc39.Text = dt.Rows[0]["txtNc39"].ToString();
            txtNc40.Text = dt.Rows[0]["txtNc40"].ToString();
            txtNc41.Text = dt.Rows[0]["txtNc41"].ToString();
            txtNc42.Text = dt.Rows[0]["txtNc42"].ToString();









            txtcsm11.Text = dt.Rows[0]["txtcsm11"].ToString();
            txtcsm12.Text = dt.Rows[0]["txtcsm12"].ToString();
            txtcsm13.Text = dt.Rows[0]["txtcsm13"].ToString();
            txtcsm21.Text = dt.Rows[0]["txtcsm21"].ToString();
            txtcsm22.Text = dt.Rows[0]["txtcsm22"].ToString();
            txtcsm23.Text = dt.Rows[0]["txtcsm23"].ToString();
            txtpws11.Text = dt.Rows[0]["txtpws11"].ToString();
            txtpws12.Text = dt.Rows[0]["txtpws12"].ToString();
            txtpws13.Text = dt.Rows[0]["txtpws13"].ToString();
            txtpws21.Text = dt.Rows[0]["txtpws21"].ToString();
            txtpws22.Text = dt.Rows[0]["txtpws22"].ToString();
            txtpws23.Text = dt.Rows[0]["txtpws23"].ToString();
            txtpws31.Text = dt.Rows[0]["txtpws31"].ToString();
            txtpws32.Text = dt.Rows[0]["txtpws32"].ToString();
            txtpws33.Text = dt.Rows[0]["txtpws33"].ToString();

            txtpws34.Text = dt.Rows[0]["txtpws34"].ToString();
            txtpws35.Text = dt.Rows[0]["txtpws35"].ToString();
            txtpws36.Text = dt.Rows[0]["txtpws36"].ToString();



            txtpwA11.Text = dt.Rows[0]["txtpwA11"].ToString();
            txtpwA12.Text = dt.Rows[0]["txtpwA12"].ToString();
            txtpwA13.Text = dt.Rows[0]["txtpwA13"].ToString();
            txtpp11.Text = dt.Rows[0]["txtpp11"].ToString();
            txtpp12.Text = dt.Rows[0]["txtpp12"].ToString();
            txtpp13.Text = dt.Rows[0]["txtpp13"].ToString();
            txtpp21.Text = dt.Rows[0]["txtpp21"].ToString();
            txtpp22.Text = dt.Rows[0]["txtpp22"].ToString();
            txtpp23.Text = dt.Rows[0]["txtpp23"].ToString();
            txtpp31.Text = dt.Rows[0]["txtpp31"].ToString();
            txtpp32.Text = dt.Rows[0]["txtpp32"].ToString();
            txtpp33.Text = dt.Rows[0]["txtpp33"].ToString();
            txtpp41.Text = dt.Rows[0]["txtpp41"].ToString();
            txtpp42.Text = dt.Rows[0]["txtpp42"].ToString();
            txtpp43.Text = dt.Rows[0]["txtpp43"].ToString();
            txtpp51.Text = dt.Rows[0]["txtpp51"].ToString();
            txtpp52.Text = dt.Rows[0]["txtpp52"].ToString();
            txtpp53.Text = dt.Rows[0]["txtpp53"].ToString();
            txtpp61.Text = dt.Rows[0]["txtpp61"].ToString();
            txtpp62.Text = dt.Rows[0]["txtpp62"].ToString();
            txtpp63.Text = dt.Rows[0]["txtpp63"].ToString();
            txtrs11.Text = dt.Rows[0]["txtrs11"].ToString();
            txtrs12.Text = dt.Rows[0]["txtrs12"].ToString();
            txtrs13.Text = dt.Rows[0]["txtrs13"].ToString();
            txtrs21.Text = dt.Rows[0]["txtrs21"].ToString();
            txtrs22.Text = dt.Rows[0]["txtrs22"].ToString();
            txtrs23.Text = dt.Rows[0]["txtrs23"].ToString();
            txtrs31.Text = dt.Rows[0]["txtrs31"].ToString();
            txtrs32.Text = dt.Rows[0]["txtrs32"].ToString();
            txtrs33.Text = dt.Rows[0]["txtrs33"].ToString();
            txtrs41.Text = dt.Rows[0]["txtrs41"].ToString();
            txtrs42.Text = dt.Rows[0]["txtrs42"].ToString();
            txtrs43.Text = dt.Rows[0]["txtrs43"].ToString();
            txtrpp11.Text = dt.Rows[0]["txtrpp11"].ToString();
            txtrpp12.Text = dt.Rows[0]["txtrpp12"].ToString();
            txtrpp13.Text = dt.Rows[0]["txtrpp13"].ToString();
            txtrpp21.Text = dt.Rows[0]["txtrpp21"].ToString();
            txtrpp22.Text = dt.Rows[0]["txtrpp22"].ToString();
            txtrpp23.Text = dt.Rows[0]["txtrpp23"].ToString();
            txtrpp31.Text = dt.Rows[0]["txtrpp31"].ToString();
            txtrpp32.Text = dt.Rows[0]["txtrpp32"].ToString();
            txtrpp33.Text = dt.Rows[0]["txtrpp33"].ToString();
            txtrpro11.Text = dt.Rows[0]["txtrpro11"].ToString();
            txtrpro12.Text = dt.Rows[0]["txtrpro12"].ToString();
            txtrpro13.Text = dt.Rows[0]["txtrpro13"].ToString();
            txtrpro21.Text = dt.Rows[0]["txtrpro21"].ToString();
            txtrpro22.Text = dt.Rows[0]["txtrpro22"].ToString();
            txtrpro23.Text = dt.Rows[0]["txtrpro23"].ToString();
            txtrpro31.Text = dt.Rows[0]["txtrpro31"].ToString();
            txtrpro32.Text = dt.Rows[0]["txtrpro32"].ToString();
            txtrpro33.Text = dt.Rows[0]["txtrpro33"].ToString();
            txtrpro41.Text = dt.Rows[0]["txtrpro41"].ToString();
            txtrpro42.Text = dt.Rows[0]["txtrpro42"].ToString();
            txtrpro43.Text = dt.Rows[0]["txtrpro43"].ToString();
            txtrpro51.Text = dt.Rows[0]["txtrpro51"].ToString();
            txtrpro52.Text = dt.Rows[0]["txtrpro52"].ToString();
            txtrpro53.Text = dt.Rows[0]["txtrpro53"].ToString();
            txtrpro54.Text = dt.Rows[0]["txtrpro54"].ToString();
            txtrpro55.Text = dt.Rows[0]["txtrpro55"].ToString();
            txtrpro56.Text = dt.Rows[0]["txtrpro56"].ToString();
            txtrproT11.Text = dt.Rows[0]["txtrproT11"].ToString();
            txtrproT12.Text = dt.Rows[0]["txtrproT12"].ToString();
            txtrproT13.Text = dt.Rows[0]["txtrproT13"].ToString();
            txtinb11.Text = dt.Rows[0]["txtinb11"].ToString();
            txtinb12.Text = dt.Rows[0]["txtinb12"].ToString();
            txtinb13.Text = dt.Rows[0]["txtinb13"].ToString();
            txtinb21.Text = dt.Rows[0]["txtinb21"].ToString();
            txtinb22.Text = dt.Rows[0]["txtinb22"].ToString();
            txtinb23.Text = dt.Rows[0]["txtinb23"].ToString();
            txtinb31.Text = dt.Rows[0]["txtinb31"].ToString();
            txtinb32.Text = dt.Rows[0]["txtinb32"].ToString();
            txtinb33.Text = dt.Rows[0]["txtinb33"].ToString();
            txtinb41.Text = dt.Rows[0]["txtinb41"].ToString();
            txtinb42.Text = dt.Rows[0]["txtinb42"].ToString();
            txtinb43.Text = dt.Rows[0]["txtinb43"].ToString();
            txtinb51.Text = dt.Rows[0]["txtinb51"].ToString();
            txtinb52.Text = dt.Rows[0]["txtinb52"].ToString();
            txtinb53.Text = dt.Rows[0]["txtinb53"].ToString();
            txtinb61.Text = dt.Rows[0]["txtinb61"].ToString();
            txtinb62.Text = dt.Rows[0]["txtinb62"].ToString();
            txtinb63.Text = dt.Rows[0]["txtinb63"].ToString();
            txtinb71.Text = dt.Rows[0]["txtinb71"].ToString();
            txtinb72.Text = dt.Rows[0]["txtinb72"].ToString();
            txtinb73.Text = dt.Rows[0]["txtinb73"].ToString();

            txtinb74.Text = dt.Rows[0]["txtinb74"].ToString();
            txtinb75.Text = dt.Rows[0]["txtinb75"].ToString();
            txtinb76.Text = dt.Rows[0]["txtinb76"].ToString();
            txtinb77.Text = dt.Rows[0]["txtinb77"].ToString();
            txtinb78.Text = dt.Rows[0]["txtinb78"].ToString();
            txtinb79.Text = dt.Rows[0]["txtinb79"].ToString();



            txtinbT11.Text = dt.Rows[0]["txtinbT11"].ToString();
            txtinbT12.Text = dt.Rows[0]["txtinbT12"].ToString();
            txtinbT13.Text = dt.Rows[0]["txtinbT13"].ToString();
            txtsd11.Text = dt.Rows[0]["txtsd11"].ToString();
            txtsd12.Text = dt.Rows[0]["txtsd12"].ToString();
            txtsd13.Text = dt.Rows[0]["txtsd13"].ToString();
            txtsd21.Text = dt.Rows[0]["txtsd21"].ToString();
            txtsd22.Text = dt.Rows[0]["txtsd22"].ToString();
            txtsd23.Text = dt.Rows[0]["txtsd23"].ToString();
            txtsd31.Text = dt.Rows[0]["txtsd31"].ToString();
            txtsd32.Text = dt.Rows[0]["txtsd32"].ToString();
            txtsd33.Text = dt.Rows[0]["txtsd33"].ToString();
            txtsd41.Text = dt.Rows[0]["txtsd41"].ToString();
            txtsd42.Text = dt.Rows[0]["txtsd42"].ToString();
            txtsd43.Text = dt.Rows[0]["txtsd43"].ToString();
            txtsd51.Text = dt.Rows[0]["txtsd51"].ToString();
            txtsd52.Text = dt.Rows[0]["txtsd52"].ToString();
            txtsd53.Text = dt.Rows[0]["txtsd53"].ToString();
            txtsd61.Text = dt.Rows[0]["txtsd61"].ToString();
            txtsd62.Text = dt.Rows[0]["txtsd62"].ToString();
            txtsd63.Text = dt.Rows[0]["txtsd63"].ToString();
            txtsd71.Text = dt.Rows[0]["txtsd71"].ToString();
            txtsd72.Text = dt.Rows[0]["txtsd72"].ToString();
            txtsd73.Text = dt.Rows[0]["txtsd73"].ToString();

            txtsd74.Text = dt.Rows[0]["txtsd74"].ToString();
            txtsd75.Text = dt.Rows[0]["txtsd75"].ToString();
            txtsd76.Text = dt.Rows[0]["txtsd76"].ToString();
            txtsd77.Text = dt.Rows[0]["txtsd77"].ToString();
            txtsd78.Text = dt.Rows[0]["txtsd78"].ToString();
            txtsd79.Text = dt.Rows[0]["txtsd79"].ToString();


            txtsdT11.Text = dt.Rows[0]["txtsdT11"].ToString();
            txtsdT12.Text = dt.Rows[0]["txtsdT12"].ToString();
            txtsdT13.Text = dt.Rows[0]["txtsdT13"].ToString();
            txtpcm11.Text = dt.Rows[0]["txtpcm11"].ToString();
            txtpcm12.Text = dt.Rows[0]["txtpcm12"].ToString();
            txtpcm13.Text = dt.Rows[0]["txtpcm13"].ToString();
            txtpcm21.Text = dt.Rows[0]["txtpcm21"].ToString();
            txtpcm22.Text = dt.Rows[0]["txtpcm22"].ToString();
            txtpcm23.Text = dt.Rows[0]["txtpcm23"].ToString();
            txtpcm31.Text = dt.Rows[0]["txtpcm31"].ToString();
            txtpcm32.Text = dt.Rows[0]["txtpcm32"].ToString();
            txtpcm33.Text = dt.Rows[0]["txtpcm33"].ToString();
            txtpcm41.Text = dt.Rows[0]["txtpcm41"].ToString();
            txtpcm42.Text = dt.Rows[0]["txtpcm42"].ToString();
            txtpcm43.Text = dt.Rows[0]["txtpcm43"].ToString();
            txtpcm51.Text = dt.Rows[0]["txtpcm51"].ToString();
            txtpcm52.Text = dt.Rows[0]["txtpcm52"].ToString();
            txtpcm53.Text = dt.Rows[0]["txtpcm53"].ToString();
            txtpcm61.Text = dt.Rows[0]["txtpcm61"].ToString();
            txtpcm62.Text = dt.Rows[0]["txtpcm62"].ToString();
            txtpcm63.Text = dt.Rows[0]["txtpcm63"].ToString();
            txtpcmT11.Text = dt.Rows[0]["txtpcmT11"].ToString();
            txtpcmT12.Text = dt.Rows[0]["txtpcmT12"].ToString();
            txtpcmT13.Text = dt.Rows[0]["txtpcmT13"].ToString();
            txtpiis11.Text = dt.Rows[0]["txtpiis11"].ToString();
            txtpiis12.Text = dt.Rows[0]["txtpiis12"].ToString();
            txtpiis13.Text = dt.Rows[0]["txtpiis13"].ToString();
            txtpiis21.Text = dt.Rows[0]["txtpiis21"].ToString();
            txtpiis22.Text = dt.Rows[0]["txtpiis22"].ToString();
            txtpiis23.Text = dt.Rows[0]["txtpiis23"].ToString();
            txtpiis31.Text = dt.Rows[0]["txtpiis31"].ToString();
            txtpiis32.Text = dt.Rows[0]["txtpiis32"].ToString();
            txtpiis33.Text = dt.Rows[0]["txtpiis33"].ToString();
            txtpiis41.Text = dt.Rows[0]["txtpiis41"].ToString();

            txtpiis42.Text = dt.Rows[0]["txtpiis42"].ToString();
            txtpiis43.Text = dt.Rows[0]["txtpiis43"].ToString();
            txtpiis51.Text = dt.Rows[0]["txtpiis51"].ToString();
            txtpiis52.Text = dt.Rows[0]["txtpiis52"].ToString();
            txtpiis53.Text = dt.Rows[0]["txtpiis53"].ToString();

            txtpiis54.Text = dt.Rows[0]["txtpiis54"].ToString();
            txtpiis55.Text = dt.Rows[0]["txtpiis55"].ToString();
            txtpiis56.Text = dt.Rows[0]["txtpiis56"].ToString();
            txtpiis57.Text = dt.Rows[0]["txtpiis57"].ToString();
            txtpiis58.Text = dt.Rows[0]["txtpiis58"].ToString();
            txtpiis59.Text = dt.Rows[0]["txtpiis59"].ToString();

            txtpiisT11.Text = dt.Rows[0]["txtpiisT11"].ToString();
            txtpiisT12.Text = dt.Rows[0]["txtpiisT12"].ToString();
            txtpiisT13.Text = dt.Rows[0]["txtpiisT13"].ToString();
            txtssc11.Text = dt.Rows[0]["txtssc11"].ToString();
            txtssc12.Text = dt.Rows[0]["txtssc12"].ToString();
            txtssc13.Text = dt.Rows[0]["txtssc13"].ToString();
            txtssc21.Text = dt.Rows[0]["txtssc21"].ToString();
            txtssc22.Text = dt.Rows[0]["txtssc22"].ToString();
            txtssc23.Text = dt.Rows[0]["txtssc23"].ToString();
            txtssc31.Text = dt.Rows[0]["txtssc31"].ToString();
            txtssc32.Text = dt.Rows[0]["txtssc32"].ToString();
            txtssc33.Text = dt.Rows[0]["txtssc33"].ToString();

            txtssc34.Text = dt.Rows[0]["txtssc34"].ToString();
            txtssc35.Text = dt.Rows[0]["txtssc35"].ToString();
            txtssc36.Text = dt.Rows[0]["txtssc36"].ToString();
            txtssc37.Text = dt.Rows[0]["txtssc37"].ToString();
            txtssc38.Text = dt.Rows[0]["txtssc38"].ToString();
            txtssc39.Text = dt.Rows[0]["txtssc39"].ToString();

            txtsscT1.Text = dt.Rows[0]["txtsscT1"].ToString();
            txtsscT2.Text = dt.Rows[0]["txtsscT2"].ToString();
            txtsscT3.Text = dt.Rows[0]["txtsscT3"].ToString();
            txtcsw11.Text = dt.Rows[0]["txtcsw11"].ToString();
            txtcsw12.Text = dt.Rows[0]["txtcsw12"].ToString();
            txtcsw13.Text = dt.Rows[0]["txtcsw13"].ToString();
            txtcsw21.Text = dt.Rows[0]["txtcsw21"].ToString();
            txtcsw22.Text = dt.Rows[0]["txtcsw22"].ToString();
            txtcsw23.Text = dt.Rows[0]["txtcsw23"].ToString();
            txtcsw31.Text = dt.Rows[0]["txtcsw31"].ToString();
            txtcsw32.Text = dt.Rows[0]["txtcsw32"].ToString();
            txtcsw33.Text = dt.Rows[0]["txtcsw33"].ToString();
            txtcsw41.Text = dt.Rows[0]["txtcsw41"].ToString();
            txtcsw42.Text = dt.Rows[0]["txtcsw42"].ToString();
            txtcsw43.Text = dt.Rows[0]["txtcsw43"].ToString();
            txtcsw51.Text = dt.Rows[0]["txtcsw51"].ToString();
            txtcsw52.Text = dt.Rows[0]["txtcsw52"].ToString();
            txtcsw53.Text = dt.Rows[0]["txtcsw53"].ToString();
            txtcswT11.Text = dt.Rows[0]["txtcswT11"].ToString();
            txtcswT12.Text = dt.Rows[0]["txtcswT12"].ToString();
            txtcswT13.Text = dt.Rows[0]["txtcswT13"].ToString();
            txtGGT11.Text = dt.Rows[0]["txtGGT11"].ToString();
            txtGGT12.Text = dt.Rows[0]["txtGGT12"].ToString();
            txtGGT13.Text = dt.Rows[0]["txtGGT13"].ToString();
            txtp2a.Text = dt.Rows[0]["txtp2a"].ToString();
            txtp2b.Text = dt.Rows[0]["txtp2b"].ToString();
            txtp2c.Text = dt.Rows[0]["txtp2c"].ToString();
            txtp2d.Text = dt.Rows[0]["txtp2d"].ToString();
            txtp2e.Text = dt.Rows[0]["txtp2e"].ToString();
            datep2.Text = dt.Rows[0]["datep2"].ToString();
            SignAssp2.Text = dt.Rows[0]["SignAssp2"].ToString();
            SignCos.Text = dt.Rows[0]["SignCos"].ToString();
            txtp3a.Text = dt.Rows[0]["txtp3a"].ToString();
            txtp3b.Text = dt.Rows[0]["txtp3b"].ToString();
            txtp3c.Text = dt.Rows[0]["txtp3c"].ToString();
            txtp3d.Text = dt.Rows[0]["txtp3d"].ToString();
            txtp3e.Text = dt.Rows[0]["txtp3e"].ToString();
            txtp3f.Text = dt.Rows[0]["txtp3f"].ToString();
            txtp3g.Text = dt.Rows[0]["txtp3g"].ToString();
            txtp3h.Text = dt.Rows[0]["txtp3h"].ToString();
            txtp3i.Text = dt.Rows[0]["txtp3i"].ToString();
            txtp3j.Text = dt.Rows[0]["txtp3j"].ToString();
            txtp41point.Text = dt.Rows[0]["txtGGT12"].ToString();//txtp41point
            txtp43point.Text = dt.Rows[0]["txtp43point"].ToString();
            txtp51point.Text = dt.Rows[0]["txtGGT13"].ToString();//txtp51point
            txtp53point.Text = dt.Rows[0]["txtp53point"].ToString();
            txt6coments.Text = dt.Rows[0]["txt6coments"].ToString();
            txt7tpoint.Text = dt.Rows[0]["txt7tpoint"].ToString();
            //txt7flg.Text = dt.Rows[0]["txt7flg"].ToString();
            txtp7r1.Text = dt.Rows[0]["txtp7r1"].ToString();
            txtp7r2.Text = dt.Rows[0]["txtp7r2"].ToString();
            txtp7r3.Text = dt.Rows[0]["txtp7r3"].ToString();
            txtVC.Text = dt.Rows[0]["txtVC"].ToString();
            Enable();
            SqlCommand cmd1 = new SqlCommand("proc_Gettbl_PMSF", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@Fid", HfStaffcode.Value);
            cmd1.Parameters.AddWithValue("@AcadmicYear", drpAcademic.SelectedValue);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            con.Open();
            da1.Fill(dt1);
            con.Close();

            


            if (dt1.Rows.Count > 0)
            {
                EnableF();

                btnApproved.Visible = false;
                btntest.Visible = true;
                if ((Session["UserGroup"].ToString() == "HR") && (dt.Rows[0]["Status"].ToString() == "3"))
                {
                    HREnble();
                    btnApproved.Visible = true;
                }
                if ((Session["UserGroup"].ToString() == "VC") && (dt.Rows[0]["Status"].ToString() == "4"))
                {
                    //HREnble();
                    txtVC.Enabled = true;
                    btnApproved.Visible = true;
                }
              
            }
            bindAttachment(HfStaffcode.Value);

            //if (drpAcademic.SelectedValue == "22-23" || Session["GlobalDimension1Code"].ToString() == "TMEG")
            //{
            //    btnApproved.Visible = true;

            //}
            //else
            //{
            //    btnApproved.Visible = false;
            //}

          
           
        }
        else
        {
            btnApproved.Visible = true;
            
        }

           DivForm.Visible = true;
            btnback.Visible = true;
           GrdExamList.Visible = false;

        
    }
    public void bindAttachment(string UserId)
    {
        SqlCommand cmd = new SqlCommand("proc_GetPMSAttachment", con);
        cmd.Parameters.AddWithValue("@Fid", UserId);
        cmd.Parameters.AddWithValue("@AcadmicYear", drpAcademic.SelectedValue);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt1 = new DataTable();
        con1.Open();
        da.Fill(dt1);
        if(dt1.Rows.Count>0)
        {
            grdDocument.Visible = true;
            grdDocument.DataSource = dt1;
            grdDocument.DataBind();
        }
        else
        {
            grdDocument.Visible = false;
            grdDocument.DataSource = "";
            grdDocument.DataBind();

        }
       
    }

    protected void btnReject_Click(object sender, EventArgs e)
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("PMSAprovalReject", con);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@Fid", FF.Value);
        cmd.Parameters.AddWithValue("@Pid", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@AcademicYear", drpAcademic.SelectedValue);
        if (Session["UserGroup"].ToString() == "HR")
        {
            cmd.Parameters.AddWithValue("@Status", "7");
        }
        else if (Session["UserGroup"].ToString() == "VC")
        {
            cmd.Parameters.AddWithValue("@Status", "8");


        }
        else if (Session["UserGroup"].ToString() == "HOD" || Session["UserGroup"].ToString() == "PRINCIPAL" || Session["UserGroup"].ToString() == "DEAN")
        {
            cmd.Parameters.AddWithValue("@Status", "6");
        }
        int i = cmd.ExecuteNonQuery();
        con.Close();
        if (i > 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('PMS  Form Rejected')", true);
            facultylist();
            DivForm.Visible = false;
            btnback.Visible = false;
            GrdExamList.Visible = true;
            grdDocument.Visible = false;
           
        }
        else
        {
        }
      
    }

    private string SortDirection
    {
        get { return ViewState["SortDirection"] != null ? ViewState["SortDirection"].ToString() : "ASC"; }
        set { ViewState["SortDirection"] = value; }
    }
    protected void GrdExamList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdExamList.PageIndex = e.NewPageIndex;
        this.facultylist();
    }
    protected void OnSorting(object sender, GridViewSortEventArgs e)
    {
        this.facultylist(e.SortExpression);
    }
    protected void DownloadInboxFile(object sender, EventArgs e)
    {
        string Eval = (sender as LinkButton).CommandArgument;
        string AssignmentNo = (sender as LinkButton).CommandArgument;

        byte[] bytes;
        string fileName, contentType;
        using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
        {
            using (SqlCommand cmd2 = new SqlCommand())
            {
                cmd2.CommandText = "select Attachment,[Content Type],[File Name] from tbl_PMSAttachment where ID='" + AssignmentNo + "' ";
                cmd2.Connection = con2;
                con2.Open();
                using (SqlDataReader sdr = cmd2.ExecuteReader())
                {
                    sdr.Read();
                    bytes = (byte[])sdr["Attachment"];
                    contentType = sdr["Content Type"].ToString();
                    fileName = sdr["File Name"].ToString();
                }
                con.Close();
            }
        }
        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = contentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
        Response.BinaryWrite(bytes);
        Response.Flush();
        Response.End();

    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        GT3();




        part7();
        Save();
     
        DivForm.Visible = false;
        btnback.Visible = false;
        GrdExamList.Visible = true;
    }
    protected void drpAcademic_SelectedIndexChanged(object sender, EventArgs e)
    {
        facultylist();
             
    }
}