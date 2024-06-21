using Dma.DatasourceLoader.Creator;
using Dma.DatasourceLoader.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ReceiptClient.Helpers;
using ReceiptClient.Shared;
using ReceiptClient.ViewModels;

namespace ReceiptClient
{
    public partial class FilterCriteriaControl : UserControl
    {
        public event System.EventHandler RemoveButtonClick;
        private FilterCriteria _criteria;
        public FilterCriteria Criteria => _criteria;

        public FilterCriteriaControl(bool isFirst = false, FilterCriteria criteria = null)
        {
            _criteria = criteria ?? new FilterCriteria(new List<Models.ColumnInfo>());

            InitializeComponent();
           
            InitColumnItems();
           
            label1.Visible = isFirst;
            combinationTypecomboBox.Visible = !isFirst;
            button1.Visible = !isFirst;

            // 今後変更されることがほぼないと思われるため、項目はハードコーディングとする
            comboProcess.Items.AddRange(new object[] { "受付", "依頼", "申請済み", "納品", "キャンセル" });

            InitState();

            _criteria.OnColumnSelected += setInputType;
            _criteria.OnOperatorsChange += setOperators;
            _criteria.OnOperatorSelected += setInputType;
            _criteria.OnOperatorSelected += setOperator;

            columnComboBox.SelectedIndexChanged += new System.EventHandler(this.ColumnSelectedIndexChanged);
            operatorComboBox.SelectedIndexChanged += new System.EventHandler(this.operatorComboBox_SelectedIndexChanged);
        }

        private void InitState()
        {
            if (_criteria != null)
            {
                numericUpDown1.Value = _criteria.NumValue;
                textBox1.Text = _criteria.TextValue;
                dateTimePicker1.Value = _criteria.DateRangeValue.Item1;
                dateTimePicker2.Value = _criteria.DateRangeValue.Item2;
                dateTimePicker3.Value = _criteria.DateValue;
                columnComboBox.SelectedIndex = _criteria.ColumnIndex;
                operatorComboBox.SelectedIndex = _criteria.OperatorIndex;
                combinationTypecomboBox.SelectedIndex = (int)_criteria.CombinationType;
                setInputType(this, EventArgs.Empty);
            }
        }
        private void setOperator(object sender, EventArgs e)
        {
            operatorComboBox.SelectedIndex = _criteria.OperatorIndex;
        }
        private void setOperators(object sender, EventArgs e)
        {
            operatorComboBox.Items.Clear();
            operatorComboBox.Items.AddRange(_criteria.Filters.Select(f => f.Value).ToArray());
        }

        private void setInputType(object sender, EventArgs e)
        {
            if (_criteria.SelectedColumn.VarName == "作業工程名")
            {
                comboProcess.Visible = true;
                dateTimePicker1.Visible = false;
                dateTimePicker2.Visible = false;
                dateTimePicker3.Visible = false;
                textBox1.Visible = false;
                numericUpDown1.Visible = false;
            }
            else
            {
                if (_criteria.SelectedColumn.Type.IsNumeric())
                {
                    comboProcess.Visible = false;
                    dateTimePicker1.Visible = false;
                    dateTimePicker2.Visible = false;
                    dateTimePicker3.Visible = false;
                    textBox1.Visible = false;
                    numericUpDown1.Visible = true;
                }
                if (_criteria.SelectedColumn.Type.IsDate())
                {
                    if (_criteria.Operator == FilterOperators.Between)
                    {
                        comboProcess.Visible = false;
                        dateTimePicker1.Visible = true;
                        dateTimePicker2.Visible = true;
                        dateTimePicker3.Visible = false;
                    }
                    else
                    {
                        comboProcess.Visible = false;
                        dateTimePicker1.Visible = false;
                        dateTimePicker2.Visible = false;
                        dateTimePicker3.Visible = true;
                    }
                    textBox1.Visible = false;
                    numericUpDown1.Visible = false;
                }
                if (_criteria.SelectedColumn.Type == typeof(string))
                {
                    comboProcess.Visible = false;
                    dateTimePicker1.Visible = false;
                    dateTimePicker2.Visible = false;
                    dateTimePicker3.Visible = false;
                    textBox1.Visible = true;
                    numericUpDown1.Visible = false;
                }
            }
        }

        private void InitColumnItems()
        {
            string[] items = _criteria.Configs.Select(c =>
                            c.Label
                        ).ToArray();
            operatorComboBox.Items.AddRange(_criteria.Filters.Select(f => f.Value).ToArray());
            columnComboBox.Items.AddRange(items);
        }

        private void ColumnSelectedIndexChanged(object sender, EventArgs e)
        {
            // Check if an item is selected (SelectedIndex is not -1)
            if (columnComboBox.SelectedIndex != -1)
            {
                // Get the selected item
                _criteria.SelectColumn(columnComboBox.SelectedIndex);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RemoveButtonClick?.Invoke(this, EventArgs.Empty);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            _criteria.DateRangeValue = (dateTimePicker1.Value, _criteria.DateRangeValue.Item2);
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            _criteria.DateRangeValue = (_criteria.DateRangeValue.Item1, dateTimePicker2.Value);
        }

        private void operatorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _criteria.SelectOperator(operatorComboBox.SelectedIndex);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            _criteria.NumValue = numericUpDown1.Value;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            _criteria.TextValue = textBox1.Text;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            _criteria.CombinationType = combinationTypecomboBox.SelectedItem.ToString() == "AND" ? FilterCombinationTypes.AND : FilterCombinationTypes.OR;
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            _criteria.DateValue = dateTimePicker3.Value;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _criteria.TextValue = comboProcess.Text;
        }
    }
}

