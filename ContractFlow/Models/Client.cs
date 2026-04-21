using System.ComponentModel.DataAnnotations;

namespace ContractMS.Models
{
    public class Client
    {
        public int Id { get; set; }

        [Required, MaxLength(150)]
        [Display(Name = "Client Name")]
        public string Name { get; set; } = string.Empty;

        [Required, MaxLength(200)]
        [Display(Name = "Contact Details")]
        public string ContactDetails { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Region { get; set; } = string.Empty;

        public ICollection<Contract> Contracts { get; set; } = new List<Contract>();
    }
}