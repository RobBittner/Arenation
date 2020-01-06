// ----------------------------------------------------------------------------------------------------
// <copyright file="Form1.cs" company="Obelisk Programming">
//   Obelisk Programming
// </copyright>
// ----------------------------------------------------------------------------------------------------

namespace Arenation {
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Windows.Forms;
  using System.Xml;

  using NAudio.Wave;



    ///  https://stackoverflow.com/questions/805165/reorder-a-winforms-listbox-using-drag-and-drop
    ///  





    /// <summary>
    /// The form 1.
    /// </summary>
    public partial class Form1 : Form {
    private WaveOut waveOutDevice;

    private string musicRoot = @"C:\Users\bittnerrd\Dropbox\MP3";


    public List<Album> PotentialAlbums = new List<Album>();

    private readonly BindingSource albums = new BindingSource();

    private readonly BindingSource songs = new BindingSource();

    /// <summary>
    /// Initializes a new instance of the <see cref="Form1"/> class.
    /// </summary>
    public Form1() {
      this.InitializeComponent();

      this.PopulatePontentialAlbums();

      this.AlbumListBox.DataSource = this.albums;
      this.SongListBox.DataSource = this.songs;

      this.FillAlbumQueue();
      this.FillSongs();

      this.waveOutDevice = new WaveOut();
      this.StopButton.Enabled = false;

      var prefsPath = $"{Environment.SpecialFolder.LocalApplicationData}\\ObeliskProgramming\\Arenation\\prefs.xml";
      var prefs = new XmlDocument();
      if (File.Exists(prefsPath)) {
        prefs.Load(prefsPath);
      } else {
        prefs.LoadXml("<arenation><queuedAlbums/><albums/></arenation>");
      }
    }

    

    public void PopulatePontentialAlbums() {
      var rootDi = new DirectoryInfo(this.musicRoot);

      var allAlbums = new List<Album>();

      foreach (var artistDi in rootDi.GetDirectories()) {
        foreach (var albumDi in artistDi.GetDirectories()) {
          var lastPlayed = DateTime.MinValue;
          foreach (var songFi in
            albumDi.EnumerateFiles("*.*", SearchOption.AllDirectories)
              .Where(q => this.validSongExtension(q.Extension))) {
            if (songFi.LastWriteTime > lastPlayed) {
              lastPlayed = songFi.LastWriteTime;
            }
          }

          allAlbums.Add(
            new Album {
              LastPlayed = lastPlayed,
              AlbumPath = albumDi.FullName,
              AlbumName = albumDi.Name,
              Artist = artistDi.Name
            });
        }
      }

      var albumsToAdd = allAlbums.OrderBy(q => q.LastPlayed).Take(allAlbums.Count / 3).ToList();
      foreach (var album in albumsToAdd) {
        var songFileInfos = 
          from fi in new DirectoryInfo(album.AlbumPath).EnumerateFiles("*.*")
          where this.validSongExtension(fi.Extension)
          select fi;
        foreach (var songFileInfo in songFileInfos) {
          using (var tagFile = TagLib.File.Create(songFileInfo.FullName)) {
            album.Songs.Add(new Song {
              TrackNumber = tagFile.Tag.Track,
              DiscNumber = tagFile.Tag.Disc,
              SongName = tagFile.Tag.Title,
              SongLength = tagFile.Properties.Duration,
              SongPath = songFileInfo.FullName
            });
          }
        }
      }

      this.PotentialAlbums.AddRange(albumsToAdd);
    }

    public void FillSongs() {
      var album = (this.AlbumListBox.SelectedItem ?? this.AlbumListBox.Items[0]) as Album;
      if (album == null) return;
      this.songs.Clear();
      foreach (var s in album.Songs) this.songs.Add(s);
    }

    public void FillAlbumQueue() {
      //Fill with New Albums first

      while (this.albums.Count < 10) {
        var rnd = new Random();
        var r = rnd.Next(this.PotentialAlbums.Count);
        this.albums.Add(this.PotentialAlbums[r]);
        this.PotentialAlbums.RemoveAt(r);
        if (!this.PotentialAlbums.Any()) this.PopulatePontentialAlbums();
      }
    }

    private void StopButtonClick(object sender, EventArgs e) {
      waveOutDevice.Stop();
      this.StopButton.Enabled = false;
    }

    private void PlayPauseButtonClick(object sender, EventArgs e) {

      if (this.waveOutDevice.PlaybackState == PlaybackState.Playing) {
        this.PlayPauseButton.Text = ">";
        this.waveOutDevice.Pause();
        return;
      }

      if (this.songs.Count == 0) {
        return;
      }

      var song = (this.SongListBox.SelectedItem ?? this.SongListBox.Items[0]) as Song;
      if (song == null) return;

      var audioFileReader = new AudioFileReader(song.SongPath);
      this.waveOutDevice.Init(audioFileReader);
      this.waveOutDevice.Play();
      this.PlayPauseButton.Text = "||";
      this.StopButton.Enabled = true;

    }

    private bool validSongExtension(string extension) {
      return 
        extension.EndsWith("mp3", StringComparison.CurrentCultureIgnoreCase) ||
        extension.EndsWith("m4a", StringComparison.CurrentCultureIgnoreCase) ||
        extension.EndsWith("wma", StringComparison.CurrentCultureIgnoreCase);
    }

    private void AlbumListBox_SelectedIndexChanged(object sender, EventArgs e) {
      FillSongs();
    }
  }

  public class Album {
    public string AlbumName { get; set; }

    public string Artist { get; set; }

    public DateTime LastPlayed { get; set; }

    public string AlbumPath { get; set; }

    public List<Song> Songs { get; } = new List<Song>();

    public TimeSpan AlbumLength => new TimeSpan(this.Songs.Sum(q => q.SongLength.Ticks));

    public override string ToString() {
      return $"{this.Artist}: {this.AlbumName}";
    }
  }

  public class Song {
    public string SongName { get; set; }

    public string SongPath { get; set; }

    public TimeSpan SongLength { get; set; }

    public uint TrackNumber { get; set; }

    public uint DiscNumber { get; set; }

    public override string ToString() {
      return $"{this.TrackNumber}: {this.SongName} + {this.SongLength.TotalMinutes:0}:{this.SongLength:ss}";
    }
  }
}