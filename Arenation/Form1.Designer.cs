namespace Arenation {
  partial class Form1 {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
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
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.AlbumListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.StopButton = new System.Windows.Forms.Button();
            this.PlayPauseButton = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.SongsGroupBox = new System.Windows.Forms.GroupBox();
            this.SongListBox = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SongsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.AlbumListBox);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(768, 391);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Album Queue";
            // 
            // AlbumListBox
            // 
            this.AlbumListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AlbumListBox.FormattingEnabled = true;
            this.AlbumListBox.ItemHeight = 16;
            this.AlbumListBox.Location = new System.Drawing.Point(4, 19);
            this.AlbumListBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.AlbumListBox.Name = "AlbumListBox";
            this.AlbumListBox.Size = new System.Drawing.Size(760, 368);
            this.AlbumListBox.TabIndex = 0;
            this.AlbumListBox.SelectedIndexChanged += new System.EventHandler(this.AlbumListBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(118, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // StopButton
            // 
            this.StopButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StopButton.Location = new System.Drawing.Point(16, 13);
            this.StopButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(43, 28);
            this.StopButton.TabIndex = 4;
            this.StopButton.Text = "■";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButtonClick);
            // 
            // PlayPauseButton
            // 
            this.PlayPauseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayPauseButton.Location = new System.Drawing.Point(67, 13);
            this.PlayPauseButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PlayPauseButton.Name = "PlayPauseButton";
            this.PlayPauseButton.Size = new System.Drawing.Size(43, 28);
            this.PlayPauseButton.TabIndex = 5;
            this.PlayPauseButton.Text = ">";
            this.PlayPauseButton.UseVisualStyleBackColor = true;
            this.PlayPauseButton.Click += new System.EventHandler(this.PlayPauseButtonClick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(16, 66);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.SongsGroupBox);
            this.splitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Size = new System.Drawing.Size(768, 676);
            this.splitContainer1.SplitterDistance = 280;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 6;
            // 
            // SongsGroupBox
            // 
            this.SongsGroupBox.Controls.Add(this.SongListBox);
            this.SongsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SongsGroupBox.Location = new System.Drawing.Point(0, 0);
            this.SongsGroupBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SongsGroupBox.Name = "SongsGroupBox";
            this.SongsGroupBox.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SongsGroupBox.Size = new System.Drawing.Size(768, 280);
            this.SongsGroupBox.TabIndex = 0;
            this.SongsGroupBox.TabStop = false;
            this.SongsGroupBox.Text = "Songs";
            // 
            // SongListBox
            // 
            this.SongListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SongListBox.FormattingEnabled = true;
            this.SongListBox.ItemHeight = 16;
            this.SongListBox.Location = new System.Drawing.Point(4, 19);
            this.SongListBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SongListBox.Name = "SongListBox";
            this.SongListBox.Size = new System.Drawing.Size(760, 257);
            this.SongListBox.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(800, 757);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.PlayPauseButton);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.SongsGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button StopButton;
    private System.Windows.Forms.Button PlayPauseButton;
    private System.Windows.Forms.ListBox AlbumListBox;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.GroupBox SongsGroupBox;
    private System.Windows.Forms.ListBox SongListBox;
  }
}

