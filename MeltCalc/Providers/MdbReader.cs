using System.IO;
using MeltCalc.Properties;

namespace MeltCalc.Providers
{
	public abstract class MdbReader
	{
		private readonly TableReader _reader;

		protected MdbReader(string path)
		{
			path = Path.Combine(System.Environment.CurrentDirectory, Settings.Default.DatabaseRelativePath, path);
			ValidatePath(path);
			_reader = new TableReader(path);
		}

		protected TableReader Reader
		{
			get { return _reader; }
		}

		private void ValidatePath(string path)
		{
			if (!File.Exists(path))
			{
				throw new FileNotFoundException(path);
			}
		}
	}
}