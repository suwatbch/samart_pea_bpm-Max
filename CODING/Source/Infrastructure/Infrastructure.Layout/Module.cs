using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.ObjectBuilder;
using PEA.BPM.Infrastructure.Interface.Constants;
using PEA.BPM.Infrastructure.Library.UI;
using PEA.BPM.Infrastructure.Interface.Services;

namespace PEA.BPM.Infrastructure.Layout
{
    public class Module : ModuleInit
    {
        private WorkItem _rootWorkItem;

        [InjectionConstructor]
        public Module([ServiceDependency] WorkItem rootWorkItem)
        {
            _rootWorkItem = rootWorkItem;
        }

        public override void Load()
        {
            base.Load();

            // Add layout view to the shell
            ShellLayoutView _shellLayout = _rootWorkItem.Items.AddNew<ShellLayoutView>();
            _rootWorkItem.Workspaces[WorkspaceNames.LayoutWorkspace].Show(_shellLayout);

            // Add window workspace to be used for modal windows
            WindowWorkspace wsp = new WindowWorkspace(_shellLayout.ParentForm);
            _rootWorkItem.Workspaces.Add(wsp, WorkspaceNames.ModalWindows);

            _rootWorkItem.Services.AddNew<LayoutService, ILayoutService>();
        }
    }
}