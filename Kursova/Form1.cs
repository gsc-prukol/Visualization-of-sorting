using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursova
{
    public partial class Form1 : Form
    {
        Picture picture1;
        Sort sort1;
        int sortMethod = 0;
        public Form1()
        {
            InitializeComponent();
            picture1 = new Picture(new Bitmap(pictureBox1.Image));
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            picture1.Shuffle();
            pictureBox1.Image = picture1.GetBitmap();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sort1 = ChoiceMethod(sortMethod);
            timer1.Enabled = sort1.SortFunction();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Enabled ^= true; ;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = sort1.SortFunction();
            pictureBox1.Image = picture1.GetBitmap();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            timer1.Interval = 1000 - trackBar1.Value;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            sortMethod = 0;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            sortMethod = 1;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            sortMethod = 2;
        }
        private Sort ChoiceMethod( int q)
        {
            Sort sort;
            switch(q)
            {
                case 1:
                    sort = new SelectionSort(picture1);
                    break;
                case 2:
                    sort = new InsertionSort(picture1);
                    break;
                default:
                    sort = new BubbleSort(picture1);
                    break;

            }
            return sort;
        }
    }
    public class Pixel
    {
        public int Key { get; }
        public Color ColorMy { get; }

        public Pixel()
        {
            Key = 0;
            ColorMy = Color.Black;
        }
        public Pixel(int key, Color color)
        {
            Key = key;
            ColorMy = color;
        }
        public static bool operator <(Pixel lhs, Pixel rhs)
        {
            return lhs.Key < rhs.Key;
        }
        public static bool operator >(Pixel lhs, Pixel rhs)
        {
            return lhs.Key > rhs.Key;
        }
        public static bool operator <=(Pixel lhs, Pixel rhs)
        {
            return lhs.Key <= rhs.Key;
        }
        public static bool operator >=(Pixel lhs, Pixel rhs)
        {
            return lhs.Key >= rhs.Key;
        }
    }
    public class  Picture
    {
        public int Width { get; }  //Ширина
        public int Height { get; } //Висота
        public Pixel[,] ArrayMy;
        public Picture()
        {
            Height = 0;
            Width = 0;
            ArrayMy = null;
        }
        public Picture(Bitmap sourse)
        {
            Width = sourse.Size.Width;
            Height = sourse.Size.Height;
            ArrayMy = new Pixel[Width, Height];
            for (int i = 0; i < Width; i++)
                for (int j = 0; j < Height; j++)
                {
                    ArrayMy[i, j] = new Pixel(i, sourse.GetPixel(i, j));
                }
        }
        public Picture( int width, int height, Pixel [,] array)
        {
            Height = height;
            Width = width;
            ArrayMy = new Pixel[width, height];
            Array.Copy(array, ArrayMy, height * width);
        }
        public Bitmap GetBitmap()
        {
            Bitmap bitmap = new Bitmap(Width, Height);
            for (int i = 0; i < Width; i++)
                for (int j = 0; j < Height; j++)
                    bitmap.SetPixel(i, j, ArrayMy[i,j].ColorMy);
            return bitmap;
        }
        public void Shuffle()
        {
            int x, y, x2;
            Pixel p;
            Random rand = new Random();
            for(int i = 0; i < Width * Height; i++)
            {
                y = rand.Next(Height);
                x = rand.Next(Width);
                x2 = rand.Next(Width);
                p = ArrayMy[x, y];
                ArrayMy[x, y] = ArrayMy[x2, y];
                ArrayMy[x2, y] = p;
            }

        }
        public Pixel this [int i, int j]
        {
            get
            {
                if (i >= 0 && i < Width && j >= 0 && j < Height)
                    return ArrayMy[i, j];
                else
                    return new Pixel();
            }
            set
            {
                if (i >= 0 && i < Width && j >= 0 && j < Height)
                    ArrayMy[i, j] = value;
            }
        }
        public bool Swap(int j, int i1, int i2) // рядокб стовпчик1, стовпчик2
        {
            if (j >= 0 && j < Height && i1 >= 0 && i1 < Width && i2 >= 0 && i2 < Width)
            {
                Pixel pixel = ArrayMy[i1, j];
                ArrayMy[i1, j] = ArrayMy[i2, j];
                ArrayMy[i2, j] = pixel;
                return true;
            }
            return false;
        }
        
    }
    public abstract class Sort
    {
        public int Width { get; set; }  //Ширина
        public int Height { get; set; } //Висота
        protected Picture pict;
        protected bool nend = false;
        protected Sort(Picture picture)
        {
            Width = picture.Width;
            Height = picture.Height;
            pict = picture;
        }
        public abstract bool  SortFunction(); //можливо ітераційна, повертає ознаку відсортованості масиву
    }
    public class BubbleSort : Sort
    {
        public BubbleSort(Picture picture1) : base(picture1) {}
        public override bool SortFunction()
        {
            nend = false;
            for (int j = 0; j < Height; j++)
            {
                for (int i = 0; i < Width - 1; i++)
                {
                    if (pict[i, j] > pict[i+1, j])
                    {
                        pict.Swap(j, i, i + 1);
                        nend = true;
                    }
                }
            }
            return nend;
        }
    }
    public class SelectionSort : Sort
    {
        protected int k = 0;
        public SelectionSort(Picture picture): base(picture){}
        public override bool SortFunction()
        {
            nend = false;
            for (int j = 0; j < Height; j++)
            {
                int q = k;
                for (int i = k; i < Width; i++)
                {
                    if( pict[q, j] > pict[i, j])
                    {
                        q = i;
                        nend = true;
                    }
                    
                }
                pict.Swap(j, k, q);
            }
            k++;
            return nend;
        }
    }
    public class InsertionSort : Sort
    {
        protected int k = 1;
        public InsertionSort(Picture picture) : base(picture) { nend = true; }
        public override bool SortFunction()
        {
            for (int j = 0; j < Height; j++)
            {
                for(int i = k; i > 0; i--)
                    if( pict[i-1, j] > pict[i, j])
                    {
                        pict.Swap(j, i - 1, i);
                    }
            }
            k++;
           if( k == Width)
            {
                nend = false;
            }
            return nend;
        }
    }
}
