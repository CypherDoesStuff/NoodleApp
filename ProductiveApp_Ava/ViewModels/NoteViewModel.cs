using Avalonia.Input;
using ProductiveApp_Ava.Models;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.Diagnostics;

namespace ProductiveApp_Ava.ViewModels
{
    public class NoteViewModel : ViewModelBase
    {
        internal Note _note;

        public double x{
            get { return _note.x; } 
            set { _note.x = value; this.RaisePropertyChanged(nameof(x)); }}

        public double y
        {
            get { return _note.y; }
            set { _note.y = value; this.RaisePropertyChanged(nameof(y)); }
        }

        public NoteViewContainer container;
        public event Action onDropped;
        public event Action onDeleted;

        public NoteViewModel(Note note)
        {
            _note = note;
        }

        public virtual void Remove()
        {
            container?.OnNoteViewRemoved(this);
        }

        public virtual void OnDrop()
        {
            onDropped?.Invoke();
            Debug.WriteLine("Dropped Model!");
        }

        public virtual void OnDelete()
        {
            onDeleted?.Invoke();
            Debug.WriteLine("Deleted Model!");
        }
    }
}
