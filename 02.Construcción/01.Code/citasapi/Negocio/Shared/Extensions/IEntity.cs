namespace Negocio.Shared.Extensions
{
    public interface IEntity
    {
        int Id { get; }
    }

    public interface ICustomCopyable<in T> : IEntity
    {
        void CustomCopy(T entity);
    }
}
