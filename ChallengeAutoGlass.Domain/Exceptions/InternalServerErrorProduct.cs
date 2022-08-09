using ChallengeAutoGlass.Domain.Core.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeAutoGlass.Domain.Exceptions
{
    public class InternalServerErrorProduct : InternalServerErrorException
    {
        public InternalServerErrorProduct(string message) : base("internal_server_error_products", message) { }
    }
}
