using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using MeltCalc.Properties;

namespace MeltCalc.Providers
{
	/// <summary>
	/// Непотокобезопасный кэш.
	/// </summary>
	public static class TableCache
	{
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
			return _cache[table];
		}

		public static void Put(string table, DataTable datatable)
		{
			_cache[table] = datatable;
		}
	}
}