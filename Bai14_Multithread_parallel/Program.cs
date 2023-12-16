namespace Multithread_parallel
{
    class Program
    {
        //In thong tin, TaskID va ThreadID dang chay
        public static void PintInfo(string info) =>
           Console.WriteLine($"{info,10}    task:{Task.CurrentId,3}    " +
                             $"thread: {Thread.CurrentThread.ManagedThreadId}");

        // Phương thức phù hợp với Action<int>, được làm tham số action của Parallel.For
        //public static void RunTask(int i)
        //{
        //    PintInfo($"Start {i,3}");
        //    Task.Delay(1000).Wait();          // Task dừng 1s - rồi mới chạy tiếp
        //    PintInfo($"Finish {i,3}");
        //}

        public static async void RunTask(int i)
        {
            PintInfo($"Start {i,3}");
            // Task.Delay(1000).Wait();          // Task dừng 1s - rồi mới chạy tiếp
            await Task.Delay(1);         // Task.Delay là một async nên có thể await, RunTask chuyển điểm gọi nó tại đây
            PintInfo($"Finish {i,3}");
        }

        public static void ParallelFor()
        {
            ParallelLoopResult result = Parallel.For(1, 20, RunTask);   // Vòng lặp tạo ra 20 lần chạy RunTask
            Console.WriteLine($"All task start and finish: {result.IsCompleted}");
        }

        public static async void RunTask1(string s)
        {
            PintInfo($"Start {s,10}");
            await Task.Delay(1);                 // Task.Delay là một async nên có thể await, RunTask chuyển điểm gọi nó tại đây
            PintInfo($"Finish {s,10}");
        }

        public static void ParallelFor1()
        {

            string[] source = new string[] {"xuanthulab1","xuanthulab2","xuanthulab3",
                                    "xuanthulab4","xuanthulab5","xuanthulab6",
                                    "xuanthulab7","xuanthulab8","xuanthulab9"};
            // Dùng List thì khởi tạo
            // List<string> source = new List<string>();
            // source.Add("xuanthulab1");

            ParallelLoopResult result = Parallel.ForEach(
                source, RunTask1
            );

            Console.WriteLine($"All task started: {result.IsCompleted}");
        }

        static void Main(string[] args)
        {
            ParallelFor();
            ParallelFor1();

            Console.WriteLine("Press any key ..."); Console.ReadKey();
        }
    }
}