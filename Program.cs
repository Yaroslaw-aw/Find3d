using System.Text;

namespace Find3d
{
    internal class Program
    {
        static string LabToString(int[,,] labirinth)
        {
            StringBuilder sb = new StringBuilder();

            int count = 0;

            for (int i = 0; i < labirinth.GetLength(0); i++)
                for (int j = 0; j < labirinth.GetLength(1); j++)
                    for (int k = 0; k < labirinth.GetLength(2); k++)
                    {
                        count++;
                        sb.Append(labirinth[i, j, k].ToString());
                        if (count % 5 == 0 && count < 125)
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

            find.GenerateLabyrinth(200);

            int exits = find.FindExitsByQueue(2, 2, 2);
            Console.WriteLine("Queue " + exits);

            RefreshLabirinth(find.map);
            Console.WriteLine("Stack " + find.FindExitsByStack(2, 2, 2));

            RefreshLabirinth(find.map);
            Console.WriteLine("Depth " + find.FindExitsByRecursion(2, 2, 2));

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

                    string text = LabToString(find.map);

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

                    continue;
                }

                find.Clear();

                find.GenerateLabyrinth(200);

                exits = find.FindExitsByQueue(2, 2, 2);
            }
        }
    }
}
