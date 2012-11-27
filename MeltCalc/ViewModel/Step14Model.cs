using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MeltCalc.Helpers;
using MeltCalc.Model;

namespace MeltCalc.ViewModel
{
	public class Step14Model : BaseViewModel
	{
		// Note: ¬ таблице IMF хранитс€ на одну колонку больше, чем необходимо.

		private static readonly LooseMdb _db = new LooseMdb();

		private readonly List<TextBox> _boxes;
		private readonly ComboBox _comboBox;
		private readonly Materials _material;
		private readonly DataTable _table;

		public Step14Model(ContentControl groupBox)
		{
			_material = groupBox.Material();
			_table = _db.Reader.FetchTable(_material.ToDatabaseName());

			_boxes = groupBox.FindVisualChild<TextBox>().ToList();
			_comboBox = groupBox.FindVisualChild<ComboBox>().SingleOrDefault();

			ValidateData();
			try
			{
				InitializeVariants();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		// TODO: Ѕыло бы лучше, если прив€зать значени€ по имени колонки, а не по индексу.
		private void SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var row = _table.Rows[_comboBox.SelectedIndex];
			var itemArray = row.ItemArray;
			if (_material == Materials.»звестковоћагнитный‘люс)
			{
				itemArray = itemArray.Take(itemArray.Length - 1).ToArray();
			}
			for (var i = 1; i < itemArray.Length; i++)
			{
				_boxes[i - 1].Text = itemArray[i].ToString();
			}
		}

		private void InitializeVariants()
		{
			// ReSharper disable PossibleNullReferenceException
			for (var i = 0; i < _table.Rows.Count; i++)
			{
				_comboBox.Items.Add(i);
			}

			_comboBox.SelectionChanged += SelectionChanged;
			_comboBox.SelectedIndex = 0;
			// ReSharper restore PossibleNullReferenceException
		}

		private void ValidateData()
		{
			if (_comboBox == null)
			{
				var message = "Combobox not found for " + _material;
				MessageBox.Show(message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
				throw new ApplicationException(message);
			}

			if (_boxes.Count == 0)
			{
				var message = "Boxes not found for " + _material;
				MessageBox.Show(message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
				throw new ApplicationException(message);
			}

			var adder = 1;
			if (_material == Materials.»звестковоћагнитный‘люс)
			{
				adder = 2;
			}

			if (_table.Columns.Count != _boxes.Count + adder) // 1 - это колонка Index.
			{
				var message = "Columns count should be equal to text boxes count in " + _material;
				MessageBox.Show(message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
				throw new ApplicationException(message);
			}
		}
	}
}