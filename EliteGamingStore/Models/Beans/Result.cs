using System;

namespace EliteGamingStore.Models.Beans
{
    public class Result<T>
    {
        public bool isSuccess { get; set; }
        public string message { get; set; }
        public T Data {get; set;}
        public Exception exception { get; set; }
    }
}
