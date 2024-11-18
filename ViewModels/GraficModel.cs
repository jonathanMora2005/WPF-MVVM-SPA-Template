using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WPF_MVVM_SPA_Template.Views;

namespace WPF_MVVM_SPA_Template.ViewModels
{
    internal class GraficModel : INotifyPropertyChanged

    {
        private Option1ViewModel _option1ViewModel;
        public void afeixiOption1(Option1ViewModel ViewModel)
        {
            _option1ViewModel = ViewModel;
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public static implicit operator GraficModel(Window1 v)
        {
            throw new NotImplementedException();
        }
    }





}
