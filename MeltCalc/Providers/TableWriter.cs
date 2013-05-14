using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.OleDb;

namespace MeltCalc.Providers
{
	public class TableWriter : MdbTable
	{
		public TableWriter(string file) :
			base(file)
		{
		}

		/// <summary>
		/// 0 row ������� �� ����������.
		/// � �������� ��������� ������ �� 1.
		/// </summary>
		public void Write(string tablename, IList<Tuple<string, double>> parameters, int row)
		{
			if (row <= 0)
			{
				throw new ArgumentException(@"Invalid row", "row");
			}

			InitCommand(tablename, parameters, row);

			using (var conn = CreateConnection())
			{
				using (Command)
				{
					conn.Open();
					Command.Connection = conn;
					Command.ExecuteNonQuery();
				}
			}
		}

		protected void InitCommand(string tablename, IList<Tuple<string, double>> parameters, int row)
		{
			var cmdText = CreateCommandText(tablename, parameters, row);
			var dbParameters = CreateCommandParameters(parameters);
			Command = new OleDbCommand(cmdText);
			Command.Parameters.AddRange(dbParameters.ToArray());
		}

		private OleDbCommand Command { get; set; }

		private static IEnumerable<OleDbParameter> CreateCommandParameters(IEnumerable<Tuple<string, double>> parameters)
		{
			return parameters
				.Select(tuple => new OleDbParameter(string.Format("@{0}", tuple.Item1), tuple.Item2));
		}

		private static string CreateCommandText(string tablename, IEnumerable<Tuple<string, double>> parameters, int row)
		{
			var text = string.Format("update {0} set", tablename);

			var paramsText = parameters
				.Select(tuple => string.Format("[{0}] = @{0}", tuple.Item1))
				.Aggregate((acc, next) => string.Join(", ", acc, next));

			return string.Format("{0} {1} where Index = {2}", text, paramsText, row);
		}
	}
}