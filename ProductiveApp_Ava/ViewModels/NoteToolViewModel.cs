using Avalonia;
using Avalonia.Input;
using Avalonia.Media.Imaging;
using ProductiveApp_Ava.Models;
using ProductiveApp_Ava.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductiveApp_Ava.ViewModels
{
    public class NoteToolViewModel : ViewModelBase
    {
        public Bitmap icon { get; private set; }
        public Note noteType;

        public NoteToolViewModel(Bitmap toolIcon, Note toolNoteType)
        {
            icon = toolIcon;
            noteType = toolNoteType;    
        }

        public Note GetNote()
        {
            return noteType;
        }
    }
}
