using System;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Faculty_DentalFacultyPunch : System.Web.UI.Page
{
    string conStr = ConfigurationManager.AppSettings["str"];

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtDate.Text = DateTime.Today.ToString("yyyy-MM-dd");

            LoadFacultyPunch();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadFacultyPunch();
    }

    private void LoadFacultyPunch()
    {
        lblMessage.Text = "";

        DateTime selectedDate;

        if (!DateTime.TryParse(txtDate.Text, out selectedDate))
        {
            lblMessage.Text = "Please select a valid date.";
            gvFacultyPunch.DataSource = null;
            gvFacultyPunch.DataBind();
            return;
        }

        try
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                using (SqlCommand cmd = new SqlCommand(
                    "GetdentalFacultyPunch", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Date", SqlDbType.Date)
                                  .Value = selectedDate.Date;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();

                        da.Fill(dt);

                        gvFacultyPunch.DataSource = dt;
                        gvFacultyPunch.DataBind();

                        lblReportDate.Text =
                            selectedDate.ToString("dd-MM-yyyy");

                        if (dt.Rows.Count == 0)
                        {
                            lblMessage.Text =
                                "No faculty record found.";
                        }
                        else
                        {
                            lblMessage.Text =
                                dt.Rows.Count + " faculty record(s) found.";
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;

            gvFacultyPunch.DataSource = null;
            gvFacultyPunch.DataBind();
        }
    }
}