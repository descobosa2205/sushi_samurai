public interface ISamuraiAction
{
    bool CanExecute(IPerformanceMetrics metrics);
    float CooldownSeconds { get; }
    void Execute();
}
