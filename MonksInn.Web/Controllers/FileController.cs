using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonksInn.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Web.Controllers
{
    public class FileController : BaseController
    {
        public FileController(IUnitOfWork uow) : base(uow)
        {
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
        [AllowAnonymous]
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
