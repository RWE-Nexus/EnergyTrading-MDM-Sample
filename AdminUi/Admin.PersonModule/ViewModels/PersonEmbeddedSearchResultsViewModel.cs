﻿// This code was generated by a tool: ViewModelTemplates\EntityEmbeddedSearchResultsViewModelTemplate.tt
namespace Admin.PersonModule.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;

    using Admin.PersonModule.Uris;

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

    public class PersonEmbeddedSearchResultsViewModel : NotificationObject, IActiveAware
    {
        private readonly IMdmService entityService;

        private readonly IEventAggregator eventAggregator;

        private readonly INavigationService navigationService;

        private readonly IRegionManager regionManager;

        private bool isActive;

        private ObservableCollection<PersonViewModel> persons;

        private Search search;

        private PersonViewModel selectedPerson;

        public PersonEmbeddedSearchResultsViewModel(
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

        public ObservableCollection<PersonViewModel> Persons
        {
            get
            {
                return this.persons;
            }

            set
            {
                this.persons = value;
                this.RaisePropertyChanged(() => this.Persons);
                if (Persons != null && Persons.Count > 0 && SelectedPerson == null)
                {
                    SelectedPerson = Persons[0];
                }
            }
        }

        public PersonViewModel SelectedPerson
        {
            get
            {
                return this.selectedPerson;
            }

            set
            {
                this.selectedPerson = value;
                this.RaisePropertyChanged(() => this.SelectedPerson);
            }
        }

        public void NavigateToDetail(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && this.SelectedPerson != null)
            {
                this.navigationService.NavigateMain(
                    new PersonEditUri(this.SelectedPerson.Id.Value, this.search.AsOf.Value));
            }
        }

        public void NavigateToDetailDoubleClick()
        {
            if (this.SelectedPerson != null)
            {
                this.navigationService.NavigateMain(
                    new PersonEditUri(this.SelectedPerson.Id.Value, this.search.AsOf.Value));
            }
        }

        public void Sorting()
        {
            this.SelectedPerson = null;
        }

        private IRegion MyRegion(IRegionCollection regions)
        {
            for (int i = regions.Count() - 1; i >= 0; i--)
            {
                if (regions.ElementAt(i).Name.EndsWith("-PersonSearchResultsRegion")
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
                    case "Person":
                        field = "Parent.Id";
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

                this.entityService.ExecuteAsyncSearch<Person>(
                    this.search = search, 
                    response =>
                        {
                            IList<Person> searchResults = response;
                            this.Persons =
                                new ObservableCollection<PersonViewModel>(
                                    searchResults.Select(
                                        x =>
                                        new PersonViewModel(new EntityWithETag<Person>(x, null), this.eventAggregator))
                                        .OrderBy(y => y.Name));
                        }, 
                    this.eventAggregator, 
                    false);
                return;
            }

            this.Persons = new ObservableCollection<PersonViewModel>();
        }
    }
}