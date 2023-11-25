namespace Bai6_IDisposable
{
    class A: IDisposable
    {
        bool resource = true;
        public void Dispose()   //Phuong thuc giai phong tai nguyen chiem giu-khi doi tuong bi huy
        {
            Console.WriteLine("Phuong thuc Dispose tu dong duoc goi khi het Using");
            resource = false;   //Giai phong tai nguyen
        }
    }

    //Trien khai IDisposable cung voi ham huy
    //Teamplate su dung cho cac lop co thuc thi interface IDisposable
    public class WriteData : IDisposable
    {

        // trường lưu trạng thái Dispose
        private bool m_Disposed = false;

        private StreamWriter stream;

        public WriteData(string filename)
        {
            stream = new StreamWriter(filename, true);
        }

        // Phương thức triển khai từ giao diện
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Nếu disposing = true -> Được thi hành do gọi trực tiếp (do Dispose gọi)
        // tài nguyên managed, unmanaged được giải phóng
        // nếu disposing = fale -> Được thi hành bởi phương thức hủy, chỉ cần giải phóng
        // các toàn nguyên unmanaged.
        protected virtual void Dispose(bool disposing)
        {
            if (!m_Disposed)
            {
                if (disposing)
                {
                    // các đối tượng có Dispose gọi ở đây
                    stream.Dispose();
                }

                // giải phóng các tài nguyên không quản lý được cua lớp (unmanaged)

                m_Disposed = true;
            }
        }

        ~WriteData()
        {
            Dispose(false);
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            A obj1 = new A();
            using(obj1)
            {
                Console.WriteLine("Do something ...");
            }
            Console.WriteLine();

            //Su dung using, het lenh using, moi tai nguyen se duoc giai phong
            using(new WriteData("file1.txt"))
            {
                //Do something ...
            }

            //Khong sd using, chu dong goi Dispose
            WriteData writeData1 = new WriteData("file2.txt");
            writeData1.Dispose();

        }
    }
}