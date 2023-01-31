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

        public MainWindowViewModel()
        {
            notes = new ObservableCollection<NoteViewModel>();

            canvas = new CanvasViewModel();
            Database db = new Database();

            foreach (Note note in db.GetNotes())
                AddNoteToCollection(note);
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

        public void EditNote()
        {
            Debug.WriteLine("Editing");
            notes[1].x = 0;
        }
    }
}
