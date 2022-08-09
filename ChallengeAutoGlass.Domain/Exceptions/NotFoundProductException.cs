using ChallengeAutoGlass.Domain.Core.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeAutoGlass.Domain.Exceptions
{
    public class NotFoundProductException : NotFoundException
    {
        public NotFoundProductException(string message) : base("notfound_product", message) { }
    }
}
