using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.Dictionaries;
using ZPDSGGW.Enums;
using ZPDSGGW.Repository;
using ZPDSGGW.Models;

namespace ZPDSGGW.Services
{
    public class FileService: Controller
    {
        private readonly IWebHostEnvironment _environment;

        public FileService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<ActionResult> DownloadFile(string pathToFile)
        {
            var ms = new MemoryStream();
            using (var stream = new FileStream(pathToFile, FileMode.Open))
            {
                await stream.CopyToAsync(ms);
            }
            ms.Position = 0;
            var extension = Path.GetExtension(pathToFile).ToLower();
            return File(ms, MimeTypeExtension.GetMimeType()[extension], Path.GetFileName(pathToFile));
        }
        public async Task<string> Uploadfile([FromForm] Models.FormFile objFile, DocumentKind documentKind)
        {
            try
            {

                if (objFile.file.Length > 0)
                {
                    switch (documentKind)
                    {
                        case DocumentKind.thesis:
                            var path = _environment.WebRootPath + "\\" + DocumentKind.thesis.ToString() + "\\" + objFile.file.FileName;
                            if (!Directory.Exists(_environment.WebRootPath + "\\" + DocumentKind.thesis.ToString()))
                                Directory.CreateDirectory(_environment.WebRootPath + "\\" + DocumentKind.thesis.ToString());
                            if (System.IO.File.Exists(_environment.WebRootPath + "\\" + DocumentKind.thesis.ToString() + "\\" + objFile.file.FileName))
                                Console.WriteLine("plik został nadpisany");
                            //walidacja nazwy pliku
                            using (FileStream fs = System.IO.File.Create(path))
                            {
                                objFile.file.CopyTo(fs);
                                fs.Flush();
                                return path;
                            }
                            break;
                        case DocumentKind.proposal:
                            var path2 = _environment.WebRootPath + "\\" + DocumentKind.proposal.ToString() + "\\" + objFile.file.FileName;
                            if (!Directory.Exists(_environment.WebRootPath + "\\" + DocumentKind.proposal.ToString()))
                                Directory.CreateDirectory(_environment.WebRootPath + "\\" + DocumentKind.proposal.ToString());
                            if (System.IO.File.Exists(_environment.WebRootPath + "\\" + DocumentKind.proposal.ToString() + "\\" + objFile.file.FileName))
                                Console.WriteLine("plik został nadpisany");
                            //walidacja nazwy pliku
                            using (FileStream fs = System.IO.File.Create(path2))
                            {
                                objFile.file.CopyTo(fs);
                                fs.Flush();
                               return path2;
                            }
                            break;
                        case DocumentKind.paymentConfirmation:
                            var path3 = _environment.WebRootPath + "\\" + DocumentKind.paymentConfirmation.ToString() + "\\" + objFile.file.FileName;
                            if (!Directory.Exists(_environment.WebRootPath + "\\" + DocumentKind.paymentConfirmation.ToString()))
                                Directory.CreateDirectory(_environment.WebRootPath + "\\" + DocumentKind.paymentConfirmation.ToString());
                            if (System.IO.File.Exists(_environment.WebRootPath + "\\" + DocumentKind.paymentConfirmation.ToString() + "\\" + objFile.file.FileName))
                                Console.WriteLine("plik został nadpisany");
                            //walidacja nazwy pliku
                            using (FileStream fs = System.IO.File.Create(path3))
                            {
                                objFile.file.CopyTo(fs);
                                fs.Flush();
                                return path3;
                            }
                            break;
                        default:
                            var path4 = _environment.WebRootPath + "\\" + DocumentKind.Inne.ToString() + "\\" + objFile.file.FileName;
                            if (!Directory.Exists(_environment.WebRootPath + "\\" + DocumentKind.Inne.ToString()))
                                Directory.CreateDirectory(_environment.WebRootPath + "\\" + DocumentKind.Inne.ToString());
                            if (System.IO.File.Exists(_environment.WebRootPath + "\\" + DocumentKind.Inne.ToString() + "\\" + objFile.file.FileName))
                                Console.WriteLine("plik został nadpisany");
                            //walidacja nazwy pliku
                            using (FileStream fs = System.IO.File.Create(path4))
                            {
                                objFile.file.CopyTo(fs);
                                fs.Flush();
                                return path4;
                            }
                            break;
                    }
                }
                else
                    throw new FileNotFoundException("Nie znaleziono pliku");
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
    }
}
