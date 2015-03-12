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

		public MainWindow()
		{
			InitializeComponent();
			InitializeApp();

			Weather testowy = new Weather("Katowice");
		}

		public void InitializeApp()
		{
			TrayIcon = new NotifyIcon();
			TrayIcon.Icon = new Icon("Resources/main.ico");
			TrayIcon.Visible = true;
		}

		protected override void OnStateChanged(EventArgs e)
		{
			if (WindowState == WindowState.Minimized)
				this.Hide();

			base.OnStateChanged(e);
		}
	}
}