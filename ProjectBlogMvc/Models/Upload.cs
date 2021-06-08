using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBlogMvc.Models
{
    public class Upload
    {
        [Obsolete]
        public static string ImageUpload(IFormFile file, IHostingEnvironment _env, out bool result)
        {
            result = false;
            var uploads = Path.Combine(_env.WebRootPath, "upload");



            if (file.ContentType.Contains("image"))
            {
                if (file.Length <= 2097152)
                {
                    string uniqueName = $"{Guid.NewGuid().ToString().Replace("-", "_").ToLower()}.{file.ContentType.Split('/')[1]}";

                    var filePath = Path.Combine(uploads, uniqueName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                        result = true;
                        return filePath.Substring(filePath.IndexOf("\\uploads\\"));
                        //\uploads\fileName.jpg
                    }
                }

                else
                {
                    return $"2 MB'tan büyük boyutta resim yükleyemezsiniz";
                }
            }
            else
            {
                return $"Lütfen sadece resim dosyası yükleyiniz!";
            }
        }
    }
}
