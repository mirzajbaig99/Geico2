using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Level2Workshop
{
    public class WorkContactDto : ContactDto
    {
        public string Title { get; set; }

        public string Company { get; set; }

        public string EmailAddress { get; set; }

        public string Url { get; set; }
    }
}
