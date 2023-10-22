using System.Diagnostics.Metrics;
using System.Reflection;

namespace rizer.Services;

public class CounterService : ICounterService
{
    private readonly ILogger<CounterService> _logger;
    private volatile bool _running;
    private int _count;

    public CounterService(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<CounterService>();
        _count = 0;
        _running = false;
    }

    private void Start()
    {
        Task.Factory.StartNew(() =>
        {
            while (_running)
            {
                Thread.Sleep(100);
                _logger.LogInformation("count {}", _count);
                _count++;
            }
        });
    }

    public int GetCount()
    {
        if (_running)
        {
            return _count;
        }
        Start();
        _running = true;
        return _count;
    }

    public void Stop()
    {
        _running = false;
        _logger.LogInformation("Stopping");
    }
}