using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using ProductiveApp_Ava.Models;
using ProductiveApp_Ava.Services;
using ProductiveApp_Ava.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductiveApp_Ava.Views
{
    public class GroupViewBase : NoteViewBase
    {
        public ItemsControl groupItemsControl;

        protected override void OnInitialized()
        {
            AddHandler(DragDrop.DropEvent, GroupDrop);

            base.OnInitialized();
        }

        private void GroupDrop(object? sender, DragEventArgs e)
        {
            GroupViewModelBase groupModel = (GroupViewModelBase)DataContext;

            Point dropPoint = e.GetPosition(groupItemsControl);

            bool isPersistent;
            Note note = DragEventConverter.DragEventToNote(e, out isPersistent);

            if (!CanDrop(note))
                return;

            int currentIndex = 0;
            int itemCount = groupItemsControl.ItemCount;

            for (int i = 0; i < itemCount; i++)
            {
                Control element = (Control)groupItemsControl.ItemContainerGenerator.ContainerFromIndex(i);
                Point? elementPoint = element.TranslatePoint(new Point(), groupItemsControl);

                if (elementPoint.HasValue)
                {
                    double elementY = elementPoint.Value.Y + element.DesiredSize.Height / 2;

                    if (dropPoint.Y < elementY)
                        break;

                    currentIndex++;
                }
            }

            //Maybe use the isPersistient bool and drag the model directly?
            if (currentIndex >= itemCount)
            {
                if (groupModel is not null && groupModel._note != note)
                    groupModel.AddNote(note);
            }
            else
            {
                if (groupModel is not null && groupModel._note != note)
                    groupModel.InsertNote(note, currentIndex);
            }

            CanvasView.isDragging = false;
            e.Handled = true;
        }

        public virtual bool CanDrop(Note note)
        {
            return false;
        }
    }
}
