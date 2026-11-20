
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;

public partial class Faculty_LivePunchRecord : Page
{
    private bool isJsonRequest = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["json"] == "1")
        {
            isJsonRequest = true;
            SendLatestPunches();
        }
    }

    protected override void Render(HtmlTextWriter writer)
    {
        if (isJsonRequest)
            return;

        base.Render(writer);
    }

    private void SendLatestPunches()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/json";
        Response.ContentEncoding = Encoding.UTF8;
        Response.Charset = "utf-8";

        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));

        JavaScriptSerializer serializer = new JavaScriptSerializer();
        serializer.MaxJsonLength = int.MaxValue;

        try
        {
            // Default machine is Device ID 21
            int deviceId;

            if (!int.TryParse(Request.QueryString["deviceId"], out deviceId) ||
                (deviceId != 21 && deviceId != 22))
            {
                deviceId = 21;
            }

            string machineName =
                deviceId == 21
                ? "Hospital Emergency Gate"
                : "SecurityGate No 2";

            List<Dictionary<string, object>> result =
                new List<Dictionary<string, object>>();

            ConnectionStringSettings settings =
                ConfigurationManager.ConnectionStrings["TMUCON"];

            if (settings == null ||
                String.IsNullOrEmpty(settings.ConnectionString))
            {
                throw new Exception(
                    "Connection string TMUCON is missing from Web.config.");
            }

            using (SqlConnection con =
                new SqlConnection(settings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 30;

                    cmd.CommandText = @"
;WITH LatestPunches AS
(
    SELECT TOP 4
        DeviceLogId,
        EmployeeCode,
        DeviceId
    FROM [172.0.1.113].eBioServerNew.dbo.DeviceLogs WITH (NOLOCK)
    WHERE DeviceId = @DeviceId
    ORDER BY DeviceLogId DESC
)
SELECT
    P.DeviceLogId AS DeviceLogId,
    P.EmployeeCode AS FingerNo,
    P.DeviceId AS DeviceId,

    ISNULL(S.[No_], '') AS StudentNo,
    S.[Graduation] AS Graduation,
    ISNULL(S.[Student Name], '') AS StudentName,
    ISNULL(S.[Fathers Name], '') AS FathersName,

    ISNULL(
        (
            SELECT TOP 1 DV.[Name]
            FROM [dbo].[TMU$Dimension Value] DV WITH (NOLOCK)
            WHERE DV.[Code] = S.[Global Dimension 1 Code]
        ), ''
    ) AS CollegeCode,

    ISNULL(S.[Mobile Number], '') AS MobileNumber,
    ISNULL(S.[Course Name], '') AS CourseName,
    S.[Student Image] AS StudentImage,
    ISNULL(S.[E-Mail Address], '') AS EmailAddress,

    CASE
        WHEN S.[Hostel Alloted] = 1 THEN 'Hostler'
        ELSE 'Day Scholar'
    END AS Accommodation,

    ISNULL(
        (
            SELECT TOP 1 H.[Name]
            FROM [dbo].[TMU$Hostel] H WITH (NOLOCK)
            WHERE H.[Code] = S.[Hostel Code]
        ), ''
    ) AS HostelCode,

    CONCAT(
        S.[Address1], ' ',
        S.[Address2], ' ',
        S.[Address3], ' ',
        S.[Address4], ' ',
        S.[City], ' ',
        S.[District], ' ',
        S.[State]
    ) AS [Address],

    CASE
        WHEN ISNULL(S.[Father’s_Guardian Mobile No_], '') = ''
            THEN ISNULL(S.[Father Mobile No], '')
        ELSE S.[Father’s_Guardian Mobile No_]
    END AS [Father Mobile No],

    CASE
        WHEN S.[Transport Allot] = 0 THEN 'No'
        ELSE 'Yes'
    END AS [Transport Facility]

FROM LatestPunches P

LEFT JOIN [dbo].[TMU$Student - COLLEGE] S
    ON LTRIM(RTRIM(CONVERT(NVARCHAR(50), S.[Finger No_])))
       COLLATE Latin1_General_100_CS_AS
       =
       LTRIM(RTRIM(CONVERT(NVARCHAR(50), P.EmployeeCode)))
       COLLATE Latin1_General_100_CS_AS

WHERE ISNULL(S.[No_], '') <> ''

ORDER BY P.DeviceLogId DESC;";

                    cmd.Parameters.Add("@DeviceId", SqlDbType.Int).Value =
                        deviceId;

                    con.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Dictionary<string, object> item =
                                new Dictionary<string, object>();

                            string studentNo =
                                GetString(dr, "StudentNo");

                            string graduation =
                                GetString(dr, "Graduation");

                            string studentName =
                                GetString(dr, "StudentName");

                            string fatherName =
                                GetString(dr, "FathersName");

                            string fingerNo =
                                GetString(dr, "FingerNo");

                            string collegeCode =
                                GetString(dr, "CollegeCode");

                            string mobile =
                                GetString(dr, "MobileNumber");

                            string course =
                                GetString(dr, "CourseName");

                            string email =
                                GetString(dr, "EmailAddress");

                            string hostel =
                                GetString(dr, "Accommodation");

                            string room =
                                GetString(dr, "HostelCode");

                            string address =
                                GetString(dr, "Address");

                            string fatherMobileNo =
                                GetString(dr, "Father Mobile No");

                            string transportFacility =
                                GetString(dr, "Transport Facility");

                            string actualDeviceId =
                                GetString(dr, "DeviceId");

                            item["studentNo"] = studentNo;
                            item["Graduation"] = graduation;

                            item["studentName"] =
                                String.IsNullOrWhiteSpace(studentName)
                                ? "Student not found"
                                : studentName;

                            item["studentImage"] =
                                GetStudentImage(dr["StudentImage"]);

                            item["deviceLogId"] =
                                GetString(dr, "DeviceLogId");

                            item["deviceId"] = actualDeviceId;
                            item["machineName"] = machineName;
                            item["fingerNo"] = fingerNo;

                            List<Dictionary<string, string>> fields =
                                new List<Dictionary<string, string>>();

                            AddField(fields, "Father Name", fatherName);
                            AddField(fields, "Student No", studentNo);
                            AddField(fields, "College", collegeCode);
                            AddField(fields, "Course", course);
                            AddField(fields, "Mobile", mobile);
                            AddField(fields, "Transport Facility",
                                transportFacility);
                            AddField(fields, "Accommodation", hostel);
                            AddField(fields, "Hostel", room);
                            AddField(fields, "Permanent Address", address);
                            AddField(fields, "Father Mobile No",
                                fatherMobileNo);

                            item["fields"] = fields;

                            result.Add(item);
                        }
                    }
                }
            }

            Response.StatusCode = 200;
            Response.Write(serializer.Serialize(result));
        }
        catch (Exception ex)
        {
        //    Trace.TraceError(
        //        "LivePunchRecord error: " + ex.ToString());

            Response.StatusCode = 500;

            Dictionary<string, object> error =
                new Dictionary<string, object>();

            error["error"] =
                "Unable to retrieve live punch records. Check server logs.";

            Response.Write(serializer.Serialize(error));
        }
    }

    private string GetString(SqlDataReader reader, string columnName)
    {
        int ordinal = reader.GetOrdinal(columnName);

        if (reader.IsDBNull(ordinal))
            return "";

        return Convert.ToString(reader.GetValue(ordinal)).Trim();
    }

    private void AddField(
        List<Dictionary<string, string>> fields,
        string label,
        string value)
    {
        Dictionary<string, string> field =
            new Dictionary<string, string>();

        field["label"] = label;

        field["value"] =
            String.IsNullOrWhiteSpace(value) ? "-" : value;

        fields.Add(field);
    }

    private string GetStudentImage(object imageValue)
    {
        if (imageValue == null || imageValue == DBNull.Value)
            return "";

        byte[] imageBytes = imageValue as byte[];

        if (imageBytes == null)
        {
            string value = Convert.ToString(imageValue).Trim();

            if (String.IsNullOrEmpty(value))
                return "";

            if (value.StartsWith(
                "0x", StringComparison.OrdinalIgnoreCase))
            {
                imageBytes = HexToBytes(value.Substring(2));
            }
            else if (
                value.StartsWith("http://",
                    StringComparison.OrdinalIgnoreCase) ||
                value.StartsWith("https://",
                    StringComparison.OrdinalIgnoreCase) ||
                value.StartsWith("data:image/",
                    StringComparison.OrdinalIgnoreCase))
            {
                return value;
            }
            else if (value.StartsWith("~/"))
            {
                return ResolveUrl(value);
            }
            else
            {
                return "";
            }
        }

        if (imageBytes == null || imageBytes.Length == 0)
            return "";

        return ConvertImageToJpeg(imageBytes);
    }

    private byte[] HexToBytes(string hex)
    {
        if (String.IsNullOrEmpty(hex))
            return new byte[0];

        hex = hex.Trim();

        if (hex.Length % 2 != 0)
            throw new FormatException(
                "Invalid hexadecimal image data.");

        byte[] bytes = new byte[hex.Length / 2];

        for (int i = 0; i < bytes.Length; i++)
        {
            bytes[i] = Convert.ToByte(
                hex.Substring(i * 2, 2), 16);
        }

        return bytes;
    }

    private string ConvertImageToJpeg(byte[] imageBytes)
    {
        try
        {
            using (MemoryStream input =
                new MemoryStream(imageBytes))
            using (System.Drawing.Image sourceImage =
                System.Drawing.Image.FromStream(input, true, true))
            using (Bitmap bitmap =
                new Bitmap(sourceImage.Width, sourceImage.Height))
            using (Graphics graphics =
                Graphics.FromImage(bitmap))
            using (MemoryStream output =
                new MemoryStream())
            {
                graphics.Clear(Color.White);

                graphics.DrawImage(
                    sourceImage,
                    new Rectangle(
                        0,
                        0,
                        bitmap.Width,
                        bitmap.Height));

                bitmap.Save(
                    output,
                    ImageFormat.Jpeg);

                return "data:image/jpeg;base64," +
                    Convert.ToBase64String(output.ToArray());
            }
        }
        catch (Exception ex)
        {
            //Trace.TraceError(
            //    "Student image conversion failed: " + ex.ToString());

            return "";
        }
    }
}
