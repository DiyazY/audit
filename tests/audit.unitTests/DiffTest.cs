using audit.Utils;
using System.Threading.Tasks;
using VerifyXunit;
using Xunit;

namespace audit.unitTests;

[UsesVerify]
public class DiffTest
{
    [Fact]
    public Task Diff_Get_Should_Return_Right_JsonDiff()
    {
        // arrange
        var oldObject = "{\"name\": \"diyaz\"}";
        var newObject = "{\"name\": \"diyaz yakubov\"}";
        
        // act
        var diff = Diff.Get(oldObject, newObject);

        // assert
        Assert.NotNull(diff);
        return Verifier.VerifyJson(diff);
    }

    [Fact]
    public Task Diff_Unpatch_Should_Return_OlderObject_By_CurrentObject_and_JsonDiff()
    {
        // arrange
        var diff = "{\"name\": [\"diyaz\",\"diyaz yakubov\"]}";
        var currentObject = "{\"name\": \"diyaz yakubov\"}";
        
        // act
        var olderObject = Diff.Unpatch(currentObject, diff);

        // assert
        Assert.NotNull(olderObject);
        return Verifier.VerifyJson(olderObject);
    }

    [Fact]
    public Task Diff_Patch_Should_Return_NewerObject_By_OldObject_and_JsonDiff()
    {
        // arrange
        var diff = "{\"name\": [\"diyaz\",\"diyaz yakubov\"]}";
        var oldObject = "{\"name\": \"diyaz\"}";
        
        // act
        var newerObject = Diff.Patch(oldObject, diff);

        // assert
        Assert.NotNull(newerObject);
        return Verifier.VerifyJson(newerObject);
    }
}