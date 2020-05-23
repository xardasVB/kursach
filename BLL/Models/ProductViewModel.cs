using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class ProductItemViewModel
    {
        [Display(Name = "Product Id")]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        [Display(Name = "Price CMV")]
        public decimal PriceCMV { get; set; }
        [Display(Name = "Count in the stock")]
        public int StockCount { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Manufacturer { get; set; }
        public int ManufacturerId { get; set; }
        public string Subcategory { get; set; }
        public int SubcategoryId { get; set; }
    }

    public class ProductViewModel : PageViewModel
    {
        public List<ProductItemViewModel> Products { get; set; }
        public ProductSearchViewModel Search { get; set; }
    }

    public class ProductInterfereViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Price CMV is required")]
        [Display(Name = "Price CMV")]
        public decimal PriceCMV { get; set; }
        [Required(ErrorMessage = "Count in the stock is required")]
        [Display(Name = "Count in the stock")]
        public int StockCount { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        [Display(Name = "Manufacturer")]
        public int ManufacturerId { get; set; }
        public List<SelectItemViewModel<int>> Manufacturers { get; set; }
        [Required(ErrorMessage = "Subcategory is required")]
        [Display(Name = "Subcategory")]
        public int SubcategoryId { get; set; }
        public List<SelectItemViewModel<int>> Subcategories { get; set; }
    }
    
    public class ProductSearchViewModel
    {
        public OrderBy Order { get; set; }
        public string Name { get; set; }
        public string Subcategory { get; set; }
        public string Category { get; set; }
    }

    public enum OrderBy
    {
        Not = 0,
        PriceAscending = 1,
        PriceDescending = 2
    }
}
