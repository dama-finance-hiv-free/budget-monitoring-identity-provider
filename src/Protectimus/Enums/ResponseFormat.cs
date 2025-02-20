using System.Collections.Generic;

namespace IdentityProvider.Protectimus.Enums
{
    public sealed class ResponseFormat
    {
        public static readonly ResponseFormat Xml = new ResponseFormat("XML", InnerEnum.Xml, ".xml");
        public static readonly ResponseFormat Json = new ResponseFormat("JSON", InnerEnum.Json, ".json");

        private static readonly IList<ResponseFormat> ValueList = new List<ResponseFormat>();

        static ResponseFormat()
        {
            ValueList.Add(Xml);
            ValueList.Add(Json);
        }

        public enum InnerEnum
        {
            Xml,
            Json
        }

        public readonly InnerEnum InnerEnumValue;
        private readonly string _nameValue;
        private readonly int _ordinalValue;
        private static int _nextOrdinal;

        private ResponseFormat(string name, InnerEnum innerEnum, string extension)
        {
            Extension = extension;

            _nameValue = name;
            _ordinalValue = _nextOrdinal++;
            InnerEnumValue = innerEnum;
        }

        public string Extension { get; }


        public static IList<ResponseFormat> Values()
        {
            return ValueList;
        }

        public int Ordinal()
        {
            return _ordinalValue;
        }

        public override string ToString()
        {
            return _nameValue;
        }

        public static ResponseFormat ValueOf(string name)
        {
            foreach (var enumInstance in ValueList)
            {
                if (enumInstance._nameValue == name)
                {
                    return enumInstance;
                }
            }
            throw new System.ArgumentException(name);
        }
    }

}
