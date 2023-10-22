namespace rizer.Services;

public interface ICounterService
{
    void Stop(); 
    
    int GetCount();
}