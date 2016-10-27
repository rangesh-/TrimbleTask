using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WeatherApp.Interfaces;
using WeatherAppData;
using WeatherProviders;


namespace WeatherWidget.ViewModel
{
    public class WeatherFinderByCityViewModel : ViewModelBase
    {
        #region Declarations
        private string _getcurrentlyselectedcity;
        private string _getselectedprovider;
        private ICityFinderProvider _cityprovider;
        private IWeatherProvider _provider;
        private ObservableCollection<WeatherInfo> _weatherinfo;
        private ObservableCollection<CityInfo> _cityInfo;

        public string ErrorMessage { get; set; }
        public bool IsBusy { get; set; }
        public RelayCommand SearchCommand { get; set; }
        #endregion
        #region Methods
        public WeatherFinderByCityViewModel()
        {
            SearchCommand = new RelayCommand(SearchWeatherInfoByCity, CanSearch);
            _provider = SimpleIoc.Default.GetInstance<IWeatherProvider>();
            _cityprovider = SimpleIoc.Default.GetInstance<ICityFinderProvider>();
            _weatherinfo = new ObservableCollection<WeatherInfo>();
            _cityInfo = new ObservableCollection<CityInfo>();
            IsBusy = true;
        }

        /// <summary>
        /// Get City for India-Makes an External call and fetches city
        /// </summary>
        public List<string> GetCity
        {
            get
            {
                var listofcities = new List<string>() { "Select" };
                var result =_cityprovider.FindCity(); 
                result.ContinueWith(arg => {
                    if (result.Result.HasErrors)
                    {
                        ErrorMessage = result.Result.ErrorMessage;
                    }
                    else
                    {
                        listofcities.AddRange(result.Result.city);
                        _getcurrentlyselectedcity = listofcities[0];
                    }

                });
                return listofcities;
            }
        }

        /// <summary>
        /// Resolve Providers using IOC
        /// </summary>
        public List<string> GetWeatherProviders
        {
            get
            {
                var listofproviders = new List<string>() { "Yahoo", "OpenWeather" };
                _getselectedprovider = listofproviders[0];
                return listofproviders;
            }
        }

        public List<string> GetCountry => new List<string>() { "India" };

        /// <summary>
        /// MVVM Binding of Selected City in Combobox.
        /// </summary>
        public string GetCurrentlySelectedCity
        {
            get
            {
                return _getcurrentlyselectedcity;
            }
            set
            {
                _getcurrentlyselectedcity = value;
                RaisePropertyChanged("GetCurrentlySelectedCity");
            }
        }

        /// <summary>
        ///  MVVM Binding of Providers in Combobox
        /// </summary>
        public string GetSelectedProvider
        {
            get
            {
                return _getselectedprovider;
            }
            set
            {
                _getselectedprovider = value;
                if (value == "Yahoo")
                {
                    SimpleIoc.Default.Unregister<IWeatherProvider>();
                    SimpleIoc.Default.Register<IWeatherProvider, YahooWeatherProvider>();
                    _provider = SimpleIoc.Default.GetInstance<IWeatherProvider>();
                }
                else
                {
                    SimpleIoc.Default.Unregister<IWeatherProvider>();
                    SimpleIoc.Default.Register<IWeatherProvider, OpenWeatherAppProvider>();
                    _provider = SimpleIoc.Default.GetInstance<IWeatherProvider>();
                }


                RaisePropertyChanged("GetSelectedProvider");
            }
        }
        /// <summary>
        /// Search result of Weather Info as Observable collection 
        /// </summary>
        public ObservableCollection<WeatherInfo> GetWeatherSearchResult
        {
            get
            {
                return _weatherinfo;
            }
            set
            {
                _weatherinfo = value;
                RaisePropertyChanged("GetWeatherSearchResult");
            }
        }
        /// <summary>
        /// Onclick Action Method of Search Button
        /// </summary>
        public void SearchWeatherInfoByCity()
        {
            if (_getcurrentlyselectedcity.Equals("Select"))
                return;
            
            IsBusy = false;
            _weatherinfo.Clear();
            var result = _provider.Fetch5DayWeatherData(_getcurrentlyselectedcity, "IN");
            result.ContinueWith(args =>
            {
                if (result.Result.HasErrors)
                {
                    ErrorMessage = result.Result.ErrorMessage;
                }
                else
                {
                    foreach (var item in result.Result.WeatherInfo)
                    {
                        App.Current.Dispatcher.Invoke(() =>
                        {
                            _weatherinfo.Add(item);
                        });
                    }
                    _cityInfo.Clear();
                    _cityInfo.Add(new CityInfo()
                    {
                        Latitude = result.Result.CityInfo.Latitude,
                        Longitude = result.Result.CityInfo.Longitude,
                        CityName = _getcurrentlyselectedcity
                    });
                }
                IsBusy = false;
            });

        }
        /// <summary>
        /// Left Pane which display city Information
        /// </summary>
        public ObservableCollection<CityInfo> CityInfo
        {
            get
            {
                return _cityInfo;
            }
            set
            {
                _cityInfo = value;
                RaisePropertyChanged("CityInfo");
            }
        }

        /// <summary>
        /// Disable Search Button when busy.
        /// </summary>
        /// <returns></returns>
        public bool CanSearch()
        {
            return true;
        } 
        #endregion

    }
}
