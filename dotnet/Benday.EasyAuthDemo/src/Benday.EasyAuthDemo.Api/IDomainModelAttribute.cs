namespace Benday.EasyAuthDemo.Api
{
    public interface IDomainModelAttribute
    {
        string AttributeKey { get; set; }
        string AttributeValue { get; set; }
    }
}
