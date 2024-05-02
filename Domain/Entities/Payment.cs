using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Payment : BaseEntity
    {
        public double Amount { get; set; }
        public PaymentType PaymentType { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime PaymentDate { get; set; }
        public Guid ProccessedBy { get; set; }


        public virtual Customer Customer { get; set; }
    }
}
