using Domain.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class OperationDetails : IOperationDetails
    {
        

        public OperationDetails(bool succeded, string message, string property)
        {
            this.Succeded = succeded;
            this.Message = message;
            this.Property = property;
        }

        public bool Succeded
        {
            get;
            private set;
        }

        public string Property
        {
            get;
            private set;
        }

        public string Message
        {
            get;
            private set;
        }
    }
}
