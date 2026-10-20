using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SSD_Lab2.Models {
    public class Product {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [DisplayName("Product Name")]
        public string? ProductName { get; set; }
        
        [Required]
        [DisplayName("Product Cost")]
        public double ProductCost { get; set; }

        [DisplayName("Product Color")]
        public string? ProductColor { get; set; }

        [DisplayName("Product Material")]
        public string? ProductMaterial { get; set; }

        [DisplayName("Origin Location")]
        public string? OriginLocation { get; set; }

    }
}