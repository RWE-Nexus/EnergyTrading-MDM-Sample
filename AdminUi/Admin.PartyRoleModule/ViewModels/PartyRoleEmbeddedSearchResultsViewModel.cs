﻿// This code was generated by a tool: ViewModelTemplates\EntityEmbeddedSearchResultsViewModelTemplate.tt
namespace Admin.PartyRoleModule.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;

    using Admin.PartyRoleModule.Uris;

    using Common.Extensions;
    using Common.Services;

    using EnergyTrading.Contracts.Search;
    using EnergyTrading.Mdm.Client.Services;
    using EnergyTrading.MDM.Contracts.Sample;
    using EnergyTrading.Search;

    using Microsoft.Practices.Prism;
    using Microsoft.Practices.Prism.Events;
    using Microsoft.Practices.Prism.Regions;
    using Microsoft.Practices.Prism.ViewModel;

    public class PartyRoleEmbeddedSearchResultsViewModel : NotificationObject, IActiveAware
    {
        private readonly IMdmService entityService;

        private readonly IEventAggregator eventAggregator;

        private readonly INavigationService navigationService;

        private readonly IRegionManager regionManager;

        private bool isActive;

        private ObservableCollection<PartyRoleViewModel> partyroles;

        private Search search;

        private PartyRoleViewModel selectedPartyRole;

        public PartyRoleEmbeddedSearchResultsViewModel(
            INavigationService navigationService, 
            IEventAggregator eventAggregator, 
            IMdmService entityService, 
            IRegionManager regionManager)
        {
            this.navigationService = navigationService;
            this.eventAggregator = eventAggregator;
            this.entityService = entityService;
            this.regionManager = regionManager;
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

        public ObservableCollection<PartyRoleViewModel> PartyRoles
        {
            get
            {
                return this.partyroles;
            }

            set
            {
                this.partyroles = value;
                this.RaisePropertyChanged(() => this.PartyRoles);
                if (PartyRoles != null && PartyRoles.Count > 0 && SelectedPartyRole == null)
                {
                    SelectedPartyRole = PartyRoles[0];
                }
            }
        }

        public PartyRoleViewModel SelectedPartyRole
        {
            get
            {
                return this.selectedPartyRole;
            }

            set
            {
                this.selectedPartyRole = value;
                this.RaisePropertyChanged(() => this.SelectedPartyRole);
            }
        }

        public void NavigateToDetail(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && this.SelectedPartyRole != null)
            {
                this.navigationService.NavigateMain(
                    new PartyRoleEditUri(
                        this.SelectedPartyRole.EditFormViewName, 
                        this.SelectedPartyRole.Id.Value, 
                        this.search.AsOf.Value));
            }
        }

        public void NavigateToDetailDoubleClick()
        {
            if (this.SelectedPartyRole != null)
            {
                this.navigationService.NavigateMain(
                    new PartyRoleEditUri(
                        this.SelectedPartyRole.EditFormViewName, 
                        this.SelectedPartyRole.Id.Value, 
                        this.search.AsOf.Value));
            }
        }

        public void Sorting()
        {
            this.SelectedPartyRole = null;
        }

        private IRegion MyRegion(IRegionCollection regions)
        {
            for (int i = regions.Count() - 1; i >= 0; i--)
            {
                if (regions.ElementAt(i).Name.EndsWith("-PartyRoleSearchResultsRegion")
                    && regions.ElementAt(i).Context != null)
                {
                    return regions.ElementAt(i);
                }
            }

            return null;
        }

        private void OnIsActiveChanged(object sender, EventArgs eventArgs)
        {
            if (this.isActive)
            {
                Search search = SearchBuilder.CreateSearch();

                var context = MyRegion(this.regionManager.Regions).Context as Tuple<int, DateTime?, string>;
                search.AsOf = context.Item2;

                string field;
                switch (context.Item3)
                {
                    case "PartyRole":
                        field = "Parent.Id";
                        break;
                    case "Party":
                        field = "PartyRole.Party.Id";
                        break;
                    default:
                        field = context.Item3 + ".Id";
                        break;
                }

                if (field.Contains("|"))
                {
                    search.SearchFields.Combinator = SearchCombinator.Or;
                    var fields = field.Split(new[] { '|' });

                    foreach (string f in fields)
                    {
                        search.AddSearchCriteria(SearchCombinator.And)
                            .AddCriteria(f, SearchCondition.NumericEquals, context.Item1.ToString());
                    }
                }
                else
                {
                    search.AddSearchCriteria(SearchCombinator.And)
                        .AddCriteria(field, SearchCondition.NumericEquals, context.Item1.ToString());
                }

                this.entityService.ExecuteAsyncSearch<PartyRole>(
                    this.search = search, 
                    response =>
                        {
                            IList<PartyRole> searchResults = response;
                            this.PartyRoles =
                                new ObservableCollection<PartyRoleViewModel>(
                                    searchResults.Select(
                                        x =>
                                        new PartyRoleViewModel(
                                            new EntityWithETag<PartyRole>(x, null), 
                                            this.eventAggregator)).OrderBy(y => y.Name));
                        }, 
                    this.eventAggregator, 
                    false);
                return;
            }

            this.PartyRoles = new ObservableCollection<PartyRoleViewModel>();
        }
    }
}