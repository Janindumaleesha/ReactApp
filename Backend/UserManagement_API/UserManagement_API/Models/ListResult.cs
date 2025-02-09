namespace UserManagement_API.Models
{
    public class ListResult<T>
    {
        public bool IsSuccessful { get; set; } = false;
        public string? Message { get; set; }
        public List<T>? ListContent { get; set; }
        public PaginationInfo? PaginationInfo { get; set; }

        public ListResult() { }

        public ListResult(bool _isSuccessful, string? _message = null)
        {
            IsSuccessful = _isSuccessful;
            Message = _message;
        }

        public ListResult(string? _message)
        {
            IsSuccessful = false;
            Message = _message;
        }

        public ListResult(bool _isSuccessful, string? _message, List<T>? _content) : this(_isSuccessful, _message)
        {
            ListContent = _content;
        }

        public ListResult(Exception _ex)
        {
            IsSuccessful = false;
            Message = _ex.Message;
        }

        public ListResult(int _index)
        {
            IsSuccessful = false;
            Message = _index switch
            {
                1 => "Something Went Wrong, Didn't Connect with the Database. Please Try Again.",
                _ => "Something Went Wrong, Please Try Again."
            };
        }

        public ListResult(string? _message, List<T>? _content) : this(_message)
        {
            ListContent = _content;
        }
    }

    public class PaginationInfo()
    {
        public int TotalPages { get; set; }
        public int Page { get; set; }
        public int NextPage { get; set; }
        public int RowCount { get; set; }
    }
}
