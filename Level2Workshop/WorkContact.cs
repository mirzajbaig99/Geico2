using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Level2Workshop
{
    public class WorkContact : Contact
    {
        public WorkContact()
            : base()
        {
            
        }

        public string Title { get; set; }

        public string Company { get; set; }

        public string EmailAddress { get; set; }

        public string Url { get; set; }

        public override string ToString()
        {
            const string WorkFormat = "Work{0}{1}{2}";
            return string.Format(WorkFormat, base.ToString(), this.FormatWorkFields(), FormatAuditDates());
        }

        private string FormatWorkFields()
        {
            const string WorkFormat = "Title: {0}\r\nCompany: {1}\r\nEmail Address: {2}\r\nURL: {3}\r\n";
            return string.Format(WorkFormat, Title, Company, EmailAddress, Url);
        }
    }
}
