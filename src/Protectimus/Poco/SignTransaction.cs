namespace IdentityProvider.Protectimus.Poco
{
    public class SignTransaction
    {

        private string challenge;
        private string transactionData;
        private string tokenName;
        private long tokenId;
        private string tokenType;

        public SignTransaction()
        {
        }

        public SignTransaction(string ocraQuestion, string transactionData, string tokenName, long tokenId, string tokenType)
        {
            this.challenge = ocraQuestion;
            this.transactionData = transactionData;
            this.tokenName = tokenName;
            this.tokenId = tokenId;
            this.tokenType = tokenType;
        }

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

        public virtual string TransactionData
        {
            get
            {
                return transactionData;
            }
            set
            {
                this.transactionData = value;
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

        public virtual long TokenId
        {
            get
            {
                return tokenId;
            }
            set
            {
                this.tokenId = value;
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
    }

}
