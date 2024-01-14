namespace TestEasyCaching.Services
{
    public class DemoService : IDemoService
    {
        public void DeleteSomething(int id)
        {
            System.Console.WriteLine("Handle delete something..");
        }

        public string GetCurrentUtcTime()
        {
            return System.DateTime.UtcNow.ToString();
        }

        public string PutSomething(string str)
        {
            return str;
        }
    }
}
