using Application.Repositories;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ArticleRepository : BaseRepository<Article>, IArticleRepository
    {
        public ArticleRepository(DataContext context) : base(context)
        {
        }
        public IEnumerable<Article> GetAllWithBatchNumber(int batchNumber, CancellationToken cancellationToken)
        {
            var getAllByBatch = Context.Articles.Where(x => x.BatchNumber == batchNumber)
                                    .Include(x=>x.ArticleTransactions).ToListAsync();

            return getAllByBatch.Result;
        }
        public  IEnumerable<Article> GetAllArticles(CancellationToken cancellationToken)
        {
            var all = Context.Articles
                                    .Include(x => x.ArticleTransactions).ToListAsync(cancellationToken);

            return all.Result;
        }

        public void UpdateQuantity(int batchNumber, int quantity)
        {
            var article = Context.Articles.FirstOrDefault(x => x.BatchNumber == batchNumber);
            if (article != null)
            {
                article.Quantity = quantity;

                Context.Update(article);
            }
        }

        public void UpdateQuantityByName(string name, int quantity)
        {
            var article = Context.Articles.FirstOrDefault(x => x.Name == name);
            if (article != null)
            {
                article.Name = name;

                Context.Update(article);
            }
        }

        public void Seed()
        {
            if (!Context.Articles.Any())
            {
                var articles = new List<Article>() {
                new Article {Id = ArticleGuidId, CreatedDate=DateTime.Now, Name = "Funny Memoir",
                             Quantity = 20, AllowDiscount = false,
                             BatchNumber = 3, Type = Domain.Enums.ArticleTypes.Books
                            },
                new Article {Id = Guid.NewGuid(), CreatedDate=DateTime.Now, Name = "The New Yorker",
                             Quantity = 200, AllowDiscount = true,
                             BatchNumber = 1, Type = Domain.Enums.ArticleTypes.Newsspaper
                            },
                 new Article {Id = Guid.NewGuid(),CreatedDate=DateTime.Now, Name = "Born A Crime",
                             Quantity = 150, AllowDiscount = true,
                             BatchNumber = 3, Type = Domain.Enums.ArticleTypes.Books
                            },
                  new Article {Id = Guid.NewGuid(),CreatedDate=DateTime.Now, Name = "Life with me",
                             Quantity = 202, AllowDiscount = false,
                             BatchNumber = 2, Type = Domain.Enums.ArticleTypes.Books
                            },
                   new Article {Id = Guid.NewGuid(),CreatedDate=DateTime.Now, Name = "Faclia",
                             Quantity = 2033, AllowDiscount = true,
                             BatchNumber = 21, Type = Domain.Enums.ArticleTypes.Newsspaper
                            },
                };

                Context.Articles.AddRange(articles);
                Context.SaveChanges();
            }
        }

       
    }
}
