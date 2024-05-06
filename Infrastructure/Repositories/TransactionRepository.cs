using Application.Repositories;
using Domain.Entities;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {

        public TransactionRepository(DataContext dataContext) : base(dataContext) { }


        public IEnumerable<Transaction> GetAllWithDiscount()
        {
            return Context.Transactions.Where(x => x.DiscountApplied).ToList();
        }

        public IEnumerable<Transaction> GetAllWithDueIn60Days()
        {
            return Context.Transactions.Where(x => x.DueDate == DateTime.Today.AddDays(60)).ToList();
        }

        public IEnumerable<Transaction> GetAllWithDueToday()
        {
            return Context.Transactions.Where(x => x.DueDate == DateTime.Today).ToList();
        }
        public async Task AddTransaction(Transaction transaction, List<Article> articles, List<Payment> payments)
        {

            using (var tr = Context.Database.BeginTransaction())
            {
                try
                {
                    base.Add(transaction);
                    articles.ForEach(article =>
                    {
                        Context.Articles.Update(article);
                        ArticleTransaction at = new ArticleTransaction();
                        at.ArticleId = article.Id;
                        at.TransactionId = transaction.Id;

                        Context.ArticlesTransactions.Add(at);
                    });

                    payments.ForEach(p => Context.Payments.Add(p));

                    Context.SaveChanges();

                    await tr.CommitAsync();
                   
                }
                catch (Exception ex)
                {

                    tr.Rollback();
                }
            }
        }

        public void Seed()
        {
            throw new NotImplementedException();
        }
    }
}
