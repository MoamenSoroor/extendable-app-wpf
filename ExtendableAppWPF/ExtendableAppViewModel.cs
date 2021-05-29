using ExtendabeApp.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ExtendableAppWPF
{
    public class ExtendableAppViewModel : INotifyPropertyChanged
    {

        public ObservableCollection<IAppExtension> AppExtensions { get; set; }

        private IAppExtension selectedAppExtension;
        public IAppExtension SelectedAppExtension
        {
            get => selectedAppExtension;
            set
            {
                if(selectedAppExtension != value)
                {
                    selectedAppExtension = value;
                    OnPropertyChanged();
                    Content = selectedAppExtension?.Content;
                }
            }
        }

        private object content;
        public object Content
        {
            get => content;

            set
            {
                if (content != value)
                {
                    content = value;
                    OnPropertyChanged();
                }
            }
        }

        public RelayCommand<IAppExtension> AddAppExtension { get; }
        public RelayCommand<IAppExtension> RemoveAppExtension { get; }
        public RelayCommand<IAppExtension> AppExtensionsSelectionChanged { get; }



        public event PropertyChangedEventHandler PropertyChanged;


        protected void OnPropertyChanged([CallerMemberName] string propery = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propery));
        }

        private ModuleLoader moduleLoader;

        public ExtendableAppViewModel()
        {
            moduleLoader = new ModuleLoader();
            AppExtensions = new ObservableCollection<IAppExtension>();
            AddAppExtension = new RelayCommand<IAppExtension>();
            RemoveAppExtension = new RelayCommand<IAppExtension>();
            AppExtensionsSelectionChanged = new RelayCommand<IAppExtension>();

            AddAppExtension.ExecuteHandler += AddAppExtension_ExecuteHandler;
            RemoveAppExtension.ExecuteHandler += RemoveAppExtension_ExecuteHandler;
            AppExtensionsSelectionChanged.ExecuteHandler += AppExtensionsSelectionChanged_ExecuteHandler;

        }

        private void AppExtensionsSelectionChanged_ExecuteHandler(IAppExtension obj)
        {
            
        }

        private void RemoveAppExtension_ExecuteHandler(IAppExtension obj)
        {
            AppExtensions.Remove(SelectedAppExtension);
        }

        private void AddAppExtension_ExecuteHandler(IAppExtension obj)
        {
            try
            {
                var extension = moduleLoader.LoadSnapin();
                if (extension == null)
                    return;
                if (AppExtensions.Any(a => a.Name == extension.Name))
                    throw new Exception("The Selected Extenion already exists.");
                
                AppExtensions.Add(extension);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Can't Load External Module");
            }



        }
    }
}
