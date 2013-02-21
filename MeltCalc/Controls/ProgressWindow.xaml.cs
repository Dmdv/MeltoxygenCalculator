using System.Windows;

namespace MeltCalc.Controls
{
	/// <summary>
	/// Interaction logic for ProgressWindow.xaml
	/// </summary>
	public partial class ProgressWindow
	{
		public ProgressWindow()
		{
			InitializeComponent();
			Topmost = true;
			ShowInTaskbar = false;
		}
	}
}
