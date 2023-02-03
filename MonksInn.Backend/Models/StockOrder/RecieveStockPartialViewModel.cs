using MonksInn.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Backend.Models.StockOrder
{
    public class ReceiveStockPartialViewModel
    {
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Invoice Number")]
        public string InvoiceNumber { get; set; }
        [Required]
        public int? Units { get; set; }
        [Required]
        [Display(Name = "Unit Size")]
        public string UnitSize { get; set; }
        [Required]
        [Display(Name = "Unit Price")]
        public decimal? UnitPrice { get; set; }
        [Required]
        [Display(Name = "Beer")]
        public Guid? BeerId { get;  set; }
        [Required]
        [Display(Name = "Sell By Date")]
        public DateTime? SellByDate { get; set; }
        [Required]
        public decimal? Strength { get;  set; }

        public List<string> UnitSizeList { get; set; }
        public List<Beer> CurrentBeers { get;  set; }
        public bool ForceCloseModal { get;  set; }
    }
}
