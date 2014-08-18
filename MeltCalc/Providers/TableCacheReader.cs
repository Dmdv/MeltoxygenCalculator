using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MeltCalc.Providers
{
	public class TableCacheReader : TableReader
	{
		public TableCacheReader(string file)
			: base(file)
		{
		}

		public override DataTable FetchTable(string table)
		{
			var datatable = TableCache.Get(new TableKey(table, SubKey));
			if (datatable == null)
			{
				datatable = base.FetchTable(table);
				TableCache.Put(new TableKey(table, SubKey), datatable);
			}
			return datatable;
		}

		public IEnumerable<string[]> SelectAllRows(string table)
		{
			return
				FetchTableSafe(table).Rows
					.Cast<DataRow>()
					.Select(row => row.ItemArray.Select(item => item.ToString()).ToArray())
					.ToList();
		}

		public EnumerableRowCollection<T> SelectColumnRange<T>(string table, string column)
		{
			var dataTable = FetchTableSafe(table);
			return dataTable.AsEnumerable().Select(row => row.Field<T>(column));
		}

		public Dictionary<string, string> SelectRowDictionary(string table, int index)
		{
			var dataTable = FetchTableSafe(table);
			var dataRow = dataTable.Rows[index];
			return
				dataTable.Columns.OfType<DataColumn>()
					.ToDictionary(column => column.ColumnName,
						column => dataRow[column.ColumnName].ToString());
		}

		public string[] SelectRowRange(string table, int rowIndex)
		{
			var dataTable = FetchTableSafe(table);
			return dataTable.Rows[rowIndex].ItemArray.Select(item => item.ToString()).ToArray();
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