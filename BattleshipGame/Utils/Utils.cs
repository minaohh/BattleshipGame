using System;

namespace Battleship
{
    public class Utils
    {
        public static bool IsHorizontalPosition()
        {
            Random rand = new Random();
            int num = rand.Next(0, 2);

            return num == 0;
        }

        public static int GetRandomNum(int min, int max)
        {
            Random rand = new Random();
            int num = rand.Next(min, max);

            return num;
        }

        #region Input validations

        /// <summary>
        /// Checks if the user input is a valid game option
        /// </summary>
        /// <param name="input">User console input</param>
        /// <returns>True if it's a letter, false otherwise</returns>
        public static bool ValidGameOptionInput(string input)
        {
            return !string.IsNullOrEmpty(input) && char.TryParse(input, out _);
        }

        /// <summary>
        /// Parses input and checks if the attack code is valid
        /// If invalid, returns \0 and -1
        /// </summary>
        /// <param name="input">User console input</param>
        /// <param name="colOutput">Letter from A to J</param>
        /// <param name="rowOutput">Number from 1 to 10</param>
        /// <returns>True if the input is valid and can be parsed, false otherwise</returns>
        public static bool ValidAttackInput(string input, out char colOutput, out int rowOutput)
        {
            input = input.Trim().ToUpper();
            colOutput = '\0';
            rowOutput = -1;

            // Max characters should be 3, e.g. J10
            if (string.IsNullOrEmpty(input) || input.Length > 3)
            {
                return false;
            }

            // Parse input
            char colData = input[0];
            string rowData = input[1..];

            // column: only accept letters A - J
            if (colData < 'A' || colData > 'J')
            {
                return false;
            }

            // row: only accept numbers from 1 - 10
            bool isNumeric = int.TryParse(rowData, out int rowNum);
            if (!isNumeric || rowNum < 1 || rowNum > 10)
            {
                return false;
            }

            // All test passed
            colOutput = colData;
            rowOutput = rowNum;

            return true;
        }

        #endregion

    }

    public enum ShipTypes
    {
        Carrier,
        Battleship,
        Cruiser,
        Destroyer,
        Submarine
    }

}
