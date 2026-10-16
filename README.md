# RainPredictor

**Rainfall Prediction for Australia with Azure Machine Learning**

A Windows desktop app that predicts whether it will rain today at an Australian weather station. You paste in one row of the current day's observations from the Australian Bureau of Meteorology (BOM), and the app sends it to a classification model deployed as a real-time endpoint on Azure Machine Learning. It then shows the prediction and its probability.

> RUAP course project. Author: Filip Krpes.

## Features

- One-click link to the BOM daily weather observations table
- Parsing of a pasted BOM table row into the model's 18 input features
- Real-time scoring through an Azure ML REST endpoint (JSON over HTTPS, bearer-token authentication)
- Result shown as a yes/no prediction with its probability
- Built-in tooltip with usage instructions

## Model inputs

| Group | Features |
|---|---|
| Daily | MinTemp, MaxTemp, Evaporation, Sunshine, WindGustDir, WindGustSpeed |
| 9 am | Temp9am, Humidity9am, Cloud9am, WindDir9am, WindSpeed9am, Pressure9am |
| 3 pm | Temp3pm, Humidity3pm, Cloud3pm, WindDir3pm, WindSpeed3pm, Pressure3pm |

The `Rain` and `LocalTime` columns from the BOM table are dropped before scoring.

## Tech stack

| Area | Technology |
|---|---|
| Desktop app | C#, .NET 8, Windows Forms |
| Machine learning | Azure Machine Learning (Designer pipeline, real-time endpoint) |
| Hosting | Azure Container Instances (West Europe) |
| Communication | REST, JSON (System.Text.Json), HttpClient |
| Data source | Australian Bureau of Meteorology daily weather observations |

## Architecture

```
BOM website (daily observations table)
     │  user copies one row
     ▼
RainPredictorApp (WinForms)
     │  parse row → WeatherData (18 features)
     │  POST /score, JSON + Bearer key
     ▼
Azure ML real-time endpoint ──── trained classifier
     │
     └──── Scored Labels (rain / no rain) + Scored Probabilities
```

