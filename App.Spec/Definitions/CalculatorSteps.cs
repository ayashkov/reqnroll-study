namespace Study.App.Spec.Definitions;

[Binding]
public sealed class CalculatorSteps {
    private readonly Calculator _calculator = new();

    [Given("the first number is {int}")]
    [Given("the result is {int}")]
    public void GivenTheFirstNumberIs(int number)
    {
        _calculator.First = number;
    }

    [Given("the second number is {int}")]
    public void GivenTheSecondNumberIs(int number)
    {
        _calculator.Second = number;
    }

    [When("the two numbers are added")]
    public void WhenTheTwoNumbersAreAdded()
    {
        _calculator.Add();
    }

    [When("calculator is reset")]
    public void WhenCalculatorIsReset()
    {
        _calculator.Reset();
    }

    [Then("the result should be {int}")]
    public void ThenTheResultShouldBe(int expected)
    {
        Assert.That(_calculator.First, Is.EqualTo(expected));
    }
}
