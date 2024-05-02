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

        public void Seed()
        {
            throw new NotImplementedException();
        }
    }
}
