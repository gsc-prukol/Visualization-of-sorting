using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Kursova
{
    public partial class Form1 : Form
    {
        Picture picture1;
        Sort sort1;
        Byte sortMethod = 0;
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
            button3.Enabled = true;
            button4.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = false;
            timer1.Enabled = sort1.SortFunction();
            button3.Enabled = timer1.Enabled;
            button4.Enabled = !timer1.Enabled;
            button1.Enabled = !timer1.Enabled;
            button2.Enabled = !timer1.Enabled;
            pictureBox1.Image = picture1.GetBitmap();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Enabled ^= true;
            button1.Enabled ^= true;
            button2.Enabled ^= true; ;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Pixel.Invers();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = sort1.SortFunction();
            button3.Enabled = timer1.Enabled;
            button4.Enabled = !timer1.Enabled;
            button1.Enabled = !timer1.Enabled;
            button2.Enabled = !timer1.Enabled;
            pictureBox1.Image = picture1.GetBitmap();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            timer1.Interval = 990 - trackBar1.Value;
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
        private Sort ChoiceMethod( Byte q)
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
                case 3:
                    sort = new CombSort(picture1);
                    break;
                case 4:
                    sort = new CocktailSort(picture1);
                    break;
                case 5:
                    sort = new QuickSort(picture1);
                    break;
                case 6:
                    sort = new ShellSort(picture1);
                    break;
                case 7:
                    sort = new BogoSort(picture1);
                    break;
                case 8:
                    sort = new MergeSort(picture1);
                    break;
                default:
                    sort = new BubbleSort(picture1);
                    break;

            }
            return sort;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            sortMethod = 3;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            sortMethod = 4;
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            sortMethod = 5;
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            sortMethod = 6;
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            sortMethod = 7;
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            sortMethod = 8;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 s = new Form2();
            s.ShowInTaskbar = false;
            // Display the form in the center of the screen.
            s.StartPosition = FormStartPosition.CenterParent;
            s.ShowDialog();
            this.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "spravka.chm");
        }
    }
    public class Pixel
    {
        protected static bool invers = false;
        public int Key { get; }
        public Color ColorMy { get; }
        public static void Invers()
        {
            invers = !invers;
        }
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
            return (lhs.Key < rhs.Key) ^ invers;
        }
        public static bool operator >(Pixel lhs, Pixel rhs)
        {
            return (lhs.Key > rhs.Key) ^ invers;
        }
        public static bool operator <=(Pixel lhs, Pixel rhs)
        {
            return (lhs.Key <= rhs.Key) ^ invers;
        }
        public static bool operator >=(Pixel lhs, Pixel rhs)
        {
            return (lhs.Key >= rhs.Key) ^ invers;
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
        public Pixel[] Line( int i, int j, int l) //  стовпчик, рядок, довжина
        {
            Pixel[] rez = null;
            if (j >= 0 && j < Height && i >= 0 && i < Width && i+l  < Width)
            {
                rez = new Pixel[l];
                for(int k = 0; k < l; k++)
                {
                    rez[k] = ArrayMy[i + k, j];
                }
            }
            return rez;
        }
        public bool ReplacingLine(Pixel[] sourse, int i, int j)
        {
            int l = sourse.GetLength(0);
            if (j >= 0 && j < Height && i >= 0 && i < Width && i + l- 1  < Width)
            {
                for (int k = 0; k < l; k++)
                {
                    ArrayMy[i + k, j] = sourse[k];
                }
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
        public abstract bool  SortFunction(); //ітераційна, повертає ознаку відсортованості масиву
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
    public class CombSort : Sort
    {
        protected int gap;
        public CombSort(Picture picture) : base(picture)
        {
            gap = Width;
        }
        public override bool SortFunction()
        {
            nend = false;
            UpdateGap();
            for(int j = 0; j < Height; j++)
            {
                for(int i = 0; i < Width - gap; i++)
                {
                    if( pict[i, j] > pict[i + gap, j])
                    {
                        pict.Swap(j, i, i + gap);
                        nend = true;
                    }
                }
            }
            if (gap != 1)
                nend = true;
            return nend;
        }
        protected void UpdateGap()
        {
             gap = (gap * 10) / 13;
             if (gap == 9 || gap == 10)
                 gap = 11;
             gap = Math.Max(1, gap);
        }
    }
    public class CocktailSort: Sort
    {
        protected bool inv;
        public CocktailSort(Picture picture):base(picture)
        {
            inv = true;
        }
        public override bool SortFunction()
        {
            nend = false;
            for (int j = 0; j < Height; j++)
            {
                if (inv)
                {
                    for (int i = 0; i < Width - 1; i++)
                    {
                        if (pict[i, j] > pict[i + 1, j])
                        {
                            pict.Swap(j, i, i + 1);
                            nend = true;
                        }
                    }
                }
                else
                {
                    for (int i = Width - 1; i > 0; i--)
                        if (pict[i - 1, j] > pict[i, j])
                        {
                            pict.Swap(j, i - 1, i);
                            nend = true;
                        }
                }
                
            }
            inv = !inv;
            return nend;
        }
    }
    public class QuickSort : Sort
    {
        Stack<int>[] stekc;
        public QuickSort(Picture picture) : base(picture)
        {
            stekc = new Stack<int>[Height];
            for (int j = 0; j < Height; j++)
            {
                stekc[j] = new Stack<int>();
                stekc[j].Push(0);
                stekc[j].Push(Width);
            }
        }
        public override bool SortFunction()
        {
            nend = false;
            for(int j = 0; j < Height; j++)
            {
                if(stekc[j].LongCount() > 0)
                {
                    nend = true;
                    int end = stekc[j].Pop();
                    int start = stekc[j].Pop();
                    if (end - start > 1)
                    {
                      
                        int p = start + ((end - start) / 2);
                        p = Partition(j, p, start, end);
                        stekc[j].Push(p + 1);
                        stekc[j].Push(end);
                        stekc[j].Push(start);
                        stekc[j].Push(p);
                    }
                }

            }
            return nend;
        }
        protected int Partition(int j, int  p, int  start, int  end)
        {
            int h = end - 2;
            Pixel r = pict[p, j];
            pict.Swap(j, p, end - 1);
            while( start < h)
            {
                if (pict[start, j] < r)
                {
                    start++;
                }
                else if (pict[h, j] >= r)
                {
                    h--;
                }
                else
                {
                    pict.Swap(j, start, h);
                }
            }
            if (pict[h, j] < r)
                h++;
            pict.Swap(j, end - 1, h );
            return h;
        }
        protected void Quick(int p, int q)
        {
            if (p >= q) return;
            Pixel r = pict[p, 0];
            int i = p - 1;
            int j = q + 1;
            while(i < j)
            {
                while (pict[i, 0] >= r)
                    i++;
                while (pict[j, 0] <= r)
                    j--;
                if (i < j)
                    pict.Swap(0, i, j);
            }
            Quick(p, j);
            Quick(j + 1, q);
        }
    }
    public class ShellSort : Sort
    {
        protected int d;
        public ShellSort(Picture picture1) : base(picture1) { d = Width / 2; }
        public override bool SortFunction()
        {
            nend = true;
            for(int j = 0; j < Height; j++)
            {
                for(int i = d; i < Width; i++)
                {
                    for(int k = i; k >= d; k -= d)
                    {
                        if (pict[k-d, j] > pict[k, j])
                            {
                                pict.Swap(j, k, k-d);
                            }
                    }
                }
            }
            d /= 2;
            if(d == 0)
            {
                nend = false;
            }
            return nend;
        }
    }
    public class BogoSort: Sort
    {
        bool[] isSortedBools;
        Random r = new Random();
        public BogoSort(Picture picture) : base(picture)
        {
            isSortedBools = new bool[Height];
            for(int j = 0; j < Height; j++)
            {
                isSortedBools[j] = false;
            }
        }
        public override bool SortFunction()
        {
            Permutation();
            IsSorted();
            nend = false;
            for(int j = 0; j < Height; j++)
            {
                nend = nend || !isSortedBools[j];
            }
            return nend;
        }
        private void IsSorted()
        {
            for(int j = 0; j < Height; j++)
            {
                isSortedBools[j] = true;
                for(int i = 0; i < Width - 1; i++)
                {
                    if(pict[i, j] > pict[i+1, j])
                    {
                        isSortedBools[j] = false;
                    }

                }
            }
        }
        protected void Permutation()
        {
            int a, b;
            for (int j = 0; j < Height; j++)
            {
                if(!isSortedBools[j])
                {
                    for (int i = 0; i < Width / 2; i++)
                    {
                        a = r.Next(Width);
                        b = r.Next(Width);
                        if((pict[a, j] > pict[b, j] && a < b) || (pict[a, j] < pict[b, j] && a > b))
                            pict.Swap(j, a, b);
                    }
                }
                

            }
        }
    }
    public class MergeSort : Sort
    {
        Stack<int> stekc;
        public MergeSort(Picture picture) : base(picture)
        {
            stekc = new Stack<int>();
            FillStekc(0, Width-1);
        }
        protected void FillStekc(int p, int q)
        {
            if( q - p < 1)
            {
                return;
            }
            int c = (p + q) / 2;
            stekc.Push(p);
            stekc.Push(c);
            stekc.Push(q);
            if (q - p > 1)
            {
                FillStekc(c + 1, q);
                FillStekc(p, c);
            }
        }
        public override bool SortFunction()
        {
            int q = stekc.Pop();
            int c = stekc.Pop();
            int p = stekc.Pop();
            for(int z = 0; z < Height; z++)
            {
                int i = p;
                int j = c + 1;
                Pixel[] b = new Pixel[q - p+1];
                for (int k = 0; k < q - p+1; k++)
                {
                    if (j > q || (i <= c && pict[i, z] < pict[j, z]))
                    {
                        b[k] = pict[i, z];
                        i++;
                    }
                    else
                    {
                        b[k] = pict[j, z];
                        j++;
                    }
                }
                pict.ReplacingLine(b, p, z);
            }
            nend = stekc.Count() != 0;
            return nend;
        }
    }
}
