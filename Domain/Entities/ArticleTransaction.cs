using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class ArticleTransaction
    {
       // [ForeignKey(nameof(Article))]
        public Guid ArticleId { get; set; }
        public virtual Article Article { get; set; }

       // [ForeignKey(nameof(Transaction))]
        public Guid TransactionId { get; set; }
        public virtual Transaction Transaction { get; set; }

    }
}
