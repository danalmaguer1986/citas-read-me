using CSharpFunctionalExtensions;
using System.Threading.Tasks;


namespace Negocio.Core
{
    public interface ICommand
    {
    }

    // ReSharper disable once UnusedTypeParameter
    public interface ICommand<TResult>
    {
    }

    public interface ICommandHandler<in TCommand>
        where TCommand : ICommand
    {
        Result Handle(TCommand command);
    }

    public interface ICommandHandler<in TCommand, TResult>
        where TCommand : ICommand<TResult>
    {
        Result<TResult> Handle(TCommand command);
    }


    public interface IAsyncCommandHandler<in TCommand>
        where TCommand : ICommand
    {
        Task<Result> HandleAsync(TCommand command);
    }
}
