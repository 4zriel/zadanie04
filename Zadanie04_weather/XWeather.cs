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

		public XWeather(string QuerryURL)
		{
			XMLparser = XElement.Load(QuerryURL);
			XMLparser.Nodes();
			if (XMLparser.NodeType == System.Xml.XmlNodeType.Element)
			{
				switch (XMLparser.Name.ToString())
				{
					case "yeather:location":
						break;
				};
			}
		}
	}
}