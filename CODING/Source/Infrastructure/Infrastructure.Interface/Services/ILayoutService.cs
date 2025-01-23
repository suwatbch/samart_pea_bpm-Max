using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Infrastructure.Interface.Services
{
    public interface ILayoutService
    {
        void LoadPlainLayout();
        void LoadVerticalLayout(int topSize);
        void LoadHorizontalLayout(int leftSize);
    }
}
