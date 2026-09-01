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
        private CancellationTokenSource? cancellationTokenSource; //check if need


        private void update_weather_lbls(WeatherData weather){
            temp_val_lbl.Text = $"{weather.Temperature}";
            humid_val_lbl.Text = $"{ weather.Humidity}";
            press_val_lbl.Text = $"{weather.Pressure}";
        }
        private void weather_station_client_Load(object sender, EventArgs e)
        {

        }

        // check through this and then reannotate
        private async Task receive_weather_data(CancellationToken cancellationToken){
            //check if ws client was created
            if (ws == null)
            {
                return;
            }

            //message buffer for weather data
            byte[] buffer = new byte[1024];

            try
            {
                //try to read weather data from esp32 while client is connected AND there was no cancellation request
                //need to understand cancellation better
                while (ws.State == WebSocketState.Open && !cancellationToken.IsCancellationRequested)
                {

                    // wait for next  message 
                    WebSocketReceiveResult result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);

                    //close message received close connection
                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        disconnect_ws();
                        break;
                    }

                    //message sent ignore other messages
                    //need to find out how this ignores other mesage
                    if (result.MessageType != WebSocketMessageType.Text)
                    {
                        continue;
                    }

                    // convert the received UTF-8 bytes into a regular C# JSON string
                    string jsonMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);

                    WeatherData weather = JsonSerializer.Deserialize<WeatherData>(jsonMessage, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});

                    if (weather != null)
                    {
                        update_weather_lbls(weather);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // Expected when the application closes.
            }
            catch (WebSocketException ex)
            {
                //figure out what this is for and reannotate
                BeginInvoke(new Action(() =>
                {
                    connect_status_lbl.Text = "Disconnected";
                    MessageBox.Show(
                        $"The ESP32 WebSocket connection was lost.\n\n{ex.Message}",
                        "Connection Lost",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }));
            }
            catch (JsonException ex)
            {
                //figure out what this is for and reannotate
                BeginInvoke(new Action(() =>
                {
                    connect_status_lbl.Text = "Invalid data";
                    MessageBox.Show(
                        $"The ESP32 sent JSON that could not be parsed.\n\n{ex.Message}",
                        "JSON Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }));
            }
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
                await receive_weather_data(cancellationTokenSource.Token);
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
            //check that socket exists and is open
            if (ws != null && ws.State == WebSocketState.Open)
                //disconnet from websocket
                connect_status_lbl.BackColor = Color.Gray;
            connect_status_lbl.Text = "disconnecting...";
            Console.WriteLine("disconnecting from websocket...");
            await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Client disconnected", CancellationToken.None);

            //find out what this does then reannotate
            ws?.Dispose();
            ws = null;

            connect_status_lbl.BackColor = Color.FromArgb(255,128,128);
            connect_status_lbl.Text = "disconnected";
        }
        private void leave_btn_Click(object sender, EventArgs e)
        {
            disconnect_ws();
        }
    }
    public class WeatherData
    {
        public float Temperature { get; set; }
        public float Humidity { get; set; }
        public float Pressure { get; set; }
    }
}
