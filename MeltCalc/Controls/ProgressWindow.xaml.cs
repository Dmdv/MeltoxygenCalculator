using System;
using System.ComponentModel;
using System.Threading.Tasks;
using MeltCalc.Controls.Internals;

namespace MeltCalc.Controls
{
	/// <summary>
	/// Progress windows, which shows the progress of long running tasks.
	/// </summary>
	public partial class ProgressWindow : IUpdater
	{
		public ProgressWindow()
		{
			InitializeComponent();
			Topmost = true;
			ShowInTaskbar = false;
		}

		void IUpdater.ShowProgress(object value)
		{
			_progress.Value = Convert.ToDouble(value);
		}

		public double? MaximumProgress { get; set; }

		public void Run(IRunner runner)
		{
			_runner = runner;
			_progress.Minimum = 0;
			_progress.Maximum = MaximumProgress.HasValue ? MaximumProgress.Value : 0.0;
			_progress.IsIndeterminate = !MaximumProgress.HasValue;
			_progress.Value = 0;

			var asyncOperation = AsyncOperationManager.CreateOperation(null);

			Task.Factory.StartNew(InternalRun, asyncOperation);

			ShowDialog();
			Close();
		}

		private void Close(object state)
		{
			Close();
		}

		private void InternalRun(object state)
		{
			var asyncOperation = (AsyncOperation) state;

			_runner.Run(x => asyncOperation.Post(((IUpdater) this).ShowProgress, x));

			asyncOperation.Post(Close, null);
			asyncOperation.OperationCompleted();
		}

		private IRunner _runner;
	}
}