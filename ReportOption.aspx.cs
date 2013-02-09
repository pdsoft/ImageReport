using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Text.RegularExpressions;
using ImageReport.Helper;

namespace ImageReport
{
    public partial class ReportOption : System.Web.UI.Page
    {

        IDictionary<string, string> LoadedImgFiles;

        protected void Page_Load(object sender, EventArgs e)
        {
            //---------------------------------------------------
            LoadedImgFiles = (IDictionary<string, string>)Session["LoadedIamges"];

            //-------------------------------------
            if (LoadedImgFiles.Count == 0)
            {
                frmOption.Style["display"] = "none";
                divLable.InnerText = "No images has been selected !";
                return;
            }

        }

        //-------------------------------------------------------------
        protected void cmdGenerateRpt_Click(object sender, EventArgs e)
        {
            ReportViewer1.Reset();
            ReportViewer1.LocalReport.EnableExternalImages = true;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("Report.rdlc");
            ReportDataSource rds = new ReportDataSource("DataSet1", getData());
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rds);
            ReportViewer1.DataBind();

            //---------------------------------------
            ReportParameter[] parames = new ReportParameter[3];

            parames[0] = new ReportParameter("ReporHeader", txtTitle.Text);
            parames[1] = new ReportParameter("ReportFooter", txtFooter.Text);
            parames[2] = new ReportParameter("ReportSubtitle",txtSubtitle.Text);

            ReportViewer1.LocalReport.SetParameters(parames);

            //-------------------------- generate PDF
            Warning[] warnings;
            string[] streamids;
            string mimetype;
            string encoding;
            string extension;

            byte[] bytes = ReportViewer1.LocalReport.Render(
                            "PDF", null, out mimetype, out encoding, out extension, out streamids, out warnings);

            string fileName = GetFileName(txtTitle.Text);       // by Fred, 1-19-2013
            string filePath = Server.MapPath(GetConfigValue.PDFPath) + @"\" + fileName + ".pdf";            // replace the Session.SessionID by fileName, by Fred, 1-19-2013

            // check if file exist, by Fred, 1-19-2013
            //if (!File.Exists(filePath))
            //{

            FileStream fs = new FileStream(filePath, FileMode.Create);

            fs.Write(bytes, 0, bytes.Length);
            fs.Close();

            //----------------------------------------
            string pdfURl = @"/" + GetConfigValue.PDFPath + "/" + fileName + ".pdf?" + DateTime.Now.ToString();
            string scriptString = "<script language=JavaScript>" + "{ openPDF('" + pdfURl + "'); } </script>";

            if (ClientScript.IsClientScriptBlockRegistered("OpenPDFSc") == false)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "OpenPDFSc", scriptString);
            }

            //}
            //else
            //{
            //    string scriptString = "<script language=JavaScript>" + "{ alert('File name exist already!'); } </script>";

            //    if (ClientScript.IsClientScriptBlockRegistered("OpenPDFSc") == false)
            //    {
            //        ClientScript.RegisterClientScriptBlock(this.GetType(), "OpenPDFSc", scriptString);
            //    }
            //}
        }

        //--------------------------------------------
        //--------------------------------------------
        // replace the invalid character with blank, by Fred, 1-19-2013
        private string GetFileName(string fileName)
        {
            string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            return r.Replace(fileName, "");
        }

        //--------------------------------------------
        //--------------------------------------------
        private DataTable getData()
        {
            //IDictionary<string, string> LoadedImgFiles = (IDictionary<string, string>)Session["LoadedIamges"];

            DataSet1 ds = new DataSet1();

            DataTable dt = ds.Tables[0];

            for (int i = 0; i < LoadedImgFiles.Count; i++)
            {
                DataRow dr = dt.NewRow();

                var pair = LoadedImgFiles.ElementAt(i);

                dr["FilePath"] = "http://" + System.Web.HttpContext.Current.Request.Url.Authority + "/uploads/" + pair.Key.ToString();
                dr["Description"] = pair.Value.ToString();

                dt.Rows.Add(dr);
            }

            return dt;
        }
    }
}