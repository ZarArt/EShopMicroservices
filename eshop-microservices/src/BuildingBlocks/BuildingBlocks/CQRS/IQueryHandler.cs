using MediatR;

namespace BuildingBlocks.CQRS
{
    public interface IQueryHandler<in TCommand> : IRequestHandler<TCommand, Unit>
        where TCommand : IQuery<Unit>
    { }

    public interface IQueryHandler<in TQuery, TResponce> : IRequestHandler<TQuery, TResponce>
        where TQuery : IQuery<TResponce>
        where TResponce : notnull
    { }
}
