using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.Base;

public  class NotFoundException
{
    public NotFoundException(string errorString) => throw new Exception(errorString);
}
