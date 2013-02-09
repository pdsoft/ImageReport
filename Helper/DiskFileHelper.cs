using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace ImageReport.Helper
{
    // by Fred, 1-19-2013
    public class DiskFileHelper
    {
        static public void ClearFolder(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);

            foreach (FileInfo file in directory.GetFiles())
            {
                if (file.CreationTime.Date.CompareTo(DateTime.Now.Date) < 0 &&
                    (file.Extension.ToUpperInvariant() == ".JPG" ||
                    file.Extension.ToUpperInvariant() == ".JPEG" ||
                    file.Extension.ToUpperInvariant() == ".PNG" ||
                    file.Extension.ToUpperInvariant() == ".GIF" ||
                    file.Extension.ToUpperInvariant() == ".BMP" ||
                    file.Extension.ToUpperInvariant() == ".PDF"))
                {
                    file.Delete();
                }
            }
        }
    }
}