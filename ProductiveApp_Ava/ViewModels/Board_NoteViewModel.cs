using ProductiveApp_Ava.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ProductiveApp_Ava.ViewModels
{
    public class Board_NoteViewModel : NoteViewModel
    {
        public string name 
        {
            get { return ((Board_Note)_note).name; }
            private set { ((Board_Note)_note).name = value; this.RaisePropertyChanged(nameof(name)); }
        }

        public Board_NoteViewModel(Note note) : base(note)
        {

        }

        public bool UpdateBoardName(string name)
        {
            //Do board check if possible to change name

            return false;
        }
    }
}
