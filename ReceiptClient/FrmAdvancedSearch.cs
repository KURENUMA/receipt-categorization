using Dma.DatasourceLoader;
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
using ReceiptClient.Shared;
using ReceiptClient.ViewModels;
using ReceiptClient.Models;

namespace ReceiptClient
{
    public partial class FrmAdvancedSearch : Form
    {
        private List<FilterCriteriaControl> filterCriteriaList = new List<FilterCriteriaControl>();
        private List<FilterCriteria> initialCriterias;
        private readonly PatternInfo patternInfo;

        public event System.EventHandler OnSearch;
        public event System.EventHandler OnSearchAndSave;
        public event System.EventHandler OnReset;
        public List<FilterCriteria> FilterCriterias => filterCriteriaList
            .Select(f => f.Criteria)
            .ToList();
        public FrmAdvancedSearch(List<FilterCriteria> initialCriterias = null, PatternInfo patternInfo = null)
        {
            InitializeComponent();
            this.initialCriterias = initialCriterias;
            this.patternInfo = patternInfo;
        }

        private void InitializeState(List<FilterCriteria> initialCriterias)
        {
            var i = 0;
            foreach (var c in initialCriterias)
            {
                var control = new FilterCriteriaControl(isFirst: i == 0, criteria: c);
                control.Dock = DockStyle.Bottom;
                addFilterCriteria(control);
                i++;
            }
        }

        private void FrmAdvancedSearch_Load(object sender, EventArgs e)
        {
            if (initialCriterias != null)
            {
                InitializeState(initialCriterias);
            }
            if (filterCriteriaList.Count > 0) return;
            FilterCriteriaControl filterCriteria = new FilterCriteriaControl(isFirst: true, criteria: new FilterCriteria(patternInfo.Columns.ToList()));
            filterCriteria.Dock = DockStyle.Top;
            addFilterCriteria(filterCriteria);

        }

        private void addFilterCriteria(FilterCriteriaControl filterCriteria)
        {
            filterCriteria.RemoveButtonClick += (s, args) =>
            {
                if (filterCriteriaList.Count == 1)
                {
                    return;
                }
                removeFilterCriteria(filterCriteria);
            };
            filterCriteriaList.Add(filterCriteria);
            filterPanel.Controls.Add(filterCriteria);
        }

        private void removeFilterCriteria(FilterCriteriaControl filterCriteria)
        {
            filterCriteriaList.Remove(filterCriteria);
            filterPanel.Controls.Remove(filterCriteria);
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            // Create a new FilterCriteria control
            FilterCriteriaControl filterCriteria = new FilterCriteriaControl(criteria: new FilterCriteria(patternInfo.Columns.ToList()));


            // Add the new filter criteria to the list and the panel
            filterCriteria.Dock = DockStyle.Bottom;
            addFilterCriteria(filterCriteria);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OnSearchAndSave?.Invoke(this, EventArgs.Empty);
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OnSearch?.Invoke(this, EventArgs.Empty);
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            foreach (var f in filterCriteriaList)
            {
                filterPanel.Controls.Remove(f);
            }
            filterCriteriaList.Clear();
            FilterCriteriaControl filterCriteria = new FilterCriteriaControl(isFirst: true, criteria: new FilterCriteria(patternInfo.Columns.ToList()));
            filterCriteria.Dock = DockStyle.Top;
            addFilterCriteria(filterCriteria);
            OnReset?.Invoke(this, EventArgs.Empty);
        }
    }
}
