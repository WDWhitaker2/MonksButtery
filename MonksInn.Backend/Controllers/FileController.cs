using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonksInn.Backend.Models.File;
using MonksInn.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Backend.Controllers
{
    public class FileController : BaseController
    {
        public FileController(IUnitOfWork uow) : base(uow)
        {
        }

        [HttpPost]
        public async Task<string> UploadFilesXHR(UploadFilesXHRViewModel model)
        {
            if (model.Files != null)
            {
                List<Guid> fileIDs = new List<Guid>();
                foreach (var formFile in model.Files)
                {
                    Domain.File file = new Domain.File();

                    using (var memoryStream = new MemoryStream())
                    {
                        await formFile.CopyToAsync(memoryStream);
                        file.FileData = memoryStream.ToArray();
                    }

                    file.Filename = formFile.FileName.Replace("&", "-").Replace("?", "-");
                    file.MimeType = formFile.ContentType;

                    var newfile = FileLogic.AddFile(file);


                    fileIDs.Add(newfile.Id);
                }

                SaveDbChanges();
                return string.Join(",", fileIDs.ToArray());
            }

            return "";
        }

        [HttpPost]
        public IActionResult GetFileDispayView(string fileIds)
        {
            var fieldIdLists = fileIds.Split(',');

            var GuidList = new List<Guid>();

            foreach (var item in fieldIdLists)
            {
                if (Guid.TryParse(item, out var result))
                {
                    GuidList.Add(result);
                }
            }


            var files = FileLogic.GetAllFiles()
                .Where(a => GuidList.Contains(a.Id))
                .ToList();

            return PartialView(files);
        }

        [HttpGet]
        public IActionResult Get(Guid id)
        {
            var newfile = FileLogic.GetFile(id);
            if (newfile != null)
            {
                return new FileContentResult(newfile.FileData, newfile.MimeType);
            }
            return NotFound();

        }

        [HttpGet]
        public IActionResult GetThumbnail(Guid id)
        {
            var newfile = FileLogic.GetFile(id);
            if (newfile != null)
            {
                try
                {
                    FileLogic.CompressImage(newfile, 500);
                }
                catch (Exception)
                {
                }

                return new FileContentResult(newfile.FileData, newfile.MimeType);
            }
            return NotFound();

        }
    }
}
