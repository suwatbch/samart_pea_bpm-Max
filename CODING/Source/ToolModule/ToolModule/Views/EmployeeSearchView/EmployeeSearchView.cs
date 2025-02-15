//----------------------------------------------------------------------------------------
// patterns & practices - Smart Client Software Factory - Guidance Package
//
// This file was generated by the "Add View" recipe.
//
// This class is the concrete implementation of a View in the Model-View-Presenter 
// pattern. Communication between the Presenter and this class is acheived through 
// an interface to facilitate separation and testability.
// Note that the Presenter generated by the same recipe, will automatically be created
// by CAB through [CreateNew] and bidirectional references will be added.
//
// For more information see:
// ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.scsf.2006jun/SCSF/html/03-030-Model%20View%20Presenter%20%20MVP%20.htm
//
// Latest version of this Guidance Package: http://go.microsoft.com/fwlink/?LinkId=62182
//----------------------------------------------------------------------------------------

using System;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using PEA.BPM.Infrastructure.Interface;
using System.Collections.Generic;
using PEA.BPM.ToolModule.Interface.BusinessEntities;

namespace PEA.BPM.ToolModule
{
    [SmartPart]
    public partial class EmployeeSearchView : UserControl, IEmployeeSearchView
    {
        List<Employee> _employeeList;

        public EmployeeSearchView()
        {
            InitializeComponent();
            employeeGv.AutoGenerateColumns = false;
        }

        public List<Employee> EmployeeList
        {
            set 
            { 
                _employeeList = value;
                employeeGv.DataSource = _employeeList;
            } 
        }

        /// <summary>
        /// Sets the presenter. The dependency injection system will automatically
        /// create a new presenter for you.
        /// </summary>
        [CreateNew]
        public EmployeeSearchViewPresenter Presenter
        {
            set
            {
                _presenter = value;
                _presenter.View = this;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            _presenter.OnViewReady();
        }


        private void FindEmployee()
        {
            string keyword = keywordText.Text.Replace(" ", "");
            if (keyword == "")
                keyword = null;

            _presenter.SearchForEmployee(keyword);
        }

        private void findBt_Click(object sender, EventArgs e)
        {
            
        }

        private void keywordText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                FindEmployee();
        }

        private void cancelBt_Click(object sender, EventArgs e)
        {
            _presenter.OnCloseView();
        }

        private void okeyBt_Click(object sender, EventArgs e)
        {
            if (employeeGv.SelectedRows.Count == 0)
                return;

            //allow only one row selected
            Employee emp = (Employee)employeeGv.SelectedRows[0].DataBoundItem;
            _presenter.SelectEmployee(emp);
        }

        private void employeeGv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Employee emp = (Employee)employeeGv.Rows[e.RowIndex].DataBoundItem;
                _presenter.SelectEmployee(emp);
                _presenter.OnCloseView();
            }
        }

        private void fineBt_Click(object sender, EventArgs e)
        {
            FindEmployee();
        }

 

    }
}

