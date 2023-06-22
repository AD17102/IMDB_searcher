using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using IMDB_test_api.Models;
using IMDB_test_api.ViewModels;
using IMDB_test_api.Views;

namespace IMDB_test_api
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(
                         new IMDB_http_client_getter()),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}