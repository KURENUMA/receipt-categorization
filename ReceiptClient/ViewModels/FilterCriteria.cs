using Dma.DatasourceLoader.Creator;
using Dma.DatasourceLoader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReceiptClient.Helpers;
using ReceiptClient.Shared;
using ReceiptClient.Models;

namespace ReceiptClient.ViewModels
{
    public delegate void EventHandler(object sender, EventArgs e);

    public class FilterCriteria
    {
        private List<ColumnInfo> _configs;
        public List<ColumnInfo> Configs => _configs;
        private ColumnInfo _selectedColumn;
        private List<FilterOperators> _operators = new List<FilterOperators>();
        private FilterOperators _operator;
        public FilterOperators Operator => _operator;
        public int OperatorIndex => _operators.Count > 0 ? _operators.IndexOf(_operator) : -1;
        public FilterCombinationTypes CombinationType { get; set; } = FilterCombinationTypes.AND;
        public decimal NumValue { get; set; } = 0;
        public (DateTime, DateTime) DateRangeValue { get; set; } = (DateTime.Now, DateTime.Now);
        public DateTime DateValue { get; set; } = DateTime.Now;
        public string TextValue { get; set; } = "";

        public event EventHandler OnColumnSelected;
        public event EventHandler OnOperatorSelected;
        public event EventHandler OnOperatorsChange;
        public ColumnInfo SelectedColumn => _selectedColumn;
        public int ColumnIndex => _configs.IndexOf(_selectedColumn) == -1 ? 0 : _configs.IndexOf(_selectedColumn);

        public FilterCriteria(List<ColumnInfo> configs)
        {
            _configs = configs;
            SelectColumn(0);
        }

        public List<FilterOperators> Filters { get => _operators; }

        public void SelectOperator(int ind)
        {
            if (ind < 0) return;
            _operator = _operators[ind];
            OnOperatorSelected?.Invoke(this, EventArgs.Empty);
        }

        public void SelectColumn(int index)
        {
            var config = _configs[index];
            SelectColumn(config);

        }
        public void SelectColumn(ColumnInfo config)
        {
            _selectedColumn = config;
            if (config.Type == typeof(string))
            {
                _operators = FilterOperators.GetStringFilters();
            }
            if (config.Type.IsNumeric())
            {
                _operators = FilterOperators.GetNumericFilters();
            }
            if (config.Type == typeof(DateTime))
            {
                _operators = FilterOperators.GetDateFilters();
            }
            OnColumnSelected?.Invoke(this, EventArgs.Empty);
            OnOperatorsChange?.Invoke(this, EventArgs.Empty);
            SelectOperator(0);

        }
        // Helper method to raise the ColumnSelected event


        private object GetValue()
        {
            if (_selectedColumn.Type == typeof(string))
                return TextValue;
            if (_selectedColumn.Type == typeof(DateTime))
            {
                if (Operator == FilterOperators.Between) return DateRangeValue;
                return DateValue;
            }
            return _selectedColumn.Type.CastNumber(NumValue);
        }

        public (FilterCombinationTypes, FilterOption) AsFilterOption()
        {
            return (CombinationType, new FilterOption(SelectedColumn.VarName, Operator, GetValue()));
        }
    }
}
