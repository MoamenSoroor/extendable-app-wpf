using ExtendabeApp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extension2
{
    public class ExtensionApp2 : IAppExtension
    {

        private UserControl2 control = new UserControl2();

        public string Name => "Login Module Extension";

        public object Content => control;
    }
}
