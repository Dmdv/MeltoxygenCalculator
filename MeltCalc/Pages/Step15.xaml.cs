using System;
using System.Globalization;
using System.Windows.Input;
using MeltCalc.Chemistry;
using MeltCalc.Converters;

namespace MeltCalc.Pages
{
	/// <summary>
	/// Interaction logic for Step15.xaml
	/// </summary>
	public partial class Step15
	{
		private readonly StringToDoubleConverter _converter = new StringToDoubleConverter();

		public Step15()
		{
			InitializeComponent();
			Loaded += OnLoaded;
		}

		private void OnLoaded(object sender, System.Windows.RoutedEventArgs e)
		{
			// Во избежание определения INotifyPropertyChanges в каждом компоненте придется инициализировать
			// поля из свойств вручную.

			InitializeFromGlobals();
			LomChem_Load();
			LomChemTotalCount();
		}

		private void LomChem_Load()
		{
			_lowSmall.Text = Params.ЛомМелкий.НизкоУглеродный.Part.ToString(CultureInfo.InvariantCulture);
			_lowMiddle.Text = Params.ЛомСредний.НизкоУглеродный.Part.ToString(CultureInfo.InvariantCulture);
			_lowLarge.Text = Params.ЛомКрупный.НизкоУглеродный.Part.ToString(CultureInfo.InvariantCulture);

			_midSmall.Text = Params.ЛомМелкий.СреднеУглеродный.Part.ToString(CultureInfo.InvariantCulture);
			_midMiddle.Text = Params.ЛомСредний.СреднеУглеродный.Part.ToString(CultureInfo.InvariantCulture);
			_midLarge.Text = Params.ЛомКрупный.СреднеУглеродный.Part.ToString(CultureInfo.InvariantCulture);

			_highSmall.Text = Params.ЛомМелкий.ВысокоУглеродный.Part.ToString(CultureInfo.InvariantCulture);
			_highMiddle.Text = Params.ЛомСредний.ВысокоУглеродный.Part.ToString(CultureInfo.InvariantCulture);
			_highLarge.Text = Params.ЛомКрупный.ВысокоУглеродный.Part.ToString(CultureInfo.InvariantCulture);
		}

		private void LomChemTotalCount()
		{
			Tube.Лом.C =
				Params.ЛомМелкий.НизкоУглеродный.C		* ToFloat(_lowSmall.Text) +
				Params.ЛомСредний.НизкоУглеродный.C		* ToFloat(_lowMiddle.Text) +
				Params.ЛомКрупный.НизкоУглеродный.C		* ToFloat(_lowLarge.Text) +
				Params.ЛомМелкий.СреднеУглеродный.C		* ToFloat(_midSmall.Text) +
				Params.ЛомСредний.СреднеУглеродный.C	* ToFloat(_midMiddle.Text) +
				Params.ЛомКрупный.СреднеУглеродный.C	* ToFloat(_midLarge.Text) +
				Params.ЛомМелкий.ВысокоУглеродный.C		* ToFloat(_highSmall.Text) +
				Params.ЛомСредний.ВысокоУглеродный.C	* ToFloat(_highMiddle.Text) +
				Params.ЛомКрупный.ВысокоУглеродный.C	* ToFloat(_highLarge.Text);

			Tube.Лом.Si =
				Params.ЛомМелкий.НизкоУглеродный.Si		* ToFloat(_lowSmall.Text) +
				Params.ЛомСредний.НизкоУглеродный.Si	* ToFloat(_lowMiddle.Text) +
				Params.ЛомКрупный.НизкоУглеродный.Si	* ToFloat(_lowLarge.Text) +
				Params.ЛомМелкий.СреднеУглеродный.Si	* ToFloat(_midSmall.Text) +
				Params.ЛомСредний.СреднеУглеродный.Si	* ToFloat(_midMiddle.Text) +
				Params.ЛомКрупный.СреднеУглеродный.Si	* ToFloat(_midLarge.Text) +
				Params.ЛомМелкий.ВысокоУглеродный.Si	* ToFloat(_highSmall.Text) +
				Params.ЛомСредний.ВысокоУглеродный.Si	* ToFloat(_highMiddle.Text) +
				Params.ЛомКрупный.ВысокоУглеродный.Si	* ToFloat(_highLarge.Text);

			Tube.Лом.Mn =
				Params.ЛомМелкий.НизкоУглеродный.Mn		* ToFloat(_lowSmall.Text) +
				Params.ЛомСредний.НизкоУглеродный.Mn	* ToFloat(_lowMiddle.Text) +
				Params.ЛомКрупный.НизкоУглеродный.Mn	* ToFloat(_lowLarge.Text) +
				Params.ЛомМелкий.СреднеУглеродный.Mn	* ToFloat(_midSmall.Text) +
				Params.ЛомСредний.СреднеУглеродный.Mn	* ToFloat(_midMiddle.Text) +
				Params.ЛомКрупный.СреднеУглеродный.Mn	* ToFloat(_midLarge.Text) +
				Params.ЛомМелкий.ВысокоУглеродный.Mn	* ToFloat(_highSmall.Text) +
				Params.ЛомСредний.ВысокоУглеродный.Mn	* ToFloat(_highMiddle.Text) +
				Params.ЛомКрупный.ВысокоУглеродный.Mn	* ToFloat(_highLarge.Text);

			Tube.Лом.P =
				Params.ЛомМелкий.НизкоУглеродный.P		* ToFloat(_lowSmall.Text) +
				Params.ЛомСредний.НизкоУглеродный.P		* ToFloat(_lowMiddle.Text) +
				Params.ЛомКрупный.НизкоУглеродный.P		* ToFloat(_lowLarge.Text) +
				Params.ЛомМелкий.СреднеУглеродный.P		* ToFloat(_midSmall.Text) +
				Params.ЛомСредний.СреднеУглеродный.P	* ToFloat(_midMiddle.Text) +
				Params.ЛомКрупный.СреднеУглеродный.P	* ToFloat(_midLarge.Text) +
				Params.ЛомМелкий.ВысокоУглеродный.P		* ToFloat(_highSmall.Text) +
				Params.ЛомСредний.ВысокоУглеродный.P	* ToFloat(_highMiddle.Text) +
				Params.ЛомКрупный.ВысокоУглеродный.P	* ToFloat(_highLarge.Text);

			Tube.Лом.S =
				Params.ЛомМелкий.НизкоУглеродный.S		* ToFloat(_lowSmall.Text) +
				Params.ЛомСредний.НизкоУглеродный.S		* ToFloat(_lowMiddle.Text) +
				Params.ЛомКрупный.НизкоУглеродный.S		* ToFloat(_lowLarge.Text) +
				Params.ЛомМелкий.СреднеУглеродный.S		* ToFloat(_midSmall.Text) +
				Params.ЛомСредний.СреднеУглеродный.S	* ToFloat(_midMiddle.Text) +
				Params.ЛомКрупный.СреднеУглеродный.S	* ToFloat(_midLarge.Text) +
				Params.ЛомМелкий.ВысокоУглеродный.S		* ToFloat(_highSmall.Text) +
				Params.ЛомСредний.ВысокоУглеродный.S	* ToFloat(_highMiddle.Text) +
				Params.ЛомКрупный.ВысокоУглеродный.S	* ToFloat(_highLarge.Text);

			Tube.Лом.C /= 100;
			Tube.Лом.S /= 100;
			Tube.Лом.P /= 100;
			Tube.Лом.Si /= 100;
			Tube.Лом.Mn /= 100;

			_lomC.Text = Tube.Лом.C .ToString(CultureInfo.InvariantCulture);
			_lomSi.Text = Tube.Лом.Si.ToString(CultureInfo.InvariantCulture);
			_lomMn.Text = Tube.Лом.Mn.ToString(CultureInfo.InvariantCulture);
			_lomP.Text = Tube.Лом.P.ToString(CultureInfo.InvariantCulture);
			_lomS.Text = Tube.Лом.S.ToString(CultureInfo.InvariantCulture);
		}

		private void InitializeFromGlobals()
		{
			_stalMass.Value = Tube.Сталь.GYield;

			if (Params.IsDuplex)
			{
				_shlak.IsEnabled = false;
				_leftShlak.IsChecked = false;
			}

			if (Params.InputForm == "auto")
			{
				_stalMass.IsEnabled = true;
				_stalMass.AllowSpin = true;
			}
			else
			{
				_stalMass.IsEnabled = false;
				_stalMass.AllowSpin = false;
			}
		}

		private float ToFloat(string text)
		{
			var convertBack = _converter.ConvertBack(text, typeof (double), null, null);
			return Convert.ToSingle(convertBack);
		}

		private void NextCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		private void PreviousCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
		}

		private void NextExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			double gYield = Tube.Сталь.GYield;
		}

		private void PreviousExecuted(object sender, ExecutedRoutedEventArgs e)
		{
		}
	}
}
