using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drive.Common.IHelpers
{
    public interface IPaging
    {
        int? Page { get; set; }
        int NumberOfObjectsPerPage { get; set; }
        int ItemsToSkip { get; set; }
        int TotalCount { get; set; }
    }
}
