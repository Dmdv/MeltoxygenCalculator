using System;
using System.Data;
using System.Data.OleDb;

namespace MeltCalc.Providers
{
	public class TableReader : MdbTable
	{
		public TableReader(string file) : 
			base(file)
		{
		}

		public virtual DataTable FetchTable(string table)
		{
			using (var conn = CreateConnection())
			{
				conn.Open();

				using (var cmd = new OleDbCommand(string.Format("select * from {0}", table)) {Connection = conn})
				{
					using (var oleDbDataReader = cmd.ExecuteReader())
					{
						if (oleDbDataReader == null)
						{
							throw new ApplicationException(string.Format("Reader is null for 'select * from {0}'", table));
						}

						using (var dt = new DataTable())
						{
							dt.Load(oleDbDataReader);
							return dt;
						}
					}
				}
			}
		}
	}
}