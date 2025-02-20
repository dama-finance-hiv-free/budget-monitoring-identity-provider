using System;
using System.Collections.Generic;

namespace IdentityProvider.Protectimus.Exceptions
{
    public class ProtectimusApiException : Exception
    {

        private const long serialVersionUID = 6507605421894258322L;
        private ErrorCode errorCode;
        private string developerMessage;
        private int httpResponseStatusCode;

        public ProtectimusApiException(string message, string developerMessage, Exception cause, ErrorCode errorCode) : base(message, cause)
        {
            this.developerMessage = developerMessage;
            this.errorCode = errorCode;
        }

        public ProtectimusApiException(string message, string developerMessage, ErrorCode errorCode) : base(message)
        {
            this.developerMessage = developerMessage;
            this.errorCode = errorCode;
        }

        public ProtectimusApiException(string message, string developerMessage, Exception cause, ErrorCode errorCode, int httpResponseStatusCode) : base(message, cause)
        {
            this.developerMessage = developerMessage;
            this.errorCode = errorCode;
            this.httpResponseStatusCode = httpResponseStatusCode;
        }

        public ProtectimusApiException(string message, string developerMessage, ErrorCode errorCode, int httpResponseStatusCode) : base(message)
        {
            this.developerMessage = developerMessage;
            this.errorCode = errorCode;
            this.httpResponseStatusCode = httpResponseStatusCode;
        }

        public sealed class ErrorCode
        {

            // duplicated exceptions
            public static readonly ErrorCode ALREADY_EXIST = new ErrorCode("ALREADY_EXIST", InnerEnum.ALREADY_EXIST, 1001);

            // size exceptions
            public static readonly ErrorCode INVALID_PARAMETER_LENGTH = new ErrorCode("INVALID_PARAMETER_LENGTH", InnerEnum.INVALID_PARAMETER_LENGTH, 2001);

            // database exceptions
            public static readonly ErrorCode DB_ERROR = new ErrorCode("DB_ERROR", InnerEnum.DB_ERROR, 3001);

            // unregistered exceptions
            public static readonly ErrorCode UNREGISTERED_NAME = new ErrorCode("UNREGISTERED_NAME", InnerEnum.UNREGISTERED_NAME, 4001);

            // missing information exceptions
            public static readonly ErrorCode MISSING_PARAMETER = new ErrorCode("MISSING_PARAMETER", InnerEnum.MISSING_PARAMETER, 5001);
            public static readonly ErrorCode MISSING_DB_ENTITY = new ErrorCode("MISSING_DB_ENTITY", InnerEnum.MISSING_DB_ENTITY, 5002);

            // invalid information exceptions
            public static readonly ErrorCode INVALID_PARAMETER = new ErrorCode("INVALID_PARAMETER", InnerEnum.INVALID_PARAMETER, 6001);
            public static readonly ErrorCode INVALID_URL_PATTERN = new ErrorCode("INVALID_URL_PATTERN", InnerEnum.INVALID_URL_PATTERN, 6002);

            // access restriction exceptions
            public static readonly ErrorCode ACCESS_RESTRICTION = new ErrorCode("ACCESS_RESTRICTION", InnerEnum.ACCESS_RESTRICTION, 7001);

            // internal server error
            public static readonly ErrorCode INTERNAL_SERVER_ERROR = new ErrorCode("INTERNAL_SERVER_ERROR", InnerEnum.INTERNAL_SERVER_ERROR, 8001);

            // unknown error exception
            public static readonly ErrorCode UNKNOWN_ERROR = new ErrorCode("UNKNOWN_ERROR", InnerEnum.UNKNOWN_ERROR, 9001);

            private static readonly IList<ErrorCode> valueList = new List<ErrorCode>();

            static ErrorCode()
            {
                valueList.Add(ALREADY_EXIST);
                valueList.Add(INVALID_PARAMETER_LENGTH);
                valueList.Add(DB_ERROR);
                valueList.Add(UNREGISTERED_NAME);
                valueList.Add(MISSING_PARAMETER);
                valueList.Add(MISSING_DB_ENTITY);
                valueList.Add(INVALID_PARAMETER);
                valueList.Add(INVALID_URL_PATTERN);
                valueList.Add(ACCESS_RESTRICTION);
                valueList.Add(INTERNAL_SERVER_ERROR);
                valueList.Add(UNKNOWN_ERROR);
            }

            public enum InnerEnum
            {
                ALREADY_EXIST,
                INVALID_PARAMETER_LENGTH,
                DB_ERROR,
                UNREGISTERED_NAME,
                MISSING_PARAMETER,
                MISSING_DB_ENTITY,
                INVALID_PARAMETER,
                INVALID_URL_PATTERN,
                ACCESS_RESTRICTION,
                INTERNAL_SERVER_ERROR,
                UNKNOWN_ERROR
            }

            public readonly InnerEnum innerEnumValue;
            private readonly string nameValue;
            private readonly int ordinalValue;
            private static int nextOrdinal = 0;

            internal readonly int code;

            internal ErrorCode(string name, InnerEnum innerEnum, int code)
            {
                this.code = code;

                nameValue = name;
                ordinalValue = nextOrdinal++;
                innerEnumValue = innerEnum;
            }

            public int Code
            {
                get
                {
                    return code;
                }
            }

            public static IList<ErrorCode> values()
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

            public static ErrorCode valueOf(string name)
            {
                foreach (ErrorCode enumInstance in ErrorCode.valueList)
                {
                    if (enumInstance.nameValue == name)
                    {
                        return enumInstance;
                    }
                }
                throw new System.ArgumentException(name);
            }
        }

        public virtual ErrorCode getErrorCode()
        {
            return errorCode;
        }

        public virtual string DeveloperMessage
        {
            get
            {
                return developerMessage;
            }
        }

        public virtual int HttpResponseStatusCode
        {
            get
            {
                return httpResponseStatusCode;
            }
        }
    }

}
