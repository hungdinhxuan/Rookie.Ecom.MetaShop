using AutoMapper;
using Rookie.Ecom.MetaShop.Business.Interfaces;
using Rookie.Ecom.MetaShop.Contracts.Dtos.ProductPicture;
using Rookie.Ecom.MetaShop.DataAccessor.Entities;
using Rookie.Ecom.MetaShop.DataAccessor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Ecom.MetaShop.Business.Services
{
    public class ProductPictureService : IProductPictureService
    {
        private readonly IBaseRepository<ProductPicture> _baseRepository;
        private readonly IMapper _mapper;

        public ProductPictureService(IBaseRepository<ProductPicture> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;

        }

        public async Task<ProductPictureDto> AddAsync(CreateProductPictureDto item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            var productPicture = _mapper.Map<ProductPicture>(item);
            var itemProductPicture = await _baseRepository.AddAsync(productPicture);
            return _mapper.Map<ProductPictureDto>(itemProductPicture);
        }

        public async Task<IEnumerable<ProductPictureDto>> AddRangeAsync(IEnumerable<CreateProductPictureDto> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            if (!items.Any())
            {
                return null;
            }

            var productPictures = _mapper.Map<IEnumerable<ProductPicture>>(items);
            var itemProductPictures = await _baseRepository.AddRangeAsync(productPictures);

            return _mapper.Map<IEnumerable<ProductPictureDto>>(itemProductPictures);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _baseRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ProductPictureDto>> GetAllByProductIdAsync(Guid productId)
        {
            var productPicture = await _baseRepository.GetAllAsync();

            throw new NotImplementedException();
        }

        public async Task RemoveRangeAsync(IEnumerable<ProductPictureDto> productPictures)
        {
            await _baseRepository.RemoveRangeAsync(_mapper.Map<IEnumerable<ProductPicture>>(productPictures));
        }

    }
}
