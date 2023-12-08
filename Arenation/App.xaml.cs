using System;
using System.Windows;
using Arenation.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Arenation; 

public partial class App {
	protected override void OnStartup(StartupEventArgs e) {
		base.OnStartup(e);

		var services = new ServiceCollection();

		services.AddSingleton<MainWindow>();
		services.AddScoped<MainWindowViewModel>();
		var musicPath = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
		services.AddScoped(_ => Settings.LoadSettings("settings.json", musicPath));

		
		var serviceProvider = services.BuildServiceProvider();

		var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
		var mainWindowViewModel = serviceProvider.GetRequiredService<MainWindowViewModel>();
		mainWindowViewModel.Close += (_,_) => mainWindow.Close();

		mainWindow.DataContext = mainWindowViewModel;
		mainWindow.Show();
	}
}