using Application.Repositories;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(DataContext dataContext):base(dataContext) { }

        public IEnumerable<Payment> GetPaymentsByPaymentDate(DateTime paymentDate)
        {
          return Context.Payments.Where(x=>x.PaymentDate==paymentDate).ToList();
        }

        public IEnumerable<Payment> GetPaymentsForCustomer(Guid customerId, CancellationToken cancellationToken)
        {
            return Context.Payments.Where(x => x.CustomerId == customerId).ToListAsync(cancellationToken).Result;
        }

        public IEnumerable<Payment> GetPaymentsProccessedBy(Guid emmployeeId)
        {
           return Context.Payments.Where(x => x.ProccessedBy == emmployeeId).ToList();
        }

        public void Seed()
        {
            if (!Context.Payments.Any()) {
                var p = new Payment
                {
                    Id = PaymentGuidId,
                    CreatedDate = DateTime.Now,
                    Amount = 123.45,
                    PaymentDate = DateTime.Today.AddDays(10),
                    CustomerId = CustomerguidId,
                    ProccessedBy = default(Guid),
                    PaymentType = PaymentType.Cash
                };
                Context.Payments.Add(p);
                Context.SaveChanges();
            }
        }
    }
}
