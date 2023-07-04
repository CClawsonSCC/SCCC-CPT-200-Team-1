using Microsoft.JSInterop;

public class ClipboardService : IClipboardService
{
    private readonly Microsoft.JSInterop.IJSRuntime _jsInterop;
    public ClipboardService(Microsoft.JSInterop.IJSRuntime jsInterop)
    {
        _jsInterop = jsInterop;
    }
    public async Task CopyToClipboard(string text)
    {
        await _jsInterop.InvokeVoidAsync("navigator.clipboard.writeText", text);
    }
}