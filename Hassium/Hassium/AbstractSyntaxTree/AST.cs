using System;
using System.Collections.Generic;

namespace Hassium
{
    public class AST
    {
        private List<Token> tokens = new List<Token>();
        private List<Token> result = new List<Token>();

        public AST(List<Token> tokens)
        {
            this.tokens = tokens;
        }

        public List<Token> Generate()
        {
            for (int x = 0; x < tokens.Count; x++)
            {
                if (tokens[x].Operator == "FUNCTION")
                {
                    List<Token> buffer = new List<Token>();
                    int y = 0;
                    for (y = x; tokens[y].Operator != "FUNCTION"; y++)
                    {
                        buffer.Add(tokens[y]);
                    }

                    foreach (Token node in calculateChildren(buffer))
                    {
                        result.Add(node);
                    }
                }
            }
        }

        private List<Token> calculateChildren(List<Token> buffer)
        {
            
        }
    }
}

