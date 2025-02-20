namespace IdentityProvider.Protectimus.Poco
{
    public class Prepare
    {

        private string challenge;
        private string tokenName;
        private string tokenType;

        public virtual string Challenge
        {
            get
            {
                return challenge;
            }
            set
            {
                this.challenge = value;
            }
        }


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


        public virtual string TokenType
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


        public override string ToString()
        {
            return "Prepare[ challenge=" + challenge + ", tokenName=" + tokenName + ", tokenType=" + tokenType + "]";
        }
    }

}
