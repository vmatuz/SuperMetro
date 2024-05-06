using Application.Repositories;
using Domain.Entities;
using Domain.Enums;
using Moq;

namespace Tests.Mocks;

internal class MockITransactionRepository
{
    private static Transaction GetTransaction()
    {

        return new Transaction
        {
            Id = Guid.NewGuid(),
            Bank = "ING",
            DueDate = DateTime.Today.AddDays(45),
            Sum = 12345,
            DiscountApplied = true,
            SplitPayment = false,
        };
    }
    public static Mock<ITransactionRepository> GetMock()
    {
        var mock = new Mock<ITransactionRepository>();

        var canc = new CancellationToken();
        var transaction = GetTransaction();

        mock.Setup(x => x.GetAll(canc)).ReturnsAsync(new List<Transaction>() { transaction });
        mock.Setup(x => x.AddTransaction(It.IsAny<Transaction>(),It.IsAny<List<Article>>(),It.IsAny<List<Payment>>())).Callback(() => { return; });

        return mock;
    }
}

