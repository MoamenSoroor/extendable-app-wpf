using ExtendabeApp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extension1
{
    public class ExtensionApp1 : IAppExtension
    {
        private UserControl1 control = new UserControl1();

        public string Name => "Quote";

        public object Content => control;
        //public object Content => "Hello World From Extension 1";
    }
}
