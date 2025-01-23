using Microsoft.Practices.CompositeUI.EventBroker;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.Infrastructure.Interface.Constants;
using System;

namespace PEA.BPM.Infrastructure.Layout
{
    public class ShellLayoutViewPresenter : Presenter<ShellLayoutView>
    {
        protected override void OnViewSet()
        {
            WorkItem.UIExtensionSites.RegisterSite(UIExtensionSiteNames.MainMenu, View.MainMenuStrip);
            WorkItem.UIExtensionSites.RegisterSite(UIExtensionSiteNames.MainStatus, View.MainStatusStrip);
            WorkItem.UIExtensionSites.RegisterSite(UIExtensionSiteNames.MainToolbar, View.MainToolbarStrip);
        }

        /// <summary>
        /// Status update handler. Updates the status strip on the main form.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopicNames.StatusUpdate, ThreadOption.UserInterface)]
        public void StatusUpdateHandler(object sender, EventArgs<string> e)
        {
            View.SetStatusLabel(e.Data);
        }

        [EventSubscription(EventTopicNames.LoginNameUpdate, ThreadOption.UserInterface)]
        public void LoginNameUpdateHandler(object sender, EventArgs<string> e)
        {
            View.SetLogingLabel(e.Data);
        }

        [EventSubscription(EventTopicNames.ConnectionInfoUpdate, ThreadOption.UserInterface)]
        public void ConnectionInfoUpdateHandler(object sender, EventArgs<string> e)
        {
            View.SetConnectionInfoLabel(e.Data);
        }

        [EventSubscription(EventTopicNames.ShowCurrentWork, ThreadOption.UserInterface)]
        public void ShowCurrentWorkHandler(object sender, EventArgs<bool> e)
        {
            View.ShowWorkId(e.Data);
        }

        [EventPublication(EventTopicNames.OnlineStatus, PublicationScope.Global)]
        public event EventHandler<EventArgs<bool>> OnlineStatusHandler;
        public void SetOnlineStatus(bool param)
        {
            if (OnlineStatusHandler != null)
                OnlineStatusHandler(this, new EventArgs<bool>(param));
        }

        [EventPublication(EventTopicNames.OnCloseViewDisconnect, PublicationScope.Global)]
        public event EventHandler<EventArgs> OnCloseViewDisconnectHandler;
        public void CloseAllViews()
        {
            if (OnCloseViewDisconnectHandler != null)
                OnCloseViewDisconnectHandler(this, new EventArgs());
        }

        /// <summary>
        /// Called when the user asks to exit the application.
        /// </summary>
        public void OnFileExit()
        {
            View.ParentForm.Close();
        }
    }
}
