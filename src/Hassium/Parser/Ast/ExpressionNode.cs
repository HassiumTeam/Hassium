using System;
using System.Collections.Generic;
using System.Linq;
using Hassium.Interpreter;
using Hassium.Lexer;

namespace Hassium.Parser.Ast
{
	public static class ExpressionNode
	{
		private static BinaryOperation GetBinaryOp(string value)
		{
			switch (value)
			{
				case "**":
				case "**=":
					return BinaryOperation.Pow;
				case "//":
				case "//=":
					return BinaryOperation.Root;
				case ">>":
				case ">>=":
					return BinaryOperation.BitshiftRight;
				case "<<":
				case "<<=":
					return BinaryOperation.BitshiftLeft;
				case "+":
				case "+=":
					return BinaryOperation.Addition;
				case "-":
				case "-=":
					return BinaryOperation.Subtraction;
				case "/":
				case "/=":
					return BinaryOperation.Division;
				case "*":
				case "*=":
					return BinaryOperation.Multiplication;
				case "%":
				case "%=":
					return BinaryOperation.Modulus;
				case "&":
				case "&=":
					return BinaryOperation.BitwiseAnd;
				case "|":
				case "|=":
					return BinaryOperation.BitwiseOr;
				case "^":
				case "^=":
					return BinaryOperation.Xor;
				case ":=":
					return BinaryOperation.Assignment;
				case "=":
					return BinaryOperation.Equals;
				case "!=":
					return BinaryOperation.NotEqualTo;
				case ">":
					return BinaryOperation.GreaterThan;
				case ">=":
					return BinaryOperation.GreaterOrEqual;
				case "<":
					return BinaryOperation.LessThan;
				case "<=":
					return BinaryOperation.LesserOrEqual;
				case "&&":
					return BinaryOperation.LogicalAnd;
				case "||":
					return BinaryOperation.LogicalOr;
				case "??":
					return BinaryOperation.NullCoalescing;
				case ".":
					return BinaryOperation.Dot;
				default:
					throw new ArgumentException("Invalid binary operation: " + value);
			}
		}


		public static AstNode Parse(Parser parser)
		{
			return ParseAssignment(parser);
		}


		private static AstNode ParseAssignment (Parser parser)
		{
			int pos = parser.codePos;

			AstNode left = ParseLogicalOr(parser);

			while (parser.CurrentToken().TokenClass == TokenType.Assignment ||
				   parser.CurrentToken().TokenClass == TokenType.OpAssign)
			{
				if (parser.AcceptToken(TokenType.Assignment))
				{
					AstNode right = ParseLogicalOr(parser);
					left = new BinOpNode(pos, BinaryOperation.Assignment, left, right);
				}
				else if (parser.AcceptToken(TokenType.OpAssign))
				{
					var assigntype = GetBinaryOp(parser.PreviousToken().Value.ToString());
					var right = ParseLogicalOr(parser);
					left = new BinOpNode(pos, BinaryOperation.Assignment, assigntype, left, right);
				}
				else break;
			}

			return left;
		}

		private static AstNode ParseLogicalOr(Parser parser)
		{
			int pos = parser.codePos;

			AstNode left = ParseLogicalAnd(parser);

			while (parser.CurrentToken().TokenClass == TokenType.Comparison || parser.CurrentToken().TokenClass == TokenType.Operation)
			{             
				if (parser.AcceptToken(TokenType.Comparison, "||"))
				{
					var right = ParseLogicalAnd(parser);
					left = new BinOpNode(pos, BinaryOperation.LogicalOr, left, right);
				}
				else if (parser.AcceptToken(TokenType.Operation, "??"))
				{
					var right = ParseLogicalAnd(parser);
					left = new BinOpNode(pos, BinaryOperation.NullCoalescing, left, right);
				}
				else break;
			}

			return left;
		}

		private static AstNode ParseLogicalAnd(Parser parser)
		{
			int pos = parser.codePos;

			AstNode left = ParseEquality(parser);

			while (parser.AcceptToken(TokenType.Comparison, "&&"))
			{
				var right = ParseEquality(parser);
				left = new BinOpNode(pos, BinaryOperation.LogicalAnd, left, right);
			}

			return left;
		}

		

		private static AstNode ParseEquality(Parser parser)
		{
			int pos = parser.codePos;

			AstNode left = ParseOr(parser);

			while (parser.CurrentToken().TokenClass == TokenType.Comparison ||
				   parser.CurrentToken().TokenClass == TokenType.Operation)
			{
				if (parser.AcceptToken(TokenType.Comparison, "="))
				{
					var right = ParseEquality(parser);
					left = new BinOpNode(pos, BinaryOperation.Equals, left, right);
				}
				else if (parser.AcceptToken(TokenType.Comparison, "!="))
				{
					var right = ParseOr(parser);
					left = new BinOpNode(pos, BinaryOperation.NotEqualTo, left, right);
				}
				else if (parser.AcceptToken(TokenType.Comparison, "<"))
				{
					var right = ParseOr(parser);
					left = new BinOpNode(pos, BinaryOperation.LessThan, left, right);
				}
				else if (parser.AcceptToken(TokenType.Comparison, ">"))
				{
					var right = ParseOr(parser);
					left = new BinOpNode(pos, BinaryOperation.GreaterThan, left, right);
				}
				else if (parser.AcceptToken(TokenType.Comparison, "<="))
				{
					var right = ParseOr(parser);
					left = new BinOpNode(pos, BinaryOperation.LesserOrEqual, left, right);
				}
				else if (parser.AcceptToken(TokenType.Comparison, ">="))
				{
					var right = ParseOr(parser);
					left = new BinOpNode(pos, BinaryOperation.GreaterOrEqual, left, right);
				}
				else if (parser.AcceptToken(TokenType.Comparison, "<=>"))
				{
					var right = ParseOr(parser);
					left = new BinOpNode(pos, BinaryOperation.CombinedComparison, left, right);
				}
				else if (parser.AcceptToken(TokenType.Operation, "<<"))
				{
					var right = ParseOr(parser);
					left = new BinOpNode(pos, BinaryOperation.BitshiftLeft, left, right);
				}
				else if (parser.AcceptToken(TokenType.Operation, ">>"))
				{
					var right = ParseOr(parser);
					left = new BinOpNode(pos, BinaryOperation.BitshiftRight, left, right);
				}
				else if (parser.AcceptToken(TokenType.Operation, "?"))
				{
					var ifbody = ParseEquality(parser);
					parser.ExpectToken(TokenType.Identifier, ":");
					var elsebody = ParseEquality(parser);
					left = new ConditionalOpNode(pos, left, ifbody, elsebody);
				}
				else break;
			}

			return left;
		}

		private static AstNode ParseOr(Parser parser)
		{
			int pos = parser.codePos;

			AstNode left = ParseXor(parser);

			while (parser.AcceptToken(TokenType.Operation, "|"))
			{
				var right = ParseXor(parser);
				left = new BinOpNode(pos, BinaryOperation.BitwiseOr, left, right);
			}

			return left;
		}

		private static AstNode ParseXor(Parser parser)
		{
			int pos = parser.codePos;

			AstNode left = ParseAnd(parser);

			while (parser.AcceptToken(TokenType.Operation, "^"))
			{
				var right = ParseAnd(parser);
				left = new BinOpNode(pos, BinaryOperation.Xor, left, right);
			}

			return left;
		}

		private static AstNode ParseAnd(Parser parser)
		{
			int pos = parser.codePos;

			AstNode left = ParseAdditive(parser);

			while (parser.AcceptToken(TokenType.Operation, "&"))
			{
				var right = ParseAdditive(parser);
				left = new BinOpNode(pos, BinaryOperation.BitwiseAnd, left, right);
			}

			return left;
		}

		

		private static AstNode ParseAdditive (Parser parser)
		{
			int pos = parser.codePos;

			AstNode left = ParseMultiplicative(parser);

			while (parser.CurrentToken().TokenClass == TokenType.Operation || parser.CurrentToken().TokenClass == TokenType.MentalOperation)
			{
				if (parser.AcceptToken(TokenType.Operation, "+"))
				{
					AstNode right = ParseMultiplicative(parser);
					left = new BinOpNode(pos, BinaryOperation.Addition, left, right);
				}
				else if (parser.AcceptToken(TokenType.Operation, "-"))
				{
					AstNode right = ParseMultiplicative(parser);
					left = new BinOpNode(pos, BinaryOperation.Subtraction, left, right);
				}
				else break;

			}
			return left;
		}

		private static AstNode ParseMultiplicative (Parser parser)
		{
			int pos = parser.codePos;

			AstNode left = ParseUnary(parser);
			
			if(parser.AcceptToken(TokenType.Operation, "**"))
			{
				AstNode right = ParseMultiplicative(parser);
				return new BinOpNode(pos, BinaryOperation.Pow, left, right);
			}
			else if (parser.AcceptToken(TokenType.Operation, "//"))
			{
				AstNode right = ParseMultiplicative(parser);
				return new BinOpNode(pos, BinaryOperation.Root, left, right);
			}
			else if (parser.AcceptToken(TokenType.Operation, "*"))
			{
				AstNode right = ParseMultiplicative(parser);
				return new BinOpNode(pos, BinaryOperation.Multiplication, left, right);
			}
			else if (parser.AcceptToken(TokenType.Operation, "/"))
			{
				AstNode right = ParseMultiplicative(parser);
				return new BinOpNode(pos, BinaryOperation.Division, left, right);
			}
			else  if (parser.AcceptToken(TokenType.Operation, "%"))
			{
				AstNode right = ParseMultiplicative(parser);
				return new BinOpNode(pos, BinaryOperation.Modulus, left, right);
			}
			else if(parser.AcceptToken(TokenType.Lambda, "=>"))
			{
				AstNode body = new ReturnNode(pos, StatementNode.Parse(parser));

				if (parser.AcceptToken(TokenType.EndOfLine)) parser.ExpectToken(TokenType.EndOfLine);

				if(left is ArrayInitializerNode) return new LambdaFuncNode(pos, ((ArrayInitializerNode)left).Value.Values.Select(x => x.ToString()).ToList(), body);
				return new LambdaFuncNode(pos, new List<string>() {left.ToString()}, body);
			}
			else
			{
				return left;
			}
		}

		private static AstNode ParseUnary(Parser parser)
		{
			int pos = parser.codePos;

			if (parser.AcceptToken(TokenType.UnaryOperation, "!"))
				return new UnaryOpNode(pos, UnaryOperation.Not, ParseUnary(parser));
			else if (parser.AcceptToken(TokenType.MentalOperation, "++"))
				return new MentalNode(pos, "++", parser.ExpectToken(TokenType.Identifier).Value.ToString(), true);
			else if (parser.AcceptToken(TokenType.MentalOperation, "--"))
				return new MentalNode(pos, "--", parser.ExpectToken(TokenType.Identifier).Value.ToString(), true);
			else if (parser.AcceptToken(TokenType.Operation, "-"))
				return new UnaryOpNode(pos, UnaryOperation.Negate, ParseUnary(parser));
			else if (parser.AcceptToken(TokenType.UnaryOperation, "~"))
				return new UnaryOpNode(pos, UnaryOperation.Complement, ParseUnary(parser));
			else
				return ParsePostfixIncDec(parser);
		}

		private static AstNode ParsePostfixIncDec(Parser parser)
		{
			int pos = parser.codePos;

			var left = ParseFunctionCall(parser);
			if (parser.AcceptToken(TokenType.MentalOperation, "++"))
			{
				var varname = "";
				var before = false;
				if (parser.AcceptToken(TokenType.Identifier))
				{
					varname = parser.ExpectToken(TokenType.Identifier).Value.ToString();
					before = true;
				}
				else
				{
					varname = parser.PreviousToken(2).Value.ToString();
				}
				return new MentalNode(pos, "++", varname, before);
			}
			else if (parser.AcceptToken(TokenType.MentalOperation, "--"))
			{
				var varname = "";
				var before = false;
				if (parser.AcceptToken(TokenType.Identifier))
				{
					varname = parser.ExpectToken(TokenType.Identifier).Value.ToString();
					before = true;
				}
				else
				{
					varname = parser.PreviousToken(2).Value.ToString();
				}
				return new MentalNode(pos, "--", varname, before);
			}
			else
			{
				return left;
			}
		}

		private static AstNode ParseFunctionCall(Parser parser)
		{
			return ParseFunctionCall(parser, ParseTerm(parser));
		}

	    private static AstNode ParseFunctionCall(Parser parser, AstNode left)
	    {
	        while (true)
	        {
	            int pos = parser.codePos;

	            if (parser.AcceptToken(TokenType.Parentheses, "("))
	            {
	                var parser1 = parser;
	                left = new FunctionCallNode(parser1.PreviousToken(2).Position, left, ArgListNode.Parse(parser1));
	            }
	            else if (parser.AcceptToken(TokenType.Bracket, "["))
	            {
	                var parser1 = parser;
	                left = new ArrayGetNode(pos, left, ArrayIndexerNode.Parse(parser1));
	            }
	            else if (parser.AcceptToken(TokenType.Dot, "."))
	            {
	                Token ident = parser.ExpectToken(TokenType.Identifier);
	                left = new MemberAccess(pos, left, ident.Value.ToString());
	            }
	            else
	            {
	                return left;
	            }
	        }
	    }

	    private static AstNode ParseTerm (Parser parser)
		{
			int pos = parser.codePos;
			if (parser.AcceptToken(TokenType.Number))
			{
				return new NumberNode(pos, Convert.ToDouble(parser.PreviousToken().Value));
			}
			else if (parser.AcceptToken(TokenType.Parentheses, "("))
			{
				AstNode statement = Parse(parser);
				parser.ExpectToken(TokenType.Parentheses, ")");
				return statement;
			}
			else if (parser.AcceptToken(TokenType.Bracket, "["))
			{
				AstNode statement = ArrayInitializerNode.Parse(parser);
				parser.ExpectToken(TokenType.Bracket, "]");
				return statement;
			}
			else if (parser.AcceptToken(TokenType.String))
			{
				return new StringNode(pos, parser.PreviousToken().Value.ToString());
			}
			else if(parser.MatchToken(TokenType.Identifier, "lambda"))
			{
				return LambdaFuncNode.Parse(parser);
			}
			else if (parser.MatchToken(TokenType.Identifier, "new"))
			{
				return InstanceNode.Parse(parser);
			}
			else if (parser.AcceptToken(TokenType.Identifier))
			{
				return new IdentifierNode(pos, parser.PreviousToken().Value.ToString());
			}
			else
			{
				throw new ParseException("Unexpected " + parser.CurrentToken().Value, pos);
			}

		}
	}
}

