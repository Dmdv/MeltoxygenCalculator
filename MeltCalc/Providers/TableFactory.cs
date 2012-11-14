using System;
using System.Data;
using System.Data.OleDb;

namespace MeltCalc.Providers
{
	public class TableFactory
	{
		private const string ConnStr = "Provider=Microsoft.JET.OLEDB.4.0;data source={0};";
		private readonly string _file;
		private readonly string _table;

		public TableFactory(string file, string table)
		{
			_file = file;
			_table = table;
		}

		public DataTable ExecuteTable()
		{
			using (var conn = new OleDbConnection(string.Format(ConnStr, _file)))
			{
				var cmd = new OleDbCommand(string.Format("select * from {0}", _table)) {Connection = conn};
				conn.Open();

				using (var oleDbDataReader = cmd.ExecuteReader())
				{
					if (oleDbDataReader == null)
					{
						throw new ApplicationException(string.Format("Reader is null for 'select * from {0}'", _table));
					}

					var dt = new DataTable();
					dt.Load(oleDbDataReader);
					return dt;
				}
			}
		}
	}
}