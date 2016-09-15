using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
class Solution
{

    struct Query
    {
        public int P;
        public char C;
    }
    private static int inputLength;
    private static int numQueries;

    private static char[] currentCharArray;
    private static ConcurrentQueue<Query> mutationQueue = new ConcurrentQueue<Query>();
    private static HashSet<string> uniqueSubs;


    static void Main(String[] args)
    {
        /* Enter your code here. Read input from STDIN. Print output to STDOUT. Your class should be named Solution */
        StreamReader sr = new StreamReader("input.txt");
        int[] NQ = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();
        inputLength = NQ[0];
        numQueries = NQ[1];

        currentCharArray = sr.ReadLine().ToCharArray();
        for (int i = 0; i < numQueries; i++)
        {
            string[] PC = sr.ReadLine().Split(' ');
            int P = int.Parse(PC[0]);
            char C = char.Parse(PC[1]);
            mutationQueue.Enqueue(new Query() { P = P, C = C });
        }

        while (!mutationQueue.IsEmpty)
        {
            Query PC;
            mutationQueue.TryDequeue(out PC);
            int P = PC.P;
            char C = PC.C;
            MutateCurrentString(P, C);
            PrintNumUniqueSubStrings();
        }
    }

    private static void MutateCurrentString(int P, char C)
    {
        currentCharArray[P - 1] = C;
    }

    private static void PrintNumUniqueSubStrings()
    {
        uniqueSubs = new HashSet<string>();
        GetUniqueSubStrings();
        Console.WriteLine($"{uniqueSubs.Count}");
    }

    private static void GetUniqueSubStrings()
    {
        for (int subLength = 1; subLength <= inputLength; subLength++)
        {
            for (int startingIndex = 0; startingIndex + subLength <= inputLength; startingIndex++)
            {
                uniqueSubs.Add(new String(currentCharArray, startingIndex, subLength));
            }
        }
    }
}