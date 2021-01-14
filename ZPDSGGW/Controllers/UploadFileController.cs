using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.Enums;

namespace ZPDSGGW.Controllers
{
    [Route("api/upload")]
    [ApiController]
    public class UploadFileController: ControllerBase
    {
        private readonly IWebHostEnvironment _environment;

        public UploadFileController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public class File
        {
            public IFormFile file { get; set; }
        }

        [HttpPost]
        public async Task<string> UploadThesis([FromForm]File objFile, DocumentKind documentKind)
        {
            try
            {
                
                if (objFile.file.Length > 0)
                {
                    switch (documentKind)
                    {
                        case DocumentKind.thesis:
                            if (!Directory.Exists(_environment.WebRootPath + "\\" + DocumentKind.thesis.ToString()))
                                Directory.CreateDirectory(_environment.WebRootPath + "\\" + DocumentKind.thesis.ToString());
                            if (System.IO.File.Exists(_environment.WebRootPath + "\\" + DocumentKind.thesis.ToString() + "\\" + objFile.file.FileName))
                                Console.WriteLine("plik został nadpisany");
                            //walidacja nazwy pliku
                            using (FileStream fs = System.IO.File.Create(_environment.WebRootPath +"\\"+ DocumentKind.thesis.ToString() +"\\"+ objFile.file.FileName))
                            {
                                objFile.file.CopyTo(fs);
                                fs.Flush();
                                return "\\" + DocumentKind.thesis.ToString() + "\\" + objFile.file.FileName;
                            }
                            break;
                        case DocumentKind.proposal:
                            if (!Directory.Exists(_environment.WebRootPath + "\\" + DocumentKind.proposal.ToString()))
                                Directory.CreateDirectory(_environment.WebRootPath + "\\" + DocumentKind.proposal.ToString());
                            if (System.IO.File.Exists(_environment.WebRootPath + "\\" + DocumentKind.proposal.ToString() + "\\" + objFile.file.FileName))
                                Console.WriteLine("plik został nadpisany");
                            //walidacja nazwy pliku
                            using (FileStream fs = System.IO.File.Create(_environment.WebRootPath + "\\" + DocumentKind.proposal.ToString() + "\\" + objFile.file.FileName))
                            {
                                objFile.file.CopyTo(fs);
                                fs.Flush();
                                return "\\" + DocumentKind.proposal.ToString() + "\\" + objFile.file.FileName;
                            }
                            break;
                        case DocumentKind.paymentConfirmation:
                            if (!Directory.Exists(_environment.WebRootPath + "\\" + DocumentKind.paymentConfirmation.ToString()))
                                Directory.CreateDirectory(_environment.WebRootPath + "\\" + DocumentKind.paymentConfirmation.ToString());
                            if (System.IO.File.Exists(_environment.WebRootPath + "\\" + DocumentKind.paymentConfirmation.ToString() + "\\" + objFile.file.FileName))
                                Console.WriteLine("plik został nadpisany");
                            //walidacja nazwy pliku
                            using (FileStream fs = System.IO.File.Create(_environment.WebRootPath + "\\" + DocumentKind.paymentConfirmation.ToString() + "\\" + objFile.file.FileName))
                            {
                                objFile.file.CopyTo(fs);
                                fs.Flush();
                                return "\\" + DocumentKind.paymentConfirmation.ToString() + "\\" + objFile.file.FileName;
                            }
                            break;
                        default:
                            if (!Directory.Exists(_environment.WebRootPath + "\\" + DocumentKind.Inne.ToString()))
                                Directory.CreateDirectory(_environment.WebRootPath + "\\" + DocumentKind.Inne.ToString());
                            if (System.IO.File.Exists(_environment.WebRootPath + "\\" + DocumentKind.Inne.ToString() + "\\" + objFile.file.FileName))
                                Console.WriteLine("plik został nadpisany");
                            //walidacja nazwy pliku
                            using (FileStream fs = System.IO.File.Create(_environment.WebRootPath + "\\" + DocumentKind.Inne.ToString() + "\\" + objFile.file.FileName))
                            {
                                objFile.file.CopyTo(fs);
                                fs.Flush();
                                return "\\" + DocumentKind.Inne.ToString() + "\\" + objFile.file.FileName;
                            }
                            break;
                    }
                }
                else
                    throw new FileNotFoundException("Nie znaleziono pliku");
            }
            catch (Exception ex )
            {
                return ex.Message.ToString();
            }
        }
    }
}
