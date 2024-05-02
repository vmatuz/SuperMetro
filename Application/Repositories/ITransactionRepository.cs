
using Domain.Entities;

namespace Application.Repositories
{
    public interface ITransactionRepository :IBaseRepository<Transaction>
    {
        IEnumerable<Transaction> GetAllWithDueIn60Days();
        IEnumerable<Transaction> GetAllWithDueToday();
        IEnumerable<Transaction> GetAllWithDiscount();
        void Seed();
    }
}
