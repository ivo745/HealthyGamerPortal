using System.ComponentModel;

namespace HealthyGamerPortal.Common.Api
{
    public enum ErrorCode
    {
        /// <summary>
        /// Error indicating that a required parameter is missing a value.
        /// </summary>
        [Description("The following parameter is misisng a value: '{0}'")]
        ParameterMissing = 1000,
        /// <summary>
        /// Error indicating that overall validation has failed for a request.
        /// </summary>
        [Description("One or more parameters have failed to validate. Check the Result property for more details.")]
        ValidationFailed = 1001,
        /// <summary>
        /// Error indicating that an encrypted string could not be decrypted successfully.
        /// </summary>
        [Description("Failed to decrypt the provided encrypted message, Was it signed with the correct public key obtained from: '/api/v{x}/Authentication/key'")]
        DecryptionFailed = 2000,
        /// <summary>
        /// Error indicating that an expected Base64 string was not in the correct Base64 format.
        /// </summary>
        [Description("The provided string was not in a valid Base64 format and could not be decoded.")]
        BadFormatBase64 = 2001,
        /// <summary>
        /// Error indicating that some unhandled exception occurred during a request.
        /// </summary>
        [Description("An unexpected error has occurred during the request. This has been logged and the developers will be notified. Please try again later!")]
        UnexpectedException = 9999,

        /// <summary>
        /// A non-null argument that is passed to a method is invalid.
        /// </summary>
        [Description("A non-null argument that is passed to a method is invalid.")]
        ArgumentException = 9000,
        /// <summary>
        /// An argument that is passed to a method is null.
        /// </summary>
        [Description("An argument that is passed to a method is null.")]
        ArgumentNullException = 9050,
        /// <summary>
        /// An argument is outside the range of valid values.
        /// </summary>
        [Description("An argument is outside the range of valid values.")]
        ArgumentOutOfRangeException = 9100,
        /// <summary>
        /// Part of a directory path is not valid.
        /// </summary>
        [Description("Part of a directory path is not valid.")]
        DirectoryNotFoundException = 9150,
        /// <summary>
        /// The denominator in an integer or Decimal division operation is zero.
        /// </summary>
        [Description("The denominator in an integer or Decimal division operation is zero.")]
        DivideByZeroException = 9200,
        /// <summary>
        /// A drive is unavailable or does not exist.
        /// </summary>
        [Description("A drive is unavailable or does not exist.")]
        DriveNotFoundException = 9250,
        /// <summary>
        /// A file does not exist.
        /// </summary>
        [Description("A file does not exist.")]
        FileNotFoundException = 9300,
        /// <summary>
        /// A value is not in an appropriate format to be converted from a string by a conversion method such as Parse.
        /// </summary>
        [Description("A value is not in an appropriate format to be converted from a string by a conversion method such as Parse.")]
        FormatException = 9350,
        /// <summary>
        /// An index is outside the bounds of an array or collection.
        /// </summary>
        [Description("An index is outside the bounds of an array or collection.")]
        IndexOutOfRangeException = 9400,
        /// <summary>
        /// A method call is invalid in an object's current state.
        /// </summary>
        [Description("A method call is invalid in an object's current state.")]
        InvalidOperationException = 9450,
        /// <summary>
        /// The specified key for accessing a member in a collection cannot be found.
        /// </summary>
        [Description("The specified key for accessing a member in a collection cannot be found.")]
        KeyNotFoundException = 9500,
        /// <summary>
        /// A method or operation is not implemented.
        /// </summary>
        [Description("A method or operation is not implemented.")]
        NotImplementedException = 9550,
        /// <summary>
        /// A method or operation is not supported.
        /// </summary>
        [Description("A method or operation is not supported.")]
        NotSupportedException = 9600,
        /// <summary>
        /// An operation is performed on an object that has been disposed.
        /// </summary>
        [Description("An operation is performed on an object that has been disposed.")]
        ObjectDisposedException = 9650,
        /// <summary>
        /// An arithmetic, casting, or conversion operation results in an overflow.
        /// </summary>
        [Description("An arithmetic, casting, or conversion operation results in an overflow.")]
        OverflowException = 9700,
        /// <summary>
        /// "A path or file name exceeds the maximum system-defined length.
        /// </summary>
        [Description("A path or file name exceeds the maximum system-defined length.")]
        PathTooLongException = 9750,
        /// <summary>
        /// The operation is not supported on the current platform.
        /// </summary>
        [Description("The operation is not supported on the current platform.")]
        PlatformNotSupportedException = 9800,
        /// <summary>
        /// An array with the wrong number of dimensions is passed to a method.
        /// </summary>
        [Description("An array with the wrong number of dimensions is passed to a method.")]
        RankException = 9850,
        /// <summary>
        /// The time interval allotted to an operation has expired.
        /// </summary>
        [Description("The time interval allotted to an operation has expired.")]
        TimeoutException = 9900,
        /// <summary>
        /// An invalid Uniform Resource Identifier (URI) is used.
        /// </summary>
        [Description("An invalid Uniform Resource Identifier (URI) is used.")]
        UriFormatException = 9950,


    }
}