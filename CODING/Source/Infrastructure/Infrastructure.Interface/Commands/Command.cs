//----------------------------------------------------------------------------------------
// patterns & practices - Smart Client Software Factory - Guidance Package
//
// This file was generated by this guidance package as part of the solution template
//
// For more information see: 
// ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.scsf.2006jun/SCSF/html/03-210-Creating%20a%20Smart%20Client%20Solution.htm
//
// Latest version of this Guidance Package: http://go.microsoft.com/fwlink/?LinkId=62182
//----------------------------------------------------------------------------------------

using System;

namespace PEA.BPM.Infrastructure.Interface.Commands
{
    public abstract class Command<TService> : ICommand
        where TService : ISupportsTimeout
    {
        private TService _service;
        private int _timeout;

        protected Command(TService service, int timeout)
        {
            _service = service;
            _timeout = timeout;
        }

        protected TService Service
        {
            get { return _service; }
        }

        public int Timeout
        {
            get { return _timeout; }
            set { _timeout = value; }
        }

        public void Execute()
        {
            int oldTimeout = Service.Timeout;
            Service.Timeout = _timeout;

            try
            {
                DoExecute();
            }
            finally
            {
                _service.Timeout = oldTimeout;
            }
        }

        protected abstract void DoExecute();
    }
}
