using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using DynamicData;
using ProductiveApp_Ava.Models;
using ProductiveApp_Ava.Services;
using ProductiveApp_Ava.Views;
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
        public string Greeting => "Indev Build";

        public static ObservableCollection<NoteViewModel> notes { get; private set; }
        static Dictionary<Note, NoteViewModel> noteDict;

        public static ObservableCollection<NoteViewModel> dragCollection { get; private set; }
        public static ObservableCollection<NoteToolViewModel> noteTools { get; private set; }

        static Database db;

        public MainWindowViewModel()
        {
            notes = new ObservableCollection<NoteViewModel>();
            noteDict = new Dictionary<Note, NoteViewModel>();

            dragCollection = new ObservableCollection<NoteViewModel>();
            noteTools = new ObservableCollection<NoteToolViewModel>();

            db = new Database();

            foreach (Note note in db.GetNotes())
                AddNoteToCollection(note);

            var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
            noteTools.Add(new NoteToolViewModel(new Bitmap(assets.Open(new Uri("avares://ProductiveApp_Ava/Assets/sticky_note.png"))), new Text_Note()));
            noteTools.Add(new NoteToolViewModel(new Bitmap(assets.Open(new Uri("avares://ProductiveApp_Ava/Assets/list.png"))), new Group_Note()));
            //noteTools.Add(new NoteToolViewModel(new Bitmap(assets.Open(new Uri("avares://ProductiveApp_Ava/Assets/board.png"))), new Board_Note()));
        }

        public void OnWindowClose()
        {
            Debug.WriteLine("Closed");
            db.SaveAll();
        }

        public static NoteViewModel AddNoteToCollection(Note note)
        {
            NoteViewModel noteView = NoteConverter.ConvertNoteToView(note);

            if (noteView is not null)
            {
                notes.Add(noteView);
                noteDict.Add(note, noteView);
            }

            return noteView;
        }

        public static void RemoveNoteFromCollection(Note note)
        {
            if(db.RemoveNote(note))
            {
                if (noteDict.ContainsKey(note))
                {
                    notes.Remove(noteDict[note]);
                    noteDict.Remove(note);
                }
            }
            else
            {
                Debug.WriteLine("Could not find note in db");
            }
        }

        public static void AddViewToCollection(NoteViewModel model)
        {
            notes.Add(model);
        }

        public static NoteViewModel AddNoteToDatabase(Note note)
        {
            db.AddNote(note);
            return AddNoteToCollection(note);
        }

        public static void SetDragModel(NoteViewModel model)
        {
            notes.Remove(model);
            dragCollection.Clear();
            dragCollection.Add(model);
        }

        public static void ClearDragModel()
        {
            dragCollection.Clear();
        }
    }
}
