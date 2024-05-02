using Domain.Entities;

namespace Application.Repositories
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        IEnumerable<Customer> GetAllArchived();
        Customer GetCustomerByEmail(string email);
        IEnumerable<string> GetEmailsForPromotions();
        IEnumerable<Customer> GetCustomersFromSpecificCity(string city);
        void UpdateAddress(Guid Id, string address);
        void Seed();
    }
}
