using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drive.Common.IHelpers
{
    public interface ISorting
    {
        string SortBy { get; set; }
        string SortByName { get; set; }
        string SortByAbbreviation { get; set; }
        string SortById { get; set; }
    }
}
