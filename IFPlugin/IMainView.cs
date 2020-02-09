using System;
using System.Collections.Generic;
using System.Text;

namespace IFPlugin
{
   public interface IMainView:IView
    {
        void Show();

        string StartupUri();



    }
}
