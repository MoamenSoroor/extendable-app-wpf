using ExtendabeApp.Shared;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExtendableAppWPF
{
    class ModuleLoader
    {
        // Allow user to select an assembly to load.
        public IAppExtension LoadSnapin()
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                //set the initial directory to the path of this project
                InitialDirectory = Assembly.GetExecutingAssembly().Location,
                Filter = "assembly (*.dll)|*.dll|All Files (*.*)|*.*",
                FilterIndex = 1
            };

            if (dlg.ShowDialog() == false)
                return null;
            else
            {
                var asm = Assembly.LoadFrom(dlg.FileName);
                if (asm == null)
                    throw new Exception("can't load the selected assembly.");
                if (asm.FullName == Assembly.GetExecutingAssembly().FullName)
                    throw new Exception("you can't load the current assembly.");

                return LoadExternalModule(asm);
            }
            
        }

        // The LoadExternalModule() method performs the following tasks:
        // • Dynamically loads the selected assembly into memory.
        // • Determines whether the assembly contains any types implementing IAppExtension
        // • Creates the type using late binding
        private IAppExtension LoadExternalModule(Assembly assembly)
        {
            // Get all IAppExtension compatible classes in assembly.
            var theClassTypes = (from t in assembly.GetTypes()
                                where t.IsClass && (t.GetInterface(nameof(IAppExtension)) != null)
                                select t).ToList();

            if (theClassTypes.Count() != 1)
                throw new Exception("no extensions, or more than one with the same assembly");

            IAppExtension itfApp = (IAppExtension)assembly.CreateInstance(theClassTypes.First().FullName, true);

            return itfApp;
        }

    }
}
