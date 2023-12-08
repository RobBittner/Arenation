using System.Text.Json;
using System.Text.Json.Serialization;
using Arenation.Models;
// ReSharper disable LocalizableElement

namespace Arenation; 

public class Settings {
	public static Settings LoadSettings(string file) {
		if (!File.Exists(file)) return new Settings(file);
		var settings = JsonSerializer.Deserialize<Settings>(File.ReadAllText(file));
		if (settings == null) return new Settings(file);

		settings.FileName = file;
		return settings;
	}

	[JsonIgnore]
	public string FileName { get; set; }

	public string? MusicPath { get; set; }

	public Album? CurrentAlbum { get; set; }

	public Song? CurrentSong { get; set; }

	public IList<Album>? Queue { get; set; }

	public IList<Album>? AllAlbums { get; set; }

	[JsonConstructor]
	public Settings(string fileName) { this.FileName = fileName; }

	public void RefreshAlbums() { 
		if (this.MusicPath == null) throw new ArgumentNullException(nameof(this.MusicPath));

		if (! Directory.Exists(this.MusicPath)) throw new ArgumentException($@"Folder {this.MusicPath} does not exist", nameof(this.MusicPath));

		this.AllAlbums ??= new List<Album>();

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
		if (this.AllAlbums == null) throw new ArgumentException("AllAlbums not Populated.", nameof(this.AllAlbums));

		if (numberOfAlbums > this.AllAlbums.Count) numberOfAlbums = this.AllAlbums.Count;
		
		this.Queue ??= new List<Album>();
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
		File.WriteAllText(this.FileName, JsonSerializer.Serialize(this));
	}
}