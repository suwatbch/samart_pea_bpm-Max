//----------------------------------------------------------------------------------------
// patterns & practices - Smart Client Software Factory - Guidance Package
//
// This file was generated by the "Add Foundational Module" recipe.
//
// The Module class derives from the CAB base class ModuleInit and it will be instantiated
// when the module is loaded by the CAB infrastructure.
//
// For more information see:
// ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.scsf.2006jun/SCSF/html/03-270-Creating%20a%20Module.htm
//
// Latest version of this Guidance Package: http://go.microsoft.com/fwlink/?LinkId=62182
//----------------------------------------------------------------------------------------

using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Services;
using Microsoft.Practices.ObjectBuilder;
using PEA.BPM.Infrastructure.Interface;


namespace PEA.BPM.AgencyManagementModule
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
            ControlledWorkItem<ModuleController> wi = _rootWorkItem.WorkItems.AddNew<ControlledWorkItem<ModuleController>>();
            wi.Controller.Run();
        }

        

    }
}
