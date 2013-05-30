using System;
using System.ComponentModel;
using System.Threading.Tasks;
using MeltCalc.Controls.Internals;

namespace MeltCalc.Controls
{
	/// <summary>
	/// Interaction logic for ProgressWindow.xaml
	/// </summary>
	public partial class ProgressWindow : IUpdater
	{
		private IRunner _runner;

		public ProgressWindow()
		{
			InitializeComponent();
			Topmost = true;
			ShowInTaskbar = false;
		}

		public void Run(IRunner runner, int iterations)
		{
			_runner = runner;
			_progress.Minimum = 0;
			_progress.Maximum = iterations;
			_progress.Value = 0;

			var asyncOperation = AsyncOperationManager.CreateOperation(null);
			Task.Factory.StartNew(InternalRun, asyncOperation);

			ShowDialog();
			Close();
		}

		public void DoProgress(object value)
		{
			_progress.Value = Convert.ToDouble(value);
		}

		public void Close(object state)
		{
			Close();
		}

		private void InternalRun(object state)
		{
			var asyncOperation = (AsyncOperation)state;

			_runner.Run(x => asyncOperation.Post(DoProgress, x));
			asyncOperation.Post(Close, null);
			asyncOperation.OperationCompleted();
		}
	}
}