using Domain.Entities;

namespace Application.Repositories
{
    public interface IPaymentRepository : IBaseRepository<Payment>
    {
        IEnumerable<Payment> GetPaymentsForCustomer(Guid customerId, CancellationToken cancellationToken);
        IEnumerable<Payment> GetPaymentsProccessedBy(Guid emmployeeId);
        IEnumerable<Payment> GetPaymentsByPaymentDate(DateTime paymentDate);
        void Seed();
    }
}
