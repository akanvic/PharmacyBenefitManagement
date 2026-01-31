namespace PharmacyBenefitManagement.Exceptions
{

    public class MemberNotFoundException : Exception
    {
        public string MemberId { get; }

        public MemberNotFoundException(string memberId)
            : base($"Member with ID '{memberId}' does not exist.")
        {
            MemberId = memberId;
        }

        public MemberNotFoundException(string memberId, string message)
            : base(message)
        {
            MemberId = memberId;
        }
    }


    public class ValidationException : Exception
    {
        public Dictionary<string, string[]> Errors { get; }

        public ValidationException(string message)
            : base(message)
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(Dictionary<string, string[]> errors)
            : base("One or more validation errors occurred.")
        {
            Errors = errors;
        }
    }

    public class ExternalServiceException : Exception
    {
        public string ServiceName { get; }

        public ExternalServiceException(string serviceName, string message)
            : base(message)
        {
            ServiceName = serviceName;
        }

        public ExternalServiceException(string serviceName, string message, Exception innerException)
            : base(message, innerException)
        {
            ServiceName = serviceName;
        }
    }
}
