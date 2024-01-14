using System.Text;

namespace Find3d
{
    internal class Program
    {
        static string LabToString(int[,,] labitinth, StringBuilder sb)
        {
            int count = 0;

            for (int i = 0; i < labitinth.GetLength(0); i++)
                for (int j = 0; j < labitinth.GetLength(1); j++)
                    for (int k = 0; k < labitinth.GetLength(2); k++)
                    {
                        count++;
                        sb.Append(labitinth[i, j, k].ToString());
                        if (count % 5 == 0)
                            sb.Append("\n");
                        if (count % 25 == 0)
                            sb.Append("\n");
                    }

            return sb.ToString() + new string('-', 20) + "\n";
        }

         static void RefreshLabirinth(int[,,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    for (int k = 0; k < matrix.GetLength(2); k++)
                        if (matrix[i, j, k] != 1) matrix[i, j, k] = 0;
        }

        static void Main(string[] args)
        {
            Find find = new Find(5, 5, 5);
            
            find.PutRandomNumbers(200);

            int exits = find.FindQueue(2, 2, 2);
            
            Console.WriteLine("Queue " + exits);

            RefreshLabirinth(find.map);

            Console.WriteLine("Stack " + find.FindStack(2, 2, 2));

            RefreshLabirinth(find.map);

            Console.WriteLine("Depth " + find.FindDepth(2, 2, 2));

            Console.ReadKey();            

            int count = 0;
            bool stop = false;
            while (!stop)
            {
                Console.CursorVisible = false;
                Console.Clear();

                count++;

                Console.Write(count);

                if (exits == 1)
                {
                    stop = true;

                    StringBuilder stringBuilder = new StringBuilder();

                    string text = LabToString(find.map, stringBuilder);

                    try
                    {
                        string path = @".\BoxLabirinth.txt";

                        using (FileStream fstream = new FileStream(path, FileMode.Create))
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

                    continue;
                }

                find.Clear();

                find.PutRandomNumbers(200);                

                exits = find.FindQueue(2, 2, 2);
            }
        }
    }
}
