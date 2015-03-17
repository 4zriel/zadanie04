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

		public Timer appTimer { get; set; }

		public int TimeInterval { get; set; }

		public Weather appWeather { get; set; }

		public MainWindow()
		{
			InitializeComponent();
			InitializeApp();

			Weather testowy = new Weather("Katowice");
			Weather testowy2 = new Weather("Dąbrowa Górnicza");
			Weather testowy3 = new Weather("grand canyon, az");
			cityText.Text = appWeather.XMLResponse.City + " " + appWeather.XMLResponse.ConditionTemp + "C " + appWeather.XMLResponse.ConditionText + " " + appWeather.XMLResponse.Country;
		}

		public void InitializeApp()
		{
			InitializeTray();
			appWeather = new Weather("katowice");
			InitializeTimer();
		}

		private void InitializeTimer()
		{
			appTimer = new Timer();
			TimeInterval = 1;
			appTimer.Interval = TimeInterval * 60000;
			appTimer.Tick += new EventHandler(timerHandler);
			appTimer.Start();
		}

		private void timerHandler(object sender, EventArgs e)
		{
			this.appWeather.Refresh();
			cityText.Text = appWeather.XMLResponse.City + " " + appWeather.XMLResponse.ConditionTemp + "C " + appWeather.XMLResponse.ConditionText + " " + appWeather.XMLResponse.Country;
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
			this.appWeather.Refresh();
			cityText.Text = appWeather.XMLResponse.City + " " + appWeather.XMLResponse.ConditionTemp + "C " + appWeather.XMLResponse.ConditionText + " " + appWeather.XMLResponse.Country;
		}

		private void Menu_Open(object sender, RoutedEventArgs e)
		{
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
		}
	}
}