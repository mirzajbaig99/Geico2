using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Level2Workshop.Validations.Attributes;

namespace Level2Workshop
{
    public abstract class Contact
    {
        #region private fields

        private int taxId;

        #endregion

        public Contact()
        {
            DateCreated = DateTime.Now;
        }
        [Required]
        public string Name { get; set; }
        [Required]
        public int TaxId { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public override string ToString()
        {
            const string ContactFormat = "\r\n-------\r\n{0}";

            return string.Format(ContactFormat, this.FormatCommonFields());
        }

        internal string FormatCommonFields()
        {
            const string CommonFieldsFormat = "Name: {0}\r\nTax ID: {1}\r\nAddress: {2}\r\nPhone Number: {3}\r\n";

            return string.Format(CommonFieldsFormat, this.Name, this.TaxId, this.Address, this.PhoneNumber);
        }

        public string FormatAuditDates()
        {
            const string AuditDatesFormat = "Date Created: {0}\r\nDate Modified: {1}\r\n";

            return string.Format(AuditDatesFormat, this.DateCreated, this.DateModified);
        }
    }
}
