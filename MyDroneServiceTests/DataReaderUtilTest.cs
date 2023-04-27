using MyDroneService.Services;

namespace MyDroneServiceTests
{
    public class DataReaderUtilTest
    {
        [Theory]
        [InlineData("[100]", 100)]
        [InlineData("[90]", 90)]
        [InlineData("[50]", 50)]
        [InlineData("[150]", 150)]
        [InlineData("[300]", 300)]
        public void WeightParser_WhenCallWithNumberWrappedWithBrackets_ShouldReturnAnInteger(string input, int expectedResult)
        {
            // Arrange
            DataReaderUtil dataReaderUtil = new DataReaderUtil();

            // Act
            int result = dataReaderUtil.WeightParser(input);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void WeightParser_WhenCallWithNull_ShouldThrowAnNullReferenceException()
        {
            // Arrange
            DataReaderUtil dataReaderUtil = new DataReaderUtil();

            // Assert
            Assert.Throws<NullReferenceException>(() => dataReaderUtil.WeightParser(null));
        }
        
        [Fact]
        public void WeightParser_WhenCallWithIncorrectFormat_ShouldThrowAnFormatException()
        {
            // Arrange
            DataReaderUtil dataReaderUtil = new DataReaderUtil();

            // Assert
            Assert.Throws<FormatException>(() => dataReaderUtil.WeightParser("[12.3]"));
        }

        [Fact]
        public void WeightParser_WhenCallWithLargeInteger_ShouldThrowAnOverflowException()
        {
            // Arrange
            DataReaderUtil dataReaderUtil = new DataReaderUtil();

            // Assert
            Assert.Throws<OverflowException>(() => dataReaderUtil.WeightParser("[2147483648]"));
        }
    }
}