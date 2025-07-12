namespace VendingMachine.Application.Abstractions;

public interface IQueryHandler<TResponse, in TQuery> where TQuery : IQuery
{
    public Task<TResponse> Handle(TQuery query, CancellationToken cancellationToken = default);
}

public interface IQueryHandler<TResponse>
{
    public Task<TResponse> Handle(CancellationToken cancellationToken = default);
}