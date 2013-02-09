using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ImageReport.Helper;

namespace ImageReport
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           // txtPasscode.Focus();        // by Fred, 1-20-2013

            Panel1.Style["display"] = "none";
            Panel2.Style["display"] = "inline";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //if (txtPasscode.Text.Trim() == GetConfigValue.Passcode)     // use Config.Passcode, by Fred, 1-20-2013
            //{
            //    Panel1.Style["display"] = "none";
            //    Panel2.Style["display"] = "inline";
            //}
            //else
            //{
            //    lblPasscode.Text = "Invalid Passcode. Try Again!";
            //}
        }
    }
}