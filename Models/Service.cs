using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SSD_Lab2.Models
{
    public class Service
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [DisplayName("Service Name")]
        public string ServiceName { get; set; }

        [Required]
        [DisplayName("Service Cost")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Must be greater than 0")]
        public double ServiceCost { get; set; }

        [DisplayName("Service Description")]
        public string? ServiceDescription { get; set; }

        [DisplayName("Service Duration (Minutes)")]
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Must be greater than 0")]
        public int DurationInMinutes { get; set; }

        [DisplayName("Requires Deposit")]
        [Required]
        public bool RequiresDeposit { get; set; }

        [DisplayName("Maximum Bookings Per Day")]
        [Range(1, 20, ErrorMessage = "Must be in range 1-20")]
        [Required]
        public int MaxBookingsPerDay { get; set; }
    }
}