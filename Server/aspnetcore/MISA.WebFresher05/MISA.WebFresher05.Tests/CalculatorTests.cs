using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        /// <summary>
        /// Test tổng 2 số nguyên
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="expectedResult"></param>
        /// Created By: vtahoang (13/07/2023)
        [TestCase(1, 2, 3)]
        [TestCase(3, 2, 5)]
        [TestCase(int.MaxValue, 1, int.MaxValue + (long)1)]
        public void Add_ValidInput_ValidResult(int x, int y, long expectedResult)
        {
            // Act
            var actualResult = new Calculator().Add(x, y);

            // Assert 
            Assert.That(actualResult, Is.EqualTo(expectedResult));

        }

        /// <summary>
        /// Test với đầu vào là chuỗi không hợp lệ
        /// </summary>
        /// Created By: vtahoang (13/07/2023)
        [Test]
        public void Add_InvalidInput_ReturnsException()
        {
            // Arrange
            var input = "a,b,1,2";

            var expectedExceptionMessage = "Chuỗi truyền vào không hợp lệ";

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => new Calculator().Add(input));
            Assert.That(exception.Message, Is.EqualTo(expectedExceptionMessage));
        }

        /// <summary>
        /// Test với đầu vào là chuỗi hợp lệ
        /// </summary>
        /// <param name="input"></param>
        /// <param name="expectedResult"></param>
        /// Created By: vtahoang (13/07/2023)
        [TestCase("", 0)]
        [TestCase("1", 1)]
        [TestCase("1,2,3", 6)]
        [TestCase("1,2, 3", 6)]
        public void Add_ValidInput_ValidResult(string input, long expectedResult)
        {
            // Act
            var actualResult = new Calculator().Add(input);

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        /// <summary>
        /// Test với đầu vào là chuỗi rỗng
        /// </summary>
        /// <param name="input"></param>
        /// <param name="expectedResult"></param>
        /// Created By: vtahoang (13/07/2023)
        [Test]
        public void Add_NullStringInput_ValidResult()
        {
            // Arrange 
            var input = "";

            var expectedResult = 0;

            // Act
            var actualResult = new Calculator().Add(input);

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        /// <summary>
        /// Test với đầu vào là chuỗi có số âm
        /// </summary>
        /// <param name="input"></param>
        /// <param name="expectedResult"></param>
        /// Created By: vtahoang (13/07/2023)
        [Test]
        public void Add_NegativeInput_ValidResult()
        {
            // Arrange
            var input = "1,-2,  -3";

            var expectedExceptionMessage = "Không chấp nhận toán tử âm: -2, -3";

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => new Calculator().Add(input));
            Assert.That(exception.Message, Is.EqualTo(expectedExceptionMessage));
        }

        /// <summary>
        /// Test hiệu 2 số nguyên
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="expectedResult"></param>
        /// Created By: vtahoang (13/07/2023)
        [TestCase(1, 2, -1)]
        [TestCase(3, 2, 1)]
        [TestCase(int.MaxValue, int.MinValue, int.MaxValue - (long)int.MinValue)]
        public void Sub_ValidInput_ValidResult(int x, int y, long expectedResult)
        {
            // Act
            var actualResult = new Calculator().Sub(x, y);

            // Assert 
            Assert.That(actualResult, Is.EqualTo(expectedResult));

        }

        /// <summary>
        /// Test tích 2 số nguyên
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="expectedResult"></param>
        /// Created By: vtahoang (13/07/2023)
        [TestCase(1, 2, 2)]
        [TestCase(5, 10, 50)]
        [TestCase(int.MaxValue, int.MinValue, int.MaxValue * (long)int.MinValue)]
        public void Mul_ValidInput_ValidResult(int x, int y, long expectedResult)
        {
            // Act
            var actualResult = new Calculator().Mul(x, y);

            // Assert 
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        /// <summary>
        /// Test chia cho 0
        /// </summary>
        /// Created By: vtahoang (13/07/2023)
        [Test]
        public void Div_DivideByZero_ReturnsException()
        {
            // Arrange
            var x = 1;
            var y = 0;

            var expectedExceptionMessage = "Không chia được cho 0";

            // Act & Assert 
            var exception = Assert.Throws<Exception>(() => new Calculator().Div(x, y));
            Assert.That(exception.Message, Is.EqualTo(expectedExceptionMessage));
        }

        /// <summary>
        /// Test thương 2 số nguyên 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="expectedResult"></param>
        /// Created By: vtahoang (13/07/2023)
        [TestCase(1, 2, 0.5)]
        [TestCase(2, 3, 0.666666)]
        public void Div_ValidInput_ValidResult(int x, int y, double expectedResult)
        {
            // Act
            var actualResult = new Calculator().Div(x, y);

            // Assert
            Assert.That(Math.Abs(actualResult - expectedResult), Is.LessThan(1e-4));

        }

    }
}