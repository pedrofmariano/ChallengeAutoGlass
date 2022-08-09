using ChallengeAutoGlass.Domain.Entities;
using ChallengeAutoGlass.Domain.Exceptions;
using ChallengeAutoGlass.Domain.Model;
using ChallengeAutoGlass.Domain.Models;
using ChallengeAutoGlass.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChallengeAutoGlass.Domain.Services {
    public class ProductService : IProductService 
    {

        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository) { 
            _productRepository = productRepository;
        }


        public async Task<Product> GetProductByCodeAsync(int productCode, CancellationToken ctx) 
        {
            try
            {
                var result = await _productRepository.GetProductByCodeAsync(productCode, ctx);
                if(result is null)
                {
                    return null;
                }

                return result;
            }
            catch(Exception ex)
            {
                throw new InternalServerErrorProduct(ex.Message);
            }
        }

        public async Task<PagedResult<Product>> GetProductsAsync(int pageSize, int pageIndex, string query = null) 
        {
            try
            {
                var result = await _productRepository.GetProductsAsync(pageSize,  pageIndex,  query);
                if (result is null)
                {
                    throw new NotFoundProductException("product is not found");
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorProduct(ex.Message);
            }
        }

        public async Task<bool> InsertNewProduct(ProductModel productModel, CancellationToken ctx)
        {
            try
            {

                var product = new Product(productModel.Description, (DateTimeOffset)productModel.FabricateDate, (DateTimeOffset)productModel.ValidityteDate)
                {
                    Status = (bool)productModel.Status,
                    ProviderCode = (int)productModel.ProviderCode,
                    ProviderDescription = productModel.ProviderDescription,
                    CNPJ = productModel.CNPJ

                };
                return await _productRepository.InsertProductAsync(product, ctx);
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorProduct(ex.Message);
            }
        }
        
        public async Task<bool> UpdateProduct(int productCode, ProductModel productModel, CancellationToken ctx)
        {
            try
            {
                var productDb =  await _productRepository.GetProductByCodeAsync(productCode, ctx);


                var productUpdate = new ProductModel()
                {
                    Description = productModel.Description is null ? productDb.Description : productModel.Description,
                    Status = productModel.Status is null ? productDb.Status : productModel.Status,
                    FabricateDate = productModel.FabricateDate is null  ? productDb.FabricateDate : productModel.FabricateDate,
                    ValidityteDate = productModel.ValidityteDate is null ? productDb.FabricateDate : productModel.FabricateDate,
                    ProviderCode = productModel.ProviderCode is null ? productDb.ProviderCode : productModel.ProviderCode,
                    ProviderDescription = productModel.Description is null ? productDb.ProviderDescription : productModel.ProviderDescription,
                    CNPJ = productModel.CNPJ is null ? productDb.CNPJ : productModel.CNPJ,
                };

                var product = new Product(productUpdate.Description, (DateTimeOffset)productUpdate.FabricateDate, (DateTimeOffset)productUpdate.ValidityteDate)
                {
                    Status = (bool)productUpdate.Status,
                    ProviderCode = (int)productUpdate.ProviderCode,
                    ProviderDescription = productUpdate.ProviderDescription,
                    CNPJ = productUpdate.CNPJ

                };

                return await _productRepository.UpdateProductAsync(productCode, product, ctx);
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorProduct(ex.Message);
            }
        }

        public async Task<bool> DeleteProduct(int productCode, CancellationToken ctx)
        {
            try
            {
                return await _productRepository.DeleteProductAsync(productCode, ctx);
            }
            catch(Exception ex)
            {
                throw new InternalServerErrorProduct(ex.Message);
            }
        }
    }
}
