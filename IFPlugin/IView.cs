using System;
using System.Collections.Generic;
using System.Text;

namespace IFPlugin
{
   public interface IView
    {
        void Init(Dictionary<string, string> arg);
        void Close();
    }
}
