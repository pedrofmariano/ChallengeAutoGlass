using System;

namespace ChallengeAutoGlass.Domain.Core.Error
{
    public abstract class BaseException : Exception
    {
        public virtual string Code { get; }

        protected BaseException(string code, string message) : base(message)
        {
            Code = code;
        }

    }
}
