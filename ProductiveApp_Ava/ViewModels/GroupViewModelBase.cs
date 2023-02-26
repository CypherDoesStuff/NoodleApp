using ProductiveApp_Ava.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductiveApp_Ava.ViewModels
{
    public class GroupViewModelBase : NoteViewModelBase
    {
        public ObservableCollection<NoteViewModelBase> notes { get; internal set; }

        public GroupViewModelBase(Note note) : base(note)
        {
            notes = new ObservableCollection<NoteViewModelBase>();
        }

        public virtual void AddNote(Note note)
        {
            throw new NotImplementedException();
        }

        public virtual void InsertNote(Note note, int index)
        {
            throw new NotImplementedException();
        }
    }
}
