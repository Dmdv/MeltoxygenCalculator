namespace MeltCalc.Providers
{
	public abstract class MdbReader
	{
		public TableCacheReader Reader
		{
			get { return _cacheReader; }
		}

		public int RowCount(string table)
		{
			return Reader.FetchTable(table).Rows.Count;
		}

		protected MdbReader(string path)
		{
			_cacheReader = new TableCacheReader(path);
		}

		private readonly TableCacheReader _cacheReader;
	}
}