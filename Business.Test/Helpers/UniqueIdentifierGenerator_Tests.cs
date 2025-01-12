using Business.Helpers;
using Domain.Factories;

namespace Business.Test.Helpers;
public class UniqueIdentifierGenerator_Tests
{
    private readonly IUniqueIdentifierGenerator IUniqueIdentifierGenerator;

    public UniqueIdentifierGenerator_Tests()
    {
        IUniqueIdentifierGenerator = new UniqueIdentifierGenerator();
    }

    [Fact]
    public void Generate_ShouldReturnAGuid_WhenGeneratedIdIsParsedToGUID()
    {
        //Arrange
        string id = IUniqueIdentifierGenerator.Generate();

        //Act
        var result = Guid.Parse(id);

        //Assert
        Assert.NotNull(id);
        Assert.IsType<string>(id);
        Assert.IsType<Guid>(result);
    }
}
