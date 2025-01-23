// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2001 - 2006 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace Avanade.ACA.Batch.Instrumentation
{
    /// <summary>
    /// Summary for BatchServerHostEventArgs class
    /// </summary>
    public class BatchServerHostEventArgs : EventArgs
    {
        private string _destination;
        private bool _started;
        private const string DefaultServerHostName = "ACA.NET Batch Server";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="destination_">System.String</param>
        /// <param name="started_">System.Boolean</param>
        public BatchServerHostEventArgs(string destination_, bool started_)
        {
            _destination = destination_;
            _started = started_;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Destination
        {
            get
            {
                return _destination;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Started
        {
            get
            {
                return _started;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ApplicationBase
        {
            get
            {
                return AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ApplicationName
        {
            get
            {
                return DefaultServerHostName;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ConfigurationFile
        {
            get
            {
                return AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            }
        }
    }
}
