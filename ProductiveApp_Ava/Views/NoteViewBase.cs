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
        protected override void OnPointerMoved(PointerEventArgs e)
        {
            if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
            {
                var viewModel = (NoteViewModel)DataContext;

                if (viewModel is not null)
                    DragDrop(e, viewModel);
            }

            base.OnPointerMoved(e);
        }

        private async void DragDrop(PointerEventArgs e, NoteViewModel viewModel)
        {
            if (!CanvasView.isDragging)
            {
                Debug.WriteLine("Starting drag drop");
                DataObject data = new DataObject();
                data.Set("PersistentObject", viewModel);

                CanvasView.isDragging = true;
                CanvasView.cursorOffset = e.GetPosition(Parent);
                CanvasView.currentDrag = this;

                DragDropEffects dropResult = await Avalonia.Input.DragDrop.DoDragDrop(e, data, DragDropEffects.Move);
                if (dropResult == DragDropEffects.None)
                {
                    CanvasView.isDragging = false;
                }
            }
        }
    }
}
