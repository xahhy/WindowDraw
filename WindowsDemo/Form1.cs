using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;

namespace WindowsDemo
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            //creat initial image for picture box
            picturebox.Image = new Bitmap(picturebox.Width,picturebox.Height);
            
            //if (File.Exists("./background.jpg"))
            //{
                //file exists
            //}
            //else
            //{
                //no image file
                //create bitmap for picturebox.Image
                Bitmap bq_background = new Bitmap(this.picturebox.Image);
                //creat graphics for bitmap
                Graphics g = Graphics.FromImage(bq_background);
                //fill the graphics with one color
                g.Clear(Color.White);            
                //save the bitmap
                bq_background.Save("./background.jpg");
                picturebox.Image = bq_background;                        
            //}                       
        }
        
        public Point PicLocation = new Point(200, 200);
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        Graphics canvas;
        private void picturebox_Paint(object sender, PaintEventArgs e)
        {
            
        }
        string str;
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "*.jpg|*.jpg|*.bmp|*.bmp|*.gif|*.gif";
            ofd.ShowDialog();          
            str = ofd.FileName;
            if (str == "") return;
            Image temp = Image.FromFile(str);
            //Pic = new Bitmap(temp,new Size(200,200));           
            //picturebox.Refresh();
        }
        public static bool IsDraw = false;
        Point StartPoint = new Point(0, 0);
        Point EndPoint = new Point(0, 0);
        private void picturebox_MouseDown(object sender, MouseEventArgs e)
        {
            IsDraw = true;
            StartPoint.X = e.X;
            StartPoint.Y = e.Y;
            Image temp = (Image)picturebox.Image.Clone();
            temp.Save("./tempo.jpg");
            temp.Dispose();
        }

        private void picturebox_MouseUp(object sender, MouseEventArgs e)
        {
            IsDraw = false;
            EndPoint.X = e.X;
            EndPoint.Y = e.Y;
                      
            //picturebox.Image.Save("./temp.jpg");            
            
        }
        Graphics g;
        private void picturebox_MouseMove(object sender, MouseEventArgs e)
        {
            if(IsDraw)
            {
                //创建一个tempo.jpg的缓存图像，代表了MouseDown时刻的初始图片状态，每次移动都在在缓存图像上绘制制定的图片，并在picturebox.Image上
                //绘制画好的缓存图片，并存储为temp.jpg，每次鼠标移动都是在tempo.jpg图片的基础上画，不会保存拖动的轨迹。
                
                Image tempb = Image.FromFile("./tempo.jpg");
                Bitmap tempImage = new Bitmap(tempb);
                tempb.Dispose();
                g = Graphics.FromImage(tempImage);                               
                EndPoint.X = e.X;
                EndPoint.Y = e.Y;
                if (str == ""||str==null) return;
                int width, hight;
                width = Math.Abs(EndPoint.X - StartPoint.X);
                hight = Math.Abs(EndPoint.Y - StartPoint.Y);
                if (width == 0 || hight == 0) return;
                Image Pic = new Bitmap(Image.FromFile(str), new Size(width,hight));
                //picturebox.Refresh();
                g.DrawImage(Pic,StartPoint);
                //Image temp = (Image).Clone();
                Graphics g2 = Graphics.FromImage(picturebox.Image);
                picturebox.Refresh();
                g2.DrawImage(tempImage, 0, 0);
                picturebox.Image.Save("./temp.jpg");
                g2.Dispose();
                tempImage.Dispose();
                
            }
        }
        const int BQ_SIZE_X = 720;
        const int BQ_SIZE_Y = 576;
        private void 标清电视ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            picturebox.Size = new Size(BQ_SIZE_X,BQ_SIZE_Y);
            Bitmap bq_background2 = new Bitmap(BQ_SIZE_X,BQ_SIZE_Y);
            Graphics g = Graphics.FromImage(bq_background2);//create g Graphics from new Bitmap bq_background
            g.FillRectangle(Brushes.Red, new Rectangle(0, 0, BQ_SIZE_X, BQ_SIZE_Y));
            //picturebox.Image = bq_background2;
            //File.Delete("./background.jpg");
            bq_background2.Save("./background.jpg");
            bq_background2.Dispose();
            g.Dispose();
        }
    }
}
