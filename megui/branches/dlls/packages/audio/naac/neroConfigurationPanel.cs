using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using MeGUI.core.plugins.interfaces;

namespace MeGUI.packages.audio.naac
{
    public partial class neroConfigurationPanel : MeGUI.core.details.audio.AudioConfigurationPanel, Gettable<AudioCodecSettings>
    {
        public CheckBox cbxCreateHintTrack;
        public TrackBar vQuality;
        public RadioButton rbtnVBR;
        public TrackBar vBitrate;
        public RadioButton rbtnCBR;
        private ComboBox comboBox1;
        private Label label1;
        public RadioButton rbtnABR;
		#region variables

        #endregion
        #region start / stop
        public neroConfigurationPanel(MainForm mainForm, string[] audioInfo)
            : base(mainForm, audioInfo)

		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
            comboBox1.Items.AddRange(EnumProxy.CreateArray(typeof(AacProfile)));
            rbtnABR_CheckedChanged(null, null);
            vBitrate_ValueChanged(null, null);
		}


		#endregion
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.vQuality = new System.Windows.Forms.TrackBar();
            this.rbtnVBR = new System.Windows.Forms.RadioButton();
            this.vBitrate = new System.Windows.Forms.TrackBar();
            this.rbtnCBR = new System.Windows.Forms.RadioButton();
            this.rbtnABR = new System.Windows.Forms.RadioButton();
            this.cbxCreateHintTrack = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.encoderGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vQuality)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vBitrate)).BeginInit();
            this.SuspendLayout();
            // 
            // encoderGroupBox
            // 
            this.encoderGroupBox.Controls.Add(this.label1);
            this.encoderGroupBox.Controls.Add(this.comboBox1);
            this.encoderGroupBox.Controls.Add(this.cbxCreateHintTrack);
            this.encoderGroupBox.Controls.Add(this.vQuality);
            this.encoderGroupBox.Controls.Add(this.rbtnVBR);
            this.encoderGroupBox.Controls.Add(this.vBitrate);
            this.encoderGroupBox.Controls.Add(this.rbtnCBR);
            this.encoderGroupBox.Controls.Add(this.rbtnABR);
            this.encoderGroupBox.Location = new System.Drawing.Point(3, 158);
            this.encoderGroupBox.Size = new System.Drawing.Size(375, 250);
            this.encoderGroupBox.Text = "NeroDigital AAC Options";
            // 
            // besweetOptionsGroupbox
            // 
            this.besweetOptionsGroupbox.Size = new System.Drawing.Size(378, 149);
            // 
            // vQuality
            // 
            this.vQuality.Dock = System.Windows.Forms.DockStyle.Top;
            this.vQuality.Location = new System.Drawing.Point(3, 133);
            this.vQuality.Maximum = 100;
            this.vQuality.Name = "vQuality";
            this.vQuality.Size = new System.Drawing.Size(369, 45);
            this.vQuality.TabIndex = 22;
            this.vQuality.TickFrequency = 5;
            this.vQuality.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.vQuality.ValueChanged += new System.EventHandler(this.vBitrate_ValueChanged);
            // 
            // rbtnVBR
            // 
            this.rbtnVBR.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbtnVBR.Location = new System.Drawing.Point(3, 109);
            this.rbtnVBR.Name = "rbtnVBR";
            this.rbtnVBR.Size = new System.Drawing.Size(369, 24);
            this.rbtnVBR.TabIndex = 19;
            this.rbtnVBR.Text = "Variable bit rate";
            this.rbtnVBR.CheckedChanged += new System.EventHandler(this.rbtnABR_CheckedChanged);
            // 
            // vBitrate
            // 
            this.vBitrate.Dock = System.Windows.Forms.DockStyle.Top;
            this.vBitrate.Location = new System.Drawing.Point(3, 64);
            this.vBitrate.Maximum = 320;
            this.vBitrate.Minimum = 16;
            this.vBitrate.Name = "vBitrate";
            this.vBitrate.Size = new System.Drawing.Size(369, 45);
            this.vBitrate.TabIndex = 20;
            this.vBitrate.TickFrequency = 8;
            this.vBitrate.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.vBitrate.Value = 16;
            this.vBitrate.ValueChanged += new System.EventHandler(this.vBitrate_ValueChanged);
            // 
            // rbtnCBR
            // 
            this.rbtnCBR.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbtnCBR.Location = new System.Drawing.Point(3, 40);
            this.rbtnCBR.Name = "rbtnCBR";
            this.rbtnCBR.Size = new System.Drawing.Size(369, 24);
            this.rbtnCBR.TabIndex = 18;
            this.rbtnCBR.Text = "Constant bit rate";
            this.rbtnCBR.CheckedChanged += new System.EventHandler(this.rbtnABR_CheckedChanged);
            // 
            // rbtnABR
            // 
            this.rbtnABR.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbtnABR.Location = new System.Drawing.Point(3, 16);
            this.rbtnABR.Name = "rbtnABR";
            this.rbtnABR.Size = new System.Drawing.Size(369, 24);
            this.rbtnABR.TabIndex = 21;
            this.rbtnABR.Text = "Adaptive bit rate";
            this.rbtnABR.CheckedChanged += new System.EventHandler(this.rbtnABR_CheckedChanged);
            // 
            // cbxCreateHintTrack
            // 
            this.cbxCreateHintTrack.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cbxCreateHintTrack.Location = new System.Drawing.Point(3, 215);
            this.cbxCreateHintTrack.Name = "cbxCreateHintTrack";
            this.cbxCreateHintTrack.Size = new System.Drawing.Size(369, 32);
            this.cbxCreateHintTrack.TabIndex = 23;
            this.cbxCreateHintTrack.Text = "Create hint track (for streaming server)";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(106, 179);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(240, 21);
            this.comboBox1.TabIndex = 24;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 182);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "AAC Profile";
            // 
            // neroConfigurationPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "neroConfigurationPanel";
            this.Size = new System.Drawing.Size(378, 408);
            this.encoderGroupBox.ResumeLayout(false);
            this.encoderGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vQuality)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vBitrate)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion
		#region properties
		/// <summary>
		/// gets / sets the settings that are being shown in this configuration dialog. These will be added to by the base class
		/// </summary>
		protected override AudioCodecSettings CodecSettings
		{
			get
			{
				NeroAACSettings nas = new NeroAACSettings();
                if (rbtnABR.Checked) nas.BitrateMode = BitrateManagementMode.ABR;
                if (rbtnCBR.Checked) nas.BitrateMode = BitrateManagementMode.CBR;
                if (rbtnVBR.Checked) nas.BitrateMode = BitrateManagementMode.VBR;
                nas.Bitrate = vBitrate.Value;
                nas.Quality= (Decimal)vQuality.Value/vQuality.Maximum;
                nas.CreateHintTrack = cbxCreateHintTrack.Checked;
                nas.Profile = (AacProfile)(comboBox1.SelectedItem as EnumProxy).RealValue;
				return nas;
			}
			set
			{
                NeroAACSettings nas = value as NeroAACSettings;
                rbtnABR.Checked = nas.BitrateMode == BitrateManagementMode.ABR;
                rbtnCBR.Checked = nas.BitrateMode == BitrateManagementMode.CBR;
                rbtnVBR.Checked = nas.BitrateMode == BitrateManagementMode.VBR;
                vBitrate.Value = Math.Max(Math.Min(nas.Bitrate, vBitrate.Maximum), vBitrate.Minimum);
                vQuality.Value = (int)(nas.Quality * (Decimal)vQuality.Maximum);
                cbxCreateHintTrack.Checked = nas.CreateHintTrack;
                comboBox1.SelectedItem = EnumProxy.Create(nas.Profile);
			}
		}
		#endregion

        private void rbtnABR_CheckedChanged(object sender, EventArgs e)
        {
            vBitrate.Enabled = !(vQuality.Enabled = rbtnVBR.Checked);
        }

        private void vBitrate_ValueChanged(object sender, EventArgs e)
        {
            rbtnABR.Text = String.Format("Adaptive Bitrate @ {0} kbit/s", vBitrate.Value);
            rbtnCBR.Text = String.Format("Constant Bitrate @ {0} kbit/s", vBitrate.Value);
            Decimal q = ((Decimal)vQuality.Value) / vQuality.Maximum;
            rbtnVBR.Text = String.Format("Variable Bitrate (Q={0}) ", q);
        }
    }
}

