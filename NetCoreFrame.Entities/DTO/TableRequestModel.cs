using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreFrame.Entities.DTO
{
    public class TableRequestViewModel
    {
        public string SortColumn { get; set; }
        public string SortColumnDirection { get; set; }
        public int PageSize { get; set; }
        public int Skip { get; set; }
        public string Keywords { get; set; }
    }
}
