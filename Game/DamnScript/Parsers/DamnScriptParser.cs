﻿#if UNITY_2020 && ENABLE_DAMN_SCRIPT
using System.Collections.Generic;
using System.Linq;

namespace Rietmon.DS
{
    public static class DamnScriptParser
    {
        private static readonly char[] ignoreSymbols = {'\n', '\t', '\r', '\a', '\v'};

        public static KeyValuePair<string, string>[] ParseRegions(string code)
        {
            bool TryFindRegion(int offset, out int start, out int end)
            {
                start = code.IndexOf('{', offset);
                end = code.IndexOf('}', offset);
                return start != -1 || end != -1;
            }

            var result = new List<KeyValuePair<string, string>>();

            var currentOffset = 0;
            while (TryFindRegion(currentOffset, out var startPosition, out var endPosition))
            {
                var regionName = code.Substring(currentOffset, startPosition - currentOffset).Replace(" ", "");
                var regionCode = code.Substring(startPosition + 1, endPosition - startPosition - 1);

                result.Add(new KeyValuePair<string, string>(regionName, regionCode));
                currentOffset = endPosition + 1;
            }

            return result.ToArray();
        }

        public static string[][] ParseCode(string code)
        {
            bool TryFindCode(int offset, out int start, out int end)
            {
                start = offset;
                end = code.IndexOf(';', offset);
                return end != -1;
            }

            var result = new List<string[]>();

            var currentOffset = 0;
            while (TryFindCode(currentOffset, out var startPosition, out var endPosition))
            {
                var methodCode = code.Substring(startPosition, endPosition - startPosition);

                var isStartCode = true;
                var isTextNow = false;
                var nowWasText = false;
                for (var i = 0; i < methodCode.Length; i++)
                {
                    foreach (var ignoreSymbol in ignoreSymbols)
                        methodCode = methodCode.Replace(new string(ignoreSymbol, 1), "");

                    var symbol = methodCode[i];

                    if (nowWasText && symbol != ' ')
                    {
                        nowWasText = false;
                        methodCode = methodCode.Insert(i, " ");
                        i++;
                        continue;
                    }

                    nowWasText = false;

                    if (!ignoreSymbols.Contains(symbol) && symbol != ' ')
                        isStartCode = false;

                    if (isStartCode && symbol == ' ')
                    {
                        methodCode = methodCode.Remove(i, 1);
                        i--;
                        continue;
                    }

                    if (symbol == '"')
                    {
                        isTextNow = !isTextNow;
                        if (!isTextNow)
                            nowWasText = true;
                    }

                    if (symbol == ' ' && isTextNow)
                    {
                        methodCode = methodCode.Remove(i, 1);
                        methodCode = methodCode.Insert(i, "||||");
                        i += 3;
                    }
                }

                var codes = methodCode.Split(' ');
                for (var i = 0; i < codes.Length; i++)
                {
                    codes[i] = codes[i].Replace("||||", " ");
                    codes[i] = codes[i].Replace("\"", "");
                }

                result.Add(codes);

                currentOffset = endPosition + 1;
            }

            return result.ToArray();
        }
    }
}
#endif