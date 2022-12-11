using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.Caching.Memory;
using NLayer.Core.Dtos;
using NLayer.Core.Dtos.Products;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.Exceptions;
using System.Linq.Expressions;

namespace NLayer.Caching
{
    public class ProductServiceWithCaching : IProductService
    {
        private const string _cacheProductKey = "ProductsCache";
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductServiceWithCaching(IMapper mapper, IMemoryCache memoryCache, IProductRepository repository, IUnitOfWork unitOfWork)
        {
            _memoryCache = memoryCache;
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;

            if (!_memoryCache.TryGetValue(_cacheProductKey, out _))
            {
                _memoryCache.Set(_cacheProductKey, _repository.GetProductsWithCategory().Result);
            }
        }

        public async Task<Product> AddAsync(Product entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProducts();
            return entity;
        }

        public async Task<IEnumerable<Product>> AddRangeAsync(IEnumerable<Product> entities)
        {
            await _repository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            await CacheAllProducts();
            return entities;
        }

        public Task<bool> AnyAsync(Expression<Func<Product, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<Product>> GetAllAsync()
        {
            return Task.FromResult(_memoryCache.Get<IQueryable<Product>>(_cacheProductKey));
        }

        public Task<Product> GetByIdAsync(int id)
        {
            var product = _memoryCache.Get<List<Product>>(_cacheProductKey).FirstOrDefault(x => x.Id == id);
            if(product == null)
            {
                throw new NotFoundException($"{typeof(Product).Name} ({id}) not found");
            }

            return Task.FromResult(product);
        }

        public Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory()
        {
            var products = _memoryCache.Get<List<Product>>(_cacheProductKey);
            var dtos = _mapper.Map<List<ProductWithCategoryDto>>(products);
            return Task.FromResult(CustomResponseDto<List<ProductWithCategoryDto>>.Success(200, dtos));
        }

        public async Task RemoveAsync(Product entity)
        {
            _repository.Remove(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProducts();
        }

        public async Task RemoveRangeAsync(IEnumerable<Product> entities)
        {
            _repository.RemoveRange(entities);
            await _unitOfWork.CommitAsync();
            await CacheAllProducts();
        }

        public async Task UpdateAsync(Product entity)
        {
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProducts();
        }

        public IQueryable<Product> Where(Expression<Func<Product, bool>> expression)
        {
            return _memoryCache.Get<List<Product>>(_cacheProductKey).Where(expression.Compile()).AsQueryable();
        }

        public async Task CacheAllProducts()
        {
            _memoryCache.Set(_cacheProductKey, await _repository.GetAll().ToListAsync());
        }
    }
}
