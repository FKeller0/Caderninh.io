namespace Caderninh.io.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        Task CommitChangesAsync();
    }
}