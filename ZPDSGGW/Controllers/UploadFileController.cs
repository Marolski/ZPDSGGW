using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.Dictionaries;
using ZPDSGGW.DTOs.FileDto;
using ZPDSGGW.Enums;
using ZPDSGGW.Repository;
using ZPDSGGW.Models;
using ZPDSGGW.Services;

namespace ZPDSGGW.Controllers
{
    [Route("api/upload")]
    [ApiController]
    public class UploadFileController: ControllerBase
    {
        private readonly ILogger<UploadFileController> _logger;
        private readonly IRepositoryFile _repository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;

        public UploadFileController(IRepositoryFile repository, IWebHostEnvironment environment)
        {
            _repository = repository;
            _environment = environment;
        }

        [HttpGet]
        public ActionResult<FileReadDto> GetFileByPath(string pathToFile)
        {
            
        }

        [HttpPost]
        public ActionResult<FileCreateDto> UploadThesis([FromForm] System.IO.File objFile, FileCreateDto createFile)
        {
            FileService service = new FileService(_environment);
            service.Uploadfile(objFile,createFile.DocumentKind)
            var fileModel = _mapper.Map<ZPDSGGW.Models.File>(createFile);
        }
    }
}
