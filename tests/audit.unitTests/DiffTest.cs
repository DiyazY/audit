using audit.Utils;
using Xunit;

namespace audit.unitTests;

public class DiffTest
{
    [Fact]
    public void Test1()
    {
        // arrange
        var a = "{\"name\": \"diyaz\"}";
        var b = "{\"name\": \"diyaz yakubov\"}";
        
        // act
        var diff = Diff.Get(a, b);

        // assert
        Assert.NotNull(diff);
    }
}