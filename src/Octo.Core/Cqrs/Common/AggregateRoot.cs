using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace Octo.Core.Cqrs.Common
{
    public abstract class AggregateRoot : IAggregateRoot
    {
        private readonly Queue<ICommittableEvent> _uncommittedEvents = new Queue<ICommittableEvent>();

        private long _lastEventSequence;

        public Guid Id { get; protected set; }

        public ReadOnlyCollection<ICommittableEvent> UncommittedEvents
        {
            get
            {
                return new ReadOnlyCollection<ICommittableEvent>(_uncommittedEvents.ToList()); 
            }
        }

        public void Apply(IEvent @event)
        {
            ApplyEventToInternalState(@event);
            @event.AggregateRootId = Id;

            var committableEvent = new CommittableEvent(@event,  ++_lastEventSequence, DateTime.UtcNow);
            _uncommittedEvents.Enqueue(committableEvent);
        }

        public void CommitEvents()
        {
            _uncommittedEvents.Clear();
        }

        public void LoadFromHistoricalEvents(IEnumerable<ICommittableEvent> committableEvents)
        {
            if (committableEvents == null) throw new ArgumentNullException("committableEvents");
            
            var orderedCommittableEvents = committableEvents.OrderBy(e => e.Sequence).ToList();
            if (!orderedCommittableEvents.Any()) return;

            orderedCommittableEvents.ForEach(ce => ApplyEventToInternalState(ce.Event));
            
            _lastEventSequence = orderedCommittableEvents.Last().Sequence;
        }

        private static bool EventHandlerMethodInfoHasCorrectParameter(MethodInfo eventHandlerMethodInfo, Type domainEventType)
        {
            var parameters = eventHandlerMethodInfo.GetParameters();
            return parameters.Length == 1 && parameters[0].ParameterType == domainEventType;
        }

        private static string GetEventHandlerMethodName(string domainEventTypeName)
        {
            var eventIndex = domainEventTypeName.LastIndexOf("Event", StringComparison.Ordinal);
            return "On" + domainEventTypeName.Remove(eventIndex, 5);
        }

        private void ApplyEventToInternalState(IEvent @event)
        {
            // todo 1341 improve code below, get rid of reflection
            // stolen from SimpleCqrs framework https://github.com/tyronegroves/SimpleCQRS

            var domainEventType = @event.GetType();
            var domainEventTypeName = domainEventType.Name;
            var aggregateRootType = GetType();

            var eventHandlerMethodName = GetEventHandlerMethodName(domainEventTypeName);
            var methodInfo = aggregateRootType.GetMethod(eventHandlerMethodName,
                BindingFlags.Instance | BindingFlags.Public |
                BindingFlags.NonPublic, null, new[] { domainEventType }, null);

            if (methodInfo != null && EventHandlerMethodInfoHasCorrectParameter(methodInfo, domainEventType))
            {
                methodInfo.Invoke(this, new object[] { @event });
            }
            else
            {
                throw new InvalidOperationException(string.Format("Method for {0} is not found!", domainEventTypeName));
            }
        }
    }
}