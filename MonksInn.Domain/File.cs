using MonksInn.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonksInn.Domain
{
    public class File : DatabaseObject
    {
        public byte[] FileData { get; set; }
        public string MimeType { get; set; }
        public string Filename { get; set; }

    }
}
