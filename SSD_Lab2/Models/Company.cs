using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SSD_Lab2.Models
{
    public class Company
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [DisplayName("Business Name")]
        public string? Name { get; set; }
        [Required]
        [DisplayName("Years in Business")]
        [Range(0, 100)]
        public int YearsInBusiness { get; set; }
        [Required]
        [DisplayName("Website URL")]
        [Url(ErrorMessage = "Please enter a valid URL.")]
        public string? Website { get; set; }

        public string? Provice { get; set; }



    }
}
