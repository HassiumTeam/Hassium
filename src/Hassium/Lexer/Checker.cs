// /**
//   * Copyright (c) 2015, HassiumTeam (JacobMisirian, zdimension) All rights reserved.
//   * Redistribution and use in source and binary forms, with or without modification,
//   * are permitted provided that the following conditions are met:
//   * 
//   *  * Redistributions of source code must retain the above copyright notice, this list
//   *    of conditions and the following disclaimer.
//   * 
//   *  * Redistributions in binary form must reproduce the above copyright notice, this
//   *    list of conditions and the following disclaimer in the documentation and/or
//   *    other materials provided with the distribution.
//   * Neither the name of the copyright holder nor the names of its contributors may be
//   * used to endorse or promote products derived from this software without specific
//   * prior written permission.
//   * 
//   * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY
//   * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
//   * OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT
//   * SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT,
//   * INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED
//   * TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR
//   * BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
//   * CONTRACT ,STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN
//   * ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH
//   * DAMAGE.
// **/
using System;
using System.Collections.Generic;

namespace Hassium.Lexer
{
    public class Checker
    {
        private List<Token> code { get; set; }

        private int openingBrackets = 0;
        private int closingBrackets = 0;
        private int openingParentheses = 0;
        private int closingParentheses = 0;
        private int openingBraces = 0;
        private int closingBraces = 0;

        public Checker(List<Token> tokens)
        {
            code = tokens;
        }

        public void Check()
        {
            foreach (Token token in code)
            {
                switch (token.TokenClass)
                {
                    case TokenType.Brace:
                        if (token.Value.ToString() == "{")
                            openingBraces++;
                        else if (token.Value.ToString() == "}")
                            closingBraces++;
                        break;
                    case TokenType.Parentheses:
                        if (token.Value.ToString() == "(")
                            openingParentheses++;
                        else if (token.Value.ToString() == ")")
                            closingParentheses++;
                        break;
                    case TokenType.Bracket:
                        if (token.Value.ToString() == "[")
                            openingBrackets++;
                        else if (token.Value.ToString() == "]")
                            closingBrackets++;
                        break;
                }
            }

            if (openingParentheses != closingParentheses)
                throw new Exception("Parentheses do not match! " + openingParentheses + " opening and " + closingParentheses + " closing.");
            else if (openingBrackets != closingBrackets)
                throw new Exception("Brackets do not match! " + openingBrackets + " opening and " + closingBrackets + " closing.");
            else if (openingBraces != closingBraces)
                throw new Exception("Braces do not match! " + openingBraces + " opening and " + closingBraces + " closing.");
        }
    }
}

