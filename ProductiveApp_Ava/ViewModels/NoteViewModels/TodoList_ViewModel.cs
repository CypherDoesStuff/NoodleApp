using ProductiveApp_Ava.Models;
using ProductiveApp_Ava.Services;
using ProductiveApp_Ava.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductiveApp_Ava.ViewModels
{
    public class TodoList_ViewModel : GroupViewModelBase, NoteViewContainer
    {
        public string name
        {
            get { return ((Todo_Note)_note).name; }
            set { ((Todo_Note)_note).name = value; this.RaisePropertyChanged(nameof(name)); }
        }

        internal Todo_Note todo_Note;

        public TodoList_ViewModel(Note note) : base(note)
        {
            todo_Note = (Todo_Note)note;
            foreach (Note todo in todo_Note.todoItems)
            {
                NoteViewModelBase noteView = NoteConverter.ConvertNoteToView(todo);
                if (noteView is not null)
                {
                    noteView.container = this;
                    notes.Add(noteView);

                    if (noteView is TodoTask_ViewModel todoModel)
                        todoModel.todoParent = this;
                }
            }

            if(todo_Note.todoItems.Count == 0)
            {
                AddNote(new TodoItem_Note());
            }
        }

        public override void AddNote(Note note)
        {
            Debug.WriteLine("Todo adding note!");
            todo_Note.todoItems.Add(note);

            MainWindowViewModel.RemoveNoteFromCollection(note);
            MainWindowViewModel.ClearDragModel();

            NoteViewModelBase noteView = NoteConverter.ConvertNoteToView(note);
            noteView.container = this;

            if (noteView is TodoTask_ViewModel todoModel)
                todoModel.todoParent = this;

            if (noteView is not null)
            {
                Debug.WriteLine("Adding group success!");
                notes.Add(noteView);
            }
        }

        public override void InsertNote(Note note, int index)
        {
            Debug.WriteLine("Todo inserting note!");
            todo_Note.todoItems.Insert(index, note);

            MainWindowViewModel.RemoveNoteFromCollection(note);
            MainWindowViewModel.ClearDragModel();

            NoteViewModelBase noteView = NoteConverter.ConvertNoteToView(note);
            noteView.container = this;

            if (noteView is TodoTask_ViewModel todoModel)
                todoModel.todoParent = this;

            if (noteView is not null)
            {
                Debug.WriteLine("Adding group success!");
                notes.Insert(index, noteView);
            }
        }

        public void RemoveNote(NoteViewModelBase noteModel)
        {
            if (todo_Note.todoItems.Count > 1 && todo_Note.todoItems.Contains(noteModel._note))
            {
                OnNoteViewRemoved(noteModel);
            }
        }

        public void OnNoteViewRemoved(NoteViewModelBase noteModel)
        {
            if (noteModel is TodoTask_ViewModel todoModel)
                todoModel.todoParent = null;

            notes.Remove(noteModel);
            todo_Note.todoItems.Remove(noteModel._note);
        }
    }
}
