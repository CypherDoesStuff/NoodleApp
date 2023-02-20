using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using ProductiveApp_Ava.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductiveApp_Ava.Views
{
    public class NoteViewBase : UserControl
    {
        protected override void OnInitialized()
        {
            NoteViewModel viewModel = (NoteViewModel)DataContext;
            if(viewModel is not null)
            {

            }

            base.OnInitialized();
        }

        protected override void OnPointerMoved(PointerEventArgs e)
        {
            if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
            {
                var viewModel = (NoteViewModel)DataContext;

                if (viewModel is not null)
                {
                    NoteDragDrop(e, viewModel);

                    e.Handled = true;
                }
            }

            base.OnPointerMoved(e);
        }

        internal async void NoteDragDrop(PointerEventArgs e, NoteViewModel viewModel)
        {
            if (!CanvasView.isDragging)
            {
                viewModel.Remove();

                DataObject data = new DataObject();
                data.Set("PersistentObject", viewModel);

                CanvasView.isDragging = true;
                CanvasView.cursorOffset = e.GetPosition(Parent);

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
