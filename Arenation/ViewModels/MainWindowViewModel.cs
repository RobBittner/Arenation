using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Arenation.Models;

namespace Arenation.ViewModels; 

public class MainWindowViewModel : INotifyPropertyChanged {
	private readonly Settings settings;
	private readonly int numberOfAlbumsInQueue = 10;
	private Album? selectedAlbum;
	public event PropertyChangedEventHandler? PropertyChanged;
	public event EventHandler? Close;

	public ObservableCollection<Album>? Albums { get; set; }

	public Album? SelectedAlbum {
		get => this.selectedAlbum;
		set { 
			this.selectedAlbum = value;
			
			this.OnPropertyChanged();
		}
	}

	public ICommand AlbumsMouseDoubleClick { get; }
	public ICommand WindowLoaded { get; }

	protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}

	public MainWindowViewModel(
		Settings settings
	) {
		this.AlbumsMouseDoubleClick = new RelayCommand<object>(AlbumsMouseDoubleClickExecute);
		this.WindowLoaded = new RelayCommand<object>(WindowLoadedExecute);

		this.settings = settings;
		this.settings.RefreshAlbums();

		if (this.settings.Queue.Count < this.numberOfAlbumsInQueue) {
			this.settings.PopulateQueue(this.numberOfAlbumsInQueue);
			if (!this.settings.Queue.Any()) {
				MessageBox.Show("No Albums Loaded");
				this.Close?.Invoke(this, EventArgs.Empty);
				return;
			}
		}
		
		this.Albums = this.settings.Queue;
		

		/*

		if (this.settings.CurrentAlbum == null || this.settings.CurrentSong == null) {
			this.AlbumsListBox.SelectedIndex = 0;
			this.SongsListBox.SelectedIndex = 0;

			this.settings.CurrentAlbum = (Album)this.AlbumsListBox.Items[0];
			this.settings.CurrentSong = (Song)this.SongsListBox.Items[0];

			this.settings.CurrentAlbum.LastPlayed = DateTime.Now;
		}


*/

	}

	private void WindowLoadedExecute(object obj) {
		this.SelectedAlbum = this.settings.CurrentAlbum;
	}

	private void AlbumsMouseDoubleClickExecute(object obj) {
		throw new NotImplementedException();
	}
}