using System;
using System.Collections.Generic;
using System.Text;

namespace TEST.AMRCloud.Services.Scoping.Domain.Common
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }

}
