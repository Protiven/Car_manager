using System;
using System.Threading;
using System.IO.Ports;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Car_s_manager
{
    public partial class Form1 : Form
    {

        static SerialPort S_1 = new SerialPort();
        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;

            this.KeyDown +=
                new KeyEventHandler(Form1_Key);

            textBox3.KeyDown += new KeyEventHandler(Form1_Key);
        }

        void listening_func()
        {
            for (; ; )
            {
                string text = S_1.ReadLine();
                textBox1.Text = text;
            }
        }

        void Form1_Key(object sender, KeyEventArgs e)
        {
            string text;
            switch ((char)e.KeyValue)
            {
                case (char)Keys.Left:
                    text = "4";
                    S_1.Write(Encoding.ASCII.GetBytes(text), 0, text.Length);
                    textBox1.Text = "Left";
                    break;
                case (char)Keys.Up:
                    text = "1";
                    S_1.Write(Encoding.ASCII.GetBytes(text), 0, text.Length);
                    textBox1.Text = "Forward";
                    break;
                case (char)Keys.Down:
                    text = "2";
                    S_1.Write(Encoding.ASCII.GetBytes(text), 0, text.Length);
                    textBox1.Text = "Backward";
                    break;
                case (char)Keys.Right:
                    text = "3";
                    S_1.Write(Encoding.ASCII.GetBytes(text), 0, text.Length);
                    textBox1.Text = "Right";
                    break;
            }
            e.Handled = true;
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            string COM = textBox2.Text;
            S_1.BaudRate = 38400;
            S_1.PortName = COM;

            textBox2.Enabled = false;
            button1.Enabled = false;

            try
            {
                if (!S_1.IsOpen)
                    S_1.Open();
            }
            catch (Exception ex)
            {
                textBox1.Text = ex.ToString();
            }
            var Th = new Thread(new ThreadStart(listening_func));
            Th.Start();
            textBox3.Enabled = true;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string add_str;
                int ch = Convert.ToInt32(Convert.ToDouble(textBox3.Text) * Convert.ToDouble(255) / Convert.ToDouble(100));
                if (ch < 100 && ch > 0)
                    add_str = '0' + ch.ToString();
                else 
                    add_str = ch.ToString();
                
                string speed = 's' + add_str;
                
                S_1.Write(Encoding.ASCII.GetBytes(speed), 0, speed.Length);
            }
            catch (Exception ex)
            {
                textBox1.Text = ex.ToString();
            }
        }

    }
}
