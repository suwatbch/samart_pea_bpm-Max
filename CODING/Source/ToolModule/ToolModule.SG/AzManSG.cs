using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.ToolModule.Interface.Services;
using PEA.BPM.ToolModule.Interface.BusinessEntities;
using System.Data.Common;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Architecture.ArchitectureTool.Security;
using PEA.BPM.ToolModule.SG.AzManWCF;
using System.ServiceModel;
using WCFExtras.Soap;

namespace PEA.BPM.ToolModule.SG
{
    public class AzManSG : IAzManService
    {
        private AzManWCFServiceClient _ws;

        public AzManSG(bool onlineConnection)
        {
            _ws = new AzManWCFServiceClient();
            string absoluteUri = _ws.Endpoint.Address.Uri.AbsoluteUri;

            if (onlineConnection)
            {
                absoluteUri = absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.Center);
                _ws = new AzManWCFServiceClient("BasicHttpBinding_IAzManWCFService", new EndpointAddress(absoluteUri));
                if (Session.User.Token.Center == null) Authorization.LoadServerToken(true);
                _ws.AuthInfoValue = new AuthInfo();
                _ws.AuthInfoValue.UserId = Session.User.Id;
                _ws.AuthInfoValue.LocalIP = MachineInfo.GetLocalIP();
                _ws.AuthInfoValue.AuthToken = Session.User.Token.Center;
            }
            else
            {
                absoluteUri = absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.Branch);
                _ws = new AzManWCFServiceClient("BasicHttpBinding_IAzManWCFService", new EndpointAddress(absoluteUri));
                if (Session.User.Token.Branch == null) Authorization.LoadServerToken(false);
                _ws.AuthInfoValue = new AuthInfo();
                _ws.AuthInfoValue.UserId = Session.User.Id;
                _ws.AuthInfoValue.LocalIP = MachineInfo.GetLocalIP();
                _ws.AuthInfoValue.AuthToken = Session.User.Token.Branch;
            }
            _ws.InnerChannel.OperationTimeout = new TimeSpan(0, 1, 0);
        }       

        #region IAzManService Members

        public string RegisterTerminal(DbTransaction trans, Terminal terminal, out string terminalCode)
        {
            try
            {
                return _ws.RegisterTerminal_Compress(out terminalCode, ServiceHelper.CompressData<Terminal>(terminal));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Tools, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public void UpdateTerminal(Terminal terminal)
        {
            try
            {
                _ws.UpdateTerminal(terminal);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Tools, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public System.ComponentModel.BindingList<Role> ListRoles()
        {
            try
            {
                return ServiceHelper.DecompressData<System.ComponentModel.BindingList<Role>>((_ws.ListRoles_Compress()));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Tools, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public System.ComponentModel.BindingList<Role> ListExpRoles(User user)
        {
            try
            {
                return ServiceHelper.DecompressData<System.ComponentModel.BindingList<Role>>((_ws.ListExpRoles_Compress(user)));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Tools, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public System.ComponentModel.BindingList<Role> ListRolesByUser(string userId)
        {
            try
            {
                return ServiceHelper.DecompressData<System.ComponentModel.BindingList<Role>>((_ws.ListRolesByUser_Compress(userId)));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Tools, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public System.ComponentModel.BindingList<User> ListUsers(string filter)
        {
            try
            {
                return ServiceHelper.DecompressData<System.ComponentModel.BindingList<User>>(_ws.ListUsers_Compress(filter));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Tools, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public System.ComponentModel.BindingList<User> ListUsersByRole(string roleId)
        {
            try
            {
                return ServiceHelper.DecompressData<System.ComponentModel.BindingList<User>>(_ws.ListUsersByRole_Compress(roleId));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Tools, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public System.ComponentModel.BindingList<Function> ListFunctions(string roleId)
        {
            try
            {
                return ServiceHelper.DecompressData<System.ComponentModel.BindingList<Function>>((_ws.ListFunctions_Compress(roleId)));

            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Tools, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public System.ComponentModel.BindingList<User> SearchUsers(string keyword)
        {
            try
            {
                return ServiceHelper.DecompressData<System.ComponentModel.BindingList<User>>((_ws.SearchUsers_Compress(keyword)));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Tools, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public void CreateUser(User user)
        {
            try
            {
                _ws.CreateUser(user);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Tools, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public void EditUser(User user)
        {
            try
            {
                _ws.EditUser(user);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Tools, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public void DeleteUser(User user)
        {
            try
            {
                _ws.DeleteUser(user);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Tools, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public void CreateRole(Role role)
        {
            try
            {
                _ws.CreateRole(role);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Tools, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public void EditRole(Role role)
        {
            try
            {
                _ws.EditRole(role);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Tools, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public void DeleteRole(Role role)
        {
            try
            {
                _ws.DeleteRole(role);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Tools, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public void AddRoleUser(User user, Role role)
        {
            try
            {
                _ws.AddRoleUser(user, role);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Tools, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public void RemoveRoleUser(User user, Role role)
        {
            try
            {
                _ws.RemoveRoleUser(user, role);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Tools, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<User> ListEmployee(string filter)
        {
            try
            {
                return ServiceHelper.DecompressData<List<User>>((_ws.ListEmployee_Compress(filter)));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Tools, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<Function> ListAllFunctions()
        {
            try
            {
                return ServiceHelper.DecompressData<List<Function>>((_ws.ListAllFunctions_Compress()));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Tools, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public bool ValidateUserScopeByUser(User user)
        {
            try
            {
                return _ws.ValidateUserScopeByUser(user);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Tools, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public bool ValidateUserScopeByRole(Role role)
        {
            try
            {
                return _ws.ValidateUserScopeByRole(role);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Tools, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public bool ValidateUserScope(User user, Role role)
        {
            try
            {
                return _ws.ValidateUserScope(user, role);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Tools, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public int UpdateUser(DbTransaction trans, User user)
        {
            try
            {
                return _ws.UpdateUser_Compress(ServiceHelper.CompressData<User>(user));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Tools, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }


        public List<PeaInfo> SearchBranchByKeyword(string keyword, string userId)
        {
            try
            {
                return ServiceHelper.DecompressData<List<PeaInfo>>((_ws.SearchBranchByKeyword_Compress(keyword, userId)));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Tools, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<ReportUnlockingLog> GetUnlockingLogReport(UnlockingLogParam param)
        {
            try
            {
                return ServiceHelper.DecompressData<List<ReportUnlockingLog>>(_ws.GetUnlockingLogReport(param));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Tools, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }       

        public System.ComponentModel.BindingList<UnlockRemark> ListRemark(string fncId)
        {
            try
            {
                return ServiceHelper.DecompressData<System.ComponentModel.BindingList<UnlockRemark>>((_ws.ListRemark(fncId)));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Tools, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<Function> ListRemarkFunctions()
        {
            try
            {
                return ServiceHelper.DecompressData<List<Function>>(_ws.ListRemarkFunctions());
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Tools, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public int AddRemark(UnlockRemark rmk)
        {
            try
            {
                return _ws.AddRemark_Compress(ServiceHelper.CompressData<UnlockRemark>(rmk));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Tools, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public int EditRemark(UnlockRemark rmk)
        {
            try
            {
                return _ws.EditRemark_Compress(ServiceHelper.CompressData<UnlockRemark>(rmk));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Tools, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public int DeleteRemark(UnlockRemark rmk)
        {
            try
            {
                return _ws.DeleteRemark_Compress(ServiceHelper.CompressData<UnlockRemark>(rmk));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Tools, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<Function> ListUnlockableFunctions()
        {
            try
            {
                return ServiceHelper.DecompressData<List<Function>>(_ws.ListUnlockableFunctions());
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Tools, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        //For Test only Connection
        public DateTime GetDBServerTime(String WSUrl)
        {
            try
            {
                string absoluteUri = _ws.Endpoint.Address.Uri.AbsoluteUri;
                absoluteUri.Replace(Session.Server.Address.Center, WSUrl);
                EndpointAddress endPoint = new EndpointAddress(absoluteUri);
                _ws.Endpoint.Address = endPoint;
                return _ws.GetWSServerTime(WSUrl);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Tools, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
            
        }

        //For Test only Connection
        public DateTime GetWSServerTime(string WSUrl)
        {
            try
                
            {
                string absoluteUri = _ws.Endpoint.Address.Uri.AbsoluteUri;
                absoluteUri.Replace(Session.Server.Address.Center, WSUrl);
                EndpointAddress endPoint = new EndpointAddress(absoluteUri);
                _ws.Endpoint.Address = endPoint;
                return _ws.GetWSServerTime(WSUrl);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Tools, ex);
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

        #region IAzManService Members

        public System.ComponentModel.BindingList<UserExceed> ListUserLimitExceeds()
        {
            try
            {
                return ServiceHelper.DecompressData<System.ComponentModel.BindingList<UserExceed>>((_ws.ListUserLimitExceeds_Compress()));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.Tools, ex);
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
