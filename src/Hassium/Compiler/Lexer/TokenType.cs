using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hassium.Compiler.Lexer
{
    public enum TokenType
    {
        Assignment,
        Char,
        CloseCurlyBrace,
        CloseParentheses,
        CloseSquareBrace,
        Colon,
        Comma,
        Comparison,
        Dot,
        Float,
        Identifier,
        Integer,
        Operation,
        OpenCurlyBrace,
        OpenParentheses,
        OpenSquareBrace,
        Question,
        Semicolon,
        String,
        Swap
    }
}
