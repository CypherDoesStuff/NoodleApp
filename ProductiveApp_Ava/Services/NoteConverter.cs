using ProductiveApp_Ava.Models;
using ProductiveApp_Ava.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductiveApp_Ava.Services
{
    public static class NoteConverter
    {
        public static NoteViewModelBase ConvertNoteToView(Note note)
        {
            switch (note)
            {
                case Image_Note:
                    return new Image_NoteViewModel(note);
                case Text_Note:
                    return new Text_NoteViewModel(note);
                case Group_Note:
                    return new Group_NoteViewModel(note);
                case Board_Note:
                    return new Board_NoteViewModel(note);
                case Todo_Note:
                    return new TodoList_ViewModel(note);
                case TodoItem_Note:
                    return new TodoTask_ViewModel(note);
            }

            return null;
        }
    }
}
