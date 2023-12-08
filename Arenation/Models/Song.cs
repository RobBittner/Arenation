namespace Arenation.Models; 

public class Song {
	public string Path { get; set; }
	public string Title { get; set; }

	public Song(string path) {
		this.Path = path;

		var tag = TagLib.File.Create(path);
		this.Title = tag.Tag.Title;
	}

	public override string ToString() {
		return $"{this.Title}";
	}
}