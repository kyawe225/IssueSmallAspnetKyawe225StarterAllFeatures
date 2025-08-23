namespace IssueTracker.UI.Services
{
    public interface ILoadingService
    {
        event Action<bool>? LoadingStateChanged;
        bool IsLoading { get; }
        void SetLoading(bool isLoading);
        void ShowLoading(string? message = null);
        void HideLoading();
        string? LoadingMessage { get; }
    }

    public class LoadingService : ILoadingService
    {
        private bool _isLoading = false;
        private string? _loadingMessage;

        public event Action<bool>? LoadingStateChanged;
        
        public bool IsLoading 
        { 
            get => _isLoading; 
            private set
            {
                if (_isLoading != value)
                {
                    _isLoading = value;
                    LoadingStateChanged?.Invoke(_isLoading);
                }
            }
        }

        public string? LoadingMessage 
        { 
            get => _loadingMessage; 
            private set => _loadingMessage = value;
        }

        public void SetLoading(bool isLoading)
        {
            IsLoading = isLoading;
        }

        public void ShowLoading(string? message = null)
        {
            LoadingMessage = message ?? "Loading...";
            IsLoading = true;
        }

        public void HideLoading()
        {
            LoadingMessage = null;
            IsLoading = false;
        }
    }
}