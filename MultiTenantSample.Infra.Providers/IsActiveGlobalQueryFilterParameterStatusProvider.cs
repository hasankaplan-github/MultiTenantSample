using Haskap.DddBase.Utilities;
using MultiTenantSample.Domain.Providers;

namespace MultiTenantSample.Infra.Providers;
public class IsActiveGlobalQueryFilterParameterStatusProvider : IIsActiveGlobalQueryFilterParameterStatusProvider
{
    public bool IsEnabled { get; private set; } = true;

    private IDisposable ChangeIsActiveFilterStatus(bool isEnabled)
    {
        var oldStatus = IsEnabled;
        IsEnabled = isEnabled;
        return new DisposeAction(() =>
        {
            IsEnabled = oldStatus;
        });
    }

    public IDisposable DisableFilterParameter()
    {
        return ChangeIsActiveFilterStatus(false);
    }

    public IDisposable EnableFilterParameter()
    {
        return ChangeIsActiveFilterStatus(true);
    }
}
