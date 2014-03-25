namespace Mdm.Client.Sample.Tests.WebClient
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.ServiceModel.Syndication;
    using System.Xml;

    using EnergyTrading;
    using EnergyTrading.Contracts.Search;
    using EnergyTrading.Mdm.Client.WebApi.WebApiClient;
    using EnergyTrading.Mdm.Client.WebClient;
    using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.MDM.Contracts.Sample;
    using EnergyTrading.Test;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class when_a_call_is_made_to_the_message_requester_to_search_for_an_entity_and_it_succeeds : SpecBaseAutoMocking<MessageRequester>
    {
        private WebResponse<IList<Person>> response;
        private IList<Person> bobs;
        private string uri;

        protected override void Establish_context()
        {
            base.Establish_context();

            this.uri = "http://someuri/person/search";
            this.bobs = new List<Person> { new Person { Details = new PersonDetails() { Forename = "Bob" } } };

            this.Mock<IHttpClientFactory>().Setup(factory => factory.Create(this.uri)).Returns(
                this.RegisterMock<IHttpClient>().Object);

            this.Mock<IHttpClient>().Setup(client => client.Post(It.IsAny<string>(), It.IsAny<HttpContent>())).Returns(
                () => new HttpResponseMessage { Content = this.CreateSearch(), StatusCode = HttpStatusCode.OK });
        }

        protected override void Because_of()
        {
            this.response = this.Sut.Search<Person>(this.uri, new Search());
        }

        [Test]
        public void should_return_a_valid_response()
        {
            Assert.AreEqual(this.response.IsValid, true);
        }

        [Test]
        public void should_return_the_entities_from_the_service()
        {
            Assert.AreEqual(1, this.response.Message.Count());
            Assert.AreEqual("Bob", this.response.Message[0].Details.Forename);
        }

        [Test]
        public void should_return_a_ok_status_code()
        {
            Assert.AreEqual(HttpStatusCode.OK, this.response.Code);
        }

        private HttpContent CreateSearch()
        {
            var feed = new SyndicationFeed
                {
                    Id = string.Format("urn:uuid:{0}:{1}", "Person", "search"),
                    Title = new TextSyndicationContent("Search Results"),
                    Generator = "Nexus Mapping Service"
                };
            feed.Authors.Add(new SyndicationPerson { Name = "Nexus Mapping Service" });
            feed.LastUpdatedTime = SystemTime.UtcNow();

            feed.Items =
                this.bobs.Select(
                    x =>
                    new SyndicationItem
                        {
                            Title = new TextSyndicationContent("Person"),
                            Content = new XmlSyndicationContent("application/xml", new SyndicationElementExtension(x))
                        });

            return HttpContentUtilities.CreateContentFromAtom10SyndicationFeed(feed);
        }
    }

    [TestFixture]
    public class
        when_a_call_is_made_to_the_message_requester_to_search_for_an_entity_and_a_request_is_made_for_multiple_pages :
            SpecBaseAutoMocking<MessageRequester>
    {
        private PagedWebResponse<IList<Person>> response;

        private IList<Person> bobs;

        private string uri;

        private Uri nextPage = new Uri("http://somehost/search/results/someguid/2");

        protected override void Establish_context()
        {
            base.Establish_context();

            this.uri = "http://someuri/person/search";

            this.bobs = new List<Person> { new Person { Details = new PersonDetails() { Forename = "Bob" } } };

            this.Mock<IHttpClientFactory>().Setup(factory => factory.Create(this.uri)).Returns(
                this.RegisterMock<IHttpClient>().Object);

            this.Mock<IHttpClient>().Setup(client => client.Post(It.IsAny<string>(), It.IsAny<HttpContent>())).Returns(
                () => new HttpResponseMessage { Content = this.CreateSearch(), StatusCode = HttpStatusCode.OK });
        }

        protected override void Because_of()
        {
            this.response = this.Sut.Search<Person>(
                this.uri, new Search() { SearchOptions = new SearchOptions() { MultiPage = true, ResultsPerPage = 1 } });
        }

        [Test]
        public void should_return_a_valid_response()
        {
            Assert.AreEqual(this.response.IsValid, true);
        }

        [Test]
        public void should_return_the_entities_from_the_service()
        {
            Assert.AreEqual(1, this.response.Message.Count());
            Assert.AreEqual("Bob", this.response.Message[0].Details.Forename);
        }

        [Test]
        public void should_return_a_ok_status_code()
        {
            Assert.AreEqual(HttpStatusCode.OK, this.response.Code);
        }

        [Test]
        public void should_return_a_link_to_the_next_page_of_results()
        {
            Assert.AreEqual(this.nextPage, this.response.NextPage);
        }

        private HttpContent CreateSearch()
        {
            var feed = new SyndicationFeed
                {
                    Id = string.Format("urn:uuid:{0}:{1}", "Person", "search"),
                    Title = new TextSyndicationContent("Search Results"),
                    Generator = "Nexus Mapping Service"
                };

            feed.Authors.Add(new SyndicationPerson { Name = "Nexus Mapping Service" });
            feed.LastUpdatedTime = SystemTime.UtcNow();
            var link = new SyndicationLink(this.nextPage) { RelationshipType = "next-results" };

            feed.Links.Add(link);

            feed.Items =
                this.bobs.Select(
                    x =>
                    new SyndicationItem
                        {
                            Title = new TextSyndicationContent("Person"),
                            Content = new XmlSyndicationContent("application/xml", new SyndicationElementExtension(x))
                        });

            return HttpContentUtilities.CreateContentFromAtom10SyndicationFeed(feed);
        }
    }

    [TestFixture]
    public class
        when_a_call_is_made_to_the_message_requester_to_search_for_an_entity_and_it_succeeds_but_finds_no_results :
            SpecBaseAutoMocking<MessageRequester>
    {
        private WebResponse<IList<Person>> response;

        private IList<Person> emptyBobs;

        private string uri;

        protected override void Establish_context()
        {
            base.Establish_context();
            this.emptyBobs = new List<Person>();

            this.uri = "http://someuri/person/search";
            this.Mock<IHttpClientFactory>().Setup(factory => factory.Create(this.uri)).Returns(
                this.RegisterMock<IHttpClient>().Object);

            var writer = new XmlTextWriter(TextWriter.Null);

            this.Mock<IHttpClient>().Setup(client => client.Post(this.uri, It.IsAny<HttpContent>())).Returns(
                () => new HttpResponseMessage { Content = this.CreateSearch(), StatusCode = HttpStatusCode.NotFound });
        }

        protected override void Because_of()
        {
            this.response = this.Sut.Search<Person>(this.uri, new Search());
        }

        [Test]
        public void should_return_a_valid_response()
        {
            Assert.AreEqual(this.response.IsValid, false);
        }

        [Test]
        public void should_return_no_entities_from_the_service()
        {
            Assert.AreEqual(0, this.response.Message.Count());
        }

        [Test]
        public void should_return_a_not_found_status_code()
        {
            Assert.AreEqual(HttpStatusCode.NotFound, this.response.Code);
        }

        private HttpContent CreateSearch()
        {
            var feed = new SyndicationFeed();
            feed.Id = string.Format("urn:uuid:{0}:{1}", "Person", "search");
            feed.Title = new TextSyndicationContent("Search Results");
            feed.Generator = "Nexus Mapping Service";
            feed.Authors.Add(new SyndicationPerson { Name = "Nexus Mapping Service" });
            feed.LastUpdatedTime = SystemTime.UtcNow();

            feed.Items =
                this.emptyBobs.Select(
                    x =>
                    new SyndicationItem
                        {
                            Title = new TextSyndicationContent("Person"),
                            Content = new XmlSyndicationContent("application/xml", new SyndicationElementExtension(x))
                        });

            return HttpContentUtilities.CreateContentFromAtom10SyndicationFeed(feed);
        }
    }

    [TestFixture]
    public class when_a_call_is_made_to_the_message_requester_to_search_for_an_entity_and_a_fault_occurs :
        SpecBaseAutoMocking<MessageRequester>
    {
        private WebResponse<IList<Person>> response;

        private string uri;

        protected override void Establish_context()
        {
            base.Establish_context();

            this.uri = "http://someuri/";
            this.Mock<IHttpClientFactory>().Setup(factory => factory.Create(this.uri)).Returns(
                this.RegisterMock<IHttpClient>().Object);
            var fault = new Fault() { Message = "Fault!" };
            var content = new ObjectContent<Fault>(fault, new XmlMediaTypeFormatter());
            this.Mock<IHttpClient>().Setup(client => client.Post(this.uri, It.IsAny<HttpContent>())).Returns(
                () => new HttpResponseMessage() { StatusCode = HttpStatusCode.InternalServerError, Content = content });
        }

        protected override void Because_of()
        {
            this.response = this.Sut.Search<Person>(this.uri, new Search());
        }

        [Test]
        public void should_return_the_fault_in_the_response()
        {
            Assert.AreEqual("Fault!", this.response.Fault.Message);
        }
    }

    [TestFixture]
    public class when_a_call_is_made_to_the_message_requester_to_search_for_an_entity_and_an_exception_is_thrown_by_the_service : SpecBaseAutoMocking<MessageRequester>
    {
        private WebResponse<IList<Person>> response;

        private string uri;

        protected override void Establish_context()
        {
            base.Establish_context();

            this.uri = "http://someuri/";
            this.Mock<IHttpClientFactory>().Setup(factory => factory.Create(this.uri)).Returns(this.RegisterMock<IHttpClient>().Object);
            this.Mock<IHttpClient>().Setup(client => client.Post(this.uri, It.IsAny<HttpContent>())).Throws(
                new Exception("unkown exception"));
        }

        protected override void Because_of()
        {
            this.response = this.Sut.Search<Person>(this.uri, new Search());
        }

        [Test]
        public void should_return_a_fault_in_the_response()
        {
            Assert.IsNotNull(this.response.Fault.Message);
        }

        [Test]
        public void should_return_an_internal_server_error_code()
        {
            Assert.AreEqual(HttpStatusCode.InternalServerError, this.response.Code);
        }

        [Test]
        public void should_mark_the_message_as_not_valid()
        {
            Assert.AreEqual(false, this.response.IsValid);
        }

        [Test]
        public void should_return_the_excpetion_message_from_the_service()
        {
            Assert.IsTrue(this.response.Fault.Message.Contains("unkown exception"));
        }
    }
}
 