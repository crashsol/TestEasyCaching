using EasyCaching.Core.Interceptor;

namespace TestEasyCaching.Services
{
    public interface IDemoService
    {
        [EasyCachingAble(Expiration = 10)]
        string GetCurrentUtcTime();

        [EasyCachingPut(Expiration = 3600000)]
        string PutSomething(string str);

        [EasyCachingEvict(IsBefore = true)]
        void DeleteSomething(int id);
    }
}
