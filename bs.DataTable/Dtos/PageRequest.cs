using bs.DataTable.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace bs.DataTable.Dtos
{
    public class PageRequest : IPageRequest
    {
        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public Column[] Columns { get; set; }
    }
}
