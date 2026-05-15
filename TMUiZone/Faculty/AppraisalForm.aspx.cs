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
using System.IO;

public partial class Faculty_feedbackfaculty : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["uid"] == null || string.IsNullOrEmpty(Session["uid"].ToString()))
        {
            // Response.Redirect("Default.aspx");
        }
        else
        {
            if (!IsPostBack)
            {
                bindAcademicYear();
                point();

                bindpms();
                Checkpms();
                SubmitTrufalse();
                bindAttachment();

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
    public void bindAttachment()
    {
        SqlCommand cmd = new SqlCommand("proc_GetPMSAttachment", con);
        cmd.Parameters.AddWithValue("@Fid", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@AcadmicYear", Label1.Text);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt1 = new DataTable();
        con1.Open();
        da.Fill(dt1);
        grdDocument.DataSource = dt1;
        grdDocument.DataBind();
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

    public void SubmitTrufalse()
    {
        SqlCommand cmd = new SqlCommand("proc_Gettbl_PMS", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Fid", Session["uid"].ToString());

        cmd.Parameters.AddWithValue("@AcadmicYear", drpAcademic.SelectedValue);


        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
        con.Open();
        da.Fill(dt);
        con.Close();
        if (dt.Rows.Count > 0)
        {
            if (drpAcademic.SelectedValue == "20-21")
            {
                if (Session["uid"].ToString() == "TMU03735" || Session["uid"].ToString() == "TMU03734" || Session["uid"].ToString() == "TMU04543" || Session["uid"].ToString() == "TMU00406" || Session["uid"].ToString() == "TMU00180" || Session["uid"].ToString() == "TMU00539" || Session["uid"].ToString() == "TMU00313")
                {
                    btnsave.Visible = true;
                    btnUpload.Visible = true;
                    btncheck.Visible = true;
                }
                else
                {
                    btnsave.Visible = false;
                    btnUpload.Visible = false;
                    btncheck.Visible = false;
                }
            }
            else
            {

                if (dt.Rows[0]["Status"].ToString() == "0" || dt.Rows[0]["Status"].ToString() == "1" || dt.Rows[0]["Status"].ToString() == "6")
                {
                    btnsave.Visible = true;
                    btnUpload.Visible = true;
                    btncheck.Visible = true;
                }
                else
                {
                    btnsave.Visible = false;
                    btnUpload.Visible = false;
                    btncheck.Visible = false;
                }

            }
        }
    

    }

    public void Checkpms()
    {
        SqlCommand cmd = new SqlCommand("proc_Gettbl_PMSubmit", con);
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
            EnableF();

            btncheck.Visible = false;
            btnsave.Visible = false;
            btntest.Visible = true;

        }
        else
        {
            EnableT();
            btnsave.Visible = true;
            btncheck.Visible = true;
        }
    }

    public void bindpms()
    {
        Label1.Text = drpAcademic.SelectedValue;
        SqlCommand cmd = new SqlCommand("proc_Gettbl_PMS", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Fid", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@AcadmicYear", drpAcademic.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Open();
        da.Fill(dt);
        con.Close();
        if (dt.Rows.Count > 0)
        {
            //printarea.Visible = true;
            //tblFoot.Visible = true;
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
            txt7flg.Text = dt.Rows[0]["txt7flg"].ToString();
            txtp7r1.Text = dt.Rows[0]["txtp7r1"].ToString();
            txtp7r2.Text = dt.Rows[0]["txtp7r2"].ToString();
            txtp7r3.Text = dt.Rows[0]["txtp7r3"].ToString();
            txtVC.Text = dt.Rows[0]["txtVC"].ToString();
            if (dt.Rows[0]["Status"].ToString() == "0" || dt.Rows[0]["Status"].ToString() == "1")
            {
                btnUpload.Visible = true;
                btncheck.Visible = true;
            }
            else
            {
                btnUpload.Visible = false;
                btncheck.Visible = false;
            }
        }
        else
        {
           if (drpAcademic.SelectedValue == "20-21" || Session["uid"].ToString() != "TMU03735" || Session["uid"].ToString() != "TMU03734" || Session["uid"].ToString() != "TMU04543" || Session["uid"].ToString() != "TMU00406" || Session["uid"].ToString() != "TMU00180" || Session["uid"].ToString() != "TMU00539" || Session["uid"].ToString() != "TMU00313")
            {
                btnsave.Visible = false;
                btncheck.Visible = false;
            }
            else
            {
                btnsave.Visible = true;
                btncheck.Visible = true;
            }
            txtct1.Text = "";
            txtct2.Text = "";
            txtct3.Text = "";
            txtsf11.Text = "";
            txtsf12.Text = "";
            txtsf13.Text = "";
            txtsf21.Text = "";
            txtsf22.Text = "";
            txtsf23.Text = "";
            txtsf31.Text = "";
            txtsf32.Text = "";
            txtsf33.Text = "";
            txtsf41.Text = "";
            txtsf42.Text = "";
            txtsf43.Text = "";
            txtsf51.Text = "";
            txtsf52.Text = "";
            txtsf53.Text = "";
            txtsf61.Text = "";
            txtsf62.Text = "";
            txtsf63.Text = "";

            txtsf64.Text = "";
            txtsf65.Text = "";
            txtsf66.Text = "";
            txtsf67.Text = "";
            txtsf68.Text = "";
            txtsf69.Text = "";
            txtsf70.Text = "";
            txtsf71.Text = "";
            txtsf72.Text = "";
            txtsf73.Text = "";
            txtsf74.Text = "";
            txtsf75.Text = "";
            txtsf76.Text = "";
            txtsf77.Text = "";
            txtsf78.Text = "";
            txtsf79.Text = "";
            txtsf80.Text = "";
            txtsf81.Text = "";
            txtip11.Text = "";
            txtip12.Text = "";
            txtip13.Text = "";
            txtip21.Text = "";
            txtip22.Text = "";
            txtip23.Text = "";
            txtip31.Text = "";
            txtip32.Text = "";
            txtip33.Text = "";
            txtNc11.Text = "";
            txtNc12.Text = "";
            txtNc13.Text = "";
            txtNc21.Text = "";
            txtNc22.Text = "";
            txtNc23.Text = "";
            txtNc31.Text = "";
            txtNc32.Text = "";
            txtNc33.Text = "";

            txtNc34.Text = "";
            txtNc35.Text = "";
            txtNc36.Text = "";
            txtNc37.Text = "";
            txtNc38.Text = "";
            txtNc39.Text = "";
            txtNc40.Text = "";
            txtNc41.Text = "";
            txtNc42.Text = "";









            txtcsm11.Text = "";
            txtcsm12.Text = "";
            txtcsm13.Text = "";
            txtcsm21.Text = "";
            txtcsm22.Text = "";
            txtcsm23.Text = "";
            txtpws11.Text = "";
            txtpws12.Text = "";
            txtpws13.Text = "";
            txtpws21.Text = "";
            txtpws22.Text = "";
            txtpws23.Text = "";
            txtpws31.Text = "";
            txtpws32.Text = "";
            txtpws33.Text = "";

            txtpws34.Text = "";
            txtpws35.Text = "";
            txtpws36.Text = "";



            txtpwA11.Text = "";
            txtpwA12.Text = "";
            txtpwA13.Text = "";
            txtpp11.Text = "";
            txtpp12.Text = "";
            txtpp13.Text = "";
            txtpp21.Text = "";
            txtpp22.Text = "";
            txtpp23.Text = "";
            txtpp31.Text = "";
            txtpp32.Text = "";
            txtpp33.Text = "";
            txtpp41.Text = "";
            txtpp42.Text = "";
            txtpp43.Text = "";
            txtpp51.Text = "";
            txtpp52.Text = "";
            txtpp53.Text = "";
            txtpp61.Text = "";
            txtpp62.Text = "";
            txtpp63.Text = "";
            txtrs11.Text = "";
            txtrs12.Text = "";
            txtrs13.Text = "";
            txtrs21.Text = "";
            txtrs22.Text = "";
            txtrs23.Text = "";
            txtrs31.Text = "";
            txtrs32.Text = "";
            txtrs33.Text = "";
            txtrs41.Text = "";
            txtrs42.Text = "";
            txtrs43.Text = "";
            txtrpp11.Text = "";
            txtrpp12.Text = "";
            txtrpp13.Text = "";
            txtrpp21.Text = "";
            txtrpp22.Text = "";
            txtrpp23.Text = "";
            txtrpp31.Text = "";
            txtrpp32.Text = "";
            txtrpp33.Text = "";
            txtrpro11.Text = "";
            txtrpro12.Text = "";
            txtrpro13.Text = "";
            txtrpro21.Text = "";
            txtrpro22.Text = "";
            txtrpro23.Text = "";
            txtrpro31.Text = "";
            txtrpro32.Text = "";
            txtrpro33.Text = "";
            txtrpro41.Text = "";
            txtrpro42.Text = "";
            txtrpro43.Text = "";
            txtrpro51.Text = "";
            txtrpro52.Text = "";
            txtrpro53.Text = "";
            txtrpro54.Text = "";
            txtrpro55.Text = "";
            txtrpro56.Text = "";
            txtrproT11.Text = "";
            txtrproT12.Text = "";
            txtrproT13.Text = "";
            txtinb11.Text = "";
            txtinb12.Text = "";
            txtinb13.Text = "";
            txtinb21.Text = "";
            txtinb22.Text = "";
            txtinb23.Text = "";
            txtinb31.Text = "";
            txtinb32.Text = "";
            txtinb33.Text = "";
            txtinb41.Text = "";
            txtinb42.Text = "";
            txtinb43.Text = "";
            txtinb51.Text = "";
            txtinb52.Text = "";
            txtinb53.Text = "";
            txtinb61.Text = "";
            txtinb62.Text = "";
            txtinb63.Text = "";
            txtinb71.Text = "";
            txtinb72.Text = "";
            txtinb73.Text = "";

            txtinb74.Text = "";
            txtinb75.Text = "";
            txtinb76.Text = "";
            txtinb77.Text = "";
            txtinb78.Text = "";
            txtinb79.Text = "";



            txtinbT11.Text = "";
            txtinbT12.Text = "";
            txtinbT13.Text = "";
            txtsd11.Text = "";
            txtsd12.Text = "";
            txtsd13.Text = "";
            txtsd21.Text = "";
            txtsd22.Text = "";
            txtsd23.Text = "";
            txtsd31.Text = "";
            txtsd32.Text = "";
            txtsd33.Text = "";
            txtsd41.Text = "";
            txtsd42.Text = "";
            txtsd43.Text = "";
            txtsd51.Text = "";
            txtsd52.Text = "";
            txtsd53.Text = "";
            txtsd61.Text = "";
            txtsd62.Text = "";
            txtsd63.Text = "";
            txtsd71.Text = "";
            txtsd72.Text = "";
            txtsd73.Text = "";

            txtsd74.Text = "";
            txtsd75.Text = "";
            txtsd76.Text = "";
            txtsd77.Text = "";
            txtsd78.Text = "";
            txtsd79.Text = "";


            txtsdT11.Text = "";
            txtsdT12.Text = "";
            txtsdT13.Text = "";
            txtpcm11.Text = "";
            txtpcm12.Text = "";
            txtpcm13.Text = "";
            txtpcm21.Text = "";
            txtpcm22.Text = "";
            txtpcm23.Text = "";
            txtpcm31.Text = "";
            txtpcm32.Text = "";
            txtpcm33.Text = "";
            txtpcm41.Text = "";
            txtpcm42.Text = "";
            txtpcm43.Text = "";
            txtpcm51.Text = "";
            txtpcm52.Text = "";
            txtpcm53.Text = "";
            txtpcm61.Text = "";
            txtpcm62.Text = "";
            txtpcm63.Text = "";
            txtpcmT11.Text = "";
            txtpcmT12.Text = "";
            txtpcmT13.Text = "";
            txtpiis11.Text = "";
            txtpiis12.Text = "";
            txtpiis21.Text = "";
            txtpiis22.Text = "";
            txtpiis23.Text = "";
            txtpiis31.Text = "";
            txtpiis32.Text = "";
            txtpiis33.Text = "";
            txtpiis41.Text = "";

            txtpiis42.Text = "";
            txtpiis43.Text = "";
            txtpiis51.Text = "";
            txtpiis52.Text = "";
            txtpiis53.Text = "";

            txtpiis54.Text = "";
            txtpiis55.Text = "";
            txtpiis56.Text = "";
            txtpiis57.Text = "";
            txtpiis58.Text = "";
            txtpiis59.Text = "";

            txtpiisT11.Text = "";
            txtpiisT12.Text = "";
            txtpiisT13.Text = "";
            txtssc11.Text = "";
            txtssc12.Text = "";
            txtssc21.Text = "";
            txtssc22.Text = "";
            txtssc23.Text = "";
            txtssc31.Text = "";
            txtssc32.Text = "";
            txtssc33.Text = "";

            txtssc34.Text = "";
            txtssc35.Text = "";
            txtssc36.Text = "";
            txtssc37.Text = "";
            txtssc38.Text = "";
            txtssc39.Text = "";

            txtsscT1.Text = "";
            txtsscT2.Text = "";
            txtsscT3.Text = "";
            txtcsw11.Text = "";
            txtcsw12.Text = "";
            txtcsw13.Text = "";
            txtcsw21.Text = "";
            txtcsw22.Text = "";
            txtcsw23.Text = "";
            txtcsw31.Text = "";
            txtcsw32.Text = "";
            txtcsw33.Text = "";
            txtcsw41.Text = "";
            txtcsw42.Text = "";
            txtcsw43.Text = "";
            txtcsw51.Text = "";
            txtcsw52.Text = "";
            txtcsw53.Text = "";
            txtcswT11.Text = "";
            txtcswT12.Text = "";
            txtcswT13.Text = "";
            txtGGT11.Text = "";
            txtGGT12.Text = "";
            txtGGT13.Text = "";
            txtp2a.Text = "";
            txtp2b.Text = "";
            txtp2c.Text = "";
            txtp2d.Text = "";
            txtp2e.Text = "";
            datep2.Text = "";
            SignAssp2.Text = "";
            SignCos.Text = "";
            txtp3a.Text = "";
            txtp3b.Text = "";
            txtp3c.Text = "";
            txtp3d.Text = "";
            txtp3e.Text = "";
            txtp3f.Text = "";
            txtp3g.Text = "";
            txtp3h.Text = "";
            txtp3i.Text = "";
            txtp3j.Text = "";
            txtp41point.Text = "";
            txtp43point.Text = "";
            txtp51point.Text = "";
            txtp53point.Text = "";
            txt6coments.Text = "";
            txt7tpoint.Text = "";
            txt7flg.Text = "";
            txtp7r1.Text = "";
            txtp7r2.Text = "";
            txtp7r3.Text = "";
            txtVC.Text = "";
            btncheck.Visible = false;
        }

    }
    public void EnableT()
    {
        txtct1.Enabled = true;
        txtct2.Enabled = true;

        //txtsf11.Enabled = true;
        //txtsf12.Enabled = true;

        //txtsf21.Enabled = true;
        //txtsf22.Enabled = true;

        //txtsf31.Enabled = true;
        //txtsf32.Enabled = true;

        //txtsf41.Enabled = true;
        //txtsf42.Enabled = true;

        //txtsf51.Enabled = true;
        //txtsf52.Enabled = true;

        //txtsf61.Enabled = true;
        //txtsf62.Enabled = true;
        //txtsf64.Enabled = true;
        //txtsf65.Enabled = true;
        //txtsf67.Enabled = true;
        //txtsf68.Enabled = true;

        //txtsf70.Enabled = true;
        //txtsf71.Enabled = true;
        //txtsf73.Enabled = true;
        //txtsf74.Enabled = true;

        //txtsf76.Enabled = true;
        //txtsf77.Enabled = true;
        //txtsf79.Enabled = true;
        //txtsf80.Enabled = true;


        txtip11.Enabled = true;
        txtip12.Enabled = true;

        txtip21.Enabled = true;
        txtip22.Enabled = true;

        txtip31.Enabled = true;
        txtip32.Enabled = true;

        txtNc11.Enabled = true;
        txtNc12.Enabled = true;

        txtNc21.Enabled = true;
        txtNc22.Enabled = true;

        txtNc31.Enabled = true;
        txtNc32.Enabled = true;
        txtNc34.Enabled = true;
        txtNc35.Enabled = true;
        txtNc37.Enabled = true;
        txtNc38.Enabled = true;
        txtNc40.Enabled = true;
        txtNc41.Enabled = true;


        txtcsm11.Enabled = true;
        txtcsm12.Enabled = true;

        txtcsm21.Enabled = true;
        txtcsm22.Enabled = true;

        txtpws11.Enabled = true;
        txtpws12.Enabled = true;

        txtpws21.Enabled = true;
        txtpws22.Enabled = true;

        txtpws31.Enabled = true;
        txtpws32.Enabled = true;

        txtpws34.Enabled = true;
        txtpws35.Enabled = true;

        txtpp11.Enabled = true;
        txtpp12.Enabled = true;

        txtpp21.Enabled = true;
        txtpp22.Enabled = true;

        txtpp31.Enabled = true;
        txtpp32.Enabled = true;

        txtpp41.Enabled = true;
        txtpp42.Enabled = true;

        txtpp51.Enabled = true;
        txtpp52.Enabled = true;

        txtpp61.Enabled = true;
        txtpp62.Enabled = true;

        txtrs11.Enabled = true;
        txtrs12.Enabled = true;

        txtrs21.Enabled = true;
        txtrs22.Enabled = true;

        txtrs31.Enabled = true;
        txtrs32.Enabled = true;

        txtrs41.Enabled = true;
        txtrs42.Enabled = true;

        txtrpp11.Enabled = true;
        txtrpp12.Enabled = true;

        txtrpp21.Enabled = true;
        txtrpp22.Enabled = true;

        txtrpp31.Enabled = true;
        txtrpp32.Enabled = true;

        txtrpro11.Enabled = true;
        txtrpro12.Enabled = true;

        txtrpro21.Enabled = true;
        txtrpro22.Enabled = true;

        txtrpro31.Enabled = true;
        txtrpro32.Enabled = true;

        txtrpro41.Enabled = true;
        txtrpro42.Enabled = true;

        txtrpro51.Enabled = true;
        txtrpro52.Enabled = true;

        txtrpro54.Enabled = true;
        txtrpro55.Enabled = true;

        txtinb11.Enabled = true;
        txtinb12.Enabled = true;

        txtinb21.Enabled = true;
        txtinb22.Enabled = true;

        txtinb31.Enabled = true;
        txtinb32.Enabled = true;

        txtinb41.Enabled = true;
        txtinb42.Enabled = true;

        txtinb51.Enabled = true;
        txtinb52.Enabled = true;

        txtinb61.Enabled = true;
        txtinb62.Enabled = true;

        txtinb71.Enabled = true;
        txtinb72.Enabled = true;

        txtinb74.Enabled = true;
        txtinb75.Enabled = true;

        txtinb77.Enabled = true;
        txtinb78.Enabled = true;

        txtsd11.Enabled = true;
        txtsd12.Enabled = true;

        txtsd21.Enabled = true;
        txtsd22.Enabled = true;

        txtsd31.Enabled = true;
        txtsd32.Enabled = true;

        txtsd41.Enabled = true;
        txtsd42.Enabled = true;

        txtsd51.Enabled = true;
        txtsd52.Enabled = true;

        txtsd61.Enabled = true;
        txtsd62.Enabled = true;

        txtsd71.Enabled = true;
        txtsd72.Enabled = true;

        txtsd74.Enabled = true;
        txtsd75.Enabled = true;
        txtsd77.Enabled = true;
        txtsd78.Enabled = true;


        txtpcm11.Enabled = true;
        txtpcm12.Enabled = true;

        txtpcm21.Enabled = true;
        txtpcm22.Enabled = true;

        txtpcm31.Enabled = true;
        txtpcm32.Enabled = true;

        txtpcm41.Enabled = true;
        txtpcm42.Enabled = true;

        txtpcm51.Enabled = true;
        txtpcm52.Enabled = true;

        txtpcm61.Enabled = true;
        txtpcm62.Enabled = true;


        txtpiis11.Enabled = true;
        txtpiis12.Enabled = true;

        txtpiis21.Enabled = true;
        txtpiis22.Enabled = true;

        txtpiis31.Enabled = true;
        txtpiis32.Enabled = true;

        txtpiis41.Enabled = true;
        txtpiis42.Enabled = true;

        txtpiis51.Enabled = true;
        txtpiis52.Enabled = true;

        txtpiis54.Enabled = true;
        txtpiis55.Enabled = true;

        txtpiis57.Enabled = true;
        txtpiis58.Enabled = true;

        txtssc11.Enabled = true;
        txtssc12.Enabled = true;

        txtssc21.Enabled = true;
        txtssc22.Enabled = true;

        txtssc31.Enabled = true;
        txtssc32.Enabled = true;
        txtssc34.Enabled = true;
        txtssc35.Enabled = true;
        txtssc37.Enabled = true;
        txtssc38.Enabled = true;


        txtcsw11.Enabled = true;
        txtcsw12.Enabled = true;

        txtcsw21.Enabled = true;
        txtcsw22.Enabled = true;

        txtcsw31.Enabled = true;
        txtcsw32.Enabled = true;

        txtcsw41.Enabled = true;
        txtcsw42.Enabled = true;

        txtcsw51.Enabled = true;
        txtcsw52.Enabled = true;


    }

    public void EnableF()
    {
        txtct1.Enabled = false;
        txtct2.Enabled = false;

        txtsf11.Enabled = false;
        txtsf12.Enabled = false;

        txtsf21.Enabled = false;
        txtsf22.Enabled = false;

        txtsf31.Enabled = false;
        txtsf32.Enabled = false;

        txtsf41.Enabled = false;
        txtsf42.Enabled = false;

        txtsf51.Enabled = false;
        txtsf52.Enabled = false;

        txtsf61.Enabled = false;
        txtsf62.Enabled = false;

        txtsf64.Enabled = false;
        txtsf65.Enabled = false;
        txtsf67.Enabled = false;
        txtsf68.Enabled = false;

        txtsf70.Enabled = false;
        txtsf71.Enabled = false;
        txtsf73.Enabled = false;
        txtsf74.Enabled = false;

        txtsf76.Enabled = false;
        txtsf77.Enabled = false;
        txtsf79.Enabled = false;
        txtsf80.Enabled = false;



        txtip11.Enabled = false;
        txtip12.Enabled = false;

        txtip21.Enabled = false;
        txtip22.Enabled = false;

        txtip31.Enabled = false;
        txtip32.Enabled = false;

        txtNc11.Enabled = false;
        txtNc12.Enabled = false;

        txtNc21.Enabled = false;
        txtNc22.Enabled = false;

        txtNc31.Enabled = false;
        txtNc32.Enabled = false;
        txtNc34.Enabled = false;
        txtNc35.Enabled = false;
        txtNc37.Enabled = false;
        txtNc38.Enabled = false;
        txtNc40.Enabled = false;
        txtNc41.Enabled = false;
        txtcsm11.Enabled = false;
        txtcsm12.Enabled = false;

        txtcsm21.Enabled = false;
        txtcsm22.Enabled = false;

        txtpws11.Enabled = false;
        txtpws12.Enabled = false;

        txtpws21.Enabled = false;
        txtpws22.Enabled = false;

        txtpws31.Enabled = false;
        txtpws32.Enabled = false;
        txtpws34.Enabled = false;
        txtpws35.Enabled = false;
        txtpp11.Enabled = false;
        txtpp12.Enabled = false;

        txtpp21.Enabled = false;
        txtpp22.Enabled = false;

        txtpp31.Enabled = false;
        txtpp32.Enabled = false;

        txtpp41.Enabled = false;
        txtpp42.Enabled = false;

        txtpp51.Enabled = false;
        txtpp52.Enabled = false;

        txtpp61.Enabled = false;
        txtpp62.Enabled = false;

        txtrs11.Enabled = false;
        txtrs12.Enabled = false;

        txtrs21.Enabled = false;
        txtrs22.Enabled = false;

        txtrs31.Enabled = false;
        txtrs32.Enabled = false;

        txtrs41.Enabled = false;
        txtrs42.Enabled = false;

        txtrpp11.Enabled = false;
        txtrpp12.Enabled = false;

        txtrpp21.Enabled = false;
        txtrpp22.Enabled = false;

        txtrpp31.Enabled = false;
        txtrpp32.Enabled = false;

        txtrpro11.Enabled = false;
        txtrpro12.Enabled = false;

        txtrpro21.Enabled = false;
        txtrpro22.Enabled = false;

        txtrpro31.Enabled = false;
        txtrpro32.Enabled = false;

        txtrpro41.Enabled = false;
        txtrpro42.Enabled = false;

        //txtrpro44.Enabled = false;
        //txtrpro45.Enabled = false;

        txtrpro51.Enabled = false;
        txtrpro52.Enabled = false;
        txtrpro54.Enabled = false;
        txtrpro55.Enabled = false;

        txtinb11.Enabled = false;
        txtinb12.Enabled = false;

        txtinb21.Enabled = false;
        txtinb22.Enabled = false;

        txtinb31.Enabled = false;
        txtinb32.Enabled = false;

        txtinb41.Enabled = false;
        txtinb42.Enabled = false;

        txtinb51.Enabled = false;
        txtinb52.Enabled = false;

        txtinb61.Enabled = false;
        txtinb62.Enabled = false;

        txtinb71.Enabled = false;
        txtinb72.Enabled = false;
        txtinb74.Enabled = false;

        txtinb75.Enabled = false;
        txtinb77.Enabled = false;

        txtinb78.Enabled = false;
        txtsd11.Enabled = false;
        txtsd12.Enabled = false;

        txtsd21.Enabled = false;
        txtsd22.Enabled = false;

        txtsd31.Enabled = false;
        txtsd32.Enabled = false;

        txtsd41.Enabled = false;
        txtsd42.Enabled = false;

        txtsd51.Enabled = false;
        txtsd52.Enabled = false;

        txtsd61.Enabled = false;
        txtsd62.Enabled = false;

        txtsd71.Enabled = false;
        txtsd72.Enabled = false;

        txtsd74.Enabled = false;
        txtsd75.Enabled = false;

        txtsd77.Enabled = false;
        txtsd78.Enabled = false;

        txtpcm11.Enabled = false;
        txtpcm12.Enabled = false;

        txtpcm21.Enabled = false;
        txtpcm22.Enabled = false;

        txtpcm31.Enabled = false;
        txtpcm32.Enabled = false;

        txtpcm41.Enabled = false;
        txtpcm42.Enabled = false;

        txtpcm51.Enabled = false;
        txtpcm52.Enabled = false;

        txtpcm61.Enabled = false;
        txtpcm62.Enabled = false;


        txtpiis11.Enabled = false;
        txtpiis12.Enabled = false;

        txtpiis21.Enabled = false;
        txtpiis22.Enabled = false;

        txtpiis31.Enabled = false;
        txtpiis32.Enabled = false;

        txtpiis41.Enabled = false;
        txtpiis42.Enabled = false;

        txtpiis51.Enabled = false;
        txtpiis52.Enabled = false;

        txtpiis54.Enabled = false;
        txtpiis55.Enabled = false;

        txtpiis57.Enabled = false;
        txtpiis58.Enabled = false;

        txtssc11.Enabled = false;
        txtssc12.Enabled = false;

        txtssc21.Enabled = false;
        txtssc22.Enabled = false;

        txtssc31.Enabled = false;
        txtssc32.Enabled = false;

        txtssc34.Enabled = false;
        txtssc35.Enabled = false;

        txtssc37.Enabled = false;
        txtssc38.Enabled = false;


        txtsscT1.Enabled = false;
        txtsscT2.Enabled = false;

        txtcsw11.Enabled = false;
        txtcsw12.Enabled = false;

        txtcsw21.Enabled = false;
        txtcsw22.Enabled = false;

        txtcsw31.Enabled = false;
        txtcsw32.Enabled = false;

        txtcsw41.Enabled = false;
        txtcsw42.Enabled = false;

        txtcsw51.Enabled = false;
        txtcsw52.Enabled = false;

    }
    public void submit(int Status)
    {
        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Pmsinsert", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Fid", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Hid", "");
            cmd.Parameters.AddWithValue("@Pid", "");
            if (lblAcad.Visible == true)
            {
                cmd.Parameters.AddWithValue("@AcademicYear", lblAcad.Text);

            }
            else
            {
                cmd.Parameters.AddWithValue("@AcademicYear", Label1.Text);

            }
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
            if (txtpwA12.Text != "")
            {
                if (Convert.ToInt32(txtpwA12.Text) > 160)
                {
                    txtpwA12.Text = "160";
                }
            }
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
            if (txtrproT12.Text != "")
            {
                if (Convert.ToInt32(txtrproT12.Text) > 60)
                {
                    txtrproT12.Text = "60";
                }
            }
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
            if (txtinbT12.Text != "")
            {
                if (Convert.ToInt32(txtinbT12.Text) > 20)
                {
                    txtinbT12.Text = "20";
                }
            }
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
            if (txtsdT12.Text != "")
            {
                if (Convert.ToInt32(txtsdT12.Text) > 15)
                {
                    txtsdT12.Text = "15";
                }
            }
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
            if (txtpcmT12.Text != "")
            {
                if (Convert.ToInt32(txtpcmT12.Text) > 20)
                {
                    txtpcmT12.Text = "20";
                }
            }
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
            if (txtpiisT12.Text != "")
            {
                if (Convert.ToInt32(txtpiisT12.Text) > 10)
                {
                    txtpiisT12.Text = "10";
                }
            }

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
            if (txtsscT2.Text != "")
            {
                if (Convert.ToInt32(txtsscT2.Text) > 10)
                {
                    txtsscT2.Text = "10";
                }
            }

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
            if (txtcswT12.Text != "")
            {
                if (Convert.ToInt32(txtcswT12.Text) > 10)
                {
                    txtcswT12.Text = "10";
                }
            }
            cmd.Parameters.AddWithValue("@txtcswT12", txtcswT12.Text);
            cmd.Parameters.AddWithValue("@txtcswT13", txtcswT13.Text);
            cmd.Parameters.AddWithValue("@txtGGT11", txtGGT11.Text);
            if (txtGGT12.Text != "")
            {
                if (Convert.ToInt32(txtGGT12.Text) > 300)
                {
                    txtGGT12.Text = "300";
                }
            }

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
            cmd.Parameters.AddWithValue("@Status", Status);
            // cmd.Parameters.AddWithValue("@CollegeCode", Session["GlobalDimension1Code"].ToString());
            cmd.ExecuteNonQuery();
            con.Close();

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data saved successfully')", true);
            SubmitTrufalse();
        }
        catch
        {
        }
    }

    protected void btnsave_Click(object sender, EventArgs e)
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

        submit(1);


        SubmitTrufalse();

    }




    public void GT()
    {

        decimal A;//Convert.ToDecimal(txtct1.Text = string.IsNullOrEmpty(txtct1.Text) ? "0" : txtct1.Text)
        A = (Convert.ToDecimal(txtct1.Text = string.IsNullOrEmpty(txtct1.Text) ? "0" : txtct1.Text)
            //+ Convert.ToDecimal(txtsf11.Text = string.IsNullOrEmpty(txtsf11.Text) ? "0" : txtsf11.Text)
            //+ Convert.ToDecimal(txtsf21.Text = string.IsNullOrEmpty(txtsf21.Text) ? "0" : txtsf21.Text)
            //+ Convert.ToDecimal(txtsf31.Text = string.IsNullOrEmpty(txtsf31.Text) ? "0" : txtsf31.Text)
            //+ Convert.ToDecimal(txtsf41.Text = string.IsNullOrEmpty(txtsf41.Text) ? "0" : txtsf41.Text)
            //+ Convert.ToDecimal(txtsf51.Text = string.IsNullOrEmpty(txtsf51.Text) ? "0" : txtsf51.Text)
            //+ Convert.ToDecimal(txtsf61.Text = string.IsNullOrEmpty(txtsf61.Text) ? "0" : txtsf61.Text)
            // + Convert.ToDecimal(txtsf64.Text = string.IsNullOrEmpty(txtsf64.Text) ? "0" : txtsf64.Text)
            //  + Convert.ToDecimal(txtsf67.Text = string.IsNullOrEmpty(txtsf67.Text) ? "0" : txtsf67.Text)
            //   + Convert.ToDecimal(txtsf70.Text = string.IsNullOrEmpty(txtsf70.Text) ? "0" : txtsf70.Text)
            //    + Convert.ToDecimal(txtsf73.Text = string.IsNullOrEmpty(txtsf73.Text) ? "0" : txtsf73.Text)
            //     + Convert.ToDecimal(txtsf76.Text = string.IsNullOrEmpty(txtsf76.Text) ? "0" : txtsf76.Text)
            //      + Convert.ToDecimal(txtsf79.Text = string.IsNullOrEmpty(txtsf79.Text) ? "0" : txtsf79.Text)








        + Convert.ToDecimal(txtip11.Text = string.IsNullOrEmpty(txtip11.Text) ? "0" : txtip11.Text)
        + Convert.ToDecimal(txtip21.Text = string.IsNullOrEmpty(txtip21.Text) ? "0" : txtip21.Text)
        + Convert.ToDecimal(txtip31.Text = string.IsNullOrEmpty(txtip31.Text) ? "0" : txtip31.Text)

        + Convert.ToDecimal(txtNc11.Text = string.IsNullOrEmpty(txtNc11.Text) ? "0" : txtNc11.Text)
        + Convert.ToDecimal(txtNc21.Text = string.IsNullOrEmpty(txtNc21.Text) ? "0" : txtNc21.Text)
        + Convert.ToDecimal(txtNc31.Text = string.IsNullOrEmpty(txtNc31.Text) ? "0" : txtNc31.Text)

         + Convert.ToDecimal(txtNc34.Text = string.IsNullOrEmpty(txtNc34.Text) ? "0" : txtNc34.Text)
        + Convert.ToDecimal(txtNc37.Text = string.IsNullOrEmpty(txtNc37.Text) ? "0" : txtNc37.Text)
        + Convert.ToDecimal(txtNc40.Text = string.IsNullOrEmpty(txtNc40.Text) ? "0" : txtNc40.Text)



        + Convert.ToDecimal(txtcsm11.Text = string.IsNullOrEmpty(txtcsm11.Text) ? "0" : txtcsm11.Text)
        + Convert.ToDecimal(txtcsm21.Text = string.IsNullOrEmpty(txtcsm21.Text) ? "0" : txtcsm21.Text)


        + Convert.ToDecimal(txtpws11.Text = string.IsNullOrEmpty(txtpws11.Text) ? "0" : txtpws11.Text)
        + Convert.ToDecimal(txtpws21.Text = string.IsNullOrEmpty(txtpws21.Text) ? "0" : txtpws21.Text)
        + Convert.ToDecimal(txtpws31.Text = string.IsNullOrEmpty(txtpws31.Text) ? "0" : txtpws31.Text)
          + Convert.ToDecimal(txtpws34.Text = string.IsNullOrEmpty(txtpws34.Text) ? "0" : txtpws34.Text));


        txtpwA11.Text = Convert.ToString(A);




        decimal A1;
        A1 = (Convert.ToDecimal(txtct2.Text = string.IsNullOrEmpty(txtct2.Text) ? "0" : txtct2.Text)
            //  + Convert.ToDecimal(txtsf12.Text = string.IsNullOrEmpty(txtsf12.Text) ? "0" : txtsf12.Text)
            //  + Convert.ToDecimal(txtsf22.Text = string.IsNullOrEmpty(txtsf22.Text) ? "0" : txtsf22.Text)
            //  + Convert.ToDecimal(txtsf32.Text = string.IsNullOrEmpty(txtsf32.Text) ? "0" : txtsf32.Text)
            //  + Convert.ToDecimal(txtsf42.Text = string.IsNullOrEmpty(txtsf42.Text) ? "0" : txtsf42.Text)
            //  + Convert.ToDecimal(txtsf52.Text = string.IsNullOrEmpty(txtsf52.Text) ? "0" : txtsf52.Text)
            //  + Convert.ToDecimal(txtsf62.Text = string.IsNullOrEmpty(txtsf62.Text) ? "0" : txtsf62.Text)
            //    + Convert.ToDecimal(txtsf65.Text = string.IsNullOrEmpty(txtsf65.Text) ? "0" : txtsf65.Text)
            //+ Convert.ToDecimal(txtsf68.Text = string.IsNullOrEmpty(txtsf68.Text) ? "0" : txtsf68.Text)
            // + Convert.ToDecimal(txtsf71.Text = string.IsNullOrEmpty(txtsf71.Text) ? "0" : txtsf71.Text)
            //  + Convert.ToDecimal(txtsf74.Text = string.IsNullOrEmpty(txtsf74.Text) ? "0" : txtsf74.Text)
            //   + Convert.ToDecimal(txtsf77.Text = string.IsNullOrEmpty(txtsf77.Text) ? "0" : txtsf77.Text)
            //    + Convert.ToDecimal(txtsf80.Text = string.IsNullOrEmpty(txtsf80.Text) ? "0" : txtsf80.Text)


            + Convert.ToDecimal(txtip12.Text = string.IsNullOrEmpty(txtip12.Text) ? "0" : txtip12.Text)
            + Convert.ToDecimal(txtip22.Text = string.IsNullOrEmpty(txtip22.Text) ? "0" : txtip22.Text)
            + Convert.ToDecimal(txtip32.Text = string.IsNullOrEmpty(txtip32.Text) ? "0" : txtip32.Text)


            + Convert.ToDecimal(txtNc12.Text = string.IsNullOrEmpty(txtNc12.Text) ? "0" : txtNc12.Text)
            + Convert.ToDecimal(txtNc22.Text = string.IsNullOrEmpty(txtNc22.Text) ? "0" : txtNc22.Text)
            + Convert.ToDecimal(txtNc32.Text = string.IsNullOrEmpty(txtNc32.Text) ? "0" : txtNc32.Text)
              + Convert.ToDecimal(txtNc35.Text = string.IsNullOrEmpty(txtNc35.Text) ? "0" : txtNc35.Text)
               + Convert.ToDecimal(txtNc38.Text = string.IsNullOrEmpty(txtNc38.Text) ? "0" : txtNc38.Text)
                 + Convert.ToDecimal(txtNc41.Text = string.IsNullOrEmpty(txtNc41.Text) ? "0" : txtNc41.Text)

            + Convert.ToDecimal(txtcsm12.Text = string.IsNullOrEmpty(txtcsm12.Text) ? "0" : txtcsm12.Text)
            + Convert.ToDecimal(txtcsm22.Text = string.IsNullOrEmpty(txtcsm22.Text) ? "0" : txtcsm22.Text)
            + Convert.ToDecimal(txtpws12.Text = string.IsNullOrEmpty(txtpws12.Text) ? "0" : txtpws12.Text)
            + Convert.ToDecimal(txtpws22.Text = string.IsNullOrEmpty(txtpws22.Text) ? "0" : txtpws22.Text)
            + Convert.ToDecimal(txtpws32.Text = string.IsNullOrEmpty(txtpws32.Text) ? "0" : txtpws32.Text)

           + Convert.ToDecimal(txtpws35.Text = string.IsNullOrEmpty(txtpws35.Text) ? "0" : txtpws35.Text));
        txtpwA12.Text = Convert.ToString(A1);


        decimal B1;//Convert.ToDecimal(txtct2.Text = string.IsNullOrEmpty(txtct2.Text) ? "0" : txtct2.Text)
        B1 = (Convert.ToDecimal(txtpp11.Text = string.IsNullOrEmpty(txtpp11.Text) ? "0" : txtpp11.Text)
         + Convert.ToDecimal(txtpp21.Text = string.IsNullOrEmpty(txtpp21.Text) ? "0" : txtpp21.Text)
         + Convert.ToDecimal(txtpp31.Text = string.IsNullOrEmpty(txtpp31.Text) ? "0" : txtpp31.Text)
         + Convert.ToDecimal(txtpp41.Text = string.IsNullOrEmpty(txtpp41.Text) ? "0" : txtpp41.Text)
         + Convert.ToDecimal(txtpp51.Text = string.IsNullOrEmpty(txtpp51.Text) ? "0" : txtpp51.Text)
         + Convert.ToDecimal(txtpp61.Text = string.IsNullOrEmpty(txtpp61.Text) ? "0" : txtpp61.Text)

         + Convert.ToDecimal(txtrs11.Text = string.IsNullOrEmpty(txtrs11.Text) ? "0" : txtrs11.Text)
         + Convert.ToDecimal(txtrs21.Text = string.IsNullOrEmpty(txtrs21.Text) ? "0" : txtrs21.Text)
         + Convert.ToDecimal(txtrs31.Text = string.IsNullOrEmpty(txtrs31.Text) ? "0" : txtrs31.Text)
         + Convert.ToDecimal(txtrs41.Text = string.IsNullOrEmpty(txtrs41.Text) ? "0" : txtrs41.Text)


         + Convert.ToDecimal(txtrpp11.Text = string.IsNullOrEmpty(txtrpp11.Text) ? "0" : txtrpp11.Text)
         + Convert.ToDecimal(txtrpp21.Text = string.IsNullOrEmpty(txtrpp21.Text) ? "0" : txtrpp21.Text)
         + Convert.ToDecimal(txtrpp31.Text = string.IsNullOrEmpty(txtrpp31.Text) ? "0" : txtrpp31.Text)


         + Convert.ToDecimal(txtrpro11.Text = string.IsNullOrEmpty(txtrpro11.Text) ? "0" : txtrpro11.Text)
         + Convert.ToDecimal(txtrpro21.Text = string.IsNullOrEmpty(txtrpro21.Text) ? "0" : txtrpro21.Text)
         + Convert.ToDecimal(txtrpro31.Text = string.IsNullOrEmpty(txtrpro31.Text) ? "0" : txtrpro31.Text)
         + Convert.ToDecimal(txtrpro41.Text = string.IsNullOrEmpty(txtrpro41.Text) ? "0" : txtrpro41.Text)
         + Convert.ToDecimal(txtrpro51.Text = string.IsNullOrEmpty(txtrpro51.Text) ? "0" : txtrpro51.Text)
         + Convert.ToDecimal(txtrpro54.Text = string.IsNullOrEmpty(txtrpro54.Text) ? "0" : txtrpro54.Text)

         );




        txtrproT11.Text = Convert.ToString(B1);
        decimal B2;
        B2 = (Convert.ToDecimal(txtpp12.Text = string.IsNullOrEmpty(txtpp11.Text) ? "0" : txtpp12.Text)
         + Convert.ToDecimal(txtpp22.Text = string.IsNullOrEmpty(txtpp22.Text) ? "0" : txtpp22.Text)
         + Convert.ToDecimal(txtpp32.Text = string.IsNullOrEmpty(txtpp32.Text) ? "0" : txtpp32.Text)
         + Convert.ToDecimal(txtpp42.Text = string.IsNullOrEmpty(txtpp42.Text) ? "0" : txtpp42.Text)
         + Convert.ToDecimal(txtpp52.Text = string.IsNullOrEmpty(txtpp52.Text) ? "0" : txtpp52.Text)
         + Convert.ToDecimal(txtpp62.Text = string.IsNullOrEmpty(txtpp62.Text) ? "0" : txtpp62.Text)
         + Convert.ToDecimal(txtrs12.Text = string.IsNullOrEmpty(txtrs12.Text) ? "0" : txtrs12.Text)
         + Convert.ToDecimal(txtrs22.Text = string.IsNullOrEmpty(txtrs22.Text) ? "0" : txtrs22.Text)
         + Convert.ToDecimal(txtrs32.Text = string.IsNullOrEmpty(txtrs32.Text) ? "0" : txtrs32.Text)
         + Convert.ToDecimal(txtrs42.Text = string.IsNullOrEmpty(txtrs42.Text) ? "0" : txtrs42.Text)
         + Convert.ToDecimal(txtrpp12.Text = string.IsNullOrEmpty(txtrpp12.Text) ? "0" : txtrpp12.Text)
         + Convert.ToDecimal(txtrpp22.Text = string.IsNullOrEmpty(txtrpp22.Text) ? "0" : txtrpp22.Text)
         + Convert.ToDecimal(txtrpp32.Text = string.IsNullOrEmpty(txtrpp32.Text) ? "0" : txtrpp32.Text)
         + Convert.ToDecimal(txtrpro12.Text = string.IsNullOrEmpty(txtrpro12.Text) ? "0" : txtrpro12.Text)
         + Convert.ToDecimal(txtrpro22.Text = string.IsNullOrEmpty(txtrpro22.Text) ? "0" : txtrpro22.Text)
         + Convert.ToDecimal(txtrpro32.Text = string.IsNullOrEmpty(txtrpro32.Text) ? "0" : txtrpro32.Text)
         + Convert.ToDecimal(txtrpro42.Text = string.IsNullOrEmpty(txtrpro42.Text) ? "0" : txtrpro42.Text)
         + Convert.ToDecimal(txtrpro52.Text = string.IsNullOrEmpty(txtrpro52.Text) ? "0" : txtrpro52.Text)
          + Convert.ToDecimal(txtrpro55.Text = string.IsNullOrEmpty(txtrpro55.Text) ? "0" : txtrpro55.Text)
         );

        txtrproT12.Text = Convert.ToString(B2);

        Decimal C1;//Convert.ToDecimal(txtct2.Text = string.IsNullOrEmpty(txtct2.Text) ? "0" : txtct2.Text)
        C1 = (Convert.ToDecimal(txtinb11.Text = string.IsNullOrEmpty(txtinb11.Text) ? "0" : txtinb11.Text)
        + Convert.ToDecimal(txtinb21.Text = string.IsNullOrEmpty(txtinb21.Text) ? "0" : txtinb21.Text)
        + Convert.ToDecimal(txtinb31.Text = string.IsNullOrEmpty(txtinb31.Text) ? "0" : txtinb31.Text)
        + Convert.ToDecimal(txtinb41.Text = string.IsNullOrEmpty(txtinb41.Text) ? "0" : txtinb41.Text)
        + Convert.ToDecimal(txtinb51.Text = string.IsNullOrEmpty(txtinb51.Text) ? "0" : txtinb51.Text)
        + Convert.ToDecimal(txtinb61.Text = string.IsNullOrEmpty(txtinb61.Text) ? "0" : txtinb61.Text)
        + Convert.ToDecimal(txtinb71.Text = string.IsNullOrEmpty(txtinb71.Text) ? "0" : txtinb71.Text)
         + Convert.ToDecimal(txtinb74.Text = string.IsNullOrEmpty(txtinb74.Text) ? "0" : txtinb74.Text)
          + Convert.ToDecimal(txtinb77.Text = string.IsNullOrEmpty(txtinb77.Text) ? "0" : txtinb77.Text)

        );



        txtinbT11.Text = Convert.ToString(C1);
        Decimal C2;
        C2 = (Convert.ToDecimal(txtinb12.Text = string.IsNullOrEmpty(txtinb12.Text) ? "0" : txtinb12.Text)
        + Convert.ToDecimal(txtinb22.Text = string.IsNullOrEmpty(txtinb22.Text) ? "0" : txtinb22.Text)
        + Convert.ToDecimal(txtinb32.Text = string.IsNullOrEmpty(txtinb32.Text) ? "0" : txtinb32.Text)
        + Convert.ToDecimal(txtinb42.Text = string.IsNullOrEmpty(txtinb42.Text) ? "0" : txtinb42.Text)
        + Convert.ToDecimal(txtinb52.Text = string.IsNullOrEmpty(txtinb52.Text) ? "0" : txtinb52.Text)
        + Convert.ToDecimal(txtinb62.Text = string.IsNullOrEmpty(txtinb62.Text) ? "0" : txtinb62.Text)
        + Convert.ToDecimal(txtinb72.Text = string.IsNullOrEmpty(txtinb72.Text) ? "0" : txtinb72.Text)
         + Convert.ToDecimal(txtinb75.Text = string.IsNullOrEmpty(txtinb75.Text) ? "0" : txtinb75.Text)
          + Convert.ToDecimal(txtinb78.Text = string.IsNullOrEmpty(txtinb78.Text) ? "0" : txtinb78.Text)

        );



        txtinbT12.Text = Convert.ToString(C2);
        Decimal D1;
        D1 = (Convert.ToDecimal(txtsd11.Text = string.IsNullOrEmpty(txtsd11.Text) ? "0" : txtsd11.Text)
        + Convert.ToDecimal(txtsd21.Text = string.IsNullOrEmpty(txtsd21.Text) ? "0" : txtsd21.Text)
        + Convert.ToDecimal(txtsd31.Text = string.IsNullOrEmpty(txtsd31.Text) ? "0" : txtsd31.Text)
        + Convert.ToDecimal(txtsd41.Text = string.IsNullOrEmpty(txtsd41.Text) ? "0" : txtsd41.Text)
        + Convert.ToDecimal(txtsd51.Text = string.IsNullOrEmpty(txtsd51.Text) ? "0" : txtsd51.Text)
        + Convert.ToDecimal(txtsd61.Text = string.IsNullOrEmpty(txtsd61.Text) ? "0" : txtsd61.Text)
        + Convert.ToDecimal(txtsd71.Text = string.IsNullOrEmpty(txtsd71.Text) ? "0" : txtsd71.Text)
         + Convert.ToDecimal(txtsd74.Text = string.IsNullOrEmpty(txtsd74.Text) ? "0" : txtsd74.Text)
          + Convert.ToDecimal(txtsd77.Text = string.IsNullOrEmpty(txtsd77.Text) ? "0" : txtsd77.Text)
        );
        txtsdT11.Text = Convert.ToString(D1);

        Decimal D2;
        D2 = (Convert.ToDecimal(txtsd12.Text = string.IsNullOrEmpty(txtsd12.Text) ? "0" : txtsd12.Text)
        + Convert.ToDecimal(txtsd22.Text = string.IsNullOrEmpty(txtsd22.Text) ? "0" : txtsd22.Text)
        + Convert.ToDecimal(txtsd32.Text = string.IsNullOrEmpty(txtsd32.Text) ? "0" : txtsd32.Text)
        + Convert.ToDecimal(txtsd42.Text = string.IsNullOrEmpty(txtsd42.Text) ? "0" : txtsd42.Text)
        + Convert.ToDecimal(txtsd52.Text = string.IsNullOrEmpty(txtsd52.Text) ? "0" : txtsd52.Text)
        + Convert.ToDecimal(txtsd62.Text = string.IsNullOrEmpty(txtsd62.Text) ? "0" : txtsd62.Text)
        + Convert.ToDecimal(txtsd72.Text = string.IsNullOrEmpty(txtsd72.Text) ? "0" : txtsd72.Text)
          + Convert.ToDecimal(txtsd75.Text = string.IsNullOrEmpty(txtsd75.Text) ? "0" : txtsd75.Text)
            + Convert.ToDecimal(txtsd78.Text = string.IsNullOrEmpty(txtsd78.Text) ? "0" : txtsd78.Text)
        );
        txtsdT12.Text = Convert.ToString(D2);
        decimal E1;
        E1 = (Convert.ToDecimal(txtpcm11.Text = string.IsNullOrEmpty(txtpcm11.Text) ? "0" : txtpcm11.Text)
            + Convert.ToDecimal(txtpcm21.Text = string.IsNullOrEmpty(txtpcm21.Text) ? "0" : txtpcm21.Text)
            + Convert.ToDecimal(txtpcm31.Text = string.IsNullOrEmpty(txtpcm31.Text) ? "0" : txtpcm31.Text)
            + Convert.ToDecimal(txtpcm41.Text = string.IsNullOrEmpty(txtpcm41.Text) ? "0" : txtpcm41.Text)
            + Convert.ToDecimal(txtpcm51.Text = string.IsNullOrEmpty(txtpcm51.Text) ? "0" : txtpcm51.Text)
            + Convert.ToDecimal(txtpcm61.Text = string.IsNullOrEmpty(txtpcm61.Text) ? "0" : txtpcm61.Text));
        txtpcmT11.Text = Convert.ToString(E1);
        decimal E2;
        E2 = (Convert.ToDecimal(txtpcm12.Text = string.IsNullOrEmpty(txtpcm12.Text) ? "0" : txtpcm12.Text)
            + Convert.ToDecimal(txtpcm22.Text = string.IsNullOrEmpty(txtpcm22.Text) ? "0" : txtpcm22.Text)
            + Convert.ToDecimal(txtpcm32.Text = string.IsNullOrEmpty(txtpcm32.Text) ? "0" : txtpcm32.Text)
            + Convert.ToDecimal(txtpcm42.Text = string.IsNullOrEmpty(txtpcm42.Text) ? "0" : txtpcm42.Text)
            + Convert.ToDecimal(txtpcm52.Text = string.IsNullOrEmpty(txtpcm52.Text) ? "0" : txtpcm52.Text)
            + Convert.ToDecimal(txtpcm62.Text = string.IsNullOrEmpty(txtpcm62.Text) ? "0" : txtpcm62.Text));
        txtpcmT12.Text = Convert.ToString(E2);

        Decimal F1;
        F1 = (Convert.ToDecimal(txtpiis11.Text = string.IsNullOrEmpty(txtpiis11.Text) ? "0" : txtpiis11.Text)
            + Convert.ToDecimal(txtpiis21.Text = string.IsNullOrEmpty(txtpiis21.Text) ? "0" : txtpiis21.Text)
             + Convert.ToDecimal(txtpiis31.Text = string.IsNullOrEmpty(txtpiis31.Text) ? "0" : txtpiis31.Text)
              + Convert.ToDecimal(txtpiis41.Text = string.IsNullOrEmpty(txtpiis41.Text) ? "0" : txtpiis41.Text)
               + Convert.ToDecimal(txtpiis51.Text = string.IsNullOrEmpty(txtpiis51.Text) ? "0" : txtpiis51.Text)
                + Convert.ToDecimal(txtpiis54.Text = string.IsNullOrEmpty(txtpiis54.Text) ? "0" : txtpiis54.Text)
                 + Convert.ToDecimal(txtpiis57.Text = string.IsNullOrEmpty(txtpiis57.Text) ? "0" : txtpiis57.Text)
               );

        txtpiisT11.Text = Convert.ToString(F1);
        Decimal F2;
        F2 = (Convert.ToDecimal(txtpiis12.Text = string.IsNullOrEmpty(txtpiis12.Text) ? "0" : txtpiis12.Text)
            + Convert.ToDecimal(txtpiis22.Text = string.IsNullOrEmpty(txtpiis22.Text) ? "0" : txtpiis22.Text)
             + Convert.ToDecimal(txtpiis32.Text = string.IsNullOrEmpty(txtpiis32.Text) ? "0" : txtpiis32.Text)
              + Convert.ToDecimal(txtpiis42.Text = string.IsNullOrEmpty(txtpiis42.Text) ? "0" : txtpiis42.Text)
               + Convert.ToDecimal(txtpiis52.Text = string.IsNullOrEmpty(txtpiis52.Text) ? "0" : txtpiis52.Text)
                + Convert.ToDecimal(txtpiis55.Text = string.IsNullOrEmpty(txtpiis55.Text) ? "0" : txtpiis55.Text)
                 + Convert.ToDecimal(txtpiis58.Text = string.IsNullOrEmpty(txtpiis58.Text) ? "0" : txtpiis58.Text)
               );
        txtpiisT12.Text = Convert.ToString(F2);
        Decimal G1;
        G1 = (Convert.ToDecimal(txtssc11.Text = string.IsNullOrEmpty(txtssc11.Text) ? "0" : txtssc11.Text)
            + Convert.ToDecimal(txtssc21.Text = string.IsNullOrEmpty(txtssc21.Text) ? "0" : txtssc21.Text)
             + Convert.ToDecimal(txtssc31.Text = string.IsNullOrEmpty(txtssc31.Text) ? "0" : txtssc31.Text)
             + Convert.ToDecimal(txtssc34.Text = string.IsNullOrEmpty(txtssc34.Text) ? "0" : txtssc34.Text)
             + Convert.ToDecimal(txtssc37.Text = string.IsNullOrEmpty(txtssc37.Text) ? "0" : txtssc37.Text)
             );
        txtsscT1.Text = Convert.ToString(G1);
        Decimal G2;
        G2 = (Convert.ToDecimal(txtssc12.Text = string.IsNullOrEmpty(txtssc12.Text) ? "0" : txtssc12.Text)
            + Convert.ToDecimal(txtssc22.Text = string.IsNullOrEmpty(txtssc22.Text) ? "0" : txtssc22.Text)
             + Convert.ToDecimal(txtssc32.Text = string.IsNullOrEmpty(txtssc32.Text) ? "0" : txtssc32.Text)
              + Convert.ToDecimal(txtssc35.Text = string.IsNullOrEmpty(txtssc35.Text) ? "0" : txtssc35.Text)
              + Convert.ToDecimal(txtssc38.Text = string.IsNullOrEmpty(txtssc38.Text) ? "0" : txtssc38.Text)
             );

        txtsscT2.Text = Convert.ToString(G2);

        Decimal H1;
        H1 = (Convert.ToDecimal(txtcsw11.Text)
            + Convert.ToDecimal(txtcsw21.Text)
           + Convert.ToDecimal(txtcsw31.Text)
           + Convert.ToDecimal(txtcsw41.Text)
           + Convert.ToDecimal(txtcsw51.Text));

        txtcswT11.Text = Convert.ToString(H1);
        Decimal H2;
        H2 = (Convert.ToDecimal(txtcsw12.Text)
            + Convert.ToDecimal(txtcsw22.Text)
           + Convert.ToDecimal(txtcsw32.Text)
           + Convert.ToDecimal(txtcsw42.Text)
           + Convert.ToDecimal(txtcsw52.Text));

        txtcswT12.Text = Convert.ToString(H2);


        Decimal GTT2;
        GTT2 = ((Convert.ToDecimal(txtpwA12.Text)
            + Convert.ToDecimal(txtrproT12.Text)
            + Convert.ToDecimal(txtinbT12.Text)
            + Convert.ToDecimal(txtsdT12.Text)
            + Convert.ToDecimal(txtpcmT12.Text))
            + Convert.ToDecimal(txtpiisT12.Text)
            + Convert.ToDecimal(txtsscT2.Text)
            + Convert.ToDecimal(txtcswT12.Text));
        txtGGT12.Text = Convert.ToString(GTT2);

        Decimal GTT1;
        GTT1 = (Convert.ToDecimal(txtpwA11.Text)
            + Convert.ToDecimal(txtrproT11.Text)
            + Convert.ToDecimal(txtinbT11.Text)

            + Convert.ToDecimal(txtsdT11.Text)

            + Convert.ToDecimal(txtpcmT11.Text)
            + Convert.ToDecimal(txtpiisT11.Text)

            + Convert.ToDecimal(txtsscT1.Text)
            + Convert.ToDecimal(txtcswT11.Text));
        txtGGT11.Text = Convert.ToString(GTT1);
    }

    protected void btncheck_Click(object sender, EventArgs e)
    {












        if (txtct1.Text == "" || txtct2.Text == "" ||

        //txtsf11.Text == "" ||
            //txtsf12.Text == "" ||

        //txtsf21.Text == "" ||
            //txtsf22.Text == "" ||

        //txtsf31.Text == "" ||
            //txtsf32.Text == "" ||

        //txtsf41.Text == "" ||
            //txtsf42.Text == "" ||

        //txtsf51.Text == "" ||
            //txtsf52.Text == "" ||

        //txtsf61.Text == "" ||
            //txtsf62.Text == "" ||

        //txtsf64.Text == "" ||
            //txtsf65.Text == "" ||

        //txtsf67.Text == "" ||
            //txtsf68.Text == "" ||

        //txtsf70.Text == "" ||
            //txtsf71.Text == "" ||

        //txtsf73.Text == "" ||
            //txtsf74.Text == "" ||

        //txtsf76.Text == "" ||
            //txtsf77.Text == "" ||


        //txtsf79.Text == "" ||
            //txtsf80.Text == "" ||




        txtip11.Text == "" ||
        txtip12.Text == "" ||
        txtip21.Text == "" ||
        txtip22.Text == "" ||

        txtip31.Text == "" ||
        txtip32.Text == "" ||

        txtNc11.Text == "" || txtNc12.Text == "" ||

        txtNc21.Text == "" ||
        txtNc22.Text == "" ||

        txtNc31.Text == "" ||
        txtNc32.Text == "" ||

         txtNc34.Text == "" ||
        txtNc35.Text == "" ||

         txtNc37.Text == "" ||
        txtNc38.Text == "" ||

         txtNc40.Text == "" ||
        txtNc41.Text == "" ||

        txtcsm11.Text == "" ||
        txtcsm12.Text == "" ||

        txtcsm21.Text == "" ||
        txtcsm22.Text == "" ||

        txtpws11.Text == "" ||
        txtpws12.Text == "" ||

        txtpws21.Text == "" ||
        txtpws22.Text == "" ||

        txtpws31.Text == "" ||
        txtpws32.Text == "" ||

         txtpws34.Text == "" ||
        txtpws35.Text == "" ||


        txtpp11.Text == "" ||
        txtpp12.Text == "" ||

        txtpp21.Text == "" ||
        txtpp22.Text == "" ||

        txtpp31.Text == "" ||
        txtpp32.Text == "" ||

        txtpp41.Text == "" ||
        txtpp42.Text == "" ||

        txtpp51.Text == "" ||
        txtpp52.Text == "" ||

        txtpp61.Text == "" ||
        txtpp62.Text == "" ||

        txtrs11.Text == "" ||
        txtrs12.Text == "" ||

        txtrs21.Text == "" ||
        txtrs22.Text == "" ||

        txtrs31.Text == "" ||
        txtrs32.Text == "" ||

        txtrs41.Text == "" ||
        txtrs42.Text == "" ||

        txtrpp11.Text == "" ||
        txtrpp12.Text == "" ||

        txtrpp21.Text == "" ||
        txtrpp22.Text == "" ||

        txtrpp31.Text == "" ||
        txtrpp32.Text == "" ||

        txtrpro11.Text == "" ||
        txtrpro12.Text == "" ||

        txtrpro21.Text == "" ||
        txtrpro22.Text == "" ||

        txtrpro31.Text == "" ||
        txtrpro32.Text == "" ||

        txtrpro41.Text == "" ||
        txtrpro42.Text == "" ||

        txtrpro51.Text == "" ||
        txtrpro52.Text == "" ||

         txtrpro54.Text == "" ||
        txtrpro55.Text == "" ||


        txtinb11.Text == "" ||
        txtinb12.Text == "" ||

        txtinb21.Text == "" ||
        txtinb22.Text == "" ||

        txtinb31.Text == "" ||
        txtinb32.Text == "" ||

        txtinb41.Text == "" ||
        txtinb42.Text == "" ||

        txtinb51.Text == "" ||
        txtinb52.Text == "" ||

        txtinb61.Text == "" ||
        txtinb62.Text == "" ||

        txtinb71.Text == "" ||
        txtinb72.Text == "" ||


        txtinb74.Text == "" ||
        txtinb75.Text == "" ||


        txtinb77.Text == "" ||
        txtinb78.Text == "" ||

        txtsd11.Text == "" ||
        txtsd12.Text == "" ||

        txtsd21.Text == "" ||
        txtsd22.Text == "" ||

        txtsd31.Text == "" ||
        txtsd32.Text == "" ||

        txtsd41.Text == "" ||
        txtsd42.Text == "" ||

        txtsd51.Text == "" ||
        txtsd52.Text == "" ||

        txtsd61.Text == "" ||
        txtsd62.Text == "" ||

        txtsd71.Text == "" ||
        txtsd72.Text == "" ||

         txtsd74.Text == "" ||
        txtsd75.Text == "" ||
         txtsd77.Text == "" ||
        txtsd78.Text == "" ||

        txtpcm11.Text == "" ||
        txtpcm12.Text == "" ||

        txtpcm21.Text == "" ||
        txtpcm22.Text == "" ||

        txtpcm31.Text == "" ||
        txtpcm32.Text == "" ||

        txtpcm41.Text == "" ||
        txtpcm42.Text == "" ||

        txtpcm51.Text == "" ||
        txtpcm52.Text == "" ||

        txtpcm61.Text == "" ||
        txtpcm62.Text == "" ||


        txtpiis11.Text == "" ||
        txtpiis12.Text == "" ||

        txtpiis21.Text == "" ||
        txtpiis22.Text == "" ||

        txtpiis31.Text == "" ||
        txtpiis32.Text == "" ||

        txtpiis41.Text == "" ||
        txtpiis42.Text == "" ||

        txtpiis51.Text == "" ||
        txtpiis52.Text == "" ||

        txtpiis54.Text == "" ||
        txtpiis55.Text == "" ||

        txtpiis57.Text == "" ||
        txtpiis58.Text == "" ||

        txtssc11.Text == "" ||
        txtssc12.Text == "" ||

        txtssc21.Text == "" ||
        txtssc22.Text == "" ||

        txtssc31.Text == "" ||
        txtssc32.Text == "" ||
        txtssc34.Text == "" ||
        txtssc35.Text == "" ||
        txtssc37.Text == "" ||
        txtssc38.Text == "" ||


        txtcsw11.Text == "" ||
        txtcsw12.Text == "" ||

        txtcsw21.Text == "" ||
        txtcsw22.Text == "" ||

        txtcsw31.Text == "" ||
        txtcsw32.Text == "" ||

        txtcsw41.Text == "" ||
        txtcsw42.Text == "" ||

        txtcsw51.Text == "" ||
        txtcsw52.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You could not fill blank')", true);
        }
        else
        {

            GT();

            submit(2);
            bindpms();
            EnableF();
            Checkpms();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Appraisal Form Submitted')", true);
        }

    }


    public void Update()
    {
        try
        {

            con.Open();
            SqlCommand cmd = new SqlCommand("PmsSubmit", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Fid", Session["uid"].ToString());

            if (lblAcad.Visible == true)
            {
                cmd.Parameters.AddWithValue("@AcademicYear", lblAcad.Text);

            }
            else
            {
                cmd.Parameters.AddWithValue("@AcademicYear", Label1.Text);

            }
            cmd.Parameters.AddWithValue("@txtct1", txtct1.Text);
            cmd.Parameters.AddWithValue("@txtct2", txtct2.Text);

            cmd.Parameters.AddWithValue("@txtsf11", txtsf11.Text);
            cmd.Parameters.AddWithValue("@txtsf12", txtsf12.Text);

            cmd.Parameters.AddWithValue("@txtsf21", txtsf21.Text);
            cmd.Parameters.AddWithValue("@txtsf22", txtsf22.Text);

            cmd.Parameters.AddWithValue("@txtsf31", txtsf31.Text);
            cmd.Parameters.AddWithValue("@txtsf32", txtsf32.Text);

            cmd.Parameters.AddWithValue("@txtsf41", txtsf41.Text);
            cmd.Parameters.AddWithValue("@txtsf42", txtsf42.Text);

            cmd.Parameters.AddWithValue("@txtsf51", txtsf51.Text);
            cmd.Parameters.AddWithValue("@txtsf52", txtsf52.Text);

            cmd.Parameters.AddWithValue("@txtsf61", txtsf61.Text);
            cmd.Parameters.AddWithValue("@txtsf62", txtsf62.Text);

            cmd.Parameters.AddWithValue("@txtip11", txtip11.Text);
            cmd.Parameters.AddWithValue("@txtip12", txtip12.Text);

            cmd.Parameters.AddWithValue("@txtip21", txtip21.Text);
            cmd.Parameters.AddWithValue("@txtip22", txtip22.Text);

            cmd.Parameters.AddWithValue("@txtip31", txtip31.Text);
            cmd.Parameters.AddWithValue("@txtip32", txtip32.Text);

            cmd.Parameters.AddWithValue("@txtNc11", txtNc11.Text);
            cmd.Parameters.AddWithValue("@txtNc12", txtNc12.Text);
            cmd.Parameters.AddWithValue("@txtNc21", txtNc21.Text);
            cmd.Parameters.AddWithValue("@txtNc22", txtNc22.Text);

            cmd.Parameters.AddWithValue("@txtNc31", txtNc31.Text);
            cmd.Parameters.AddWithValue("@txtNc32", txtNc32.Text);

            cmd.Parameters.AddWithValue("@txtcsm11", txtcsm11.Text);
            cmd.Parameters.AddWithValue("@txtcsm12", txtcsm12.Text);

            cmd.Parameters.AddWithValue("@txtcsm21", txtcsm21.Text);
            cmd.Parameters.AddWithValue("@txtcsm22", txtcsm22.Text);

            cmd.Parameters.AddWithValue("@txtpws11", txtpws11.Text);
            cmd.Parameters.AddWithValue("@txtpws12", txtpws12.Text);

            cmd.Parameters.AddWithValue("@txtpws21", txtpws21.Text);
            cmd.Parameters.AddWithValue("@txtpws22", txtpws22.Text);

            cmd.Parameters.AddWithValue("@txtpws31", txtpws31.Text);
            cmd.Parameters.AddWithValue("@txtpws32", txtpws32.Text);

            cmd.Parameters.AddWithValue("@txtpwA11", txtpwA11.Text);
            if (Convert.ToInt32(txtpwA12.Text) > 160)
            {
                txtpwA12.Text = "160";
            }
            cmd.Parameters.AddWithValue("@txtpwA12", txtpwA12.Text);

            cmd.Parameters.AddWithValue("@txtpp11", txtpp11.Text);
            cmd.Parameters.AddWithValue("@txtpp12", txtpp12.Text);

            cmd.Parameters.AddWithValue("@txtpp21", txtpp21.Text);
            cmd.Parameters.AddWithValue("@txtpp22", txtpp22.Text);

            cmd.Parameters.AddWithValue("@txtpp31", txtpp31.Text);
            cmd.Parameters.AddWithValue("@txtpp32", txtpp32.Text);

            cmd.Parameters.AddWithValue("@txtpp41", txtpp41.Text);
            cmd.Parameters.AddWithValue("@txtpp42", txtpp42.Text);

            cmd.Parameters.AddWithValue("@txtpp51", txtpp51.Text);
            cmd.Parameters.AddWithValue("@txtpp52", txtpp52.Text);

            cmd.Parameters.AddWithValue("@txtpp61", txtpp61.Text);
            cmd.Parameters.AddWithValue("@txtpp62", txtpp62.Text);

            cmd.Parameters.AddWithValue("@txtrs11", txtrs11.Text);
            cmd.Parameters.AddWithValue("@txtrs12", txtrs12.Text);
            cmd.Parameters.AddWithValue("@txtrs21", txtrs21.Text);
            cmd.Parameters.AddWithValue("@txtrs22", txtrs22.Text);

            cmd.Parameters.AddWithValue("@txtrs31", txtrs31.Text);
            cmd.Parameters.AddWithValue("@txtrs32", txtrs32.Text);

            cmd.Parameters.AddWithValue("@txtrs41", txtrs41.Text);
            cmd.Parameters.AddWithValue("@txtrs42", txtrs42.Text);

            cmd.Parameters.AddWithValue("@txtrpp11", txtrpp11.Text);
            cmd.Parameters.AddWithValue("@txtrpp12", txtrpp12.Text);

            cmd.Parameters.AddWithValue("@txtrpp21", txtrpp21.Text);
            cmd.Parameters.AddWithValue("@txtrpp22", txtrpp22.Text);

            cmd.Parameters.AddWithValue("@txtrpp31", txtrpp31.Text);
            cmd.Parameters.AddWithValue("@txtrpp32", txtrpp32.Text);

            cmd.Parameters.AddWithValue("@txtrpro11", txtrpro11.Text);
            cmd.Parameters.AddWithValue("@txtrpro12", txtrpro12.Text);

            cmd.Parameters.AddWithValue("@txtrpro21", txtrpro21.Text);
            cmd.Parameters.AddWithValue("@txtrpro22", txtrpro22.Text);

            cmd.Parameters.AddWithValue("@txtrpro31", txtrpro31.Text);
            cmd.Parameters.AddWithValue("@txtrpro32", txtrpro32.Text);

            cmd.Parameters.AddWithValue("@txtrpro41", txtrpro41.Text);
            cmd.Parameters.AddWithValue("@txtrpro42", txtrpro42.Text);

            cmd.Parameters.AddWithValue("@txtrpro51", txtrpro51.Text);
            cmd.Parameters.AddWithValue("@txtrpro52", txtrpro52.Text);

            cmd.Parameters.AddWithValue("@txtrproT11", txtrproT11.Text);
            if (Convert.ToInt32(txtrproT12.Text) > 60)
            {
                txtrproT12.Text = "60";
            }
            cmd.Parameters.AddWithValue("@txtrproT12", txtrproT12.Text);

            cmd.Parameters.AddWithValue("@txtinb11", txtinb11.Text);
            cmd.Parameters.AddWithValue("@txtinb12", txtinb12.Text);

            cmd.Parameters.AddWithValue("@txtinb21", txtinb21.Text);
            cmd.Parameters.AddWithValue("@txtinb22", txtinb22.Text);

            cmd.Parameters.AddWithValue("@txtinb31", txtinb31.Text);
            cmd.Parameters.AddWithValue("@txtinb32", txtinb32.Text);

            cmd.Parameters.AddWithValue("@txtinb41", txtinb41.Text);
            cmd.Parameters.AddWithValue("@txtinb42", txtinb42.Text);

            cmd.Parameters.AddWithValue("@txtinb51", txtinb51.Text);
            cmd.Parameters.AddWithValue("@txtinb52", txtinb52.Text);

            cmd.Parameters.AddWithValue("@txtinb61", txtinb61.Text);
            cmd.Parameters.AddWithValue("@txtinb62", txtinb62.Text);

            cmd.Parameters.AddWithValue("@txtinb71", txtinb71.Text);
            cmd.Parameters.AddWithValue("@txtinb72", txtinb72.Text);

            cmd.Parameters.AddWithValue("@txtinbT11", txtinbT11.Text);
            if (Convert.ToInt32(txtinbT12.Text) > 20)
            {
                txtinbT12.Text = "20";
            }
            cmd.Parameters.AddWithValue("@txtinbT12", txtinbT12.Text);

            cmd.Parameters.AddWithValue("@txtsd11", txtsd11.Text);
            cmd.Parameters.AddWithValue("@txtsd12", txtsd12.Text);

            cmd.Parameters.AddWithValue("@txtsd21", txtsd21.Text);
            cmd.Parameters.AddWithValue("@txtsd22", txtsd22.Text);

            cmd.Parameters.AddWithValue("@txtsd31", txtsd31.Text);
            cmd.Parameters.AddWithValue("@txtsd32", txtsd32.Text);

            cmd.Parameters.AddWithValue("@txtsd41", txtsd41.Text);
            cmd.Parameters.AddWithValue("@txtsd42", txtsd42.Text);

            cmd.Parameters.AddWithValue("@txtsd51", txtsd51.Text);
            cmd.Parameters.AddWithValue("@txtsd52", txtsd52.Text);

            cmd.Parameters.AddWithValue("@txtsd61", txtsd61.Text);
            cmd.Parameters.AddWithValue("@txtsd62", txtsd62.Text);

            cmd.Parameters.AddWithValue("@txtsd71", txtsd71.Text);
            cmd.Parameters.AddWithValue("@txtsd72", txtsd72.Text);

            cmd.Parameters.AddWithValue("@txtsdT11", txtsdT11.Text);
            if (Convert.ToInt32(txtsdT12.Text) > 15)
            {
                txtsdT12.Text = "15";
            }
            cmd.Parameters.AddWithValue("@txtsdT12", txtsdT12.Text);

            cmd.Parameters.AddWithValue("@txtpcm11", txtpcm11.Text);
            cmd.Parameters.AddWithValue("@txtpcm12", txtpcm12.Text);

            cmd.Parameters.AddWithValue("@txtpcm21", txtpcm21.Text);
            cmd.Parameters.AddWithValue("@txtpcm22", txtpcm22.Text);

            cmd.Parameters.AddWithValue("@txtpcm31", txtpcm31.Text);
            cmd.Parameters.AddWithValue("@txtpcm32", txtpcm32.Text);

            cmd.Parameters.AddWithValue("@txtpcm41", txtpcm41.Text);
            cmd.Parameters.AddWithValue("@txtpcm42", txtpcm42.Text);

            cmd.Parameters.AddWithValue("@txtpcm51", txtpcm51.Text);
            cmd.Parameters.AddWithValue("@txtpcm52", txtpcm52.Text);

            cmd.Parameters.AddWithValue("@txtpcm61", txtpcm61.Text);
            cmd.Parameters.AddWithValue("@txtpcm62", txtpcm62.Text);

            cmd.Parameters.AddWithValue("@txtpcmT11", txtpcmT11.Text);
            if (Convert.ToInt32(txtpcmT12.Text) > 20)
            {
                txtpcmT12.Text = "20";
            }

            cmd.Parameters.AddWithValue("@txtpcmT12", txtpcmT12.Text);

            cmd.Parameters.AddWithValue("@txtpiis11", txtpiis11.Text);
            cmd.Parameters.AddWithValue("@txtpiis12", txtpiis12.Text);

            cmd.Parameters.AddWithValue("@txtpiis21", txtpiis21.Text);
            cmd.Parameters.AddWithValue("@txtpiis22", txtpiis22.Text);

            cmd.Parameters.AddWithValue("@txtpiis31", txtpiis31.Text);
            cmd.Parameters.AddWithValue("@txtpiis32", txtpiis32.Text);

            cmd.Parameters.AddWithValue("@txtpiis41", txtpiis41.Text);
            cmd.Parameters.AddWithValue("@txtpiis42", txtpiis42.Text);

            cmd.Parameters.AddWithValue("@txtpiis51", txtpiis51.Text);
            cmd.Parameters.AddWithValue("@txtpiis52", txtpiis52.Text);

            cmd.Parameters.AddWithValue("@txtpiisT11", txtpiisT11.Text);
            if (Convert.ToInt32(txtpiisT12.Text) > 10)
            {
                txtpiisT12.Text = "10";
            }
            cmd.Parameters.AddWithValue("@txtpiisT12", txtpiisT12.Text);

            cmd.Parameters.AddWithValue("@txtssc11", txtssc11.Text);
            cmd.Parameters.AddWithValue("@txtssc12", txtssc12.Text);

            cmd.Parameters.AddWithValue("@txtssc21", txtssc21.Text);
            cmd.Parameters.AddWithValue("@txtssc22", txtssc22.Text);

            cmd.Parameters.AddWithValue("@txtssc31", txtssc31.Text);
            cmd.Parameters.AddWithValue("@txtssc32", txtssc32.Text);

            cmd.Parameters.AddWithValue("@txtsscT1", txtsscT1.Text);
            if (Convert.ToInt32(txtsscT2.Text) > 10)
            {
                txtsscT2.Text = "10";
            }
            cmd.Parameters.AddWithValue("@txtsscT2", txtsscT2.Text);

            cmd.Parameters.AddWithValue("@txtcsw11", txtcsw11.Text);
            cmd.Parameters.AddWithValue("@txtcsw12", txtcsw12.Text);

            cmd.Parameters.AddWithValue("@txtcsw21", txtcsw21.Text);
            cmd.Parameters.AddWithValue("@txtcsw22", txtcsw22.Text);

            cmd.Parameters.AddWithValue("@txtcsw31", txtcsw31.Text);
            cmd.Parameters.AddWithValue("@txtcsw32", txtcsw32.Text);

            cmd.Parameters.AddWithValue("@txtcsw41", txtcsw41.Text);
            cmd.Parameters.AddWithValue("@txtcsw42", txtcsw42.Text);

            cmd.Parameters.AddWithValue("@txtcsw51", txtcsw51.Text);
            cmd.Parameters.AddWithValue("@txtcsw52", txtcsw52.Text);

            cmd.Parameters.AddWithValue("@txtcswT11", txtcswT11.Text);
            if (Convert.ToInt32(txtcswT12.Text) > 10)
            {
                txtcswT12.Text = "10";
            }
            cmd.Parameters.AddWithValue("@txtcswT12", txtcswT12.Text);

            cmd.Parameters.AddWithValue("@txtGGT11", txtGGT11.Text);
            cmd.Parameters.AddWithValue("@txtGGT12", txtGGT12.Text);


            cmd.Parameters.AddWithValue("@Status", "1");


            cmd.ExecuteNonQuery();
            con.Close();

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data saved successfully')", true);

            bindpms();


        }
        catch
        {
        }


    }


    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string filename = Path.GetFileName(UploadFile.PostedFile.FileName);
        string contentType = UploadFile.PostedFile.ContentType;
        using (Stream fs = UploadFile.PostedFile.InputStream)
        {
            using (BinaryReader br = new BinaryReader(fs))
            {
                byte[] bytes = br.ReadBytes((Int32)fs.Length);
                using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
                {
                    SqlCommand cmd = new SqlCommand("proc_InsertPMSAttachment", con2);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
                    cmd.Parameters.Add("@AcademicYear", Label1.Text);
                    cmd.Parameters.Add("@Attachment", bytes);
                    cmd.Parameters.Add("@AttachmentType", contentType);
                    cmd.Parameters.Add("@FileName", filename);
                    if (con2.State == ConnectionState.Closed)
                        con2.Open();
                    cmd.ExecuteNonQuery();
                    con2.Close();



                }
            }
        }
        bindAttachment();
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

    protected void DeleteFile(object sender, EventArgs e)
    {
        string Eval = (sender as LinkButton).CommandArgument;
        string AssignmentNo = (sender as LinkButton).CommandArgument;


        using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
        {
            using (SqlCommand cmd2 = new SqlCommand())
            {
                cmd2.CommandText = "delete from tbl_PMSAttachment where ID='" + AssignmentNo + "' ";
                cmd2.Connection = con2;
                con2.Open();
                cmd2.ExecuteNonQuery();
                con2.Close();
            }
        }
        bindAttachment();
    }
    protected void drpAcademic_SelectedIndexChanged(object sender, EventArgs e)
    {
        point();

        bindpms();
        Checkpms();
        SubmitTrufalse();
        bindAttachment();
    }
}