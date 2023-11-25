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
    class Program
    {
        static void Main(string[] args)
        {
            A obj1 = new A();
            A obj2 = new A();
            using(obj1)
            {
                Console.WriteLine("Do something ...");
            }
        }
    }
}