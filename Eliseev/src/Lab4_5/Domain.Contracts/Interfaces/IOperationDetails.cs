using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Interfaces
{
    public interface IOperationDetails
    {
        bool Succeded { get; }
        string Property { get; }
        string Message { get; }
    }
}
