using System.ComponentModel.DataAnnotations;

namespace SSD_Lab2.Models
{
    public class Message
    {
        [Key]
        public Guid messageId { get; set; }

        [Required]
        [StringLength(1000)]
        public required string message { get; set; }

        [Required]
        public Guid userId { get; set; }



    }
}
