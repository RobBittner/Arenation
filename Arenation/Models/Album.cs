using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Arenation.Models; 

public class Album :INotifyPropertyChanged{
	private bool isSelected;
	public string Name { get; set; }
	public string Artist { get; set; }
	public string Path { get; set; }
	public DateTime LastPlayed { get; set; }

	public bool IsSelected {
		get => this.isSelected;
		set {
			if (value == this.isSelected) return;
			this.isSelected = value;
			this.OnPropertyChanged();
		}
	}

	public event PropertyChangedEventHandler? PropertyChanged;


	public Album(string path) {
		var di = new DirectoryInfo(path);
		this.Name = di.Name;
		this.Artist = di.Parent!.Name;
		this.Path = path;
		this.LastPlayed = DateTime.MinValue;
	}

	public IEnumerable<Song> Songs() {
		return 
			Directory
				.GetFiles(this.Path)
				.Where(q => q.EndsWith("mp3", StringComparison.CurrentCultureIgnoreCase) ||
				            q.EndsWith("m4a", StringComparison.CurrentCultureIgnoreCase) ||
				            q.EndsWith("wma", StringComparison.CurrentCultureIgnoreCase))
				.Select(path => new Song(path));
	}

	public override string ToString() {
		return $"{this.Artist} : {this.Name}";
	}

	
	protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}

	
}