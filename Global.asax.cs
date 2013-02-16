using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using ImageReport.Helper;
using ImageReport.PDFWriter;

namespace ImageReport
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {
            IDictionary<string, MyImage> LoadedImgFiles = new Dictionary<string, MyImage>();
            Session["LoadedIamges"] = LoadedImgFiles;

            // clear the folder, by Fred, 1-19-2013
            DiskFileHelper.ClearFolder(Server.MapPath(GetConfigValue.UploadPath));
          //  DiskFileHelper.ClearFolder(Server.MapPath(GetConfigValue.PDFPath));
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {
            //------------------ delete all image files uploaded by this session. GL
            //string tempPath = System.Configuration.ConfigurationManager.AppSettings["FolderPath"];

            //string filePath = Server.MapPath(tempPath);

            //if (System.IO.Directory.Exists(filePath))
            //{
            //    System.IO.File.Delete(filePath + "/" + Session.SessionID + "*.*");
            //}
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}