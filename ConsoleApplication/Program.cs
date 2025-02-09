
namespace ConsoleApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ClassLibrary.Models.Expression expression = new ClassLibrary.Models.Expression("test word", "pl");
            Console.WriteLine("Hello, Langchips!" + expression.ExpressionText);
        }
    }
}
