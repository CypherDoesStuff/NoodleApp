using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media.TextFormatting.Unicode;
using Avalonia.Styling;
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

        public static SelectableNoteBase currentSelectable;

        public static double snapSize = 10;

        CanvasViewModel viewModel;

        public CanvasView()
        {
            InitializeComponent();

            AddHandler(DragDrop.DragEnterEvent, Canvas_Enter);
            AddHandler(DragDrop.DragOverEvent, Canvas_Over);
            AddHandler(DragDrop.DropEvent, Canvas_Drop);
            AddHandler(PointerReleasedEvent, Canvas_Released);

            viewModel = (CanvasViewModel)DataContext;
        }

        private void Canvas_Released(object? sender, PointerReleasedEventArgs e)
        {
            if(e.InitialPressMouseButton == MouseButton.Left)
            {
                if (currentSelectable is not null)
                    currentSelectable.Selected = false;
            }

            if(e.Source is AutoCanvas)
            {
                FocusManager.Instance.Focus(null);
            }
            Debug.WriteLine(e.Source);
        }

        protected void Canvas_Enter(object? sender, DragEventArgs e)
        {
            e.DragEffects = DragDropEffects.Move;
        }

        protected void Canvas_Over(object? sender, DragEventArgs e)
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

        protected void Canvas_Drop(object? sender, DragEventArgs e)
        {
            isDragging = false;

            bool isPersistent;
            Note note = DragEventConverter.DragEventToNote(e, out isPersistent);
            if (note != null)
            {
                if (!isPersistent)
                {
                    Point dropPoint = e.GetPosition(this);

                    note.x = dropPoint.X;
                    note.y = dropPoint.Y;
                    MainWindowViewModel.AddNoteToDatabase(note);
                }
                else
                {
                    Point movePoint = new Point(
                        e.GetPosition(itemControl).X - cursorOffset.X,
                        e.GetPosition(itemControl).Y - cursorOffset.Y
                    );

                    if (e.KeyModifiers != KeyModifiers.Control)
                        movePoint = SnapPointToGrid(movePoint);

                    object data = e.Data.Get("PersistentObject");
                    if (data is NoteViewModel model)
                    {
                        MainWindowViewModel.AddViewToCollection(model);
                        model.x = movePoint.X;
                        model.y = movePoint.Y;
                    }

                    MainWindowViewModel.ClearDragModel();
                }
            }
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
