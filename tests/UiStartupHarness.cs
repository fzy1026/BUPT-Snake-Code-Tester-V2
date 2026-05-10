using System;
using System.Reflection;
using System.Windows.Forms;
using SnakeOJTester;

internal static class UiStartupHarness
{
    [STAThread]
    private static int Main()
    {
        try
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (MainForm form = new MainForm())
            {
                MethodInfo applyLayout = typeof(MainForm).GetMethod("ApplyStartupLayout", BindingFlags.Instance | BindingFlags.NonPublic);
                if (applyLayout == null)
                {
                    Console.WriteLine("ApplyStartupLayout not found");
                    return 1;
                }
                applyLayout.Invoke(form, null);
                Console.WriteLine("mainFormConstructedAndLayoutApplied");
            }
            return 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.GetType().FullName);
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            return 1;
        }
    }
}
