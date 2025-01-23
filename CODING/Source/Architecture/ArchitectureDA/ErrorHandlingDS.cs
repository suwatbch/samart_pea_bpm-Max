using System.Data.SqlClient;
using System.Data;
namespace PEA.BPM.Architecture.ArchitectureDA {


    partial class ErrorHandlingDS
    {
        partial class tErrorMessageDataTable
        {
        }
    
        partial class mErrorMessageDataTable
        {
            

        }

    }

}


namespace PEA.BPM.Architecture.ArchitectureDA.ErrorHandlingDSTableAdapters
{

    public partial class mErrorMessageTableAdapter
    {
        private SqlTransaction _trans = null;
        public void SetTransaction(SqlTransaction trans)
        {
            _trans = trans;
            if (this.Adapter.SelectCommand != null) this.Adapter.SelectCommand.Transaction = _trans;
            if (this.Adapter.InsertCommand != null) this.Adapter.InsertCommand.Transaction = _trans;
            if (this.Adapter.UpdateCommand != null) this.Adapter.UpdateCommand.Transaction = _trans;
            if (this.Adapter.DeleteCommand != null) this.Adapter.DeleteCommand.Transaction = _trans;

            for (int i = 0; (i < this.CommandCollection.Length); i++)
            {
                if (this.CommandCollection[i] != null) this.CommandCollection[i].Transaction = _trans;
            }
        }
    }
    
    public partial class mErrorMessageKeyTableAdapter
    {
        private SqlTransaction _trans = null;
        public void SetTransaction(SqlTransaction trans)
        {
            _trans = trans;
            if (this.Adapter.SelectCommand != null) this.Adapter.SelectCommand.Transaction = _trans;
            if (this.Adapter.InsertCommand != null) this.Adapter.InsertCommand.Transaction = _trans;
            if (this.Adapter.UpdateCommand != null) this.Adapter.UpdateCommand.Transaction = _trans;
            if (this.Adapter.DeleteCommand != null) this.Adapter.DeleteCommand.Transaction = _trans;

            for (int i = 0; (i < this.CommandCollection.Length); i++)
            {
                if (this.CommandCollection[i] != null) this.CommandCollection[i].Transaction = _trans;
            }
        }
    }

    public partial class tErrorMessageTableAdapter
    {
        private SqlTransaction _trans = null;
        public void SetTransaction(SqlTransaction trans)
        {
            _trans = trans;
            if (this.Adapter.SelectCommand != null) this.Adapter.SelectCommand.Transaction = _trans;
            if (this.Adapter.InsertCommand != null) this.Adapter.InsertCommand.Transaction = _trans;
            if (this.Adapter.UpdateCommand != null) this.Adapter.UpdateCommand.Transaction = _trans;
            if (this.Adapter.DeleteCommand != null) this.Adapter.DeleteCommand.Transaction = _trans;

            for (int i = 0; (i < this.CommandCollection.Length); i++)
            {
                if (this.CommandCollection[i] != null) this.CommandCollection[i].Transaction = _trans;
            }
        }
    }


}
