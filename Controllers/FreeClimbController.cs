using System;
using Microsoft.AspNetCore.Mvc;
using freeclimb.Api;
using freeclimb.Model;
using freeclimb.Enums;
using System.Collections.Generic;

namespace ReceiveMessage.Controllers
{
    [Route("/voice")]
    [ApiController]
    public class FreeClimbController : ControllerBase
    {
        [HttpPost]
        public string Post(CallStatus request)
        {
            PerclScript helloScript = new PerclScript(new List<PerclCommand>());
            string to = getToNumber();
            // Use as env variable rather than defining it in the code
            string from = getFromNumber();
            Console.WriteLine("to: " + to);
            Console.WriteLine("from: " + from);
            // Create a SMS Command
            Sms messageToSend = new Sms(to, from, "Hello from the C# SDK!", "https://test.abc");

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
        private string getFromNumber()
        {
            return System.Environment.GetEnvironmentVariable("FROM_NUMBER");
        }
        private static string getToNumber()
        {
            return System.Environment.GetEnvironmentVariable("TO_NUMBER");
        }
    }
}