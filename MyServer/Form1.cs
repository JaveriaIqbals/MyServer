using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimpleTCP;
using System.Net;

namespace MyServer
{
    public partial class Form1 : Form
    {
        SimpleTcpServer tcp;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.textBox3.Text = "Started ... ";
            string tempIP = this.textBox1.Text;
            IPAddress ipObj = IPAddress.Parse(tempIP);
            int portNumber = Convert.ToInt32(this.textBox2.Text);
            
            tcp.Start(ipObj, portNumber);
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (tcp.IsStarted)
            {
                tcp.Stop();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tcp = new SimpleTcpServer();
            tcp.DataReceived += serverFunction;
        }

        void serverFunction (object sender, SimpleTCP.Message e)
        {
            // this is from client
           this.textBox3.Invoke((MethodInvoker)delegate ()
           {
               this.textBox3.Text = e.MessageString; // this is client message
               
               // this is server message
               e.ReplyLine(String.Format("From server : I have received your message, {0}" , 
                   e.MessageString));

           });

        }
    }
}
