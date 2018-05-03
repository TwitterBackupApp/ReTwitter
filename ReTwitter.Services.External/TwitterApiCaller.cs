using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using ReTwitter.Services.External.Contracts;

namespace ReTwitter.Services.External
{
    public class TwitterApiCaller : ITwitterApiCaller
    {
        private readonly string consumerKey;
        private readonly string consumerSecret;
        private readonly string accessToken;
        private readonly string accessSecret;
        private const string version = "1.0";
        private const string signatureMethod = "HMAC-SHA1";

        public TwitterApiCaller(ITwitterCredentialsProvider credentials)
        {
            if (credentials == null)
            {
                throw new ArgumentNullException(nameof(credentials));
            }

            this.consumerSecret = string.IsNullOrWhiteSpace(credentials.ConsumerSecret) ? throw new ArgumentNullException(nameof(consumerSecret)) : credentials.ConsumerSecret;
            this.consumerKey = string.IsNullOrWhiteSpace(credentials.ConsumerKey) ? throw new ArgumentNullException(nameof(consumerKey)) : credentials.ConsumerKey;
            this.accessToken = string.IsNullOrWhiteSpace(credentials.AccessToken) ? throw new ArgumentNullException(nameof(accessToken)) : credentials.AccessToken;
            this.accessSecret = string.IsNullOrWhiteSpace(credentials.AccessTokenSecret) ? throw new ArgumentNullException(nameof(accessSecret)) : credentials.AccessTokenSecret;
        }

        public string GetTwitterData(string resourceUrl)
        {
            var originalResource = resourceUrl;
            if (string.IsNullOrWhiteSpace(resourceUrl))
            {
                throw new ArgumentNullException();
            }

            List<string> parameterList;

            if (resourceUrl.Contains("?"))
            {
                parameterList = this.GetParametersFromUrl(resourceUrl);
                resourceUrl = resourceUrl.Substring(0, resourceUrl.IndexOf('?'));
            }

            else
            {
                parameterList = null;
            }

            string authHeader = this.BuildHeader(resourceUrl, parameterList);

            string jsonResponse = this.RequestFromTwitter(originalResource, authHeader);

            return jsonResponse;
        }

        private List<string> GetParametersFromUrl(string resourceUrl)
        {
            string querystring = resourceUrl.Substring(resourceUrl.IndexOf('?') + 1);

            var listtoreturn = new List<string>();

            var nv = HttpUtility.ParseQueryString(querystring);

            foreach (string parameter in nv)
            {
                listtoreturn.Add(parameter + "=" + Uri.EscapeDataString(nv[parameter]));
            }
            return listtoreturn;
        }

        private string BuildHeader(string resourceUrl, List<string> parameterList)
        {
            var nonce = Convert.ToBase64String(new ASCIIEncoding().GetBytes(DateTime.Now.Ticks.ToString()));
            var timespan = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var timestamp = Convert.ToInt64(timespan.TotalSeconds).ToString();

            var signature = GetSignature(nonce, timestamp, resourceUrl, parameterList);

            var headerFormat = "OAuth " +
            "oauth_consumer_key=\"{0}\", " +
            "oauth_nonce=\"{1}\", " +
            "oauth_signature=\"{2}\", " +
            "oauth_signature_method=\"{3}\", " +
            "oauth_timestamp=\"{4}\", " +
            "oauth_token=\"{5}\", " +
            "oauth_version=\"{6}\"";

            var authHeader = string.Format(headerFormat,
            Uri.EscapeDataString(consumerKey),
            Uri.EscapeDataString(nonce),
            Uri.EscapeDataString(signature),
            Uri.EscapeDataString(signatureMethod),
            Uri.EscapeDataString(timestamp),
            Uri.EscapeDataString(accessToken),
            Uri.EscapeDataString(version)
            );

            return authHeader;
        }

        private string GetSignature(string nonce, string timestamp, string resourceUrl, List<string> parameterList)
        {
            var baseString = this.GenerateBaseString(nonce, timestamp, parameterList);

            baseString = string.Concat("GET&", Uri.EscapeDataString(resourceUrl), "&", Uri.EscapeDataString(baseString));

            var signingKey = string.Concat(Uri.EscapeDataString(consumerSecret), "&", Uri.EscapeDataString(accessSecret));

            var hasher = new HMACSHA1(Encoding.ASCII.GetBytes(signingKey));

            var signature = Convert.ToBase64String(hasher.ComputeHash(Encoding.ASCII.GetBytes(baseString)));

            return signature;
        }

        private string GenerateBaseString(string nonce, string timestamp, List<string> parameterList)
        {
            var baseString = string.Empty;
            List<string> baseformat = new List<string>();
            baseformat.Add("oauth_consumer_key=" + consumerKey);
            baseformat.Add("oauth_nonce=" + nonce);
            baseformat.Add("oauth_signature_method=" + signatureMethod);
            baseformat.Add("oauth_timestamp=" + timestamp);
            baseformat.Add("oauth_token=" + accessToken);
            baseformat.Add("oauth_version=" + version);

            if (parameterList != null)
            {
                baseformat.AddRange(parameterList);
            }

            baseformat.Sort();

            foreach (var value in baseformat)
            {
                baseString += value + "&";
            }

            baseString = baseString.TrimEnd('&');

            return baseString;
        }

        private string RequestFromTwitter(string resourceUrl, string authHeader)
        {
            var request = (HttpWebRequest)WebRequest.Create(resourceUrl);
            request.Headers.Add("Authorization", authHeader);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";

            var response = request.GetResponse();

            string responseData = new StreamReader(response.GetResponseStream()).ReadToEnd();

            return responseData;
        }
    }
}