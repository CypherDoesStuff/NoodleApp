using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using Avalonia.Input;
using ProductiveApp_Ava.ViewModels;
using ProductiveApp_Ava.Models;
using ProductiveApp_Ava.Services;
using System.Diagnostics;
using Avalonia.VisualTree;
using Avalonia;
using System.Linq;

namespace ProductiveApp_Ava.Views
{
    public partial class Group_NoteView : GroupViewBase
    {
        public Group_NoteView()
        {
            InitializeComponent();

            groupItemsControl = groupPanel;

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

        public override bool CanDrop(Note note)
        {
            return note is not Group_Note;
        }
    }
}
