using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsFeedApplication.Utils
{
    public class Logger
    {
        public static void AddException(Exception inputData)
        {
            String fileName = DateTime.Now.ToString("yyyyMMdd") + ".txt";
            string filePath = ConfigurationManager.AppSettings["LogFileFolderPath"];
            string fullPath = Path.Combine(filePath, fileName);

            // Check if the directory exists, if not, create it
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            using (StreamWriter writer = new StreamWriter(fullPath, true))
            {
                Exception realerror = inputData;
                while (realerror.InnerException != null)
                {
                    realerror = realerror.InnerException;
                    writer.WriteLine(realerror.ToString());
                }
            }
        }
    }
}
