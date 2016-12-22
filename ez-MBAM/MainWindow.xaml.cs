using System;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Timers;
using System.Windows;
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
			var maintimer = new Timer(1000) {AutoReset = true};
			maintimer.Elapsed += MaintimerOnElapsed;
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

		private void MaintimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
		{
			if (Directory.Exists(@"C:\Program Files\Malwarebytes"))
			{
				Status_label.Content = "Malwarebyted 3 Found!";
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
