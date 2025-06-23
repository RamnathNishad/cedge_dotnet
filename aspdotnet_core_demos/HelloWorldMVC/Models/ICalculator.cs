namespace HelloWorldMVC.Models
{
    public interface ICalculator
    {
        int a {  get; set; }
        int b { get; set; }
        int Add(int a,int b);
        int Subtract(int a,int b);
        int Multiply(int a,int b);
    }
}
