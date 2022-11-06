﻿using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using SegurosUdla.WebApi.Controllers;

namespace SegurosUdla.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task Test1()
    {
        var application = new WebApplicationFactory<Program>()
        .WithWebHostBuilder(builder =>
        {
            // ... Configure test services
        });

        var client = application.CreateClient();

        var result = await client.GetAsync("/api/test/bye");
        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));


        var registerResult = await client.PostAsJsonAsync("api/auth/register", new
        {
            username = "alain2",
            password = "Alain123!"
        });

        Assert.That(registerResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        var loginResult = await client.PostAsJsonAsync("api/auth/login", new
        {
            username = "alain2",
            password = "Alain123!"
        });

        Assert.That(registerResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        string jwt = await loginResult.Content.ReadAsStringAsync();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", jwt);
        var byeResult = await client.GetAsync("/api/test/bye");
        Assert.That(byeResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
}
