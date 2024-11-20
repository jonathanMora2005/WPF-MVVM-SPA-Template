using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using WPF_MVVM_SPA_Template.Models;

namespace WPF_MVVM_SPA_Template.ViewModels
{
    //Els ViewModels deriven de INotifyPropertyChanged per poder fer Binding de propietats
    class EditaroGuardaModel : INotifyPropertyChanged
    {
        // Referència al ViewModel principal
        private readonly MainViewModel _mainViewModel;
        // Col·lecció de Courses (podrien carregar-se d'una base de dades)
        // ObservableCollection és una llista que notifica els canvis a la vista



        private string? _name;
        private string? _DNI;
        private string? _email;
        private String _phone;
        private DateTime _date;

        private Option1ViewModel _option1ViewModel;
        private string nameContent = "Nombre";
        public string NameContent
        {
            get { return nameContent; }
            set
            {
                nameContent = value;
                OnPropertyChanged(nameof(NameContent));
            }
        }   
        private string emailContent = "Correu";
        public string EmailContent
        {
            get { return nameContent; }
            set
            {
                emailContent = value;
                OnPropertyChanged(nameof(EmailContent));
            }
        }
        private string cancelarContent = "Cancelar";
        public string CancelarContent
        {
            get { return cancelarContent; }
            set
            {
                cancelarContent = value;
                OnPropertyChanged(nameof(CancelarContent));
            }
        }
        private string guardaContent = "Guarda";
        public string SaveContent
        {
            get { return guardaContent; }
            set
            {
                guardaContent = value;
                OnPropertyChanged(nameof(SaveContent));
            }
        }
        private string dataContent = "Data";
        public string DataContent
        {
            get { return dataContent; }
            set
            {
                dataContent = value;
                OnPropertyChanged(nameof(DataContent));
            }
        }      
        private string phoneContent = "Telefon";
        public string PhoneContent
        {
            get { return phoneContent; }
            set
            {
                phoneContent = value;
                OnPropertyChanged(nameof(PhoneContent));
            }
        }  
        private string cognomContent = "DNI";
        public string CognomContent
        {
            get { return cognomContent; }
            set
            {
                cognomContent = value;
                OnPropertyChanged(nameof(CognomContent));
            }
        }

        public void afeixiOption1(Option1ViewModel ViewModel) {
            _option1ViewModel = ViewModel;

}

        public string? Name
        {
            get => _name; 
            set
            {
                _name = value; 
                OnPropertyChanged(); 
            }
        }




        public string? DNI
        {
            get => _DNI;
            set
            {
                _DNI = value;
                OnPropertyChanged();


            }
        }

        public string? Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }


        public String Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                OnPropertyChanged();
            }
        }
        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged();
                
            }
        }


        public ICommand CancelCommand { get; }
        public ICommand GuardaCommand { get; }
       

     



        public EditaroGuardaModel(MainViewModel mainViewModel)
        {
                _mainViewModel = mainViewModel;
                Name = string.Empty;
                CancelCommand = new RelayCommand(ExecuteCancel);
                GuardaCommand = new RelayCommand(Guarda);
        }

        // Método que se ejecuta cuando se llama al comando
        private void ExecuteCancel(object? parameter)
        {
            Name = string.Empty;
            DNI = string.Empty;
            Email = string.Empty;
            Phone = "0";
            Date = DateTime.Today;
            _mainViewModel.SelectedView = "Option1";
            _mainViewModel.ChangeView();
        }
      

        private bool _isValidDNI;
        private bool _isEmailValid;
        private bool _isPhoneValid;
        private bool _isValidText;

        public bool IsValidText
        {
            get => _isValidText;
            set
            {
                if (_isValidText != value)
                {
                    _isValidText = value;
                    OnPropertyChanged(nameof(IsValidText));
                }
            }
        }

        public bool IsPhoneValid
        {
            get => _isPhoneValid;
            set
            {
                if (_isPhoneValid != value)
                {
                    _isPhoneValid = value;
                    OnPropertyChanged(nameof(IsPhoneValid));
                }
            }
        }

        public bool IsEmailValid
        {
            get => _isEmailValid;
            set
            {
                if (_isEmailValid != value)
                {
                    _isEmailValid = value;
                    OnPropertyChanged(nameof(IsEmailValid));
                }
            }
        }

        public bool IsValidDNI
        {
            get => _isValidDNI;
            set
            {
                if (_isValidDNI != value)
                {
                    _isValidDNI = value;
                    OnPropertyChanged(nameof(IsValidDNI));
                }
            }
        }

        private void Guarda(object? parameter)
        {
            if (_option1ViewModel.actualitza)
            {
                _option1ViewModel.SelectedStudent.Name = Name;
                _option1ViewModel.SelectedStudent.DNI = DNI;
                _option1ViewModel.SelectedStudent.Email = Email;
                _option1ViewModel.SelectedStudent.Phone = Phone;
                _option1ViewModel.SelectedStudent.Date = Date;
                ExecuteCancel(parameter);
                _mainViewModel.SelectedView = "Option1";
                _mainViewModel.ChangeView();
            }
            else if (IsValidDNI && IsEmailValid && IsPhoneValid && IsValidText)
            {
                MessageBox.Show(Phone + Name);
                _option1ViewModel.AddStudent(Name, DNI, Email, Phone, Date);
                ExecuteCancel(parameter);
                _mainViewModel.SelectedView = "Option1";
                _mainViewModel.ChangeView();
            }
            else
            {
                MessageBox.Show("Dades Incorrectes");

            }
        }

        // Això és essencial per fer funcionar el Binding de propietats entre Vistes i ViewModels
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


    }
}
