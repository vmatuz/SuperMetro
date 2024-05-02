using Application.Repositories;
using Domain.Entities;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(DataContext dataContext) : base(dataContext) { }
        public IEnumerable<Customer> GetAllArchived()
        {
            return Context.Customers.Where(x => x.IsArchived).ToList();
        }

        public Customer GetCustomerByEmail(string email)
        {
            return  Context.Customers.Where(x => x.Email == email).FirstOrDefault();
        }

        public IEnumerable<Customer> GetCustomersFromSpecificCity(string city)
        {
            return Context.Customers.Where(x => x.City == city).ToList();
        }

        public IEnumerable<string> GetEmailsForPromotions()
        {
            return Context.Customers.Where(x => x.CanSendPromotionsEmails).Select(x => x.Email).ToList();
        }

        public void UpdateAddress(Guid Id, string address)
        {
            var entity = Context.Customers.FirstOrDefault(x => x.Id == Id);
            if (entity != null)
            {
                entity.Address = address;

                Context.Update(entity);
            }
        }

        public void Seed()
        {
            if (!Context.Customers.Any())
            {
                var customers = new List<Customer>() {
                    new Customer { Id = CustomerguidId, CreatedDate = DateTime.Now,
                                       Name="Trevor Noah", CreditCard = Guid.NewGuid().ToString(),
                                       Address = "Broadway No.3", City="New York", Country = "USA",
                                       Email = "IAmTrevor@myEmail.com", FIN = Guid.NewGuid().ToString(),
                                       CanSendPromotionsEmails =true
                                     },
                       new Customer { Id = Guid.NewGuid(), CreatedDate = DateTime.Now,
                                       Name="Jerry Seinfeld", CreditCard = Guid.NewGuid().ToString(),
                                       Address = "Park Avenue", City="New York", Country = "USA",
                                       Email = "Jerry@myEmail.com", FIN = Guid.NewGuid().ToString(),
                                       CanSendPromotionsEmails =false
                                     },
                        new Customer { Id = Guid.NewGuid(), CreatedDate = DateTime.Now,
                                       Name="Chandler Bing", CreditCard = Guid.NewGuid().ToString(),
                                       Address = "Washington Street", City="Los Angeles", Country = "USA",
                                       Email = "bing@bing.com", FIN = Guid.NewGuid().ToString(),
                                       CanSendPromotionsEmails =false
                                     },
                         new Customer { Id = Guid.NewGuid(), CreatedDate = DateTime.Now,
                                       Name="Larry David", CreditCard = Guid.NewGuid().ToString(),
                                       Address = "Crosby Street", City="Los Angeles", Country = "USA",
                                       Email = "pretty@good.com", FIN = Guid.NewGuid().ToString(),
                                       CanSendPromotionsEmails =true
                                     }
           };
                Context.Customers.AddRange(customers);
                Context.SaveChanges();
            }
        }
    }
}
