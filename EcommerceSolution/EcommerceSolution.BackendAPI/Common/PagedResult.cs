using System.Collections.Generic;

namespace EcommerceSolution.BackendAPI.Common
{
    public class PagedResult<T> : PagedResultBase
    {
        public List<T> Items { set; get; }
    }
}