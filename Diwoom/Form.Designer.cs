namespace Diwoom
{
    partial class DiwoomForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DiwoomForm));
            this.deviceSelector = new System.Windows.Forms.ComboBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.portSelector = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.picturePicker = new System.Windows.Forms.PictureBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button5 = new System.Windows.Forms.Button();
            this.buttonAnim = new System.Windows.Forms.Button();
            this.textBoxB = new System.Windows.Forms.NumericUpDown();
            this.textBoxR = new System.Windows.Forms.NumericUpDown();
            this.buttonScore = new System.Windows.Forms.Button();
            this.buttonVis = new System.Windows.Forms.Button();
            this.btnLightning = new System.Windows.Forms.Button();
            this.buttonVJ = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portSelector)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picturePicker)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // deviceSelector
            // 
            this.deviceSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.deviceSelector.FormattingEnabled = true;
            this.deviceSelector.Location = new System.Drawing.Point(36, 12);
            this.deviceSelector.Margin = new System.Windows.Forms.Padding(4);
            this.deviceSelector.Name = "deviceSelector";
            this.deviceSelector.Size = new System.Drawing.Size(199, 25);
            this.deviceSelector.TabIndex = 0;
            // 
            // btnConnect
            // 
            this.btnConnect.Enabled = false;
            this.btnConnect.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnConnect.Location = new System.Drawing.Point(241, 12);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(4);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(51, 25);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.Text = "⇅";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(12, 12);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(20, 25);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "↺";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Enabled = false;
            this.progressBar1.Location = new System.Drawing.Point(12, 45);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(280, 11);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.portSelector);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(12, 70);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(120, 93);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "HTTP Server";
            // 
            // portSelector
            // 
            this.portSelector.Location = new System.Drawing.Point(50, 20);
            this.portSelector.Margin = new System.Windows.Forms.Padding(4);
            this.portSelector.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.portSelector.Minimum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.portSelector.Name = "portSelector";
            this.portSelector.Size = new System.Drawing.Size(64, 23);
            this.portSelector.TabIndex = 7;
            this.portSelector.Value = new decimal(new int[] {
            10119,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Port";
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(7, 48);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // picturePicker
            // 
            this.picturePicker.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picturePicker.Location = new System.Drawing.Point(17, 20);
            this.picturePicker.Margin = new System.Windows.Forms.Padding(0);
            this.picturePicker.Name = "picturePicker";
            this.picturePicker.Size = new System.Drawing.Size(65, 65);
            this.picturePicker.TabIndex = 0;
            this.picturePicker.TabStop = false;
            this.picturePicker.Click += new System.EventHandler(this.picturePicker_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.Control;
            this.button2.Location = new System.Drawing.Point(6, 20);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(63, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "Color";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(6, 49);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(63, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "Time";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(144, 49);
            this.button4.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(63, 23);
            this.button4.TabIndex = 8;
            this.button4.Text = "Cloud";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button5);
            this.groupBox2.Controls.Add(this.buttonAnim);
            this.groupBox2.Controls.Add(this.textBoxB);
            this.groupBox2.Controls.Add(this.textBoxR);
            this.groupBox2.Controls.Add(this.buttonScore);
            this.groupBox2.Controls.Add(this.buttonVis);
            this.groupBox2.Controls.Add(this.btnLightning);
            this.groupBox2.Controls.Add(this.buttonVJ);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Enabled = false;
            this.groupBox2.Location = new System.Drawing.Point(12, 163);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(280, 106);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Commands";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(75, 20);
            this.button5.Margin = new System.Windows.Forms.Padding(4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(63, 23);
            this.button5.TabIndex = 17;
            this.button5.Text = "Plain";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // buttonAnim
            // 
            this.buttonAnim.Location = new System.Drawing.Point(145, 20);
            this.buttonAnim.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAnim.Name = "buttonAnim";
            this.buttonAnim.Size = new System.Drawing.Size(63, 23);
            this.buttonAnim.TabIndex = 16;
            this.buttonAnim.Text = "Anim";
            this.buttonAnim.UseVisualStyleBackColor = true;
            this.buttonAnim.Click += new System.EventHandler(this.buttonAnim_Click);
            // 
            // textBoxB
            // 
            this.textBoxB.Location = new System.Drawing.Point(84, 79);
            this.textBoxB.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxB.Name = "textBoxB";
            this.textBoxB.Size = new System.Drawing.Size(45, 23);
            this.textBoxB.TabIndex = 14;
            // 
            // textBoxR
            // 
            this.textBoxR.Location = new System.Drawing.Point(16, 79);
            this.textBoxR.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxR.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.textBoxR.Name = "textBoxR";
            this.textBoxR.Size = new System.Drawing.Size(45, 23);
            this.textBoxR.TabIndex = 13;
            // 
            // buttonScore
            // 
            this.buttonScore.Location = new System.Drawing.Point(145, 78);
            this.buttonScore.Margin = new System.Windows.Forms.Padding(4);
            this.buttonScore.Name = "buttonScore";
            this.buttonScore.Size = new System.Drawing.Size(131, 23);
            this.buttonScore.TabIndex = 12;
            this.buttonScore.Text = "Scoreboard";
            this.buttonScore.UseVisualStyleBackColor = true;
            this.buttonScore.Click += new System.EventHandler(this.buttonScore_Click);
            // 
            // buttonVis
            // 
            this.buttonVis.Location = new System.Drawing.Point(213, 20);
            this.buttonVis.Margin = new System.Windows.Forms.Padding(4);
            this.buttonVis.Name = "buttonVis";
            this.buttonVis.Size = new System.Drawing.Size(63, 23);
            this.buttonVis.TabIndex = 11;
            this.buttonVis.Text = "Visual";
            this.buttonVis.UseVisualStyleBackColor = true;
            this.buttonVis.Click += new System.EventHandler(this.buttonVis_Click);
            // 
            // btnLightning
            // 
            this.btnLightning.Location = new System.Drawing.Point(75, 49);
            this.btnLightning.Margin = new System.Windows.Forms.Padding(4);
            this.btnLightning.Name = "btnLightning";
            this.btnLightning.Size = new System.Drawing.Size(63, 23);
            this.btnLightning.TabIndex = 10;
            this.btnLightning.Text = "Light";
            this.btnLightning.UseVisualStyleBackColor = true;
            this.btnLightning.Click += new System.EventHandler(this.btnLightning_Click);
            // 
            // buttonVJ
            // 
            this.buttonVJ.Location = new System.Drawing.Point(213, 49);
            this.buttonVJ.Margin = new System.Windows.Forms.Padding(4);
            this.buttonVJ.Name = "buttonVJ";
            this.buttonVJ.Size = new System.Drawing.Size(63, 23);
            this.buttonVJ.TabIndex = 9;
            this.buttonVJ.Text = "VJ";
            this.buttonVJ.UseVisualStyleBackColor = true;
            this.buttonVJ.Click += new System.EventHandler(this.buttonVJ_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(67, 84);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 17);
            this.label2.TabIndex = 15;
            this.label2.Text = "-";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(93, 16);
            this.trackBar1.Margin = new System.Windows.Forms.Padding(4);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar1.Size = new System.Drawing.Size(45, 71);
            this.trackBar1.TabIndex = 10;
            this.trackBar1.Value = 100;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.trackBar1);
            this.groupBox3.Controls.Add(this.picturePicker);
            this.groupBox3.Enabled = false;
            this.groupBox3.Location = new System.Drawing.Point(138, 70);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(154, 93);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Display, Brightness";
            // 
            // DiwoomForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(304, 281);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.deviceSelector);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(320, 320);
            this.Name = "DiwoomForm";
            this.Text = "Diwoom";
            this.Load += new System.EventHandler(this.Diwoom_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portSelector)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picturePicker)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox deviceSelector;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown portSelector;
        private System.Windows.Forms.PictureBox picturePicker;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnLightning;
        private System.Windows.Forms.Button buttonVJ;
        private System.Windows.Forms.Button buttonVis;
        private System.Windows.Forms.NumericUpDown textBoxB;
        private System.Windows.Forms.NumericUpDown textBoxR;
        private System.Windows.Forms.Button buttonScore;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonAnim;
        private System.Windows.Forms.Button button5;
    }
}

