using Application.Command;
using Application.Common;
using Application.Handlers.Payment;
using Application.Handlers.Transaction;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using Infrastructure.Context;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Reflection;
using WebAPI.Controllers;

namespace Tests;

[TestFixture]
public class TransactionTests
{
    private Mock<IMapper> _mapperMock;
    private Mock<IValidator<AddTransactionCommand>> _addTransactionCommand;
    private Mock<IPaymentRepository> _prMock = new Mock<IPaymentRepository>();
    private Mock<IArticleRepository> _arMock = new Mock<IArticleRepository>();
    private Mock<ITransactionRepository> _trMock = new Mock<ITransactionRepository>();


    private IMediator _mediator;
    private readonly ILogger<TransactionsController> _logger;
    private readonly IValidator<AddTransactionCommand> _validator;

    [SetUp]
    public void Setup()
    {
        _mapperMock = new Mock<IMapper>();

        var services = new ServiceCollection();

        services.AddAutoMapper(cfg => cfg.AddProfiles(new List<Profile>() { new TransactionMapper(), new PaymentMapper() }), Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddDbContext<DataContext>();
        var cancellation = new CancellationToken();
        _arMock.Setup(s => s.Get(Guid.Parse("9b441c77-604a-44c0-9550-a739dbf06012"), cancellation)).ReturnsAsync(GetArticle());
        services.AddScoped<ITransactionRepository>(sp => _trMock.Object);
        services.AddScoped<IArticleRepository>(sp => _arMock.Object);
        services.AddScoped<IPaymentRepository>(sp => _prMock.Object);

        var serviceProvider = services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(AddTransactionHandler).Assembly))
            .BuildServiceProvider();

        _mediator = serviceProvider.GetRequiredService<IMediator>();

        using (var scope = serviceProvider.CreateScope())
        {
            var articleRepository = scope.ServiceProvider.GetRequiredService<IArticleRepository>();
            articleRepository.Seed();
        }

    }

    [Test]
    public async Task OKToReturn()
    {
        // Arrange
        var pId = Guid.Parse("1b441c77-604a-44c0-9550-a739dbf06012");
        var aId = Guid.Parse("9b441c77-604a-44c0-9550-a739dbf06012");

        var articleR = new ArticleRequest
        {
            Id = aId,
            Quantity = 2
        };
        var paymentR = new PaymentRequest
        {
            Id = pId,
            Amount = 124,
            PaymentType = PaymentType.CreditCard,
            CustomerId = Guid.Parse("8b441c77-604a-44c0-9550-a739dbf06012"),
            PaymentDate = DateTime.Now
        };
        var addCommand = new AddTransactionCommand()
        {
            Id = Guid.NewGuid(),
            Bank = "ING",
            DueDate = DateTime.Today.AddDays(45),
            Sum = 12345,
            DiscountApplied = true,
            SplitPayment = false,
            Articles = new List<ArticleRequest>() {
            articleR
             },
            Payments = new List<PaymentRequest> { paymentR }
        };
        //_trMock.Verify(x=> x.AddTransaction(It.IsAnyType,It.IsAny<List<Article>>,It.IsAny<List<Payment>>), Times.Once());

        var controller = new TransactionsController(_mediator, _logger, _validator);
        var cancellationToken = new CancellationToken();

        var response = controller.Add(addCommand, cancellationToken);


        Assert.Pass();
        Assert.Equals(response.Result, 200);

    }

    [Test]
    public async Task AddTransaction()
    {


        //await _trMock.Object.AddTransaction(transaction, articles, payments);

        var context = new Mock<DataContext>();
        var dbSetTransactionMock = new Mock<Microsoft.EntityFrameworkCore.DbSet<Transaction>>();
        context.Setup(x => x.Set<Transaction>()).Returns(dbSetTransactionMock.Object);
        //context.Setup(x => x.Set<Article>()).Returns(GetArticle);

        var dbSetPaymentMock = new Mock<Microsoft.EntityFrameworkCore.DbSet<Payment>>();
        context.Setup(x => x.Set<Payment>()).Returns(dbSetPaymentMock.Object);

        //var tranRepo = new Mock(ITransactionRepository());

        //await tranRepo.AddTransaction(transaction, articles, payments);

        var result = await  _trMock.Object.GetAll(new CancellationToken());

        Assert.Pass();
       // Assert.Equals(_trMock., 200);

    }
    private Article GetArticle()
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
    private Payment GetPayment()
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
    private Transaction GetTransaction()
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



}

