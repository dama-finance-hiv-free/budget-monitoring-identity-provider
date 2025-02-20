using System.Collections.Generic;

namespace IdentityProvider.Protectimus.Enums
{
    public sealed class TokenType
    {
        public static readonly TokenType GOOGLE_AUTHENTICATOR = new TokenType("GOOGLE_AUTHENTICATOR", InnerEnum.GOOGLE_AUTHENTICATOR, Type.SOFTWARE);
        public static readonly TokenType PROTECTIMUS = new TokenType("PROTECTIMUS", InnerEnum.PROTECTIMUS, Type.HARDWARE);
        public static readonly TokenType SAFENET_ETOKEN_PASS = new TokenType("SAFENET_ETOKEN_PASS", InnerEnum.SAFENET_ETOKEN_PASS, Type.HARDWARE);
        public static readonly TokenType SMS = new TokenType("SMS", InnerEnum.SMS, Type.SOFTWARE);
        public static readonly TokenType MAIL = new TokenType("MAIL", InnerEnum.MAIL, Type.SOFTWARE);
        public static readonly TokenType PROTECTIMUS_ULTRA = new TokenType("PROTECTIMUS_ULTRA", InnerEnum.PROTECTIMUS_ULTRA, Type.HARDWARE);
        public static readonly TokenType PROTECTIMUS_SMART = new TokenType("PROTECTIMUS_SMART", InnerEnum.PROTECTIMUS_SMART, Type.SOFTWARE);
        public static readonly TokenType YUBICO_OATH_MODE = new TokenType("YUBICO_OATH_MODE", InnerEnum.YUBICO_OATH_MODE, Type.HARDWARE);
        public static readonly TokenType UNIFY_OATH_TOKEN = new TokenType("UNIFY_OATH_TOKEN", InnerEnum.UNIFY_OATH_TOKEN, Type.SOFTWARE);
        public static readonly TokenType PROTECTIMUS_SLIM = new TokenType("PROTECTIMUS_SLIM", InnerEnum.PROTECTIMUS_SLIM, Type.HARDWARE);

        private static readonly IList<TokenType> valueList = new List<TokenType>();

        static TokenType()
        {
            valueList.Add(GOOGLE_AUTHENTICATOR);
            valueList.Add(PROTECTIMUS);
            valueList.Add(SAFENET_ETOKEN_PASS);
            valueList.Add(SMS);
            valueList.Add(MAIL);
            valueList.Add(PROTECTIMUS_ULTRA);
            valueList.Add(PROTECTIMUS_SMART);
            valueList.Add(YUBICO_OATH_MODE);
            valueList.Add(UNIFY_OATH_TOKEN);
            valueList.Add(PROTECTIMUS_SLIM);
        }

        public enum InnerEnum
        {
            GOOGLE_AUTHENTICATOR,
            PROTECTIMUS,
            SAFENET_ETOKEN_PASS,
            SMS,
            MAIL,
            PROTECTIMUS_ULTRA,
            PROTECTIMUS_SMART,
            YUBICO_OATH_MODE,
            UNIFY_OATH_TOKEN,
            PROTECTIMUS_SLIM
        }

        public readonly InnerEnum innerEnumValue;
        private readonly string nameValue;
        private readonly int ordinalValue;
        private static int nextOrdinal = 0;

        private Type type;

        private TokenType(string name, InnerEnum innerEnum, Type type)
        {
            this.type = type;

            nameValue = name;
            ordinalValue = nextOrdinal++;
            innerEnumValue = innerEnum;
        }

        public Type Type
        {
            get
            {
                return type;
            }
        }


        public static IList<TokenType> values()
        {
            return valueList;
        }

        public int ordinal()
        {
            return ordinalValue;
        }

        public override string ToString()
        {
            return nameValue;
        }

        public static TokenType valueOf(string name)
        {
            foreach (TokenType enumInstance in TokenType.valueList)
            {
                if (enumInstance.nameValue == name)
                {
                    return enumInstance;
                }
            }
            throw new System.ArgumentException(name);
        }
    }

}
