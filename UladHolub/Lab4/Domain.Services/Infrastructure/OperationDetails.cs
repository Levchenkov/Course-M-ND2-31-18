using Domain.Contracts.Interfaces;

namespace Domain.Services.Infrastructure
{
    public class OperationDetails : IOperationDetails
    {
        public OperationDetails(bool succedeed, string message, string prop)
        {
            Succeeded = succedeed;
            Message = message;
            Property = prop;
        }
        public bool Succeeded { get; private set; }
        public string Message { get; private set; }
        public string Property { get; private set; }
    }
}
