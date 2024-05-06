using AutoMapper;
using NUnit.Framework;
using Application.Handlers.Transaction;
using Tests.Mocks;
using Domain.Entities;
using Domain.Enums;

namespace Tests.Tests;
[TestFixture]
public class TransactionsRepositoryTests
{
    public IMapper GetMapper()
    {
        var mappingProfile = new TransactionMapper();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(mappingProfile));
        return new Mapper(configuration);
    }

    [Test]
    public async Task AddTRansaction_ReturnsOK()
    {
        var trR = MockITransactionRepository.GetMock();       
        var arR = MockIArticleRepository.GetMock();
        var pR = MockIPaymentRepository.GetMock();

        var mapper = GetMapper();


       await trR.Object.AddTransaction(GetTransaction(), new List<Article> { GetArticle() }, new List<Payment> { GetPayment() });

        var transaction =  trR.Object.GetAll(new CancellationToken());
        var article =  arR.Object.Get(Guid.NewGuid(),new CancellationToken());

        var count = transaction.Result.Count();
        Assert.That(1,Is.EqualTo(count));
        Assert.That(18, Is.EqualTo(article.Result.Quantity));
        Assert.Pass();
    }

    private  Transaction GetTransaction()
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
    private  Payment GetPayment()
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
    private  Article GetArticle()
    {
        var aId = Guid.Parse("9b441c77-604a-44c0-9550-a739dbf06012");

        return new Article
        {
            Id = aId,
            CreatedDate = DateTime.Now,
            Name = "Funny Memoir",
            Quantity = 20,
            AllowDiscount = false,
            BatchNumber = 3,
            Type = ArticleTypes.Books
        };
    }

}
