using BarnDynamicTask;
using System.Diagnostics;

CountTime();


static bool[,] GenerateMap(int rank, int seed)
{
    var map = new bool[rank, rank];
    Random random = new Random(seed);
    for(int y = 0; y < rank; y++)
    {
        for(int x = 0; x < rank; x++)
        {
            map[x, y] = random.Next(rank) == rank / 2;
        }
    }
    if (rank > 12) return map;
    for(int y = 0; y < rank;y++) 
    {
        for(int x = 0; x < rank; x++)
        {
            Console.Write(map[x, y] ? "X ": ". ");
        }
        Console.WriteLine();
    }
    return map;
}

static void CountTime()
{
    Stopwatch clock = new Stopwatch();
    for (int N = 1000; N <= 5_000; N += 1000)
    {
        var map = GenerateMap(N, 123456);
        var barn = new Barn(map);
        clock.Start();
        var result = barn.SolveN2();
        clock.Stop();
        Console.WriteLine($"{N} Result {result} - SolveN2 {clock.ElapsedMilliseconds / 1000} sec");
    }
}
