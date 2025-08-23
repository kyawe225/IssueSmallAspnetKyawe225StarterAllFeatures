using System;

namespace IssuePJ.Api.Response;

public class IndexModel<T>
{
    public int TotalCount { get; set; }
    public int Page { set; get; }
    public int PageSize { set; get; }
    public List<T> Items { get; set; } = new List<T>();
}
