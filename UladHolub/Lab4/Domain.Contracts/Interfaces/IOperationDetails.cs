using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Interfaces
{
    public interface IOperationDetails
    {
        string Message { get; }
        string Property { get; }
        bool Succeeded { get; }
    }
}
