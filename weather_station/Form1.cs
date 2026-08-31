/*
 * non functional missing some ? modifiers to prevent accessing null targets will figure that out later, 
 * next thing to work on is moving the entire project to a .net8 project because of commpatibility issues
 * 
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
        private CancellationTokenSource _cancellationTokenSource; //check if need
        private void Form1_Load(object sender, EventArgs e)
        {

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
                //non-blocking join
                await ws.ConnectAsync(esp32Uri, CancellationToken.None);

                status.BackColor = Color.Lime;
                status.Text = "connected";
                Console.WriteLine("connected to weather station!");
                //non-blocking receive
                await receive_weather_data(_cancellationTokenSource.Token);
            }
            catch (Exception ex)
            {
                status.BackColor = Color.Red;
                status.Text = "connection failed";
                Console.WriteLine($"Connection error: {ex.Message}");
            }



        }
        private async void disconnect_ws(){
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
        private void leave_btn_Click(object sender, EventArgs e)
        {
            disconnect_ws();
        }

       
        // check through this and then reannotate
        private async Task receive_weather_data(CancellationToken cancellationToken)
        {
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
                while (ws.State == WebSocketState.Open &&!cancellationToken.IsCancellationRequested){

                    // wait for next  message 
                    WebSocketReceiveResult result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);

                    //close message received close connection
                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        disconnect_ws();
                        break;
                    }

                    //message sent ignore other message
                    //need to find out how this ignores other mesage
                    if (result.MessageType != WebSocketMessageType.Text)
                    {
                        continue;
                    }

                    // convert the received UTF-8 bytes into a regular C# JSON string
                    string jsonMessage = Encoding.UTF8.GetString(buffer,0,result.Count);

                    WeatherData weather = JsonSerializer.Deserialize<WeatherData>(jsonMessage,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                    if (weather != null)
                    {
                        UpdateWeatherLabels(weather);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // Expected when the application closes.
            }
            catch (WebSocketException ex)
            {
                BeginInvoke(new Action(() =>
                {
                    lblConnectionStatus.Text = "Disconnected";
                    MessageBox.Show(
                        $"The ESP32 WebSocket connection was lost.\n\n{ex.Message}",
                        "Connection Lost",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }));
            }
            catch (JsonException ex)
            {
                BeginInvoke(new Action(() =>
                {
                    lblConnectionStatus.Text = "Invalid data";
                    MessageBox.Show(
                        $"The ESP32 sent JSON that could not be parsed.\n\n{ex.Message}",
                        "JSON Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }));
            }
        }

    }
    public class WeatherData
    {
        public float Temperature { get; set; }
        public float Humidity { get; set; }
        public float Pressure { get; set; }
    }

}
