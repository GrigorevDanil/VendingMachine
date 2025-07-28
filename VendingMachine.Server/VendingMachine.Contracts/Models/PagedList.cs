    namespace VendingMachine.Contracts.Models;

    public class PagedList<T>
    {
        public IReadOnlyList<T> Items { get; init; } = [];

        public long TotalCount { get; init; }

        public int PageSize { get; init; }

        public int Page { get; init; }

        public int PageCount => Items.Count > 0 ? (int)Math.Ceiling((double)TotalCount / PageSize) : 0;
        
        public bool HasNextPage => Page * PageSize < TotalCount;
        
        public bool HasPreviousPage => Page > 1;
        
        public Dictionary<string, object>? Options { get; init; } = new();
    }