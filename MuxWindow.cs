using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MeGUI
{
    public partial class MuxWindow : baseMuxWindow
    {
        private IMuxing muxer;
        private CommandLineGenerator gen = new CommandLineGenerator();

        public MuxWindow(IMuxing muxer)
        {
            InitializeComponent();
            this.muxer = muxer;
            if (muxer.GetSupportedAudioTypes().Count == 0)
                audioGroupbox.Enabled = false;
            if (muxer.GetSupportedChapterTypes().Count == 0)
                chaptersGroupbox.Enabled = false;
            if (muxer.GetSupportedSubtitleTypes().Count == 0)
                subtitleGroupbox.Enabled = false;
            if (muxer.GetSupportedContainerInputTypes().Count == 0)
                muxedInputOpenButton.Enabled = false;
        }
        protected virtual MuxJob generateMuxJob()
        {
            MuxJob job = new MuxJob();
            convertLanguagesToISO();
            foreach (SubStream stream in audioStreams)
            {
                job.Settings.AudioStreams.Add(stream);
            }
            foreach (SubStream stream in subtitleStreams)
            {
                job.Settings.SubtitleStreams.Add(stream);
            }
            job.Settings.ChapterFile = this.chaptersInput.Text;
            job.Settings.VideoName = this.videoName.Text;
            job.Settings.VideoInput = this.videoInput.Text;
            job.Settings.MuxedOutput = muxedOutput.Text;
            job.Settings.MuxedInput = this.muxedInput.Text;
            job.Settings.PARX = base.parX;
            job.Settings.PARY = base.parY;

            if (string.IsNullOrEmpty(job.Settings.VideoInput))
                job.Input = job.Settings.MuxedInput;
            else
                job.Input = job.Settings.VideoInput;

            job.Output = job.Settings.MuxedOutput;
            job.MuxType = muxer.MuxerType;
            if ((!job.Settings.MuxedInput.Equals("") || !job.Settings.VideoInput.Equals(""))
                && !job.Settings.MuxedOutput.Equals(""))
            {
                if (this.muxFPS.SelectedIndex != -1)
                    job.Settings.Framerate = Double.Parse(muxFPS.Text);
                if (this.muxFPS.SelectedIndex != -1 || !isFPSRequired())
                {
                    if (this.enableSplit.Checked && !splitSize.Text.Equals(""))
                        job.Settings.SplitSize = Int32.Parse(this.splitSize.Text) * 1024;
                    job.Commandline = CommandLineGenerator.generateMuxCommandline(job.Settings, job.MuxType);
                }
            }
            return job;
        }

        public MuxJob Job
        {
            get { return generateMuxJob(); }
        }

        public void setConfig(string videoInput, string muxedInput, double framerate, SubStream[] audioStreams,
            SubStream[] subtitleStreams, string chapterFile, string output, int splitSize, int parX, int parY)
        {
            base.setConfig(videoInput, framerate, audioStreams, subtitleStreams, chapterFile, output, splitSize, parX, parY);
            this.muxedInput.Text = muxedInput;
        }

        #region overriden filters
        public override string AudioFilter
        {
            get
            {
                return muxer.GetAudioInputFilter();
            }
        }
        public override string ChaptersFilter
        {
            get
            {
                return "Chapter files (*.txt)|*.txt";
            }
        }
        public override string OutputFilter
        {
            get
            {
                return muxer.GetOutputTypeFilter();
            }
        }
        public override string SubtitleFilter
        {
            get
            {
                return muxer.GetSubtitleInputFilter();
            }
        }
        public override string VideoInputFilter
        {
            get
            {
                return muxer.GetVideoInputFilter();
            }
        }
        public string MuxedInputFilter
        {
            get
            {
                return muxer.GetMuxedInputFilter();
            }
        }
        #endregion

        private void muxedInputOpenButton_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = MuxedInputFilter;
            openFileDialog.Title = "Select your already-muxed file";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                muxedInput.Text = openFileDialog.FileName;
                checkIO();
                fileUpdated();
            }
        }

        protected override void checkIO()
        {
            if (videoInput.Text.Equals("") && muxedInput.Text.Equals(""))
            {
                muxButton.DialogResult = DialogResult.None;
                return;
            }
            else if (muxedOutput.Text.Equals(""))
            {
                muxButton.DialogResult = DialogResult.None;
                return;
            }
            else if (muxFPS.SelectedIndex == -1 && isFPSRequired())
            {
                muxButton.DialogResult = DialogResult.None;
                return;
            }
            else
                muxButton.DialogResult = DialogResult.OK;
        }

        protected override bool isFPSRequired()
        {
            if (videoInput.Text.Length > 0)
                return base.isFPSRequired();
            else if (muxedInput.Text.Length > 0)
                return false;
            else
                return true;
        }
    }
}