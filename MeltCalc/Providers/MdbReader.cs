namespace MeltCalc.Providers
{
	public abstract class MdbReader
	{
		private readonly TableCacheReader _cacheReader;

		protected MdbReader(string path)
		{
			_cacheReader = new TableCacheReader(path);
		}

		public TableCacheReader Reader
		{
			get { return _cacheReader; }
		}

		public int RowCount(string table)
		{
			return Reader.FetchTable(table).Rows.Count;
		}
	}
}