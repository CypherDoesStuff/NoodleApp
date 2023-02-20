using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductiveApp_Ava.ViewModels
{
    public interface NoteViewContainer
    {
        public abstract void OnNoteViewRemoved(NoteViewModel noteModel);
    }
}
