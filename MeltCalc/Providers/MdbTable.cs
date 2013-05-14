using System;
using System.Data.OleDb;
using System.IO;
using MeltCalc.Properties;

namespace MeltCalc.Providers
{
	public class MdbTable
	{
		private const string ConnStr = "Provider=Microsoft.JET.OLEDB.4.0;data source={0};";

		protected MdbTable(string file)
		{
			file = Path.Combine(Environment.CurrentDirectory, Settings.Default.DatabaseRelativePath, file);
			ValidatePath(file);
			MdbFile = file;
			SubKey = Path.GetFileName(MdbFile);
		}

		protected string SubKey { get; private set; }

		protected OleDbConnection CreateConnection()
		{
			return new OleDbConnection(String.Format(ConnStr, MdbFile));
		}

		private static void ValidatePath(string path)
		{
			if (!File.Exists(path))
			{
				throw new FileNotFoundException(path);
			}
		}

		private string MdbFile { get; set; }
	}
}