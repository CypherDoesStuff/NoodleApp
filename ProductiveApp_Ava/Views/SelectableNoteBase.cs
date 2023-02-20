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
    public class SelectableNoteBase : NoteViewBase
    {
        private bool _selected;
        public bool Selected
        {
            get { return _selected; }
            set { _selected = value; selectedChanged?.Invoke(this, _selected); }
        }

        internal event EventHandler<bool> selectedChanged;

        private Point initialPressPoint;
        private const double dragDistance = 200;

        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            initialPressPoint = e.GetPosition(this);

            base.OnPointerPressed(e);
        }

        protected override void OnPointerReleased(PointerReleasedEventArgs e)
        {
            if (e.InitialPressMouseButton == MouseButton.Left)
            {
                if(CanvasView.currentSelectable is not null)
                    CanvasView.currentSelectable.Selected = false;

                Selected = true;
                CanvasView.currentSelectable = this;

                e.Handled = true;
            }

            base.OnPointerReleased(e);
        }

        protected override void OnPointerMoved(PointerEventArgs e)
        {
            Point point = e.GetPosition(this);
            double distance = Math.Abs(Math.Pow(initialPressPoint.X - point.X, 2) + Math.Pow(initialPressPoint.Y - point.Y, 2));

            if (!Selected && e.GetCurrentPoint(this).Properties.IsLeftButtonPressed && distance > dragDistance)
            {
                var viewModel = (NoteViewModel)DataContext;

                if (viewModel is not null)
                    NoteDragDrop(e, viewModel);

                e.Handled|= true;
            }
        }
    }
}
