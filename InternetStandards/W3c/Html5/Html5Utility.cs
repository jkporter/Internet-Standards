using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InternetStandards.W3c.Html5
{
    public static class Html5Utility
    {
        const string SpaceCharacters = "\u0020\u0009\u000A\u000C\u000D";
        
        public static string[] SplitStringOnSpaces(string input)
        {
            int position = 0;
            var tokens = new List<string>();

            SkipWhitespace(ref input, ref position);

            while (position < input.Length)
            {
                tokens.Add(CollectASequenceOfCharacters(ref input, ref position, c => !SpaceCharacters.Contains(c)));
                SkipWhitespace(ref input, ref position);
            }

            return tokens.ToArray();
        }

        public static string RemoveATokenFromAString(string input, string token)
        {
            int position = 0;
            string output = string.Empty;

            while (position < input.Length)
            {
                if (SpaceCharacters.Contains(input[position]))
                {
                    output += input[position];
                    position++;
                }
                else
                {
                    string s = CollectASequenceOfCharacters(ref input, ref position, c => !SpaceCharacters.Contains(c));
                    if (s == token)
                    {
                        SkipWhitespace(ref input, ref position);
                        output = output.TrimEnd(SpaceCharacters.ToCharArray());
                        if (position < input.Length && output != string.Empty)
                            output += " ";
                    }
                    else
                    {
                        output += s;
                    }
                }
            }

            return output;
        }

        public static void SkipWhitespace(ref string input, ref int position)
        {
            CollectASequenceOfCharacters(ref input, ref position, c => SpaceCharacters.Contains(c));
        }

        public static string CollectASequenceOfCharacters(ref string input, ref int position, Func<char, bool> isCharacter)
        {
            var result = new StringBuilder();
            while (position < input.Length && isCharacter(input[position]))
            {
                result.Append(input[position]);
                position++;
            }

            return result.ToString();
        }
    }
}
