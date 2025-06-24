namespace HelloWorldMVC.Models
{
    public interface IDemo
    {
        int counter {  get; set; }
        string id { get; }

        void Increment();        
    }
}
