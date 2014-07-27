using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Application;
using JetBrains.Application.Progress;
using JetBrains.DocumentManagers;
using JetBrains.DocumentModel;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Intentions.CSharp.QuickFixes;
using JetBrains.ReSharper.Intentions.Extensibility;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.CodeStyle;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Tree;

namespace ReSharperNant.QuickFix
{
    public class DefaultTargetBulbItem : IBulbAction
    {
        private readonly IExpression literalExpression;

        public DefaultTargetBulbItem(IExpression literalExpression)
        {
            this.literalExpression = literalExpression;
        }

        public void Execute(JetBrains.ProjectModel.ISolution solution, JetBrains.TextControl.ITextControl textControl)
        {
            if (!literalExpression.IsValid()) { return; }

            var containingFile = literalExpression.GetContainingFile();
            var elementFactory = CSharpElementFactory.GetInstance(literalExpression.GetPsiModule());

            IExpression newExpression = null;
            literalExpression.GetPsiServices().Transactions.Execute(GetType().Name, () =>
            {
                using (solution.GetComponent<IShellLocks>().UsingWriteLock())
                { 
                    newExpression = ModificationUtil.ReplaceChild(literalExpression, elementFactory.CreateExpression(literalExpression.GetText()));
                }
            });

            if (newExpression == null) return;
            var marker = newExpression.GetDocumentRange().CreateRangeMarker(solution.GetComponent<DocumentManager>());
            containingFile.OptimizeImportsAndRefs(marker, false, true, NullProgressIndicator.Instance);
        }

        public string Text
        {
            get { return string.Format("Make '{0}' the default target.", literalExpression.GetText()); }
        }
    }
}
