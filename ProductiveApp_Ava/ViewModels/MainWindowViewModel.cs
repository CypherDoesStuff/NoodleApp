using DynamicData;
using ProductiveApp_Ava.Models;
using ProductiveApp_Ava.Services;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

namespace ProductiveApp_Ava.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Greeting => "Send me Noods!";
        public static ObservableCollection<NoteViewModel> notes { get; private set; }
        public CanvasViewModel canvas;

        static Database db;

        public MainWindowViewModel()
        {
            notes = new ObservableCollection<NoteViewModel>();

            canvas = new CanvasViewModel();
            db = new Database();

            foreach (Note note in db.GetNotes())
                AddNoteToCollection(note);
        }

        public void OnWindowClose()
        {
            Debug.WriteLine("Closed");
            db.SaveAll();
        }

        public static void AddNoteToCollection(Note note)
        {
            switch (note)
            {
                case Image_Note:
                    notes.Add(new Image_NoteViewModel(note));
                    break;
                case Text_Note:
                    notes.Add(new Text_NoteViewModel(note));
                    break;
                case null:
                    break;
            }
        }

        public static void AddNoteToDatabase(Note note)
        {
            db.AddNote(note);
            AddNoteToCollection(note);
        }

        public void EditNote()
        {
            foreach (Note note in db.GetNotes())
                Debug.WriteLine(note.ToString());
        }
    }
}
