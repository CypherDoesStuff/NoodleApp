using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using DynamicData;
using JetBrains.Annotations;
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
        public string Greeting => "";

        public static ObservableCollection<PathLinkViewModel> paths { get; private set; }

        public static ObservableCollection<NoteViewModelBase> notes { get; private set; }
        static Dictionary<Note, NoteViewModelBase> noteDict;

        public static ObservableCollection<NoteViewModelBase> dragCollection { get; private set; }
        public static ObservableCollection<NoteToolViewModel> noteTools { get; private set; }

        static MainWindowViewModel instance;
        static Database db;

        public MainWindowViewModel()
        {
            instance = this;

            paths = new ObservableCollection<PathLinkViewModel>();

            notes = new ObservableCollection<NoteViewModelBase>();
            noteDict = new Dictionary<Note, NoteViewModelBase>();

            dragCollection = new ObservableCollection<NoteViewModelBase>();
            noteTools = new ObservableCollection<NoteToolViewModel>();

            db = new Database();

            foreach (Note note in db.GetNotes())
                AddNoteToCollection(note);

            var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
            noteTools.Add(new NoteToolViewModel(new Bitmap(assets.Open(new Uri("avares://ProductiveApp_Ava/Assets/sticky_note.png"))), typeof(Text_Note)));
            noteTools.Add(new NoteToolViewModel(new Bitmap(assets.Open(new Uri("avares://ProductiveApp_Ava/Assets/list.png"))), typeof(Group_Note)));
            noteTools.Add(new NoteToolViewModel(new Bitmap(assets.Open(new Uri("avares://ProductiveApp_Ava/Assets/todo.png"))), typeof(Todo_Note)));
            noteTools.Add(new NoteToolViewModel(new Bitmap(assets.Open(new Uri("avares://ProductiveApp_Ava/Assets/board.png"))), typeof(Board_Note)));

            UpdateBoardPath();
        }

        public void OnWindowClose()
        {
            Debug.WriteLine("Closed");
            db.SaveAll();
        }

        public static NoteViewModelBase AddNoteToCollection(Note note)
        {
            NoteViewModelBase noteView = NoteConverter.ConvertNoteToView(note);

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

        public static void AddViewToCollection(NoteViewModelBase model)
        {
            notes.Add(model);
        }

        public static NoteViewModelBase AddNoteToDatabase(Note note)
        {
            db.AddNote(note);
            return AddNoteToCollection(note);
        }

        public static ulong AddBoardToDatabase(string name)
        {
            return db.AddBoard(name);
        }

        public static void LoadBoardFromDatabase(ulong index)
        {
            if (db.GetSelectedBoard() == index)
                return;

            db.SaveBoard(db.GetSelectedBoard());
            db.SelectBoard(index);

            notes.Clear();
            noteDict.Clear();

            foreach (Note note in db.GetNotes())
                AddNoteToCollection(note);

            instance.UpdateBoardPath();
        }

        public static void RenameBoardInDatabase(ulong index, string name)
        {
            db.RenameBoard(index, name);
        }

        public static bool DeleteBoardFromDatabase(ulong index)
        {
            return db.DeleteBoard(index);
        }

        public static void SetDragModel(NoteViewModelBase model)
        {
            notes.Remove(model);
            dragCollection.Clear();
            dragCollection.Add(model);
        }

        public static void ClearDragModel()
        {
            dragCollection.Clear();
        }

        private void UpdateBoardPath()
        {
            paths.Clear();

            ulong[] path = db.GetSelectedPath();
            string pathString = string.Empty;
            foreach (ulong pathId in path)
            {
                paths.Add(new PathLinkViewModel(pathId, db.GetBoardName(pathId)));
            }

            ulong currentId = db.GetSelectedBoard();
            paths.Add(new PathLinkViewModel(currentId, db.GetBoardName(currentId)));
        }
    }
}
