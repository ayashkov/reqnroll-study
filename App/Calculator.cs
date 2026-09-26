namespace Study.App;

public class Calculator {
    public int First { get; set; }

    public int Second { get; set; }

    public void Add()
    {
        First += Second;
    }

    public void Reset()
    {
        First = 0;
    }
}
