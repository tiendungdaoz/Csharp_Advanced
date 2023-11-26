using System.IO;
using System.Text;
using System.Xml.Linq;

namespace File_Stream
{
    class Product
    {
        public int id { set; get; }
        public double price { set; get; }
        public string name { set; get; }

        public void Save(Stream stream)
        {
            //int -> 4byte
            var bytes_id = BitConverter.GetBytes(id);
            stream.Write(bytes_id, 0, 4);

            var bytes_price = BitConverter.GetBytes(price);
            stream.Write(bytes_price, 0, 8);

            var bytes_name = Encoding.UTF8.GetBytes(name);
            var bytes_length = BitConverter.GetBytes(bytes_name.Length);

            stream.Write(bytes_length, 0, 4);
            stream.Write(bytes_name, 0, bytes_name.Length);
        }

        public void Restore(Stream stream)
        {
            var bytes_id = new byte[4];
            stream.Read(bytes_id, 0, 4);
            id = BitConverter.ToInt32(bytes_id,0);

            var bytes_price = new byte[8];
            stream.Read(bytes_price, 0, 8);
            price = BitConverter.ToDouble(bytes_price, 0);

            var bytes_length_name = new byte[4];
            stream.Read(bytes_length_name, 0, 4);
            int length_name = BitConverter.ToInt32(bytes_length_name,0);

            var bytes_name = new byte[length_name];
            stream.Read(bytes_name, 0, length_name);
            name = Encoding.UTF8.GetString(bytes_name);

        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            string filepath = "Test1.txt";
            using (var stream = new FileStream(path: filepath, mode: FileMode.OpenOrCreate, access: FileAccess.ReadWrite, share: FileShare.ReadWrite))
            {
                // code sử dụng stream (System.IO.Stream)

                //Luu du lieu
                byte[] buffer = new byte[] { 1, 2, 3, 4, 9 };
                int offset = 0;
                int count = 5;
                stream.Write(buffer, offset, count);

                //Doc du lieu
                byte[] buffer2 = new byte[20];
                int soByteDocDuoc = stream.Read(buffer2, offset, count);

                //Chuyen kieu du lieu co ban: int, double, ... sang mang byte[]
                int abc = 1;
                var byte_abc = BitConverter.GetBytes(abc);

                foreach (var item in byte_abc)
                {
                    Console.WriteLine(item);
                }

                //Chuyen doi nguoc lai tu mang byte[] sang cac kieu du lieu co ban
                int cba = BitConverter.ToInt32(byte_abc);
                Console.WriteLine(cba);

                //Chuyen doi chuoi sang byte[] va nguoc lai
                string str1 = "Dao Tien Dung";
                var bytes_s = Encoding.UTF8.GetBytes(str1);
                string str2 = Encoding.UTF8.GetString(bytes_s);   // str2 = "Dao Tien Dung"
            }

            string filepath2 = "Test2.txt";
            using(var stream2 = new FileStream(path: filepath2, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                Console.WriteLine("-------------------------------");
                Product product1 = new Product();

                //product1.id = 60;
                //product1.price = 1000;
                //product1.name = "IP13pro";
                ////Luu thong so san pham 1 vao stream
                //product1.Save(stream2);

                Product res_product1 = new Product();
                res_product1.Restore(stream2);
                Console.WriteLine($"{res_product1.id} - {res_product1.price} - {res_product1.name}");

            }
        }  
    }
}