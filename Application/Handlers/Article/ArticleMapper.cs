using Application.Command;
using AutoMapper;

namespace Application.Handlers.Article;
public sealed class ArticleMapper : Profile
{
    public ArticleMapper()
    {
        CreateMap<GetArticleRequest, Domain.Entities.Article>();
        CreateMap<AddArticleCommand, Domain.Entities.Article>();
        CreateMap<Domain.Entities.Article, GetArticleResponse>();
        CreateMap<UpdateArticleRequest, Domain.Entities.Article>();
        CreateMap<Domain.Entities.Article, UpdateArticleResponse>();
    }
}
