using System;
using System.Collections.Generic;
using SnakeOJTester;

internal static class ScoringTraceHarness
{
    private static int Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("usage: ScoringTraceHarness.exe student.exe");
            return 2;
        }

        TestCase scoringCase = null;
        List<TestCase> cases = TestCaseFactory.CreateDefaultCases();
        for (int i = 0; i < cases.Count; i++)
        {
            if (cases[i].IsScoringCase)
            {
                scoringCase = cases[i];
                break;
            }
        }

        if (scoringCase == null)
        {
            Console.WriteLine("no scoring case");
            return 1;
        }

        RunOptions options = new RunOptions();
        options.LineTimeoutMs = 1000;
        options.FinalTimeoutMs = 1500;
        options.CaptureDetails = true;
        options.MaxSnapshotsToKeep = 2500;
        options.MaxLogChars = 60000;
        options.SnapshotInterval = 1;

        JudgeResult result = JudgeEngine.RunCase(args[0], scoringCase, options);
        Console.WriteLine("case=" + result.CaseName + " status=" + result.StatusText + " steps=" + result.Steps + " snapshots=" + result.Snapshots.Count);

        if (result.Snapshots.Count == 0)
        {
            Console.WriteLine("no snapshots captured");
            return 1;
        }

        for (int i = 1; i < result.Snapshots.Count; i++)
        {
            int previous = result.Snapshots[i - 1].Step;
            int current = result.Snapshots[i].Step;
            if (current < previous || current - previous > 1)
            {
                Console.WriteLine("snapshot step gap: " + previous + " -> " + current);
                return 1;
            }
        }

        Console.WriteLine("scoringTraceIsStepByStep");
        return 0;
    }
}
