using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Timers;
using System.Windows;
using System.Windows.Threading;
using Path = System.IO.Path;

namespace ez_MBAM
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		readonly SoundPlayer _khgSound = new SoundPlayer(Properties.Resources.khg);
		static readonly string MbamexeName = Path.Combine(Path.GetTempPath(), "mbam218.exe");
		static readonly string ActivationExeName = Path.Combine(Path.GetTempPath(), "activation218.exe");
		static readonly string Mbam3ExeName = Path.Combine(Path.GetTempPath(), "mbam3.exe");
		public MainWindow()
		{
			InitializeComponent();

			installLabel1.Content = "";
			installLabel2.Content = "";
			installLabel3.Content = "";
			installLabel4.Content = "";

			var distimer = new DispatcherTimer();
			distimer.Interval = new TimeSpan(0, 0, 0, 2);
			distimer.Tick += DistimerOnTick;
			distimer.Start();
			distimer.IsEnabled = true;

			_khgSound.Stop();

			try
			{
				File.Delete(MbamexeName);
				File.Delete(ActivationExeName);
				File.Delete(Mbam3ExeName);
			}
			catch (Exception)
			{
				// ignored
			}
		}

		private void DistimerOnTick(object sender, EventArgs eventArgs)
		{
			if(Directory.Exists(@"C:\Program Files\Malwarebytes"))
			{
				Status_label.Content = "Found MBAM v3!";
				installLabel1.Content = "";
				installLabel2.Content = "";
				installLabel3.Content = "";
				installLabel4.Content = "Recommended";

			}
			if(Directory.Exists(@"C:\Program Files (x86)\Malwarebytes Anti-Malware"))
			{
				Status_label.Content = "Found MBAM v2!";
				installLabel1.Content = "";
				installLabel2.Content = "Recommended";
				installLabel3.Content = "Recommended";
				installLabel4.Content = "";
			}
			else
			{
				Status_label.Content = "Could not find an installation of MBAM.";
				installLabel1.Content = "Recommended";
				installLabel2.Content = "";
				installLabel3.Content = "";
				installLabel4.Content = "";
			}
		} 

		private void checkBox_Checked(object sender, RoutedEventArgs e)
		{
			_khgSound.PlayLooping();	

			
		}

		private void checkBox_Unchecked(object sender, RoutedEventArgs e)
		{
			_khgSound.Stop();
		}

		private void button_Click(object sender, RoutedEventArgs e)
		{
			using(FileStream fsDst = new FileStream(MbamexeName, FileMode.CreateNew, FileAccess.Write))
			{
				byte[] bytes = Properties.Resources.mbam218;

				fsDst.Write(bytes, 0, bytes.Length);
				fsDst.Close();
			}
			Process.Start(MbamexeName);
		}

		private void button_Copy2_Click(object sender, RoutedEventArgs e)
		{
			using(FileStream fsDst = new FileStream(ActivationExeName, FileMode.CreateNew, FileAccess.Write))
			{
				byte[] bytes = Properties.Resources.activation218;

				fsDst.Write(bytes, 0, bytes.Length);
				fsDst.Close();
			}
			Process.Start(ActivationExeName);
		}

		private void button2_Click(object sender, RoutedEventArgs e)
		{
			using(FileStream fsDst = new FileStream(Mbam3ExeName, FileMode.CreateNew, FileAccess.Write))
			{
				byte[] bytes = Properties.Resources.mbam3;

				fsDst.Write(bytes, 0, bytes.Length);
				fsDst.Close();
			}
			Process.Start(Mbam3ExeName);
		}

		public static void WhenExit()
		{
			File.Delete(MbamexeName);
			File.Delete(ActivationExeName);
			File.Delete(Mbam3ExeName);
		}
	}
}
