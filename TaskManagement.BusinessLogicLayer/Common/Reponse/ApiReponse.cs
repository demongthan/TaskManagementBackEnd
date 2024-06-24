namespace TaskManagement.BusinessLogicLayer.Common.Reponse
{
    public class ApiReponse<T>
    {
        private string? message;
        private T? data;

        public string Message { get => message; set => message = value; }
        public T Data { get => data; set => data = value; }

        public ApiReponse() { }

        public ApiReponse(string? message = null, T? data = default(T))
        {
            this.message = message;
            this.data = data;
        }
    }
}
