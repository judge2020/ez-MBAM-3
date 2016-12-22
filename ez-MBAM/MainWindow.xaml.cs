using System;
using System.Collections.Generic;
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

namespace ez_MBAM
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		readonly SoundPlayer _khgSound = new SoundPlayer(Assembly.GetExecutingAssembly().GetManifestResourceStream("ez_MBAM.khg.wav"));

		public MainWindow()
		{
			InitializeComponent();
			var maintimer = new Timer(1000) {AutoReset = true};
			maintimer.Elapsed += MaintimerOnElapsed;
			_khgSound.PlayLooping();
			_khgSound.Stop();

		}

		private void MaintimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
		{
			
		}

		private void checkBox_Checked(object sender, RoutedEventArgs e)
		{
			_khgSound.PlayLooping();	
			
		}

		private void checkBox_Unchecked(object sender, RoutedEventArgs e)
		{
			_khgSound.Stop();
		}
	}
}
