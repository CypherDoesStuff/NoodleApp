using ProductiveApp_Ava.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductiveApp_Ava.ViewModels
{
    public class TodoTask_ViewModel : NoteViewModelBase
    {
        public TodoList_ViewModel? todoParent;

        public string name
        {
            get { return ((TodoItem_Note)_note).name; }
            private set { ((TodoItem_Note)_note).name = value; this.RaisePropertyChanged(nameof(name)); }
        }

        public bool done
        { 
            get { return ((TodoItem_Note)_note).isDone; } 
            private set { ((TodoItem_Note)_note).isDone = value; this.RaisePropertyChanged(nameof(done)); } 
        }

        public TodoTask_ViewModel(Note note) : base(note)
        {

        }

        public void AddNewTask()
        {
            todoParent?.AddNote(new TodoItem_Note());
        }

        public void DeleteTask()
        {
            todoParent?.RemoveNote(this);
        }
    }
}
