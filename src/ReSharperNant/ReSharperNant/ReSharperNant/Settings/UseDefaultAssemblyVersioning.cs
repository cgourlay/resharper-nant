using JetBrains.Application.Settings;
using JetBrains.ReSharper.Settings;

namespace ReSharperNant.Settings
{
    [SettingsKey(typeof(CodeInspectionSettings), "Use default assembly versioning settings")]
    public class UseDefaultAssemblyVersioning
    {
        [SettingsEntry(true, "Enable default assembly versioning")]
        public bool Enable { get; set; }
    }
}
