using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Linq;

namespace WPF_MVVM_SPA_Template.ViewModels
{
    class ConfiguracioModel : INotifyPropertyChanged {
        private readonly MainViewModel _mainViewModel;
        private string _buttonContent1 = "Seleciona el tema";
        public string ButtonContent1
        {
            get { return _buttonContent1; }
            set
            {
                _buttonContent1 = value;
                OnPropertyChanged(nameof(ButtonContent1));
            }
        }
        private string _buttonContent5 = "Fosc";
        public string ButtonContent5
        {
            get { return _buttonContent5; }
            set
            {
                _buttonContent5 = value;
                OnPropertyChanged(nameof(ButtonContent5));
            }
        }
        private string _buttonContent3 = "Clar";
        public string ButtonContent3
        {
            get { return _buttonContent3; }
            set
            {
                _buttonContent3 = value;
                OnPropertyChanged(nameof(ButtonContent3));
            }
        }
        private string _buttonContent2 = "Seleciona el idioma";
        public string ButtonContent2
        {
            get { return _buttonContent2; }
            set
            {
                _buttonContent2 = value;
                OnPropertyChanged(nameof(ButtonContent2));
            }
        }
        public ConfiguracioModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            ChangeToDarkThemeCommand = new RelayCommand(ChangeToDarkTheme);
            ChangeToLightThemeCommand = new RelayCommand(ChangeToLightTheme);
        }
        public void ChangeToDarkTheme(object? parameter)
        {
            tema.Source = new Uri("pack://application:,,/Views/Themes/DarkTheme.xaml");
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(tema);
            _mainViewModel.SelectedView = "Option5";
            _mainViewModel.ChangeView();
        }
        

        public void ChangeToLightTheme(object? parameter)
        {
            tema.Source = new Uri("pack://application:,,/Views/Themes/ModernTheme.xaml");
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(tema);
            _mainViewModel.SelectedView = "Option5";
            _mainViewModel.ChangeView();
        }
        
        private Option1ViewModel _option1ViewModel;
        private EditaroGuardaModel _option2ViewModel;
        public void enviarView2(EditaroGuardaModel _option2ViewModel)
        {
            this._option2ViewModel = _option2ViewModel;
        }

        private string _selectedIdioma;
        public string SelectedIdioma
        {
            get { return _selectedIdioma; }
            set
            {
                if (_selectedIdioma != value)
                {
                    _selectedIdioma = value;
                    OnPropertyChanged(nameof(SelectedIdioma));
                    CambiarIdioma();  
                }
            }
        }

        public RelayCommand ChangeToDarkThemeCommand { get; set; }
        public RelayCommand ChangeToLightThemeCommand { get; set; }
        ResourceDictionary tema = new ResourceDictionary();
        private void CambiarIdioma()
           
        {
            if (SelectedIdioma.Split(" ")[1] == "Catala")
            {
                ButtonContent1 = "Seleciona el tema";
                ButtonContent2 = "Seleciona el idioma";
                ButtonContent3 = "Clar";
                ButtonContent5 = "Fosc";

                _option1ViewModel.ButtonContent = "Editar";
                _option1ViewModel.ButtonContent2 = "Grafic";
                _option1ViewModel.ButtonContent3 = "Inserta";
                _option1ViewModel.ButtonContent4 = "Eliminar";
                _option1ViewModel.ButtonContent5 = "Nom";
                _option2ViewModel.NameContent = "Nom";
                _option2ViewModel.CognomContent = "DNI";
                _option2ViewModel.EmailContent = "Correu";
                _option2ViewModel.PhoneContent = "Telefon";
                _option2ViewModel.DataContent = "Data";
                _option2ViewModel.CancelarContent = "Cancelar";
                _option2ViewModel.SaveContent = "Guarda";


            }
            else if (SelectedIdioma.Split(" ")[1] == "Castella")
            {
                ButtonContent1 = "Seleciona el tema";
                ButtonContent2 = "Seleciona el idioma";
                ButtonContent3 = "Claro";
                _option2ViewModel.SaveContent = "Guardar";

                ButtonContent5 = "Oscuro";
                _option1ViewModel.ButtonContent2 = "Grafico";
                _option1ViewModel.ButtonContent3 = "Insertar";
                _option1ViewModel.ButtonContent = "Editar";
                _option1ViewModel.ButtonContent4 = "Eliminar";
                _option1ViewModel.ButtonContent5 = "Nombre";
                _option2ViewModel.NameContent = "Nombre";
                _option2ViewModel.CognomContent = "DNI";
                _option2ViewModel.EmailContent = "Correo";
                _option2ViewModel.PhoneContent = "Telefono";
                _option2ViewModel.DataContent = "Fecha";
                _option2ViewModel.CancelarContent = "Cancelar";



            }
            else if (SelectedIdioma.Split(" ")[1] == "Angles")
            {
                ButtonContent1 = "Select the Thema";
                ButtonContent2 = "Select the language";
                ButtonContent3 = "Ligth";
                ButtonContent5 = "Dark";
                _option2ViewModel.SaveContent = "Save";

                _option1ViewModel.ButtonContent3 = "Insert";
                _option1ViewModel.ButtonContent4 = "Delete";
                _option1ViewModel.ButtonContent2 = "Graphic";
                _option1ViewModel.ButtonContent = "Edit";
                _option1ViewModel.ButtonContent5 = "Name";
                _option2ViewModel.NameContent = "Nombre";
                _option2ViewModel.CognomContent = "DNI";
                _option2ViewModel.EmailContent = "Email";
                _option2ViewModel.PhoneContent = "Phone";
                _option2ViewModel.DataContent = "Date";
                _option2ViewModel.CancelarContent = "Cancel";




            }
        }
        public void enviarView(Option1ViewModel _option1ViewModel)
        {
            this._option1ViewModel = _option1ViewModel;
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
