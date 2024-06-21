using System.Collections.Generic;
using System.Data;
using ReceiptClient.ViewModels;

namespace ReceiptClient.UserControls
{
    public interface IGridManager
    {
        DataTable Dt { get; }
        List<FilterCriteria> Filters { get; }
        int Total { get; }
        int TotalPages { get; }

        event System.EventHandler OnDataSourceChange;

        void Reload();
        void Reload(List<FilterCriteria> filters);
        void SetCurrentPage(int currentPage);
        void SetFilters(List<FilterCriteria> filters);
        void SetPageSize(int pageSize);
    }
}