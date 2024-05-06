using Application.Repositories;
using Domain.Entities;
using Domain.Enums;
using Moq;

namespace Tests.Mocks
{
    internal class MockIArticleRepository
    {
        private static Article GetArticle()
        {
            var aId = Guid.Parse("9b441c77-604a-44c0-9550-a739dbf06012");

            return new Article
            {
                Id = aId,
                CreatedDate = DateTime.Now,
                Name = "Funny Memoir",
                Quantity = 18,
                AllowDiscount = false,
                BatchNumber = 3,
                Type = ArticleTypes.Books
            };
        }
        public static Mock<IArticleRepository> GetMock()
        {
            var mock = new Mock<IArticleRepository>();

            var canc = new CancellationToken();
            var article = GetArticle();

            mock.Setup(x => x.Get(It.IsAny<Guid>(), canc)).ReturnsAsync(article);

            return mock;
        }
    }
}
