using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.BillPrintingModule.Interface.Services;
using PEA.BPM.BillPrintingModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Architecture.ArchitectureTool.Security;
using PEA.BPM.BillPrintingModule.SG.BillPrintingWCF;
using System.ServiceModel;
using WCFExtras.Soap;


namespace PEA.BPM.BillPrintingModule.SG
{
    public class BillPrintingSG : IBillPrintingServices
    {
        private BillPrintingWCFServiceClient _ws;

        public BillPrintingSG(bool onlineConnection)
        {
            _ws = new BillPrintingWCFServiceClient();
            string absoluteUri = _ws.Endpoint.Address.Uri.AbsoluteUri;

            if (onlineConnection)
            {
                absoluteUri = absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.Center);
                _ws = new BillPrintingWCFServiceClient("BasicHttpBinding_IBillPrintingWCFService", new EndpointAddress(absoluteUri));
                if (Session.User.Token.Center == null) Authorization.LoadServerToken(true);
                _ws.AuthInfoValue = new AuthInfo();
                _ws.AuthInfoValue.UserId = Session.User.Id;
                _ws.AuthInfoValue.LocalIP = MachineInfo.GetLocalIP();
                _ws.AuthInfoValue.AuthToken = Session.User.Token.Center;
            }
            else
            {
                absoluteUri = absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.Branch);
                _ws = new BillPrintingWCFServiceClient("BasicHttpBinding_IBillPrintingWCFService", new EndpointAddress(absoluteUri));
                if (Session.User.Token.Branch == null) Authorization.LoadServerToken(false);
                _ws.AuthInfoValue = new AuthInfo();
                _ws.AuthInfoValue.UserId = Session.User.Id;
                _ws.AuthInfoValue.LocalIP = MachineInfo.GetLocalIP();
                _ws.AuthInfoValue.AuthToken = Session.User.Token.Branch;
            }
            _ws.InnerChannel.OperationTimeout = new TimeSpan(0, 5, 0);
        }

        #region IBillPrintingService Members

        public List<Bills> PrintBlueBill(BillPrintingConditionParam param)
        {
            try
            {
                return ServiceHelper.DecompressData<List<Bills>>(_ws.PrintBlueBill(param));
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

        public List<Bills> PrintGreenBill(BillPrintingConditionParam param)
        {
            try 
            {
                return ServiceHelper.DecompressData<List<Bills>>(_ws.PrintGreenBill(param));
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

        public List<Bills> PrintA4Bill(BillPrintingConditionParam param)
        {
            try 
            {
                return ServiceHelper.DecompressData<List<Bills>>(_ws.PrintA4Bill(param));
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

        public List<Bills> ReprintBlueBill(BillPrintingConditionParam param)
        {
            try 
            {
                return ServiceHelper.DecompressData<List<Bills>>(_ws.ReprintBlueBill(param));
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

        public List<Bills> ReprintGreenBill(BillPrintingConditionParam param)
        {
            try 
            {
                return ServiceHelper.DecompressData<List<Bills>>(_ws.ReprintGreenBill(param));
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

        public List<Bills> PrintGroupInvoiceA4Bill(BillPrintingConditionParam param)
        {
            try 
            {
                return ServiceHelper.DecompressData<List<Bills>>(_ws.PrintGroupInvoiceA4Bill(param));
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

        public List<Bills> ReprintGroupInvoiceA4Bill(BillPrintingConditionParam param)
        {
            try 
            {
                return ServiceHelper.DecompressData<List<Bills>>(_ws.ReprintGroupInvoiceA4Bill(param));
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

        public List<Bills> PrintGreenBillByBank(BillPrintingConditionParam param)
        {
            try 
            {
                return ServiceHelper.DecompressData<List<Bills>>(_ws.PrintGreenBillByBank(param));
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

        public List<Bills> ReprintGreenBillByBank(BillPrintingConditionParam param)
        {
            try 
            {
                return ServiceHelper.DecompressData<List<Bills>>(_ws.ReprintGreenBillByBank(param));
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

        public List<Bills> PrintBlueBillByBank(BillPrintingConditionParam param)
        {
            try 
            {
                return ServiceHelper.DecompressData<List<Bills>>(_ws.PrintBlueBillByBank(param));
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

        public List<Bills> ReprintBlueBillByBank(BillPrintingConditionParam param)
        {
            try 
            {
                return ServiceHelper.DecompressData<List<Bills>>(_ws.ReprintBlueBillByBank(param));
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

        public List<PrintableId> CheckExistRecord(BillPrintingConditionParam param)
        {
            try
            {
                return ServiceHelper.DecompressData<List<PrintableId>>(_ws.CheckExistRecord(param));
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

        public List<PrintableId> CheckExistReprintRecord(BillPrintingConditionParam param)
        {
            try 
            {
                return ServiceHelper.DecompressData<List<PrintableId>>(_ws.CheckExistReprintRecord(param));
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

        public List<PrintableId> CheckExistGroupInvoiceRecord(GroupInvoiceParam param)
        {
            try 
            {
                return ServiceHelper.DecompressData<List<PrintableId>>(_ws.CheckExistGroupInvoiceRecord(param));
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

        public List<PrintableId> CheckExistReprintGroupInvoiceRecord(GroupInvoiceParam param)
        {
            try 
            {
                return ServiceHelper.DecompressData<List<PrintableId>>(_ws.CheckExistReprintGroupInvoiceRecord(param));
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

        public List<PrintableIdByBank> CheckExistByBank(GreenBillParam param)
        {
            try 
            {
                return ServiceHelper.DecompressData<List<PrintableIdByBank>>(_ws.CheckExistByBank(param));
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

        public List<PrintableIdByBank> CheckExistReprintByBank(GreenBillReprintParam param)
        {
            try 
            {
                return ServiceHelper.DecompressData<List<PrintableIdByBank>>(_ws.CheckExistReprintByBank(param));
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

        public List<PrintableId> CheckExistGreenReceipt(BillPrintingConditionParam param)
        {
            try 
            {
                return ServiceHelper.DecompressData<List<PrintableId>>(_ws.CheckExistGreenReceipt(param));
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

        public List<PrintableId> CheckExistReprintGreenReceipt(BillPrintingConditionParam param)
        {
            try 
            {
                return ServiceHelper.DecompressData<List<PrintableId>>(_ws.CheckExistReprintGreenReceipt(param));
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

        public List<Bills> ReprintGreenReceipt(BillPrintingConditionParam param)
        {
            try 
            {
                return ServiceHelper.DecompressData<List<Bills>>(_ws.ReprintGreenReceipt(param));
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

        public List<Bills> PrintGreenReceipt(BillPrintingConditionParam param)
        {
            try
            {
                return ServiceHelper.DecompressData<List<Bills>>(_ws.PrintGreenReceipt(param));
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

        public List<Bills> ReprintSpotBill(BillPrintingConditionParam param)
        {
            try
            {
                return ServiceHelper.DecompressData<List<Bills>>(_ws.ReprintSpotBill(param));
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

        public List<Bills> PrintSpotBill(BillPrintingConditionParam param)
        {
            try
            {
                return ServiceHelper.DecompressData<List<Bills>>(_ws.PrintSpotBill(param));
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
