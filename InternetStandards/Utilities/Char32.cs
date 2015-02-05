namespace System
{
    using System.Globalization;
    using System.Runtime.InteropServices;

    /// <summary>Represents a Unicode character.</summary>
    /// <filterpriority>1</filterpriority>
    [Serializable, StructLayout(LayoutKind.Sequential), ComVisible(true)]
    public struct Char32 : IComparable, IConvertible, IComparable<Char32>, IEquatable<Char32>
    {
        /// <summary>Represents the largest possible value of a <see cref="T:System.Char"></see>. This field is constant.</summary>
        /// <filterpriority>1</filterpriority>
        public const Char32 MaxValue = 0x10FFFF;
        /// <summary>Represents the smallest possible value of a <see cref="T:System.Char"></see>. This field is constant.</summary>
        /// <filterpriority>1</filterpriority>
        public const Char32 MinValue = 0;
        internal const int UNICODE_PLANE00_END = 0xffff;
        internal const int UNICODE_PLANE01_START = 0x10000;
        internal const int UNICODE_PLANE16_END = 0x10ffff;
        internal const int HIGH_SURROGATE_START = 0xd800;
        internal const int LOW_SURROGATE_END = 0xdfff;
        internal char m_value;

        /// <summary>Returns the hash code for this instance.</summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            return (this | (this << 0x10));
        }

        /// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
        /// <returns>true if obj is an instance of <see cref="T:System.Char"></see> and equals the value of this instance; otherwise, false.</returns>
        /// <param name="obj">An object to compare with this instance or null. </param>
        /// <filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            return ((obj is char) && (this == ((char) obj)));
        }

        /// <summary>Returns a value indicating whether this instance is equal to the specified <see cref="T:System.Char"></see> object.</summary>
        /// <returns>true if the value parameter equals the value of this instance; otherwise, false.</returns>
        /// <param name="obj">A <see cref="T:System.Char"></see> object to compare to this instance. </param>
        /// <filterpriority>2</filterpriority>
        public bool Equals(char obj)
        {
            return (this == obj);
        }

        /// <summary>Compares this instance to a specified object and returns an indication of their relative values.</summary>
        /// <returns>A signed number indicating the relative values of this instance and the value parameter.Return Value Description Less than zero This instance is less than value. Zero This instance is equal to value. Greater than zero This instance is greater than value.-or- value is null. </returns>
        /// <param name="value">An object to compare this instance to, or null. </param>
        /// <exception cref="T:System.ArgumentException">value is not a <see cref="T:System.Char"></see> object. </exception>
        /// <filterpriority>2</filterpriority>
        public int CompareTo(object value)
        {
            if (value == null)
            {
                return 1;
            }
            if (!(value is char))
            {
                throw new ArgumentException(Environment.GetResourceString("Arg_MustBeChar"));
            }
            return (this - ((char) value));
        }

        /// <summary>Compares this instance to a specified <see cref="T:System.Char"></see> object and returns an indication of their relative values.</summary>
        /// <returns>A signed number indicating the relative values of this instance and the value parameter.Return Value Description Less than zero This instance is less than value. Zero This instance is equal to value. Greater than zero This instance is greater than value. </returns>
        /// <param name="value">A <see cref="T:System.Char"></see> object to compare. </param>
        /// <filterpriority>2</filterpriority>
        public int CompareTo(char value)
        {
            return (this - value);
        }

        /// <summary>Converts the value of this instance to its equivalent string representation.</summary>
        /// <returns>The string representation of the value of this instance.</returns>
        /// <filterpriority>1</filterpriority>
        public override string ToString()
        {
            return ToString(this);
        }

        /// <summary>Converts the value of this instance to its equivalent string representation using the specified culture-specific format information.</summary>
        /// <returns>The string representation of the value of this instance as specified by provider.</returns>
        /// <param name="provider">(Reserved) An <see cref="T:System.IFormatProvider"></see> that supplies culture-specific formatting information. </param>
        /// <filterpriority>1</filterpriority>
        public string ToString(IFormatProvider provider)
        {
            return ToString(this);
        }

        /// <summary>Converts the specified Unicode character to its equivalent string representation.</summary>
        /// <returns>The string representation of the value of c.</returns>
        /// <param name="c">A Unicode character. </param>
        /// <filterpriority>1</filterpriority>
        public static string ToString(char c)
        {
            return new string(c, 1);
        }

        /// <summary>Converts the value of the specified string to its equivalent Unicode character.</summary>
        /// <returns>A Unicode character equivalent to the sole character in s.</returns>
        /// <param name="s">A string containing a single character or null. </param>
        /// <exception cref="T:System.ArgumentNullException">s is null. </exception>
        /// <exception cref="T:System.FormatException">The length of s is not 1. </exception>
        /// <filterpriority>1</filterpriority>
        public static char Parse(string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }
            if (s.Length != 1)
            {
                throw new FormatException(Environment.GetResourceString("Format_NeedSingleChar"));
            }
            return s[0];
        }

        /// <summary>Converts the value of the specified string to its equivalent Unicode character. A return code indicates whether the conversion succeeded or failed.</summary>
        /// <returns>true if the s parameter was converted successfully; otherwise, false.</returns>
        /// <param name="s">A string containing a single character or null. </param>
        /// <param name="result">When this method returns, contains a Unicode character equivalent to the sole character in s, if the conversion succeeded, or an undefined value if the conversion failed. The conversion fails if the s parameter is null or the length of s is not 1. This parameter is passed uninitialized. </param>
        /// <filterpriority>1</filterpriority>
        public static bool TryParse(string s, out char result)
        {
            result = '\0';
            if (s == null)
            {
                return false;
            }
            if (s.Length != 1)
            {
                return false;
            }
            result = s[0];
            return true;
        }

        /// <summary>Indicates whether the specified Unicode character is categorized as a decimal digit.</summary>
        /// <returns>true if c is a decimal digit; otherwise, false.</returns>
        /// <param name="c">A Unicode character. </param>
        /// <filterpriority>1</filterpriority>
        public static bool IsDigit(char c)
        {
            if (!IsLatin1(c))
            {
                return (CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.DecimalDigitNumber);
            }
            return ((c >= '0') && (c <= '9'));
        }

        internal static bool CheckLetter(UnicodeCategory uc)
        {
            switch (uc)
            {
                case UnicodeCategory.UppercaseLetter:
                case UnicodeCategory.LowercaseLetter:
                case UnicodeCategory.TitlecaseLetter:
                case UnicodeCategory.ModifierLetter:
                case UnicodeCategory.OtherLetter:
                    return true;
            }
            return false;
        }

        /// <summary>Indicates whether the specified Unicode character is categorized as an alphabetic letter.</summary>
        /// <returns>true if c is an alphabetic letter; otherwise, false.</returns>
        /// <param name="c">A Unicode character. </param>
        /// <filterpriority>1</filterpriority>
        public static bool IsLetter(char c)
        {
            if (!IsLatin1(c))
            {
                return CheckLetter(CharUnicodeInfo.GetUnicodeCategory(c));
            }
            if (!IsAscii(c))
            {
                return CheckLetter(GetLatin1UnicodeCategory(c));
            }
            c = (char) (c | ' ');
            return ((c >= 'a') && (c <= 'z'));
        }

        private static bool IsWhiteSpaceLatin1(char c)
        {
            if (((c != ' ') && ((c < '\t') || (c > '\r'))) && ((c != '\x00a0') && (c != '\x0085')))
            {
                return false;
            }
            return true;
        }

        /// <summary>Indicates whether the specified Unicode character is categorized as white space.</summary>
        /// <returns>true if c is white space; otherwise, false.</returns>
        /// <param name="c">A Unicode character. </param>
        /// <filterpriority>1</filterpriority>
        public static bool IsWhiteSpace(char c)
        {
            if (IsLatin1(c))
            {
                return IsWhiteSpaceLatin1(c);
            }
            return CharUnicodeInfo.IsWhiteSpace(c);
        }

        /// <summary>Indicates whether the specified Unicode character is categorized as an uppercase letter.</summary>
        /// <returns>true if c is an uppercase letter; otherwise, false.</returns>
        /// <param name="c">A Unicode character. </param>
        /// <filterpriority>1</filterpriority>
        public static bool IsUpper(char c)
        {
            if (!IsLatin1(c))
            {
                return (CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.UppercaseLetter);
            }
            if (!IsAscii(c))
            {
                return (GetLatin1UnicodeCategory(c) == UnicodeCategory.UppercaseLetter);
            }
            return ((c >= 'A') && (c <= 'Z'));
        }

        /// <summary>Indicates whether the specified Unicode character is categorized as a lowercase letter.</summary>
        /// <returns>true if c is a lowercase letter; otherwise, false.</returns>
        /// <param name="c">A Unicode character. </param>
        /// <filterpriority>1</filterpriority>
        public static bool IsLower(char c)
        {
            if (!IsLatin1(c))
            {
                return (CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.LowercaseLetter);
            }
            if (!IsAscii(c))
            {
                return (GetLatin1UnicodeCategory(c) == UnicodeCategory.LowercaseLetter);
            }
            return ((c >= 'a') && (c <= 'z'));
        }

        internal static bool CheckPunctuation(UnicodeCategory uc)
        {
            switch (uc)
            {
                case UnicodeCategory.ConnectorPunctuation:
                case UnicodeCategory.DashPunctuation:
                case UnicodeCategory.OpenPunctuation:
                case UnicodeCategory.ClosePunctuation:
                case UnicodeCategory.InitialQuotePunctuation:
                case UnicodeCategory.FinalQuotePunctuation:
                case UnicodeCategory.OtherPunctuation:
                    return true;
            }
            return false;
        }

        /// <summary>Indicates whether the specified Unicode character is categorized as a punctuation mark.</summary>
        /// <returns>true if c is a punctuation mark; otherwise, false.</returns>
        /// <param name="c">A Unicode character. </param>
        /// <filterpriority>1</filterpriority>
        public static bool IsPunctuation(char c)
        {
            if (IsLatin1(c))
            {
                return CheckPunctuation(GetLatin1UnicodeCategory(c));
            }
            return CheckPunctuation(CharUnicodeInfo.GetUnicodeCategory(c));
        }

        internal static bool CheckLetterOrDigit(UnicodeCategory uc)
        {
            switch (uc)
            {
                case UnicodeCategory.UppercaseLetter:
                case UnicodeCategory.LowercaseLetter:
                case UnicodeCategory.TitlecaseLetter:
                case UnicodeCategory.ModifierLetter:
                case UnicodeCategory.OtherLetter:
                case UnicodeCategory.DecimalDigitNumber:
                    return true;
            }
            return false;
        }

        /// <summary>Indicates whether the specified Unicode character is categorized as an alphabetic letter or a decimal digit.</summary>
        /// <returns>true if c is an alphabetic letter or a decimal digit; otherwise, false.</returns>
        /// <param name="c">A Unicode character. </param>
        /// <filterpriority>1</filterpriority>
        public static bool IsLetterOrDigit(char c)
        {
            if (IsLatin1(c))
            {
                return CheckLetterOrDigit(GetLatin1UnicodeCategory(c));
            }
            return CheckLetterOrDigit(CharUnicodeInfo.GetUnicodeCategory(c));
        }

        /// <summary>Converts the value of a specified Unicode character to its uppercase equivalent using specified culture-specific formatting information.</summary>
        /// <returns>The uppercase equivalent of c, modified according to culture, or the unchanged value of c, if c is already uppercase or not alphabetic.</returns>
        /// <param name="culture">A <see cref="T:System.Globalization.CultureInfo"></see> object that supplies culture-specific casing rules, or null. </param>
        /// <param name="c">A Unicode character. </param>
        /// <exception cref="T:System.ArgumentNullException">culture is null. </exception>
        /// <filterpriority>1</filterpriority>
        public static char ToUpper(char c, CultureInfo culture)
        {
            if (culture == null)
            {
                throw new ArgumentNullException("culture");
            }
            return culture.TextInfo.ToUpper(c);
        }

        /// <summary>Converts the value of a Unicode character to its uppercase equivalent.</summary>
        /// <returns>The uppercase equivalent of c, or the unchanged value of c, if c is already uppercase or not alphabetic.</returns>
        /// <param name="c">A Unicode character. </param>
        /// <filterpriority>1</filterpriority>
        public static char ToUpper(char c)
        {
            return ToUpper(c, CultureInfo.CurrentCulture);
        }

        /// <summary>Converts the value of a Unicode character to its uppercase equivalent using the casing rules of the invariant culture.</summary>
        /// <returns>The uppercase equivalent of the c parameter, or the unchanged value of c, if c is already uppercase or not alphabetic.</returns>
        /// <param name="c">A Unicode character. </param>
        /// <filterpriority>1</filterpriority>
        public static char ToUpperInvariant(char c)
        {
            return ToUpper(c, CultureInfo.InvariantCulture);
        }

        /// <summary>Converts the value of a specified Unicode character to its lowercase equivalent using specified culture-specific formatting information.</summary>
        /// <returns>The lowercase equivalent of c, modified according to culture, or the unchanged value of c, if c is already lowercase or not alphabetic.</returns>
        /// <param name="culture">A <see cref="T:System.Globalization.CultureInfo"></see> object that supplies culture-specific casing rules, or null. </param>
        /// <param name="c">A Unicode character. </param>
        /// <exception cref="T:System.ArgumentNullException">culture is null. </exception>
        /// <filterpriority>1</filterpriority>
        public static char ToLower(char c, CultureInfo culture)
        {
            if (culture == null)
            {
                throw new ArgumentNullException("culture");
            }
            return culture.TextInfo.ToLower(c);
        }

        /// <summary>Converts the value of a Unicode character to its lowercase equivalent.</summary>
        /// <returns>The lowercase equivalent of c, or the unchanged value of c, if c is already lowercase or not alphabetic.</returns>
        /// <param name="c">A Unicode character. </param>
        /// <filterpriority>1</filterpriority>
        public static char ToLower(char c)
        {
            return ToLower(c, CultureInfo.CurrentCulture);
        }

        /// <summary>Converts the value of a Unicode character to its lowercase equivalent using the casing rules of the invariant culture.</summary>
        /// <returns>The lowercase equivalent of the c parameter, or the unchanged value of c, if c is already lowercase or not alphabetic.</returns>
        /// <param name="c">A Unicode character. </param>
        /// <filterpriority>1</filterpriority>
        public static char ToLowerInvariant(char c)
        {
            return ToLower(c, CultureInfo.InvariantCulture);
        }

        /// <summary>Returns the <see cref="T:System.TypeCode"></see> for value type <see cref="T:System.Char"></see>.</summary>
        /// <returns>The enumerated constant, <see cref="F:System.TypeCode.Char"></see>.</returns>
        /// <filterpriority>2</filterpriority>
        public TypeCode GetTypeCode()
        {
            return TypeCode.Char;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            throw new InvalidCastException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("InvalidCast_FromTo"), new object[] { "Char", "Boolean" }));
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            return this;
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte(this);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            return Convert.ToByte(this);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            return Convert.ToInt16(this);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            return Convert.ToUInt16(this);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            return Convert.ToInt32(this);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            return Convert.ToUInt32(this);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            return Convert.ToInt64(this);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            return Convert.ToUInt64(this);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            throw new InvalidCastException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("InvalidCast_FromTo"), new object[] { "Char", "Single" }));
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            throw new InvalidCastException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("InvalidCast_FromTo"), new object[] { "Char", "Double" }));
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            throw new InvalidCastException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("InvalidCast_FromTo"), new object[] { "Char", "Decimal" }));
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            throw new InvalidCastException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("InvalidCast_FromTo"), new object[] { "Char", "DateTime" }));
        }

        object IConvertible.ToType(Type type, IFormatProvider provider)
        {
            return Convert.DefaultToType(this, type, provider);
        }

        /// <summary>Indicates whether the specified Unicode character is categorized as a control character.</summary>
        /// <returns>true if c is a control character; otherwise, false.</returns>
        /// <param name="c">A Unicode character. </param>
        /// <filterpriority>1</filterpriority>
        public static bool IsControl(char c)
        {
            if (IsLatin1(c))
            {
                return (GetLatin1UnicodeCategory(c) == UnicodeCategory.Control);
            }
            return (CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.Control);
        }

        /// <summary>Indicates whether the character at the specified position in a specified string is categorized as a control character.</summary>
        /// <returns>true if the character at position index in s is a control character; otherwise, false.</returns>
        /// <param name="s">A string. </param>
        /// <param name="index">The character position in s. </param>
        /// <exception cref="T:System.ArgumentNullException">s is null. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">index is less than zero or greater than the last position in s. </exception>
        /// <filterpriority>1</filterpriority>
        public static bool IsControl(string s, int index)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }
            if (index >= s.Length)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            char ch = s[index];
            if (IsLatin1(ch))
            {
                return (GetLatin1UnicodeCategory(ch) == UnicodeCategory.Control);
            }
            return (CharUnicodeInfo.GetUnicodeCategory(s, index) == UnicodeCategory.Control);
        }

        /// <summary>Indicates whether the character at the specified position in a specified string is categorized as a decimal digit.</summary>
        /// <returns>true if the character at position index in s is a decimal digit; otherwise, false.</returns>
        /// <param name="s">A <see cref="T:System.String"></see>. </param>
        /// <param name="index">The character position in s. </param>
        /// <exception cref="T:System.ArgumentNullException">s is null. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">index is less than zero or greater than the last position in s. </exception>
        /// <filterpriority>1</filterpriority>
        public static bool IsDigit(string s, int index)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }
            if (index >= s.Length)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            char ch = s[index];
            if (!IsLatin1(ch))
            {
                return (CharUnicodeInfo.GetUnicodeCategory(s, index) == UnicodeCategory.DecimalDigitNumber);
            }
            return ((ch >= '0') && (ch <= '9'));
        }

        /// <summary>Indicates whether the character at the specified position in a specified string is categorized as an alphabetic character.</summary>
        /// <returns>true if the character at position index in s is an alphabetic character; otherwise, false.</returns>
        /// <param name="s">A string. </param>
        /// <param name="index">The character position in s. </param>
        /// <exception cref="T:System.ArgumentNullException">s is null. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">index is less than zero or greater than the last position in s. </exception>
        /// <filterpriority>1</filterpriority>
        public static bool IsLetter(string s, int index)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }
            if (index >= s.Length)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            char ch = s[index];
            if (!IsLatin1(ch))
            {
                return CheckLetter(CharUnicodeInfo.GetUnicodeCategory(s, index));
            }
            if (!IsAscii(ch))
            {
                return CheckLetter(GetLatin1UnicodeCategory(ch));
            }
            ch = (char) (ch | ' ');
            return ((ch >= 'a') && (ch <= 'z'));
        }

        /// <summary>Indicates whether the character at the specified position in a specified string is categorized as an alphabetic character or a decimal digit.</summary>
        /// <returns>true if the character at position index in s is an alphabetic character or a decimal digit; otherwise, false.</returns>
        /// <param name="s">A string. </param>
        /// <param name="index">The character position in s. </param>
        /// <exception cref="T:System.ArgumentNullException">s is null. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">index is less than zero or greater than the last position in s. </exception>
        /// <filterpriority>1</filterpriority>
        public static bool IsLetterOrDigit(string s, int index)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }
            if (index >= s.Length)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            char ch = s[index];
            if (IsLatin1(ch))
            {
                return CheckLetterOrDigit(GetLatin1UnicodeCategory(ch));
            }
            return CheckLetterOrDigit(CharUnicodeInfo.GetUnicodeCategory(s, index));
        }

        /// <summary>Indicates whether the character at the specified position in a specified string is categorized as a lowercase letter.</summary>
        /// <returns>true if the character at position index in s is a lowercase letter; otherwise, false.</returns>
        /// <param name="s">A string. </param>
        /// <param name="index">The character position in s. </param>
        /// <exception cref="T:System.ArgumentNullException">s is null. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">index is less than zero or greater than the last position in s. </exception>
        /// <filterpriority>1</filterpriority>
        public static bool IsLower(string s, int index)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }
            if (index >= s.Length)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            char ch = s[index];
            if (!IsLatin1(ch))
            {
                return (CharUnicodeInfo.GetUnicodeCategory(s, index) == UnicodeCategory.LowercaseLetter);
            }
            if (!IsAscii(ch))
            {
                return (GetLatin1UnicodeCategory(ch) == UnicodeCategory.LowercaseLetter);
            }
            return ((ch >= 'a') && (ch <= 'z'));
        }

        internal static bool CheckNumber(UnicodeCategory uc)
        {
            switch (uc)
            {
                case UnicodeCategory.DecimalDigitNumber:
                case UnicodeCategory.LetterNumber:
                case UnicodeCategory.OtherNumber:
                    return true;
            }
            return false;
        }

        /// <summary>Indicates whether the specified Unicode character is categorized as a number.</summary>
        /// <returns>true if c is a number; otherwise, false.</returns>
        /// <param name="c">A Unicode character. </param>
        /// <filterpriority>1</filterpriority>
        public static bool IsNumber(char c)
        {
            if (!IsLatin1(c))
            {
                return CheckNumber(CharUnicodeInfo.GetUnicodeCategory(c));
            }
            if (!IsAscii(c))
            {
                return CheckNumber(GetLatin1UnicodeCategory(c));
            }
            return ((c >= '0') && (c <= '9'));
        }

        /// <summary>Indicates whether the character at the specified position in a specified string is categorized as a number.</summary>
        /// <returns>true if the character at position index in s is a number; otherwise, false.</returns>
        /// <param name="s">A string. </param>
        /// <param name="index">The character position in s. </param>
        /// <exception cref="T:System.ArgumentNullException">s is null. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">index is less than zero or greater than the last position in s. </exception>
        /// <filterpriority>1</filterpriority>
        public static bool IsNumber(string s, int index)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }
            if (index >= s.Length)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            char ch = s[index];
            if (!IsLatin1(ch))
            {
                return CheckNumber(CharUnicodeInfo.GetUnicodeCategory(s, index));
            }
            if (!IsAscii(ch))
            {
                return CheckNumber(GetLatin1UnicodeCategory(ch));
            }
            return ((ch >= '0') && (ch <= '9'));
        }

        /// <summary>Indicates whether the character at the specified position in a specified string is categorized as a punctuation mark.</summary>
        /// <returns>true if the character at position index in s is a punctuation mark; otherwise, false.</returns>
        /// <param name="s">A string. </param>
        /// <param name="index">The character position in s. </param>
        /// <exception cref="T:System.ArgumentNullException">s is null. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">index is less than zero or greater than the last position in s. </exception>
        /// <filterpriority>1</filterpriority>
        public static bool IsPunctuation(string s, int index)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }
            if (index >= s.Length)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            char ch = s[index];
            if (IsLatin1(ch))
            {
                return CheckPunctuation(GetLatin1UnicodeCategory(ch));
            }
            return CheckPunctuation(CharUnicodeInfo.GetUnicodeCategory(s, index));
        }

        internal static bool CheckSeparator(UnicodeCategory uc)
        {
            switch (uc)
            {
                case UnicodeCategory.SpaceSeparator:
                case UnicodeCategory.LineSeparator:
                case UnicodeCategory.ParagraphSeparator:
                    return true;
            }
            return false;
        }

        private static bool IsSeparatorLatin1(char c)
        {
            if (c != ' ')
            {
                return (c == '\x00a0');
            }
            return true;
        }

        /// <summary>Indicates whether the specified Unicode character is categorized as a separator character.</summary>
        /// <returns>true if c is a separator character; otherwise, false.</returns>
        /// <param name="c">A Unicode character. </param>
        /// <filterpriority>1</filterpriority>
        public static bool IsSeparator(char c)
        {
            if (IsLatin1(c))
            {
                return IsSeparatorLatin1(c);
            }
            return CheckSeparator(CharUnicodeInfo.GetUnicodeCategory(c));
        }

        /// <summary>Indicates whether the character at the specified position in a specified string is categorized as a separator character.</summary>
        /// <returns>true if the character at position index in s is a separator character; otherwise, false.</returns>
        /// <param name="s">A string. </param>
        /// <param name="index">The character position in s. </param>
        /// <exception cref="T:System.ArgumentNullException">s is null. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">index is less than zero or greater than the last position in s. </exception>
        /// <filterpriority>1</filterpriority>
        public static bool IsSeparator(string s, int index)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }
            if (index >= s.Length)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            char ch = s[index];
            if (IsLatin1(ch))
            {
                return IsSeparatorLatin1(ch);
            }
            return CheckSeparator(CharUnicodeInfo.GetUnicodeCategory(s, index));
        }

        /// <summary>Indicates whether the specified Unicode character is categorized as a surrogate character.</summary>
        /// <returns>true if c is a surrogate character; otherwise, false.</returns>
        /// <param name="c">A Unicode character. </param>
        /// <filterpriority>1</filterpriority>
        public static bool IsSurrogate(char c)
        {
            return ((c >= 0xd800) && (c <= 0xdfff));
        }

        /// <summary>Indicates whether the character at the specified position in a specified string is categorized as a surrogate character.</summary>
        /// <returns>true if the character at position index in s is a surrogate character; otherwise, false.</returns>
        /// <param name="s">A string. </param>
        /// <param name="index">The character position in s. </param>
        /// <exception cref="T:System.ArgumentNullException">s is null. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">index is less than zero or greater than the last position in s. </exception>
        /// <filterpriority>1</filterpriority>
        public static bool IsSurrogate(string s, int index)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }
            if (index >= s.Length)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            return IsSurrogate(s[index]);
        }

        internal static bool CheckSymbol(UnicodeCategory uc)
        {
            switch (uc)
            {
                case UnicodeCategory.MathSymbol:
                case UnicodeCategory.CurrencySymbol:
                case UnicodeCategory.ModifierSymbol:
                case UnicodeCategory.OtherSymbol:
                    return true;
            }
            return false;
        }

        /// <summary>Indicates whether the specified Unicode character is categorized as a symbol character.</summary>
        /// <returns>true if c is a symbol character; otherwise, false.</returns>
        /// <param name="c">A Unicode character. </param>
        /// <filterpriority>1</filterpriority>
        public static bool IsSymbol(char c)
        {
            if (IsLatin1(c))
            {
                return CheckSymbol(GetLatin1UnicodeCategory(c));
            }
            return CheckSymbol(CharUnicodeInfo.GetUnicodeCategory(c));
        }

        /// <summary>Indicates whether the character at the specified position in a specified string is categorized as a symbol character.</summary>
        /// <returns>true if the character at position index in s is a symbol character; otherwise, false.</returns>
        /// <param name="s">A string. </param>
        /// <param name="index">The character position in s. </param>
        /// <exception cref="T:System.ArgumentNullException">s is null. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">index is less than zero or greater than the last position in s. </exception>
        /// <filterpriority>1</filterpriority>
        public static bool IsSymbol(string s, int index)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }
            if (index >= s.Length)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            if (IsLatin1(s[index]))
            {
                return CheckSymbol(GetLatin1UnicodeCategory(s[index]));
            }
            return CheckSymbol(CharUnicodeInfo.GetUnicodeCategory(s, index));
        }

        /// <summary>Indicates whether the character at the specified position in a specified string is categorized as an uppercase letter.</summary>
        /// <returns>true if the character at position index in s is an uppercase letter; otherwise, false.</returns>
        /// <param name="s">A string. </param>
        /// <param name="index">The character position in s. </param>
        /// <exception cref="T:System.ArgumentNullException">s is null. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">index is less than zero or greater than the last position in s. </exception>
        /// <filterpriority>1</filterpriority>
        public static bool IsUpper(string s, int index)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }
            if (index >= s.Length)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            char ch = s[index];
            if (!IsLatin1(ch))
            {
                return (CharUnicodeInfo.GetUnicodeCategory(s, index) == UnicodeCategory.UppercaseLetter);
            }
            if (!IsAscii(ch))
            {
                return (GetLatin1UnicodeCategory(ch) == UnicodeCategory.UppercaseLetter);
            }
            return ((ch >= 'A') && (ch <= 'Z'));
        }

        /// <summary>Indicates whether the character at the specified position in a specified string is categorized as white space.</summary>
        /// <returns>true if the character at position index in s is white space; otherwise, false.</returns>
        /// <param name="s">A string. </param>
        /// <param name="index">The character position in s. </param>
        /// <exception cref="T:System.ArgumentNullException">s is null. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">index is less than zero or greater than the last position in s. </exception>
        /// <filterpriority>1</filterpriority>
        public static bool IsWhiteSpace(string s, int index)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }
            if (index >= s.Length)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            if (IsLatin1(s[index]))
            {
                return IsWhiteSpaceLatin1(s[index]);
            }
            return CharUnicodeInfo.IsWhiteSpace(s, index);
        }

        /// <summary>Categorizes a specified Unicode character into a group identified by one of the <see cref="T:System.Globalization.UnicodeCategory"></see> values.</summary>
        /// <returns>A <see cref="T:System.Globalization.UnicodeCategory"></see> value that identifies the group that contains c.</returns>
        /// <param name="c">A Unicode character. </param>
        /// <filterpriority>1</filterpriority>
        public static UnicodeCategory GetUnicodeCategory(char c)
        {
            if (IsLatin1(c))
            {
                return GetLatin1UnicodeCategory(c);
            }
            return CharUnicodeInfo.InternalGetUnicodeCategory(c);
        }

        /// <summary>Categorizes the character at the specified position in a specified string into a group identified by one of the <see cref="T:System.Globalization.UnicodeCategory"></see> values.</summary>
        /// <returns>A <see cref="T:System.Globalization.UnicodeCategory"></see> enumerated constant that identifies the group that contains the character at position index in s.</returns>
        /// <param name="s">A <see cref="T:System.String"></see>. </param>
        /// <param name="index">The character position in s. </param>
        /// <exception cref="T:System.ArgumentNullException">s is null. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">index is less than zero or greater than the last position in s. </exception>
        /// <filterpriority>1</filterpriority>
        public static UnicodeCategory GetUnicodeCategory(string s, int index)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }
            if (index >= s.Length)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            if (IsLatin1(s[index]))
            {
                return GetLatin1UnicodeCategory(s[index]);
            }
            return CharUnicodeInfo.InternalGetUnicodeCategory(s, index);
        }

        /// <summary>Converts the specified numeric Unicode character to a double-precision floating point number.</summary>
        /// <returns>The numeric value of c if that character represents a number; otherwise, -1.0.</returns>
        /// <param name="c">A Unicode character. </param>
        /// <filterpriority>1</filterpriority>
        public static double GetNumericValue(char c)
        {
            return CharUnicodeInfo.GetNumericValue(c);
        }

        /// <summary>Converts the numeric Unicode character at the specified position in a specified string to a double-precision floating point number.</summary>
        /// <returns>The numeric value of the character at position index in s if that character represents a number; otherwise, -1.</returns>
        /// <param name="s">A <see cref="T:System.String"></see>. </param>
        /// <param name="index">The character position in s. </param>
        /// <exception cref="T:System.ArgumentNullException">s is null. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">index is less than zero or greater than the last position in s. </exception>
        /// <filterpriority>1</filterpriority>
        public static double GetNumericValue(string s, int index)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }
            if (index >= s.Length)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            return CharUnicodeInfo.GetNumericValue(s, index);
        }

        /// <summary>Indicates whether the specified <see cref="T:System.Char"></see> object is a high surrogate.</summary>
        /// <returns>true if the numeric value of the c parameter ranges from U+D800 through U+DBFF; otherwise, false.</returns>
        /// <param name="c">A character. </param>
        /// <filterpriority>1</filterpriority>
        public static bool IsHighSurrogate(char c)
        {
            return ((c >= 0xd800) && (c <= 0xdbff));
        }

        /// <summary>Indicates whether the <see cref="T:System.Char"></see> object at the specified position in a string is a high surrogate.</summary>
        /// <returns>true if the numeric value of the specified character in the s parameter ranges from U+D800 through U+DBFF; otherwise, false.</returns>
        /// <param name="s">A string. </param>
        /// <param name="index">A position within s. </param>
        /// <exception cref="T:System.ArgumentNullException">s is null. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">index is not a position within s. </exception>
        /// <filterpriority>1</filterpriority>
        public static bool IsHighSurrogate(string s, int index)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }
            if ((index < 0) || (index >= s.Length))
            {
                throw new ArgumentOutOfRangeException("index");
            }
            return IsHighSurrogate(s[index]);
        }

        /// <summary>Indicates whether the specified <see cref="T:System.Char"></see> object is a low surrogate.</summary>
        /// <returns>true if the numeric value of the c parameter ranges from U+DC00 through U+DFFF; otherwise, false.</returns>
        /// <param name="c">A character. </param>
        /// <filterpriority>1</filterpriority>
        public static bool IsLowSurrogate(char c)
        {
            return ((c >= 0xdc00) && (c <= 0xdfff));
        }

        /// <summary>Indicates whether the <see cref="T:System.Char"></see> object at the specified position in a string is a low surrogate.</summary>
        /// <returns>true if the numeric value of the specified character in the s parameter ranges from U+DC00 through U+DFFF; otherwise, false.</returns>
        /// <param name="s">A string. </param>
        /// <param name="index">A position within s. </param>
        /// <exception cref="T:System.ArgumentNullException">s is null. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">index is not a position within s. </exception>
        /// <filterpriority>1</filterpriority>
        public static bool IsLowSurrogate(string s, int index)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }
            if ((index < 0) || (index >= s.Length))
            {
                throw new ArgumentOutOfRangeException("index");
            }
            return IsLowSurrogate(s[index]);
        }

        /// <summary>Indicates whether two adjacent <see cref="T:System.Char"></see> objects at a specified position in a string form a surrogate pair.</summary>
        /// <returns>true if the s parameter and the index parameter specify a pair of adjacent characters, and the numeric value of the character at position index ranges from U+D800 through U+DBFF, and the numeric value of the character at position index+1 ranges from U+DC00 through U+DFFF; otherwise, false.</returns>
        /// <param name="s">A string. </param>
        /// <param name="index">A position within s. </param>
        /// <exception cref="T:System.ArgumentNullException">s is null. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">index is not a position within s. </exception>
        /// <filterpriority>1</filterpriority>
        public static bool IsSurrogatePair(string s, int index)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }
            if ((index < 0) || (index >= s.Length))
            {
                throw new ArgumentOutOfRangeException("index");
            }
            return (((index + 1) < s.Length) && IsSurrogatePair(s[index], s[index + 1]));
        }

        /// <summary>Indicates whether the two specified <see cref="T:System.Char"></see> objects form a surrogate pair.</summary>
        /// <returns>true if the numeric value of the highSurrogate parameter ranges from U+D800 through U+DBFF, and the numeric value of the lowSurrogate parameter ranges from U+DC00 through U+DFFF; otherwise, false.</returns>
        /// <param name="highSurrogate">A character. </param>
        /// <param name="lowSurrogate">A character. </param>
        /// <filterpriority>1</filterpriority>
        public static bool IsSurrogatePair(char highSurrogate, char lowSurrogate)
        {
            if ((highSurrogate < 0xd800) || (highSurrogate > 0xdbff))
            {
                return false;
            }
            return ((lowSurrogate >= 0xdc00) && (lowSurrogate <= 0xdfff));
        }

        /// <summary>Converts the specified Unicode code point into a UTF-16 encoded string.</summary>
        /// <returns>A string consisting of one <see cref="T:System.Char"></see> object or a surrogate pair of <see cref="T:System.Char"></see> objects equivalent to the code point specified by the utf32 parameter.</returns>
        /// <param name="utf32">A 21-bit Unicode code point. </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">utf32 is not a valid 21-bit Unicode code point ranging from U+0 through U+10FFFF, excluding the surrogate pair range from U+D800 through U+DFFF. </exception>
        /// <filterpriority>1</filterpriority>
        public static string ConvertFromUtf32(int utf32)
        {
            if (((utf32 < 0) || (utf32 > 0x10ffff)) || ((utf32 >= 0xd800) && (utf32 <= 0xdfff)))
            {
                throw new ArgumentOutOfRangeException("utf32", Environment.GetResourceString("ArgumentOutOfRange_InvalidUTF32"));
            }
            if (utf32 < 0x10000)
            {
                return ToString((char) utf32);
            }
            utf32 -= 0x10000;
            return new string(new char[] { (char) ((utf32 / 0x400) + 0xd800), (char) ((utf32 % 0x400) + 0xdc00) });
        }

        /// <summary>Converts the value of a UTF-16 encoded surrogate pair into a Unicode code point.</summary>
        /// <returns>The 21-bit Unicode code point represented by the highSurrogate and lowSurrogate parameters.</returns>
        /// <param name="highSurrogate">A high surrogate character (that is, a code point ranging from U+D800 through U+DBFF). </param>
        /// <param name="lowSurrogate">A low surrogate character (that is, a code point ranging from U+DC00 through U+DFFF). </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">highSurrogate is not in the range U+D800 through U+DBFF, or lowSurrogate is not in the range U+DC00 through U+DFFF. </exception>
        /// <filterpriority>1</filterpriority>
        public static int ConvertToUtf32(char highSurrogate, char lowSurrogate)
        {
            if (!IsHighSurrogate(highSurrogate))
            {
                throw new ArgumentOutOfRangeException("highSurrogate", Environment.GetResourceString("ArgumentOutOfRange_InvalidHighSurrogate"));
            }
            if (!IsLowSurrogate(lowSurrogate))
            {
                throw new ArgumentOutOfRangeException("lowSurrogate", Environment.GetResourceString("ArgumentOutOfRange_InvalidLowSurrogate"));
            }
            return ((((highSurrogate - 0xd800) * 0x400) + (lowSurrogate - 0xdc00)) + 0x10000);
        }

        /// <summary>Converts the value of a UTF-16 encoded character or surrogate pair at a specified position in a string into a Unicode code point.</summary>
        /// <returns>The 21-bit Unicode code point represented by the character or surrogate pair at the position in the s parameter specified by the index parameter.</returns>
        /// <param name="s">A string that contains a character or surrogate pair. </param>
        /// <param name="index">The index position of the character or surrogate pair in s.</param>
        /// <exception cref="T:System.ArgumentException">The specified index position contains a surrogate pair, and either the first character in the pair is not a valid high surrogate or the second character in the pair is not a valid low surrogate. </exception>
        /// <exception cref="T:System.ArgumentNullException">s is null. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">index is not a position within s. </exception>
        /// <filterpriority>1</filterpriority>
        public static int ConvertToUtf32(string s, int index)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }
            if ((index < 0) || (index >= s.Length))
            {
                throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
            }
            int num = s[index] - 0xd800;
            if ((num < 0) || (num > 0x7ff))
            {
                return s[index];
            }
            if (num > 0x3ff)
            {
                throw new ArgumentException(Environment.GetResourceString("Argument_InvalidLowSurrogate", new object[] { index }), "s");
            }
            if (index >= (s.Length - 1))
            {
                throw new ArgumentException(Environment.GetResourceString("Argument_InvalidHighSurrogate", new object[] { index }), "s");
            }
            int num2 = s[index + 1] - 0xdc00;
            if ((num2 < 0) || (num2 > 0x3ff))
            {
                throw new ArgumentException(Environment.GetResourceString("Argument_InvalidHighSurrogate", new object[] { index }), "s");
            }
            return (((num * 0x400) + num2) + 0x10000);
        }

        /* static Char()
        {
            categoryForLatin1 = new byte[] { 
                14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 
                14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 
                11, 0x18, 0x18, 0x18, 0x1a, 0x18, 0x18, 0x18, 20, 0x15, 0x18, 0x19, 0x18, 0x13, 0x18, 0x18, 
                8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 0x18, 0x18, 0x19, 0x19, 0x19, 0x18, 
                0x18, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 20, 0x18, 0x15, 0x1b, 0x12, 
                0x1b, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 20, 0x19, 0x15, 0x19, 14, 
                14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 
                14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 
                11, 0x18, 0x1a, 0x1a, 0x1a, 0x1a, 0x1c, 0x1c, 0x1b, 0x1c, 1, 0x16, 0x19, 0x13, 0x1c, 0x1b, 
                0x1c, 0x19, 10, 10, 0x1b, 1, 0x1c, 0x18, 0x1b, 10, 1, 0x17, 10, 10, 10, 0x18, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0x19, 0, 0, 0, 0, 0, 0, 0, 1, 
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
                1, 1, 1, 1, 1, 1, 1, 0x19, 1, 1, 1, 1, 1, 1, 1, 1
             };
        } */
    }
}

