using Business.Helpers;

namespace Business.Test.Helpers
{
    public class UniqueIdentifierGenerator_Tests
    {
        [Fact]
        public void Generate_ShouldReturnAGuid_WhenGeneratedIdIsParsedToGUID()
        {
            //Arrange
            string id = UniqueIdentifierGenerator.Generate();

            //Act
            var result = Guid.Parse(id);

            //Assert
            Assert.NotNull(id);
            Assert.IsType<string>(id);
            Assert.IsType<Guid>(result);
        }
    }
}
