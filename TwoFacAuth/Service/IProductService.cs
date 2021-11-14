using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoFacAuth.Areas.Identity.Data;
using TwoFacAuth.Model;

namespace TwoFacAuth.Service
{
    public interface IProductService
    {
        Task<Response<List<ProductDto>>> SaveProductAsync(ProductDto model);
        Task<Response<ProductDto>> UpdateProductAsync(ProductDto model);
        Task<Response<List<ProductDto>>> GetAllProductAsync();
        Task<Response<ProductDto>> GetProductAsync(int id);
        Task<Response<List<ProductDto>>> DeleteProductAsync(int id);
    }
}
