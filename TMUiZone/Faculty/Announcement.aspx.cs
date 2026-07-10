using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_Announcement : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(
        ConfigurationManager.ConnectionStrings["TMUCON"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            BindAnnouncementGrid();
            bindDrpCollageList();
        }
    }

    public void bindDrpCollageList()
    {
        SqlCommand cmd = new SqlCommand("proc_GetCollageFromHOD", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@UserID", Session["uid"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Open();
        da.Fill(dt);
        con.Close();

        // Because we added a static first item in markup, keep it by using AppendDataBoundItems
        drpCollage.DataSource = dt;
        drpCollage.DataTextField = "Details";
        drpCollage.DataValueField = "No_";
        drpCollage.DataBind();
    }
    public void bindDrpCourseList()
    {
        SqlCommand cmd = new SqlCommand("proc_GetCourseListFromCollege", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@UserID", Session["uid"].ToString());
        cmd.Parameters.Add("@CollegeCode", drpCollage.SelectedValue);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Open();
        da.Fill(dt);
        con.Close();
        drpCourse.DataSource = dt;
        drpCourse.DataTextField = "Details";
        drpCourse.DataValueField = "No_";
        drpCourse.DataBind();
    }

    protected void drpCollage_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindDrpCourseList();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        // Ensure ASP.NET validators ran
        if (!Page.IsValid) return;

        try
        {
            // Upload file once
            string filePath = "";
            if (fuAttachment.HasFile)
            {
                string fileName = System.IO.Path.GetFileName(fuAttachment.PostedFile.FileName);
                fuAttachment.SaveAs(Server.MapPath("~/Uploads/Announcement/" + fileName));
                filePath = "~/Uploads/Announcement/" + fileName;
            }

            con.Open();

            // 🔁 LOOP THROUGH SELECTED COURSES
            foreach (ListItem item in drpCourse.Items)
            {
                if (item.Selected)
                {
                    SqlCommand cmd = new SqlCommand("Sp_InsertAnnouncement", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Title", txtTitle.Text.Trim());
                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text.Trim());
                    cmd.Parameters.AddWithValue("@AnnouncementDate", txtDate.Text.Trim());

                    cmd.Parameters.AddWithValue("@CollegeID", drpCollage.SelectedValue);
                    cmd.Parameters.AddWithValue("@CourseIDs", item.Value); // 🔥 SINGLE COURSE
                    cmd.Parameters.AddWithValue("@Semester", drpSemester.SelectedValue);
                    cmd.Parameters.AddWithValue("@FilePath", filePath);
                    cmd.Parameters.AddWithValue("@CreatedBy", Session["uid"].ToString());

                    cmd.ExecuteNonQuery();
                }
            }

            con.Close();

            ClearFields();
            BindAnnouncementGrid();

            ScriptManager.RegisterClientScriptBlock(
                this, this.GetType(),
                "alertMessage", "alert('Announcement Saved Successfully ')", true);
        }
        catch (Exception ex)
        {
            con.Close();
            ScriptManager.RegisterClientScriptBlock(
                this, this.GetType(),
                "alertMessage", "alert('Error : " + ex.Message + "')", true);
        }
    }



    private void BindAnnouncementGrid()
    {
        try
        {

            SqlCommand cmd = new SqlCommand("Sp_GetAnnouncement_Faculty", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FacultyCode", Session["uid"].ToString());

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                gvAnnouncement.DataSource = dt;
                gvAnnouncement.DataBind();
            }
            else
            {
                gvAnnouncement.DataSource = null;
                gvAnnouncement.DataBind();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(
                this, this.GetType(),
                "alertMessage", "alert('Error : " + ex.Message + "')", true);
        }
    }
    private void ClearFields()
    {
        txtTitle.Text = string.Empty;
        txtDescription.Text = string.Empty;
        txtDate.Text = string.Empty;

        if (hfAnnouncementID != null)
            hfAnnouncementID.Value = string.Empty;

        // also clear ViewState fallback
        ViewState["AnnouncementID"] = null;

        if (btnUpdate != null) btnUpdate.Visible = false;
        if (btnSave != null) btnSave.Visible = true;

        // clear session fallback
        Session["EditingAnnouncementID"] = null;

        // reset client UI for courses
        string resetScript = "window.setTimeout(function(){ document.getElementById('courseBox').innerHTML = '-- Select Course --'; var cbs = document.querySelectorAll('#" + drpCourse.ClientID + " input[type=checkbox]'); if(cbs){ cbs.forEach(function(cb){ cb.checked=false; }); } },50);";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "resetCourseBox", resetScript, true);
    }

    protected void gvAnnouncement_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e == null || String.IsNullOrEmpty(e.CommandName)) return;

        if (e.CommandName == "EditAnn")
        {
            int id;
            if (!int.TryParse(Convert.ToString(e.CommandArgument), out id)) return;
            LoadAnnouncementForEdit(id);
        }
        else if (e.CommandName == "DeleteAnn")
        {
            int id;
            if (!int.TryParse(Convert.ToString(e.CommandArgument), out id)) return;

            try
            {
                SqlCommand cmd = new SqlCommand("Sp_DeleteAnnouncement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AnnouncementID", id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                BindAnnouncementGrid();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg", "alert('Announcement deleted.');", true);
            }
            catch (Exception ex)
            {
                con.Close();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "err", "alert('Delete failed: " + ex.Message + "');", true);
            }
        }
    }

    private void LoadAnnouncementForEdit(int announcementId)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("Sp_GetAnnouncementByID", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AnnouncementID", announcementId);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 0) return;

            DataRow row = dt.Rows[0];
            hfAnnouncementID.Value = announcementId.ToString();

            // store in ViewState as reliable fallback for postback
            ViewState["AnnouncementID"] = announcementId;

            // also store in Session as an additional reliable fallback
            Session["EditingAnnouncementID"] = announcementId;

            txtTitle.Text = row["Title"].ToString();
            txtDescription.Text = row["Description"].ToString();

            DateTime d;
            if (DateTime.TryParse(row["AnnouncementDate"].ToString(), out d))
                txtDate.Text = d.ToString("yyyy-MM-dd");

            bindDrpCollageList();

            string collegeId = row["CollegeID"].ToString();
            if (drpCollage.Items.FindByValue(collegeId) != null)
                drpCollage.SelectedValue = collegeId;

            bindDrpCourseList();

            string courseIds = row["CourseIDs"].ToString();

            if (!string.IsNullOrEmpty(courseIds) && drpCourse.Items.Count > 0)
            {
                var ids = courseIds
                            .Split(',')
                            .Select(x => x.Trim())
                            .ToList();

                foreach (ListItem li in drpCourse.Items)
                {
                    if (ids.Contains(li.Value.Trim()))
                    {
                        li.Selected = true;
                    }
                }
            }
            string semester = row["Semester"].ToString();
            if (drpSemester.Items.FindByValue(semester) != null)
                drpSemester.SelectedValue = semester;
            // ensure client-side courseBox shows selected items
            ScriptManager.RegisterStartupScript(this, this.GetType(), "updateCourseText", "window.setTimeout(function(){ if(typeof updateCourseText === 'function'){ updateCourseText(); } },50);", true);

            btnUpdate.Visible = true;
            btnSave.Visible = false;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(
                this, GetType(), "err",
                "alert('Edit load failed: " + ex.Message + "')", true);
        }
    }



    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        // Ensure validators ran
        if (!Page.IsValid) return;

        // 1️⃣ Validate AnnouncementID
        if (hfAnnouncementID == null || string.IsNullOrWhiteSpace(hfAnnouncementID.Value))
        {
            ScriptManager.RegisterClientScriptBlock(
                this, this.GetType(),
                "err", "alert('Announcement ID missing. Please click Edit again.');", true);
            return;
        }

        int announcementId;
        if (!int.TryParse(hfAnnouncementID.Value, out announcementId))
        {
            ScriptManager.RegisterClientScriptBlock(
                this, this.GetType(),
                "err", "alert('Invalid Announcement ID.');", true);
            return;
        }

        try
        {
            // 2️⃣ Handle File Upload (optional when editing)
            string filePath = "";
            if (fuAttachment.HasFile)
            {
                string fileName = System.IO.Path.GetFileName(fuAttachment.PostedFile.FileName);
                fuAttachment.SaveAs(Server.MapPath("~/Uploads/" + fileName));
                filePath = "~/Uploads/" + fileName;
            }

            // 3️⃣ Collect CourseIDs (multiple)
            string courseIds = string.Join(",",
                drpCourse.Items.Cast<ListItem>()
                .Where(i => i.Selected)
                .Select(i => i.Value)
            );

            if (string.IsNullOrEmpty(courseIds))
            {
                ScriptManager.RegisterClientScriptBlock(
                    this, this.GetType(),
                    "err", "alert('Please select at least one course.');", true);
                return;
            }

            // 4️⃣ Execute UPDATE
            using (SqlCommand cmd = new SqlCommand("Sp_UpdateAnnouncement", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@AnnouncementID", SqlDbType.Int).Value = announcementId;
                cmd.Parameters.Add("@CollegeID", SqlDbType.VarChar).Value = drpCollage.SelectedValue;
                cmd.Parameters.Add("@CourseIDs", SqlDbType.VarChar, 200).Value = courseIds;
                cmd.Parameters.Add("@Semester", SqlDbType.VarChar, 20).Value = drpSemester.SelectedValue;
                cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 200).Value = txtTitle.Text.Trim();
                cmd.Parameters.Add("@Description", SqlDbType.NVarChar, -1).Value = txtDescription.Text.Trim();
                cmd.Parameters.Add("@AnnouncementDate", SqlDbType.Date).Value =
                    DateTime.Parse(txtDate.Text.Trim());

                if (!string.IsNullOrEmpty(filePath))
                    cmd.Parameters.Add("@FilePath", SqlDbType.NVarChar, 500).Value = filePath;
                else
                    cmd.Parameters.Add("@FilePath", SqlDbType.NVarChar, 500).Value = DBNull.Value;

                cmd.Parameters.Add("@ModifiedBy", SqlDbType.VarChar, 50)
                    .Value = Session["uid"].ToString();

                con.Open();
                int rows = cmd.ExecuteNonQuery();
                con.Close();

                // 5️⃣ Confirm update
                if (rows > 0)
                {
                    ClearFields();
                    BindAnnouncementGrid();
                    ScriptManager.RegisterClientScriptBlock(
                        this, this.GetType(),
                        "msg", "alert('Announcement updated successfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(
                        this, this.GetType(),
                        "err", "alert('Update failed. No rows affected.');", true);
                }
            }
        }
        catch (Exception ex)
        {
            con.Close();
            ScriptManager.RegisterClientScriptBlock(
                this, this.GetType(),
                "err", "alert('Update failed: " + ex.Message + "');", true);
        }
    }

    // Server-side validator for course checkboxlist
    protected void cvCourse_ServerValidate(object source, ServerValidateEventArgs args)
    {
        bool any = drpCourse.Items.Cast<ListItem>().Any(i => i.Selected);
        args.IsValid = any;
    }

    // Server-side validator for fileupload.
    // When editing (hfAnnouncementID has value) file is optional; for new save file is required.
    protected void cvFile_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (hfAnnouncementID != null && !string.IsNullOrWhiteSpace(hfAnnouncementID.Value))
        {
            // editing - file optional
            args.IsValid = true;
            return;
        }

        args.IsValid = fuAttachment.HasFile;
    }
}