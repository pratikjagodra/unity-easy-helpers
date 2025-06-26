
namespace EasyHelpers.Runtime.Tools.EventService
{
    public interface IEventListener { }

    public interface IEventListener<T> : IEventListener where T : EventBase
    {
        public void OnTrigger(T eventData);
    }
}
