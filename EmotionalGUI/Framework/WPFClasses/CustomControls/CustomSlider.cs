using System.Windows.Controls;
using System.Windows.Input;
using System;

namespace Framework
{
    public class CustomSlider : Slider
    {
        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            MouseButtonEventArgs args = new MouseButtonEventArgs(e.MouseDevice, 0, MouseButton.XButton1);
            args.RoutedEvent = e.RoutedEvent;
            if (e.LeftButton == MouseButtonState.Pressed) OnPreviewMouseLeftButtonDown(args);
        }
    }
}
