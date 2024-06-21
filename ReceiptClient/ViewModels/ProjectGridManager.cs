using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dma.DatasourceLoader.Creator;
using Dma.DatasourceLoader.Models;
using Dma.DatasourceLoader;
using ReceiptClient.Models;
using ReceiptClient.TestData;
using ReceiptClient.ViewModels;

namespace ReceiptClient.UserControls
{
    //State management
    public class ProjectGridManager : IGridManager
    {
        private List<ReceiptInfo> dataSource = new List<ReceiptInfo>();

        private bool showAll = false;
        public int TotalPages { get; private set; } = 1;
        public int Total { get; private set; } = 0;
        public string FilterOptionStr { get; private set; } = "";

        private readonly DataTable dt = new DataTable();
        private List<FilterCriteria> filters = new List<FilterCriteria>();
        public event System.EventHandler OnDataSourceChange;

        private int currentPage = 1;
        public int pageSize = 100;
        public DataTable Dt => dt;
        public void SetCurrentPage(int currentPage) { this.currentPage = currentPage; }
        public void SetPageSize(int pageSize) { this.pageSize = pageSize; }
        public void SetFilters(List<FilterCriteria> filters) { this.filters = filters; }
        public List<FilterCriteria> Filters => filters;

        public PatternInfo PatternInfo { get; private set; }

        public ProjectGridManager(PatternInfo patternInfo)
        {
            this.dt = new DataTable();
            this.PatternInfo = patternInfo;
            foreach (var columnInfo in patternInfo.Columns)
            {
                dt.Columns.Add(columnInfo.VarName, columnInfo.Type);
            }
        }

        public bool ToggleShowAll()
        {
            showAll = !showAll;
            return showAll;
        }

        public void Reload()
        {
            Reload(this.PatternInfo, this.filters);
        }

        public void Reload(List<FilterCriteria> filters)
        {
            Reload(this.PatternInfo, filters);
        }

        public async void Reload(PatternInfo patternInfo, List<FilterCriteria> filters)
        {
            dt.Columns.Clear();
            this.PatternInfo = patternInfo;
            foreach (var columnInfo in patternInfo.Columns)
            {
                dt.Columns.Add(columnInfo.VarName, columnInfo.Type);
            }

            dt.Clear();
            var conditions = filters.Select(f => f.AsFilterOption()).ToList();

            // テストデータの取得
            this.dataSource = ReceiptInfoTestData.GetTestData();

            FilterOptionStr = CreateFilterOptionStr(conditions);

            this.currentPage = 1;
            var query = dataSource.AsQueryable();

            var paginatedQuery = query
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize);

            Total = query.Count();
            TotalPages = (Total - 1) / pageSize + 1;

            PopulateDataToDataTable(paginatedQuery.ToList(), true);
            OnDataSourceChange?.Invoke(this, EventArgs.Empty);
        }

        public void SyncPage()
        {
            var query = dataSource.AsQueryable();
            var paginatedQuery = query
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize);
            PopulateDataToDataTable(paginatedQuery.ToList(), true);
        }

        private string CreateFilterOptionStr(List<(FilterCombinationTypes, FilterOption)> filterOptions)
        {
            StringBuilder sb = new StringBuilder();
            int cnt = 0;
            foreach (var f in filterOptions)
            {
                if (cnt == 0)
                {
                    sb.AppendLine(" 検索項目名：「" + f.Item2.PropertyName + "」 検索条件：「" + f.Item2.Operator + "」 検索値：「" + f.Item2.Value + "」");
                }
                else
                {
                    sb.AppendLine(f.Item1.ToString() + " 検索項目名：「" + f.Item2.PropertyName + "」 検索条件：「" + f.Item2.Operator + "」 検索値：「" + f.Item2.Value + "」");
                }
                cnt++;
            }
            return sb.ToString();
        }

        private void PopulateDataToDataTable(List<ReceiptInfo> list, bool shouldReset = false)
        {
            if (shouldReset)
            {
                dt.Clear();
            }

            foreach (var data in list)
            {
                DataRow newRow = dt.NewRow();

                foreach (var columnInfo in PatternInfo.Columns)
                {
                    object mappedValue = PatternInfo.GetValue(data, columnInfo.VarName);
                    if (mappedValue == null)
                    {
                        mappedValue = DBNull.Value;
                    }
                    newRow[columnInfo.VarName] = mappedValue;
                }
                dt.Rows.Add(newRow);
            }
        }

        public DataTable getAllFilteredData()
        {
            var query = dataSource.AsQueryable();
            if (filters != null)
                query = DataSourceLoader.ApplyCombinedFilters(query, filters.Select(f => f.AsFilterOption()).ToList());
            var list = query.ToList();
            DataTable dtAllData = new DataTable();
            foreach (var config in ProjectDatumHelpers.ColumnConfigs)
            {
                dtAllData.Columns.Add(config.Caption, config.DataType);
            }

            foreach (var data in list)
            {
                DataRow newRow = dtAllData.NewRow();

                foreach (var config in ProjectDatumHelpers.ColumnConfigs)
                {
                    object mappedValue = config.MapData(data);
                    if (mappedValue == null)
                    {
                        mappedValue = DBNull.Value;
                    }
                    newRow[config.Caption] = mappedValue;
                }

                dtAllData.Rows.Add(newRow);
            }
            return dtAllData;
        }
    }
}
