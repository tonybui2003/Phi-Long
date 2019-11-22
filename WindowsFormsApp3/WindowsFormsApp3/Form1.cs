using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        UdpClient client = new UdpClient(8888); //PORT NUM
        string data = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                client.BeginReceive(new AsyncCallback(recv), null);
            }
            catch (Exception ex)
            {
                richTextBox1.Text += ex.Message.ToString();
                
            }
        }
        void recv(IAsyncResult res)
        { IPEndPoint RemoteIP = new IPEndPoint(IPAddress.Any, 60240);
            byte[] received = client.EndReceive(res, ref RemoteIP);
            data = Encoding.UTF8.GetString(received);
            this.Invoke(new MethodInvoker(delegate
            {
                richTextBox1.Text += "\nReceived data:" + data;
            }));
            client.BeginReceive(new AsyncCallback(recv), null);
            
        }
    }
}
