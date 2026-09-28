namespace BoilerSystemController.Domain.Model
{
    /// <summary>
    /// Result object - used to send the result of an operation
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Constructor that initializes Result object
        /// </summary>
        /// <param name="isSuccess">Denotes whether an operation is success or not</param>
        /// <param name="message">Message</param>
        public Result(bool isSuccess, string message)
        {
            this.IsSuccess = isSuccess;
            this.Message = message;
        }

        /// <summary>
        /// Gets or sets the value of IsSuccess
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Gets or sets the value of the message
        /// </summary>
        public string Message { get; set; }
    }
}
