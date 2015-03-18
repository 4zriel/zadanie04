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

		public String Region { get; set; }

		public String Country { get; set; }

		public String City { get; set; }

		public String WindChill { get; set; }

		public String WindDirection { get; set; }

		public String WindSpeed { get; set; }

		public String Humidity { get; set; }

		public String Visibility { get; set; }

		public String Pressure { get; set; }

		public String PressureIsRising { get; set; }

		public String Sunrise { get; set; }

		public String Sunset { get; set; }

		public String ConditionText { get; set; }

		public String ConditionTemp { get; set; }

		public enum pressureRising
		{
			Steady = 0,
			Rising = 1,
			Falling = 2
		}

		public XWeather(String QuerryURL)
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
								this.Region = (String.IsNullOrWhiteSpace(reader.GetAttribute("region").ToString())) ? "Region not received" : reader.GetAttribute("region");
								break;

							case "yweather:wind":
								this.WindChill = reader.GetAttribute("chill");
								this.WindDirection = reader.GetAttribute("direction");
								this.WindSpeed = reader.GetAttribute("speed");
								break;

							case "yweather:atmosphere":
								this.Humidity = reader.GetAttribute("humidity");
								this.Visibility = reader.GetAttribute("visibility");
								this.Pressure = reader.GetAttribute("pressure");
								int tmp = Convert.ToInt32(reader.GetAttribute("rising"));
								this.PressureIsRising = ((pressureRising)tmp).ToString();
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