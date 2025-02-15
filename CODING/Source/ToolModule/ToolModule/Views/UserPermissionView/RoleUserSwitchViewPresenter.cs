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
using PEA.BPM.ToolModule.Interface.Services;
using System.Windows.Forms;
using System.Collections.Generic;
using PEA.BPM.ToolModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;
using Microsoft.Practices.CompositeUI.EventBroker;
using PEA.BPM.ToolModule.Interface.Constants;
using System.Threading;
using System.Collections;
using System.ComponentModel;

namespace PEA.BPM.ToolModule
{
    public class RoleUserSwitchViewPresenter : Presenter<IRoleUserSwitchView>
    {
        IAzManService _iAzmanService;        
        
        [InjectionConstructor]
        public RoleUserSwitchViewPresenter([ServiceDependency] IAzManService iAzManService)
		{
            _iAzmanService = iAzManService;
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

        public BindingList<User> ListAllUsers(string filter)
        {
            return _iAzmanService.ListUsers(filter);
        }

        public BindingList<User> ListUsersByRole(string roleId)
        {
            return _iAzmanService.ListUsersByRole(roleId);
        }

        public BindingList<Role> ListAllRoles()
        {
            return _iAzmanService.ListRoles();
        }

        public BindingList<Role> ListExpRoles(User user)
        {
            return _iAzmanService.ListExpRoles(user);
        }

        public BindingList<Role> ListRolesByUser(string userId)
        {
            return _iAzmanService.ListRolesByUser(userId);
        }

        public BindingList<Function> ListFunctions(string roleId)
        {
            return _iAzmanService.ListFunctions(roleId);
        }

        public void AddRoleUser(User user, Role role)
        {
            _iAzmanService.AddRoleUser(user, role);
        }

        public List<User> ListEmployee(string filter)
        {
            return _iAzmanService.ListEmployee(filter);
        }

        public void EditUser(User user)
        {
            _iAzmanService.EditUser(user);
        }

        public void CreateUser(User user)
        {
            _iAzmanService.CreateUser(user);
        }

        public void DeleteUser(User user)
        {
            _iAzmanService.DeleteUser(user);
        }

        public void RemoveRoleUser(User user, Role role)
        {
            _iAzmanService.RemoveRoleUser(user, role);
        }

        public void DeleteRole(Role role)
        {
            _iAzmanService.DeleteRole(role);
        }

        public List<Function> ListAllFunction()
        {
            return _iAzmanService.ListAllFunctions();
        }

        public void CreateRole(Role role)
        {
            _iAzmanService.CreateRole(role);
        }

        public void EditRole(Role role)
        {
            _iAzmanService.EditRole(role);
        }

        public bool ValidateUserScope(User user)
        {
            if (!_iAzmanService.ValidateUserScopeByUser(user))
            {
                MessageBox.Show("�Ѵ��駡Ѻ�ͺࢵ�����ҹ�ͧ����� ��سҵ�Ǩ�ͺ", "���͹حҵ�", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else
            {
                return true;
            }
        }
        //change role e.g. add function to role
        public bool ValidateUserScope(Role role)
        {
            if (!_iAzmanService.ValidateUserScopeByRole(role))
            {
                MessageBox.Show("�Ѵ��駡Ѻ�ͺࢵ�����ҹ�ͧ����� ��سҵ�Ǩ�ͺ", "���͹حҵ�", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool ValidateUserScope(User user, Role role)
        {
            if (!_iAzmanService.ValidateUserScope(user, role))
            {
                MessageBox.Show("�Ѵ��駡Ѻ�ͺࢵ�����ҹ�ͧ����� ��سҵ�Ǩ�ͺ", "���͹حҵ�", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else
            {
                return true;
            }
        }

        

        public BindingList<UserExceed> ListUserLimitExceedsdDetails()
        {
            return _iAzmanService.ListUserLimitExceeds();
        }
    }
}

