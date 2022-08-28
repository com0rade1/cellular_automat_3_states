using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        int Scale;
        Random rnd = new Random();
        int[] InputData = new int[301];
        int[] TempData = new int[301];
        int[] BordPixNum = new int[301];
        int Count;
        int PrintStr;
        string LifeRule = "01000000000";

        public Form1()
        {
            InitializeComponent();
        }

        private void Run_Click(object sender, EventArgs e)
        {
            this.Scale = 2 * (ScaleBar.Value + 1);
            Graphics g = GameField.CreateGraphics();
            g.Clear(Color.White);
            this.Count = (int)(GameField.Width / this.Scale);
            textBox1.Text = Convert.ToString(this.Count);

        }

        private void Step_Click(object sender, EventArgs e)
        {
            Graphics g = GameField.CreateGraphics();
            int ALive = 0;
            this.PrintStr++;
            if (PrintStr < Count)
            {
                for (int i = 0; i < this.Count; i++)
                {
                    ALive = 0;
                    if (i == 0)
                    {
                        ALive = InputData[Count - 2] + InputData[Count - 1] + InputData[0] + InputData[1] + InputData[2];
                    }
                    if (i == 1)
                    {
                        ALive = InputData[Count - 1] + InputData[0] + InputData[1] + InputData[2] + InputData[3];
                    }
                    if (i == Count - 1)
                    {
                        ALive = InputData[Count - 3] + InputData[Count - 2] + InputData[Count - 1] + InputData[0] + InputData[1];
                    }
                    if (i == Count - 2)
                    {
                        ALive = InputData[Count - 4] + InputData[Count - 3] + InputData[Count - 2] + InputData[Count - 1] + InputData[0];
                    }
                    if (i != 0 && i != 1 && i != Count - 2 && i != Count - 1)
                    {
                        int k = -2;
                        while ((k - 3) != 0)
                        {
                            ALive += InputData[i + k];
                            k++;
                        }
                    }
                    if (Convert.ToInt32(LifeRule[ALive]) == '0')
                    {
                        TempData[i] = 0;
                    }
                    else if (Convert.ToInt32(LifeRule[ALive]) == '1')
                    {
                        TempData[i] = 1;
                    }
                    else if (Convert.ToInt32(LifeRule[ALive]) == '2')
                    {
                        TempData[i] = 2;
                    }
                }
                for (int i = 0; i < Count; i++)
                {
                    if (TempData[i] == 0)
                    {
                        g.FillRectangle(Brushes.Red, BordPixNum[i], BordPixNum[PrintStr], this.Scale, this.Scale);
                    }
                    else if (TempData[i] == 1)
                    {
                        g.FillRectangle(Brushes.Green, BordPixNum[i], BordPixNum[PrintStr], this.Scale, this.Scale);
                    }
                    else if (TempData[i] == 2)
                    {
                        g.FillRectangle(Brushes.Blue, BordPixNum[i], BordPixNum[PrintStr], this.Scale, this.Scale);
                    }
                }
                for (int i = 0; i < Count; i++)
                {
                    InputData[i] = TempData[i];
                }
            }
            else
            {
                g.Clear(Color.White);
                PrintStr = -1;
                this.Step_Click(sender, e);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            while (PrintStr < Count - 1)
            {
                this.Step_Click(sender, e);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Graphics g = GameField.CreateGraphics();
            g.Clear(Color.White);
            int k = 0;
            for (int i = 0; i <= GameField.Width; i += Scale)
            {
                this.BordPixNum[k] = i;
                k++;
            }
            for (int i = 0; i < Count; i++)
            {
                InputData[i] = rnd.Next(0, 3);
            }
            for (int i = 0; i < Count; i++)
            {
                if (InputData[i] == 0)
                {
                    g.FillRectangle(Brushes.Red, BordPixNum[i], 0, this.Scale, this.Scale);
                }
                else if (InputData[i] == 1)
                {
                    g.FillRectangle(Brushes.Green, BordPixNum[i], 0, this.Scale, this.Scale);
                }
                else if (InputData[i] == 2)
                {
                    g.FillRectangle(Brushes.Blue, BordPixNum[i], 0, this.Scale, this.Scale);
                }
            }
            this.PrintStr = 0;

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (maskedTextBox1.Text != "")
            {
                this.LifeRule = maskedTextBox1.Text;
                while (LifeRule.Length < 11)
                {
                    this.LifeRule = this.LifeRule + "0";
                }
            }

        }

        private void Button4_Click(object sender, EventArgs e)
        {
            string Rule = "";
            maskedTextBox1.Text = "";
            for (int i = 0; i<=10;i++)
            {
                Rule += rnd.Next(0, 3);
            }
            this.LifeRule = Rule;
            maskedTextBox1.Text = Rule;
        }
    }
}

