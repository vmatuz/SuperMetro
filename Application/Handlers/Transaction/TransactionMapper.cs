using Application.Command;
using AutoMapper;

namespace Application.Handlers.Transaction;

public sealed class TransactionMapper : Profile
{
    public TransactionMapper()
    {
        CreateMap<AddTransactionCommand, Domain.Entities.Transaction>();
        CreateMap<Domain.Entities.Transaction, GetAllTransactionsResponse>();        
    }
}