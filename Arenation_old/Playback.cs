using Arenation.Models;
using NAudio.Wave;

namespace Arenation; 

public class Playback : IDisposable {
	private readonly WaveOutEvent outputDevice = new();
	private AudioFileReader? audioFile;

	public event EventHandler<PlaybackStoppedEventArgs>? PlaybackStopped;

	public Playback() {
		this.outputDevice.PlaybackStopped += this.OutputDeviceOnPlaybackStopped;
	}
	
	public void Load(string filename) { 
		this.audioFile = new AudioFileReader(filename);
		this.outputDevice.Init(this.audioFile);
	}

	public void Cue(long ms) {
		if (this.audioFile == null) throw new Exception("Audio file not loaded.");
		var position = (long)(ms *
		               this.outputDevice.OutputWaveFormat.BitsPerSample *
		               this.outputDevice.OutputWaveFormat.Channels / 8 *
		               this.outputDevice.OutputWaveFormat.SampleRate
		               / 1000.0);
		this.audioFile.Position = position;
	}

	public bool IsPlaying => this.outputDevice.PlaybackState == PlaybackState.Playing;

	public void Play() { 
		this.outputDevice.Play();
	}

	public long? Position => (long?)
		(this.audioFile?.Position * 1000.0 /
		this.outputDevice.OutputWaveFormat.BitsPerSample /
		this.outputDevice.OutputWaveFormat.Channels * 8 /
		this.outputDevice.OutputWaveFormat.SampleRate);
		
	

	public long? Length => 
		(long)(this.audioFile?.Length * 1000.0 /
	this.outputDevice.OutputWaveFormat.BitsPerSample /
	this.outputDevice.OutputWaveFormat.Channels * 8 /
	this.outputDevice.OutputWaveFormat.SampleRate);

	public void Stop() { 
		this.outputDevice.Stop();
	}

	public void SetVolume(int volume) {
		this.outputDevice.Volume = volume / 100f;
	}

	public void Dispose() {
		this.outputDevice.Dispose();
		this.audioFile?.Dispose();
	}

	private void OutputDeviceOnPlaybackStopped(object? sender, StoppedEventArgs e) {
		if (this.audioFile == null) {
			this.PlaybackStopped?.Invoke(this, new PlaybackStoppedEventArgs(false, new Exception("Audio File not loaded.")));
			return;
		}

		var endOfFile = this.audioFile.Position >= this.audioFile.Length;
		this.PlaybackStopped?.Invoke(this, new PlaybackStoppedEventArgs(endOfFile, e.Exception));
	}
}