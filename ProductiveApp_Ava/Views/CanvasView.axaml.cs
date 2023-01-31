using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using ProductiveApp_Ava.Models;
using ProductiveApp_Ava.Services;
using ProductiveApp_Ava.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ProductiveApp_Ava.Views
{
    public partial class CanvasView : UserControl
    {
        public static bool isDragging;
        public static Point cursorOffset = new Point();
        public static NoteViewBase currentDrag;

        public static double snapSize = 10;

        public CanvasView()
        {
            InitializeComponent();

            AddHandler(DragDrop.DragEnterEvent, Canvas_Enter);
            AddHandler(DragDrop.DragOverEvent, Canvas_Over);
            AddHandler(DragDrop.DropEvent, Canvas_Drop);
        }

        public void Canvas_Enter(object? sender, DragEventArgs e)
        {
            e.DragEffects = DragDropEffects.Move;
        }

        public void Canvas_Over(object? sender, DragEventArgs e)
        {
            var data = e.Data.Get("PersistentObject");

            Point movePoint = new Point(
                e.GetPosition(this).X - cursorOffset.X,
                e.GetPosition(this).Y - cursorOffset.Y
                );

            if(e.KeyModifiers != KeyModifiers.Control)
                movePoint = SnapPointToGrid(movePoint);

            if(data is NoteViewModel model)
            {
                model.x = movePoint.X;
                model.y = movePoint.Y;
            }
        }

        public void Canvas_Drop(object? sender, DragEventArgs e)
        {
            isDragging = false;
            /*
            Note note = DragEventConverter.DragEventToNote(e);
            Point dropPoint = e.GetPosition(this);

            note.x = dropPoint.X;
            note.y = dropPoint.Y;
            MainWindowViewModel.AddNoteToCollection(note);
            */
        }

        private static Point SnapPointToGrid(Point pointToSnap)
        {
            Point returnPoint = new Point(
                 Math.Round(pointToSnap.X / snapSize) * snapSize,
                 Math.Round(pointToSnap.Y / snapSize) * snapSize
            );

            return returnPoint;
        }
    }
}
