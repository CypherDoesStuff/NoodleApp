using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using ProductiveApp_Ava.Models;
using ProductiveApp_Ava.Services;
using ProductiveApp_Ava.ViewModels;
using System;
using System.Diagnostics;

namespace ProductiveApp_Ava.Views
{
    public partial class NoteToolView : UserControl
    {
        private Point initialPressPoint;
        private bool dragStart;
        private const double dragDistance = 20;

        public NoteToolView()
        {
            InitializeComponent();
        }

        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            initialPressPoint = e.GetPosition(this);
            dragStart = true;

            base.OnPointerPressed(e);
        }

        protected override void OnPointerMoved(PointerEventArgs e)
        {
            Point point = e.GetPosition(this);
            double distance = Math.Abs(Math.Pow(initialPressPoint.X - point.X, 2) + Math.Pow(initialPressPoint.Y - point.Y, 2));

            if (dragStart && e.GetCurrentPoint(this).Properties.IsLeftButtonPressed && distance >= dragDistance)
            {
                //Create New Note
                Debug.WriteLine("Creating note at point");
                dragStart = false;

                NoteToolViewModel toolModel = (NoteToolViewModel)DataContext;
                if (toolModel is not null)
                {
                    NoteViewModel model = MainWindowViewModel.AddNoteToDatabase(toolModel.GetNote());
                    NoteDragDrop(e, model);
                }
            }
        }

        internal async void NoteDragDrop(PointerEventArgs e, NoteViewModel viewModel)
        {
            viewModel.Remove();

            if (!CanvasView.isDragging)
            {
                DataObject data = new DataObject();
                data.Set("PersistentObject", viewModel);

                CanvasView.isDragging = true;

                MainWindowViewModel.SetDragModel(viewModel);

                DragDropEffects dropResult = await DragDrop.DoDragDrop(e, data, DragDropEffects.Move);
                if (dropResult == DragDropEffects.None)
                {
                    CanvasView.isDragging = false;
                }
            }
        }
    }
}
