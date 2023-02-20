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
    public class Group_NoteViewModel : NoteViewModel, NoteViewContainer
    {
        public string name
        {
            get { return ((Group_Note)_note).name; }
            set { ((Group_Note)_note).name = value; this.RaisePropertyChanged(nameof(name)); }
        }

        public ObservableCollection<NoteViewModel> notes { get; private set; }
        internal Group_Note group_Note;

        public Group_NoteViewModel(Note note) : base(note)
        {
            notes = new ObservableCollection<NoteViewModel>();

            group_Note = (Group_Note)_note;
            foreach (Note subNote in group_Note.subNotes)
            {
                NoteViewModel noteView = NoteConverter.ConvertNoteToView(subNote);

                if (noteView is not null && noteView is not Group_NoteViewModel)
                {
                    noteView.container = this;
                    notes.Add(noteView);
                }
            }
        }

        public void AddNote(Note note)
        {
            Debug.WriteLine("Group adding note!");
            group_Note.subNotes.Add(note);

            MainWindowViewModel.RemoveNoteFromCollection(note);
            MainWindowViewModel.ClearDragModel();

            NoteViewModel noteView = NoteConverter.ConvertNoteToView(note);
            noteView.container = this;

            if (noteView is not null && noteView is not Group_NoteViewModel)
            {
                Debug.WriteLine("Adding group success!");
                notes.Add(noteView);
            }
        }

        public void OnNoteViewRemoved(NoteViewModel noteModel)
        {
            Debug.WriteLine("REMOVING FROM GROUP");
            notes.Remove(noteModel);
            group_Note.subNotes.Remove(noteModel._note);
        }
    }
}
