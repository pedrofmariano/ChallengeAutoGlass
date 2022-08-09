namespace ChallengeAutoGlass.Domain.Core.Error
{
    public abstract class InternalServerErrorException : BaseException
    {
        protected InternalServerErrorException(string code, string message) : base(code, message) { }
    }
}
