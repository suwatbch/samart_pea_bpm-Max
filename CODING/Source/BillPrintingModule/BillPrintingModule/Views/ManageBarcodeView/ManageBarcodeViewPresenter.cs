//----------------------------------------------------------------------------------------
// patterns & practices - Smart Client Software Factory - Guidance Package
//
// This file was generated by the "Add View" recipe.
//
// A presenter calls methods of a view to update the information that the view displays. 
// The view exposes its methods through an interface definition, and the presenter contains
// a reference to the view interface. This allows you to test the presenter with different 
// implementations of a view (for example, a mock view).
//
// For more information see:
// ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.scsf.2006jun/SCSF/html/03-030-Model%20View%20Presenter%20%20MVP%20.htm
//
// Latest version of this Guidance Package: http://go.microsoft.com/fwlink/?LinkId=62182
//----------------------------------------------------------------------------------------

using System;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.BillPrintingModule.Interface.BusinessEntities;
using PEA.BPM.BillPrintingModule.Interface.Services;
using System.Collections.Generic;

namespace PEA.BPM.BillPrintingModule
{
    public class ManageBarcodeViewPresenter : Presenter<IManageBarcodeView>
    {
        private IControlServices _controlServices;

        [InjectionConstructor]
        public ManageBarcodeViewPresenter([ServiceDependency] IControlServices controlServices)
        {
            _controlServices = controlServices;
        }

        /// <summary>
        /// This method is a placeholder that will be called by the view when it's been loaded <see cref="System.Winforms.Control.OnLoad"/>
        /// </summary>
        public override void OnViewReady()
        {
            base.OnViewReady();
        }

        /// <summary>
        /// Close the view
        /// </summary>
        public void OnCloseView()
        {
            base.CloseView();
        }

        public void ListBarcodeMRU(ManageBarcodeParam param)
        {
            View.BarcodeMRU = _controlServices.GetBarcodeMRU(param);
        }

        public void UpdateBarcodeMRU(BarcodeMRU param)
        {
            _controlServices.UpdateBarcodeMRU(param);
        }
    }
}

