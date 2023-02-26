using Avalonia.Controls;
using ProductiveApp_Ava.Models;

namespace ProductiveApp_Ava.Views
{
    public partial class TodoList_View : GroupViewBase
    {
        public TodoList_View()
        {
            InitializeComponent();

            groupItemsControl = todoPanel;
        }

        public override bool CanDrop(Note note)
        {
            return note is TodoItem_Note;
        }
    }
}
