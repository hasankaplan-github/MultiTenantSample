using Haskap.DddBase.Domain;

namespace MultiTenantSample.Domain;
public class SomeTenantDataClass : AggregateRoot, IHasMultiTenant, ISoftDeletable, IIsActive
{
    public int MyProperty { get; private set; }
	public Guid? TenantId { get; set; }
	public bool IsDeleted { get; set; }
    public bool IsActive { get; set; }

    private SomeTenantDataClass()
	{
	}

	public SomeTenantDataClass(Guid id, int myProperty, Guid? tenantId)
		: base(id)
	{
		MyProperty = myProperty;
		TenantId = tenantId;
	}
}
