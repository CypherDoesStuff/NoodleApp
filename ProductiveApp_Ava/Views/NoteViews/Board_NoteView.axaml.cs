using Avalonia.Controls;
using Avalonia.Input;
using ProductiveApp_Ava.ViewModels;

namespace ProductiveApp_Ava.Views
{
    public partial class Board_NoteView : NoteViewBase
    {
        public Board_NoteView()
        {
            InitializeComponent();

            boardLabel.Focusable = false;
            boardLabel.Cursor = new Cursor(StandardCursorType.Arrow);
            boardLabel.DoubleTapped += BoardLabel_DoubleTapped;
            boardLabel.LostFocus += BoardLabel_LostFocus;

            DoubleTapped += Board_NoteView_DoubleTapped;
        }

        private void Board_NoteView_DoubleTapped(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Board_NoteViewModel viewModel = (Board_NoteViewModel)DataContext;
            viewModel?.LoadBoard();
        }

        private void BoardLabel_DoubleTapped(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            boardLabel.Focusable = true;
            boardLabel.Focus();
            boardLabel.SelectAll();
            boardLabel.Cursor = new Cursor(StandardCursorType.Ibeam);

            e.Handled = true;
        }

        private void BoardLabel_LostFocus(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            boardLabel.Focusable = false;
            boardLabel.Cursor = new Cursor(StandardCursorType.Arrow);
        }
    }
}
