using ChallengeAutoGlass.Domain.Entities;
using ChallengeAutoGlass.Domain.Repositories;
using ChallengeAutoGlass.Domain.Services;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace ChallengeAutoGlass.Tests.Services
{
    public class ProductServiceTest
    {
        private readonly IProductService _subject;
        private readonly Mock<IProductRepository> _productRepository = new();

        private readonly Product _product;
        private readonly List<Product> _products;

        public ProductServiceTest()
        {
            _product = new Product()
            {
                ProductCode = 1,
                Description = "teste",
                Status = true,
                FabricateDate = DateTime.Now,
                ValidityteDate = DateTime.Now.AddDays(4),
                ProviderCode = 2505,
                CNPJ = "0000.0000.0000.0000"
            };

            _products = new List<Product>()
            {
                _product,
            };

           _productRepository.Setup(x => x.GetProductByCodeAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_product)
                .Verifiable();
                

            _subject = new ProductService(_productRepository.Object);
        }

        [Fact]
        public async void GetProductByCode_WhenProductExists_ShouldReturnProduct()
        {
            var response = await _subject.GetProductByCodeAsync(It.IsAny<int>(), It.IsAny<CancellationToken>());

            _productRepository.VerifyAll();
            response.ShouldBeEquivalentTo(_product);
        }
    }
}
