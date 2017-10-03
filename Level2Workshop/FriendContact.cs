namespace Level2Workshop
{
    public class FriendContact : Contact
    {
        public FriendContact()
            : base()
        {
            
        }

        public string EmailAddress { get; set; }

        public string Birthday { get; set; }

        public override string ToString()
        {
            const string FriendFormat = "Friend{0}{1}{2}";
            return string.Format(FriendFormat, base.ToString(), this.FormatFriendFields(), FormatAuditDates());
        }

        private string FormatFriendFields()
        {
            const string FriendFormat = "Email Address: {0}\r\nBirthday: {1}\r\n";
            return string.Format(FriendFormat, EmailAddress, Birthday);
        }
    }
}
