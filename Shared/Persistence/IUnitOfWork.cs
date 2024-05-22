namespace WebApi.Shared.Persistence
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
