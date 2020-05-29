using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VideoCaptureApp.Services;
using VideoCaptureApp.ViewModel;

namespace VideoCaptureApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }
 
        public IConfiguration Configuration { get; private set; }
 
        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
 
            Configuration = builder.Build();
 
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
 
            ServiceProvider = serviceCollection.BuildServiceProvider();
 
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
 
        private void ConfigureServices(IServiceCollection services)
        {
            // ...
 
            //services.AddTransient(typeof(MainWindow));
            services.AddTransient<MainWindow>();
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<IVideoCaptureService, VideoCaptureService>();
        }
    }
}
