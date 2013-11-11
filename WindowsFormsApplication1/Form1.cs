using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using Microsoft.WindowsAPICodePack.Taskbar;
using System.Diagnostics;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private int day;
        private int max;
        private Form1 that;
        private Stopwatch timer = new Stopwatch();
        public Form1()
        {
            InitializeComponent();
            day = 8 * 60;
            max = 100;
            that = this;

            ThumbnailToolBarButton oneButton = new ThumbnailToolBarButton(TimeTrack.Properties.Resources.one, "+1 Minute");
            oneButton.Click += button1_Click;

            ThumbnailToolBarButton fiveButton = new ThumbnailToolBarButton(TimeTrack.Properties.Resources.five, "+5 Minuten");
            fiveButton.Click += button2_Click;

            ThumbnailToolBarButton tenButton = new ThumbnailToolBarButton(TimeTrack.Properties.Resources.ten, "+10 Minuten");
            tenButton.Click += button3_Click;

            ThumbnailToolBarButton thirtyButton = new ThumbnailToolBarButton(TimeTrack.Properties.Resources.thirty, "+30 Minuten");
            thirtyButton.Click += button4_Click;

            ThumbnailToolBarButton sixtyButton = new ThumbnailToolBarButton(TimeTrack.Properties.Resources.sixty, "+60 Minuten");
            sixtyButton.Click += button5_Click;

            TaskbarManager.Instance.ThumbnailToolBars.AddButtons(this.Handle, oneButton, fiveButton, tenButton, thirtyButton, sixtyButton);
        }

        private void callbacks() {
            if (day < 0)
            {
                day = 8 * 60;
            }
            label1.Text = labelText();
            label1.Update();
            that.Text = labelText();
            double percentage = day / 4.8;
            progressBar1.Value = 100 - Convert.ToInt32(percentage);
            progressBar1.Update();
            var prog = Microsoft.WindowsAPICodePack.Taskbar.TaskbarManager.Instance;
            prog.SetProgressState(Microsoft.WindowsAPICodePack.Taskbar.TaskbarProgressBarState.Normal);
            prog.SetProgressValue(100 - Convert.ToInt32(percentage), max);
        }

        private string labelText() {
            double tempHours = ( 480 - day ) / 60;
            int hours = Convert.ToInt32(Math.Floor(tempHours));
            int minutes = ( 480 - day ) % 60;
            if (minutes < 10)
            {
                return hours + ":0" + minutes + " h";
            }
            else
            {
                return hours + ":" + minutes + " h";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            day = day - 1;
            callbacks();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            day = day - 5;
            callbacks();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            day = day - 10;
            callbacks();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            day = day - 30;
            callbacks();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            day = day - 60;
            callbacks();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            float hours;
            try
            {
                if (int.Parse(textBox1.Text) < 0)
                {
                    textBox1.ForeColor = Color.Red;
                }
                else
                {
                    textBox1.ForeColor = Color.Black;
                    hours = float.Parse(textBox1.Text) / 60;
                    label2.Text = "Minuten sind " + String.Format("{0:0.00}", hours) + " Stunden";
                    label2.Update();
                }
            }
            catch
            {
                textBox1.ForeColor = Color.Red;
            }
        }


    }
}
