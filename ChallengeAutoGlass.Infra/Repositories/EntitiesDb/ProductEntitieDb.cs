using System;

namespace ChallengeAutoGlass.Infra.Repositories.EntitiesDb
{
    public class ProductEntitieDb
    {
        public int product_code { get; set; }
        public string description { get; set; }
        public bool status { get; set; }
        public DateTimeOffset fabricate_date { get; set; }
        public DateTimeOffset validity_date { get; set; }
        public int provider_code { get; set; }
        public string provider_description { get; set; }
        public string CNPJ { get; set; }
    }
}
