using System.IO;

namespace MeltCalc.Providers
{
	public class TableKey
	{
		public TableKey(string table, string file)
		{
			Table = table;
			_file = Path.GetFileName(file);
		}

		public string Table { get; private set; }

		public string Value
		{
			get { return string.Format("{0}|{1}", Table, _file.ToLower()); }
		}

		public override string ToString()
		{
			return Value;
		}

		private readonly string _file;
	}
}