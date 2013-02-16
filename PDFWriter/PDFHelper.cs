#region MigraDoc - Creating Documents on the Fly
//
// Authors:
//   PDFsharp Team (mailto:PDFsharpSupport@pdfsharp.de)
//
// Copyright (c) 2001-2009 empira Software GmbH, Cologne (Germany)
//
// http://www.pdfsharp.com
// http://www.migradoc.com
// http://sourceforge.net/projects/pdfsharp
//
// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included
// in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.
#endregion

using System;
using System.Globalization;
using System.Xml;
using System.Xml.XPath;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.Rendering;
using System.Diagnostics;
using System.Xml.Linq;

namespace ImageReport.PDFWriter
{
    public enum ImageRowType
    {
        ImageImage,
        ImageText
    }


    public class MyImage
    {
        public string ImageFile { get; set; }
        public string ImageNo { get; set; }
        public string ImageName { get; set; }
        public string ImageDesc { get; set; }

        public Orientation ImageOrientation { get; set; }

        public MyImage()
        {
        }


        public MyImage(string _ImageFile, Orientation _ImageOrientation)
        {
            ImageFile = _ImageFile;
            ImageOrientation = _ImageOrientation;
        }
    }

  /// <summary>
  /// Creates the invoice form.
  /// </summary>
  public class PDFHelper
  {
    //-----------------------------------
    public PDFHelper()
    {

    }

    #region Page Setup

    //-----------------------------------
    public static PageSetup NewDefaultPageSetup()
    {
        PageSetup p = new PageSetup();
        p.PageFormat = PageFormat.Letter;
        p.SectionStart = BreakType.BreakNextPage;
        p.Orientation = Orientation.Portrait;
        //p.PageWidth = "21cm";
        //p.PageHeight = "29.7cm";
        p.TopMargin = "0.5cm";
        p.BottomMargin = "1cm";
        p.LeftMargin = "0.5cm";
        p.RightMargin = "0.5cm";
        p.HeaderDistance = "1.25cm";
        p.FooterDistance = "1.25cm";
        p.OddAndEvenPagesHeaderFooter = false;
        p.DifferentFirstPageHeaderFooter = false;
        p.MirrorMargins = false;
        p.HorizontalPageBreak = false;
        return p;
    }
    #endregion

    #region Define style
    /// <summary>
    /// Defines the styles used to format the MigraDoc document.
    /// </summary>
    /// /// //--------------------------------
    public static void DefineStyles(Document document)
    {
      // Get the predefined style Normal.
      Style style = document.Styles["Normal"];
      // Because all styles are derived from Normal, the next line changes the 
      // font of the whole document. Or, more exactly, it changes the font of
      // all styles and paragraphs that do not redefine the font.

      style.Font.Name = "Verdana";

      style = document.Styles[StyleNames.Header];
      style.ParagraphFormat.AddTabStop("10cm", TabAlignment.Right);

      style = document.Styles[StyleNames.Footer];
      style.ParagraphFormat.AddTabStop("4cm", TabAlignment.Center);

      // Create a new style called Table based on style Normal
      style = document.Styles.AddStyle("Table", "Normal");
      style.Font.Name = "Verdana";
      style.Font.Name = "Times New Roman";
      style.Font.Size = 9;

      // Create a new style called Reference based on style Normal
      style = document.Styles.AddStyle("Reference", "Normal");
      style.ParagraphFormat.SpaceBefore = "5mm";
      style.ParagraphFormat.SpaceAfter = "5mm";
      style.ParagraphFormat.TabStops.AddTabStop("16cm", TabAlignment.Right);
    }

    #endregion

    #region Image Row
    //----------------------------------------------------
    //----------------------------------------------------
    public static void AddImageWithTextRow(MigraDoc.DocumentObjectModel.Section section, int iRow, MyImage Image1)
    { 
        Paragraph paragraph = section.AddParagraph();
        paragraph.Format.SpaceBefore = new Unit(3 * (1 - iRow) + .5 * iRow, UnitType.Centimeter);

        //------------------------------------
        Table tbl = AddTable4Image(section, ImageRowType.ImageText);

        //--------- frist row: image name and number
        Row row = tbl.AddRow();
        row.HeadingFormat = true;
        row.Format.Alignment = ParagraphAlignment.Left;
        row.Format.Font.Bold = true;

        //row.Cells[0].MergeRight = 2;
        row.Cells[0].AddParagraph("Image #:" + Image1.ImageNo + "     -     " + Image1.ImageName);

        if (Image1.ImageDesc.Length > 0)
        {
            row.Cells[1].AddParagraph("Inspection / Recommendation:");
        }
        else
        {
            row.Cells[1].AddParagraph("--");
        }
        //AddIamge(row.Cells[0], Image1);

       // second row
        row = tbl.AddRow();
        row.HeadingFormat = true;
        row.Format.Alignment = ParagraphAlignment.Left;
        row.Format.Font.Bold = false;
        AddIamge(row.Cells[0], Image1, "10.5cm");

        if (Image1.ImageDesc.Length > 0)
        {
            Paragraph prg = row.Cells[1].AddParagraph(RenderDescXML(Image1.ImageDesc));
            prg.Format.Font.Size = 8;
            row.Cells[1].Format.Alignment = ParagraphAlignment.Left;
        }
   
    }

    //----------------------------------------------------
    //----------------------------------------------------
    public static void AddImageWithoutTextRow(MigraDoc.DocumentObjectModel.Section section, int iRow, MyImage Image1, MyImage Image2)
   // public static void AddImageWithoutTextRow(MigraDoc.DocumentObjectModel.Section section)
    {
        Paragraph paragraph = section.AddParagraph();
        paragraph.Format.SpaceBefore = new Unit(2.5 * (1 - iRow) + .3 * iRow, UnitType.Centimeter);

        //------------------------------------
        Table tbl = AddTable4Image(section, ImageRowType.ImageImage);

        // Create the header of the table
        Row row = tbl.AddRow();
        row.HeadingFormat = true;
        row.Format.Alignment = ParagraphAlignment.Center;
        row.Format.Font.Bold = true;
        
        //
        row.Cells[0].AddParagraph("Image #:" + Image1.ImageNo + "     -     " + Image1.ImageName);
        row.Cells[1].AddParagraph("Image #:" + Image2.ImageNo + "     -     " + Image2.ImageName);
        //AddIamge(row.Cells[0], Image1);

        // second row
        row = tbl.AddRow();
        row.HeadingFormat = true;
        row.Format.Alignment = ParagraphAlignment.Left;
        row.Format.Font.Bold = false;

        AddIamge(row.Cells[0], Image1, "9.8cm");

        AddIamge(row.Cells[1], Image2, "9.8cm");

    }

    //----------------------------------------------------
    //----------------------------------------------------
    public static void AddSummaryPage(MigraDoc.DocumentObjectModel.Section section, string Summary)
    {
        Paragraph paragraph = section.AddParagraph();
        paragraph.Format.SpaceBefore = new Unit(4, UnitType.Centimeter);

        //------------------------------------
        Table tbl = AddTable4Summary(section);

        //--------- frist row: image name and number
        Row row = tbl.AddRow();
        row.HeadingFormat = true;
        row.Format.Alignment = ParagraphAlignment.Left;
        row.Format.Font.Bold = true;

        row.Cells[0].AddParagraph("Roof Overview");

        // second row
        row = tbl.AddRow();
        row.HeadingFormat = true;
        row.Format.Alignment = ParagraphAlignment.Left;
        row.Format.Font.Bold = false;

        row.Cells[0].AddParagraph(Summary);

    }

    //----------------------------------------
    private static string RenderDescXML(string XMLdesc)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(XMLdesc);
        
        XmlNode node = xmlDoc.FirstChild;

        return "Observation: \r\n" + node.SelectSingleNode("Observation").InnerText + "\r\n\r\n" +
               "Deficiency: \r\n" + node.SelectSingleNode("Deficiency").InnerText + "\r\n\r\n" +
               "Recommendation: \r\n" + node.SelectSingleNode("Recommendation").InnerText;
    }


    //-------------------------------------
    private static Table AddTable4Image(MigraDoc.DocumentObjectModel.Section section, ImageRowType ImgRowType)
    {
       Table tbl = section.AddTable();

        tbl.Style = "Table";
        tbl.Borders.Color = Colors.Black;
        tbl.Borders.Width = 0.25;
        tbl.Borders.Left.Width = 0.5;
        tbl.Borders.Right.Width = 0.5;
        tbl.Rows.LeftIndent = 0;

        string Col1Width = "10.25cm";
        string Col2Width = "10.25cm";

        if (ImgRowType == ImageRowType.ImageText)
        {
            Col1Width = "11cm";
            Col2Width = "9.5cm";
        }

        // Before you can add a row, you must define the columns
        Column column = tbl.AddColumn(Col1Width);
        column.Format.Alignment = ParagraphAlignment.Center;

        column = tbl.AddColumn(Col2Width);
        column.Format.Alignment = ParagraphAlignment.Right;

        return tbl;
    }

    //--------------------------------------------------------------
      private static Table AddTable4Summary(MigraDoc.DocumentObjectModel.Section section)
    {
        Table tbl = section.AddTable();

        //tbl.Style = "Table";
        //tbl.Borders.Color = Colors.Black;
        //tbl.Borders.Width = 0.25;
        //tbl.Borders.Left.Width = 0.5;
        //tbl.Borders.Right.Width = 0.5;
        //tbl.Rows.LeftIndent = 0;

        //string Col1Width = "10.25cm";
        //string Col2Width = "10.25cm";

        //if (ImgRowType == ImageRowType.ImageText)
        //{
        //    Col1Width = "11cm";
        //    Col2Width = "9.5cm";
        //}

        // Before you can add a row, you must define the columns
        Column column = tbl.AddColumn("20.25cm");
        column.Format.Alignment = ParagraphAlignment.Center;

        return tbl;
    }

    //---------------------------
    private static void AddIamge(Cell cell, MyImage ImageFile, string ImageWidth)
    {
        //row.Shading.Color = TableBlue;
        Image img = cell.AddImage(ImageFile.ImageFile);

        if (ImageFile.ImageOrientation == Orientation.Landscape)
        { img.Width = ImageWidth; }
        else
        { img.Height = "9.8cm"; }

        //img.Height = "9cm";
        img.LockAspectRatio = true;

        cell.Format.Font.Bold = false; 
        cell.Format.Alignment =  ParagraphAlignment.Center;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }

    #endregion

    #region Header footer
    //----------------------------------------------------
    //----------------------------------------------------
    public static void HeaderFooter(MigraDoc.DocumentObjectModel.Section section, string LogoImage)
    {

        //TextFrame textFrame = section.Headers.Primary.AddTextFrame();
        //textFrame.Height = "0.2cm";
        //textFrame.Width = "6cm";
        //textFrame.FillFormat.Color = Colors.SlateGray;
        //textFrame.MarginTop = "1cm";
        
        //textFrame.WrapFormat.DistanceLeft = new Unit(14.5, UnitType.Centimeter);
        //textFrame.WrapFormat.DistanceTop = new Unit(-3, UnitType.Millimeter);

        //TextFrame tf2 = section.Headers.Primary.AddTextFrame();
        //tf.Height = "0.1cm";
        //tf.Width = "21cm";
        //tf.FillFormat.Color = Colors.White;

        // Put a logo in the header
        Image image = section.Headers.Primary.AddImage(LogoImage);
        image.Height = "0.85cm";
        image.LockAspectRatio = true;
        image.RelativeVertical = RelativeVertical.Line;
        image.RelativeHorizontal = RelativeHorizontal.Margin;
        image.Top = ShapePosition.Top;
        image.Left = ShapePosition.Left;
        image.WrapFormat.Style = WrapStyle.Through;
        image.WrapFormat.DistanceTop = new Unit(3, UnitType.Millimeter);

        // Create headr
        Paragraph parHeader = section.Headers.Primary.AddParagraph();
        parHeader.AddText("BOTHWELL-ACCURATE CO. INC. ");
        parHeader.AddLineBreak();
        parHeader.AddText("QUOTATION ");
        parHeader.Format.Alignment = ParagraphAlignment.Right;


        // Create footer
        Paragraph paragraph = section.Footers.Primary.AddParagraph();
        paragraph.AddText("BOTHWELL-ACCURATE EST 1927");
        paragraph.AddLineBreak();
        paragraph.AddText("baroof.com");

        paragraph.Format.Font.Size = 8;
        paragraph.Format.Font.Color = Colors.White;
        paragraph.Format.Alignment = ParagraphAlignment.Center;

        paragraph.Format.Shading.Color = Colors.Black;

        //// Create the text frame for the address
        //this.addressFrame = section.AddTextFrame();
        //this.addressFrame.Height = "3.0cm";
        //this.addressFrame.Width = "7.0cm";
        //this.addressFrame.Left = ShapePosition.Left;
        //this.addressFrame.RelativeHorizontal = RelativeHorizontal.Margin;
        //this.addressFrame.Top = "5.0cm";
        //this.addressFrame.RelativeVertical = RelativeVertical.Page;

        //// Put sender in address frame
        //paragraph = this.addressFrame.AddParagraph("BOTHWELL-ACCURATE EST 1927");
        //paragraph.Format.Font.Name = "Times New Roman";
        //paragraph.Format.Font.Size = 7;
        //paragraph.Format.SpaceAfter = 3;

        //// Add the print date field
        //paragraph = section.AddParagraph();
        //paragraph.Format.SpaceBefore = "8cm";
        //paragraph.Style = "Reference";
        //paragraph.AddFormattedText("INVOICE", TextFormat.Bold);
        //paragraph.AddTab();
        //paragraph.AddText("Cologne, ");
        //paragraph.AddDateField("dd.MM.yyyy");
    }

    #endregion

  }
}
