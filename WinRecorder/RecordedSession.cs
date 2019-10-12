using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinRecorder
{
    public class RecordedSession
    {
        public RecordedSession()
        {
            Events = new List<WinEvent>();
        }

        public string Name { get; set; }
        public string CreationDate { get; set; }
        public string Application { get; set; }

        public string RecorderVersion { get; set; }
       
        public List<WinEvent> Events { get; set; }
    }
}
