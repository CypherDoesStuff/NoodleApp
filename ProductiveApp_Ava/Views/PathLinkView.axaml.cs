using Avalonia.Controls;
using ProductiveApp_Ava.ViewModels;

namespace ProductiveApp_Ava.Views
{
    public partial class PathLinkView : UserControl
    {
        public PathLinkView()
        {
            InitializeComponent();
            DoubleTapped += PathLinkView_DoubleTapped;
        }

        private void PathLinkView_DoubleTapped(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            ((PathLinkViewModel)DataContext).LoadBoardFromPath();

            e.Handled= true;
        }
    }
}
