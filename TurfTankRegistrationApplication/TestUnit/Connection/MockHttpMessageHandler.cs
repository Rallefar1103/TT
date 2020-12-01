using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Http;
using System.Threading;

namespace TestUnit.Connection
{
    /// <summary>
    /// A mock of the inner workings of HttpClient, so that it is able to be mocked.
    /// An instance of this class sets the value for what the HttpClient returns from its GetAsync method.
    /// </summary>
    /// <see>https://dev.to/n_develop/mocking-the-httpclient-in-net-core-with-nsubstitute-k4j</see>
    public class MockHttpMessageHandler : HttpMessageHandler
    {
        private readonly string _response;
        private readonly HttpStatusCode _statusCode;

        /// <summary>
        /// The parameter input which the method received when it was called.
        /// </summary>
        public string Input { get; private set; }
        /// <summary>
        /// A setup to "remember" the amount of times it has been called.
        /// </summary>
        public int NumberOfCalls { get; private set; }

        /// <summary>
        /// The primary method which handles the actual return value of HttpClient.
        /// </summary>
        /// <param name="response">A string which consists of the mocked response</param>
        /// <param name="statusCode">A status code to pair with the response</param>
        public MockHttpMessageHandler(string response, HttpStatusCode statusCode)
        {
            _response = response;
            _statusCode = statusCode;
        }
        /// <summary>
        /// The SendAsync method which is being overwritten to make it able to serve as a mock.
        /// Since SendAsync is what all other methods such as GetAsync or PostAsync uses, this mocks it all.
        /// </summary>
        /// <param name="request">This is the URI which is being requested</param>
        /// <param name="cancellationToken">I don't know yet...</param>
        /// <returns></returns>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            NumberOfCalls++;

            if (request.Content != null)
            {
                Input = await request.Content.ReadAsStringAsync();
            }

            return new HttpResponseMessage
            {
                StatusCode = _statusCode,
                Content = new StringContent(_response)
            };
        }
    }
}
