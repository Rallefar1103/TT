using System;
using System.Collections.Generic;
namespace TurfTankRegistrationApplication.Model
{
    public class AccessToken//: Token
    {
        public string access_token;
        public string scope;
        public string expires_in;
        public string token_type;
        public string state;

        public AccessToken(string fragment)
        {
            char[] delimiters = {'&'};
            Dictionary<string, string> dic = _parseFragment(fragment, delimiters);
            access_token = dic[nameof(access_token)];
            scope = dic[nameof(scope)];
            expires_in = dic[nameof(expires_in)];
            token_type = dic[nameof(token_type)];
            state = dic[nameof(state)];
        }


        /// <summary>
        /// parse (#access_token=VAt58QTLodcq5gtVFZugyNYoI_uLnV3i&scope=openid%20profile%20email%20offline_access&expires_in=7200&token_type=Bearer&state=lrbbvzktjppljxso)
        /// into accessToken
        /// </summary>
        /// <param name="fragment"></param>
        /// <param name="delimiters"></param>
        /// <returns></returns>https://docs.microsoft.com/en-us/advertising/hotel-service/code-example-code-grant-flow
        Dictionary<string, string> _parseFragment(string fragment, char[] delimiters)
        {
                string tempstr = fragment.TrimStart('#');
                var parameters = new Dictionary<string, string>();

                string[] pairs = tempstr.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                foreach (string pair in pairs)
                {
                    string[] nameValaue = pair.Split(new char[] { '=' });
                    parameters.Add(nameValaue[0], nameValaue[1]);
                }

                return parameters;            
        }
    }
}
