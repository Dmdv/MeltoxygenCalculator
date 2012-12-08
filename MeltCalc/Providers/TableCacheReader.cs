using System;
using System.Linq;
using System.Data;

namespace MeltCalc.Providers
{
	public class TableCacheReader : TableReader
	{
		public TableCacheReader(string file) : base(file)
		{
		}

		public EnumerableRowCollection<T> SelectColumnRange<T>(string table, string column)
		{
			var dataTable = FetchTableSafe(table);
			return dataTable.AsEnumerable().Select(row => row.Field<T>(column));
		}

		public string[] SelectRowRange(string table, int index)
		{
			var dataTable = FetchTableSafe(table);
			return dataTable.Rows[index].ItemArray.Select(item => item.ToString()).ToArray();
		}

		public override DataTable FetchTable(string table)
		{
			var datatable = TableCache.Get(table);
			if (datatable == null)
			{
				datatable = base.FetchTable(table);
				TableCache.Put(table, datatable);
			}
			return datatable;
		}

		private DataTable FetchTableSafe(string table)
		{
			var dataTable = FetchTable(table);
			if (dataTable == null)
			{
				throw new Exception(string.Format("Table '{0}' was  not found", table));
			}
			return dataTable;
		}
	}
}