using System;
using System.Collections.Generic;
using SimpleJSON;
using Eskills.Ui;

namespace Eskills.Service.Repository
{
    public class ParameterMapper
    {
        private readonly string packageName = "com.appcoins.eskills.unitytest";
        private readonly int matchMaxDuration = 3600; 
        public TicketParameters Map(MatchParameters matchParameters)
        {
            return new TicketParameters(packageName,Convert.ToDecimal(matchParameters.value),matchParameters.currency,matchParameters.product,matchParameters.matchEnvironment,matchParameters.numberOfPlayers,matchMaxDuration);
        }
    }
}