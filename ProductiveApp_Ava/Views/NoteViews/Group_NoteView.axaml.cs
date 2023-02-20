using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using Avalonia.Input;
using ProductiveApp_Ava.ViewModels;
using ProductiveApp_Ava.Models;
using ProductiveApp_Ava.Services;
using System.Diagnostics;

namespace ProductiveApp_Ava.Views
{
    public partial class Group_NoteView : NoteViewBase
    {
        public Group_NoteView()
        {
            InitializeComponent();

            AddHandler(DragDrop.DropEvent, GroupDrop);

            groupLabel.Focusable = false;
            groupLabel.Cursor = new Cursor(StandardCursorType.Arrow);
            groupLabel.DoubleTapped += GroupLabel_DoubleTapped;
            groupLabel.LostFocus += GroupLabel_LostFocus;
        }

        private void GroupLabel_DoubleTapped(object? sender, RoutedEventArgs e)
        {
            groupLabel.Focusable = true;
            groupLabel.Focus();
            groupLabel.SelectAll();
            groupLabel.Cursor = new Cursor(StandardCursorType.Ibeam);
        }

        private void GroupLabel_LostFocus(object? sender, RoutedEventArgs e)
        {
            groupLabel.Focusable = false;
            groupLabel.Cursor =  new Cursor(StandardCursorType.Arrow);
        }

        private void GroupDrop(object? sender, DragEventArgs e)
        {
            Group_NoteViewModel groupModel = (Group_NoteViewModel)DataContext;

            bool isPersistent;
            Note note = DragEventConverter.DragEventToNote(e, out isPersistent);

            if (groupModel is not null && groupModel._note != note)
            {
                if (!isPersistent)
                    groupModel.AddNote(note);
                else //Add model directly for speed?
                    groupModel.AddNote(note);
            }

            Debug.WriteLine("Attempt drag drop on group!");

            CanvasView.isDragging = false;
            e.Handled = true;
        }
    }
}
