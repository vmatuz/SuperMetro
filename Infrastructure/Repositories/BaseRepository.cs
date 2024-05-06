using Application.Repositories;
using Domain.Common;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly DataContext Context;

        public static Guid CustomerguidId = Guid.Parse("8b441c77-604a-44c0-9550-a739dbf06012");
        public static Guid PaymentGuidId = Guid.Parse("8b441c77-604a-44c0-9550-a739dbf06011");
        public static Guid ArticleGuidId = Guid.Parse("8b441c77-604a-44c0-9550-a739dbf06000");

        public BaseRepository(DataContext context)
        {
            Context = context;
        }
        public void Add(T entity)
        {
            Context.Add(entity);
            Context.SaveChanges();
        }

        public void Update(T entity)
        {
            Context.Update(entity);
            Context.SaveChanges();
        }

        public void Delete(T entity)
        {
            Context.Remove(entity);
            Context.SaveChanges();
        }

        public Task<T> Get(Guid id, CancellationToken cancellationToken)
        {
            return Context.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public Task<List<T>> GetAll(CancellationToken cancellationToken)
        {
            return Context.Set<T>().ToListAsync(cancellationToken);
        }
    }
}
