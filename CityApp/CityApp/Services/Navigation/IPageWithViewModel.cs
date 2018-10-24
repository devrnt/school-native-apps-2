namespace CityApp.Services.Navigation
{
    // Every Page with a corresponding viewmodel should have these 2 methods
    // Easy to re-use
    public interface IPageWithViewModel<T> where T : class
    {
        T ViewModel { get; set; }
    }
}
