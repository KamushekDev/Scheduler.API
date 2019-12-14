namespace Contracts.Parser
{
    public interface IParserProgress
    {
        double PercentWorkComplete { get; }
        WorkType CurrentWorkType { get; }
    }
}