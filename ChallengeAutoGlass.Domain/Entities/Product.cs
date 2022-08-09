using ChallengeAutoGlass.Domain.Core.DomainObjects;
using System;
using System.Collections.Generic;

namespace ChallengeAutoGlass.Domain.Entities
{
    public class Product
    {
        public int ProductCode { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public DateTimeOffset FabricateDate { get; set; }
        public DateTimeOffset ValidityteDate { get; set; }
        public int ProviderCode { get; set; }
        public string ProviderDescription { get; set; }
        public string CNPJ { get; set; }

        public Product() { }

        public Product(string description, DateTimeOffset fabricateDate, DateTimeOffset validityDate)
        {
            Description = description;
            FabricateDate = fabricateDate; 
            ValidityteDate =  validityDate;

            Validate();
        }

        public void Validate()
        {
            Validations.ValidarSeNulo(Description, "Product description can't be null");
            Validations.GreaterDateValidate(FabricateDate, ValidityteDate, "Fabricate Date can't be greater validity date");
        }

        public static implicit operator Product(List<Product> v)
        {
            throw new NotImplementedException();
        }
    }
}
