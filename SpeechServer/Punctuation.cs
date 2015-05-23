namespace EmacspeakWindowsSpeechServer
{
    using System;
    using System.Collections.Generic;

    internal static class Punctuation
    {
        public static readonly Dictionary<char, string> Replacements = new Dictionary<char, string>
        {
            { '<', "less" },
{ '>', "greater" },
{ '#', "number" },
{ '?', "question" },
{ '!', "exclaim" },
{ '"', "quote" },
{ '\'', "apostrophe" },
{ '*', "star" },
{ '&', "and" },
{ '£', "pounds" },
{ '$', "dollar" },
{ '%', "percent" },
{ '^', "caret" },
{ '=', "equals" },
{ '-', "dash" },
{ '+', "plus" },
{ '(', "left paren" },
{ ')', "right paren" },
{ '{', "left brace" },
{ '}', "right brace" },
{ '[', "left bracket" },
{ ']', "right bracket" }
        };
    }
}
