﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Utilities.Json
{
    public static class JavaScriptUtils
    {
        public static void WriteEscapedJavaScriptString(TextWriter writer, string s, char delimiter, bool appendDelimiters)
        {
            // leading delimiter
            if (appendDelimiters)
                writer.Write(delimiter);

            if (s != null)
            {
                char[] chars = null;
                int lastWritePosition = 0;

                for (int i = 0; i < s.Length; i++)
                {
                    var c = s[i];

                    // don't escape standard text/numbers except '\' and the text delimiter
                    if (c >= ' ' && c < 128 && c != '\\' && c != delimiter)
                        continue;

                    string escapedValue;

                    switch (c)
                    {
                        case '\t':
                            escapedValue = @"\t";
                            break;
                        case '\n':
                            escapedValue = @"\n";
                            break;
                        case '\r':
                            escapedValue = @"\r";
                            break;
                        case '\f':
                            escapedValue = @"\f";
                            break;
                        case '\b':
                            escapedValue = @"\b";
                            break;
                        case '\\':
                            escapedValue = @"\\";
                            break;
                        case '\u0085': // Next Line
                            escapedValue = @"\u0085";
                            break;
                        case '\u2028': // Line Separator
                            escapedValue = @"\u2028";
                            break;
                        case '\u2029': // Paragraph Separator
                            escapedValue = @"\u2029";
                            break;
                        case '\'':
                            // this charater is being used as the delimiter
                            escapedValue = @"\'";
                            break;
                        case '"':
                            // this charater is being used as the delimiter
                            escapedValue = "\\\"";
                            break;
                        default:
                            escapedValue = (c <= '\u001f') ? StringUtils.ToCharAsUnicode(c) : null;
                            break;
                    }

                    if (escapedValue == null)
                        continue;

                    if (i > lastWritePosition)
                    {
                        if (chars == null)
                            chars = s.ToCharArray();

                        // write unchanged chars before writing escaped text
                        writer.Write(chars, lastWritePosition, i - lastWritePosition);
                    }

                    lastWritePosition = i + 1;
                    writer.Write(escapedValue);
                }

                if (lastWritePosition == 0)
                {
                    // no escaped text, write entire string
                    writer.Write(s);
                }
                else
                {
                    if (chars == null)
                        chars = s.ToCharArray();

                    // write remaining text
                    writer.Write(chars, lastWritePosition, s.Length - lastWritePosition);
                }
            }

            // trailing delimiter
            if (appendDelimiters)
                writer.Write(delimiter);
        }

        public static string ToEscapedJavaScriptString(string value)
        {
            return ToEscapedJavaScriptString(value, '"', true);
        }

        public static string ToEscapedJavaScriptString(string value, char delimiter, bool appendDelimiters)
        {
            using (StringWriter w = StringUtils.CreateStringWriter(StringUtils.GetLength(value) ?? 16))
            {
                WriteEscapedJavaScriptString(w, value, delimiter, appendDelimiters);
                return w.ToString();
            }
        }
    }
}