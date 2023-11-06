using Microsoft.AspNetCore.Components;

namespace Game.Blazor.Shared
{
    public partial class TableTemplate<TItem>
    {
        [Parameter]
        public bool ShowTableCaption { get; set; }

        [Parameter]
        public RenderFragment? TableCaption { get; set; }

        [Parameter]
        public RenderFragment? TableHeader { get; set; }

        [Parameter]
        public RenderFragment<TItem>? RowTemplate { get; set; }

        [Parameter]
        public IEnumerable<TItem>? Items { get; set; }
    }
}
