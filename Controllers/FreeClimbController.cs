using System;
using Microsoft.AspNetCore.Mvc;
using freeclimb.Api;
using freeclimb.Model;
using System;
using System.Collections.Generic;

namespace ReceiveMessage.Controllers
{
    [Route("/")]
    [ApiController]
    public class FreeClimbController : ControllerBase
    {

        [HttpPost]
        // public ActionResult smsConnect(CallResult freeClimbRequest)
        // {
        //     string acctId = getAcctId();
        //     string apiKey = getApiKey();
        //     FreeClimbClient client = new FreeClimbClient(acctId, apiKey);
        //     string to = freeClimbRequest.getFrom;
        //     string from = "+19809396134";
        //     client.getMessagesRequester.create(from, to, "Hello from the C# SDK!");

        //     Console.WriteLine("to: " + to);
        //     Console.WriteLine("from: " + from);

        //     return Ok();
        // }
        public string Post(CallResult request)
        {
            // Create a PerCl script
            PerclScript helloScript = new PerclScript(new List<PerclCommand>());
            string to = request.From;
            // Use as env variable rather than defining it in the code
            string from = "+19809396134";
            Console.WriteLine("to: " + to);
            Console.WriteLine("from: " + from);
            // Create a SMS Command
            Sms messageToSend = new Sms(to, from, "Hello from the C# SDK!");

            Console.WriteLine(messageToSend.ToJson());
            // Add the command
            helloScript.Commands.Add(messageToSend);

            Console.WriteLine(helloScript.ToJson());

            // Respond to FreeClimb with your script
            return helloScript.ToJson();
        }

        private string getAcctId()
        {
            return System.Environment.GetEnvironmentVariable("ACCOUNT_ID");
        }

        private string getApiKey()
        {
            return System.Environment.GetEnvironmentVariable("API_KEY");
        }
    }
}