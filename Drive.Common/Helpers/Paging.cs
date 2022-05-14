using Drive.Common.IHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drive.Common.Helpers
{

    public class Paging : IPaging
    {
        public int? Page { get; set; }
        public int NumberOfObjectsPerPage { get; set; }
        public int ItemsToSkip { get; set; }
        public int TotalCount { get; set; }


        public Paging(int? page)
        {
            NumberOfObjectsPerPage = 5;
            Page = page;
            ItemsToSkip = NumberOfObjectsPerPage * ((Page ?? 1) - 1);
        }
    }
}
