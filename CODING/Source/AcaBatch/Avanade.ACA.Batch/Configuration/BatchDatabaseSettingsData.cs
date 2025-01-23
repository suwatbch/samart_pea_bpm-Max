using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections;

namespace Avanade.ACA.Batch.Configuration
{
    public class BatchDatabaseSettingsData : ConfigurationElement, IBatchDBData
    {
        private const string databaseInstanceNameProperty = "databaseInstanceName";

        private Database databaseInstance = null;
        private JobTypeCollection jobTypeCollection;
        private BatchTypeCollection batchTypeCollection;
        private JobDefinitionCollection jobDefinitionCollection;
        private BatchDefinitionCollection batchDefinitionCollection;
        private DestinationCollection destinationCollection;
        private ArrayList alChildren;

        /// <summary>BatchDatabaseSettingsData</summary>
        public BatchDatabaseSettingsData() : this(string.Empty)
        {
        }

        /// <summary>BatchDatabaseSettingsData</summary>
        /// <param name="db">Microsoft.Practices.EnterpriseLibrary.Data.Database</param>
        /// <returns>void</returns>
        public BatchDatabaseSettingsData(Database db):this(string.Empty)            
        {
            databaseInstance = db;
            LoadChildren();
        }

        /// <summary>BatchDatabaseSettingsData</summary>
        /// <param name="databaseInstanceName">System.String</param>
        public BatchDatabaseSettingsData(string databaseInstanceName) : base()
        {
            this[databaseInstanceNameProperty] = databaseInstanceName;
            this.jobTypeCollection = new JobTypeCollection();
            this.batchTypeCollection = new BatchTypeCollection();
            this.destinationCollection = new DestinationCollection();
            this.jobDefinitionCollection = new JobDefinitionCollection(jobTypeCollection);
            this.batchDefinitionCollection = new BatchDefinitionCollection(batchTypeCollection, destinationCollection, jobDefinitionCollection);
            alChildren = new ArrayList(5);
        }

        /// <summary>LoadChildren</summary>
        private void LoadChildren()
        {
            this.jobTypeCollection.LoadJobTypes(databaseInstance);
            this.batchTypeCollection.LoadBatchTypes(databaseInstance);
            this.destinationCollection.LoadDestinations(databaseInstance);
            this.jobDefinitionCollection.LoadJobDefinitions(databaseInstance);
            this.batchDefinitionCollection.LoadBatchDefinitions(databaseInstance);
        }

        [ConfigurationProperty(databaseInstanceNameProperty, IsRequired = true)]
        public string DatabaseInstanceName
        {
            get 
            { 
                return this[databaseInstanceNameProperty].ToString();
            }
            set 
            { 
                this[databaseInstanceNameProperty] = value; 
            }
        }
                
        public Database DatabaseInstance
        {
            get
            {
                return databaseInstance;
            }
            set
            {
                databaseInstance = value;
            }
        }

        public JobTypeCollection JobTypeCollection
        {
            get { return this.jobTypeCollection; }
            set { this.jobTypeCollection = value; }
        }

        /// <summary>
        /// The Batch types in the Batch database.
        /// </summary>
        /// <value>a collection of the <see cref="BatchTypeData"/>.</value>        
        public BatchTypeCollection BatchTypeCollection
        {
            get { return this.batchTypeCollection; }
            set { this.batchTypeCollection = value; }
        }

        /// <summary>
        /// The Job definitation instances in the Batch database.
        /// </summary>
        /// <value>a collection of the <see cref="JobDefinitionData"/>.</value>        
        public JobDefinitionCollection JobDefinitionCollection
        {
            get { return this.jobDefinitionCollection; }
            set { this.jobDefinitionCollection = value; }
        }

        /// <summary>
        /// The Batch definition instances in the Batch database.
        /// </summary>
        /// <value>a collection of the <see cref="BatchDefinitionData"/>.</value>        
        public BatchDefinitionCollection BatchDefinitionCollection
        {
            get { return this.batchDefinitionCollection; }
            set { this.batchDefinitionCollection = value; }
        }


        /// <summary>
        /// The defined Batch destination instances in the Batch database.
        /// </summary>
        /// <value>a collection of the <see cref="DestinationData"/>.</value>        
        public DestinationCollection DestinationCollection
        {
            get { return this.destinationCollection; }
            set { this.destinationCollection = value; }
        }

        /// <summary>
        /// The Key of the object instance.  Unused for this class. Always returns <see cref="Guid.Empty"/>.
        /// </summary>        
        public Guid Key
        {
            get
            {
                return Guid.Empty;
            }
        }

        /// <summary>
        /// Provides the list of the child configurations for the design time.  The children are
        /// represented by a list consist of <see cref="JobTypeCollection"/>, 
        /// <see cref="BatchTypeCollection"/>, <see cref="DestinationCollection"/>, 
        /// <see cref="JobDefinitionCollection"/>, and <see cref="BatchDefinitionCollection"/>
        /// </summary>        
        public System.Collections.IList Children
        {
            get
            {
                alChildren.Clear();
                alChildren.Add(jobTypeCollection);
                alChildren.Add(batchTypeCollection);
                alChildren.Add(destinationCollection);
                alChildren.Add(jobDefinitionCollection);
                alChildren.Add(batchDefinitionCollection);
                return this.alChildren;
            }
        }

        #region IBatchData Members

        public string DisplayName
        {
            get
            {
                return this[databaseInstanceNameProperty].ToString();
            }
        }

        #endregion
    }
}
