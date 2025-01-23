using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using PEA.BPM.AgencyManagementModule.Interface.Services;
using PEA.BPM.Infrastructure.Library.UI;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.ObjectBuilder;
using PEA.BPM.AgencyManagementModule.Interface.Constants;
using Microsoft.Practices.CompositeUI.EventBroker;
using System.Collections;


namespace PEA.BPM.AgencyManagementModule
{
    public class ModuleConfigController : WorkItemController
    {
        private IAgencyModuleConfigService _agencyModuleConfigService;


        [InjectionConstructor]
        public ModuleConfigController([ServiceDependency] IAgencyModuleConfigService agencyModuleConfigService)
		{
            _agencyModuleConfigService = agencyModuleConfigService;
		}       

        public override void Run()
        {
        }


        public void LoadCalendar(string branchId)
        {
            //_calendar = _agencyModuleConfigService.LoadCalendarForBranch(branchId);
        }


        /// non working day checking
        [EventSubscription(EventTopicNames.NoneWorkingDayRequest, Thread = ThreadOption.UserInterface)]
        public void NoneWorkingDayRequestHandler(object sender, EventArgs<ArrayList> e)
        {
            //NonWorkingDayInfo result = _agencyModuleConfigService.GetNonWorkingDayInfo(e.Data);
            //SendNoneWorkingDayResponse(result);
        }

        [EventPublication(EventTopicNames.DaysFromLastDateResponse, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs> DayBetweenDatesResponseHandler;
        public void SendDaysFromLastDateResponse(int? days)
        {
            if (DayBetweenDatesResponseHandler != null)
                DayBetweenDatesResponseHandler(this, new EventArgs<int?>(days));
        }

        //find working date by ref date plus number of working days  
        [EventSubscription(EventTopicNames.NextDateRequest, Thread = ThreadOption.UserInterface)]
        public void NextDateRequestHandler(object sender, EventArgs<ArrayList> e)
        {
            //DateTime nextDate = _agencyModuleConfigService.GetNextDate(e.Data);
            //SendNextDateResponse(nextDate);
        }

        [EventPublication(EventTopicNames.NextDateResponse, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs> NextDateResponseHandler;
        public void SendNextDateResponse(DateTime nextDate)
        {
            if (NextDateResponseHandler != null)
                NextDateResponseHandler(this, new EventArgs<DateTime>(nextDate));
        }
           




    }
}
