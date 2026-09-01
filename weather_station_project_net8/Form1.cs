/*
 * new .net8 framework
 * non functional missing some ? modifiers to prevent accessing null targets will figure that out later, 
 * next thing to work on is moving the old project functions to this file
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json; //not accepted by compiler
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace weather_station_project_net8
{
    public partial class weather_station_client : Form
    {
        public weather_station_client()
        {
            InitializeComponent();
        }
        //initalize webSocket
        private ClientWebSocket? ws;
        private CancellationTokenSource? _cancellationTokenSource; //check if need
        private void weather_station_client_Load(object sender, EventArgs e)
        {

        }

        //join weather station server
        private async void join_btn_Click(object sender, EventArgs e)
        {
            ws = new ClientWebSocket();
            Uri esp32Uri = new Uri("ws://192.168.0.238");
            Console.WriteLine("connected to weather station!");

            //try connecting to websocket
            connect_status_lbl.BackColor = Color.Gray;
            connect_status_lbl.Text = "connecting...";
            Console.WriteLine("attempting to connect to websocket...");

            try
            {
                //non-blocking join
                await ws.ConnectAsync(esp32Uri, CancellationToken.None);

                connect_status_lbl.BackColor = Color.Lime;
                connect_status_lbl.Text = "connected";
                Console.WriteLine("connected to weather station!");
                //non-blocking receive
                // uncomment this after testing join
                //await receive_weather_data(_cancellationTokenSource.Token);
            }
            catch (Exception ex)
            {
                connect_status_lbl.BackColor = Color.Red;
                connect_status_lbl.Text = "connection failed";
                Console.WriteLine($"Connection error: {ex.Message}");
            }

        }
        //disconnect and update screen
        private async void disconnect_ws()
        {
            if (ws?.State == WebSocketState.Open)
                //disconnet from websocket
                connect_status_lbl.BackColor = Color.Gray;
            connect_status_lbl.Text = "disconnecting...";
            Console.WriteLine("disconnecting from websocket...");
            await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Client disconnected", CancellationToken.None);

            ws?.Dispose();
            ws = null;

            connect_status_lbl.BackColor = Color.Red;
            connect_status_lbl.Text = "disconnected";
        }
        private void leave_btn_Click(object sender, EventArgs e)
        {
            disconnect_ws();
        }
    }
}
