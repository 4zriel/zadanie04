using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Zadanie04_weather
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public NotifyIcon TrayIcon { get; set; }

		public string BaseURL { get; set; }

		public string YahooQuery { get; set; }

		public WebRequest MainRequest { get; set; }

		public HttpWebResponse MainResponse { get; set; }

		public MainWindow()
		{
			InitializeComponent();
			InitializeApp();
		}

		public void InitializeApp()
		{
			TrayIcon = new NotifyIcon();
			TrayIcon.Icon = new Icon("test.ico");
			TrayIcon.Visible = true;
			BaseURL = "https://query.yahooapis.com/v1/public/yql?";
		}

		protected override void OnStateChanged(EventArgs e)
		{
			if (WindowState == WindowState.Minimized)
				this.Hide();

			base.OnStateChanged(e);
		}
	}
}

//https://query.yahooapis.com/v1/public/yql?q=select%20item.condition.text%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places(1)%20where%20text%3D%22dallas%2C%20tx%22)&format=xml&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys