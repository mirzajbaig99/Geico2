namespace Level2Workshop
{
    public class CompanyContact : Contact
    {
        public CompanyContact()
            : base()
        {
            
        }

        public string Url { get; set; }

        public override string ToString()
        {
            const string CompanyFormat =
                "Company{0}{1}{2}";
            return string.Format(CompanyFormat, base.ToString(), this.FormatCompanyFields(), FormatAuditDates());
        }

        private string FormatCompanyFields()
        {
            const string CompanyFormat = "URL: {0}\r\n";
            return string.Format(CompanyFormat, Url);
        }
    }
}
