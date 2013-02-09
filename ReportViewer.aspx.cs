using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Data;

namespace ImageReport
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ReportViewer1.Reset();
                this.ReportViewer1.LocalReport.EnableExternalImages = true;
                this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("Report.rdlc");
                ReportDataSource rds = new ReportDataSource("DataSet1", getData());

                this.ReportViewer1.LocalReport.DataSources.Clear();
                this.ReportViewer1.LocalReport.DataSources.Add(rds);
                this.ReportViewer1.DataBind();
                this.ReportViewer1.LocalReport.Refresh();
            }    
        }

        //--------------------------------------------
        private DataTable getData()
        {
            IDictionary<string, List<string>> LoadedImgFiles =
                (IDictionary<string, List<string>>)Session["LoadedIamges"];

            DataSet1 ds = new DataSet1();

            DataTable dt = ds.Tables[0];

            for (int i = 0; i < LoadedImgFiles.Count; i ++)
            {
                DataRow dr = dt.NewRow();

                var pair = LoadedImgFiles.ElementAt(i);
                //tr.Cells.Add(AddImage(pair.Key.ToString(), pair.Value.ToString()));

                //if (i < LoadedImgFiles.Count - 1)
                //{
                //    pair = LoadedImgFiles.ElementAt(i + 1);
                //    tr.Cells.Add(AddImage(pair.Key.ToString(), pair.Value.ToString()));
                //}

                dr["FilePath"] = "http://" + System.Web.HttpContext.Current.Request.Url.Authority + "/uploads/" + pair.Key.ToString();
                dr["Description"] = dr["FilePath"];

                dt.Rows.Add(dr);
            }

            return dt;
        }
    }
}