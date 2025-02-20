using IdentityProvider.Protectimus.Enums;

namespace IdentityProvider.Protectimus.Filters
{
    public class UserFilter
    {

        private string login;
        private string email;
        private string firstName;
        private string secondName;
        private UserBlockType block;
        private string resourceIds;

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
                this.email = value;
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
                this.firstName = value;
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
                this.secondName = value;
            }
        }


        public virtual UserBlockType Block
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

    }

}
