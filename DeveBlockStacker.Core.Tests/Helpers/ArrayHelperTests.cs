using DeveBlockStacker.Core.Helpers;
using Xunit;

namespace DeveBlockStacker.Core.Tests.Helpers
{
    public class ArrayHelperTests
    {
        [Fact]
        public void ArrayHelperReturnsCorrectWidth()
        {
            //Arrange
            var array = new bool[5, 10];

            //Act
            var width = ArrayHelper.Width(array);

            //Assert
            Assert.Equal(5, width);
        }

        [Fact]
        public void ArrayHelperReturnsCorrectHeight()
        {
            //Arrange
            var array = new bool[5, 10];

            //Act
            var height = ArrayHelper.Height(array);

            //Assert
            Assert.Equal(10, height);
        }
    }
}
