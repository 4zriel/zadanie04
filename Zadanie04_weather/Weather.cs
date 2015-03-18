﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Zadanie04_weather
{
	public class Weather
	{
		public String BaseURL { get; set; }

		public String BaseRSSurl { get; set; }

		public String Woeid { get; set; }

		public char Units { get; set; }

		public String Location { get; set; }

		public String LocationQuerry { get; set; }

		public XWeather XMLResponse { get; set; }

		public Weather(string loc, char units = 'c')
		{
			BaseURL = "https://query.yahooapis.com/v1/public/yql?";
			BaseRSSurl = "http://weather.yahooapis.com/forecastrss";
			Units = units;
			Location = loc;
			LocationQuerry = string.Format("https://query.yahooapis.com/v1/public/yql?q=select%20woeid%20from%20geo.places%20where%20text%3D%22{0}%22&diagnostics=true&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys", loc);
			this.getWOEID(LocationQuerry);
			XMLResponse = new XWeather(String.Format(BaseRSSurl + "?w={0}&u={1}", this.Woeid, this.Units));
		}

		public void Refresh()
		{
			XMLResponse = new XWeather(String.Format(BaseRSSurl + "?w={0}&u={1}", this.Woeid, this.Units));
		}

		private void getWOEID(String LocQuerry)
		{
			try
			{
				WebRequest request = WebRequest.Create(LocQuerry);
				HttpWebResponse response = (HttpWebResponse)request.GetResponse();
				if (response.StatusCode == HttpStatusCode.OK)
				{
					Stream xmlStream = response.GetResponseStream();
					String tmpResponse = String.Empty;
					using (xmlStream)
					{
						StreamReader reader = new StreamReader(xmlStream);
						tmpResponse = reader.ReadLine();
						while (!reader.EndOfStream)
						{
							tmpResponse = reader.ReadLine();
							if (tmpResponse.Contains("<woeid>"))
								break;
						}
						reader.Close();
						xmlStream.Close();
						response.Close();
						XElement test = XElement.Parse(tmpResponse);
						this.Woeid = test.Element("results").Value;
						//this.Woeid = this.Woeid.Substring(0, 5);
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}