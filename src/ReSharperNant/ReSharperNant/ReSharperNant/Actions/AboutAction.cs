using System.Windows.Forms;
using JetBrains.ActionManagement;
using JetBrains.Application.DataContext;

namespace ReSharperNant
{
    [ActionHandler("ReSharperNant.About")]
    public class AboutAction : IActionHandler
    {
        public bool Update(IDataContext context, ActionPresentation presentation, DelegateUpdate nextUpdate)
        {
            // return true or false to enable/disable this action
            return true;
        }

        public void Execute(IDataContext context, DelegateExecute nextExecute)
        {
            MessageBox.Show(
              "resharper-nant\nColin Gourlay\n\nA ReSharper plugin that adds support for NAnt.",
              "About resharper-nant",
              MessageBoxButtons.OK,
              MessageBoxIcon.Information);
        }
    }
}