using EcommerceSolution.BackendAPI.Common;

namespace EcommerceSolution.BackendAPI.ViewModels.Product
{
    public class GetProductListRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
        public string SortOrder { get; set; }
    }
}
