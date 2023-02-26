using Avalonia.Controls;
using Avalonia.Input;
using ProductiveApp_Ava.ViewModels;

namespace ProductiveApp_Ava.Views
{
    public partial class TodoTask_View : SelectableNoteBase
    {
        public TodoTask_View()
        {
            InitializeComponent();
            taskText.KeyDown += TaskText_KeyDown;
            taskText.KeyUp += TaskText_KeyUp;
            selectedChanged += TodoTask_View_selectedChanged;
            taskText.IsHitTestVisible = false;
            Selected = false;
        }

        private void TodoTask_View_selectedChanged(object? sender, bool e)
        {
            taskText.IsHitTestVisible = e;
            taskText.Focusable = e;

            if (e)
            {
                taskText.Focus();
                taskText.CaretIndex = taskText.Text.Length;
            }
            else
                FocusManager.Instance.Focus(null);
        }

        private void TaskText_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ((TodoTask_ViewModel)DataContext)?.AddNewTask();
                FocusManager.Instance.Focus(null);
            }
        }

        private void TaskText_KeyUp(object? sender, KeyEventArgs e)
        {
            if(e.Key == Key.Back && taskText.Text.Length <= 0)
            {
                ((TodoTask_ViewModel)DataContext)?.DeleteTask();
            }
        }
    }
}
