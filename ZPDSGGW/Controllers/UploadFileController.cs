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
using File = ZPDSGGW.Models.File;
using Microsoft.AspNetCore.JsonPatch;

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

        public UploadFileController(IRepositoryFile repository, IWebHostEnvironment environment, IMapper mapper, ILogger<UploadFileController> logger)
        {
            _repository = repository;
            _environment = environment;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("{id}", Name = "GetFileById")]
        public async Task<ActionResult> GetFileById(Guid id)
        {
            var pathToFile = _repository.GetPathById(id);
            FileService service = new FileService(_environment);
            return await service.DownloadFile(pathToFile);
        }

        [HttpGet]
        public ActionResult<IEnumerable<FileReadDto>> GetFileModel(Guid id)
        {
            var file = _repository.GetFileById(id);
            if (file != null)
                return Ok(_mapper.Map<IEnumerable<FileReadDto>>(file));
            return NotFound();
        }

        [HttpPost]
        public ActionResult<FileReadDto> UploadThesis([FromForm] Models.FormFile objFile, DocumentKind documentKind, bool accepted, Guid userId)
        {
            FileService service = new FileService(_environment);
            var path = service.Uploadfile(objFile, documentKind);
            var fileModel = new File
            {
                Id = Guid.NewGuid(),
                Path = path.Result,
                DocumentKind = documentKind,
                Accepted = accepted,
                UserId = userId,
                Date = DateTime.Now,
                FileName = objFile.file.FileName
            };
            _repository.SavePath(fileModel);
            _repository.SaveChanges();
            return Ok();
        }

        [HttpPatch("{id}")]
        public ActionResult UpdateFileModel(string path, JsonPatchDocument<FileUpdateDto> json)
        {
            var fileModel = _repository.GetFileByPathToFile(path);
            if (fileModel == null)
                return NotFound();
            var fileToPatch = _mapper.Map<FileUpdateDto>(fileModel);
            json.ApplyTo(fileToPatch, ModelState);
            if (!TryValidateModel(fileToPatch))
                return ValidationProblem(ModelState);
            _mapper.Map(fileToPatch, fileModel);
            _repository.UpdateProposal(fileModel);
            _repository.SaveChanges();
            return NoContent();

        }

        [HttpDelete("{id}")]
        public ActionResult DeleteFile(Guid id)
        {
            _repository.DeleteFile(id);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}
