using MeltCalc.Properties;
using MeltCalc.Providers;

namespace MeltCalc.Tables
{
	public class LooseMdb : MdbReader
	{
		public LooseMdb() : base(Settings.Default.LooseMdb)
		{
		}
	}
}