using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Level2Workshop.Model
{
    public abstract class ContactDto
    {
        private int taxId;

        public ContactDto()
        {
            DateCreated = DateTime.Now;
        }

        public string Name { get; set; }

        public int TaxId { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}
