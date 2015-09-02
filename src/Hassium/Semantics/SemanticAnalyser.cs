using Hassium.Parser;
using Hassium.Parser.Ast;

namespace Hassium.Semantics
{
	public class SemanticAnalyser
	{
		public AstNode Code { get; private set; }

		private SymbolTable result = new SymbolTable();
		private LocalScope currentLocalScope;

		public SemanticAnalyser(AstNode code)
		{
			Code = code;
		}

		public SymbolTable Analyse()
		{
			checkout(Code);

			return result;
		}

		private void checkout(AstNode theNode)
		{
			foreach (AstNode node in theNode.Children)
			{
				if (node is BinOpNode)
				{
					BinOpNode bnode = ((BinOpNode) node);
					if (((BinOpNode) node).BinOp == BinaryOperation.Assignment)
					{
						if (!result.Symbols.Contains(bnode.Left.ToString()))
						{
							result.Symbols.Add(bnode.Left.ToString());
							checkout(node);
						}
					}
				}
				else
					checkout(node);
			}

			foreach (AstNode node in theNode.Children)
			{
				if (node is FuncNode)
				{
					FuncNode fnode = ((FuncNode)node);
					currentLocalScope = new LocalScope();
					result.ChildScopes[fnode.Name] = currentLocalScope;
					currentLocalScope.Symbols.AddRange(fnode.Parameters);
					analyseLocalCode(fnode.Body);
				}
				else if (node is LambdaFuncNode)
				{
					LambdaFuncNode fnode = ((LambdaFuncNode)node);
					currentLocalScope = new LocalScope();
					result.ChildScopes["lambda_" + fnode.GetHashCode()] = currentLocalScope;
					currentLocalScope.Symbols.AddRange(fnode.Parameters);
					analyseLocalCode(fnode.Body);
				}

			}
		}

		private void analyseLocalCode(AstNode theNode)
		{
			foreach(AstNode node in theNode.Children)
			{
				if (node is BinOpNode)
				{
					BinOpNode bnode = ((BinOpNode)node);
					if (bnode.BinOp == BinaryOperation.Assignment)
					{
						if (!result.Symbols.Contains(bnode.Left.ToString()) && !currentLocalScope.Symbols.Contains(bnode.Left.ToString()))
						{
							currentLocalScope.Symbols.Add(bnode.Left.ToString());
						}
					}
				}
				else
					analyseLocalCode(node);
			}
		}
	}
}

