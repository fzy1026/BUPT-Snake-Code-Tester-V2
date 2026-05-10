using System;
using System.Collections.Generic;
using System.Drawing;
using SnakeOJTester;

internal static class CaseValidityHarness
{
    private static int Main()
    {
        List<TestCase> cases = TestCaseFactory.CreateDefaultCases();
        int failures = 0;

        for (int i = 0; i < cases.Count; i++)
        {
            TestCase testCase = cases[i];
            MapInfo map = Parse(testCase.InitialMap);
            List<Point> adjacentBodies = AdjacentBodies(map.Head, map.Body);
            int fullPaths = CountFullPaths(map.Head, map.Body);

            Console.WriteLine(
                "case=" + (i + 1) +
                " name=" + testCase.Name +
                " bodyCount=" + map.Body.Count +
                " adjacentBodies=" + adjacentBodies.Count +
                " fullPaths=" + fullPaths);

            if (map.Body.Count != 2 || adjacentBodies.Count != 1 || fullPaths != 1)
            {
                failures++;
            }
        }

        if (failures > 0)
        {
            Console.WriteLine("invalidCases=" + failures);
            return 1;
        }

        Console.WriteLine("allCasesMatchOfficialInitialSnakeRule");
        return 0;
    }

    private static MapInfo Parse(string[] lines)
    {
        MapInfo info = new MapInfo();
        for (int r = 0; r < lines.Length; r++)
        {
            for (int c = 0; c < lines[r].Length; c++)
            {
                char ch = lines[r][c];
                if (ch == 'H')
                {
                    info.Head = new Point(c, r);
                }
                else if (ch == 'B')
                {
                    info.Body.Add(new Point(c, r));
                }
            }
        }
        return info;
    }

    private static List<Point> AdjacentBodies(Point head, List<Point> body)
    {
        List<Point> result = new List<Point>();
        for (int i = 0; i < body.Count; i++)
        {
            if (Adjacent(head, body[i]))
            {
                result.Add(body[i]);
            }
        }
        return result;
    }

    private static int CountFullPaths(Point head, List<Point> body)
    {
        bool[] used = new bool[body.Count];
        int count = 0;
        for (int i = 0; i < body.Count; i++)
        {
            if (!Adjacent(head, body[i]))
            {
                continue;
            }
            used[i] = true;
            CountFrom(body[i], body, used, 1, ref count);
            used[i] = false;
        }
        return count;
    }

    private static void CountFrom(Point current, List<Point> body, bool[] used, int usedCount, ref int count)
    {
        if (usedCount == body.Count)
        {
            count++;
            return;
        }

        for (int i = 0; i < body.Count; i++)
        {
            if (used[i] || !Adjacent(current, body[i]))
            {
                continue;
            }
            used[i] = true;
            CountFrom(body[i], body, used, usedCount + 1, ref count);
            used[i] = false;
        }
    }

    private static bool Adjacent(Point a, Point b)
    {
        return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y) == 1;
    }

    private sealed class MapInfo
    {
        public Point Head;
        public List<Point> Body;

        public MapInfo()
        {
            Head = new Point(-1, -1);
            Body = new List<Point>();
        }
    }
}
