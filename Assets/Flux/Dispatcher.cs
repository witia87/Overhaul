using System.Collections.Generic;

namespace Assets.Flux
{
    public class Dispatcher : IDispatcher
    {
        private readonly Dictionary<Commands, List<DispatchCallback>> _callbackLists;
        private bool _isDispatching;

        public Dispatcher()
        {
            _callbackLists = new Dictionary<Commands, List<DispatchCallback>>();
        }

        public void Dispatch(Commands commandId, IPayload payload)
        {
            if (_isDispatching)
            {
                throw new DispatcherException("Dispatcher is already dispatching. Forbidden chain of actions.");
            }

            _isDispatching = true;
            var callbacks = _callbackLists[commandId];
            foreach (var callback in callbacks)
            {
                callback(payload);
            }
            _isDispatching = false;
        }

        public void Register(Commands commandId, DispatchCallback callback)
        {
            if (!_callbackLists.ContainsKey(commandId))
            {
                _callbackLists.Add(commandId, new List<DispatchCallback>());
            }
            _callbackLists[commandId].Add(callback);
        }
    }
}