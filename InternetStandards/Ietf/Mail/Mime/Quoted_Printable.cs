using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;

namespace InternetStandards.Ietf.Mail.Mime
{
    public static class Quoted_Printable
    {
        static Regex quoted_PrintableSplit = new Regex("=[0-9A-F]{2}|.", RegexOptions.Compiled);
        public static string Encode(string str)
        {
            //return Encode(str, Encoding.UTF8, true);
            return null;
        }

        public static string Encode(Quoted_PrintableInterpreter interpreter)
        {
            const string crLf = "\u000D\u000A";
            const string softLineBreak = "=" + crLf;

            StringBuilder quotedPrintable = new StringBuilder();
            int lineLength = 0;
            int lastEncodedTextElementLength = 0;
            bool continueProcess = false;

            while (interpreter.Read() || (continueProcess = !continueProcess))
            {
                bool asciiLineBreak = interpreter.LineBreak && interpreter.EquivalentToAscii && interpreter.Value != null && interpreter.Value.Length == 2 && interpreter.Value[0] == 13 && interpreter.Value[1] == 10;
                if (asciiLineBreak || continueProcess)
                {
                    if (quotedPrintable.Length > 0)
                    {
                        char lastChar = quotedPrintable[quotedPrintable.Length - 1];
                        if (lastChar == 32 || lastChar == 9)
                        {
                            int formerLineLength = lineLength;
                            if (formerLineLength != 75)
                            {
                                quotedPrintable = quotedPrintable.Remove(quotedPrintable.Length - 1, 1);
                                lineLength -= 1;
                            }
                            if (formerLineLength == 75 || formerLineLength > 74)
                            {
                                quotedPrintable.Append(softLineBreak);
                                lineLength = 0;
                            }
                            if (formerLineLength != 75)
                            {
                                quotedPrintable.Append(HexOctet((new byte[] { (byte)lastChar })));
                                lineLength += 3;
                            }
                        }
                    }

                    if (asciiLineBreak)
                        quotedPrintable.Append(crLf);

                    lineLength = 0;
                    lastEncodedTextElementLength = 0;
                }
                else
                {
                    if (lineLength == 76)
                    {
                        quotedPrintable.Insert(76 - lastEncodedTextElementLength, softLineBreak);
                        lineLength = lastEncodedTextElementLength;
                    }
                    if (interpreter.Value != null)
                    {
                        string encodedTextElement = null;
                        if (interpreter.EquivalentToAscii)
                            encodedTextElement = EncodeAsciiBytes(interpreter.Value);
                        else
                            encodedTextElement = HexOctet(interpreter.Value);
                        
                        int maxLineLength = interpreter.LineBreak ? 75 : 76;
                        if (encodedTextElement.Length > maxLineLength)
                        {
                            foreach (string encodedPiece in quoted_PrintableSplit.Split(encodedTextElement))
                            {
                                if (lineLength == 76)
                                {
                                    quotedPrintable.Insert(76 - lastEncodedTextElementLength, softLineBreak);
                                    lineLength = lastEncodedTextElementLength;
                                }

                                if (lineLength + encodedPiece.Length > maxLineLength)
                                {
                                    quotedPrintable.Append(softLineBreak);
                                    lineLength = 0;
                                }

                                quotedPrintable.Append(encodedPiece);
                                lineLength += encodedPiece.Length;
                                lastEncodedTextElementLength = encodedPiece.Length;
                            }
                        }
                        else
                        {
                            if (lineLength + encodedTextElement.Length > maxLineLength)
                            {
                                quotedPrintable.Append(softLineBreak);
                                lineLength = 0;
                            }
                            quotedPrintable.Append(encodedTextElement);
                            lineLength += encodedTextElement.Length;
                            lastEncodedTextElementLength = encodedTextElement.Length;
                        }
                    }

                    if (interpreter.LineBreak)
                    {
                        quotedPrintable.Append(softLineBreak);
                        lineLength = 0;
                        lastEncodedTextElementLength = 0;
                    }
                }
            }

            return quotedPrintable.ToString();
        }

        private static string HexOctet(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder(bytes.Length * 3, bytes.Length * 3);
            foreach (byte b in bytes)
                sb.Append('=' + b.ToString("X2"));

            return sb.ToString();
        }

        private static string EncodeAsciiBytes(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder(bytes.Length, bytes.Length * 3);
            foreach (byte b in bytes)
                if ((b >= 33 && b <= 60) || (b >= 62 && b <= 126) || b == 9 || b == 32)
                    sb.Append((char)b);
                else
                    sb.Append(HexOctet((new byte[] { b })));

            return sb.ToString();
        }
    }
}
