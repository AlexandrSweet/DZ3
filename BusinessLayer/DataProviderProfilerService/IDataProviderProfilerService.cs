using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.DataProviderProfilerService
{
    public interface IDataProviderProfilerService
    {
        public Dictionary<string, string> ComparePerformance();
       
    }
}
