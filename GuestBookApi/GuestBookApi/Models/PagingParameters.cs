using System;
using System.ComponentModel.DataAnnotations;

namespace GuestBookApi.Models
{
    public class PagingParameters
    {
        [Required]
        [Range(1, Int32.MaxValue)]
        public int PageNumber { get; set; }
        [Required]
        [Range(25, 25)]
        public int PageSize { get; set; }
        [Required]
        [StringLength(20)]
        public string SortColumn { get; set; }
        [Required]
        public bool DescendingOrder { get; set; }
    }
}
