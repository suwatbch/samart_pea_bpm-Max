using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities
{
    public class UserProfileExt
    {
        private UserProfile _userProfile;
        private string _scopeId;

        public UserProfile UserProfile
        {
            set { _userProfile = value; }
            get { return _userProfile; }
        }

        public string ScopeId
        {
            set { _scopeId = value; }
            get { return _scopeId; }
        }
    }
}
