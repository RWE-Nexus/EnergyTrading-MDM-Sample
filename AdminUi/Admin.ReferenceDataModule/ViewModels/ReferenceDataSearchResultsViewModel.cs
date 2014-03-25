using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Admin.ReferenceDataModule.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
	using System.Windows.Input;
    using Admin.ReferenceDataModule.Uris;
    using Common.Events;
    using Common.Extensions;
    using Common.Services;
    using Microsoft.Practices.Prism;
    using Microsoft.Practices.Prism.Events;
    using Microsoft.Practices.Prism.ViewModel;
    using EnergyTrading.Contracts.Search;
    using EnergyTrading.Mdm.Client.Services;
    using EnergyTrading.MDM.Contracts.Sample; using EnergyTrading.Mdm.Contracts;

    public class ReferenceDataSearchResultsViewModel : NotificationObject, IActiveAware
    {
        private readonly IReferenceDataService entityService;
        private readonly IEventAggregator eventAggregator;
        private readonly INavigationService navigationService;
        private bool isActive;

        private ObservableCollection<ReferenceDataViewModel> referenceDatas;

        private Search search;
        private ReferenceDataViewModel selectedReferenceData;

        public ReferenceDataSearchResultsViewModel(
            INavigationService navigationService, 
            IEventAggregator eventAggregator, 
            IReferenceDataService entityService)
        {
            this.navigationService = navigationService;
            this.eventAggregator = eventAggregator;
            this.entityService = entityService;
            this.IsActiveChanged += this.OnIsActiveChanged;
        }

        public event EventHandler IsActiveChanged;

        public bool IsActive
        {
            get
            {
                return this.isActive;
            }

            set
            {
                if (this.isActive != value)
                {
                    this.isActive = value;
                }

                this.IsActiveChanged(this, EventArgs.Empty);
            }
        }

        public ObservableCollection<ReferenceDataViewModel> ReferenceDatas
        {
            get
            {
                return this.referenceDatas;
            }

            set
            {
                this.referenceDatas = value;
                this.RaisePropertyChanged(() => this.ReferenceDatas);
                if (ReferenceDatas != null && ReferenceDatas.Count > 0 && SelectedReferenceData == null) SelectedReferenceData = ReferenceDatas[0];            }
        }

        public ReferenceDataViewModel SelectedReferenceData
        {
            get
            {
                return this.selectedReferenceData;
            }

            set
            {
                this.selectedReferenceData = value;
                this.RaisePropertyChanged(() => this.SelectedReferenceData);
            }
        }
		
		public void NavigateToDetail(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && this.SelectedReferenceData != null)
            {
                this.navigationService.NavigateMain(new ReferenceDataEditUri(this.SelectedReferenceData.ReferenceKey, this.SelectedReferenceData.Values));
            }
        }

        public void NavigateToDetailDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DependencyObject src = VisualTreeHelper.GetParent((DependencyObject)e.OriginalSource);

            if (src.GetType() == typeof(ContentPresenter))
            {
                if (this.SelectedReferenceData != null)
                {
                    this.navigationService.NavigateMain(new ReferenceDataEditUri(this.SelectedReferenceData.ReferenceKey, this.SelectedReferenceData.Values));
                }
            }
        }

        public void Sorting()
        {
            this.SelectedReferenceData = null;
        }

        private void OnIsActiveChanged(object sender, EventArgs eventArgs)
        {
            if (this.isActive)
            {
                this.eventAggregator.Subscribe<SearchRequestEvent>(this.SearchRequest);
                return;
            }

            this.eventAggregator.Unsubscribe<SearchRequestEvent>(this.SearchRequest);
        }

        private void SearchRequest(SearchRequestEvent searchRequestEvent)
        {
            searchRequestEvent.Search.SearchOptions.OrderBy = "ReferenceKey";

            this.entityService.ExecuteAsyncSearchRD(
                this.search = searchRequestEvent.Search,
                (response) =>
                {
                    ReferenceDataList searchResults = response;
                    Dictionary<string, string> combinedResults = new Dictionary<string, string>();
                    foreach (var rd in searchResults)
                    {
                        if (combinedResults.ContainsKey(rd.ReferenceKey))
                        {
                            combinedResults[rd.ReferenceKey] = combinedResults[rd.ReferenceKey] + "|" + rd.Value;
                        }
                        else
                        {
                            combinedResults.Add(rd.ReferenceKey, rd.Value);
                        }
                    }
                    this.ReferenceDatas =
                        new ObservableCollection<ReferenceDataViewModel>(
                            combinedResults.Select(
                                x =>
                                new ReferenceDataViewModel(x.Key, x.Value, this.eventAggregator)).OrderBy(y => y.ReferenceKey));
                },
                this.eventAggregator);
        }
    }
}
