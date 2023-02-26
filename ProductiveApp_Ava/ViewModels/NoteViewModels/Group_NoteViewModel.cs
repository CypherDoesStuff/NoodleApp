using ProductiveApp_Ava.Models;
using ProductiveApp_Ava.Services;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ProductiveApp_Ava.ViewModels
{
    public class Group_NoteViewModel : GroupViewModelBase, NoteViewContainer
    {
        public string name
        {
            get { return ((Group_Note)_note).name; }
            set { ((Group_Note)_note).name = value; this.RaisePropertyChanged(nameof(name)); }
        }

        internal Group_Note group_Note;

        public Group_NoteViewModel(Note note) : base(note)
        {
            group_Note = (Group_Note)_note;
            foreach (Note subNote in group_Note.subNotes)
            {
                NoteViewModelBase noteView = NoteConverter.ConvertNoteToView(subNote);

                if (noteView is not null && noteView is not Group_NoteViewModel)
                {
                    noteView.container = this;
                    notes.Add(noteView);
                }
            }
        }

        public override void AddNote(Note note)
        {
            Debug.WriteLine("Group adding note!");
            group_Note.subNotes.Add(note);

            MainWindowViewModel.RemoveNoteFromCollection(note);
            MainWindowViewModel.ClearDragModel();

            NoteViewModelBase noteView = NoteConverter.ConvertNoteToView(note);
            noteView.container = this;

            if (noteView is not null && noteView is not Group_NoteViewModel)
            {
                Debug.WriteLine("Adding group success!");
                notes.Add(noteView);
            }
        }

        public override void InsertNote(Note note, int index)
        {
            Debug.WriteLine("Group inserting note!");
            group_Note.subNotes.Insert(index, note);

            MainWindowViewModel.RemoveNoteFromCollection(note);
            MainWindowViewModel.ClearDragModel();

            NoteViewModelBase noteView = NoteConverter.ConvertNoteToView(note);
            noteView.container = this;

            if (noteView is not null && noteView is not Group_NoteViewModel)
            {
                Debug.WriteLine("Adding group success!");
                notes.Insert(index, noteView);
            }
        }

        public void OnNoteViewRemoved(NoteViewModelBase noteModel)
        {
            Debug.WriteLine("REMOVING FROM GROUP");
            notes.Remove(noteModel);
            group_Note.subNotes.Remove(noteModel._note);
        }
    }
}
