namespace IdentityProvider.Protectimus.Poco
{
    public class User
    {

        private long id;
        private string login;
        private string email;
        private string phoneNumber;
        private string firstName;
        private string secondName;
        private bool apiSupport;
        private bool hasTokens;
        private long? creatorId;
        private string creatorUsername;

        public virtual long Id
        {
            get
            {
                return id;
            }
            set
            {
                this.id = value;
            }
        }


        public virtual string Login
        {
            get
            {
                return login;
            }
            set
            {
                this.login = value;
            }
        }


        public virtual string Email
        {
            get
            {
                return email;
            }
            set
            {
                this.email = !string.ReferenceEquals(value, null) && value.Length == 0 ? null : value;
            }
        }


        public virtual string PhoneNumber
        {
            get
            {
                return phoneNumber;
            }
            set
            {
                this.phoneNumber = !string.ReferenceEquals(value, null) && value.Length == 0 ? null : value;
            }
        }


        public virtual string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                this.firstName = !string.ReferenceEquals(value, null) && value.Length == 0 ? null : value;
            }
        }


        public virtual string SecondName
        {
            get
            {
                return secondName;
            }
            set
            {
                this.secondName = !string.ReferenceEquals(value, null) && value.Length == 0 ? null : value;
            }
        }


        public virtual bool ApiSupport
        {
            get
            {
                return apiSupport;
            }
            set
            {
                this.apiSupport = value;
            }
        }


        public virtual bool HasTokens
        {
            get
            {
                return hasTokens;
            }
            set
            {
                this.hasTokens = value;
            }
        }


        public virtual long? CreatorId
        {
            get
            {
                return creatorId;
            }
            set
            {
                this.creatorId = value;
            }
        }


        public virtual string CreatorUsername
        {
            get
            {
                return creatorUsername;
            }
            set
            {
                this.creatorUsername = value;
            }
        }


        public override string ToString()
        {
            return "User [id=" + id + ", login=" + login + ", email=" + email + ", phoneNumber=" + phoneNumber + ", firstName=" + firstName + ", secondName=" + secondName + ", apiSupport=" + apiSupport + ", hasTokens=" + hasTokens + ", creatorId=" + creatorId + ", creatorUsername=" + creatorUsername + "]";
        }

    }

}
