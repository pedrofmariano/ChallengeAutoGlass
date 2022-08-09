using System;

namespace ChallengeAutoGlass.Domain.Model
{
    public class ProductModel
    {
        public string Description { get; set; }
        public bool? Status { get; set; }
        public DateTimeOffset? FabricateDate { get; set; }
        public DateTimeOffset? ValidityteDate { get; set; }
        public int? ProviderCode { get; set; }
        public string ProviderDescription { get; set; }
        public string CNPJ { get; set; }
    }
}
