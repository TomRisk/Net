namespace ConsoleApp1.多态
{
    public class Assets
    {
        public string Name { get; set; }
    }

    public class Stock:Assets
    {
        public long SharesOwned { get; set; }
    }

    public class House : Assets
    {
        public decimal Mortgage { get; set; }
    }
}
