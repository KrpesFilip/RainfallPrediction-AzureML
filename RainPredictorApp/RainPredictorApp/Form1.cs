using System.Drawing.Drawing2D;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Globalization;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace RainPredictorApp
{


    public partial class Form1 : Form
    {
        private static readonly HttpClient client = new HttpClient();

        public Form1()
        {
            InitializeComponent();

        }

        public WeatherData ParseWeatherLine(string line)
        {
            // Split by any whitespace and ignore empty entries
            List<string> parts = new List<string>();
            foreach (string p in line.Split((char[])null, StringSplitOptions.RemoveEmptyEntries))
            {
                if (!string.IsNullOrWhiteSpace(p))
                    parts.Add(p);
            }

            // Remove unwanted fields: Rain (index 4), LocalTime (index 9)
            parts.RemoveAt(9); // LocalTime
            parts.RemoveAt(4); // Rain

            // Map remaining fields to WeatherData
            return new WeatherData
            {
                MinTemp = double.Parse(parts[2], CultureInfo.InvariantCulture),
                MaxTemp = double.Parse(parts[3], CultureInfo.InvariantCulture),
                Evaporation = double.Parse(parts[4], CultureInfo.InvariantCulture),
                Sunshine = double.Parse(parts[5], CultureInfo.InvariantCulture),

                WindGustDir = parts[6],
                WindGustSpeed = double.Parse(parts[7], CultureInfo.InvariantCulture),

                Temp9am = double.Parse(parts[8], CultureInfo.InvariantCulture),
                Humidity9am = double.Parse(parts[9], CultureInfo.InvariantCulture),
                Cloud9am = double.Parse(parts[10], CultureInfo.InvariantCulture),
                WindDir9am = parts[11],
                WindSpeed9am = double.Parse(parts[12], CultureInfo.InvariantCulture),
                Pressure9am = double.Parse(parts[13], CultureInfo.InvariantCulture),

                Temp3pm = double.Parse(parts[14], CultureInfo.InvariantCulture),
                Humidity3pm = double.Parse(parts[15], CultureInfo.InvariantCulture),
                Cloud3pm = double.Parse(parts[16], CultureInfo.InvariantCulture),
                WindDir3pm = parts[17],
                WindSpeed3pm = double.Parse(parts[18], CultureInfo.InvariantCulture),
                Pressure3pm = double.Parse(parts[19], CultureInfo.InvariantCulture)
            };
        }

        



        private void Form1_Load(object sender, EventArgs e)
        {

            string line = "1\tFr\t8.5\t13.0\t9.6\t3.0\t0.0\tSSW\t39\t15:15\t8.8\t93\t7\tWNW\t20\t1023.7\t12.9\t86\t7\tSW\t15\t1020.5";
            


            GraphicsPath gp = new GraphicsPath();
            gp.AddEllipse(0, 0, label3.Width, label3.Height);
            label3.Region = new Region(gp);


            label3.TextAlign = ContentAlignment.MiddleCenter;
            label3.ForeColor = Color.White;
            label3.BackColor = Color.DarkBlue;
            label3.Font = new Font("Segoe UI", 12, FontStyle.Bold);


            toolTip1.SetToolTip(label3, "Press get current weather data, \ncopy one line from the weather measurements table \nand paste it into the input field then press start.");
            toolTip1.AutoPopDelay = 9000;
        }

        private void button1_Click(object sender, EventArgs e)
        {



            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = "https://www.bom.gov.au/climate/dwo/IDCJDW2124.latest.shtml",
                UseShellExecute = true
            });
        }

        private async void button2_ClickAsync(object sender, EventArgs e)
        {

            string url = "http://f7037e7f-5311-419c-be7f-1cc5a80b8886.westeurope.azurecontainer.io/score";
            string apiKey = "fCsbNGsKTlyerg0t8EaeVRu3nPaYZlbp";

            if (string.IsNullOrEmpty(apiKey))
            {
                MessageBox.Show("Please provide an API key.");
                return;
            }

            ////////////////////////////////////////

            string line = textBox1.Text; // take user input
            WeatherData wd = ParseWeatherLine(line);
            var requestBody = new
            {
                Inputs = new
                {
                    input1 = new[] { wd }
                }
            };

            string jsonBody = JsonSerializer.Serialize(requestBody);


            /////////////////////////////////////////////////           

            try
            {
                using (var client = new HttpClient()) 
                using (var request = new HttpRequestMessage(HttpMethod.Post, url))
                {
                    request.Headers.Add("Authorization", "Bearer " + apiKey);
                    request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    var response = await client.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();

                        var root = JsonNode.Parse(responseBody);
                        var output = root?["Results"]?["WebServiceOutput0"]?[0];

                        bool willRain = output?["Scored Labels"]?.GetValue<bool>() ?? false;
                        double probability = output?["Scored Probabilities"]?.GetValue<double>() ?? 0.0;

                        string message = willRain
                            ? $"There will be rain today ({probability * 100:F1}% chance)."
                            : $"There won’t be rain today ({probability * 100:F1}% chance).";

                        MessageBox.Show(message, "Prediction Result");


                        //MessageBox.Show("Prediction result:\n" + responseBody, "Azure ML Result");
                    }
                    else
                    {
                        string errorBody = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Error calling endpoint:\n{response.StatusCode}\n{errorBody}", "Error");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error calling endpoint:\n" + ex.Message, "Error");
            }

            textBox1.Text = "";

        }
    }

    public class WeatherData
    {
        public double MinTemp { get; set; }
        public double MaxTemp { get; set; }
        public double Evaporation { get; set; }
        public double Sunshine { get; set; }

        public string WindGustDir { get; set; }
        public double WindGustSpeed { get; set; }

        public double Temp9am { get; set; }
        public double Humidity9am { get; set; }
        public double Cloud9am { get; set; }
        public string WindDir9am { get; set; }
        public double WindSpeed9am { get; set; }
        public double Pressure9am { get; set; }

        public double Temp3pm { get; set; }
        public double Humidity3pm { get; set; }
        public double Cloud3pm { get; set; }
        public string WindDir3pm { get; set; }
        public double WindSpeed3pm { get; set; }
        public double Pressure3pm { get; set; }
    }

}
