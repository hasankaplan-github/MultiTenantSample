using Haskap.DddBase.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace MultiTenantSample.Domain;
public class UserNotFoundException : DomainException
{
    public UserNotFoundException()
        : base("__L__UserNotFound__L__", HttpStatusCode.BadRequest)
    {

    }
}
