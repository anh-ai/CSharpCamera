using Emgu.CV;
using System;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV.Util;
namespace C___Webcam
{

    public partial class Form1 : Form
    {
        private readonly TACamera mCam = new TACamera();
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            GetFrametoPictureBox(sender, e);
        }

        private void Form1_FormClosing_1(object sender, FormClosingEventArgs e)
        {
        }

        private void GetFrametoPictureBox(object sender, EventArgs e)
        {
             Bitmap bitmap = mCam.GetFrame();
            if (bitmap != null)
            {
                pictureBox2.Image = bitmap;
            }
        }
        private void btnStart_Click(object sender, EventArgs e)
        {

        }

        private void btnStart_Click_1(object sender, EventArgs e)
        {
            mCam.initCam(0);
            if (mCam.isCapturing)
            {
                Application.Idle += GetFrametoPictureBox;
            }
            else
            {
                Application.Idle -= GetFrametoPictureBox;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mCam.StopCam();
            if (mCam.isCapturing)
            {
                Application.Idle += GetFrametoPictureBox;
            }
            else
            {
                Application.Idle -= GetFrametoPictureBox;
            }
        }

        private void btnOpenCam_Click(object sender, EventArgs e)
        {
            mCam.initCam(0);
            timer1.Start();
        }
    }

    internal class TACamera
    {
        /* Emgu Install:
         - Emgu.CV.runtime.windows
         - If you are targeting .Net Framework, when using Emgu.CV.runtime.windows(.dldt / .cuda / .cuda.dldt) nuget packages for windows, 
            please set the build architecture to either "x86" or "x64". Do not set the architecture to "Any CPU" 
            Targeting .Net Framework: (https://www.emgu.com/wiki/index.php/Download_And_Installation#Using_the_Downloadable_packages)
         * */
        private VideoCapture capture;
        public bool isCapturing = false;
        Mat frame = new Mat();
        Bitmap image;
        public void initCam(int CamID = 0)
        {
            // Create a new VideoCapture object with the default camera
            capture = new VideoCapture(CamID);
            //capture.Set(Emgu.CV.CvEnum.CapProp.FrameWidth, 800);
            //capture.Set(Emgu.CV.CvEnum.CapProp.FrameHeight,600);
            //capture.Set(Emgu.CV.CvEnum.CapProp.Exposure,50000);

            // Start capturing frames
            capture.Start();
            isCapturing = true;
        }
        public Bitmap GetFrame()
        {
            _ = capture.Read(frame);
            if (!frame.IsEmpty)
            {
                if (image != null)
                {
                    image.Dispose();
                }
                image = frame.ToBitmap();
                return image;
            }
            return null;
        }


        public Bitmap GetFrame0()
        {            
            _ = capture.Read(frame);
            if (!frame.IsEmpty)
            {
                image = frame.ToBitmap();

                // Display the image in the PictureBox control
                //pictureBox1.Image = image;
                return image;
            }
            return null;
        }
        public void StopCam()
        {
            isCapturing = false;
            capture.Stop();
            capture.Dispose();            
        }

    }
}
