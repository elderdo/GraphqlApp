using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using GraphQLProductApp.Data;
using RestSharp;
using Xunit;
using Xunit.Abstractions;

namespace RestSharpDemoRider;

public class UnitTest1
{
    private readonly ITestOutputHelper _output;

    public UnitTest1(ITestOutputHelper output)
    {
        _output = output;
    }

    
    [Fact]
    public async Task GetOperationTest()
    {
        var restClientOptions = new RestClientOptions
        {
            BaseUrl = new Uri("https://localhost:5001/"),
            RemoteCertificateValidationCallback = (sender, certificate, chain, errors) => true
        };
        
        //Rest Client
        var client = new RestClient(restClientOptions);
        //Rest Request
        var request = new RestRequest("Product/GetProductById/1");
        //Perform GET operation
        var response = await client.GetAsync<Product>(request);
        //Assert
        response?.Name.Should().Be("Keyboard");
    }
    
    [Fact]
    public async Task GetWithQuerySegmentTest()
    {
        var restClientOptions = new RestClientOptions
        {
            BaseUrl = new Uri("https://localhost:5001/"),
            RemoteCertificateValidationCallback = (sender, certificate, chain, errors) => true
        };
        
        //Rest Client
        var client = new RestClient(restClientOptions);
        //Rest Request
        var request = new RestRequest("Product/GetProductById/{id}");
        request.AddUrlSegment("id", 2);
        //Perform GET operation
        var response = await client.GetAsync<Product>(request);
        //Assert
        response?.Price.Should().Be(400);
    }
    
    [Fact]
    public async Task GetWithQueryParameterTest()
    {
        var restClientOptions = new RestClientOptions
        {
            BaseUrl = new Uri("https://localhost:5001/"),
            RemoteCertificateValidationCallback = (sender, certificate, chain, errors) => true
        };
        
        //Rest Client
        var client = new RestClient(restClientOptions);
        //Rest Request
        var request = new RestRequest("Product/GetProductByIdAndName");
        request.AddQueryParameter("id", 2);
        request.AddQueryParameter("name", "Monitor");
        //Perform GET operation
        var response = await client.GetAsync<Product>(request);
        //Assert
        response?.Price.Should().Be(400);
    }
    
    
    [Fact]
    public async Task PostProductTest()
    {
        var restClientOptions = new RestClientOptions
        {
            BaseUrl = new Uri("https://localhost:5001/"),
            RemoteCertificateValidationCallback = (sender, certificate, chain, errors) => true
        };
        
        //Rest Client
        var client = new RestClient(restClientOptions);
        //Rest Request
        var request = new RestRequest("Product/Create");
        request.AddJsonBody(new Product
        {
            Name = "Cabinet",
            Description = "Gaming Cabinet",
            Price = 300,
            ProductType = ProductType.PERIPHARALS
        });
        //Perform POST operation
        var response = await client.PostAsync<Product>(request);
        //Assert
        response?.Price.Should().Be(300);
    }
    [Fact]
    public async Task FileUploadTest()
    {
        var restClientOptions = new RestClientOptions
        {
            BaseUrl = new Uri("https://localhost:5001/"),
            RemoteCertificateValidationCallback = (sender, certificate, chain, errors) => true
        };
        
        //Rest Client
        var client = new RestClient(restClientOptions);
        //Rest Request
        var request = new RestRequest("Product",Method.Post);
        request.AddFile("file", "../../TestData/restsharp.png","multipart/form-data");
        //Perform Post operation
        var response = await client.ExecuteAsync(request);
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

}