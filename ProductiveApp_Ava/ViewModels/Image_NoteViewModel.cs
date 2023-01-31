using ProductiveApp_Ava.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductiveApp_Ava.ViewModels
{
    public class Image_NoteViewModel : NoteViewModel
    {
        string uri;

        public Image_NoteViewModel(Note note) : base(note)
        {

        }
    }
}
