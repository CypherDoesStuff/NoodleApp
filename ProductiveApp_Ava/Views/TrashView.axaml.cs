using Avalonia.Controls;
using Avalonia.Input;
using ProductiveApp_Ava.Models;
using ProductiveApp_Ava.Services;
using ProductiveApp_Ava.ViewModels;
using System;

namespace ProductiveApp_Ava.Views
{
    public partial class TrashView : UserControl
    {
        public TrashView()
        {
            InitializeComponent();

            AddHandler(DragDrop.DragOverEvent, Trash_DragOver);
            AddHandler(DragDrop.DropEvent, Trash_Drop);
        }

        private void Trash_DragOver(object? sender, DragEventArgs e)
        {
            e.DragEffects = DragDropEffects.Move;
        }

        private void Trash_Drop(object? sender, DragEventArgs e)
        {
            CanvasView.isDragging = false;

            var data = e.Data.Get("PersistentObject");
            if (data is NoteViewModel model)
            {
                MainWindowViewModel.RemoveNoteFromCollection(model._note);
                MainWindowViewModel.ClearDragModel();

                e.Handled = true;
            }
        }
    }
}
