using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;

namespace ImageReport.PDFWriter
{
    public class ImageReportPDF
    {

        public IDictionary<string, MyImage> LoadedImgFiles;

        public string ReportTitle;
        public string ReportSummary;

        //public string ImageFilePath;
        public string PDFFilePath;
        public string PDFFileName;

        //----------------------
        Section section;

        //--------------------------------
        public ImageReportPDF()
        {
        }

        ////--------------------------------
        //public ImageReportPDF(IDictionary<string, MyImage> _LoadedImgFiles, string _ReportTitle, string _ReportSummary)
        //{
        //    LoadedImgFiles = _LoadedImgFiles;
        //    ReportTitle = _ReportTitle;
        //    ReportSummary = _ReportSummary;
        //}

        ////--------------------------------
        //public ImageReportPDF(string _FilePath, string _FileName)
        //{
        //    FilePath = _FilePath;
        //    FileName = _FileName;
        //}

        //-------------------------------
        //-------------------------------
        public string GeneratePDFReport()
        {

            try
            {
                // Create a MigraDoc document
                //Document document = pdf.CreateDocument();
                Document document = CreateDocument();

                document.UseCmykColor = true;

                // Create a renderer for PDF that uses Unicode font encoding
                PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(true);

                // Set the MigraDoc document
                pdfRenderer.Document = document;

                // Create the PDF document
                pdfRenderer.RenderDocument();

                // Save the PDF document...
                string filename = PDFFilePath + "\\" + PDFFileName + ".pdf";

                pdfRenderer.Save(filename);
                // ...and start a viewer.
                //  Process.Start(filename);

                return "";

            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }


        //--------------------------------
        //--------------------------------
        private Document CreateDocument()
        {
            // Create a new MigraDoc document
            Document document = new Document();
            //this.document.Info.Title = "A sample invoice";
            //this.document.Info.Subject = "Demonstrates how to create an invoice.";
            //this.document.Info.Author = "Stefan Lange";

            PDFHelper.DefineStyles(document);
            //-------
            section = document.AddSection();
            //-------
            section.PageSetup = PDFHelper.NewDefaultPageSetup();
            //-------
            PDFHelper.HeaderFooter(section, "D:\\GitHub\\ImageReport\\images\\BA-Email-Picture.gif");

            //------------------------------------
            AddImageRows();

            //------------------------------------
            AddSummaryPage();

            return document;
        }

        //---------------------------------------------------------
        private void AddImageRows()
        {
            int iRow = 0;
                    //------------------------------------------ Place images
            for (int i = 0; i < LoadedImgFiles.Count; i++)
            {
                MyImage img1 = (MyImage)LoadedImgFiles[LoadedImgFiles.ElementAt(i).Key];

                if (img1.ImageDesc.Length > 0)
                {
                    //AddSectionImagesWithNotes(section, img1);
                    PDFHelper.AddImageWithTextRow(section, iRow, img1);
                }
                else
                {
                    i++;
                    if (i >= LoadedImgFiles.Count)
                    {
                        //AddSectionImagesWithNotes(section, img1);
                        PDFHelper.AddImageWithTextRow(section, iRow, img1);
                        break;
                    }

                    //---------------------- check second image
                    MyImage img2 = (MyImage)LoadedImgFiles[LoadedImgFiles.ElementAt(i).Key];

                    if (img2.ImageDesc.Length > 0)
                    {
                        //AddSectionImagesWithNotes(section, img1);
                        PDFHelper.AddImageWithTextRow(section, iRow, img1);
                        i--;
                    }
                    else
                    {
                        PDFHelper.AddImageWithoutTextRow(section, iRow, img1, img2);
                        //AddSection2Images(section, img1, img2);
                    }
                }

                if (iRow == 1 && i < LoadedImgFiles.Count - 1) { section.AddPageBreak(); }
                //-------------------------------------------------------------------
                iRow = 1 - iRow;
            }

        }

        
        //---------------------------------------------------------
        private void AddSummaryPage()
        {
            if (ReportSummary.Length > 0)
            {
                section.AddPageBreak();

                PDFHelper.AddSummaryPage(section, ReportSummary);
            }


        }

        ////--------------------------------
        ////--------------------------------
        //private void AddSection2Images(Section section, MyImage img1, MyImage img2)
        //{

        //    MyImage Image1 = new MyImage("D:\\GitHub\\ImageReport\\uploads\\15svvlydysfjjpt5nskxmqyk-bigBen.jpg", Orientation.Portrait);

        //    MyImage Image2 = new MyImage("D:\\GitHub\\ImageReport\\uploads\\qrtmzf1buyjofdqsnqsm5qoy-3789471816_a27524d3da_b.jpg", Orientation.Landscape);

        //    //Image2.ImageNo = "1";
        //    //Image2.ImageName = "Beatiful Rice Lake";
        //    //Image2.ImageDesc = "Beatiful Rice LakeBeatiful Rice LakeBeatiful Rice LakeBeatiful Rice LakeBeatiful Rice Lake Beatiful Rice LakeBeatiful Rice Lake Beatiful Rice Lak \r\n" +
        //    //                    "Beatiful Rice LakeBeatiful Rice LakeBeatiful Rice LakeBeatiful Rice LakeBeatiful Rice Lake Beatiful Rice LakeBeatiful Rice Lake Beatiful Rice Lak \r\n" +
        //    //                    "Beatiful Rice LakeBeatiful Rice LakeBeatiful Rice LakeBeatiful Rice LakeBeatiful Rice Lake Beatiful Rice LakeBeatiful Rice Lake Beatiful Rice Lak \r\n" +
        //    //                    "Beatiful Rice LakeBeatiful Rice LakeBeatiful Rice LakeBeatiful Rice LakeBeatiful Rice Lake Beatiful Rice LakeBeatiful Rice Lake Beatiful Rice Lak \r\n" +
        //    //                    "Beatiful Rice LakeBeatiful Rice LakeBeatiful Rice LakeBeatiful Rice LakeBeatiful Rice Lake Beatiful Rice LakeBeatiful Rice Lake Beatiful Rice Lak \r\n" +
        //    //                    "Beatiful Rice LakeBeatiful Rice LakeBeatiful Rice LakeBeatiful Rice LakeBeatiful Rice Lake Beatiful Rice LakeBeatiful Rice Lake Beatiful Rice Lak \r\n" +
        //    //                    "Beatiful Rice LakeBeatiful Rice LakeBeatiful Rice LakeBeatiful Rice LakeBeatiful Rice Lake Beatiful Rice LakeBeatiful Rice Lake Beatiful Rice Lak \r\n" +
        //    //                    "Beatiful Rice LakeBeatiful Rice LakeBeatiful Rice LakeBeatiful Rice LakeBeatiful Rice Lake Beatiful Rice LakeBeatiful Rice Lake Beatiful Rice Lak \r\n" +
        //    //                    "Beatiful Rice LakeBeatiful Rice LakeBeatiful Rice LakeBeatiful Rice LakeBeatiful Rice Lake Beatiful Rice LakeBeatiful Rice Lake Beatiful Rice Lak \r\n" +
        //    //                    "Beatiful Rice LakeBeatiful Rice LakeBeatiful Rice LakeBeatiful Rice LakeBeatiful Rice Lake Beatiful Rice LakeBeatiful Rice Lake Beatiful Rice Lak \r\n" +
        //    //                    "Beatiful Rice LakeBeatiful Rice LakeBeatiful Rice LakeBeatiful Rice LakeBeatiful Rice Lake Beatiful Rice LakeBeatiful Rice Lake Beatiful Rice Lak \r\n";

        //    //string Image2 = "D:\\GitHub\\ImageReport\\uploads\\wufnjh05rmotd0nuhzi2z3t4-2665839611_378a598be1_c.jpg";

        //    PDFHelper.AddImageWithoutTextRow(section, 0, Image1, Image2);

        //    PDFHelper.AddImageWithTextRow(section, 1, Image2);
        //}

        // //--------------------------------
        ////--------------------------------
        //private void AddSectionImagesWithNotes(Section section, MyImage ImgFilePath)
        //{
        //}
    }
}