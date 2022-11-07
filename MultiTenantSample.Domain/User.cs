using Haskap.DddBase.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantSample.Domain;
public class User : AggregateRoot, IMultiTenant
{
	public string Username { get; set; }
    public string Password { get; set; }
    public Guid? TenantId { get; set; }

	private User()
	{

	}

	public User(Guid id)
		: base(id)
	{
	}
}
