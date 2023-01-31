using Avalonia.Controls;
using ProductiveApp_Ava.ViewModels;
using System.ComponentModel;

namespace ProductiveApp_Ava.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            MainWindowViewModel model = (MainWindowViewModel)DataContext;
            model?.OnWindowClose();

            base.OnClosing(e);
        }
    }
}
