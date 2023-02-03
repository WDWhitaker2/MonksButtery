using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Backend.Models.File
{
    public class UploadFilesXHRViewModel
    {
        public IEnumerable<IFormFile> Files { get; set; }
    }
}
