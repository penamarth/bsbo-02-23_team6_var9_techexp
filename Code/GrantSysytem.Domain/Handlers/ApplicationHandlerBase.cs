namespace GrantSysytem.Domain.Handlers
{
    public abstract class ApplicationHandlerBase : IApplicationHandler
    {
        protected IApplicationHandler _next;

        public void SetNext(IApplicationHandler next)
        {
            _next = next;
        }

        public virtual GrantApplication Handle(GrantApplication application)
        {
            return _next?.Handle(application) ?? application;
        }
    }
}