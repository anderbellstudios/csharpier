using CSharpier.Core.DocTypes;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpier.Core.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class CollectionExpression {
    public static Doc Print(CollectionExpressionSyntax node, PrintingContext context) {
        var alwaysBreak =
            node.Elements.FirstOrDefault()
                is ExpressionElementSyntax { Expression: CollectionExpressionSyntax };

        var result = Doc.Concat(
            Token.Print(node.OpenBracketToken, context),
            node.Elements.Any()
                ? Doc.Indent(
                    alwaysBreak ? Doc.HardLine : Doc.IfBreak(Doc.Line, Doc.Null),
                    SeparatedSyntaxList.PrintWithTrailingComma(
                        node.Elements,
                        Node.Print,
                        alwaysBreak ? Doc.HardLine : Doc.Line,
                        context,
                        node.CloseBracketToken
                    )
                )
                : Doc.Null,
            node.Elements.Any()
                ? alwaysBreak
                    ? Doc.HardLine
                    : Doc.IfBreak(Doc.Line, Doc.Null)
                : Doc.Null,
            node.CloseBracketToken.LeadingTrivia.Any(o => o.IsComment() || o.IsDirective)
                ? Doc.Concat(
                    Doc.Indent(Token.PrintLeadingTrivia(node.CloseBracketToken, context)),
                    Doc.HardLine
                )
                : Doc.Null,
            Token.PrintWithoutLeadingTrivia(node.CloseBracketToken, context)
        );
        return Doc.Group(result);
    }
}
