using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace weather_station

{
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //initalize webSocket
        private ClientWebSocket ws;
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void load_elements(){

        }
        private async void join_btn_Click(object sender, EventArgs e)
        {
            ws = new ClientWebSocket();
            Uri esp32Uri = new Uri("ws://192.168.0.238");
            Console.WriteLine("connected to weather station!");

            //try connecting to websocket
            status.BackColor = Color.Gray;
            status.Text = "connecting...";
            Console.WriteLine("attempting to connect to websocket...");

            try
            {
                await ws.ConnectAsync(esp32Uri, CancellationToken.None);

                status.BackColor = Color.Lime;
                status.Text = "connected";
                Console.WriteLine("connected to weather station!");
            }
            catch (Exception ex)
            {
                status.BackColor = Color.Red;
                status.Text = "connection failed";
                Console.WriteLine($"Connection error: {ex.Message}");
            }

        }

        private async void leave_btn_Click(object sender, EventArgs e)
        {
            if (ws?.State == WebSocketState.Open)
                //disconnet from websocket
                status.BackColor = Color.Gray;
                status.Text = "disconnecting...";
                Console.WriteLine("disconnecting from websocket...");
                await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Client disconnected", CancellationToken.None);

            ws?.Dispose();
            ws = null;

            status.BackColor = Color.Red;
            status.Text = "disconnected";
        }
    }
}
