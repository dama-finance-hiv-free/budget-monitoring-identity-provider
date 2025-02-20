using IdentityProvider.Protectimus.Enums;

namespace IdentityProvider.Protectimus.Poco
{
    public class Token
    {

        private long id;
        private string name;
        private TokenType type;
        private string serialNumber;
        private bool enabled;
        private bool apiSupport;
        private long? userId;
        private long? clientStaffId;
        private long? creatorId;
        private string username;
        private string clientStaffUsername;
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


        public virtual string Name
        {
            get
            {
                return name;
            }
            set
            {
                this.name = value;
            }
        }


        public virtual TokenType Type
        {
            get
            {
                return type;
            }
            set
            {
                this.type = value;
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


        public virtual bool Enabled
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


        public virtual long? UserId
        {
            get
            {
                return userId;
            }
            set
            {
                this.userId = value;
            }
        }


        public virtual long? ClientStaffId
        {
            get
            {
                return clientStaffId;
            }
            set
            {
                this.clientStaffId = value;
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
            return "Token [id=" + id + ", name=" + name + ", type=" + type + ", serialNumber=" + serialNumber + ", enabled=" + enabled + ", apiSupport=" + apiSupport + ", userId=" + userId + ", clientStaffId=" + clientStaffId + ", creatorId="
                    + creatorId + ", username=" + username + ", clientStaffUsername=" + clientStaffUsername + ", creatorUsername=" + creatorUsername + "]";
        }

    }

}
