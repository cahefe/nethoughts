using System;
using System.Runtime.Serialization;

namespace AppStruct.Domain.Common.Models.Exceptions
{
    public class AppException : Exception
    {
        public readonly EnumExceptionReason ExceptionReason;
        public AppException(EnumExceptionReason exceptionReason) => ExceptionReason = exceptionReason;

        public AppException(EnumExceptionReason exceptionReason, string message) : base(message) => ExceptionReason = exceptionReason;

        public AppException(EnumExceptionReason exceptionReason, string message, Exception innerException) : base(message, innerException) => ExceptionReason = exceptionReason;

        protected AppException(EnumExceptionReason exceptionReason, SerializationInfo info, StreamingContext context) : base(info, context) => ExceptionReason = exceptionReason;
    }
}
