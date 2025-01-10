using Microsoft.AspNetCore.Mvc;
using freeclimb.Api;
using freeclimb.Client;
using freeclimb.Model;
using DotNetEnv;

namespace CSharp_SMS_Quickstart.Controllers;

public class IncomingSmsWebhookRequest
{
    public string? accountId { get; set; }
    public string? applicationId { get; set; }
    public string? direction { get; set; }
    public string? from { get; set; }
    public string? messageId { get; set; }
    public string? phoneNumberId { get; set; }
    public string? requestType { get; set; }
    public string? status { get; set; }
    public string? text { get; set; }
    public string? to { get; set; }
}

[ApiController]
[Route("[controller]")]
public class IncomingSmsController : ControllerBase
{
    private readonly ILogger<IncomingSmsController> _logger;
    private DefaultApi freeclimbClient;

    public IncomingSmsController(ILogger<IncomingSmsController> logger)
    {
        _logger = logger;
        DotNetEnv.Env.Load();
        
        string apiServer = System.Environment.GetEnvironmentVariable("API_SERVER") ?? "https://www.freeclimb.com/apiserver";
        string accountId = System.Environment.GetEnvironmentVariable("ACCOUNT_ID");
        string apiKey = System.Environment.GetEnvironmentVariable("API_KEY");
        Configuration config = new Configuration();
        config.BasePath = apiServer;
        config.Username = accountId;
        config.Password = apiKey;
        freeclimbClient = new DefaultApi(config);
    }

    [HttpPost(Name = "PostIncomingSms")]
    public IActionResult Post([FromBody] IncomingSmsWebhookRequest request)
    {
        string to = request.from;
        string from = System.Environment.GetEnvironmentVariable("FREECLIMB_NUMBER");

        var messageRequest = new MessageRequest(from: from, to: to, text: "Hello, World!", mediaUrls: new List<string>());
        MessageResult result = freeclimbClient.SendAnSmsMessage(messageRequest);

        return Ok();
    }
}
