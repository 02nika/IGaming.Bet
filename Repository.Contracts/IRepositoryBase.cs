using System.Linq.Expressions;

namespace Repository.Contracts;

public interface IRepositoryBase<T>
{
    IQueryable<T> FindAll();
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
    Task Create(T entity);
    Task BulkCreate(List<T> entityList);
    void Update(T entity);
    void Delete(T entity);
}