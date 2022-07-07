using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Rendering;

namespace RenderLoopTaskDemo;

public partial class MainWindow : Window
{
    private IRenderLoopTask? _renderTask;

    public MainWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif

        CreateRenderLoopTask();

        Renderer.DrawFps = false;
        Renderer.DrawDirtyRects = false;
    }

    private void CreateRenderLoopTask()
    {
        var frame = 0;
        var previousTime = TimeSpan.Zero;
        TimeSpan frameTime;

        _renderTask = RenderLoopTaskFunc.Add(
            time =>
            {
                frameTime = time - previousTime;
                previousTime = time;
                Fps.Text = $"{frameTime.TotalMilliseconds}ms | {frame}";
            },
            () => frame++);
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);

        if (_renderTask is { })
        {
            RenderLoopTaskFunc.Remove(_renderTask);
            _renderTask = null;
        }
    }
}
