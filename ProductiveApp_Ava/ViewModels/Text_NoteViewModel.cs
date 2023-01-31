using ProductiveApp_Ava.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductiveApp_Ava.ViewModels
{
    public class Text_NoteViewModel : NoteViewModel
    {
        public string text { get; set; }

        public Text_NoteViewModel(Note note) : base(note)
        {
            Text_Note textNote = (Text_Note)_note;
            text = textNote.text;
        }
    }
}
