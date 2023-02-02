using ProductiveApp_Ava.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductiveApp_Ava.ViewModels
{
    public class Text_NoteViewModel : NoteViewModel
    {
        public string text
        {
            get { return ((Text_Note)_note).text; }
            set { ((Text_Note)_note).text = value; this.RaisePropertyChanged(nameof(text)); }
        }

        public Text_NoteViewModel(Note note) : base(note)
        {

        }
    }
}
