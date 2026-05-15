using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO.Ports;

public partial class SMS : System.Web.UI.Page
{
    SerialPort sp = new SerialPort();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        sp.PortName = "COM9";
        sp.Open();
        string ph_no;
        ph_no = char.ConvertFromUtf32(34) + txtMobileNo.Text + char.ConvertFromUtf32(34);
        sp.Write("AT+CMGF=1" + char.ConvertFromUtf32(13));
        sp.Write("AT+CMGS=" + ph_no + char.ConvertFromUtf32(13));
        sp.Write(txtMsg.Text + char.ConvertFromUtf32(26) + char.ConvertFromUtf32(13));
        sp.Close();
    }
}
