using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Zadanie04_weather
{
	public class XWeather
	{
		public XElement XMLparser { get; set; }

		public string Region { get; set; }

		public string Country { get; set; }

		public string City { get; set; }

		public string WindChill { get; set; }

		public string WindDirection { get; set; }

		public string WindSpeed { get; set; }

		public string Humidity { get; set; }

		public string Visibility { get; set; }

		public string Pressure { get; set; }

		public string PressureIsRising { get; set; }

		public string Sunrise { get; set; }

		public string Sunset { get; set; }

		public string ConditionText { get; set; }

		public string ConditionTemp { get; set; }

		public XWeather(string QuerryURL)
		{
			//XMLparser = XElement.Load(QuerryURL);
			XDocument XMLparser = XDocument.Load(QuerryURL);
			try
			{
				var reader = XMLparser.CreateReader();
				//XMLparser.Nodes();
				while (reader.Read())
				{
					if (reader.NodeType == System.Xml.XmlNodeType.Element)
					{
						switch (reader.Name.ToString())
						{
							case "yweather:location":
								this.City = reader.GetAttribute("city");
								this.Country = reader.GetAttribute("country");
								this.Region = (reader.GetAttribute("region").ToString() == string.Empty) ? "Region not received" : reader.GetAttribute("region");
								break;

							case "yweather:wind":
								this.WindChill = reader.GetAttribute("chill");
								this.WindDirection = reader.GetAttribute("direction");
								this.WindSpeed = reader.GetAttribute("speed");
								break;

							case "yweather:atmosphere":
								this.Humidity = reader.GetAttribute("humidity");
								this.Visibility = reader.GetAttribute("humidity");
								this.Pressure = reader.GetAttribute("visibility");
								this.PressureIsRising = reader.GetAttribute("rising");

								break;

							case "yweather:astronomy":
								this.Sunrise = reader.GetAttribute("sunrise");
								this.Sunset = reader.GetAttribute("sunset");
								break;

							case "yweather:condition":
								this.ConditionText = reader.GetAttribute("text");
								this.ConditionTemp = reader.GetAttribute("temp");
								break;

							case "yweather:forecast":
								break;
						};
					}
					if (reader.NodeType == System.Xml.XmlNodeType.Comment)
						this.Country = reader.Value;
				}
				reader.Dispose();
				reader.Close();
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}