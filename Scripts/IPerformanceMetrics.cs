public interface IPerformanceMetrics
{
    int CurrentStreak { get; }
    float SecondsSinceLastMiss { get; }
}

