using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Kinect;
using System.IO;

namespace KinectForm
{
    public partial class Form1 : Form
    {
        private KinectSensor kinect = null;
        static public DateTime dt = DateTime.Now;//日付
        static string pathDoc = Environment.GetFolderPath(Environment.SpecialFolder.Personal);//マイドキュメントのパス
        static string path = null;
        StreamWriter swc;//ファイル書き込み用
        private List<Bitmap> bmp = new List<Bitmap>();
        int frame = 0;//フレームカウント

        byte[] colorData = new byte[640 * 480 * 4];

        public Form1()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            try
            {
                if (KinectSensor.KinectSensors.Count == 0)
                    throw new Exception("Kinectが接続されていません");

                // Kinectセンサーの取得
                kinect = KinectSensor.KinectSensors[0];

                //スケルトン平滑化パラメータ
                TransformSmoothParameters parameters = new TransformSmoothParameters();
                parameters.Smoothing = 0.2f;
                parameters.Correction = 0.8f;
                parameters.Prediction = 0.0f;
                parameters.JitterRadius = 0.5f;
                parameters.MaxDeviationRadius = 0.5f;

                //color,skeletonの有効化(引数は解像度とフレームレート)
                kinect.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
                kinect.SkeletonStream.Enable(parameters);

                //ファイル書き込み用のdirectoryを用意
                string date = dt.Year + digits(dt.Month) + digits(dt.Day) + digits(dt.Hour) + digits(dt.Minute);
                path = pathDoc + "/HoeData/" + date;
                Directory.CreateDirectory(path);
                Directory.CreateDirectory(path+"/bmp");
                //ファイルを用意
                swc = new StreamWriter(path + "/Kinect.csv", false);

                //カラーフレームのイベントハンドラの登録
                kinect.ColorFrameReady += ColorImageReady;

                //スケルトンフレームのイベントハンドラの登録
                kinect.SkeletonFrameReady += SkeletonFrameReady;

                //Kinectセンサーからのストリーム取得を開始、以降、Kinectランタイムからフレーム毎に登録したColorFrameReadyメソッドが呼び出される
                kinect.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }
        }

        private void SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            //スケルトンフレームのデータをskeletonFrame変数に保持
            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {
                //スケルトンフレームがnullでないならそのまま
                if (skeletonFrame != null)
                {
                    //人数分のスケルトンデータ変数を配列で作る
                    Skeleton[] skeletonData = new Skeleton[skeletonFrame.SkeletonArrayLength];
                    //配列に各人物のスケルトンフレームのデータをコピー
                    skeletonFrame.CopySkeletonDataTo(skeletonData);

                    //スケルトンを認識した人数の数だけ繰り返す
                    foreach (var skeleton in skeletonData)
                    {
                        //その人物のスケルトンがTracked状態なら続ける
                        if (skeleton.TrackingState == SkeletonTrackingState.Tracked)
                        {
                            jointNum.Text = "joint:"+skeleton.Joints.Count;
                            //各ジョイントの数だけ繰り返す
                            foreach (Joint joint in skeleton.Joints)
                            {
                                /*
                                swc.WriteLine(frame + ","
                                   + skeleton.Position.X + ","
                                   + skeleton.Position.Y + ","
                                   + skeleton.Position.Z);
                                */
                                if(kinectRec.Checked)
                                swc.WriteLine(frame+","
                                   +(skeleton.Position.X-skeleton.Joints[JointType.HipCenter].Position.X)+","
                                   +(skeleton.Position.Y-skeleton.Joints[JointType.HipCenter].Position.Y)+","
                                   +(skeleton.Position.Z-skeleton.Joints[JointType.HipCenter].Position.Z));
                            }
                            if (kinectRec.Checked)
                                swc.WriteLine(frame + ","
                                    + skeleton.Position.X + ","
                                    + skeleton.Position.Y + ","
                                    + skeleton.Position.Z);
                        }
                    }
                }
            }
        }

        private void ColorImageReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            //ColorImageFrameデータを貰う
            using (ColorImageFrame imageFrame = e.OpenColorImageFrame())
            {
                if (imageFrame != null)
                {
                    imageFrame.CopyPixelDataTo(colorData);
                    unsafe
                    {
                        fixed (byte* ptr = &colorData[0])
                        {
                            Bitmap bmpImage = new Bitmap(
                                    imageFrame.Width,
                                    imageFrame.Height,
                                    imageFrame.BytesPerPixel * imageFrame.Width,
                                    System.Drawing.Imaging.PixelFormat.Format32bppRgb,
                                    (IntPtr)ptr);
                            this.rgbImage.Image = bmpImage;
                            if (kinectRec.Checked)
                            {
                                bmp.Add(bmpImage);
                                //bmpImage.Save(path + "/bmp/" + frame + ".bmp", System.Drawing.Imaging.ImageFormat.Bmp);
                            }
                            limit.Text = "limit:"+bmp.Count;
                        }
                    }
                }
            }
            frame++;
        }

        public String digits(int date)
        {
            if (date / 10 == 0) return "0" + date;
            else return date.ToString();
        }

        protected override void OnClosed(EventArgs e)
        {
            int cnt = 0;
            foreach (var b in bmp)
            {
                b.Save(path + "/bmp/" + cnt + ".bmp", System.Drawing.Imaging.ImageFormat.Bmp);
                cnt++;
            }
            swc.Close();
            kinect.Stop();
            kinect.Dispose();
        }
    }
}
