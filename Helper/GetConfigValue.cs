using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImageReport.Helper
{
    // by Fred, 1-19-2013, 1-20-2013
    public class GetConfigValue
    {
        //--------------------------------------
        static public string Passcode
        {
            get
            {
                string value = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Passcode"]);
                return value;
            }
        }

        //--------------------------------------
        static public int MaxWidth
        {
            get
            {
                int value = 1000;

                try
                {
                    value = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ImageMaxWidth"]); 
                }
                catch(Exception ex)
                {  
                }

                return value;
            }
        }

        //--------------------------------------
        static public int MaxHeight
        {
            get
            {
                int value = 1000;

                try
                {
                    value = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ImageMaxHeight"]);
                }
                catch (Exception ex)
                {
                }

                return value;
            }
        }

        //--------------------------------------
        static public string UploadPath
        {
            get
            {
                string value = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["UploadPath"]);
                return value;
            }
        }

        //--------------------------------------
        static public string PDFPath
        {
            get
            {
                string value = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["PDFPath"]);
                return value;
            }
        }
    }
}