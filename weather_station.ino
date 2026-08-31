
#include <Wire.h>
#include <WiFi.h>
#include <WebSocketsServer.h>
#include <Adafruit_BMP280.h>
#include <DHT.h>
#include <DHT_U.h> 
#include <ArduinoJson.h>


//constants
#define BMP280_ADDR 0x76 //i2c address
#define DHT11_PIN 4
// Globals
WebSocketsServer webSocket = WebSocketsServer(80);
bool client_connected = false;

// Called when receiving any WebSocket message
void onWebSocketEvent(uint8_t num,
                      WStype_t type,
                      uint8_t * payload,
                      size_t length) {

  // Figure out the type of WebSocket event
  switch(type) {

    // Client has disconnected
    case WStype_DISCONNECTED:
      Serial.printf("[%u] Disconnected!\n", num);
      break;

    // New client has connected
    case WStype_CONNECTED:
      {
        IPAddress ip = webSocket.remoteIP(num);
        Serial.printf("[%u] Connection from ", num);
        Serial.println(ip.toString());
      }
      break;

    // For everything else: do nothing
    case WStype_TEXT:
    case WStype_BIN:
    case WStype_ERROR:
    case WStype_FRAGMENT_TEXT_START:
    case WStype_FRAGMENT_BIN_START:
    case WStype_FRAGMENT:
    case WStype_FRAGMENT_FIN:
    default:
      break;
  }
}

const int num_bytes = 16;
const char* ssid     = "TheFenDen";
const char* password = "StinkyCat@1613";


//objects instantiation
Adafruit_BMP280 bmp;
DHT_Unified dht(DHT11_PIN, DHT11);
sensors_event_t event;

void send_weather_data(float temperature, float humidity, float pressure) {
    //create json file
    StaticJsonDocument<128> doc; 

    doc["temperature"] = temperature;
    doc["humidity"] = humidity;
    doc["pressure"] = pressure;

    String json;
    serializeJson(doc, json);

    ws.textAll(json);

    Serial.println("Sent: " + json);
}

void setup() {
  Wire.begin();
  Serial.begin(115200);

  // wifi stufff
  WiFi.begin(ssid, password);
  delay(500);
  while (WiFi.status() != WL_CONNECTED) {
    Serial.println("wifi not connected, trying again...");
    delay(500);
  }

  // Print IP address
  Serial.println("Connected!");
  Serial.print("My IP address: ");
  Serial.println(WiFi.localIP());

  // Start WebSocket server and assign callback
  webSocket.begin();
  webSocket.onEvent(onWebSocketEvent);

  //sensor stuff
  if(!bmp.begin(BMP280_ADDR)){
    while(1){
      Serial.println("BMP280 not found");
      delay(2000);
    }
  }
  dht.begin();
  delay(500);
  bmp.setSampling(  Adafruit_BMP280::MODE_NORMAL,     /* Operating Mode. */
                    Adafruit_BMP280::SAMPLING_X2,     /* Temp. oversampling */
                    Adafruit_BMP280::SAMPLING_X16,    /* Pressure oversampling */
                    Adafruit_BMP280::FILTER_X16,      /* Filtering. */
                    Adafruit_BMP280::STANDBY_MS_500); /* Standby time. */
}



void loop() {
  webSocket.loop();
  /*
  Serial.print("Temperature = ");
  float correction_val = 1;
  float temp_c = (bmp.readTemperature()-correction_val);
  float temp_f = temp_c * (9.0/5.0) + 32;
  Serial.print(temp_c);
  Serial.print(" *C or ");
  Serial.print(temp_f);
  Serial.println(" *F");


  Serial.print("Pressure:");
  int pa_press = bmp.readPressure();
  Serial.print(pa_press);
  Serial.println(" Pa");


  dht.humidity().getEvent(&event);
  float humidity = event.relative_humidity;
  Serial.print("Humidity: ");
  Serial.print(humidity);
  Serial.println("%");
  Serial.println();
  */

  delay(2000); // send once every 2 second
}
