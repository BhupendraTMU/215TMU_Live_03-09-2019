using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_BodyDonors : System.Web.UI.Page
{
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    string cs = ConfigurationManager.ConnectionStrings["TMUCON"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid();
        }
    }

    // SAVE DONOR
  protected void btnSave_Click(object sender, EventArgs e)
{
        if (!Page.IsValid)
            return;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString()))
    {
        con.Open();

        SqlTransaction trans = con.BeginTransaction();

        try
        {
            int donorId = 0;

            // =========================
            // INSERT DONOR + GET ID
            // =========================
            using (SqlCommand cmd = new SqlCommand(@"
            INSERT INTO BodyDonors
            (Name, FatherOrHusbandName, DOB, Age, Gender, Address, Mobile, Email, Religion, AadhaarNumber,CreatedBy)
            OUTPUT INSERTED.Id
            VALUES
            (@Name, @Father, @DOB, @Age, @Gender, @Address, @Mobile, @Email, @Religion, @Aadhaar,@CreatedBy)
            ", con, trans))
            {
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@Father", txtFather.Text);
                cmd.Parameters.AddWithValue("@DOB", string.IsNullOrEmpty(txtDOB.Text) ? (object)DBNull.Value : Convert.ToDateTime(txtDOB.Text));
                cmd.Parameters.AddWithValue("@Age", string.IsNullOrEmpty(txtAge.Text) ? (object)DBNull.Value : Convert.ToInt32(txtAge.Text));
                cmd.Parameters.AddWithValue("@Gender", ddlGender.SelectedValue);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Religion", txtReligion.Text);
                cmd.Parameters.AddWithValue("@Aadhaar", txtAadhaar.Text);
                cmd.Parameters.AddWithValue("@CreatedBy", Session["uid"].ToString() ?? "Admin");

                    donorId = Convert.ToInt32(cmd.ExecuteScalar());
            }

            // =========================
            // INSERT WITNESS 1
            // =========================
            using (SqlCommand cmd = new SqlCommand(@"
            INSERT INTO BodyDonorsWitnesses
            (DonorId, WitnessType, Name, Address, Relationship, MobileOrEmail, AadhaarNumber)
            VALUES
            (@DonorId, 'Witness1', @Name, @Address, @Relation, @Mobile, @Aadhaar)
            ", con, trans))
            {
                cmd.Parameters.AddWithValue("@DonorId", donorId);
                cmd.Parameters.AddWithValue("@Name", txtW1Name.Text);
                cmd.Parameters.AddWithValue("@Address", txtW1Address.Text);
                cmd.Parameters.AddWithValue("@Relation", txtW1Relation.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtW1Mobile.Text);
                cmd.Parameters.AddWithValue("@Aadhaar", txtW1Aadhaar.Text);

                cmd.ExecuteNonQuery();
            }

            // =========================
            // INSERT NEXT OF KIN
            // =========================
            using (SqlCommand cmd = new SqlCommand(@"
            INSERT INTO BodyDonorsWitnesses
            (DonorId, WitnessType, Name, Address, Relationship, MobileOrEmail, AadhaarNumber)
            VALUES
            (@DonorId, 'NextOfKin', @Name, @Address, @Relation, @Mobile, @Aadhaar)
            ", con, trans))
            {
                cmd.Parameters.AddWithValue("@DonorId", donorId);
                cmd.Parameters.AddWithValue("@Name", txtKinName.Text);
                cmd.Parameters.AddWithValue("@Address", txtKinAddress.Text);
                cmd.Parameters.AddWithValue("@Relation", txtKinRelation.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtKinMobile.Text);
                cmd.Parameters.AddWithValue("@Aadhaar", txtKinAadhaar.Text);

                cmd.ExecuteNonQuery();
            }

            // =========================
            // COMMIT
            // =========================
            trans.Commit();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                "alert('Donor + Witness Saved Successfully'); window.location='BodyDonors.aspx';", true);
        }
        catch (Exception ex)
        {
            trans.Rollback();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                "alert('Error: " + ex.Message.Replace("'", "") + "');", true);
        }
    }
}

    // GRID BIND
    private void BindGrid()
    {
        SqlDataAdapter da = new SqlDataAdapter(
            "SELECT * FROM BodyDonors ORDER BY DonorId DESC", con1);

        DataTable dt = new DataTable();
        da.Fill(dt);

        grdDonors.DataSource = dt;
        grdDonors.DataBind();
    }
    protected void grdDonors_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDonors.PageIndex = e.NewPageIndex;

        BindGrid(); // 🔥 data reload
    }
    // EXPORT

    protected void btnexporttoexcel_Click(object sender, EventArgs e)
    {
        bool isPaging = grdDonors.AllowPaging;

        grdDonors.AllowPaging = false;

        DataTable dt;

        // 🔥 Check if search data exists
        if (ViewState["SearchData"] != null)
        {
            dt = (DataTable)ViewState["SearchData"];
        }
        else
        {
            // fallback full data
            SqlDataAdapter da = new SqlDataAdapter(@"
        SELECT  [Id]
      ,[DonorNumber]
      ,[DonorId]
      ,[Name]
      ,[FatherOrHusbandName]
      ,[DOB]
      ,[Age]
      ,[Gender]
      ,[Address]
      ,[Mobile]
      ,[Email]
      ,[Religion]
      ,[AadhaarNumber]
      ,[IsActive]
      ,[CreatedAt]
      ,[CreatedBy]
  FROM [EDUCOLLEGELIVE-R2].[dbo].[BodyDonors] C
        ORDER BY C.CreatedAt DESC", con1);

            dt = new DataTable();
            da.Fill(dt);
        }

        grdDonors.DataSource = dt;
        grdDonors.DataBind();

        // 🔥 Hide Action column
        grdDonors.Columns[grdDonors.Columns.Count - 1].Visible = false;
        grdDonors.PagerSettings.Visible = false;

        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=CadaverList.xls");
        Response.ContentType = "application/vnd.ms-excel";

        System.IO.StringWriter sw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);

        grdDonors.RenderControl(hw);

        Response.Write(sw.ToString());
        Response.End();

        // Restore
        grdDonors.AllowPaging = isPaging;
        grdDonors.Columns[grdDonors.Columns.Count - 1].Visible = true;
        grdDonors.PagerSettings.Visible = true;

        BindGrid();
    }
    public override void VerifyRenderingInServerForm(Control control) { }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(cs))
        {
            string search = txtDonorNo.Text.Trim();

            string query = @"
        SELECT Id, DonorId, Name, Mobile,FatherOrHusbandName, Gender, CreatedAt 
        FROM BodyDonors
        WHERE 
            (@search = '' 
            OR DonorId LIKE '%' + @search + '%' 
            OR Name LIKE '%' + @search + '%')
        ORDER BY DonorId DESC";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@search", search);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ViewState["SearchData"] = dt;
            grdDonors.DataSource = dt;
            grdDonors.DataBind();
        }
    }

    protected void grdDonors_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewDonor")
        {
            int donorId = Convert.ToInt32(e.CommandArgument);

            LoadDonor(donorId);

            // 🔥 Modal open karne ke liye JS call
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                "Popup", "$('#donorModal').modal('show');", true);
        }
    }

    private void LoadDonor(int id)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());

        con.Open();

        // 🔹 Donor Data
        SqlCommand cmd = new SqlCommand("SELECT * FROM BodyDonors WHERE Id=@Id", con);
        cmd.Parameters.AddWithValue("@Id", id);

        SqlDataReader dr = cmd.ExecuteReader();

        if (dr.Read())
        {
            lblName.Text = dr["Name"].ToString();
            lblFather.Text = dr["FatherOrHusbandName"].ToString();
            if (dr["DOB"] != DBNull.Value && !string.IsNullOrEmpty(dr["DOB"].ToString()))
            {
                lblDOB.Text = Convert.ToDateTime(dr["DOB"]).ToString("dd-MM-yyyy");
            }
            else
            {
                lblDOB.Text = ""; // ya "N/A"
            }
            lblAge.Text = dr["Age"].ToString();
            lblGender.Text = dr["Gender"].ToString();
            lblAddress.Text = dr["Address"].ToString();
            lblMobile.Text = dr["Mobile"].ToString();
            lblEmail.Text = dr["Email"].ToString();
            lblReligion.Text = dr["Religion"].ToString();
            lblAadhaar.Text = dr["AadhaarNumber"].ToString();
        }
        dr.Close();

        // 🔹 Witness Data
        SqlCommand cmdW = new SqlCommand("SELECT * FROM BodyDonorsWitnesses WHERE DonorId=@Id", con);
        cmdW.Parameters.AddWithValue("@Id", id);

        SqlDataReader drW = cmdW.ExecuteReader();

        int i = 1;
        while (drW.Read())
        {
            if (i == 1)
            {
                lblW1Name.Text = drW["Name"].ToString();
                lblW1Address.Text = drW["Address"].ToString();
                lblW1Relation.Text = drW["Relationship"].ToString();
                lblW1Mobile.Text = drW["MobileOrEmail"].ToString();
                lblW1Aadhaar.Text = drW["AadhaarNumber"].ToString();
            }
            else if (i == 2)
            {
                lblW2Name.Text = drW["Name"].ToString();
                lblW2Address.Text = drW["Address"].ToString();
                lblW2Relation.Text = drW["Relationship"].ToString();
                lblW2Mobile.Text = drW["MobileOrEmail"].ToString();
                lblW2Aadhaar.Text = drW["AadhaarNumber"].ToString();
            }
            i++;
        }

        con.Close();
    }
}