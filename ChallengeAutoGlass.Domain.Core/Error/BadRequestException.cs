namespace ChallengeAutoGlass.Domain.Core.Error
{
    public abstract class BadRequestException : BaseException
    {
        protected BadRequestException(string code, string message) : base(code, message) { }
    }
}
