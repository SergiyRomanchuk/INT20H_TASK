using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgrammCreator.API.Models.Helpers
{
    public class AuthHelper
    {
        public static Dictionary<string, string> GetSessionParamsFromLink(string authLink)
        {
            if (String.IsNullOrWhiteSpace(authLink))
            {
                return null;
            }
            var paramDictionary = new Dictionary<string, string>();
            var urlParts = authLink.Split('#');
            var urlParams = urlParts[1].Split('&');
            // access_token=sometoken
            foreach (var paramExpression in urlParams)
            {
                //have a key/value pair
                var param_value = paramExpression.Split('=');
                paramDictionary.Add(param_value[0], param_value[1]);
            }
            return paramDictionary;
        }
    }
}