namespace CRUDTests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        // Arrange
        var mm = new MyMath();
        int input1 = 10, input2 = 5;
        var expected = 15;

        // Act
        var actual = mm.Add(input1, input2);

        // Assert
        Assert.Equal(expected, actual);
    }
}