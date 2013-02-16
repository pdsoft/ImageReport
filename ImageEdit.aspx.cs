using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.IO;
using System.Data;
using ImageReport.Helper;

namespace ImageReport
{
    public partial class ImageEdit : System.Web.UI.Page
    {
        private int columns;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                GenerateImageTable();

                PopulateDropdown4ImageText();
            }
            else
            {
                if (_Action.Value == "REMOVE")
                {
                    RemoveFileNamefromList(_FileName.Value);
                    GenerateImageTable();
                }

                _Action.Value = "";
            }
        }

        //------------------------------------------------
        private void GenerateImageTable()
        {
            //--------------------------------------
            columns = Convert.ToInt16(DropDownList1.SelectedValue);

            IDictionary<string, string> LoadedImgFiles =
                       (IDictionary<string, string>)Session["LoadedIamges"];

            //-------------------------------------
            if (LoadedImgFiles.Count == 0)
            {
                ColumnOption.Style["display"] = "none";
                divTable.InnerText = "No images has been selected !";
                return;
            }
            
            //------------------------------------
            Table imgTable = new Table();

            imgTable.CellPadding = 10;
            imgTable.BorderStyle = BorderStyle.Solid;
            imgTable.BorderColor = System.Drawing.Color.Black;
            imgTable.GridLines = GridLines.Both;

            for (int i = 0; i < LoadedImgFiles.Count; i = i + columns)
            {
                TableRow tr = new TableRow();

                for (int j = 0; j < columns; j++)
                {
                    if (i + j < LoadedImgFiles.Count)
                    {
                        var pair = LoadedImgFiles.ElementAt(i + j);
                        tr.Cells.Add(AddImageCell(pair.Key.ToString(), pair.Value.ToString(), (i+j+1).ToString()));
                    }
                    else
                    {
                        tr.Cells.Add(AddEmptyCell());
                    }
                }

                imgTable.Rows.Add(tr);
            }

            divTable.Controls.Add(imgTable);
        }

        //------------------------------
        private TableCell AddEmptyCell()
        {
            return new TableCell();
        }

        //----------------------------
        private void RemoveFileNamefromList(string FileName)
        {

            IDictionary<string, string> LoadedImgFiles
                        = (IDictionary<string, string>)Session["LoadedIamges"];

            LoadedImgFiles.Remove(FileName);

            Session["LoadedIamges"] = LoadedImgFiles;
        }

        //--------------------------------------------------------
        //--------------------------------------------------------
        private TableCell AddImageCell(string FileName, string txtNotes, string ImageNo)
        {
            TableCell tc = new TableCell();

            tc.Controls.Add(AddOneImage(FileName, txtNotes, ImageNo));

            return tc;
        }

        //--------------------------------------------------------
        //--------------------------------------------------------
        private Table AddOneImage(string FileName, string txtNotes, string ImageNo)
        {
            Table tbl = new Table();
            tbl.CellPadding = 0;
            tbl.CellSpacing = 0;
            tbl.BorderStyle = BorderStyle.None;

            //-------------------------------------
            TableRow tr = new TableRow();

            ////-----------------------------------
            //tbl.Rows.Add(tr2);

            //TableRow tr1 = new TableRow();

            //-------------------------------------
            TableCell tc = new TableCell();
            tc.CssClass = "dragdrop";           // by Fred, 2-13-2013

            string tempPath = GetConfigValue.UploadPath;    // by Fred, 1-20-2013

            Image img = new Image();

            img.CssClass = "dragdrop";          // by Fred, 2-13-2013
            img.ImageUrl = @"\" + tempPath + @"\" + FileName;
            img.Style["width"] = Convert.ToString(1100 / columns) + "px";
            img.Attributes["ondblclick"] = "ShowEditTextbox(" + ImageNo + ",\"" + FileName + "\")";
            img.ID = "img" + ImageNo;
            img.Attributes["FileName"] = FileName;          // by Fred, 2-13-2013
            img.Attributes["ImageNo"] = ImageNo;          // by Fred, 2-14-2013

            tc.Controls.Add(img);
            tr.Cells.Add(tc);

            //--------------------------------
            TableCell tc2 = new TableCell();
            tc2.Controls.Add(Icons(FileName, ImageNo));
            tc2.VerticalAlign = VerticalAlign.Top;
            tc2.HorizontalAlign = HorizontalAlign.Left;

            tr.Cells.Add(tc2);
            tbl.Rows.Add(tr);

            return tbl;
        }

        //---------------------------------------------------
        private HtmlGenericControl Icons(string FileName, string ImageNo)
        {
            HtmlGenericControl icons = new HtmlGenericControl();

            string img1 = "<img alt='' class='edit' src='images/edit.gif' Style='cursor:pointer' onclick='ShowEditTextbox(" + ImageNo + ",\"" + FileName + "\")'  />";      // by Fred, add class, 2-14-2013
            string img2 = "<img alt='' class='delete' src='images/del.gif' Style='cursor:pointer' onclick='RemoveImage(" + ImageNo + ",\"" + FileName + "\")'  />";         // by Fred, add class, 2-14-2013

            icons.InnerHtml = "&nbsp;" + ImageNo.ToString() + "<br />" + img1 + "<br />" + img2;

            return icons;
        }


        //----------------------------------------------------
        protected void PopulateDropdown4ImageText()
        {
            string FilePicklist = Server.MapPath("Picklist.txt");

            //------------------------ convert to DataTable
            using (StreamReader sr = new StreamReader(FilePicklist))
            {
                string line = sr.ReadToEnd();
                string[] ar = line.Split(';');

                ddImageTextList.DataSource = GetDataTable(ar);
                ddImageTextList.DataTextField = "Text"; // displayed text in UI
                ddImageTextList.DataValueField = "Value"; 

                ddImageTextList.DataBind();
            }
        }

        //---------------------------------------------
        protected DataTable GetDataTable(string[] list)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Text", typeof(string));
            dt.Columns.Add("Value", typeof(string));

            for (int i = 0; i < list.Length; i++)
            {
                dt.Rows.Add(GetTableFields(list[i]));
            }

            return dt;
        }

        //---------------------------------------------
        protected string[] GetTableFields(string str)
        {
            string[] fields = new string[2];

            if (str.IndexOf(":") < 0)
            {
                fields[0] = str;
                fields[1] = str;
            }
            else
            {
                fields = str.Split(':');
                if (fields[0] == null) fields[0] = "";
                if (fields[1] == null) fields[0] = "";
            }

            return fields;
        }

        //----------------------------------------------------
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerateImageTable();
        }

        //----------------------------------------------------
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static void SubmitNewDesc(string fileName, string Desc) 
        {
            IDictionary<string, string> LoadedImgFiles
                        = (IDictionary<string, string>)HttpContext.Current.Session["LoadedIamges"];

            LoadedImgFiles[fileName] = Desc;

            HttpContext.Current.Session["LoadedIamges"] = LoadedImgFiles;
        }

        //----------------------------------------------------
        [WebMethod(EnableSession = true)]
        public static string GetDescription(string fileName) 
        {
            IDictionary<string, string> LoadedImgFiles
                        = (IDictionary<string, string>)HttpContext.Current.Session["LoadedIamges"];

          return LoadedImgFiles[fileName].ToString();
        }

        // by Fred, 2-13-2013
        [WebMethod(EnableSession = true)]
        public static void SwitchImage(string img1, string img2)
        {
            IDictionary<string, string> temp = new Dictionary<string, string>();
            IDictionary<string, string> LoadedImgFiles
                        = (IDictionary<string, string>)HttpContext.Current.Session["LoadedIamges"];

            // re-create the Dictionary
            for (int i = 0; i < LoadedImgFiles.Count; i++)
            {
                var pair = LoadedImgFiles.ElementAt(i);

                if (pair.Key == img1)
                {
                    temp.Add(img2, LoadedImgFiles[img2]);
                }
                else if (pair.Key == img2)
                {
                    temp.Add(img1, LoadedImgFiles[img1]);
                }
                else
                {
                    temp.Add(pair.Key, pair.Value);
                } 
            }

            HttpContext.Current.Session["LoadedIamges"] = temp;
        }
    }
}