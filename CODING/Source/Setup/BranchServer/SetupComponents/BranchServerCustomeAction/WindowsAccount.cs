using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices.AccountManagement;
using System.Windows.Forms;

namespace PEA.BPM.Setup.BranchServer
{

    public class WindowsAccount
    {
        public static bool CreateLocalWindowsAccount(string username, string password, string displayName, string description, bool canChangePwd, bool pwdExpires)
        {
            try
            {
                PrincipalContext context = new PrincipalContext(ContextType.Machine);
                UserPrincipal user = new UserPrincipal(context);
                user.SetPassword(password);
                user.DisplayName = displayName;
                user.Name = username;
                user.Description = description;
                user.UserCannotChangePassword = canChangePwd;
                user.PasswordNeverExpires = pwdExpires;
                user.Save();

                //now add user to "Users" group so it displays in Control Panel
                GroupPrincipal group = GroupPrincipal.FindByIdentity(context, "Users");
                group.Members.Add(user);
                group.Save();

                return true;
            }
            catch (Exception )
            {
                throw;
            }
        }

        public static bool Exist(string username)
        {
            bool bReturn = false;
            PrincipalContext principalContext = new PrincipalContext(ContextType.Machine);
            UserPrincipal userPrincipal = UserPrincipal.FindByIdentity(principalContext, username);

            if (userPrincipal != null)
                bReturn = true;

            return bReturn;
        }

        public static bool DeleteLocalWindowsAcccount(string username)
        {
            // Creating the PrincipalContext
            PrincipalContext principalContext = new PrincipalContext(ContextType.Machine);

            //Deleting the user account
            bool bReturn = false;
            UserPrincipal userPrincipal = UserPrincipal.FindByIdentity(principalContext, username);

            if (userPrincipal != null)
            {                                
                try
                {
                    userPrincipal.Delete();
                    bReturn = true;
                }
                catch (Exception e)
                {
                    MessageBox.Show("Exception when deleting the account. " + e);
                }
            }

            return bReturn;
        }
    }
}
