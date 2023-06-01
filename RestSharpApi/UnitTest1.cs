using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;

namespace RestSharpApi
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void PrimerTestGet()
        {
            var client = new RestClient("http://localhost:3000/");
            var request = new RestRequest("posts/{postid}", Method.Get);
            request.AddUrlSegment("postid", "1");
            var response = client.ExecuteGet(request);
            //var response = client.Get(request);
            var content = response.Content;
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            // Deserealizar (Libreria: NewtonSoft)
                            // Custom serialization, se crea los propios serializadores
            var jsonObject = JObject.Parse(response.Content);
            var result = jsonObject.SelectToken("title").ToString();
            Assert.That(result, Is.EqualTo("json-server"), "Title is not correct");
            Assert.That(result, !Is.Empty, "Title is not correct");

        }

        [Test]
        public void TestPostRequest()
        {
            var restClient = new RestClient("http://localhost:3000/");
            //var client = new RestClient("http://localhost:3000/");
            //var request = new RestRequest("posts/{postid}", Method.Post);
            var request = new RestRequest("posts/", Method.Post);
            request.AddJsonBody(new { title = "raju"});
            //request.AddUrlSegment("postid", 4);
            var response = restClient.Execute(request);
            var jsonObject = JObject.Parse(response.Content);
            var result = jsonObject.SelectToken("title").ToString();
            Assert.That(result, Is.EqualTo("raju"), "Title is not correct");
        }
    }
}