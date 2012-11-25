using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Controls;
using MeltCalc.Helpers;
using MeltCalc.Model;

namespace MeltCalc.ViewModel
{
	public class Step14Model : BaseViewModel
	{
		private static readonly LooseMdb _db = new LooseMdb();

		private readonly List<TextBox> _boxes;
		private readonly ComboBox _comboBox;
		private readonly Materials _material;
		private readonly DataTable _table;

		public Step14Model()
		{
		}

		public Step14Model(ContentControl groupBox)
		{
			_material = groupBox.Material();
			_table = _db.Reader.FetchTable(_material.ToDatabaseName());

			_boxes = groupBox.FindVisualChild<TextBox>().ToList();
			_comboBox = groupBox.FindVisualChild<ComboBox>().SingleOrDefault();

			if (_comboBox == null)
			{
				throw new ApplicationException("Combobox not found for " + _material);
			}
			if (_boxes.Count == 0)
			{
				throw new ApplicationException("Boxes not found for " + _material);
			}
			if (_table.Columns.Count != _boxes.Count + 1) // 1 - это колонка Index.
			{
				throw new ApplicationException("Columns count should be equal to text boxes count");
			}

			for (var i = 0; i < _table.Rows.Count; i++)
			{
				_comboBox.Items.Add(i);
			}

			_comboBox.SelectionChanged += SelectionChanged;
			_comboBox.SelectedIndex = 0;
		}

		// TODO: Было бы лучше, если привязать значения по имени колонки, а не по индексу.
		private void SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var row = _table.Rows[_comboBox.SelectedIndex];
			var itemArray = row.ItemArray;
			for (var i = 1; i < itemArray.Length; i++)
			{
				_boxes[i - 1].Text = itemArray[i].ToString();
			}
		}
	}
}