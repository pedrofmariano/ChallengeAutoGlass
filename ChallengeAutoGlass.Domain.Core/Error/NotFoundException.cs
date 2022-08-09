namespace ChallengeAutoGlass.Domain.Core.Error
{
    public abstract class NotFoundException : BaseException
    {
        protected NotFoundException(string code, string message) : base(code, message) { }
    }
}
