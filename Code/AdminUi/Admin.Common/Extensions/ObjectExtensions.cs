namespace Common.Extensions
{
    using System;
    using System.Net.Http;

    using Common.Events;

    using EnergyTrading.Mdm.Client.WebClient;

    using Microsoft.Practices.Prism.Events;

    public static class ObjectExtensions
    {
        public static TR Try<TR>(this object o, Func<TR> func, IEventAggregator eventAggregator) where TR : class
        {
            try
            {
                return func.Invoke();
            }
            catch (MdmFaultException e)
            {
                eventAggregator.Publish(new ErrorEvent(e.Fault));
            }
            catch (HttpRequestException e)
            {
                eventAggregator.Publish(new ErrorEvent(e.Message));
            }

            return null;
        }
    }
}