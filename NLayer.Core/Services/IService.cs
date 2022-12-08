using System.Linq.Expressions;

namespace NLayer.Core.Services
{
    public interface IService<T> where T:class
    {
        Task<T> GetByIdAsync(int id);
        IQueryable<T> GetAll(Expression<Func<T, bool>> expression);//direk veritabanına sorgu atmamak için IQueryable kullanıldı. tolist vs ile sorgu atılır
        //productRepository.Where(x => x.Id>5).orderby.tolistasync();
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entities);
    }
}
