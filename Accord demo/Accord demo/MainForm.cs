using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Accord.Imaging.Filters;
using Accord.Vision.Detection;
using Accord.Vision.Detection.Cascades;
using Accord.Vision.Tracking;
using Accord.Imaging;
using Accord.Math.Geometry;
using Accord.Video;
using Accord.Video.DirectShow;
using Accord.Video.VFW;
using System.Threading;

namespace Accord_demo
{
    public partial class MainForm : Form
    {
        private IVideoSource videoSource = null;

        private HaarObjectDetector detector;

        private Camshift tracker = null;

        private RectanglesMarker marker;

        private bool detecting = false;
        private bool tracking = false;

        private FilterInfoCollection videoDevices;

        public MainForm()
        {
            InitializeComponent();
            HaarCascade cascade = new FaceHaarCascade();
            detector = new HaarObjectDetector(cascade,
                25, ObjectDetectorSearchMode.Single, 1.2f,
                ObjectDetectorScalingMode.GreaterToSmaller);

            try
            {
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

                if (videoDevices.Count == 0)
                    throw new ApplicationException();

                foreach (FilterInfo device in videoDevices)
                {
                    devicesCombo.Items.Add(device.Name);
                }
            }
            catch (ApplicationException)
            {
                devicesCombo.Items.Add("Không tìm thấy thiết bị ghi hình");
                devicesCombo.Enabled = false;
                buttonPlay.Enabled = false;
            }

            devicesCombo.SelectedIndex = 0;
        }

        private void OpenVideoSource(IVideoSource source)
        {
            this.Cursor = Cursors.WaitCursor;

            CloseVideoSource();

            videoSourcePlayer1.VideoSource = source;
            videoSourcePlayer1.Start();

            videoSource = source;

            this.Cursor = Cursors.Default;
        }

        private void CloseVideoSource()
        {
            this.Cursor = Cursors.WaitCursor;

            videoSourcePlayer1.SignalToStop();

            for (int i = 0; (i < 50) && (videoSourcePlayer1.IsRunning); i++)
            {
                Thread.Sleep(100);
            }
            if (videoSourcePlayer1.IsRunning)
                videoSourcePlayer1.Stop();

            tracker = new Camshift();

            tracker.Conservative = true;
            tracker.AspectRatio = 1.5f;

            videoSourcePlayer1.BorderColor = Color.Black;
            this.Cursor = Cursors.Default;
        }

        private static VideoCapabilities selectResolution(VideoCaptureDevice device)
        {
            foreach (var cap in device.VideoCapabilities)
            {
                if (cap.FrameSize.Height == 240)
                    return cap;
                if (cap.FrameSize.Width == 320)
                    return cap;
            }

            return device.VideoCapabilities.Last();
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            VideoCaptureDevice videoSource = new VideoCaptureDevice(videoDevices[devicesCombo.SelectedIndex].MonikerString);

            videoSource.VideoResolution = selectResolution(videoSource);

            OpenVideoSource(videoSource);
        }

        private void buttonOption_Click(object sender, EventArgs e)
        {
            if ((videoSource != null) && (videoSource is VideoCaptureDevice))
            {
                try
                {
                    ((VideoCaptureDevice)videoSource).DisplayPropertyPage(this.Handle);
                }
                catch (NotSupportedException)
                {
                    MessageBox.Show("Không có video ", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseVideoSource();
        }

        private void videoSourcePlayer1_NewFrame(object sender, ref Bitmap image)
        {
            if (!detecting && !tracking)
                return;

            lock (this)
            {
                if (detecting)
                {
                    detecting = false;
                    tracking = false;

                    UnmanagedImage im = UnmanagedImage.FromManagedImage(image);

                    float xscale = image.Width / 160f;
                    float yscale = image.Height / 120f;

                    ResizeNearestNeighbor resize = new ResizeNearestNeighbor(160, 120);
                    UnmanagedImage downsample = resize.Apply(im);

                    Rectangle[] regions = detector.ProcessFrame(downsample);

                    if (regions.Length > 0)
                    {
                        tracker.Reset();

                        Rectangle face = regions[0];

                        Rectangle window = new Rectangle(
                            (int)((regions[0].X + regions[0].Width / 2f) * xscale),
                            (int)((regions[0].Y + regions[0].Height / 2f) * yscale),
                            1, 1);

                        window.Inflate(
                            (int)(0.2f * regions[0].Width * xscale),
                            (int)(0.4f * regions[0].Height * yscale));

                        tracker.SearchWindow = window;
                        tracker.ProcessFrame(im);

                        marker = new RectanglesMarker(window);
                        marker.ApplyInPlace(im);

                        image = im.ToManagedImage();

                        tracking = true;
                        detecting = true;
                    }
                    else
                    {
                        detecting = true;
                    }
                }
                else
                {
                    if (marker != null)
                        image = marker.Apply(image);
                }
            }
        }

        private void buttonDetect_Click(object sender, EventArgs e)
        {
            detecting = true;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}