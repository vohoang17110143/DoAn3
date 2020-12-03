namespace Accord_demo
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.videoSourcePlayer1 = new Accord.Controls.VideoSourcePlayer();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonOption = new System.Windows.Forms.Button();
            this.buttonDetect = new System.Windows.Forms.Button();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.devicesCombo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonCapture = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // videoSourcePlayer1
            // 
            this.videoSourcePlayer1.Location = new System.Drawing.Point(93, 50);
            this.videoSourcePlayer1.Name = "videoSourcePlayer1";
            this.videoSourcePlayer1.Size = new System.Drawing.Size(942, 423);
            this.videoSourcePlayer1.TabIndex = 0;
            this.videoSourcePlayer1.Text = "videoSourcePlayer1";
            this.videoSourcePlayer1.VideoSource = null;
            this.videoSourcePlayer1.NewFrame += new Accord.Controls.VideoSourcePlayer.NewFrameHandler(this.videoSourcePlayer1_NewFrame);
            // 
            // buttonClose
            // 
            this.buttonClose.BackgroundImage = global::Accord_demo.Properties.Resources.shutdown;
            this.buttonClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonClose.Location = new System.Drawing.Point(1121, 517);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(99, 104);
            this.buttonClose.TabIndex = 4;
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonOption
            // 
            this.buttonOption.BackgroundImage = global::Accord_demo.Properties.Resources.img_239291;
            this.buttonOption.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonOption.Location = new System.Drawing.Point(966, 517);
            this.buttonOption.Name = "buttonOption";
            this.buttonOption.Size = new System.Drawing.Size(99, 104);
            this.buttonOption.TabIndex = 3;
            this.buttonOption.UseVisualStyleBackColor = true;
            this.buttonOption.Click += new System.EventHandler(this.buttonOption_Click);
            // 
            // buttonDetect
            // 
            this.buttonDetect.BackgroundImage = global::Accord_demo.Properties.Resources.detect;
            this.buttonDetect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonDetect.Location = new System.Drawing.Point(213, 517);
            this.buttonDetect.Name = "buttonDetect";
            this.buttonDetect.Size = new System.Drawing.Size(99, 104);
            this.buttonDetect.TabIndex = 2;
            this.buttonDetect.UseVisualStyleBackColor = true;
            this.buttonDetect.Click += new System.EventHandler(this.buttonDetect_Click);
            // 
            // buttonPlay
            // 
            this.buttonPlay.BackgroundImage = global::Accord_demo.Properties.Resources.play;
            this.buttonPlay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonPlay.Location = new System.Drawing.Point(64, 517);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(99, 104);
            this.buttonPlay.TabIndex = 1;
            this.buttonPlay.UseVisualStyleBackColor = true;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // devicesCombo
            // 
            this.devicesCombo.FormattingEnabled = true;
            this.devicesCombo.Location = new System.Drawing.Point(604, 556);
            this.devicesCombo.Name = "devicesCombo";
            this.devicesCombo.Size = new System.Drawing.Size(247, 28);
            this.devicesCombo.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(600, 517);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Select Device: ";
            // 
            // buttonCapture
            // 
            this.buttonCapture.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonCapture.BackgroundImage")));
            this.buttonCapture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonCapture.Location = new System.Drawing.Point(406, 517);
            this.buttonCapture.Name = "buttonCapture";
            this.buttonCapture.Size = new System.Drawing.Size(99, 104);
            this.buttonCapture.TabIndex = 7;
            this.buttonCapture.UseVisualStyleBackColor = true;
            this.buttonCapture.Click += new System.EventHandler(this.buttonCapture_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1319, 633);
            this.Controls.Add(this.buttonCapture);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.devicesCombo);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonOption);
            this.Controls.Add(this.buttonDetect);
            this.Controls.Add(this.buttonPlay);
            this.Controls.Add(this.videoSourcePlayer1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Accord.Controls.VideoSourcePlayer videoSourcePlayer1;
        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.Button buttonDetect;
        private System.Windows.Forms.Button buttonOption;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.ComboBox devicesCombo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonCapture;
    }
}