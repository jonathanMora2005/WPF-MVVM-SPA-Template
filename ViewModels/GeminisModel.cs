using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPF_MVVM_SPA_Template.ViewModels
{
    public class GeminisModel : INotifyPropertyChanged
    {
        private string _userInput;
        private string _apiResponse;
        private Option1ViewModel _option1ViewModel;
        public static string convertiLLista(int[] l)
        {
            string t = "[";

           
            foreach (int element in l)
            {
                t += element.ToString() + ",";  
            }
            t += "]";
            return t;
            
        }

        public string UserInput
        {
            get { return _userInput; }
            set
            {
                _userInput = value;
                OnPropertyChanged(nameof(UserInput));
            }
        }

        public string ApiResponse
        {
            get { return _apiResponse; }
            set
            {
                _apiResponse = value;
                OnPropertyChanged(nameof(ApiResponse));
            }
        }
        internal void afeigirView(Option1ViewModel _option1ViewModel)
        {
            this._option1ViewModel = _option1ViewModel;
        }
        public GeminisModel()
        {
            SendDataToApiCommand = new RelayCommand(async (param) => await SendDataToApi());
        }

        public ICommand SendDataToApiCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async Task SendDataToApi()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string apiUrl = "https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash-latest:generateContent?key=AIzaSyBmnOiK4_FeffFKPCwmdUxvk7ePEiXMUzM";

                    var jsonContent = JsonConvert.SerializeObject(new
                    {
                        contents = new[]
                        {
                            new
                            {
                                parts = new[]
                                {
                                    new { text = "si pots contesta fent servi las  dades de aquet usuari nom = "  + _option1ViewModel.SelectedStudent.Name + ",DNI = " + _option1ViewModel.SelectedStudent.DNI + ", Email = " + _option1ViewModel.SelectedStudent.Email + ",Phone = " + _option1ViewModel.SelectedStudent.Phone + ",Neixament = " + _option1ViewModel.SelectedStudent.Date + "Dines del ultims 12 mesos = " + convertiLLista( _option1ViewModel.SelectedStudent.LR ) + ",en cas de que la pragunta no sigui sobra el usuar  contesta que la pregunta no es adacuada  Pregunta =  "
                                     + UserInput }  
                                }
                            }
                        }
                    });

                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();

                        var deserializedResponse = JsonConvert.DeserializeObject<Root>(jsonResponse);

                        ApiResponse = deserializedResponse.Candidates?[0].Content.Parts?[0].Text ?? "No se encontró el campo 'text'";
                    }
                    else
                    {
                        ApiResponse = "Error: " + response.ReasonPhrase;
                    }
                }
            }
            catch (Exception ex)
            {
                ApiResponse = "Exception: " + ex.Message;
            }
        }
    }
}

public class Part
{
    public string Text { get; set; }
}

public class Content
{
    public List<Part> Parts { get; set; }
}

public class Candidate
{
    public Content Content { get; set; }
    public string Role { get; set; }
}

public class Root
{
    public List<Candidate> Candidates { get; set; }
}