using AutoMapper;
using ChallengeAutoGlass.Domain.Entities;
using ChallengeAutoGlass.Domain.Models;
using ChallengeAutoGlass.Domain.Repositories;
using ChallengeAutoGlass.Infra.Repositories.EntitiesDb;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChallengeAutoGlass.Infra.Repositories
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        private readonly IMapper _mapper;

        public ProductRepository(IMapper mapper) : base()
        {
            _mapper = mapper;
        }

        #region
        private readonly static string GET_PRODUCT_BY_CODE = @"
            SELECT product_code, description, status, fabricate_date, validity_date, provider_code, provider_description, CNPJ FROM dbo.Product
            WHERE
            product_code = @productCode
        ";

        private readonly static string INSERT_PRODUCT = @"
            INSERT INTO [dbo].[Product] 
            ( 
                [description], 
                [status], 
                [fabricate_date], 
                [validity_date], 
                [provider_code], 
                [provider_description], 
                [CNPJ]
            ) 
            VALUES 
            ( 
                @description,
                @status,
                @fabricateDate,
                @validityDate,
                @providerCode,
                @providerDescription,
                @CNPJ
            ) 
        ";

        private readonly static string INACTIVADE_PRODUCT = @"
             UPDATE dbo.Product SET status = 0 WHERE product_code = @productCode
        ";

        private readonly static string UPDATE_PRODUCT = @"
             UPDATE dbo.Product SET
                description = @description,
                status =  @status, 
                fabricate_date = @fabricateDate,
                validity_date = @validityDate,
                provider_code = @providerCode,
                provider_description =  @providerDescription,
                CNPJ = @CNPJ
            WHERE product_code = @productCode
        ";

        #endregion

        public async Task<Product> GetProductByCodeAsync(int productCode, CancellationToken ctx)
        {
            return await WrapConnection(async (connection) =>
            {
                var parameters = new DynamicParameters();
                parameters.Add("@productCode", productCode, DbType.Int32);
                var result = await connection.QueryFirstOrDefaultAsync<ProductEntitieDb>(GET_PRODUCT_BY_CODE, parameters);

                if (result is null)
                {
                    return null;
                }
                return _mapper.Map<ProductEntitieDb, Product>(result);

            }, ctx);
        }

        public async Task<PagedResult<Product>> GetProductsAsync(int pageSize, int pageIndex, string query)
        {
            return await WrapConnection(async (connection) =>
            {
                var sql = @$" SELECT product_code, description, status, fabricate_date, validity_date, provider_code, provider_description, CNPJ 
                    FROM dbo.Product WHERE (@Query IS NULL OR status = @Query )
                    ORDER BY [product_code]
                    OFFSET {@pageSize * (@pageIndex - 1)} ROWS
                    FETCH NEXT {@pageSize} ROWS ONLY
                    SELECT COUNT(product_code) FROM dbo.Product 
                    WHERE (@Query IS NULL OR status = @Query )";
                var result = await connection.QueryMultipleAsync(sql, new { Query = query});


                var products = result.Read<ProductEntitieDb>();
                var total = result.Read<int>().FirstOrDefault();

                return new PagedResult<Product>()
                {
                    List = _mapper.Map<IEnumerable<ProductEntitieDb>, IEnumerable<Product>>(products),
                    TotalResults = total,
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    Query = query
                };

            }, CancellationToken.None);
        }


        public async Task<bool> InsertProductAsync(Product product, CancellationToken ctx)
        {
            return await WrapConnection(async (connection) =>
            {
                var parameters = new DynamicParameters();
                parameters.Add("@description", product.Description, DbType.AnsiString);
                parameters.Add("@status", product.Status, DbType.Byte);
                parameters.Add("@fabricateDate", product.FabricateDate, DbType.DateTimeOffset);
                parameters.Add("@validityDate", product.ValidityteDate, DbType.DateTimeOffset);
                parameters.Add("@providerCode", product.ProviderCode, DbType.Int32);
                parameters.Add("@providerDescription", product.ProviderDescription, DbType.AnsiString);
                parameters.Add("@CNPJ", product.CNPJ, DbType.AnsiString);

                var result = await connection.ExecuteAsync(INSERT_PRODUCT, parameters);

                return result is 0 ? false : true;

            }, ctx);
        }

        public async Task<bool> UpdateProductAsync(int productCode, Product product, CancellationToken ctx)
        {
            return await WrapConnection(async (connection) =>
            {

                var parameters = new DynamicParameters();
                parameters.Add("@productCode", productCode, DbType.Int32);
                parameters.Add("@description", product.Description, DbType.AnsiString);
                parameters.Add("@status", product.Status, DbType.Byte);
                parameters.Add("@fabricateDate", product.FabricateDate, DbType.DateTimeOffset);
                parameters.Add("@validityDate", product.ValidityteDate, DbType.DateTimeOffset);
                parameters.Add("@providerCode", product.ProviderCode, DbType.Int32);
                parameters.Add("@providerDescription", product.ProviderDescription, DbType.AnsiString);
                parameters.Add("@CNPJ", product.CNPJ, DbType.AnsiString);

                var result = await connection.ExecuteAsync(UPDATE_PRODUCT, parameters);

                return result is 0 ? false : true;

            }, ctx);
        }
        public async Task<bool> DeleteProductAsync(int productCode, CancellationToken ctx)
        {
            return await WrapConnection(async (connection) =>
            {
                var parameters = new DynamicParameters();
                parameters.Add("@productCode", productCode, DbType.Int32);
                var result = await connection.ExecuteAsync(INACTIVADE_PRODUCT, parameters);

                return result is 0 ? false : true;

            }, ctx);
        }
    }
}
