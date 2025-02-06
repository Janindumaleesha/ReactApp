namespace UserManagement_API.Models
{
    public class ResponseResult<T>
    {
        public bool IsSuccessful { get; set; } = false;
        public string? Message { get; set; }
        public T? Content { get; set; }

        public ResponseResult() { }

        public ResponseResult(bool _isSuccessful, string? _message = null)
        {
            IsSuccessful = _isSuccessful;
            Message = _message;
        }

        public ResponseResult(string? _message)
        {
            IsSuccessful = false;
            Message = _message;
        }

        public ResponseResult(bool _isSuccessful, string? _message, T? _content) : this(_isSuccessful, _message)
        {
            Content = _content;
        }

        public ResponseResult(Exception _ex)
        {
            IsSuccessful = false;
            Message = _ex.Message;
        }

        public ResponseResult(int _index)
        {
            IsSuccessful = false;
            Message = _index switch
            {
                1 => "Something Went Wrong, Didn't Connect with the Database. Please Try Again.",
                _ => "Something Went Wrong, Please Try Again."
            };
        }

        public ResponseResult(string? _message, T? _content) : this(_message)
        {
            Content = _content;
        }
    }
}
