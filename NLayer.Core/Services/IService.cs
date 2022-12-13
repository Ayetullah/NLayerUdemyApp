using NLayer.Core.Dtos;
using NLayer.Core.Models;
using System.Linq.Expressions;

namespace NLayer.Core.Services
{
    public interface IService<Entity, Dto> where Entity : BaseEntity where Dto: class
    {
        Task<CustomResponseDto<Dto>> GetByIdAsync(int id);
        Task<CustomResponseDto<IEnumerable<Dto>>> GetAllAsync();//direk veritabanına sorgu atmamak için IQueryable kullanıldı. tolist vs ile sorgu atılır
        //productRepository.Where(x => x.Id>5).orderby.tolistasync();
        Task<CustomResponseDto<IEnumerable<Dto>>> Where(Expression<Func<Entity, bool>> expression);
        Task<CustomResponseDto<bool>> AnyAsync(Expression<Func<Entity, bool>> expression);
        Task<CustomResponseDto<Dto>> AddAsync(Dto entity);
        Task<CustomResponseDto<IEnumerable<Dto>>> AddRangeAsync(IEnumerable<Dto> dtos);
        Task<CustomResponseDto<NoContentDto>> UpdateAsync(Dto dto);
        Task<CustomResponseDto<NoContentDto>> RemoveAsync(int id);
        Task<CustomResponseDto<NoContentDto>> RemoveRangeAsync(IEnumerable<int> ids);
    }
}
