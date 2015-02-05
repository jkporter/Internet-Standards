using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Globalization;

namespace InternetStandards.Ietf.Http
{
    [Serializable, StructLayout(LayoutKind.Sequential), ComVisible(true)]
    public struct QualityValue : IComparable, IFormattable, IConvertible, IComparable<QualityValue>, IEquatable<QualityValue>
    {
        public const QualityValue MinValue = (QualityValue)0;
        public const QualityValue Epsilon = (QualityValue)0.001;
        public const QualityValue MaxValue = (QualityValue)1;
        internal float m_value;

        public QualityValue(int value)
        {
            if (value > 1 || value < 0)
            {
                throw new System.ArgumentException();
            }
            m_value = value;
        }

        public QualityValue(byte value)
        {
            if (value > 1)
            {
                throw new System.ArgumentException();
            }
            m_value = value;
        }

        public QualityValue(float value)
        {
            if (value > 1 || value < 0)
            {
                throw new System.ArgumentException();
            }
            m_value = (float)Math.Round(value, 3);
        }

        public QualityValue(double value)
        {
            if (value > 1 || value < 0)
            {
                throw new System.ArgumentException();
            }
            m_value = (float)Math.Round(value, 3);
        }

        public QualityValue(decimal value)
        {
            if (value > 1 || value < 0)
            {
                throw new System.ArgumentException();
            }
            m_value = (float)Math.Round(value, 3);
        }

        public QualityValue(bool value)
        {
            m_value = value ? 1 : 0;
        }

        public static explicit operator QualityValue(byte b)
        {
            QualityValue qv = new QualityValue(b);
            return qv;
        }

        public static explicit operator QualityValue(int i)
        {
            QualityValue qv = new QualityValue(i);
            return qv;
        }

        public static explicit operator QualityValue(double d)
        {
            QualityValue qv = new QualityValue(d);
            return qv;
        }

        public static explicit operator QualityValue(float f)
        {
            QualityValue qv = new QualityValue(f);
            return qv;
        }

        public static explicit operator QualityValue(decimal d)
        {
            QualityValue qv = new QualityValue(d);
            return qv;
        }

        public static implicit operator QualityValue(bool b)
        {
            QualityValue qv = new QualityValue(b);
            return qv;
        }

        public static bool operator >(QualityValue a, QualityValue b)
        {
            return a.m_value > b.m_value;
        }

        public static bool operator <(QualityValue a, QualityValue b)
        {
            return a.m_value < b.m_value;
        }

        public static bool operator >(QualityValue a, int b)
        {
            return a.m_value > b;
        }

        public int CompareTo(object value)
        {
            if (value == null)
            {
                return 1;
            }
            if (!(value is QualityValue))
            {
                throw new ArgumentException("Must be a QualityValue");
            }
            QualityValue qv = (QualityValue)value;
            /* if (this < qv)
            {
                return -1;
            }
            if (this > qv)
            {
                return 1;
            }
            if (this != qv)
            {
                if (!IsNaN(this))
                {
                    return 1;
                }
                if (!IsNaN(qv))
                {
                    return -1;
                }
            } */
            return 0;
        }

        public int CompareTo(QualityValue value)
        {
            /* if (this < value)
            {
                return -1;
            }
            if (this > value)
            {
                return 1;
            }
            if (this != value)
            {
                if (!IsNaN(this))
                {
                    return 1;
                }
                if (!IsNaN(value))
                {
                    return -1;
                }
            } */
            return 0;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is QualityValue))
            {
                return false;
            }
            QualityValue qv = (QualityValue)obj;
            return qv.m_value == this.m_value;
        }

        public bool Equals(QualityValue obj)
        {
            return obj.m_value == this.m_value;
        }

        public override unsafe int GetHashCode()
        {
            QualityValue num = this;

            if (num.m_value == 0f)
            {
                return 0;
            }
            return *(((int*)&num.m_value));
        }

        public override string ToString()
        {
            return m_value.ToString("0.###");
        }

        public string ToString(IFormatProvider provider)
        {
            return m_value.ToString(provider);
        }

        public string ToString(string format)
        {
            return m_value.ToString(format);
        }

        public string ToString(string format, IFormatProvider provider)
        {
            return m_value.ToString(format, provider);
        }

        public static QualityValue Parse(string s)
        {
            // return float.Parse(s, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, NumberFormatInfo.)

            //Parse(s, NumberStyles.Float | NumberStyles.AllowThousands, NumberFormatInfo.CurrentInfo);

            return (QualityValue)0;
        }

        public static QualityValue Parse(string s, NumberStyles style)
        {
            //NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
            return Parse(s, style, NumberFormatInfo.CurrentInfo);
        }

        public static QualityValue Parse(string s, IFormatProvider provider)
        {
            return Parse(s, NumberStyles.Float | NumberStyles.AllowThousands, NumberFormatInfo.GetInstance(provider));
        }

        public static QualityValue Parse(string s, NumberStyles style, IFormatProvider provider)
        {
            // NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
            return Parse(s, style, NumberFormatInfo.GetInstance(provider));
        }

        private static QualityValue Parse(string s, NumberStyles style, NumberFormatInfo info)
        {
            /* try
            {
                return Number.ParseSingle(s, style, info);
            }
            catch (FormatException)
            {
                string str = s.Trim();
                if (str.Equals(info.PositiveInfinitySymbol))
                {
                    return PositiveInfinity;
                }
                if (str.Equals(info.NegativeInfinitySymbol))
                {
                    return NegativeInfinity;
                }
                if (!str.Equals(info.NaNSymbol))
                {
                    throw;
                }
                return NaN;
            } */

            return 0;
        }

        public static bool TryParse(string s, out float result)
        {
            return TryParse(s, NumberStyles.Float | NumberStyles.AllowThousands, NumberFormatInfo.CurrentInfo, out result);
        }

        public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out float result)
        {
            //NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
            return TryParse(s, style, NumberFormatInfo.GetInstance(provider), out result);
        }

        private static bool TryParse(string s, NumberStyles style, NumberFormatInfo info, out float result)
        {
            /* if (s == null)
            {
                result = 0f;
                return false;
            }
            if (!Number.TryParseSingle(s, style, info, out result))
            {
                string str = s.Trim();
                if (!str.Equals(info.PositiveInfinitySymbol))
                {
                    if (!str.Equals(info.NegativeInfinitySymbol))
                    {
                        if (!str.Equals(info.NaNSymbol))
                        {
                            return false;
                        }
                        result = NaN;
                    }
                    else
                    {
                        result = NegativeInfinity;
                    }
                }
                else
                {
                    result = PositiveInfinity;
                }
            } */
            result = 0;
            return true;
        }

        public TypeCode GetTypeCode()
        {
            return TypeCode.Single;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            return Convert.ToBoolean(m_value);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            // throw new InvalidCastException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("InvalidCast_FromTo"), new object[] { "Single", "Char" }));
            return null;
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte(m_value);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            return Convert.ToByte(m_value);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            return Convert.ToInt16(m_value);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            return Convert.ToUInt16(m_value);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            return Convert.ToInt32(m_value);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            return Convert.ToUInt32(m_value);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            return Convert.ToInt64(m_value);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            return Convert.ToUInt64(m_value);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            return m_value;
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            return Convert.ToDouble(m_value);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            return Convert.ToDecimal(m_value);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            throw new InvalidCastException(string.Format(CultureInfo.CurrentCulture, "{0} {1}", new object[] { "Single", "DateTime" }));
        }

        object IConvertible.ToType(Type type, IFormatProvider provider)
        {
            // return Convert.DefaultToType(this, type, provider);
            return null;
        }
    }


}
