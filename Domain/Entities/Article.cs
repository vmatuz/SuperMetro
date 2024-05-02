using Domain.Common;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Article : BaseEntity
{
    public Article()
    {
        ArticleTransactions = new HashSet<ArticleTransaction>();
    }

    [Required]
    public string Name { get; set; }
    [Required]
    public int Quantity { get; set; }
    public bool AllowDiscount { get; set; }
    public int BatchNumber { get; set; }
    public ArticleTypes Type { get; set; }

    public virtual ICollection<ArticleTransaction> ArticleTransactions { get; set; }
}
