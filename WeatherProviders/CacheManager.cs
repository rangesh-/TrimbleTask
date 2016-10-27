using WeatherApp.Interfaces;

namespace WeatherProviders
{
    /// <summary>
    /// Get Instance of Cache to serve cache functionalities
    /// </summary>
    public class CacheManager
    {
        private static ICaching _instance;
        private static readonly object InstanceLock = new object();

        private CacheManager()
        { }

        public static ICaching Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (InstanceLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new CacheProvider();
                        }
                    }
                }
                return _instance;
            }
        }

    }
}
