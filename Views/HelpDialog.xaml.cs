using Avalonia;
using Avalonia.Controls.Shapes;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;

namespace BookEditor.Views
{
    public class HelpDialog : Window
    {
        public HelpDialog()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            Close("OK Clicked!");
        }
    }
}