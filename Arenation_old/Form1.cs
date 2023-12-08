using Arenation.Models;

using Microsoft.Win32;

using Timer = System.Threading.Timer;
// ReSharper disable LocalizableElement

namespace Arenation;

public partial class Form1:Form {
	private readonly GlobalHotKeys globalHotKeys;
	private readonly Playback playback;
	private readonly Settings settings;

	private readonly Timer songPosition;
	private bool wasPlaying;
	private string? songLength;
	private readonly int numberOfAlbumsInQueue = 10;
	
	public Form1() {
		this.InitializeComponent();

		this.globalHotKeys = new GlobalHotKeys();
		this.globalHotKeys.RegisterHotKey(Keys.MediaPlayPause);
		this.globalHotKeys.KeyPressed += this.GlobalHotKeysOnKeyPressed;
		SystemEvents.SessionSwitch += this.SystemEventsOnSessionSwitch;

		this.playback = new Playback();
		this.playback.PlaybackStopped += this.PlaybackOnPlaybackStopped;

		this.songPosition = new Timer(this.SongPositionCallback);

		this.settings = Settings.LoadSettings("settings.json");
		this.settings.MusicPath ??= Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);

		this.settings.RefreshAlbums();

		if (this.settings.Queue == null || this.settings.Queue.Count < this.numberOfAlbumsInQueue) {
			this.settings.PopulateQueue(numberOfAlbumsInQueue);
			if (!this.settings.Queue!.Any()) {
				MessageBox.Show("No Albums Loaded");
				this.Close();
				return;
			}

			this.settings.Save();
		}

		foreach (var album in this.settings.Queue!) this.AlbumsListBox.Items.Add(album);

		if (this.settings.CurrentAlbum == null || this.settings.CurrentSong == null) {
			this.AlbumsListBox.SelectedIndex = 0;
			this.SongsListBox.SelectedIndex = 0;

			this.settings.CurrentAlbum = (Album)this.AlbumsListBox.Items[0];
			this.settings.CurrentSong = (Song)this.SongsListBox.Items[0];

			this.settings.CurrentAlbum.LastPlayed = DateTime.Now;

			this.settings.Save();
		}

		this.AlbumsListBox.SelectedIndex = 0;

		this.PopulateSongs();
	}

	private void PlaybackOnPlaybackStopped(object? sender, PlaybackStoppedEventArgs e) {
		if (e.EndOfFile) {
			this.PlayNextSong();
		}
	}

	private void PlayNextSong() {
		var index = this.SongsListBox.SelectedIndex+1;

		if (this.SongsListBox.Items.Count == index) {
			this.PlayNextAlbum();
			return;
		}

		this.SongsListBox.SelectedIndex = index;
		this.Play();

		var song = (Song)this.SongsListBox.Items[this.SongsListBox.SelectedIndex];
		this.settings.CurrentSong = song;
		this.settings.Save();
	}

	private void SongPositionCallback(object? state) {
		this.Invoke(() => {
			var position = (int)(100.0 * this.playback.Position / this.playback.Length);
			this.PositionTrackBar.Value = position;
			var ts = new TimeSpan(this.playback.Position.Value * 10000);

			this.TimeElapsedLabel.Text = $"{ts:m\\:ss} / {this.songLength}";
		});
	}

	private void PopulateSongs() {
		this.SongsListBox.Items.Clear();
		var album = (Album)this.AlbumsListBox.Items[this.AlbumsListBox.SelectedIndex];

		foreach (var song in album.Songs()) {
			this.SongsListBox.Items.Add(song);
		}

		this.SongsListBox.SelectedIndex = 0;
	}

	private void GlobalHotKeysOnKeyPressed(object? sender, Keys e) {
		if (e == Keys.MediaPlayPause) {
			this.StartStop();
		} else if (e == Keys.MediaNextTrack) {
			this.PlayNextSong();
		}
	}

	private void SystemEventsOnSessionSwitch(object sender, SessionSwitchEventArgs e) {
		if (e.Reason == SessionSwitchReason.SessionLock) {
			if (this.playback.IsPlaying) {
				this.playback.Stop();
				this.wasPlaying = true;
			} else {
				this.wasPlaying = false;
			}
		} else if (e.Reason == SessionSwitchReason.SessionUnlock) {
			if (this.wasPlaying) this.playback.Play();
		}
	}

	private void StartStopButton_Click(object sender, EventArgs e) {
		this.StartStop();
	}

	private void StartStop() {
		if (this.StartStopButton.Text == ">") {
			this.Play();
		} else {
			this.Stop();
		}
	}

	private void Stop() {
		this.playback.Stop();
		this.StartStopButton.Text = ">";
		this.songPosition.Change(Timeout.Infinite, 500);
	}

	private void Play() {
		this.playback.Play();
		this.StartStopButton.Text = "| |";
		this.songPosition.Change(500, 500);
	}

	private void VolumeTrackBar_Scroll(object sender, EventArgs e) {
		this.playback.SetVolume(this.VolumeTrackBar.Value);
	}

	private void SongsListBox_SelectedIndexChanged(object sender, EventArgs e) {
		var song = (Song)this.SongsListBox.Items[this.SongsListBox.SelectedIndex];
		this.settings.CurrentSong = song;
		this.settings.Save();

		if (this.playback.IsPlaying) {
			this.playback.Stop();
			this.playback.Load(song.Path);
			this.playback.Play();
		} else {
			this.playback.Load(song.Path);
		}
		var ts = new TimeSpan(this.playback.Length.Value * 10000);
		this.songLength = ts.ToString("m\\:ss");
	}

	private void AlbumsListBox_SelectedIndexChanged(object? sender, EventArgs e) {
		this.AlbumsListBox.SelectedIndexChanged -= this.AlbumsListBox_SelectedIndexChanged;

		var album = (Album)this.AlbumsListBox.Items[this.AlbumsListBox.SelectedIndex];
		this.AlbumsListBox.Items.Remove(album);
		this.AlbumsListBox.Items.Insert(0, album);
		this.AlbumsListBox.SelectedIndex = 0;

		this.PopulateSongs();
		this.AlbumsListBox.SelectedIndexChanged += this.AlbumsListBox_SelectedIndexChanged;

		var song = (Song)this.SongsListBox.Items[0];

		this.settings.CurrentAlbum = album;
		this.settings.CurrentSong = song;
		this.settings.CurrentAlbum.LastPlayed = DateTime.Now;
		this.settings.Save();
	}

	private void PositionTrackBar_Scroll(object sender, EventArgs e) {
		var position = (long)(this.playback.Length * (this.PositionTrackBar.Value / 100.0));

		this.playback.Cue(position);
	}

	private void NextSongButton_Click(object sender, EventArgs e) {
		if (ModifierKeys.HasFlag(Keys.Alt)) {
			this.PlayNextAlbum();
		} else {
			this.PlayNextSong();
		}
	}

	private void PlayNextAlbum() {
		this.AlbumsListBox.SelectedIndexChanged -= this.AlbumsListBox_SelectedIndexChanged;

		this.AlbumsListBox.SelectedIndex++;
		this.AlbumsListBox.Items.RemoveAt(0);
		this.AlbumsListBox.SelectedIndex = 0;
		this.PopulateSongs();
		this.SongsListBox.SelectedIndex = 0;

		this.settings.Queue!.RemoveAt(0);

		if (this.AlbumsListBox.Items.Count < this.numberOfAlbumsInQueue) {
			this.settings.PopulateQueue(this.numberOfAlbumsInQueue);

			foreach (var album in this.settings.Queue!) {
				if (this.AlbumsListBox.Items.Contains(album)) continue;
				this.AlbumsListBox.Items.Add(album);
			}
		}

		this.settings.CurrentAlbum = (Album)this.AlbumsListBox.Items[0];
		this.settings.CurrentAlbum.LastPlayed = DateTime.Now;
		this.settings.CurrentSong = (Song)this.SongsListBox.Items[0];
		this.settings.Save();
		
		this.AlbumsListBox.SelectedIndexChanged += this.AlbumsListBox_SelectedIndexChanged;
	}

	~Form1() {
		this.songPosition.Change(Timeout.Infinite, Timeout.Infinite);
		this.songPosition.Dispose();
		this.globalHotKeys.Dispose();
		this.playback.Dispose();
	}
}