using System.Linq.Expressions;

namespace NLayer.Core.Services
{
    public interface IService<T> where T:class
    {
        Task<T> GetByIdAsync(int id);
        Task<IQueryable<T>> GetAllAsync();//direk veritabanına sorgu atmamak için IQueryable kullanıldı. tolist vs ile sorgu atılır
        //productRepository.Where(x => x.Id>5).orderby.tolistasync();
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entities);
    }
}
