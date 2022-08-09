using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChallengeAutoGlass.Domain.Core.DomainObjects {
    public class Validations {

        public static void GreaterDateValidate (DateTimeOffset date1, DateTimeOffset date2, string message) {
            if (date1 > date2) {
                throw new DomainException(message);
            }
        }

        public static void ValidarSeNulo(object object1, string message) {
            if (object1 == null) {
                throw new DomainException(message);
            }
        }
    }
}
