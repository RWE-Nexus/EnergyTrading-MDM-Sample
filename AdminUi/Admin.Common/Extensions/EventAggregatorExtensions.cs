namespace Common.Extensions
{
    using System;

    using Common.EventAggregator;

    using Microsoft.Practices.Prism.Events;

    public static class EventAggregatorExtensions
    {
        private static IEventAggregatorExtensionsProvider _provider = new EventAggregatorExtensionsProvider();

        // used for testing
        public static void Publish<TEvent>(this IEventAggregator eventAggregator, TEvent publishEvent)
        {
            _provider.Publish(eventAggregator, publishEvent);
        }

        public static void SetProvider(IEventAggregatorExtensionsProvider provider)
        {
            _provider = provider;
        }

        public static SubscriptionToken Subscribe<TEvent>(
            this IEventAggregator eventAggregator, 
            Action<TEvent> subscription)
        {
            return _provider.Subscribe(eventAggregator, subscription);
        }

        public static SubscriptionToken Subscribe<TEvent>(
            this IEventAggregator eventAggregator, 
            Action<TEvent> subscription, 
            bool keepSubscriberReferenceAlive)
        {
            return _provider.Subscribe(eventAggregator, subscription, keepSubscriberReferenceAlive);
        }

        public static SubscriptionToken Subscribe<TEvent>(
            this IEventAggregator eventAggregator, 
            Action<TEvent> subscription, 
            ThreadOption threadOption, 
            bool keepSubscriberReferenceAlive = false, 
            Predicate<TEvent> filter = null)
        {
            return _provider.Subscribe(
                eventAggregator, 
                subscription, 
                threadOption, 
                keepSubscriberReferenceAlive, 
                filter);
        }

        public static void Unsubscribe<TEvent>(this IEventAggregator eventAggregator, SubscriptionToken token)
        {
            _provider.Unsubscribe<TEvent>(eventAggregator, token);
        }

        public static void Unsubscribe<TEvent>(this IEventAggregator eventAggregator, Action<TEvent> subscription)
        {
            _provider.Unsubscribe(eventAggregator, subscription);
        }
    }
}