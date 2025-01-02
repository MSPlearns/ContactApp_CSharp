using DataManagement.Services;
namespace DataManagement.Test.Services
{
    public class DataService_Test
    {
        private DataService _dataService = new ("DataTest", "TestFile.json");
        [Fact]
        public void SaveAndLoadList_ShouldSaveListToFile_Wehnwhatever() {
            //Arrange 
            List<string> testList = new () { "Test1", "Test2", "Test3" };
            //Act
            _dataService.SaveListToFile(testList);

            var result = _dataService.LoadListFromFile<string>();
            //Assert
            Assert.True(File.Exists("DataTest/TestFile.json"));
            Assert.True(testList.SequenceEqual(result));
        }

    }
}
