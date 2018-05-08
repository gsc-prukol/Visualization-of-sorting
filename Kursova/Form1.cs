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
        public Form1()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

    }
    public class Pixel
    {
        protected int Key { get; }
        protected Color Color { get; }

        public Pixel()
        {
            Key = 0;
            Color = Color.Black;
        }
        public Pixel(int key, Color color)
        {
            Key = key;
            Color = color;
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
        int Height, Width; // Висота, Ширина
        Pixel[,] ArrayMy;
        Picture()
        {
            Height = 0;
            Width = 0;
            ArrayMy = null;
        }
        Picture(int height, int width, Pixel [,] array)
        {
            Height = height;
            Width = width;
            ArrayMy = new Pixel[height, width];
            Array.Copy(array, ArrayMy, height * width);
        }
    }
}
