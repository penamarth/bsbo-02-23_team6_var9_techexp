namespace GrantSysytem.Domain.Handlers
{
    public interface IApplicationHandler
    {
        void SetNext(IApplicationHandler next);
        GrantApplication Handle(GrantApplication application);
    }
}