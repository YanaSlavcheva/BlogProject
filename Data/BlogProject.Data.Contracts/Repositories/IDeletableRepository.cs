namespace BlogProject.Data.Contracts.Repositories
{
    using System.Linq;

    public interface IDeletableRepository<T> : IRepository<T>
        where T : class
    {
        IQueryable<T> AllWithDeleted();

        void HardDelete(T entity);

        void HardDelete(params object[] id);
    }
}
