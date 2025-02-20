namespace IdentityProvider.Protectimus.Poco
{
    public class Resource
    {

        private long id;
        private string name;
        private short failedAttemptsBeforeLock;
        private long? geoFilterId;
        private string geoFilterName;
        private bool? geoFilterEnabled;
        private long? timeFilterId;
        private string timeFilterName;
        private bool? timeFilterEnabled;
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


        public virtual short FailedAttemptsBeforeLock
        {
            get
            {
                return failedAttemptsBeforeLock;
            }
            set
            {
                this.failedAttemptsBeforeLock = value;
            }
        }


        public virtual long? GeoFilterId
        {
            get
            {
                return geoFilterId;
            }
            set
            {
                this.geoFilterId = value;
            }
        }


        public virtual string GeoFilterName
        {
            get
            {
                return geoFilterName;
            }
            set
            {
                this.geoFilterName = value;
            }
        }


        public virtual bool? GeoFilterEnabled
        {
            get
            {
                return geoFilterEnabled;
            }
            set
            {
                this.geoFilterEnabled = value;
            }
        }


        public virtual long? TimeFilterId
        {
            get
            {
                return timeFilterId;
            }
            set
            {
                this.timeFilterId = value;
            }
        }


        public virtual string TimeFilterName
        {
            get
            {
                return timeFilterName;
            }
            set
            {
                this.timeFilterName = value;
            }
        }


        public virtual bool? TimeFilterEnabled
        {
            get
            {
                return timeFilterEnabled;
            }
            set
            {
                this.timeFilterEnabled = value;
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
            return "Resource [id=" + id + ", name=" + name + ", failedAttemptsBeforeLock=" + failedAttemptsBeforeLock + ", geoFilterId=" + geoFilterId + ", geoFilterName="
                    + geoFilterName + ", geoFilterEnabled=" + geoFilterEnabled + ", timeFilterId=" + timeFilterId + ", timeFilterName="
                    + timeFilterName + ", timeFilterEnabled=" + timeFilterEnabled + ", creatorId=" + creatorId + ", creatorUsername="
                    + creatorUsername + "]";
        }

    }

}
