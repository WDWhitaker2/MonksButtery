using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Backend.Models.Cellar
{
    public class RemoveStockPartialViewModel
    {
        public Guid Id { get;  set; }
        public string Beer { get; set; }
        public string UnitSize { get; set; }
        [Required]
        public string Reason { get; set; }
        public bool ForceCloseModal { get; internal set; }
    }
}
