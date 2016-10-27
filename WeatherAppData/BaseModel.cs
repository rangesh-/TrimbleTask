namespace WeatherAppData
{
    /// <summary>
    /// This is the Base Model which will be implemented by all
    /// viewmodel class.Has Error indicates if any error has occured in
    /// any where in Data logic which can be used to display errror message in UI 
    /// </summary>
    public class BaseModel
    {
        public bool HasErrors { get; set; }

        public string ErrorMessage { get; set; }
    }
}