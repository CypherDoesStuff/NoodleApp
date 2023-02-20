using Avalonia;
using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductiveApp_Ava.Views
{
    public class AutoCanvas : Canvas
    {
        private const double sizeMultiplier = 1.2;

        protected override Size MeasureOverride(Size availableSize)
        {
            base.MeasureOverride(availableSize);

            double width = 0;
            double height = 0;

            if (Children.Count > 0)
            {
                width = Children.OfType<Control>().Max(i => i.DesiredSize.Width + (double)i.GetValue(LeftProperty));
                height = Children.OfType<Control>().Max(i => i.DesiredSize.Height + (double)i.GetValue(TopProperty));
            }

            return new Size(Math.Max(width * sizeMultiplier, DesiredSize.Width), Math.Max(height * sizeMultiplier, DesiredSize.Height));
        }
    }
}
