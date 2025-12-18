using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talapat.Infrastructure.Helpers.UploadHandler
{
    public class UploadHandler
    {
        public string Upload(IFormFile file)
        {
            //extention
            List<string> validExtention = new List<string>() { ".jpg", ".png", ".gif" };
            
            string extention = Path.GetExtension(file.FileName);

            if (!validExtention.Contains(extention)) 
            {
                return $"Extention is not valid ({string.Join(',', validExtention)})";
            }
            // file size
            long size = file.Length;
            if (size > 5 * 1024 * 1024)
                return "maximum size can be 5mb";

            //name changing
            string fileName = Guid.NewGuid().ToString() + extention;
            string uploadDirectory = "wwwroot/images/Uploaded Pics"; // or any directory you want to save files in
            string filePath = Path.Combine(uploadDirectory, fileName);
           using FileStream stream = new FileStream(filePath + fileName, FileMode.Create);
            file.CopyTo(stream);
            return fileName;
            

        }
    }
}
