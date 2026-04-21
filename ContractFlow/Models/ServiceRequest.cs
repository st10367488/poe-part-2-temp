using System.ComponentModel.DataAnnotations;

namespace ContractMS.Models
{
    public enum ServiceRequestStatus { Pending, InProgress, Completed, Cancelled }

    public class ServiceRequest
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Contract")]
        public int ContractId { get; set; }
        public Contract? Contract { get; set; }

        [Required, MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Currency)]
        public decimal Cost { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal CostZAR { get; set; }

        [Required]
        public ServiceRequestStatus Status { get; set; } = ServiceRequestStatus.Pending;
    }
}