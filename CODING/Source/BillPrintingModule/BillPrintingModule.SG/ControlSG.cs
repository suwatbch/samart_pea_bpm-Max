using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.BillPrintingModule.Interface.Services;
using PEA.BPM.BillPrintingModule.Interface.BusinessEntities;
using System.Data.Common;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;
using PEA.BPM.Architecture.ArchitectureTool.Security;
using PEA.BPM.BillPrintingModule.SG.ControlWCF;
using System.ServiceModel;
using WCFExtras.Soap;


namespace PEA.BPM.BillPrintingModule.SG
{
    public class ControlSG : IControlServices
    {
        private ControlWCFServiceClient _ws;

        public ControlSG(bool onlineConnection)
        {
            _ws = new ControlWCFServiceClient();
            string absoluteUri = _ws.Endpoint.Address.Uri.AbsoluteUri;

            if (onlineConnection)
            {
                absoluteUri = absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.Center);
                _ws = new ControlWCFServiceClient("BasicHttpBinding_IControlWCFService", new EndpointAddress(absoluteUri));
                if (Session.User.Token.Center == null) Authorization.LoadServerToken(true);
                _ws.AuthInfoValue = new AuthInfo();
                _ws.AuthInfoValue.UserId = Session.User.Id;
                _ws.AuthInfoValue.LocalIP = MachineInfo.GetLocalIP(); 
                _ws.AuthInfoValue.AuthToken = Session.User.Token.Center;
            }
            else
            {
                absoluteUri = absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.Branch);
                _ws = new ControlWCFServiceClient("BasicHttpBinding_IControlWCFService", new EndpointAddress(absoluteUri));
                if (Session.User.Token.Branch == null) Authorization.LoadServerToken(false);
                _ws.AuthInfoValue = new AuthInfo();
                _ws.AuthInfoValue.UserId = Session.User.Id;
                _ws.AuthInfoValue.LocalIP = MachineInfo.GetLocalIP(); 
                _ws.AuthInfoValue.AuthToken = Session.User.Token.Branch;
            }
            _ws.InnerChannel.OperationTimeout = new TimeSpan(0, 1, 0);
        }

        #region IControlService Members

        public List<DeliveryPlace> GetDeliveryPlace(string createBranchId) 
        {
            try 
            {
                return new List<DeliveryPlace>(_ws.GetDeliveryPlace(createBranchId));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.BLAN, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        

        public string InsertDeliveryPlace(DbTransaction trans, DeliveryPlace dp)
        {
            try 
            {
                return _ws.InsertDeliveryPlace(dp);
            }
             catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.BLAN, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public string UpdateDeliveryPlace(DbTransaction trans, DeliveryPlace dp)
        {
            try 
            {
                return _ws.UpdateDeliveryPlace(dp);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.BLAN, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public string DeleteDeliveryPlace(DbTransaction trans, DeliveryPlace dp)
        {
            try 
            {
                return _ws.DeleteDeliveryPlace(dp);
            }
             catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.BLAN, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<String> GetChildBranch(string branchId)
        {
            try 
            {
                return new List<String>(_ws.GetChildBranch(branchId));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.BLAN, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<Bank> GetBank(string branchId)
        {
            try 
            {
                return new List<Bank>(_ws.GetBank(branchId));
            }
             catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.BLAN, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }
      
        public List<Portion> GetPortion(string branchId)
        {
            try 
            {
                return new List<Portion>(_ws.GetPortion(branchId));
            }
           catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.BLAN, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<AuthorizedPerson> GetApprover(string createBranchId)
        {
            try 
            {
                return new List<AuthorizedPerson>(_ws.GetApprover(createBranchId));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.BLAN, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public string InsertApprover(DbTransaction trans, AuthorizedPerson approver)
        {
            try 
            {
                return _ws.InsertApprover(approver);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.BLAN, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public string UpdateApprover(DbTransaction trans, AuthorizedPerson approver)
        {
            try 
            {
                return _ws.UpdateApprover(approver);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.BLAN, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public string DeleteApprover(DbTransaction trans, AuthorizedPerson approver)
        {
            try 
            {
                return _ws.DeleteApprover(approver);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.BLAN, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<Invoice> GetContractAccountHistory(string caId, string printBranch)
        {
            try
            {
                   return new List<Invoice>(_ws.GetContractAccountHistory(caId, printBranch));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.BLAN, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<BarcodeMRU> GetBarcodeMRU(ManageBarcodeParam param)
        {
            try 
            {
                return new List<BarcodeMRU>(_ws.GetBarcodeMRU(param));
            }
           catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.BLAN, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public void UpdateBarcodeMRU(BarcodeMRU param)
        {
            try
            {
                _ws.UpdateBarcodeMRU(param);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.BLAN, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        #endregion
    }
}
