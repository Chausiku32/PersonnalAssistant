using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace PersonnalAssistant
{
    class WeatherForecast : Form1
    {

        string temp, condition;

        public WeatherForecast()
        {
            InitializeComponent();
        }

        private void WeatherForecast_Load(object sender, EventArgs e)
        {

        }

        public String GetWeather(String input)
        {
            try
            {
                String query = String.Format("https://query.yahooapis.com/v1/public/yql?q=select * from weather.forecast where woeid in (select woeid from geo.places(1) where text='Nairobi, state')&format=xml&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys");
                XmlDocument wData = new XmlDocument();
                wData.Load(query);

                XmlNamespaceManager manager = new XmlNamespaceManager(wData.NameTable);
                manager.AddNamespace("yweather", "http://xml.weather.yahoo.com/ns/rss/1.0");

                XmlNode channel = wData.SelectSingleNode("query").SelectSingleNode("results").SelectSingleNode("channel");
                XmlNodeList nodes = wData.SelectNodes("query/results/channel");
                try
                {
                    temp = channel.SelectSingleNode("item").SelectSingleNode("yweather:condition", manager).Attributes["temp"].Value;
                    condition = channel.SelectSingleNode("item").SelectSingleNode("yweather:condition", manager).Attributes["text"].Value;

                    if (input == "temp")
                    {
                        return temp;
                    }
                    if (input == "cond")
                    {
                        return condition;
                    }
                }
                catch
                {
                    return "Error Reciving data";
                }
                return "error";
            }
            catch (System.Xml.XmlException)
            {
                return "s";
            }

        }

    }
}
