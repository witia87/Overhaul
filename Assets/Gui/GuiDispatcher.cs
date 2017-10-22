using System;
using System.Collections.Generic;
using System.Diagnostics;
using Assets.Gui.Payloads;
using UnityEngine;

namespace Assets.Gui
{
    public delegate void ActionCallback(IGuiPayload payload);

    public class GuiDispatcher : MonoBehaviour
    {
        private readonly List<AggregatedCommand> _aggregatedCommands = new List<AggregatedCommand>();
        private readonly Dictionary<GuiCommandIds, List<ActionCallback>> _callbacks;

        private bool _isDispatching;

        public GuiDispatcher()
        {
            _callbacks = new Dictionary<GuiCommandIds, List<ActionCallback>>();
        }

        public void Register(GuiCommandIds commandId, ActionCallback callback)
        {
            if (!_callbacks.ContainsKey(commandId))
            {
                _callbacks.Add(commandId, new List<ActionCallback>());
            }
            _callbacks[commandId].Add(callback);
        }

        public void Dispatch(GuiCommandIds commandId, IGuiPayload payload)
        {
            _aggregatedCommands.Add(new AggregatedCommand {commandId = commandId, payload = payload});
        }

        public void Update()
        {
            if (_isDispatching)
            {
                throw new ApplicationException("Dispatcher is already dispatching. Forbidden chain of actions.");
            }
            _isDispatching = true;
            var watch = new Stopwatch();
            watch.Start();
            foreach (var command in _aggregatedCommands)
            {
                foreach (var callback in _callbacks[command.commandId])
                {
                    callback(command.payload);
                }
            }
            _aggregatedCommands.Clear();
            _isDispatching = false;
        }

        private struct AggregatedCommand
        {
            public GuiCommandIds commandId;
            public IGuiPayload payload;
        }
    }
}