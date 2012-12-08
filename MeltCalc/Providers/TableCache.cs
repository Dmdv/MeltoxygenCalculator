using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using MeltCalc.Properties;

namespace MeltCalc.Providers
{
	/// <summary>
	/// Потокобезопасный кеш.
	/// </summary>
	public static class TableCache
	{
		private static readonly object _lock = new object();
		private static readonly Dictionary<string, DataTable> _cache = new Dictionary<string, DataTable>();

		public static void Refresh()
		{
			var path = Path.Combine(Environment.CurrentDirectory, Settings.Default.DatabaseRelativePath);
			foreach (var file in Directory.GetFiles(path, @"*.mdb"))
			{
				var reader = new TableReader(file);
				var schema = new TablesSchema(file);
				foreach (var tableName in schema.GetTableNames())
				{
					Put(tableName, reader.FetchTable(tableName));
				}
			}
		}

		public static DataTable Get(string table)
		{
			lock (_lock)
			{
				return _cache.ContainsKey(table) ? _cache[table] : null;
			}
		}

		public static void Put(string table, DataTable datatable)
		{
			lock (_lock)
			{
				_cache[table] = datatable;
			}
		}
	}
}