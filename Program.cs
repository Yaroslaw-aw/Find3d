using System.Text;

namespace Find3d
{
    internal class Program
    {
        static  void Main(string[] args)
        {            
            Find find = new Find(5, 5, 5);

            find.PutRandomNumbers(150);

            int exits = find.FindQueue(2, 2, 2);

            Console.WriteLine("Queue " + exits);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < find.map.GetLength(0); i++)
                for (int j = 0; j < find.map.GetLength(1); j++)
                    for (int k = 0; k < find.map.GetLength(2); k++)
                        sb.Append(find.map[i, j, k].ToString());

            string text = sb.ToString();


            if (exits == 1)
            {
                try
                {
                    string path = @".\BoxLabirinth.txt";

                    using (FileStream fstream = new FileStream(path, FileMode.Append))
                    {
                        byte[] buffer = Encoding.UTF8.GetBytes(text);
                        fstream.WriteAsync(buffer, 0, buffer.Length);
                        Console.WriteLine("\nЛабиринт записан в файл");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }


            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    for (int k = 0; k < 5; k++)
                        if (find.map[i, j, k] == 4) find.map[i, j, k] = 0;

            Console.WriteLine("Stack " + find.FindStack(2, 2, 2));

            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    for (int k = 0; k < 5; k++)
                        if (find.map[i, j, k] == 4) find.map[i, j, k] = 0;

            Console.WriteLine("Depth " + find.FindDepth(2, 2, 2));





        }
    }
}
