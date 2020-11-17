using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using IxMilia.Dxf.Sections;

namespace IxMilia.Dxf
{
    public partial class DxfCodePair : IDxfCodePairOrGroup
    {
        public const int CommentCode = 999;

        private KeyValuePair<int, object> data;

        public bool IsCodePair { get { return true; } }

        public int Code
        {
            get { return data.Key; }
            set { data = new KeyValuePair<int, object>(value, data.Value); }
        }

        public object Value
        {
            get { return data.Value; }
            set { data = new KeyValuePair<int, object>(data.Key, value); }
        }

        public string StringValue
        {
            get { return (string)Value; }
        }

        public double DoubleValue
        {
            get { return (double)Value; }
        }

        public short ShortValue
        {
            get { return (short)Value; }
        }

        public int IntegerValue
        {
            get { return (int)Value; }
        }

        public long LongValue
        {
            get { return (long)Value; }
        }

        public bool BoolValue
        {
            get
            {
                switch (Value)
                {
                    case short s:
                        return s != 0;
                    case bool b:
                        return b;
                    default:
                        return false;
                }
            }
        }

        public byte[] BinaryValue
        {
            get { return (byte[])Value; }
        }

        public DxfCodePair(int code, string value)
        {
            Debug.Assert(ExpectedType(code) == typeof(string));
            data = new KeyValuePair<int, object>(code, value ?? string.Empty);
        }

        public DxfCodePair(int code, double value)
        {
            Debug.Assert(ExpectedType(code) == typeof(double));
            data = new KeyValuePair<int, object>(code, value);
        }

        public DxfCodePair(int code, short value)
        {
            // some code pairs in the spec expect code 290 shorts even though the spec says code 290
            // should really be a bool
            if (!IsPotentialShortAsBool(code))
                Debug.Assert(ExpectedType(code) == typeof(short));
            data = new KeyValuePair<int, object>(code, value);
        }

        public DxfCodePair(int code, int value)
        {
            Debug.Assert(ExpectedType(code) == typeof(int));
            data = new KeyValuePair<int, object>(code, value);
        }

        public DxfCodePair(int code, long value)
        {
            Debug.Assert(ExpectedType(code) == typeof(long));
            data = new KeyValuePair<int, object>(code, value);
        }

        public DxfCodePair(int code, bool value)
        {
            Debug.Assert(ExpectedType(code) == typeof(bool));
            data = new KeyValuePair<int, object>(code, value);
        }

        public DxfCodePair(int code, byte[] value)
        {
            Debug.Assert(ExpectedType(code) == typeof(byte[]));
            data = new KeyValuePair<int, object>(code, value);
        }

        /// <summary>
        /// Internal for specific cases where the type isn't known.
        /// </summary>
        internal DxfCodePair(int code, object value)
        {
            // it's annoying to always cast, this is just a convenience helper
            var expectedType = ExpectedType(code);
            if (value?.GetType() == typeof(int))
            {
                if (expectedType == typeof(short) || expectedType == typeof(bool))
                {
                    value = Convert.ToInt16(value);
                }
                else if (expectedType == typeof(long))
                {
                    value = Convert.ToInt64(value);
                }
            }
            else if (value?.GetType() == typeof(string) && expectedType != typeof(string))
            {
                throw new InvalidOperationException($"Illegal cast to string for code {code}; expected type is {expectedType.Name}");
            }

            data = new KeyValuePair<int, object>(code, value);
        }

        internal static bool IsPotentialShortAsBool(int code)
        {
            return code >= 290 && code <= 299;
        }

        public override string ToString()
        {
            return $"[{DxfWriter.CodeAsString(Code)}: {Value ?? "<null>"}]";
        }

        public static bool IsSectionStart(DxfCodePair pair)
        {
            return pair.Code == 0 && pair.StringValue == DxfSection.SectionText;
        }

        public static bool IsSectionEnd(DxfCodePair pair)
        {
            return pair.Code == 0 && pair.StringValue == DxfSection.EndSectionText;
        }

        public static bool IsEof(DxfCodePair pair)
        {
            return pair.Code == 0 && pair.StringValue == DxfFile.EofText;
        }

        public static bool IsComment(DxfCodePair pair)
        {
            return pair.Code == CommentCode;
        }

        public static bool operator ==(DxfCodePair a, DxfCodePair b)
        {
            if (ReferenceEquals(a, b))
                return true;
            if (((object)a) == null || ((object)b) == null)
                return false;
            return a.Code == b.Code && ValuesEqual(a.Value, b.Value);
        }

        private static bool ValuesEqual(object a, object b)
        {
            if (a?.GetType() == b?.GetType() && a?.GetType() == typeof(byte[]))
            {
                var aa = (byte[])a;
                var ba = (byte[])b;
                if (ReferenceEquals(aa, ba))
                    return true;
                if (aa.Length != ba.Length)
                    return false;
                for (int i = 0; i < aa.Length; i++)
                {
                    if (aa[i] != ba[i])
                        return false;
                }

                return true;
            }

            return a.Equals(b);
        }

        public static bool operator !=(DxfCodePair a, DxfCodePair b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            var hash = Code.GetHashCode();
            if (Value != null)
            {
                hash ^= Value.GetHashCode();
            }

            return hash;
        }

        public override bool Equals(object obj)
        {
            if (obj is DxfCodePair)
                return this == (DxfCodePair)obj;
            return false;
        }
    }
}
