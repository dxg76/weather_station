# ESP32 Webserver Weather Station

A compact, Wi-Fi-connected weather station built around an ESP32. It reads local temperature, relative humidity, and atmospheric pressure, then delivers the measurements as JSON to a .NET 8 Windows desktop client over WebSockets.

> This project was developed after graduation from the University of West Florida as a hands-on embedded systems, networking, and C#/.NET project.

## Overview

The prototype consists of an ESP32-WROOM-32D installed on a solderless breadboard alongside temperature/humidity and pressure sensors. The ESP32 hosts a WebSocket server that allows a remote client to connect and receive current sensor readings.

The companion Windows application is built with C# and .NET 8 in Visual Studio. It connects to the ESP32, receives weather data in JSON format, parses the incoming values, and updates the application's temperature, pressure, and humidity displays.

```text
┌─────────────────────────────────────────────┐
│              ESP32 Weather Station          │
│                                             │
│  BMP280 ──┐                                 │
│           ├── I²C / GPIO ──> ESP32          │
│  DHT11 ──┘                    │             │
│                                │ Wi-Fi      │
└────────────────────────────────┼────────────┘
                                 │
                                 │ WebSocket + JSON
                                 ▼
                  ┌─────────────────────────┐
                  │ .NET 8 Windows Client   │
                  │                         │
                  │ -  Connects to ESP32     │
                  │ -  Receives JSON data    │
                  │ -  Parses sensor values  │
                  │ -  Updates UI labels     │
                  └─────────────────────────┘
```

## Features

- Collects **temperature** and **relative humidity** using a DHT11 sensor
- Collects **atmospheric pressure** using a BMP280 sensor
- Connects to a local Wi-Fi network
- Hosts a WebSocket server on the ESP32
- Sends sensor readings in JSON format
- Uses a C#/.NET 8 Windows application as the remote monitoring client
- Updates weather values in the desktop application interface

## Hardware

| Quantity | Component | Purpose |
|---:|---|---|
| 1 | ESP32-WROOM-32D | Main microcontroller, Wi-Fi connectivity, and WebSocket server |
| 1 | BMP280 pressure sensor | Atmospheric pressure measurement |
| 1 | DHT11 sensor | Temperature and relative-humidity measurement |
| 1 | Small solderless breadboard | Prototype assembly |
| Assorted | Color-coded jumper wires | Power and signal connections |

## Software

| Component | Technology |
|---|---|
| Embedded firmware | ESP32-compatible Arduino framework |
| Desktop client | C# |
| Desktop framework | .NET 8 |
| IDE | Visual Studio |
| Device-to-client communication | WebSockets |
| Message format | JSON |

## System Operation

1. The ESP32 starts, connects to the configured Wi-Fi network, and initializes the BMP280 and DHT11 sensors.
2. The ESP32 starts a WebSocket server on the local network.
3. The Windows application connects to the ESP32 using its IP address and WebSocket endpoint.
4. The ESP32 reads the sensors at a defined interval.
5. The sensor readings are formatted as a JSON message.
6. The .NET client receives and parses the JSON payload.
7. The application updates the temperature, pressure, and humidity labels in the user interface.

## Example Data Format

The ESP32 sends weather measurements as JSON. The exact property names may be adjusted as the project evolves.

```json
{
  "temperature": 24.6,
  "humidity": 58.2,
  "pressure": 1013.4
}
```

### Field Reference

| Field | Type | Unit | Description |
|---|---:|---|---|
| `temperature` | Number | °C or °F | Current temperature reported by the DHT11 |
| `humidity` | Number | `%` | Current relative humidity reported by the DHT11 |
| `pressure` | Number | hPa | Current atmospheric pressure reported by the BMP280 |

> **Note:** Document the selected temperature unit in the ESP32 firmware and keep the Windows client consistent with it.

## Network Configuration

The Windows client and ESP32 must be reachable from the same local network unless additional routing or remote-access infrastructure is configured.

Example WebSocket endpoint:

```text
ws://192.168.1.100:81/
```

Replace the IP address and port with the values configured in the ESP32 firmware.

### Finding the ESP32 IP Address

- Print the ESP32's assigned IP address to the Arduino serial monitor after Wi-Fi connection.
- Check the connected-device list in the router's administration interface.
- Assign a DHCP reservation in the router so the ESP32 receives a predictable local IP address.
- Add a display, mDNS hostname, or configuration page in a future revision.

## Suggested Repository Structure

```text
ESP32-Webserver-Weather-Station/
├── firmware/
│   ├── WeatherStation.ino
│   └── README.md
│
├── desktop-client/
│   ├── WeatherStationClient.sln
│   ├── WeatherStationClient/
│   └── README.md
│
├── docs/
│   ├── wiring-diagram.png
│   ├── screenshots/
│   └── protocol.md
│
├── LICENSE
└── README.md
```

> The directory layout above is a suggested structure and can be adapted to match the repository.

## Setup

### 1. Assemble the Prototype

Mount the ESP32, BMP280, and DHT11 on the breadboard. Connect power, ground, and signal pins according to the GPIO pins defined in the firmware.

Before powering the device, verify the following:

- All modules share a common ground.
- The BMP280 is configured for the correct communication interface, typically I²C.
- The BMP280 supply voltage matches the breakout board's requirements.
- The DHT11 data pin is connected to the GPIO configured in the firmware.
- No sensor signal is connected to a pin that could interfere with ESP32 boot mode.

### 2. Configure the ESP32 Firmware

Update the Wi-Fi credentials and other project settings before uploading:

```cpp
const char* ssid = "YOUR_WIFI_SSID";
const char* password = "YOUR_WIFI_PASSWORD";
```

Also confirm or configure:

- The DHT11 GPIO pin
- The BMP280 I²C address
- The WebSocket port
- The reporting interval
- The JSON field names
- The desired temperature unit

### 3. Upload Firmware

1. Connect the ESP32 to the computer over USB.
2. Open the firmware project in the Arduino IDE or another ESP32-compatible development environment.
3. Select the correct ESP32 board and serial port.
4. Install any required libraries.
5. Compile and upload the firmware.
6. Open the serial monitor and record the ESP32's assigned local IP address.

### 4. Run the Desktop Client

1. Open the `.sln` file in Visual Studio.
2. Confirm the project targets .NET 8.
3. Restore NuGet dependencies, if applicable.
4. Update the WebSocket URI in the application settings or source code.
5. Build and run the application.
6. Confirm that the temperature, pressure, and humidity labels update after the WebSocket connection is established.

## Required Libraries

The precise library list depends on the firmware implementation. Typical Arduino libraries for this project may include:

```text
WiFi
WebSockets
ArduinoJson
Adafruit BMP280 Library
Adafruit Unified Sensor
DHT Sensor Library
```

Example installation workflow:

1. Open the Arduino IDE Library Manager.
2. Search for the required library.
3. Install the library and any prompted dependencies.
4. Rebuild the firmware.

> Keep the final library names and versions documented here once the project is finalized. Pinning known-working versions makes future troubleshooting and rebuilds easier.

## Troubleshooting

| Issue | Possible Cause | Suggested Check |
|---|---|---|
| ESP32 does not connect to Wi-Fi | Incorrect credentials or unsupported network configuration | Recheck SSID/password and view serial-monitor output |
| Desktop client cannot connect | Incorrect IP address, port, endpoint, or firewall rule | Confirm the ESP32 IP, WebSocket URI, and Windows firewall settings |
| Sensor returns invalid values | Loose wiring, wrong GPIO, wrong I²C address, or missing library | Recheck wiring and print raw sensor values to the serial monitor |
| BMP280 is not detected | Incorrect I²C wiring or address | Run an I²C scanner and verify SDA/SCL connections |
| DHT11 returns `NaN` | Timing issue, incorrect pin, or unstable power | Add a read delay, verify the data pin, and check power/ground |
| Values do not update in the UI | JSON field mismatch or client parsing error | Log the incoming payload and compare it with the C# data model |
| WebSocket disconnects unexpectedly | Wi-Fi instability or unhandled connection state | Add reconnect handling and diagnostic logging on both sides |

## Limitations

- The DHT11 is suitable for basic prototype measurements but has limited accuracy and resolution compared with newer sensors.
- The station is intended for local-network use in its current form.
- The breadboard prototype is not weatherproof and should be kept indoors.
- Sensor readings may require calibration, smoothing, or validation before being used for long-term environmental monitoring.
- The device may receive a different IP address after reconnecting unless the router assigns a DHCP reservation.

## Planned Improvements

- [ ] Replace the DHT11 with a higher-accuracy temperature and humidity sensor
- [ ] Add automatic WebSocket reconnection in the Windows client
- [ ] Add a configurable settings screen for IP address, port, and update interval
- [ ] Add timestamped historical data logging
- [ ] Export readings to CSV or a local database
- [ ] Add rolling charts for temperature, humidity, and pressure
- [ ] Add error and connection-status indicators to the UI
- [ ] Add mDNS or a configuration webpage to avoid relying on a fixed IP address
- [ ] Add an enclosure suitable for long-term deployment
- [ ] Add OTA firmware updates
- [ ] Add authentication or encryption before exposing the device beyond a trusted local network

## Development Notes

This project combines several practical areas of development:

- Embedded programming on the ESP32
- Sensor integration and hardware prototyping
- Wi-Fi networking
- WebSocket-based client/server communication
- JSON serialization and deserialization
- C# desktop application development with .NET 8
- Basic user-interface data binding and update logic

## License

Choose and add a license before publishing or sharing the repository publicly.

For example:

```text
MIT License
```

## Author

**Dante**  
Post-graduation personal project — University of West Florida

## Future Documentation

Useful additions for later versions include:

- A completed wiring diagram with ESP32 GPIO pin numbers
- A photograph of the breadboard prototype
- Firmware installation instructions with exact library versions
- A screenshot of the Windows application
- The full WebSocket message contract
- Sample serial-monitor output
- A changelog documenting future hardware and software revisions
```
