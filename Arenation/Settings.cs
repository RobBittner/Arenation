using Arenation.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json.Serialization;
using System.Text.Json;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Arenation; 

public class Settings {
	private Album? currentAlbum;
	private Song? currentSong;

	public static Settings LoadSettings(string file, string musicPath) {
		if (!File.Exists(file)) return new Settings(file, musicPath);
		var text = File.ReadAllText(file);
		var settings = JsonSerializer.Deserialize<Settings>(text);
		if (settings == null) return new Settings(file, musicPath);

		settings.FileName = file;
		settings.MusicPath = musicPath;
		return settings;
	}
	

	[JsonIgnore]
	public string? FileName { get; set; }

	public string MusicPath { get; set; }

	public Album? CurrentAlbum {
		get => this.currentAlbum;
		set { 
			this.currentAlbum = value;
			this.Save();
		}
	}

	public Song? CurrentSong {
		get => this.currentSong;
		set { 
			this.currentSong = value;
			this.Save();
		}
	}

	public ObservableCollection<Album> Queue { get; } = new();

	public IList<Album> AllAlbums { get; } = new List<Album>();

	[JsonConstructor]
	public Settings(string? fileName, string musicPath) { 
		this.FileName = fileName; 
		this.MusicPath = musicPath;

		this.Queue.CollectionChanged += this.Queue_CollectionChanged;
	}

	private void Queue_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
		this.Save();
	}

	public void RefreshAlbums() { 
		if (this.MusicPath == null) throw new ArgumentNullException(nameof(this.MusicPath));

		if (! Directory.Exists(this.MusicPath)) throw new ArgumentException($"Folder {this.MusicPath} does not exist", nameof(this.MusicPath));
		
		var di = new DirectoryInfo(this.MusicPath);
		foreach (var artistDi in di.GetDirectories()) {
			foreach (var album in artistDi.GetDirectories()) {
				if (this.AllAlbums.All(q => q.Path != album.FullName)) {
					this.AllAlbums.Add(new Album (album.FullName));
				}
			}
		}

		foreach (var albumPath in this.AllAlbums) { 
			if (!Directory.Exists(albumPath.Path)) this.AllAlbums.Remove(albumPath);
		}
	}

	public void PopulateQueue(int numberOfAlbums) {
		if (this.AllAlbums == null) throw new Exception("AllAlbums not Populated.");

		if (numberOfAlbums > this.AllAlbums.Count) numberOfAlbums = this.AllAlbums.Count;
		
		var rand = new Random();

		while (this.Queue.Count < numberOfAlbums) {
			var tempAlbums = this.AllAlbums.Where(q => q.LastPlayed == DateTime.MinValue).ToList();

			if (tempAlbums.Count < numberOfAlbums) {
				tempAlbums.AddRange(this.AllAlbums.OrderBy(q => q.LastPlayed).Take(numberOfAlbums - tempAlbums.Count));
			}

			var added = false;

			while (!added) {
				var index = rand.Next(tempAlbums.Count);
				var album = tempAlbums[index];
				
				if (this.Queue.Contains(album)) continue;

				added = true;
				this.Queue.Add(album);
			}
		}
	}

	public void Save() {
		if (this.FileName == null) return;
		File.WriteAllText(this.FileName, JsonSerializer.Serialize(this));
	}
}