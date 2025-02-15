//----------------------------------------------------------------------------------------
// patterns & practices - Smart Client Software Factory - Guidance Package
//
// This file was generated by the "Add CAB Module" recipe.
//
// This class contains placeholder methods for the common module initialization 
// tasks, such as adding services, or user-interface element
//
// For more information see: 
// ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.scsf.2006jun/SCSF/html/03-220-Smart%20Client%20Application%20Template.htm
//
// Latest version of this Guidance Package: http://go.microsoft.com/fwlink/?LinkId=62182
//----------------------------------------------------------------------------------------

using System;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using PEA.BPM.Infrastructure.Interface;

namespace PEA.BPM.Infrastructure.Module
{
    public class ModuleController : WorkItemController
    {
        public override void Run()
        {
            AddServices();
            ExtendMenu();
            ExtendToolStrip();
            AddViews();
        }

        private void AddServices()
        {
            //TODO: add services provided by the Module. See: Add or AddNew method in WorkItem.Services collection or 
            //		See: ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.practices.2005Nov.cab/CAB/html/03-020-Adding%20Services.htm
        }

        private void ExtendMenu()
        {
            //TODO: add menu items here, normally by calling the "Add" method on
            //		on the WorkItem.UIExtensionSites collection. For an example 
            //		See: ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.practices.2005Nov.cab/CAB/html/03-100-Showing%20UIElements.htm
        }

        private void ExtendToolStrip()
        {
            //TODO: add new items to the ToolStrip in the Shell. See the UIExtensionSites collection in the WorkItem. 
            //		See: ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.practices.2005Nov.cab/CAB/html/03-100-Showing%20UIElements.htm
        }

        private void AddViews()
        {
            //TODO: create the Module views, add them to the WorkItem and show them in 
            //		a Workspace. See: ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.scsf.2006jun/SCSF/html/03-300-Adding%20a%20View.htm
        }

        //TODO: Add CommandHandlers and/or Event Subscriptions
        //		See ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.practices.2005Nov.cab/CAB/html/03-110-Registering%20Commands.htm
        //		See ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.practices.2005Nov.cab/CAB/html/03-080-Publishing%20and%20Subscribing%20to%20Events.htm
    }
}
