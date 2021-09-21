using Microsoft.VisualStudio.TestTools.UnitTesting;
using Battleship;

namespace BattleshipTests
{
    [TestClass]
    public class UtilitiesTests
    {
        #region Valid Attack Code Input

        [TestMethod]
        public void ValidAttackInput_NullOrEmpty_ExpectFalse()
        {
            // Arrange
            string input = "";
            char expectedColOutput = '\0';
            int expectedRowOutput = -1;
            bool expected = false;

            // Act
            var result = Utils.ValidAttackInput(input, out char colOutput, out int rowOutput);

            // Assert
            Assert.AreEqual(expected, result, "Null or empty is invalid");
            Assert.AreEqual(expectedColOutput, colOutput);
            Assert.AreEqual(expectedRowOutput, rowOutput);
        }

        [TestMethod]
        public void ValidAttackInput_LengthOver3_ExpectFalse()
        {
            // Arrange
            string input = "Z234";
            char expectedColOutput = '\0';
            int expectedRowOutput = -1;
            bool expected = false;

            // Act
            var result = Utils.ValidAttackInput(input, out char colOutput, out int rowOutput);

            // Assert
            Assert.AreEqual(expected, result, "Valid input length is 1-3");
            Assert.AreEqual(expectedColOutput, colOutput);
            Assert.AreEqual(expectedRowOutput, rowOutput);
        }

        [TestMethod]
        public void ValidAttackInput_SpecialCharacters_ExpectFalse()
        {
            // Arrange
            string input = "   (*)*(&*(./";
            char expectedColOutput = '\0';
            int expectedRowOutput = -1;
            bool expected = false;

            // Act
            var result = Utils.ValidAttackInput(input, out char colOutput, out int rowOutput);

            // Assert
            Assert.AreEqual(expected, result, "Special characters are invalid");
            Assert.AreEqual(expectedColOutput, colOutput);
            Assert.AreEqual(expectedRowOutput, rowOutput);
        }

        [TestMethod]
        public void ValidAttackInput_LetterInputNotBetweenAToJ_ExpectFalse()
        {
            // Arrange
            string input = "X5";
            char expectedColOutput = '\0';
            int expectedRowOutput = -1;
            bool expected = false;

            // Act
            var result = Utils.ValidAttackInput(input, out char colOutput, out int rowOutput);

            // Assert
            Assert.AreEqual(expected, result, "Valid letters are A-F");
            Assert.AreEqual(expectedColOutput, colOutput);
            Assert.AreEqual(expectedRowOutput, rowOutput);
        }

        [TestMethod]
        public void ValidAttackInput_LetterAndNegativeNumber_ExpectFalse()
        {
            // Arrange
            string input = "C-2";
            char expectedColOutput = '\0';
            int expectedRowOutput = -1;
            bool expected = false;

            // Act
            var result = Utils.ValidAttackInput(input, out char colOutput, out int rowOutput);

            // Assert
            Assert.AreEqual(expected, result, "Negative numbers are invalid");
            Assert.AreEqual(expectedColOutput, colOutput);
            Assert.AreEqual(expectedRowOutput, rowOutput);
        }

        [TestMethod]
        public void ValidAttackInput_LetterAndNumOver10_ExpectFalse()
        {
            // Arrange
            string input = "a22";
            char expectedColOutput = '\0';
            int expectedRowOutput = -1;
            bool expected = false;

            // Act
            var result = Utils.ValidAttackInput(input, out char colOutput, out int rowOutput);

            // Assert
            Assert.AreEqual(expected, result, "Valid numbers are 1-10");
            Assert.AreEqual(expectedColOutput, colOutput);
            Assert.AreEqual(expectedRowOutput, rowOutput);
        }

        [TestMethod]
        public void ValidAttackInput_LetterAToJAndNum1To10_ExpectTrue()
        {
            // input
            string input = "I7";
            char expectedColOutput = 'I';
            int expectedRowOutput = 7;
            bool expected = true;

            // Act
            var result = Utils.ValidAttackInput(input, out char colOutput, out int rowOutput);

            // Assert
            Assert.AreEqual(expected, result, "A-J and 1-10 combination is valid");
            Assert.AreEqual(expectedColOutput, colOutput);
            Assert.AreEqual(expectedRowOutput, rowOutput);
        }

        #endregion

        #region Valid Game Option Input

        [TestMethod]
        public void ValidGameOptionInput_EmptyInput_ExpectFalse()
        {
            // Arrange
            string input = "";
            bool expected = false;

            // Act
            var result = Utils.ValidGameOptionInput(input);

            // Assert
            Assert.AreEqual(expected, result, "Null or empty is invalid");
        }

        [TestMethod]
        public void ValidGameOptionInput_Whitespaces_ExpectFalse()
        {
            // Arrange
            string input = "       ";
            bool expected = false;

            // Act
            var result = Utils.ValidGameOptionInput(input);

            // Assert
            Assert.AreEqual(expected, result, "Whitespaces are invalid");
        }

        [TestMethod]
        public void ValidGameOptionInput_SpecialCharacters_ExpectFalse()
        {
            // Arrange
            string input = "&!@;/";
            bool expected = false;

            // Act
            var result = Utils.ValidGameOptionInput(input);

            // Assert
            Assert.AreEqual(expected, result, "Special characters are invalid");
        }

        [TestMethod]
        public void ValidGameOptionInput_Numbers_ExpectFalse()
        {
            // Arrange
            string input = "7384279";
            bool expected = false;

            // Act
            var result = Utils.ValidGameOptionInput(input);

            // Assert
            Assert.AreEqual(expected, result, "Numbers are invalid");
        }

        [TestMethod]
        public void ValidGameOptionInput_Words_ExpectFalse()
        {
            // Arrange
            string input = "input";
            bool expected = false;

            // Act
            var result = Utils.ValidGameOptionInput(input);

            // Assert
            Assert.AreEqual(expected, result, "More than one character is invalid");
        }

        [TestMethod]
        public void ValidGameOptionInput_Letter_ExpectTrue()
        {
            // Arrange
            string input = "B";
            bool expected = true;

            // Act
            var result = Utils.ValidGameOptionInput(input);

            // Assert
            Assert.AreEqual(expected, result, "One character is invalid");
        }

        #endregion

    }
}
