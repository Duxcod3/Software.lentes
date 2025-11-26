using FluentAssertions;
using RestSharp;
using System.Text.Json;


namespace Lente.Tests
{
    public class Lente
    {
        private readonly string baseUrl = "http://mlens-apii-bcf4akbafshnesek.brazilsouth-01.azurewebsites.net/";


        [Fact]
        public async void LoginComCredenciaisValidas_DeveRetornarToken()
        {
            var client = new RestClient("http://mlens-apii-bcf4akbafshnesek.brazilsouth-01.azurewebsites.net/");
            var request = new RestRequest("/api/Auth/login", Method.Post);
            request.AddJsonBody(new { nomeDeUsuario = "admin", senha = "admin123" });

            var response = await client.ExecuteAsync(request);

            // Validar status code 200 Ok
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            //Validar se mp JSPM retornar existe o token

            var jsonResponse = JsonSerializer.Deserialize<JsonElement>(response.Content);
            jsonResponse.TryGetProperty("token", out var token).Should().BeTrue();
            token.GetString().Should().NotBeNullOrEmpty();




        }

    }
}
