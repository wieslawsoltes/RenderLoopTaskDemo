using System;
using Avalonia;
using Avalonia.Rendering;

namespace RenderLoopTaskDemo;

public class RenderLoopTaskFunc : IRenderLoopTask
{
    private readonly Action<TimeSpan>? _update;
    private readonly Action? _render;

    public RenderLoopTaskFunc(Action<TimeSpan>? update, Action? render)
    {
        _update = update;
        _render = render;
    }

    public void Update(TimeSpan time) => _update?.Invoke(time);

    public void Render() => _render?.Invoke();

    public bool NeedsUpdate => true;

    public static IRenderLoopTask? Add(Action<TimeSpan>? update, Action? render)
    {
        var renderLoop = AvaloniaLocator.Current.GetService<IRenderLoop?>();
        if (renderLoop is null)
        {
            return null;
        }
        
        var renderTask = new RenderLoopTaskFunc(update, render);
        
        renderLoop.Add(renderTask);

        return renderTask;
    }
    
    public static bool Remove(IRenderLoopTask? renderTask)
    {
        if (renderTask is null)
        {
            return false;
        }

        var renderLoop = AvaloniaLocator.Current.GetService<IRenderLoop?>();
        if (renderLoop is null)
        {
            return false;
        }

        renderLoop.Remove(renderTask);

        return true;
    }
}
