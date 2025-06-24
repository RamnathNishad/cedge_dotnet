namespace HelloWorldMVC.Models
{
    public class Demo : IDemo
    {
        public string id { get; set; }
        public int counter { get; set; }

        public Demo()
        {
            id=Guid.NewGuid().ToString();
        }
       
        public void Increment()
        {
            this.counter++;
        }
    }
}
