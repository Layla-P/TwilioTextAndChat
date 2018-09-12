
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.TwiML;
using Twilio.Types;
using TwilioTextAndCall.Models;

namespace TwilioTextAndCall.Controllers
{

    [Route("/")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly TwilioAccountDetails _acc;

        public HomeController(IOptions<TwilioAccountDetails> acc)
        {
            _acc = acc.Value ?? throw new ArgumentException(nameof(acc));
        }
        [Route("/hello")]
        public IActionResult Hello()
        {
            return Content("Hello!");
        }

        [Route("/voice")]
        public IActionResult Voice()
        {

            var response = @"
                <Response>
                      // write some TWIML here: https://www.twilio.com/docs/voice/twiml
                </Response>
            ";
            return Content(response, "text/xml");
        }


        [Route("/call")]
        public void Call()
        {
            TwilioClient.Init(_acc.AccountSid, _acc.AuthToken);
            var ngrokport = "";
            var fromNumber = "";
            var toNumber = new PhoneNumber("");

            CallResource.Create(to: toNumber, from: fromNumber,
                url: new Uri($"http://{ngrokport}.ngrok.io/voice"));

        }

        [Route("/text")]
        public void Text()
        {
            TwilioClient.Init(_acc.AccountSid, _acc.AuthToken);

            var fromNumber = "";
            var toNumber = "";

            MessageResource.Create(
                toNumber,
                from: fromNumber,
                body: "your message here");
        }
    }
}
    