namespace Study.App.Spec.Definitions;

[Binding]
public class Global {
    [BeforeTestRun]
    public static void BeforeRun()
    {
        Console.WriteLine("Before run");
    }

    [AfterTestRun]
    public static void AfterRun()
    {
        Console.WriteLine("After run");
    }
}
