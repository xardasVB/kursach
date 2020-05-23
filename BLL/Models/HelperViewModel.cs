using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class PageViewModel
    {
        public int TotalPages { get; set; }
        [Display(Name = "Items per page")]
        [Range(1, short.MaxValue)]
        public int Items { get; set; }
        public int CurrentPage { get; set; }
    }

    public class SelectItemViewModel<T>
    {
        public T Id { get; set; }
        public string Name { get; set; }
    }

    public class ImageViewModel
    {
        public string ImageName { get; set; }
        public string Folder { get; set; }
    }

    public enum StatusViewModel
    {
        Success = 0,
        Dublication = 1,
        Error = 2
    }
}
