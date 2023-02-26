using ProductiveApp_Ava.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ProductiveApp_Ava.ViewModels
{
    public class Board_NoteViewModel : NoteViewModelBase
    {
        public string name 
        {
            get { return ((Board_Note)_note).name; }
            private set { ((Board_Note)_note).name = value; this.RaisePropertyChanged(nameof(name)); UpdateBoardName(value); }
        }

        ulong index;

        public Board_NoteViewModel(Note note) : base(note)
        {
            Board_Note boardNote = (Board_Note)_note;
            index = boardNote.index;

            if (index == 0)
            {
                name = "New Board";
                boardNote.index = MainWindowViewModel.AddBoardToDatabase(name);
                index = boardNote.index;
            }
        }

        public void LoadBoard()
        {
            MainWindowViewModel.LoadBoardFromDatabase(index);
        }

        public void UpdateBoardName(string name)
        {
            MainWindowViewModel.RenameBoardInDatabase(index, name);
        }

        public override void OnDelete()
        {
            if (MainWindowViewModel.DeleteBoardFromDatabase(index))
                Debug.WriteLine($"Deleted board: {name}");

            base.OnDelete();
        }
    }
}
