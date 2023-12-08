namespace Arenation {
	partial class Form1 {
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			var resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			AlbumsListBox=new ListBox();
			StartStopButton=new Button();
			VolumeTrackBar=new TrackBar();
			label1=new Label();
			SongsListBox=new ListBox();
			label2=new Label();
			PositionTrackBar=new TrackBar();
			TimeElapsedLabel=new Label();
			NextSongButton=new Button();
			((System.ComponentModel.ISupportInitialize)VolumeTrackBar).BeginInit();
			((System.ComponentModel.ISupportInitialize)PositionTrackBar).BeginInit();
			this.SuspendLayout();
			// 
			// AlbumsListBox
			// 
			AlbumsListBox.FormattingEnabled=true;
			AlbumsListBox.ItemHeight=15;
			AlbumsListBox.Location=new Point(12, 27);
			AlbumsListBox.Name="AlbumsListBox";
			AlbumsListBox.Size=new Size(337, 169);
			AlbumsListBox.TabIndex=0;
			AlbumsListBox.SelectedIndexChanged+=this.AlbumsListBox_SelectedIndexChanged;
			// 
			// StartStopButton
			// 
			StartStopButton.Location=new Point(582, 81);
			StartStopButton.Name="StartStopButton";
			StartStopButton.Size=new Size(75, 23);
			StartStopButton.TabIndex=1;
			StartStopButton.Text=">";
			StartStopButton.UseVisualStyleBackColor=true;
			StartStopButton.Click+=this.StartStopButton_Click;
			// 
			// VolumeTrackBar
			// 
			VolumeTrackBar.Location=new Point(464, 390);
			VolumeTrackBar.Maximum=100;
			VolumeTrackBar.Name="VolumeTrackBar";
			VolumeTrackBar.Size=new Size(242, 45);
			VolumeTrackBar.TabIndex=2;
			VolumeTrackBar.TickFrequency=10;
			VolumeTrackBar.Value=100;
			VolumeTrackBar.Scroll+=this.VolumeTrackBar_Scroll;
			// 
			// label1
			// 
			label1.AutoSize=true;
			label1.Location=new Point(12, 9);
			label1.Name="label1";
			label1.Size=new Size(48, 15);
			label1.TabIndex=3;
			label1.Text="Albums";
			// 
			// SongsListBox
			// 
			SongsListBox.FormattingEnabled=true;
			SongsListBox.ItemHeight=15;
			SongsListBox.Location=new Point(12, 236);
			SongsListBox.Name="SongsListBox";
			SongsListBox.Size=new Size(337, 199);
			SongsListBox.TabIndex=4;
			SongsListBox.SelectedIndexChanged+=this.SongsListBox_SelectedIndexChanged;
			// 
			// label2
			// 
			label2.AutoSize=true;
			label2.Location=new Point(12, 214);
			label2.Name="label2";
			label2.Size=new Size(39, 15);
			label2.TabIndex=5;
			label2.Text="Songs";
			// 
			// PositionTrackBar
			// 
			PositionTrackBar.Location=new Point(464, 249);
			PositionTrackBar.Maximum=100;
			PositionTrackBar.Name="PositionTrackBar";
			PositionTrackBar.Size=new Size(236, 45);
			PositionTrackBar.TabIndex=6;
			PositionTrackBar.TickFrequency=10;
			PositionTrackBar.TickStyle=TickStyle.None;
			PositionTrackBar.Scroll+=this.PositionTrackBar_Scroll;
			// 
			// TimeElapsedLabel
			// 
			TimeElapsedLabel.AutoSize=true;
			TimeElapsedLabel.Location=new Point(490, 209);
			TimeElapsedLabel.Name="TimeElapsedLabel";
			TimeElapsedLabel.Size=new Size(60, 15);
			TimeElapsedLabel.TabIndex=7;
			TimeElapsedLabel.Text="0:00 / 0:00";
			// 
			// NextSongButton
			// 
			NextSongButton.Location=new Point(679, 81);
			NextSongButton.Name="NextSongButton";
			NextSongButton.Size=new Size(75, 23);
			NextSongButton.TabIndex=8;
			NextSongButton.Text=">>|";
			NextSongButton.UseVisualStyleBackColor=true;
			NextSongButton.Click+=this.NextSongButton_Click;
			// 
			// Form1
			// 
			this.AutoScaleDimensions=new SizeF(7F, 15F);
			this.AutoScaleMode=AutoScaleMode.Font;
			this.ClientSize=new Size(800, 450);
			this.Controls.Add(NextSongButton);
			this.Controls.Add(TimeElapsedLabel);
			this.Controls.Add(PositionTrackBar);
			this.Controls.Add(label2);
			this.Controls.Add(SongsListBox);
			this.Controls.Add(label1);
			this.Controls.Add(VolumeTrackBar);
			this.Controls.Add(StartStopButton);
			this.Controls.Add(AlbumsListBox);
			this.Icon=(Icon)resources.GetObject("$this.Icon");
			this.Name="Form1";
			this.Text="Arenation";
			((System.ComponentModel.ISupportInitialize)VolumeTrackBar).EndInit();
			((System.ComponentModel.ISupportInitialize)PositionTrackBar).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		#endregion

		private ListBox AlbumsListBox;
		private Button StartStopButton;
		private TrackBar VolumeTrackBar;
		private Label label1;
		private ListBox SongsListBox;
		private Label label2;
		private TrackBar PositionTrackBar;
		private Label TimeElapsedLabel;
		private Button NextSongButton;
	}
}