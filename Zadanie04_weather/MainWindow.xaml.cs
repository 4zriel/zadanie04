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
		public bool isStarted { get; set; }

		public NotifyIcon TrayIcon { get; set; }

		public Timer appTimer { get; set; }

		public int TimeInterval { get; set; }

		public Weather appWeather { get; set; }

		public String city { get; set; }

		public MainWindow()
		{
			InitializeComponent();
			InitializeApp();
		}

		public void InitializeApp()
		{
			try
			{
				InitializeTray();
				timeIntervalText.Text = String.Empty;
				InitializeTimer();
				this.city = String.Empty;
				this.isStarted = false;
				this.RefreshWeather();
			}
			catch (Exception)
			{
				throw;
			}
		}

		private void InitializeTimer()
		{
			appTimer = new Timer();
			var tmp = this.converter();
			if (tmp)
				TimeInterval = Convert.ToInt32(timeIntervalText.ToString());
			else
				TimeInterval = 15;
			appTimer.Interval = TimeInterval * 60000;
			appTimer.Tick += new EventHandler(timerHandler);
			appTimer.Start();
		}

		private bool converter()
		{
			try
			{
				Convert.ToInt32(timeIntervalText.ToString());
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		private void timerHandler(object sender, EventArgs e)
		{
			this.RefreshWeather();
		}

		private void RefreshWeather()
		{
			var tmp = cityText.Text.ToString();
			if (isStarted)
			{
				if (tmp != this.city)
				{
					this.city = cityText.Text.ToString();
					if (String.IsNullOrWhiteSpace(this.city))
					{
						MessageBoxResult result = System.Windows.MessageBox.Show("Please eneter city name", "Error - no city provided", MessageBoxButton.OK, MessageBoxImage.Stop);
					}
					else
					{
						appWeather = new Weather(cityText.Text.ToString());
						this.appWeather.Refresh();
						this.buildWeatherForms();
						InitializeTimer();
					}
				}
			}
		}

		private void buildWeatherForms()
		{
			try
			{
				this.cityText2.Text = this.appWeather.XMLResponse.City;
				this.conditionTempText.Text = this.appWeather.XMLResponse.ConditionTemp + " C";
				this.conditionText.Text = this.appWeather.XMLResponse.ConditionText;
				this.countryText.Text = this.appWeather.XMLResponse.Country;
				this.humidityText.Text = this.appWeather.XMLResponse.Humidity + " %";
				this.pressureText.Text = this.appWeather.XMLResponse.Pressure + " mb";
				this.pressureIsRisingText.Text = this.appWeather.XMLResponse.PressureIsRising;
				this.regionText.Text = this.appWeather.XMLResponse.Region;
				this.sunriseText.Text = this.appWeather.XMLResponse.Sunrise;
				this.sunsetText.Text = this.appWeather.XMLResponse.Sunset;
				this.visibilityText.Text = this.appWeather.XMLResponse.Visibility + " km";
				this.windChillText.Text = this.appWeather.XMLResponse.WindChill + " C";
				this.windSpeedText.Text = this.appWeather.XMLResponse.WindSpeed + " kph";
				this.windSpeedText.Text += " " + this.appWeather.XMLResponse.WindDirection;
			}
			catch (Exception)
			{
				throw;
			}
		}

		private void InitializeTray()
		{
			TrayIcon = new NotifyIcon();
			TrayIcon.Icon = new Icon("Resources/main.ico");
			TrayIcon.Visible = true;
			TrayIcon.MouseDown += new System.Windows.Forms.MouseEventHandler(MouseDown_handler);
			TrayIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(MouseDoubleClicker);
		}

		private void MouseDoubleClicker(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if ((e.Button == MouseButtons.Left) && (e.Clicks == 2))
			{
				this.Show();
				if (this.WindowState == WindowState.Minimized)
					this.WindowState = WindowState.Normal;
			}
		}

		private void MouseDown_handler(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				System.Windows.Controls.ContextMenu menu = (System.Windows.Controls.ContextMenu)this.FindResource("NotifierContextMenu");
				menu.IsOpen = true;
			}
		}

		protected override void OnStateChanged(EventArgs e)
		{
			if (WindowState == WindowState.Minimized)
				this.Hide();
			base.OnStateChanged(e);
		}

		private void Menu_Refresh(object sender, RoutedEventArgs e)
		{
			this.isStarted = true;
			this.RefreshWeather();
		}

		private void Menu_Open(object sender, RoutedEventArgs e)
		{
			this.isStarted = true;
			this.Show();
			if (this.WindowState == WindowState.Minimized)
				this.WindowState = WindowState.Normal;
		}

		private void Menu_Close(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void searchButton_click(object sender, RoutedEventArgs e)
		{
			this.isStarted = true;
			this.RefreshWeather();
		}
	}
}