using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows;
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

		public static DataTable Get(Key key)
		{
			lock (_lock)
			{
				return _cache.ContainsKey(key.Value) ? _cache[key.Value] : null;
			}
		}

		public static void Put(Key key, DataTable datatable)
		{
			lock (_lock)
			{
				if (Get(key) != null)
				{
					throw new Exception(string.Format("{0} doesn't exist", key));
				}
				_cache[key.Value] = datatable;
			}
		}

		public static void Refresh()
		{
			var path = Path.Combine(Environment.CurrentDirectory, Settings.Default.DatabaseRelativePath);

			if (!Directory.Exists(path))
			{
				MessageBox.Show(string.Format("Не найдена директория с данными: '{0}'", path));
				return;
			}

			foreach (var file in Directory.GetFiles(path, @"*.mdb"))
			{
				var reader = new TableReader(file);
				var schema = new TablesSchema(file);
				foreach (var tableName in schema.GetTableNames())
				{
					Put(new Key(tableName, file), reader.FetchTable(tableName));
				}
			}
		}
	}
}