using System;

namespace WeatherApp.Interfaces
{
    public interface ICaching
    {
        bool Put<T>(string key, object value, TimeSpan timeSpan);
        T Get<T>(string key) where  T:class ;
    }
}
