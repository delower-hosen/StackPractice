namespace Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class RateLimitAttribute(string period = "60s", int limit = 5) : Attribute
    {
        public string Period { get; } = period;
        public int Limit { get; } = limit;
    }
}
