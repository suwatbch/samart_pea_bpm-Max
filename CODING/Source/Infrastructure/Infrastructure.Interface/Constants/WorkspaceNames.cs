//----------------------------------------------------------------------------------------
// patterns & practices - Smart Client Software Factory - Guidance Package
//
// This file was generated by this guidance package as part of the solution template
//
// For more information see: 
// ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.scsf.2006jun/SCSF/html/03-210-Creating%20a%20Smart%20Client%20Solution.htm
//
// Latest version of this Guidance Package: http://go.microsoft.com/fwlink/?LinkId=62182
//----------------------------------------------------------------------------------------

namespace PEA.BPM.Infrastructure.Interface.Constants
{
    /// <summary>
    /// Constants for workspace names.
    /// </summary>
    public class WorkspaceNames
    {
        public const string LayoutWorkspace = "LayoutWorkspace";
        public const string CenterWorkspace = "CenterWorkspace";

        public class PlainLayout
        {
            public const string CenterWorkspace = "CenterWorkspacex";
        }

        public class HorizontalLayout
        {
            public const string LeftWorkspace = "LeftWorkspace";
            public const string RightWorkspace = "RightWorkspace";
        }

        public class VerticalLayout
        {
            public const string TopWorkspace = "TopWorkspace";
            public const string BottomWorkspace = "BottomWorkspace";
        }

        public const string ModalWindows = "ModalWindows";
    }
}
