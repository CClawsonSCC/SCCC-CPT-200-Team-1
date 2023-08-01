using Microsoft.JSInterop;

// This class inherits the IClipboardService in order to use the methods.
// We use the JSInterop in order to copy our credentials to the clipboard.
public class ClipboardService : IClipboardService
{
    private readonly Microsoft.JSInterop.IJSRuntime _jsInterop;
    public ClipboardService(Microsoft.JSInterop.IJSRuntime jsInterop)
    {
        _jsInterop = jsInterop; // We initialize the service to be used
    }
    // We call this method and take in a string, which is the username and password, then copies to the user's clipboard.
    public async Task CopyToClipboard(string text)
    {
        await _jsInterop.InvokeVoidAsync("navigator.clipboard.writeText", text);
    }
}