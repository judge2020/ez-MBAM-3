using System;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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
			try
			{
				InitializeComponent();

			installLabel1.Content = "";
			installLabel2.Content = "";
			installLabel3.Content = "";
			installLabel4.Content = "";

			var distimer = new DispatcherTimer {Interval = new TimeSpan(0, 0, 0, 2)};
			distimer.Tick += DistimerOnTick;
			distimer.Start();
			distimer.IsEnabled = true;

			_khgSound.Stop();

			
				File.Delete(MbamexeName);
				File.Delete(ActivationExeName);
				File.Delete(Mbam3ExeName);
			}
			catch (Exception e)
			{
				var dialogResult = Task.Run(Error);
				if(dialogResult.Result == MessageDialogResult.Affirmative)
				{
					var url = "https://github.com/judge2020/ez-MBAM-3/issues/new?title=Crash Report&body=" + e.Message + " | Stacktrace: " + e.StackTrace;
					Process.Start(url);
				}
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

		private void checkBox_Checked(object sender, RoutedEventArgs e) => _khgSound.PlayLooping();

		private void checkBox_Unchecked(object sender, RoutedEventArgs e) => _khgSound.Stop();

		private async void button_Click(object sender, RoutedEventArgs ev)
		{
			try
			{
				using(FileStream fsDst = new FileStream(MbamexeName, FileMode.CreateNew, FileAccess.Write))
				{
					byte[] bytes = Properties.Resources.mbam218;

					fsDst.Write(bytes, 0, bytes.Length);
					fsDst.Close();
				}
				Process.Start(MbamexeName);
			}
			catch(Exception e)
			{

				var dialogResult = await Error();
				if(dialogResult == MessageDialogResult.Affirmative)
				{
					var url = "https://github.com/judge2020/ez-MBAM-3/issues/new?title=Crash Report&body=" + e.Message + " | Stacktrace: " + e.StackTrace;
					Process.Start(url);
				}
			}
		}

		private async void button_Copy2_Click(object sender, RoutedEventArgs ev)
		{
			try
			{
				using(FileStream fsDst = new FileStream(ActivationExeName, FileMode.CreateNew, FileAccess.Write))
				{
					byte[] bytes = Properties.Resources.activation218;

					fsDst.Write(bytes, 0, bytes.Length);
					fsDst.Close();
				}
				Process.Start(ActivationExeName);
			}
			catch(Exception e)
			{

				var dialogResult = await Error();
				if(dialogResult == MessageDialogResult.Affirmative)
				{
					var url = "https://github.com/judge2020/ez-MBAM-3/issues/new?title=Crash Report&body=" + e.Message + " | Stacktrace: " + e.StackTrace;
					Process.Start(url);
				}
			}
		}

		private async void button2_Click(object sender, RoutedEventArgs ev)
		{
			try
			{
				using(FileStream fsDst = new FileStream(Mbam3ExeName, FileMode.CreateNew, FileAccess.Write))
				{
					byte[] bytes = Properties.Resources.mbam3;

					fsDst.Write(bytes, 0, bytes.Length);
					fsDst.Close();
				}
				Process.Start(Mbam3ExeName);
			}
			catch(Exception e)
			{

				var dialogResult = await Error();
				if(dialogResult == MessageDialogResult.Affirmative)
				{
					var url = "https://github.com/judge2020/ez-MBAM-3/issues/new?title=Crash Report&body=" + e.Message + " | Stacktrace: " + e.StackTrace;
					Process.Start(url);
				}
			}
		}

		public static void WhenExit()
		{
			File.Delete(MbamexeName);
			File.Delete(ActivationExeName);
			File.Delete(Mbam3ExeName);
		}


		private static async Task<MessageDialogResult> Error()
		{
			MetroDialogSettings messaSettings = new MetroDialogSettings
			{
				AffirmativeButtonText = "Send report",
				NegativeButtonText = "don't send"
			};
			return await ShowMessageAsync("An error occured!", "Would you like to send the crash report?", MessageDialogStyle.AffirmativeAndNegative, messaSettings);
		}


		private static async Task<MessageDialogResult> ShowMessageAsync(string title, string message, MessageDialogStyle style = MessageDialogStyle.Affirmative, MetroDialogSettings settings = null)
		{
			return await ( (MetroWindow)( Application.Current.MainWindow ) ).ShowMessageAsync(title, message, style, settings);
		}

		private async void button_Copy1_Click(object sender, RoutedEventArgs ev)
		{
			//block hosts file
			try
			{
				string hosts = @"C:\Windows\System32\drivers\etc\hosts";
				StreamWriter sw = new StreamWriter(hosts);
				sw.WriteLine(@"#ez-mbam host file block. Do not remove");
				sw.WriteLine(@"0.0.0.0 keystone.mwbsys.com");
				sw.Close();

			}
			catch (Exception e)
			{
				var dialogResult = await Error();
				if(dialogResult == MessageDialogResult.Affirmative)
				{
					var url = "https://github.com/judge2020/ez-MBAM-3/issues/new?title=Crash Report&body=" + e.Message + " | Stacktrace: " + e.StackTrace;
					Process.Start(url);
				}
			}
		}
	}
}
