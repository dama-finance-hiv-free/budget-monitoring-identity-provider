using IdentityProvider.Protectimus.Enums;

namespace IdentityProvider.Protectimus.Filters
{
    public class TokenFilter
    {
        private string tokenName;
        private TokenType tokenType;
        private string serialNumber;
        private bool? enabled;
        private TokenBlockType block;
        private string username;
        private string clientStaffUsername;
        private string resourceIds;
        private bool? useBlankNames;
        private string clientName;
        private bool? useBlankClientName;

        public virtual string TokenName
        {
            get
            {
                return tokenName;
            }
            set
            {
                this.tokenName = value;
            }
        }


        public virtual TokenType TokenType
        {
            get
            {
                return tokenType;
            }
            set
            {
                this.tokenType = value;
            }
        }


        public virtual string SerialNumber
        {
            get
            {
                return serialNumber;
            }
            set
            {
                this.serialNumber = value;
            }
        }


        public virtual bool? Enabled
        {
            get
            {
                return enabled;
            }
            set
            {
                this.enabled = value;
            }
        }


        public virtual TokenBlockType Block
        {
            get
            {
                return block;
            }
            set
            {
                this.block = value;
            }
        }


        public virtual string Username
        {
            get
            {
                return username;
            }
            set
            {
                this.username = value;
            }
        }


        public virtual string ClientStaffUsername
        {
            get
            {
                return clientStaffUsername;
            }
            set
            {
                this.clientStaffUsername = value;
            }
        }


        public virtual string ResourceIds
        {
            get
            {
                return resourceIds;
            }
            set
            {
                this.resourceIds = value;
            }
        }


        public virtual bool? UseBlankNames
        {
            get
            {
                return useBlankNames;
            }
            set
            {
                this.useBlankNames = value;
            }
        }


        public virtual string ClientName
        {
            get
            {
                return clientName;
            }
            set
            {
                this.clientName = value;
            }
        }


        public virtual bool? UseBlankClientName
        {
            get
            {
                return useBlankClientName;
            }
            set
            {
                this.useBlankClientName = value;
            }
        }

    }

}
