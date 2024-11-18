using System.ComponentModel;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using WPF_MVVM_SPA_Template.Views;

namespace WPF_MVVM_SPA_Template.ViewModels
{
    //Els ViewModels deriven de INotifyPropertyChanged per poder fer Binding de propietats
    class MainViewModel : INotifyPropertyChanged
    {

        // ViewModels de les diferents opcions
        public Option1ViewModel Option1VM { get; set; }
        public EditaroGuardaModel Option2VM { get; set; }
        public GraficModel Option3VM { get; set; }
        public GeminisModel Option4VM { get; set; }
        public ConfiguracioModel Option5VM { get; set; }


        // Propietat que conté la vista actual (és un objecte)
        private object? _currentView;
        public object? CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        // Propietat per controlar la vista seleccionada al ListBox (És un string)
        private string? _selectedView;
        public string? SelectedView
        {
            get { return _selectedView; }
            set
            {
                _selectedView = value;
                OnPropertyChanged();
                ChangeView();
            }
        }

        public MainViewModel()
        {
            // Inicialitzem els diferents ViewModels
            Option2VM = new EditaroGuardaModel(this);

            Option1VM = new Option1ViewModel(this, Option2VM);
            Option2VM.afeixiOption1(Option1VM);
            Option3VM = new GraficModel();
            Option4VM = new GeminisModel();
            Option4VM = new GeminisModel();
            Option5VM = new ConfiguracioModel(this);
            Option3VM.afeixiOption1(Option1VM);
            SelectedView = "Option1";
            Option4VM.afeigirView(Option1VM);
            Option5VM.enviarView(Option1VM);
            Option5VM.enviarView2(Option2VM);
            ChangeView();
        }

        // Canvi de Vista
        public Window1 opcio3()
        {
            // Check if a selected student exists in Option1VM
            if (Option1VM.SelectedStudent != null)
            {
                // Create a new Window1 instance with the student's LR values and set the DataContext to Option3VM
                return new Window1(Option1VM.SelectedStudent.LR) { DataContext = Option3VM };
            }

            // If no selected student, create a new Window1 instance with default values and set the DataContext to Option3VM
            return new Window1() { DataContext = Option3VM };
        }
        

        // Change the current view based on SelectedView property
        public void ChangeView()
        {
            switch (SelectedView)
            {
                case "Option1":
                    Option2VM.Name = "";
                    Option2VM.Surname = "";
                    Option2VM.Email = "";
                    Option2VM.Phone = 0;
                    Option2VM.Date= DateTime.Today;
                    Option1VM.actualitza = false;
                    CurrentView = new Option1View { DataContext = Option1VM };
                    break;
                case "Option2":
                    CurrentView = new Option2View { DataContext = Option2VM };
                    break; 
                case "Option4":
                    CurrentView = new Window2 { DataContext = Option4VM };
                    break;
                case "Option3":
                    CurrentView = opcio3();
                    break;
                case "Option5":
                    CurrentView = new Window3 { DataContext = Option5VM };
                    break;
                   
                default:
                    // Optionally handle an invalid view case
                    break;
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
