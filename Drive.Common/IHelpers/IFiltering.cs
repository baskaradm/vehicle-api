using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drive.Common.IHelpers
{
    public interface IFiltering
    {
        string SearchString { get; set; }
        
        string FilterBy { get; set; }

        bool ShouldApplyFilters();
    }
}
