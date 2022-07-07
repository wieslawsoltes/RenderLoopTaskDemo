using System;
using Avalonia.Controls;
using Avalonia.Rendering;

namespace RenderLoopTaskDemo;

public class RenderLoopTask : IRenderLoopTask
{
    private int _frame;
    private TimeSpan _previousTime;
    private TimeSpan _frameTime;
    private readonly TextBlock _textBlock;
    
    public RenderLoopTask(TextBlock textBlock)
    {
        _textBlock = textBlock;
    }
    
    public void Update(TimeSpan time)
    {
        _frameTime = time - _previousTime;
        _previousTime = time;
        _textBlock.Text = $"{_frameTime.TotalMilliseconds}ms | {_frame}";
    }

    public void Render()
    {
        _frame++;
    }

    public bool NeedsUpdate => true;
}
