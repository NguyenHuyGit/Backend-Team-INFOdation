using EcommerceSolution.BackendAPI.Common;

namespace EcommerceSolution.BackendAPI.ViewModels.Products
{
    public class GetProductListRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
        public string SortOrder { get; set; }
    }
}
