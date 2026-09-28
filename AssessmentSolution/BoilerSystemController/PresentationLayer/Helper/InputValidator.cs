namespace BoilerSystemController.PresentationLayer.Helper
{
    public static class InputValidator
    {
        public static bool ValidateInteger(string input, out int number)
        {
            return int.TryParse(input, out number);
        }
    }
}
