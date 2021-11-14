using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoFacAuth.Areas.Identity.Data;
using TwoFacAuth.Data;
using TwoFacAuth.Model;

namespace TwoFacAuth.Service
{
    public class ProductService : IProductService
    {
        private readonly TwoFacAuthContext _appDbContext;
        private readonly IMapper _mapper;
        public ProductService(TwoFacAuthContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        public async Task<Response<List<ProductDto>>> DeleteProductAsync(int id)
        {
            Response<List<ProductDto>> serviceResponse = new Response<List<ProductDto>>();
            var model = await _appDbContext.Products.FindAsync(id);
            if (model != null)
            {
                _appDbContext.Products.Remove(model);
                await _appDbContext.SaveChangesAsync();
            }

            serviceResponse.Data = await (_appDbContext.Products.Select(c => _mapper.Map<ProductDto>(c))).ToListAsync();
            return serviceResponse;            
        }

        public async Task<Response<List<ProductDto>>> GetAllProductAsync()
        {
            Response<List<ProductDto>> serviceResponse = new Response<List<ProductDto>>();
            serviceResponse.Data = await (_appDbContext.Products.Select(c => _mapper.Map<ProductDto>(c))).ToListAsync();
            return serviceResponse;
        }

        public async Task<Response<ProductDto>> GetProductAsync(int id)
        {
            Response<ProductDto> serviceResponse = new Response<ProductDto>();
            serviceResponse.Data = _mapper.Map<ProductDto>(await _appDbContext.Products.FirstOrDefaultAsync(c => c.Id == id));
            return serviceResponse;
        }
        public async Task<Response<List<ProductDto>>> SaveProductAsync(ProductDto model)
        {
            Response<List<ProductDto>> serviceResponse = new Response<List<ProductDto>>();
            Product product = _mapper.Map<Product>(model);
            _appDbContext.Products.Add(product);
            await _appDbContext.SaveChangesAsync();

            serviceResponse.Data = (_appDbContext.Products.Select(c => _mapper.Map<ProductDto>(c))).ToList();
            return serviceResponse;            
        }

        public async Task<Response<ProductDto>> UpdateProductAsync(ProductDto model)
        {
            Response<ProductDto> serviceResponse = new Response<ProductDto>();
            Product product = await _appDbContext.Products.FirstOrDefaultAsync(c => c.Id == model.Id);
            if(product != null)
            {
                product.Name = model.Name;
                product.Quantity = model.Quantity;
                product.Price = model.Price;
                _appDbContext.Entry(product).State = EntityState.Modified;
                await _appDbContext.SaveChangesAsync();
                serviceResponse.Message = "Product Updated Successfully!";

                serviceResponse.Data = _mapper.Map<ProductDto>(product);
                return serviceResponse;
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Product Not Found!";
            }
            return serviceResponse;
        }

    }
}
