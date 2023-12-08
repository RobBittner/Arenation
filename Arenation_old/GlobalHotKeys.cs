using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Arenation; 

public class GlobalHotKeys : IDisposable {
	[DllImport("user32.dll")]
	private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);
	[DllImport("user32.dll")]
	private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

	private int currentKeyId;

	private readonly Window window = new();

	public event EventHandler<Keys>? KeyPressed;

	public GlobalHotKeys() {
		this.window.KeyPressed += delegate(object? _, Keys key) {
			this.KeyPressed?.Invoke(this, key);
		};
	}

	public void RegisterHotKey(Keys key) { 
		if (!RegisterHotKey(window.Handle, this.currentKeyId++, 0, (int)key))
			throw new InvalidOperationException("Couldn't register the hot key.");
	}


	public void Dispose() {
		for (var i = 0; i < this.currentKeyId; i++) { 
			UnregisterHotKey(this.window.Handle, i);
		}

		this.window.Dispose();
	}


	private sealed class Window : NativeWindow, IDisposable {
		private const int WmHotKey = 0x0312;
		public event EventHandler<Keys>? KeyPressed;

		public Window() {
			this.CreateHandle(new CreateParams());
		}

		protected override void WndProc(ref Message m) {
			base.WndProc(ref m);

			if (m.Msg != WmHotKey) return;

			var key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);
			var modifier = (KeyModifier)((int)m.LParam & 0xFFFF);

			key = modifier switch { 
				KeyModifier.Control => Keys.Control | key,
				KeyModifier.Alt => Keys.Alt | key,
				KeyModifier.Shift => Keys.Shift | key,
				_ => key
			};

			this.KeyPressed?.Invoke(this, key);
		}

		public void Dispose() {
			this.DestroyHandle();
		}

		public enum KeyModifier {
			None = 0,
			Alt = 1,
			Control = 2,
			Shift = 4,
			WinKey = 8
		}
	}
}