using CSharpier.Core.DocTypes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpier.Core.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class InitializerExpression
{
    public static Doc Print(InitializerExpressionSyntax node, PrintingContext context)
    {
        Doc separator = node.Parent
            is AssignmentExpressionSyntax
                or EqualsValueClauseSyntax { Parent: not PropertyDeclarationSyntax }
            ? " " // Doc.Line
            : Doc.Null;

        var alwaysBreak =
            (
                node.Expressions.Count >= 3
                && (
                    node.Kind() is SyntaxKind.ObjectInitializerExpression
                    || node.Kind() is SyntaxKind.CollectionInitializerExpression
                        && node.Expressions.FirstOrDefault()?.Kind()
                            is SyntaxKind.ComplexElementInitializerExpression
                )
            )
            || (
                node.Kind() is SyntaxKind.ArrayInitializerExpression
                && node.Expressions.FirstOrDefault()?.Kind()
                    is SyntaxKind.ArrayInitializerExpression
            );

        var result = Doc.Concat(
            separator,
            Token.Print(node.OpenBraceToken, context),
            node.Expressions.Count == 0
                ? " "
                : Doc.Concat(
                    Doc.Indent(
                        alwaysBreak ? Doc.HardLine : Doc.Line,
                        SeparatedSyntaxList.PrintWithTrailingComma(
                            node.Expressions,
                            Node.Print,
                            alwaysBreak ? Doc.HardLine : Doc.Line,
                            context,
                            closingToken: node.Kind()
                                is not SyntaxKind.ComplexElementInitializerExpression
                                ? node.CloseBraceToken
                                : null
                        )
                    ),
                    alwaysBreak ? Doc.HardLine : Doc.Line
                ),
            Token.Print(node.CloseBraceToken, context)
        );
        return
            node.Parent
                is not (
                    ObjectCreationExpressionSyntax
                    or ArrayCreationExpressionSyntax
                    or ImplicitArrayCreationExpressionSyntax
                    or ImplicitObjectCreationExpressionSyntax
                )
            && node.Kind() is not SyntaxKind.WithInitializerExpression
            ? Doc.Group(result)
            : result;
    }
}
