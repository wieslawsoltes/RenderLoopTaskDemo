using Avalonia;
using Avalonia.Controls;
using Avalonia.Rendering;

namespace RenderLoopTaskDemo;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif

        var renderLoop = AvaloniaLocator.Current.GetService<IRenderLoop?>();
        if (renderLoop is { })
        {
            var renderTask = new RenderLoopTask(Fps);

            renderLoop.Add(renderTask);
        }

        Renderer.DrawFps = true;
        Renderer.DrawDirtyRects = false;
    }
}
