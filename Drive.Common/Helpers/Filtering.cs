using Drive.Common.IHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drive.Common.Helpers
{
    public class Filtering : IFiltering
    {
        public string SearchString { get; set; }
        public string FilterBy { get; set; }

        public Filtering(string searchString)
        {
           
            SearchString = searchString;
        }

        public bool ShouldApplyFilters()
        {
           

            if (!string.IsNullOrEmpty(SearchString))
            {
                FilterBy = SearchString;
                return true;
            }

            return false;
        }
    }
}
