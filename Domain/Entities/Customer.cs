using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Customer : BaseEntity
    {
        [Required]
        [StringLength(52)]
        public  string Name { get; set; }
        public string CreditCard { get; set; }
        [Required]
        public  string Address { get; set; }
        public  string City { get; set; }
        public  string Country { get; set; }
        [Required]
        [EmailAddress]
        public  string Email { get; set; }
        public  string FIN { get; set; }
        public bool IsArchived { get; set; }
        public bool CanSendPromotionsEmails { get; set; }
    }
}
