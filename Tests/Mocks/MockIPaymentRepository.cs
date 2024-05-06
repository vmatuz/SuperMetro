using Application.Repositories;
using Domain.Entities;
using Domain.Enums;
using Moq;

namespace Tests.Mocks;

internal class MockIPaymentRepository
{

    private static Payment GetPayment()
    {
        var pId = Guid.Parse("9b441c77-604a-12c0-9550-a739dbf06012");

        return new Payment
        {
            Id = pId,
            Amount = 123,
            PaymentType = PaymentType.Cash,
            CustomerId = Guid.Parse("8b441c77-604a-44c0-9550-a739dbf06012"),
            PaymentDate = DateTime.Today,
            ProccessedBy = Guid.NewGuid(),
            CreatedDate = DateTime.Now
        };
    }

    public static Mock<IPaymentRepository> GetMock()
    {
        var mock = new Mock<IPaymentRepository>();

        var canc = new CancellationToken();
        var payment = GetPayment();

        mock.Setup(x => x.Get(It.IsAny<Guid>(), canc)).ReturnsAsync(payment);

        return mock;
    }
}
