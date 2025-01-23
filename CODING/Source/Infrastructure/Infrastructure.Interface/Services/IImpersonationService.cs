//----------------------------------------------------------------------------------------
// patterns & practices - Smart Client Software Factory - Guidance Package
//
// This file was generated by this guidance package as part of the solution template
//
// The IImpersonationService interface defines the contract to implement impersonation.
// The Reference Implementation 2 has an implementation using GenericPrincipal
//
// For more information see: 
// ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.scsf.2006jun/SCSF/html/03-210-Creating%20a%20Smart%20Client%20Solution.htm
//
// Latest version of this Guidance Package: http://go.microsoft.com/fwlink/?LinkId=62182
//----------------------------------------------------------------------------------------

using Microsoft.Practices.CompositeUI.Services;

namespace PEA.BPM.Infrastructure.Interface.Services
{
    public interface IImpersonationService
    {
        IAuthenticationService AuthenticationService
        {
            get;
        }

        IImpersonationContext Impersonate();
    }
}
