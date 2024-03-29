﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Rookie.Ecom.MetaShop.Business.Extensions;
using Rookie.Ecom.MetaShop.Business.Interfaces;
using Rookie.Ecom.MetaShop.Contracts;
using Rookie.Ecom.MetaShop.Contracts.Dtos;
using Rookie.Ecom.MetaShop.Contracts.Dtos.Product;
using Rookie.Ecom.MetaShop.DataAccessor.Entities;
using Rookie.Ecom.MetaShop.DataAccessor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Ecom.MetaShop.Business.Services
{
    public class ProductService : IProductService
    {
        private readonly IBaseRepository<Product> _baseRepository;
        private readonly IMapper _mapper;

        public ProductService(IBaseRepository<Product> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public async Task<ProductDto> AddAsync(CreateProductDto newProductDto)
        {
            if (newProductDto == null)
            {
                throw new ArgumentNullException(nameof(newProductDto));
            }
            var product = _mapper.Map<Product>(newProductDto);
            var item = await _baseRepository.AddAsync(product);
            return _mapper.Map<ProductDto>(item);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _baseRepository.DeleteAsync(id);
        }

        public async Task UpdateAsync(UpdateProductDto ProductDto)
        {
            var product = _mapper.Map<Product>(ProductDto);
            await _baseRepository.UpdateAsync(product);
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _baseRepository.GetAllAsync();
            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task<ProductDto> GetByIdAsync(Guid id)
        {
            IQueryable<Product> query = _baseRepository.Entities;

            query = query.Where(Product => Product.Id == id);
            if (query == null)
                return null;
            query = query.Include(c => c.Category).Include(c => c.ProductPictures).OrderBy(x => x.Name);
            Product product = await query.FirstOrDefaultAsync();


            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> GetByNameAsync(string name)
        {
            var product = await _baseRepository.GetByAsync(x => x.Name == name);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<PagedResponseModel<ProductDto>> PagedQueryAsync(string name, Guid? categoryId, int? page, int limit)
        {
            var query = _baseRepository.Entities;

            query = query.Where(x => string.IsNullOrEmpty(name) || x.Name.Contains(name));

            if (categoryId != null)
                query = query.Where(x => x.CategoryId == categoryId);

            query = query.Include(c => c.Category).Include(c => c.ProductPictures);

            query = query.OrderBy(x => x.Name);

            var assets = await query
                .AsNoTracking()
                .PaginateAsync(page ?? 1, limit);

            return new PagedResponseModel<ProductDto>
            {
                CurrentPage = assets.CurrentPage,
                TotalPages = assets.TotalPages,
                TotalItems = assets.TotalItems,
                Items = _mapper.Map<IEnumerable<ProductDto>>(assets.Items)
            };
        }

        public async Task SoftDeleteAsync(Guid id)
        {
            await _baseRepository.SoftDeleteAsync(id);
        }

        public async Task<List<ProductDto>> GetRelatedProducts(Guid categroyId, int num)
        {
            var query = _baseRepository.Entities;
            List<Product> products = await query.Where(p => p.CategoryId == categroyId).OrderByDescending(p => p.Price).Include(p => p.Category).Include(p => p.ProductPictures).Take(num).ToListAsync();
            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task<List<ProductDto>> FilterProducts(bool isLastest, bool isFeatured, int num = 5)
        {
            var query = _baseRepository.Entities;

            if (isLastest)
                query = query.Include(p => p.Category).Include(p => p.ProductPictures).OrderByDescending(p => p.CreatedBy).Take(num).OrderByDescending(p => p.Price);
            else
                query = query.Where(p => p.IsFeatured == true).Include(p => p.Category).Include(p => p.ProductPictures).Take(num).OrderByDescending(p => p.Price);

            List<Product> products = await query.ToListAsync();
            return _mapper.Map<List<ProductDto>>(products);
        }
    }
}
