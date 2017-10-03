using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Level2Workshop.Model
{
    public class FriendDto : ContactDto
    {
        public FriendDto()
            : base()
        {

        }

        public string EmailAddress { get; set; }

        public string Birthday { get; set; }
    }
}
