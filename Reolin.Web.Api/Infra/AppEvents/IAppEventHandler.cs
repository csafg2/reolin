namespace Reolin.Web.Api.Infra.AppEvents
{
    public interface IAppEventHandler
    {
        void Execute();
        AppEventType EventType { get;  }
    }
}