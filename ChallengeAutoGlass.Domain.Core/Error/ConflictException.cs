namespace ChallengeAutoGlass.Domain.Core.Error
{
    public abstract class ConflictException : BaseException
    {
        protected ConflictException(string code, string message) : base(code, message) { }
    }
}
