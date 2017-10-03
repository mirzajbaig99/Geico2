using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Level2Workshop.Model
{
    public class CompanyDto : ContactDto
    {
        public CompanyDto()
            : base()
        {

        }

        public string Url { get; set; }
    }
}
