using MyDroneService.Models;
using MyDroneService.Services;

namespace MyDroneServiceTests
{
    public class PackageParserServiceTest
    {
        [Theory, MemberData(nameof(packagesData))]
        public void Parse_WhenCalledWithAnInput_ShouldReturnAIEnumerableWithAllPackages(IEnumerable<string> line, int packagesExpected)
        {
            // Arrange
            PackageParserService packageParserService = new PackageParserService();

            // Act
            IEnumerable<Packages> packages = packageParserService.Parse(line);

            // Assert
            Assert.Equal(packagesExpected, packages.Count());
        }

        public static IEnumerable<object[]> packagesData => 
        new List<object[]>
        {
            new object[] { new List<string> { "[A], [1]" }, 1 },
            new object[] { new List<string> { "[A], [1]", "[B], [2]" }, 2 },
            new object[] { new List<string> { "[A], [1]", "[B], [2]", "[C], [3]" }, 3 },
            new object[] { new List<string> { "[A], [1]", "[B], [2]", "[C], [3]", "[D], [4]" }, 4 },
            new object[] { new List<string> { "[A], [1]", "[B], [2]", "[C], [3]", "[D], [4]", "[E], [5]" }, 5 }
        };
    }
}
