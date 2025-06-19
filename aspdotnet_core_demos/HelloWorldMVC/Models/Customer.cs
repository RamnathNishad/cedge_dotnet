namespace HelloWorldMVC.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string CustName { get; set; }
        public string Gender { get; set; }
        public List<string> Hobbies { get; set; }
    }
}
