using Microsoft.JSInterop;

namespace Game.Blazor.Shared
{

    public static class ConfirmExtensions
    {
        public static ValueTask<bool> Confirm(this IJSRuntime jsRuntime, string message)
        {
            return jsRuntime.InvokeAsync<bool>("confirm", message);
        }
    }
}
