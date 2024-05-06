using Domain.Common;

namespace Domain.Entities;
public class ArticleTransaction :BaseEntity
{
   // [ForeignKey(nameof(Article))]
    public Guid ArticleId { get; set; }
    public virtual Article Article { get; set; }

   // [ForeignKey(nameof(Transaction))]
    public Guid TransactionId { get; set; }
    public virtual Transaction Transaction { get; set; }

}
