using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using ProductiveApp_Ava.ViewModels;
using System;
using System.Diagnostics;

namespace ProductiveApp_Ava.Views
{
    public partial class Text_NoteView : SelectableNoteBase
    {
        public Text_NoteView()
        {
            InitializeComponent();
            selectedChanged += Text_NoteView_selectedChanged;
            Selected = false;
        }

        private void Text_NoteView_selectedChanged(object? sender, bool e)
        {
            textBox.Focusable = e;
            textBox.IsHitTestVisible = e;

            if (e)
            {
                textBox.Focus();
                textBox.CaretIndex = textBox.Text.Length;
            }
            else
                FocusManager.Instance.Focus(null);
        }
    }
}
