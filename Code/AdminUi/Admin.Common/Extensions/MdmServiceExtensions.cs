namespace Common.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Configuration;
    using System.Net;

    using Common.Events;

    using EnergyTrading;
    using EnergyTrading.Contracts.Search;
    using EnergyTrading.Mdm.Client.Services;
    using EnergyTrading.Mdm.Client.WebClient;
    using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.Search;

    using Microsoft.Practices.Prism.Events;

    public static class MdmServiceUiExtensions
    {
        public static void ExecuteAsync<T>(
            this IMdmService mdmService, 
            Func<WebResponse<T>> serviceCall, 
            Action<WebResponse<T>> success, 
            IEventAggregator eventAggregator)
        {
            var worker = new BackgroundWorker();

            worker.DoWork += (sender, args) =>
                {
                    eventAggregator.Publish(new BusyEvent(true));
                    WebResponse<T> response = serviceCall();
                    args.Result = response;
                };

            worker.RunWorkerCompleted += (o, eventArgs) =>
                {
                    var response = (WebResponse<T>)eventArgs.Result;

                    if (eventArgs.Error != null)
                    {
                        eventAggregator.Publish(new BusyEvent(false));
                        eventAggregator.Publish(new ErrorEvent(eventArgs.Error));
                        return;
                    }

                    if (!response.IsValid)
                    {
                        eventAggregator.Publish(new BusyEvent(false));
                        eventAggregator.Publish(new ErrorEvent(response.Fault));
                        return;
                    }

                    success(response);
                    eventAggregator.Publish(new BusyEvent(false));
                };

            worker.RunWorkerAsync();
        }

        public static void ExecuteAsync<T>(
            this IMdmService mdmService, 
            Func<WebResponse<T>> serviceCall, 
            Action success, 
            string statusUpate, 
            IEventAggregator eventAggregator)
        {
            var worker = new BackgroundWorker();

            worker.DoWork += (sender, args) =>
                {
                    WebResponse<T> response = serviceCall();
                    args.Result = response;
                };

            worker.RunWorkerCompleted += (o, eventArgs) =>
                {
                    var response = (WebResponse<T>)eventArgs.Result;

                    if (eventArgs.Error != null)
                    {
                        eventAggregator.Publish(new BusyEvent(false));
                        eventAggregator.Publish(new ErrorEvent(eventArgs.Error));
                        return;
                    }

                    if (!response.IsValid)
                    {
                        eventAggregator.Publish(new BusyEvent(false));
                        eventAggregator.Publish(new ErrorEvent(response.Fault));
                        return;
                    }

                    success();
                    eventAggregator.Publish(new BusyEvent(false));
                    eventAggregator.Publish(new StatusEvent(statusUpate));
                };

            worker.RunWorkerAsync();
        }

        public static void ExecuteAsyncRD(
            this IReferenceDataService mdmService, 
            Func<WebResponse<IList<ReferenceData>>> serviceCall, 
            Action success, 
            string statusUpate, 
            IEventAggregator eventAggregator)
        {
            var worker = new BackgroundWorker();

            worker.DoWork += (sender, args) =>
                {
                    WebResponse<IList<ReferenceData>> response = serviceCall();
                    args.Result = response;
                };

            worker.RunWorkerCompleted += (o, eventArgs) =>
                {
                    var response = (WebResponse<IList<ReferenceData>>)eventArgs.Result;

                    if (eventArgs.Error != null)
                    {
                        eventAggregator.Publish(new BusyEvent(false));
                        eventAggregator.Publish(new ErrorEvent(eventArgs.Error));
                        return;
                    }

                    if (!response.IsValid)
                    {
                        eventAggregator.Publish(new BusyEvent(false));
                        eventAggregator.Publish(new ErrorEvent(response.Fault));
                        return;
                    }

                    success();
                    eventAggregator.Publish(new BusyEvent(false));
                    eventAggregator.Publish(new StatusEvent(statusUpate));
                };

            worker.RunWorkerAsync();
        }

        public static void ExecuteAsyncSearch<T>(
            this IMdmService mdmService, 
            Search search, 
            Action<IList<T>> success, 
            IEventAggregator eventAggregator, 
            bool limited = true) where T : IMdmEntity
        {
            var worker = new BackgroundWorker();

            worker.DoWork += (sender, args) =>
                {
                    var start = SystemTime.UtcNow();
                    eventAggregator.Publish(new BusyEvent(true));
                    if (limited)
                    {
                        search.NotMultiPage()
                            .MaxPageSize(int.Parse(ConfigurationManager.AppSettings["maxSearchResults"]));
                    }
                    else
                    {
                        search.NotMultiPage().MaxPageSize(int.MaxValue);
                    }

                    WebResponse<IList<T>> response = mdmService.Search<T>(search);
                    var responseAndTime = new Tuple<WebResponse<IList<T>>, DateTime>(response, start);
                    args.Result = responseAndTime;
                };

            worker.RunWorkerCompleted += (o, eventArgs) =>
                {
                    if (eventArgs.Error != null)
                    {
                        eventAggregator.Publish(new BusyEvent(false));
                        eventAggregator.Publish(new ErrorEvent(eventArgs.Error));
                        return;
                    }

                    var responseAndTime = (Tuple<WebResponse<IList<T>>, DateTime>)eventArgs.Result;
                    var response = responseAndTime.Item1;
                    var duration = SystemTime.UtcNow().Subtract(responseAndTime.Item2);

                    if (response.Code == HttpStatusCode.InternalServerError)
                    {
                        eventAggregator.Publish(new BusyEvent(false));
                        eventAggregator.Publish(new ErrorEvent("MDM Service Internal Server Error"));
                        return;
                    }

                    if (!response.IsValid && response.Code != HttpStatusCode.NotFound)
                    {
                        eventAggregator.Publish(new BusyEvent(false));
                        eventAggregator.Publish(new ErrorEvent(response.Fault));
                        return;
                    }

                    success(response.Message);
                    eventAggregator.Publish(new SearchResultsFound(response.Message.Count));
                    eventAggregator.Publish(new BusyEvent(false));
                    eventAggregator.Publish(new StatusEvent("Query Execution: " + duration.TotalSeconds));
                };

            worker.RunWorkerAsync();
        }

        public static void ExecuteAsyncSearchRD(
            this IReferenceDataService mdmService, 
            Search search, 
            Action<ReferenceDataList> success, 
            IEventAggregator eventAggregator, 
            bool limited = true)
        {
            var worker = new BackgroundWorker();

            worker.DoWork += (sender, args) =>
                {
                    var start = SystemTime.UtcNow();
                    eventAggregator.Publish(new BusyEvent(true));
                    if (limited)
                    {
                        search.NotMultiPage()
                            .MaxPageSize(int.Parse(ConfigurationManager.AppSettings["maxSearchResults"]));
                    }
                    else
                    {
                        search.NotMultiPage().MaxPageSize(int.MaxValue);
                    }

                    // WebResponse<IList<ReferenceData>> response = mdmService.Search(search);
                    WebResponse<ReferenceDataList> response =
                        mdmService.List(
                            search.SearchFields.Criterias.Count == 0
                                ? "{}"
                                : search.SearchFields.Criterias[0].Criteria[0].ComparisonValue);
                    var responseAndTime = new Tuple<WebResponse<ReferenceDataList>, DateTime>(response, start);
                    args.Result = responseAndTime;
                };

            worker.RunWorkerCompleted += (o, eventArgs) =>
                {
                    if (eventArgs.Error != null)
                    {
                        eventAggregator.Publish(new BusyEvent(false));
                        eventAggregator.Publish(new ErrorEvent(eventArgs.Error));
                        return;
                    }

                    var responseAndTime = (Tuple<WebResponse<ReferenceDataList>, DateTime>)eventArgs.Result;
                    var response = responseAndTime.Item1;
                    var duration = SystemTime.UtcNow().Subtract(responseAndTime.Item2);

                    if (response.Code == HttpStatusCode.InternalServerError)
                    {
                        eventAggregator.Publish(new BusyEvent(false));
                        eventAggregator.Publish(new ErrorEvent("MDM Service Internal Server Error"));
                        return;
                    }

                    if (!response.IsValid && response.Code != HttpStatusCode.NotFound)
                    {
                        eventAggregator.Publish(new BusyEvent(false));
                        eventAggregator.Publish(new ErrorEvent(response.Fault));
                        return;
                    }

                    success(response.Message);
                    eventAggregator.Publish(new SearchResultsFound(response.Message.Count));
                    eventAggregator.Publish(new BusyEvent(false));
                    eventAggregator.Publish(new StatusEvent("Query Execution: " + duration.TotalSeconds));
                };

            worker.RunWorkerAsync();
        }
    }
}