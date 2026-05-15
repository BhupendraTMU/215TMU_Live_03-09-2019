using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;


using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using System.IO;
using System.Collections;
using System.Net;
using iTextSharp.text.html.simpleparser;
  


public partial class Payslipdetail : System.Web.UI.Page
{
    Connection Portalcon;
    ServicePoratal con;
    protected void Page_Load(object sender, EventArgs e)
    {
        btnsendEmail.Visible = false;
        try
        {
            Portalcon = new Connection();
            con = new ServicePoratal();
            if (!IsPostBack)
            {
                showYear();

            }


        }
        catch (Exception)
        {
            Response.Redirect("Default.aspx");
        }
    }

    public void showYear()
    {

       int lstyear= System.DateTime.Now.Year;

       for (int i = 2010; i <= lstyear; i++)
            ddYear.Items.Add(i.ToString());
    }

    string maxdate = ""; string maxdateforarrrer = "";
    public void showProfleDetail()
    {
        lblCalenderDate.Text = ddMonth.SelectedItem.Text + " " + ddYear.Text;
        string stable = Session["Company"].ToString();
        string rtable = stable.Replace(".", "_");
        string tblNameEmployee = "[" + rtable + "$Employee" + "]";

        SqlDataReader dr = Portalcon.Profile_Detail(tblNameEmployee, Session["uid"].ToString());
        dr.Read();
        if (dr.HasRows)
        {
            lblEmployeeid.Text = Session["uid"].ToString();

            string fname = dr["First Name"].ToString();
            string sname = dr["Middle Name"].ToString();
            string lname = dr["Last Name"].ToString();
            lblEmployeeName.Text = fname + " " + sname + " " + lname;
            lblDesignation.Text = dr["Job Title_Grade Desc"].ToString();
            lblDesignation.Text = dr["Job Title_Grade Desc"].ToString();
            lblDepartment.Text = dr["Department Name"].ToString();
            lblPFNo.Text = dr["PF No"].ToString();


            lblPanNo.Text = dr["PAN No"].ToString();
            lblESINo.Text = dr["ESI No"].ToString();
            lblLocation.Text = dr["Location Code"].ToString();
            string paymethod = dr["Payment Method"].ToString();
            if (paymethod == "1")
            {
                lblPayMode.Text = "Cash";
            }
            if (paymethod == "2")
            {
                lblPayMode.Text = "Cheque";
            }
            if (paymethod == "3")
            {
                lblPayMode.Text = "Bank Transfer";
            }
            lblDOJ.Text = Convert.ToDateTime(dr["Employment Date"].ToString()).ToString("dd-MMM-yy");

            lblAcoountNo.Text = dr["Account No"].ToString();







        }
        dr.Close();
        Portalcon.DisConnect();

      
        string tblsCompanyn = "[" + rtable + "$Company Information" + "]";
        SqlDataReader dr1 = Portalcon.SHow_CompanyInformation(tblsCompanyn);
        dr1.Read();

        if (dr1.HasRows)
        {
            lblCompanyAddress.Text = dr1["Address"].ToString();

            byte[] img = (byte[])(dr1["Picture"]);

            //string base64String = Convert.ToBase64String(img, 0, img.Length);
            //imgPhoto.ImageUrl = "data:image/jpg;base64," + base64String;
            //imgPhoto.Visible = true;
        }
        dr1.Close();
        Portalcon.DisConnect();



      

        string tblePayEmployeePayDetails = "[" + rtable + "$Pay Employee Pay Details" + "]";

        SqlDataReader drmaxd=Portalcon.ShowPayMaxDate(tblePayEmployeePayDetails,Convert.ToInt32(ddMonth.SelectedValue),Convert.ToInt32(ddYear.SelectedItem.Text),Session["uid"].ToString());
        drmaxd.Read();

        if (drmaxd.HasRows)
        {
            maxdate = drmaxd["ForMonthDate"].ToString();
        
        }
        drmaxd.Close();
        Portalcon.DisConnect();
        SqlDataReader odr = Portalcon.ShowPayEmployeePayDetails(tblePayEmployeePayDetails, Convert.ToInt32(ddMonth.SelectedValue), Convert.ToInt32(ddYear.SelectedItem.Text), Session["uid"].ToString(), maxdate);
        DataTable Dt = new DataTable();
        Dt.Load(odr);
        grdSalary.DataSource = Dt;
        grdSalary.DataBind();
        odr.Close();
        Portalcon.DisConnect();

        SqlDataReader drarre = Portalcon.ShowPayEmployeePayDetailsArear(tblePayEmployeePayDetails, Convert.ToInt32(ddMonth.SelectedValue), Convert.ToInt32(ddYear.SelectedItem.Text), Session["uid"].ToString());
        drarre.Read();
        if (drarre.HasRows)
        {
            maxdateforarrrer = drarre["ForMonthDate"].ToString();
        }
        drarre.Close();
        Portalcon.DisConnect();

        SqlDataReader odrarrear = Portalcon.ShowPayEmployeePayDetails(tblePayEmployeePayDetails, Convert.ToInt32(ddMonth.SelectedValue), Convert.ToInt32(ddYear.SelectedItem.Text), Session["uid"].ToString(), maxdateforarrrer);
        DataTable Dtrear = new DataTable();
        Dtrear.Load(odrarrear);
        if (Dtrear.Rows.Count > 0)
        {
            
          


            int arrone; string aharrone1 = "";
            for (arrone = 0; arrone <= Dtrear.Rows.Count-1; arrone++)
            {
               string dtva= Dtrear.Rows[arrone]["Payable Amount"].ToString();
                decimal dtva1=Convert.ToDecimal(dtva);
                grdSalary.Rows[arrone].Cells[3].Text = dtva1.ToString("00.00");


            }

            int ah; string ah1 = ""; string ah2 = "";
            for (ah = 0; ah <= grdSalary.Rows.Count - 1; ah++)
            {
                ah1 = grdSalary.Rows[ah].Cells[3].Text;
                ah2 = grdSalary.Rows[ah].Cells[2].Text;


                if (ah1 == "")
                {
                    ah1 = "0";
                }

                if (ah2 == "")
                {
                    ah2 = "0";
                }
                decimal rw0 = Convert.ToDecimal(ah1);
                decimal rw2 = Convert.ToDecimal(ah2);
                grdSalary.Rows[ah].Cells[4].Text = (rw0 + rw2).ToString("00.00");


            }



            grdSalary.Columns[3].Visible = true;
            grdSalary.Columns[4].Visible = true;

            

        }
        else

        {

         
            grdSalary.Columns[3].Visible = true;
            grdSalary.Columns[4].Visible = true;
            int ah; string ah1 = "";
            for (ah = 0; ah <= grdSalary.Rows.Count - 1; ah++)
            {
                ah1 = grdSalary.Rows[ah].Cells[2].Text;
                grdSalary.Rows[ah].Cells[4].Text = ah1;
                grdSalary.Rows[ah].Cells[3].Text = "0.00";

            }
         
        
        }
        odrarrear.Close();
        Portalcon.DisConnect();

        SqlDataReader drlwp = Portalcon.ShowPayNowofDaysworking(tblePayEmployeePayDetails, Convert.ToInt32(ddMonth.SelectedValue), Convert.ToInt32(ddYear.SelectedItem.Text), Session["uid"].ToString(), maxdate);
        drlwp.Read();
        if (drlwp.HasRows)
        {
            string paiddays = drlwp["Paid Days"].ToString();
            decimal paiddays1 = Convert.ToDecimal(paiddays);
            lblDaysPaid.Text = paiddays1.ToString("00");
            string lwp = drlwp["LWP Days Full"].ToString();
            decimal lwp1 = Convert.ToDecimal(lwp);
            lbllwp.Text = lwp1.ToString("00");
        }
        else
        {
            lblDaysPaid.Text = "";
            lbllwp.Text = "";
        }
        drlwp.Close();
        Portalcon.DisConnect();
    }

    protected void btnPreview_Click(object sender, EventArgs e)
    {
        pnlSalarySlip.Visible = true;
        showProfleDetail();
        showTotal();
        btnPrint.Visible = true;
        btnsendEmail.Visible = false;
    }
    private decimal TotalSales = (decimal)0.0;
    private decimal TotalPay_amt = (decimal)0.0;
    private decimal TotalSalesarerPay_AMT = (decimal)0.0;
    private decimal TotalSalesarerPay_AMTArrear = (decimal)0.0;
    protected void grdSalary_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
           
            TotalSales += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Actual Amount"));
        else if (e.Row.RowType == DataControlRowType.Footer)

            e.Row.Cells[1].Text = String.Format("{0:f2}", TotalSales);



        if (e.Row.RowType == DataControlRowType.DataRow)

            TotalPay_amt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Payable Amount"));
        else if (e.Row.RowType == DataControlRowType.Footer)

            e.Row.Cells[2].Text = String.Format("{0:f2}", TotalPay_amt);


      
     
    }


    public String changeNumericToWords(double numb)
    {
        String num = numb.ToString();
        return changeToWords(num, false);
    }

    public String changeCurrencyToWords(String numb)
    {
        return changeToWords(numb, true);
    }

    public String changeNumericToWords(String numb)
    {
        return changeToWords(numb, false);
    }

    public String changeCurrencyToWords(double numb)
    {
        return changeToWords(numb.ToString(), true);
    }

    private String changeToWords(String numb, bool isCurrency)
    {
        String val = "", wholeNo = numb, points = "", andStr = "", pointStr = "";
        String endStr = (isCurrency) ? ("Only") : ("");
        try
        {
            int decimalPlace = numb.IndexOf(".");
            if (decimalPlace > 0)
            {
                wholeNo = numb.Substring(0, decimalPlace);
                points = numb.Substring(decimalPlace + 1);
                if (Convert.ToInt32(points) > 0)
                {
                    andStr = (isCurrency) ? ("and") : ("point");// just to separate whole numbers from points/Rupees
                    endStr = (isCurrency) ? ("Rupees " + endStr) : ("");
                    pointStr = translateRupees(points);
                }
            }
            val = String.Format("{0} {1}{2} {3}", translateWholeNumber(wholeNo).Trim(), andStr, pointStr, endStr);
        }
        catch
        {
            ;
        }
        return val;
    }

    private String translateWholeNumber(String number)
    {
        string word = "";
        try
        {
            bool beginsZero = false;//tests for 0XX
            bool isDone = false;//test if already translated
            double dblAmt = (Convert.ToDouble(number));
            //if ((dblAmt > 0) && number.StartsWith("0"))

            if (dblAmt > 0)
            {//test for zero or digit zero in a nuemric
                beginsZero = number.StartsWith("0");
                int numDigits = number.Length;
                int pos = 0;//store digit grouping
                String place = "";//digit grouping name:hundres,thousand,etc...
                switch (numDigits)
                {
                    case 1://ones' range
                        word = ones(number);
                        isDone = true;
                        break;
                    case 2://tens' range
                        word = tens(number);
                        isDone = true;
                        break;
                    case 3://hundreds' range
                        pos = (numDigits % 3) + 1;
                        place = " Hundred ";
                        break;
                    case 4://thousands' range
                    case 5:
                    case 6:
                        pos = (numDigits % 4) + 1;
                        place = " Thousand ";
                        break;
                    case 7://millions' range
                    case 8:
                    case 9:
                        pos = (numDigits % 7) + 1;
                        place = " Million ";
                        break;
                    case 10://Billions's range
                        pos = (numDigits % 10) + 1;
                        place = " Billion ";
                        break;
                    //add extra case options for anything above Billion...
                    default:
                        isDone = true;
                        break;
                }
                if (!isDone)
                {//if transalation is not done, continue...(Recursion comes in now!!)
                    word = translateWholeNumber(number.Substring(0, pos)) + place + translateWholeNumber(number.Substring(pos));
                    //check for trailing zeros
                    if (beginsZero) word = " and " + word.Trim();
                }
                //ignore digit grouping names
                if (word.Trim().Equals(place.Trim())) word = "";
            }
        }
        catch
        {
            ;
        }
        return word.Trim();
    }

    private String tens(String digit)
    {
        int digt = Convert.ToInt32(digit);
        String name = null;
        switch (digt)
        {
            case 10:
                name = "Ten";
                break;
            case 11:
                name = "Eleven";
                break;
            case 12:
                name = "Twelve";
                break;
            case 13:
                name = "Thirteen";
                break;
            case 14:
                name = "Fourteen";
                break;
            case 15:
                name = "Fifteen";
                break;
            case 16:
                name = "Sixteen";
                break;
            case 17:
                name = "Seventeen";
                break;
            case 18:
                name = "Eighteen";
                break;
            case 19:
                name = "Nineteen";
                break;
            case 20:
                name = "Twenty";
                break;
            case 30:
                name = "Thirty";
                break;
            case 40:
                name = "Fourty";
                break;
            case 50:
                name = "Fifty";
                break;
            case 60:
                name = "Sixty";
                break;
            case 70:
                name = "Seventy";
                break;
            case 80:
                name = "Eighty";
                break;
            case 90:
                name = "Ninety";
                break;
            default:
                if (digt > 0)
                {
                    name = tens(digit.Substring(0, 1) + "0") + " " + ones(digit.Substring(1));
                }
                break;
        }
        return name;
    }

    private String ones(String digit)
    {
        int digt = Convert.ToInt32(digit);
        String name = "";
        switch (digt)
        {
            case 1:
                name = "One";
                break;
            case 2:
                name = "Two";
                break;
            case 3:
                name = "Three";
                break;
            case 4:
                name = "Four";
                break;
            case 5:
                name = "Five";
                break;
            case 6:
                name = "Six";
                break;
            case 7:
                name = "Seven";
                break;
            case 8:
                name = "Eight";
                break;
            case 9:
                name = "Nine";
                break;
        }
        return name;
    }

    private String translateRupees(String Rupees)
    {
        String cts = "", digit = "", engOne = "";
        for (int i = 0; i < Rupees.Length; i++)
        {
            digit = Rupees[i].ToString();
            if (digit.Equals("0"))
            {
                engOne = "Zero";
            }
            else
            {
                engOne = ones(digit);
            }
            cts += " " + engOne;
        }
        return cts;
    }





    public void showTotal()
    {
        
        try
        {
            grdSalary.FooterRow.Cells[0].Text = "Total ";

            decimal sum = 0;
            for (int index = 0; index < this.grdSalary.Rows.Count; index++)
                sum += Convert.ToDecimal(this.grdSalary.Rows[index].Cells[3].Text);
            grdSalary.FooterRow.Cells[3].Text = String.Format("{0:f2}", sum);
            
            decimal sum1 = 0;
            for (int index = 0; index < this.grdSalary.Rows.Count; index++)
                sum1 += Convert.ToDecimal(this.grdSalary.Rows[index].Cells[4].Text);
            grdSalary.FooterRow.Cells[4].Text = String.Format("{0:f2}", sum1);

            lblTotalEarning.Text = String.Format("{0:f2}", sum1);
            lblNetPayable.Text = String.Format("{0:f2}", sum1);

           // lblTextRupees.Text = NumberToText.Convert(Convert.ToDecimal(lblNetPayable.Text));
       //lblTextRupees.Text= changeCurrencyToWords(Convert.ToDouble(lblNetPayable.Text));

        }
        catch (Exception)
        { }
        


    }

    static class NumberToText
    {
        private static string[] _ones =
        {
            "zero",
            "one",
            "two",
            "three",
            "four",
            "five",
            "six",
            "seven",
            "eight",
            "nine"
        };

        private static string[] _teens =
        {
            "ten",
            "eleven",
            "twelve",
            "thirteen",
            "fourteen",
            "fifteen",
            "sixteen",
            "seventeen",
            "eighteen",
            "nineteen"
        };

        private static string[] _tens =
        {
            "",
            "ten",
            "twenty",
            "thirty",
            "forty",
            "fifty",
            "sixty",
            "seventy",
            "eighty",
            "ninety"
        };

        // US Nnumbering:
        private static string[] _thousands =
        {
            "",
            "thousand",
            "million",
            "billion",
            "trillion",
            "quadrillion"
        };

        /// <summary>
        /// Converts a numeric value to words suitable for the portion of
        /// a check that writes out the amount.
        /// </summary>
        /// <param name="value">Value to be converted</param>
        /// <returns></returns>
        public static string Convert(decimal value)
        {
            string digits, temp;
            bool showThousands = false;
            bool allZeros = true;

            // Use StringBuilder to build result
            StringBuilder builder = new StringBuilder();
            // Convert integer portion of value to string
            digits = ((long)value).ToString();
            // Traverse characters in reverse order
            for (int i = digits.Length - 1; i >= 0; i--)
            {
                int ndigit = (int)(digits[i] - '0');
                int column = (digits.Length - (i + 1));

                // Determine if ones, tens, or hundreds column
                switch (column % 3)
                {
                    case 0:        // Ones position
                        showThousands = true;
                        if (i == 0)
                        {
                            // First digit in number (last in loop)
                            temp = String.Format("{0} ", _ones[ndigit]);
                        }
                        else if (digits[i - 1] == '1')
                        {
                            // This digit is part of "teen" value
                            temp = String.Format("{0} ", _teens[ndigit]);
                            // Skip tens position
                            i--;
                        }
                        else if (ndigit != 0)
                        {
                            // Any non-zero digit
                            temp = String.Format("{0} ", _ones[ndigit]);
                        }
                        else
                        {
                            // This digit is zero. If digit in tens and hundreds
                            // column are also zero, don't show "thousands"
                            temp = String.Empty;
                            // Test for non-zero digit in this grouping
                            if (digits[i - 1] != '0' || (i > 1 && digits[i - 2] != '0'))
                                showThousands = true;
                            else
                                showThousands = false;
                        }

                        // Show "thousands" if non-zero in grouping
                        if (showThousands)
                        {
                            if (column > 0)
                            {
                                temp = String.Format("{0}{1}{2}",
                                    temp,
                                    _thousands[column / 3],
                                    allZeros ? " " : ", ");
                            }
                            // Indicate non-zero digit encountered
                            allZeros = false;
                        }
                        builder.Insert(0, temp);
                        break;

                    case 1:        // Tens column
                        if (ndigit > 0)
                        {
                            temp = String.Format("{0}{1}",
                                _tens[ndigit],
                                (digits[i + 1] != '0') ? "-" : " ");
                            builder.Insert(0, temp);
                        }
                        break;

                    case 2:        // Hundreds column
                        if (ndigit > 0)
                        {
                            temp = String.Format("{0} hundred ", _ones[ndigit]);
                            builder.Insert(0, temp);
                        }
                        break;
                }
            }

            // Append fractional portion/cents
            builder.AppendFormat("and {0:00}/100", (value - (long)value) * 100);

            // Capitalize first letter
            return String.Format("{0}{1}",
                Char.ToUpper(builder[0]),
                builder.ToString(1, builder.Length - 1));
        }
    }




    string mailfrom = ""; string subject1 = ""; string Body1 = ""; string smtpfromportal = ""; int portNo1; string Pass_From = ""; string Mail_Sending_Option = ""; string Leave_Applymail = ""; string CCMail = "";
    public void ShowMailData(string mailTo1)
    {

        SqlDataReader dr = con.Show_tble_MailSetup(Session["Company"].ToString(), "Profile");
        dr.Read();
        if (dr.HasRows)
        {
            mailfrom = dr["from_Email"].ToString();
            smtpfromportal = dr["smtp"].ToString();
            Pass_From = dr["Password_From"].ToString();
            CCMail = dr["CCMail"].ToString();
            string portNo = dr["Port_No"].ToString();
            portNo1 = Convert.ToInt32(portNo);
            //Mail_Sending_Option = dr["Mail_Sending_Option"].ToString();
            //Leave_Applymail = dr["Leave_Apply"].ToString();

        }

        dr.Close();
        con.DisConnect();
        //if (Mail_Sending_Option == "1" && Leave_Applymail == "True")
        //{
            SendMail(mailTo1);
        //}

    }


    public void SendMail(string MailTo)
    {

        System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
        if (mailfrom == "" && MailTo == "")
        { }

        else
        {
            if (mailfrom == "")
            {
            }

            else
            {

                msg.From = new MailAddress(Session["CompanyEmail"].ToString().Trim(), "  " + Session["Fulname"].ToString());
                if (MailTo == "")
                { }
                else
                {

                    string[] multi = MailTo.Split(',');
                    foreach (string multiTo in multi)
                    {
                        msg.To.Add(multiTo);
                    }
                }
                if (CCMail == "")
                { }
                else
                {
                    string[] ccmulti = CCMail.Split(',');
                    foreach (string ccm in ccmulti)
                    {
                        msg.CC.Add(ccm);
                    }
                }
                msg.Subject = "Pay Slip for the month of " + ddMonth.SelectedItem.Text + " " + ddYear.SelectedItem.Text;
                //var sb = new StringBuilder();
                //pnldata.RenderControl(new HtmlTextWriter(new StringWriter(sb)));

                //string s = sb.ToString();

                msg.Body += gridviewData(pnlSalarySlip);
                msg.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient();

                smtp.Port = portNo1;
                smtp.Host = smtpfromportal;
                smtp.EnableSsl = true;
                NetworkCredential credential = new NetworkCredential(mailfrom, Pass_From);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = credential;

                try
                {
                    smtp.Send(msg);
                    msg.Dispose();

                }
                catch (Exception)
                {
                    msg.Dispose();
                }
            }
        }
    }

    public string gridviewData(Panel grid)
    {
        StringBuilder sb = new StringBuilder();
        StringWriter sw = new StringWriter(sb);
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        grid.RenderControl(htw);

        return sb.ToString();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
    protected void btnsendEmail_Click(object sender, System.EventArgs e)
    {
        try
        {
            ShowMailData(Session["CompanyEmail"].ToString());
        }
        catch (Exception)
        { }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
}