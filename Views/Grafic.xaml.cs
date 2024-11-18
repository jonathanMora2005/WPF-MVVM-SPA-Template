using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;
using WPF_MVVM_SPA_Template.ViewModels;

namespace WPF_MVVM_SPA_Template.Views
{
    public partial class Window1 : UserControl

    {
        private int[] l = null;
        public Window1(int[] l)

        {
            this.l = l;
            InitializeComponent();

            SetupChart();

        }
        public Window1()

        {
            InitializeComponent();

            SetupChart();

        }

        private void SetupChart()
        {
            Gràfic.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<int>(l)
                }
            };
        }
    }
}
