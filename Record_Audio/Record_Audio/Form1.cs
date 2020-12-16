using Accord.Audio;
using Accord.Audio.Formats;
using Accord.DirectSound;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Record_Audio
{
    public partial class Form1 : Form
    {
        private MemoryStream stream;

        private IAudioSource source;
        private IAudioOutput output;

        private WaveEncoder encoder;
        private WaveDecoder decoder;

        private float[] current;

        private int frames;
        private int sample;

        private TimeSpan duration;

        public Form1()
        {
            InitializeComponent();
            wavechart1.SimpleMode = true;
            wavechart1.AddWaveform("wave", Color.Green, 1, false);
            updateButtons();
        }

        private void updateButtons()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(updateButtons));
                return;
            }

            if (source != null && source.IsRunning)
            {
                buttonPlay.Enabled = false;
                buttonStop.Enabled = true;
                buttonRecord.Enabled = false;
                trackBar1.Enabled = false;
            }
            else if (output != null && output.IsRunning)
            {
                buttonPlay.Enabled = false;
                buttonStop.Enabled = true;
                buttonRecord.Enabled = false;
                trackBar1.Enabled = true;
            }
            else
            {
                buttonPlay.Enabled = stream != null;
                buttonStop.Enabled = false;
                buttonRecord.Enabled = true;
                trackBar1.Enabled = decoder != null;
                trackBar1.Value = 0;
            }
        }

        private void updateWaveForm(float[] sample, int length)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => wavechart1.UpdateWaveform("wave", sample, length)));
            }
            else
            {
                wavechart1.UpdateWaveform("wave", current, length);
            }
        }

        private void updateTrackBar(int value)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => trackBar1.Value = Math.Max(trackBar1.Minimum, Math.Min(trackBar1.Maximum, value))));
            }
            else
            {
                trackBar1.Value = Math.Max(trackBar1.Minimum, Math.Min(trackBar1.Maximum, value));
            }
        }

        private void source_AudioSourceError(object sender, AudioSourceErrorEventArgs e)
        {
            throw new Exception(e.Description);
        }

        private void source_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            eventArgs.Signal.CopyTo(current);

            updateWaveForm(current, eventArgs.Signal.Length);
            encoder.Encode(eventArgs.Signal);
            duration += eventArgs.Signal.Duration;
            sample += eventArgs.Signal.Samples;
            frames += eventArgs.Signal.Length;
        }

        private void output__FramePlayingStarted(object sender, PlayFrameEventArgs e)
        {
            updateTrackBar(e.FrameIndex);
            if (e.FrameIndex + e.Count < decoder.Frames)
            {
                int previous = decoder.Position;
                decoder.Seek(e.FrameIndex);

                Signal s = decoder.Decode(e.Count);
                decoder.Seek(previous);
                updateWaveForm(s.ToFloat(), s.Length);
            }
        }

        private void output_PlayingFinished(object sender, EventArgs e)
        {
            updateButtons();
            Array.Clear(current, 0, current.Length);
            updateWaveForm(current, current.Length);
        }

        private void output_NewFrameRequested(object sender, NewFrameRequestedEventArgs e)
        {
            e.FrameIndex = decoder.Position;

            Signal signal = decoder.Decode(e.Frames);

            if (signal == null)
            {
                e.Stop = true;
                return;
            }
            e.Frames = signal.Length;
            signal.CopyTo(e.Buffer);
        }

        private void buttonRecord_Click(object sender, EventArgs e)
        {
            source = new AudioCaptureDevice()
            {
                DesiredFrameSize = 4096,
                SampleRate = 22050,
                Format = SampleFormat.Format16Bit
            };

            source.NewFrame += source_NewFrame;
            source.AudioSourceError += source_AudioSourceError;
            current = new float[source.DesiredFrameSize];

            stream = new MemoryStream();
            encoder = new WaveEncoder(stream);
            source.Start();
            updateButtons();
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            stream.Seek(0, SeekOrigin.Begin);

            decoder = new WaveDecoder(stream);

            if (trackBar1.Value < decoder.Frames)
                decoder.Seek(trackBar1.Value);
            trackBar1.Maximum = decoder.Samples;

            output = new AudioOutputDevice(this.Handle, decoder.SampleRate, decoder.Channels);

            output.FramePlayingStarted += output__FramePlayingStarted;
            output.NewFrameRequested += output_NewFrameRequested;
            output.Stopped += output_PlayingFinished;

            output.Play();

            updateButtons();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            if (source != null)
            {
                source.SignalToStop();
                source.WaitForStop();
            }
            if (output != null)
            {
                output.SignalToStop();
                source.WaitForStop();
            }

            updateButtons();
            Array.Clear(current, 0, current.Length);

            updateWaveForm(current, current.Length);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (source != null) source.SignalToStop();
            if (output != null) source.SignalToStop();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            Stream fileStream = saveFileDialog1.OpenFile();
            stream.WriteTo(fileStream);
            fileStream.Close();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog(this);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbLength.Text = String.Format("{0:00,00} sec.", duration.Seconds);
        }
    }
}