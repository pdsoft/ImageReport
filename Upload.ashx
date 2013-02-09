<%@ WebHandler Language="C#" Class="Upload" %>

using System;
using System.Web;
using System.IO;
using System.Web.SessionState;
using System.Collections.Generic;
using ImageReport.Helper;

public class Upload : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Expires = -1;
        try
        {
            HttpPostedFile postedFile = context.Request.Files["Filedata"];

            string savepath = "";
            //string tempPath = "";

           // tempPath = System.Configuration.ConfigurationManager.AppSettings["UploadPath"];

           // savepath = context.Server.MapPath(tempPath);
            savepath = context.Server.MapPath(GetConfigValue.UploadPath);       // by Fred, 1-19-2013

            //------------------ Use session ID to avoid duplicate file name by different users
            string SessionId = HttpContext.Current.Session.SessionID;

            string filename = SessionId + '-' + postedFile.FileName;

            //--------------- save the file to session for report usage
            IDictionary<string, string> LoadedImgFiles =
                        (IDictionary<string, string>)HttpContext.Current.Session["LoadedIamges"];

            if (LoadedImgFiles.ContainsKey(filename) == false)
            {
                LoadedImgFiles.Add(filename, "");
                HttpContext.Current.Session["LoadedIamges"] = LoadedImgFiles;

                //--------------------------
                if (!Directory.Exists(savepath))
                    Directory.CreateDirectory(savepath);

                //postedFile.SaveAs(savepath + @"\" + filename);

                ImageHelper.ResizeImage(postedFile, savepath + @"\" + filename);      // by Fred, 1-19-2013
                
                // context.Response.Write(tempPath + "/" + filename);
                context.Response.StatusCode = 200;
            }

            context.Response.End();
        }
        catch (Exception ex)
        {
            context.Response.Write("Error: " + ex.Message);
        }
    }

    //--------------------------------------------------
    public bool IsReusable {
        get {
            return false;
        }
    }
}