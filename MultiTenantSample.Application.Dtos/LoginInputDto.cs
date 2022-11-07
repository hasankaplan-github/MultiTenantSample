using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantSample.Application.Dtos;
public class LoginInputDto
{
    public string Username { get; set; }
    public string Password { get; set; }
    public Guid? TenantId { get; set; }
}
