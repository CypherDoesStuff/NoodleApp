using Avalonia.Controls;
using Avalonia.Input;
using ProductiveApp_Ava.ViewModels;

namespace ProductiveApp_Ava.Views
{
    public partial class Link_NoteView : NoteViewBase
    {
        public Link_NoteView()
        {
            InitializeComponent();

            titleText.Cursor = new Cursor(StandardCursorType.Hand);
            titleText.PointerReleased += TitleText_PointerReleased;
        }

        private void TitleText_PointerReleased(object? sender, Avalonia.Input.PointerReleasedEventArgs e)
        {
            if (e.InitialPressMouseButton == Avalonia.Input.MouseButton.Left)
            {
                ((Link_NoteViewModel)DataContext)?.LaunchLink();

                e.Handled = true;
            }
        }
    }
}
