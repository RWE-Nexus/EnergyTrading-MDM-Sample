﻿// This code was generated by a tool: ViewModelTemplates\EntitySearchResultsViewModelTemplate.tt
namespace Admin.PartyModule.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;

    using Admin.PartyModule.Uris;

    using Common.Events;
    using Common.Extensions;
    using Common.Services;

    using EnergyTrading.Contracts.Search;
    using EnergyTrading.Mdm.Client.Services;
    using EnergyTrading.MDM.Contracts.Sample;

    using Microsoft.Practices.Prism;
    using Microsoft.Practices.Prism.Events;
    using Microsoft.Practices.Prism.ViewModel;

    public class PartySearchResultsViewModel : NotificationObject, IActiveAware
    {
        private readonly IMdmService entityService;

        private readonly IEventAggregator eventAggregator;

        private readonly INavigationService navigationService;

        private bool isActive;

        private ObservableCollection<PartyViewModel> partys;

        private Search search;

        private PartyViewModel selectedParty;

        public PartySearchResultsViewModel(
            INavigationService navigationService, 
            IEventAggregator eventAggregator, 
            IMdmService entityService)
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

        public ObservableCollection<PartyViewModel> Partys
        {
            get
            {
                return this.partys;
            }

            set
            {
                this.partys = value;
                this.RaisePropertyChanged(() => this.Partys);
                if (Partys != null && Partys.Count > 0 && SelectedParty == null)
                {
                    SelectedParty = Partys[0];
                }
            }
        }

        public PartyViewModel SelectedParty
        {
            get
            {
                return this.selectedParty;
            }

            set
            {
                this.selectedParty = value;
                this.RaisePropertyChanged(() => this.SelectedParty);
            }
        }

        public void NavigateToDetail(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && this.SelectedParty != null)
            {
                this.navigationService.NavigateMain(
                    new PartyEditUri(this.SelectedParty.Id.Value, this.search.AsOf.Value));
            }
        }

        public void NavigateToDetailDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DependencyObject src = VisualTreeHelper.GetParent((DependencyObject)e.OriginalSource);

            if (src.GetType() == typeof(System.Windows.Controls.ContentPresenter))
            {
                if (this.SelectedParty != null)
                {
                    this.navigationService.NavigateMain(
                        new PartyEditUri(this.SelectedParty.Id.Value, this.search.AsOf.Value));
                }
            }
        }

        public void Sorting()
        {
            this.SelectedParty = null;
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
            searchRequestEvent.Search.SearchOptions.OrderBy = "Name";

            this.entityService.ExecuteAsyncSearch<Party>(
                this.search = searchRequestEvent.Search, 
                response =>
                    {
                        IList<Party> searchResults = response;
                        this.Partys =
                            new ObservableCollection<PartyViewModel>(
                                searchResults.Select(
                                    x => new PartyViewModel(new EntityWithETag<Party>(x, null), this.eventAggregator))
                                    .OrderBy(y => y.Name));
                    }, 
                this.eventAggregator);
        }
    }
}