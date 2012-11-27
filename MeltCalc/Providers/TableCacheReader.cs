using System.Data;

namespace MeltCalc.Providers
{
	public class TableCacheReader : TableReader
	{
		public TableCacheReader(string file) : base(file)
		{
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
	}
}