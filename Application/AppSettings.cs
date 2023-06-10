using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class AppSettings
    {
        public long SizeForFilesToUseParallelism { get; set; }
        public int FilesCountToUseParallelism { get; set; }
    }
}
