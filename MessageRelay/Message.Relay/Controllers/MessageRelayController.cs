// -----------------------------------------------------------------------------
// copyright (c) 2019, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using System.Composition;
using System.Net;
using System.Threading.Tasks;
using MessageRelay.Models;
using MessageRelay.Providers;
using Http.Service.Contract.Boundaries;
using Http.Service.Contract.Sets;
using Tiny.API.Contracts;
using Tiny.Framework.Flow;
using Tiny.Framework.Utility;
using Tiny.Framework.Services.macOS;
using Tiny.Framework.Diagnostic;

namespace MessageRelay.Controllers
{
    /// <summary>
    /// Message relay controller.
    /// </summary>
    [Export(typeof(ITinyAPIController))]
    internal sealed class MessageRelayController :
        ITinyAPIController
    {
        /// <summary>
        /// Gets or sets the respond with (service).
        /// </summary>
        /// <value>The respond with.</value>
        [Import]
        public IRespondWith RespondWith { get; set; }

        /// <summary>
        /// Gets or sets the apple script (exeucution service).
        /// </summary>
        /// <value>The apple script.</value>
        [Import]
        public IRunAppleScripts AppleScript { get; set; }

        /// <summary>
        /// Gets or sets the (script) provider.
        /// </summary>
        /// <value>The provider.</value>
        [Import]
        public IProvideAppleScripts Provider { get; set; }

        /// <summary>
        /// Gets or sets the submission (coordinator).
        /// </summary>
        /// <value>The submission.</value>
        [Import]
        public ICoordinateRequestSubmissions Submission { get; set; }

        /// <summary>
        /// Gets or sets the substitutes (service).
        /// </summary>
        /// <value>The substitutes.</value>
        [Import]
        public IProvideTokenSubstitutions Substitutes { get; set; }

        /// <summary>
        /// Gets or sets the (message) mediator.
        /// </summary>
        /// <value>The mediator.</value>
        [Import]
        public IManageMessageMediation Mediator { get; set; }

        /// <summary>
        /// Gets or sets the message (factory).
        /// </summary>
        /// <value>The message.</value>
        [Import]
        public ICreateDiagnosticMessages Message { get; set; }

        /// <summary>
        /// Do incoming relay.
        /// </summary>
        /// <returns>The incoming relay.</returns>
        /// <param name="request">Request.</param>
        [ResourceFormatAndVerb("/relay", WebMethod.GET)]
        public async Task<IServerResponse> DoIncomingRelay(IRelayRequest request)
        {
            var theSwitch = Substitutes.GetSubstituteFor($"{request.Device}");
            if (It.IsEmpty(theSwitch))
            {
                Mediator.Publish(Message.Create($"unknown device: '{request.Device}'"));
            }

            if (It.Has(theSwitch))
            {
                if (Submission.RequiresAction(request))
                {
                    var candidate = Provider.GetScriptFor(request.Trigger);
                    if (It.Has(candidate) && It.Has(theSwitch))
                    {
                        var theScript = Substitutes.ApplySubstitutesTo(candidate)
                            .Replace(DynamicSubstitute.SwitchToken, theSwitch);

                        var theResponse = await AppleScript.Run(theScript);

                        Mediator.Publish(Message.Create($"'{request.Device}', '{request.Trigger}', '{theResponse}'"));
                    }
                }
                else
                {
                    Mediator.Publish(Message.Create($"duplicate request: '{request.Device}', '{request.Trigger}', '{request.Timestamp}'"));
                }
            }

            return await RespondWith.Result(HttpStatusCode.OK, "OK, buddy..");
        }

        /// <summary>
        /// Do internal relay.
        /// things you want to do inside the house, or on the box itself.
        /// </summary>
        /// <returns>The internal relay.</returns>
        /// <param name="theCommand">Command.</param>
        [ResourceFormatAndVerb("/{theCommand}/relay", WebMethod.GET)]
        public async Task<IServerResponse> DoInternalRelay(MessageCommand theCommand)
        {
            var theCandidate = Provider.GetScriptFor(theCommand);
            if (It.Has(theCandidate))
            {
                var theScript = Substitutes.ApplySubstitutesTo(theCandidate);
                var theResponse = await AppleScript.Run(theScript);

                Mediator.Publish(Message.Create(theResponse));
            }

            return await RespondWith.Result(HttpStatusCode.OK, "OK, buddy..");
        }

        /// <summary>
        /// Do outgoing relay.
        /// </summary>
        /// <returns>The outgoing relay.</returns>
        /// <param name="theRecipient">The recipient.</param>
        /// <param name="theMessage">The message.</param>
        [ResourceFormatAndVerb("/{theRecipient}/notify", WebMethod.GET)]
        public async Task<IServerResponse> DoOutgoingRelay(
            string theRecipient,
            string theMessage)
        {
            var thePhone = Substitutes.GetSubstituteFor(theRecipient);
            if (It.IsEmpty(thePhone))
            {
                Mediator.Publish(Message.Create($"unknown recipient: '{theRecipient}'"));
            }
            else
            {
                var theCandidate = Provider.GetScriptFor(MessageCommand.text_message);
                if (It.Has(theCandidate))
                {
                    var theScript = theCandidate
                        .Replace(DynamicSubstitute.MessageToken, theMessage)
                        .Replace(DynamicSubstitute.PersonToken, theRecipient)
                        .Replace(DynamicSubstitute.PhoneToken, thePhone);

                    var response = await AppleScript.Run(theScript);

                    Mediator.Publish(Message.Create(response));
                }
            }

            return await RespondWith.Result(HttpStatusCode.OK, "OK, buddy..");
        }
    }
}
