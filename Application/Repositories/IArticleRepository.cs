using Domain.Entities;

namespace Application.Repositories
{
    public interface IArticleRepository : IBaseRepository<Article>
    {
        IEnumerable<Article> GetAllWithBatchNumber(int batchNumber,CancellationToken cancellationToken);
        void UpdateQuantity(int batchNumber, int quantity);
        void UpdateQuantityByName(string name, int quantity);
        IEnumerable<Article> GetAllArticles(CancellationToken cancellationToken);

        void Seed();
    }
}
