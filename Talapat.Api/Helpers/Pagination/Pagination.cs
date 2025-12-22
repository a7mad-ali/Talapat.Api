using Talabat.Core.Entities;
using Talapat.Api.DTOs.Product;

namespace Talapat.Api.Helpers.Pagination
{
    public class Pagination<T>
    {
        private int pageindex;
        private IReadOnlyList<T> data;

       

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IReadOnlyList<T> Data { get; set; }

        public Pagination(int pageIndex, int pageSize, IReadOnlyList<T> data,int count)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Data = data;
            Count = count;
        }

    }
}
