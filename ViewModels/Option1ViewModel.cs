using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using WPF_MVVM_SPA_Template.Models;
using WPF_MVVM_SPA_Template.Views;
using static System.Runtime.InteropServices.JavaScript.JSType;
using FastReport;
using FastReport.Data;
using FastReport.Export.PdfSimple;
using Microsoft.CodeAnalysis.Text;


namespace WPF_MVVM_SPA_Template.ViewModels
{
    class Option1ViewModel : INotifyPropertyChanged
    {
        private readonly MainViewModel _mainViewModel;
        public  bool actualitza = false;
        public ObservableCollection<Client> Clients { get; set; } = new ObservableCollection<Client>();
        private readonly EditaroGuardaModel _option2ViewModel;

        private Client? _selectedStudent;
        public Client? SelectedStudent
        {
            get { return _selectedStudent; }
            set { _selectedStudent = value; OnPropertyChanged(); }
        }

        public RelayCommand AddStudentCommand { get; set; }
        public RelayCommand DelStudentCommand { get; set; }
        public RelayCommand EditStudentCommand { get; set; }

        public RelayCommand GraStudentCommand { get; set; }
        public RelayCommand IaStudentCommand { get; set; }
        public RelayCommand ExportarCommand { get; set; }


        public RelayCommand InsStudentCommand { get; set; }
        private string _buttonContent;
        private string _buttonContent2;
        private string _buttonContent3;
        private string _buttonContent4;
        private string _buttonContent5;
        public string ButtonContent5
        {
            get { return _buttonContent5; }
            set
            {
                _buttonContent5 = value;
                OnPropertyChanged(nameof(ButtonContent5));
            }
        }

        public string ButtonContent4
        {
            get { return _buttonContent4; }
            set
            {
                _buttonContent4 = value;
                OnPropertyChanged(nameof(ButtonContent4));
            }
        }
        public string ButtonContent3
        {
            get { return _buttonContent3; }
            set
            {
                _buttonContent3 = value;
                OnPropertyChanged(nameof(ButtonContent3));
            }
        }
        public string ButtonContent2
        {
            get { return _buttonContent2; }
            set
            {
                _buttonContent2 = value;
                OnPropertyChanged(nameof(ButtonContent2));
            }
        }

        public string ButtonContent
        {
            get { return _buttonContent; }
            set
            {
                _buttonContent = value;
                OnPropertyChanged(nameof(ButtonContent));
            }
        }

  

        public Option1ViewModel(MainViewModel mainViewModel, EditaroGuardaModel option2ViewModel)
        {
            _option2ViewModel = option2ViewModel;
            _mainViewModel = mainViewModel;
            ButtonContent = "Editar";
            ButtonContent2 = "Grafic";
            ButtonContent3 = "Insertar";
            ButtonContent4 = "Eliminar";
            ButtonContent = "Editar";



            // Sample data
            Clients.Add(new Client
            {
                Name = "John",
                Surname = "Doe",
                Email = "john.doe@example.com",
                Phone = 123456789,
                Date = DateTime.Now,
                LR = novaLlista()

            });
            Clients.Add(new Client
            {
                Name = "Anna",
                Surname = "Smith",
                Email = "anna.smith@example.com",
                Phone = 987654321,
                Date = new DateTime(2024, 1, 15),
                LR = novaLlista()

            });

            // Initialize commands
            AddStudentCommand = new RelayCommand(x => AddStudent());
            DelStudentCommand = new RelayCommand(x => DelStudent());
            EditStudentCommand = new RelayCommand(x => EditStudent());
            GraStudentCommand = new RelayCommand(x => GraStudent());
            IaStudentCommand = new RelayCommand(x => IaStudent());
            InsStudentCommand = new RelayCommand(x => InsStudent());
            ExportarCommand = new RelayCommand(x => report());

        }
        public void GraStudent()
        {
            if (SelectedStudent != null)
            {
                _mainViewModel.SelectedView = "Option3";
                _mainViewModel.ChangeView();
            }
            else {
                MessageBox.Show("Selecciona un estidiante");
            }
        }    
        public void IaStudent()
        {
            if (SelectedStudent != null)
            {
                _mainViewModel.SelectedView = "Option4";
                _mainViewModel.ChangeView();
            }
            else
            {
                MessageBox.Show("Selecciona un estidiante");
            }
        }     
        public void InsStudent()
        {
            _mainViewModel.SelectedView = "Option2";
            _mainViewModel.ChangeView();
            

        }

public void report()
{
    // Crear instancia del reporte
    Report report = new Report();

    // Construir la ruta completa del archivo Esquema.frx
    string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Models/Esquema.frx");

    // Verificar si el archivo existe
    if (!File.Exists(filePath))
    {
        throw new FileNotFoundException($"No se encontró el archivo: {filePath}");
    }

    // Cargar el archivo FRX
    report.Load(filePath);

    // Registrar los datos
    report.RegisterData(Clients, "Clients");

    // Habilitar el DataSource
    DataSourceBase dataSource = report.GetDataSource("JSON");
    if (dataSource == null)
    {
        throw new NullReferenceException("No se pudo encontrar el DataSource 'Clients'.");
    }
    dataSource.Enabled = true;

    // Vincular el DataSource al DataBand
    DataBand? dataBand = report.FindObject("Data1") as DataBand;
    if (dataBand != null)
    {
        dataBand.DataSource = dataSource;
    }

    // Preparar el reporte
    report.Prepare();

    // Exportar el reporte a un PDF
    using (MemoryStream ms = new MemoryStream())
    {
        PDFSimpleExport pdfExport = new PDFSimpleExport();
        report.Export(pdfExport, ms);

        // Guardar el PDF en el directorio de salida
        string pdfPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Esquema.pdf");
        File.WriteAllBytes(pdfPath, ms.ToArray());

        // Abrir el PDF automáticamente
        try
        {
            // Verificar si la ruta al archivo es válida
            if (File.Exists(pdfPath))
            {
                // Abrir el archivo con la aplicación predeterminada
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(pdfPath)
                {
                    UseShellExecute = true
                });
            }
            else
            {
                throw new FileNotFoundException("No se pudo encontrar el archivo PDF generado.");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al intentar abrir el PDF: {ex.Message}");
        }
    }
}


        public void EditStudent()
        {
            if (SelectedStudent != null)
            {
                _option2ViewModel.Name = SelectedStudent.Name;
                _option2ViewModel.Surname = SelectedStudent.Surname;
                _option2ViewModel.Email = SelectedStudent.Email;
                _option2ViewModel.Phone = SelectedStudent.Phone;
                _option2ViewModel.Date = SelectedStudent.Date;
                _mainViewModel.SelectedView = "Option2";
                actualitza = true;

            }
            else
            {
                MessageBox.Show("Selecciona un estidiante");
            }

           
                _mainViewModel.ChangeView();


        }

        public bool IsValidEmail(string email)
        {
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }
        public int[] novaLlista()
        {
            Random random = new Random();
            List<int> listaNumeros = new List<int>();

            for (int i = 0; i < 12; i++)
            {
                // Genera un número aleatorio entre 1 y 100
                int numero = random.Next(1, 101);
                listaNumeros.Add(numero);
            }
            return listaNumeros.ToArray();

        }

        public void AddStudent(string name = "NEW", string surname = "NEW", string email = "New@example.com", int phone = 0, DateTime date = default)
        {
           
                Clients.Add(new Client
                {
                    Name = name,
                    Surname = surname,
                    Email = email,
                    Phone = phone,
                    Date = date,
                    LR = novaLlista()
                });
            
           
            
        }

        private void DelStudent()
        {
            if (SelectedStudent != null)
            {
                Clients.Remove(SelectedStudent);
            }
            else
            {
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public static implicit operator Option1ViewModel(Option1View v)
        {
            throw new NotImplementedException();
        }
    }
}
