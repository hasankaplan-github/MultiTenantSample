using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantSample.Application.Dtos;
public class LoginOutputDto
{
    public Guid UserId { get; set; }
    public Guid? TenantId { get; set; }
}
