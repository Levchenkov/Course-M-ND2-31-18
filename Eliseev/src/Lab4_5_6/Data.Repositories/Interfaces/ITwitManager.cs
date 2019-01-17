using Data.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface ITwitManager: IDisposable
    {
        void Create(Twit twit);
    }
}
