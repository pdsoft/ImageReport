using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using System.Diagnostics;
using System.Web.SessionState;

namespace ImageReport.PDFWriter
{
    public class PDFWriter
    {
        public PDFWriter()
        { }

        public bool WritePDFReport(string FilePath)
        {

            //try
            //{
                // Create a invoice form with the sample invoice data

               InvoiceForm invoice = new InvoiceForm(FilePath + "\\Invoice.xml");

                // Create a MigraDoc document
                Document document = invoice.CreateDocument();
                document.UseCmykColor = true;

                //        #if DEBUG
                //                // for debugging only...
                //                MigraDoc.DocumentObjectModel.IO.DdlWriter.WriteToFile(document, "MigraDoc.mdddl");
                //                document = MigraDoc.DocumentObjectModel.IO.DdlReader.DocumentFromFile("MigraDoc.mdddl");
                //        #endif

                // Create a renderer for PDF that uses Unicode font encoding
                PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(true);

                // Set the MigraDoc document
                pdfRenderer.Document = document;

                // Create the PDF document
                pdfRenderer.RenderDocument();

                // Save the PDF document...
                string filename = FilePath + "\\Invoice.pdf";

                //#if DEBUG
                //                // I don't want to close the document constantly...
                //                filename = "Invoice-" + Guid.NewGuid().ToString("N").ToUpper() + ".pdf";
                //#endif
                pdfRenderer.Save(filename);
                // ...and start a viewer.
                //  Process.Start(filename);

            //}
            //catch (Exception ex)
            //{
            //    ex.
            //    //Console.WriteLine(ex.Message);
            //    //Console.ReadLine();
            //}

            return true;

        }
    }
}