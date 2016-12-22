using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using Path = System.IO.Path;

namespace ez_MBAM
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		readonly SoundPlayer _khgSound = new SoundPlayer(Properties.Resources.khg);

		public MainWindow()
		{
			InitializeComponent();
			var maintimer = new Timer(1000) {AutoReset = true};
			maintimer.Elapsed += MaintimerOnElapsed;
			_khgSound.Stop();

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
			string tempExeName = Path.Combine(Path.GetTempPath(), "mbam218.exe");
			using(FileStream fsDst = new FileStream(tempExeName, FileMode.CreateNew, FileAccess.Write))
			{
				byte[] bytes = Properties.Resources.mbam218;

				fsDst.Write(bytes, 0, bytes.Length);
				fsDst.Close();
			}
			Process.Start(tempExeName);
		}

		private void button_Copy2_Click(object sender, RoutedEventArgs e)
		{
			string tempExeName = Path.Combine(Path.GetTempPath(), "activation218.exe");
			using(FileStream fsDst = new FileStream(tempExeName, FileMode.CreateNew, FileAccess.Write))
			{
				byte[] bytes = Properties.Resources.activation218;

				fsDst.Write(bytes, 0, bytes.Length);
				fsDst.Close();
			}
			Process.Start(tempExeName);
		}

		private void button2_Click(object sender, RoutedEventArgs e)
		{
			string tempExeName = Path.Combine(Path.GetTempPath(), "mbam3.exe");
			using(FileStream fsDst = new FileStream(tempExeName, FileMode.CreateNew, FileAccess.Write))
			{
				byte[] bytes = Properties.Resources.mbam3;

				fsDst.Write(bytes, 0, bytes.Length);
				fsDst.Close();
			}
			Process.Start(tempExeName);
		}
	}
}
