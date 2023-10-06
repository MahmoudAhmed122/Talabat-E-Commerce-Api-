namespace Talabat.APIs.Helpers
{
    public class Pagination<T>
    {


        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int Count { get; set; }

        public IReadOnlyList<T> Data { get; set; }


        public Pagination(int PageSize  , int PageIndex , int Count , IReadOnlyList<T> Data)
        {
            this.PageSize = PageSize;
            this.PageIndex = PageIndex;
            this.Count = Count; 
            this.Data = Data;   
        }
    }
}
