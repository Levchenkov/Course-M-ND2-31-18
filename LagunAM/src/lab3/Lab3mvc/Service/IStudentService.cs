using Lab3mvc.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab3mvc.Service
{
    public interface IStudentService
    {
        string MakeSmthWithRepository();
        string Add();
    }
}