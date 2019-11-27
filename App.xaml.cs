using Avalonia;
using Avalonia.Markup.Xaml;

namespace BookEditor
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
   }
}