using Newtonsoft.Json;


namespace Class_lib
{
    class Product
    {
        public string Name { get; set; }
        public DateTime Expiry { get; set; }
        public string[] Sizes { get; set; }
    }
    class Program
    {
        static void Main(string[] args) 
        {
            Product product = new Product();
            product.Name = "Apple";
            product.Expiry = new DateTime(2008, 12, 28);
            product.Sizes = new string[] { "Small" };

            string json = JsonConvert.SerializeObject(product);

            Console.WriteLine(json);

            // {
            //   "Name": "Apple",
            //   "Expiry": "2008-12-28T00:00:00",
            //   "Sizes": [
            //     "Small"
            //   ]
            // }
            Console.WriteLine();

            string json1 = @"
            {
                ""Name"": ""Dien thoai Iphone"",
                ""Expiry"": ""2008-12-28T00:00:00"",
                ""Size"": [""Large"",""Big""]
            }
            ";

            Product product1 = JsonConvert.DeserializeObject<Product>(json1);
            Console.WriteLine(product1.Name);
        }
    }
}