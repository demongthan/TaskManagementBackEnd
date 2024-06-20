namespace TaskManagement.DataAccessLayer.UnitOfWork.AstractClass
{
    public interface IUnitOfWork
    {
        Task<bool> SaveChangesAsync();
        Task DisposeAsync();
    }
}
