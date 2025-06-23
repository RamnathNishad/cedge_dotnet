namespace HelloWorldMVC.Models
{
    public class Calculator : ICalculator
    {
        public int a {  get; set; }
        public int b { get; set; }
        public int Add(int a, int b)
        {
            return a + b;
        }

        public int Multiply(int a, int b)
        {
            return a*b;
        }

        public int Subtract(int a, int b)
        {
            return a - b;
        }
    }
}
