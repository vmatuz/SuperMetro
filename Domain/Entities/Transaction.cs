using Domain.Common;

namespace Domain.Entities
{
    public class Transaction :BaseEntity
    {
        public double Sum { get; set; }
        public string Bank { get; set; }
        public DateTime DueDate { get; set; }
        public bool DiscountApplied { get; set; }
        public bool SplitPayment { get; set; }

        public ICollection<ArticleTransaction> ArticleTransactions{ get; set; }

    }
}
