namespace BoilerSystemController.PresentationLayer.Helper
{
    /// <summary>
    /// Helper class that contains methods to validate user input
    /// </summary>
    public static class InputValidator
    {
        /// <summary>
        /// Checks whether a given input is a valid integer
        /// </summary>
        /// <param name="input">User input</param>
        /// <param name="number">Parsed Integer</param>
        /// <returns>Boolean value representing whether an integer is valid or not</returns>
        public static bool ValidateInteger(string input, out int number)
        {
            return int.TryParse(input, out number);
        }
    }
}
