namespace Arenation.Models;

public class PlaybackStoppedEventArgs : EventArgs { 
	public Exception? Exception {get; set; }
	public bool EndOfFile { get; set; }

	public PlaybackStoppedEventArgs(bool endOfFile, Exception? exception) {
		this.EndOfFile = endOfFile;
		this.Exception = exception;
	}
}