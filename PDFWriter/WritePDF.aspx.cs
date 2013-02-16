using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using PdfSharp.Pdf;

namespace ImageReport.PDFWriter
{
    public partial class WritePDF : System.Web.UI.Page
    {
        //string FilePath;
        //string FileName = "ImageReport.pdf";

        protected void Page_Load(object sender, EventArgs e)
        {
            ////---------------------------------------------------
            //ImageReportPDF pw = new ImageReportPDF(, FileName);
            //pw.PDFFilePath = Server.MapPath(".");
            //pw.PDFFileName = "";


            //FilePath = Server.MapPath(".");

            //try
            //{
            //    pw.GeneratePDFReport();
            //    //CreateHelloWorldPdf();

            //    //Response.Write("PDF Complete!");

            //    Response.Redirect(FileName);
            //}
            //catch (Exception ex)
            //{
            //    Response.Write(ex.Message);
            //}
           
        }
    }
}