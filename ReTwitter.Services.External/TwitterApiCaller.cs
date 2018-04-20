using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
            this.consumerKey = credentials.ConsumerKey;
            this.consumerSecret = credentials.ConsumerSecret;
            this.accessToken = credentials.AccessToken;
            this.accessSecret = credentials.AccessTokenSecret;
        }

        public string GetTwitterData(string resourceurl)
        {
            List<string> parameterlist;
            if (resourceurl.Contains("?"))
            {
                parameterlist = GetParametersFromUrl(resourceurl);
                resourceurl = resourceurl.Substring(0, resourceurl.IndexOf('?'));
            }

            else
            {
                parameterlist = null;
            }

            string authheader = BuildHeader(resourceurl, parameterlist);

            string jsonresponse = this.TwitterWebRequest(resourceurl, authheader, parameterlist);

            return jsonresponse;
        }

        private List<string> GetParametersFromUrl(string resourceUrl)
        {
            string querystring = resourceUrl.Substring(resourceUrl.IndexOf('?') + 1);

            List<string> listtoreturn = new List<string>();

            NameValueCollection nv = HttpUtility.ParseQueryString(querystring);

            foreach (string parameter in nv)
            {
                listtoreturn.Add(parameter + "=" + Uri.EscapeDataString(nv[parameter]));
            }
            return listtoreturn;
        }

        private string BuildHeader(string resourceurl, List<string> parameterlist)
        {
            string nonce = Convert.ToBase64String(new ASCIIEncoding().GetBytes(DateTime.Now.Ticks.ToString()));
            TimeSpan timespan = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            string timestamp = Convert.ToInt64(timespan.TotalSeconds).ToString();

            string signature = GetSignature(nonce, timestamp, resourceurl, parameterlist);

            var HeaderFormat = "OAuth " +
            "oauth_consumer_key=\"{0}\", " +
            "oauth_nonce=\"{1}\", " +
            "oauth_signature=\"{2}\", " +
            "oauth_signature_method=\"{3}\", " +
            "oauth_timestamp=\"{4}\", " +
            "oauth_token=\"{5}\", " +
            "oauth_version=\"{6}\"";

            string authHeader = string.Format(HeaderFormat,
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

        private string GetSignature(string nonce, string timestamp, string resourceurl, List<string> parameterlist)
        {
            string baseString = GenerateBaseString(nonce, timestamp, parameterlist);

            baseString = string.Concat("GET&", Uri.EscapeDataString(resourceurl), "&", Uri.EscapeDataString(baseString));

            var signingKey = string.Concat(Uri.EscapeDataString(consumerSecret), "&", Uri.EscapeDataString(accessSecret));
            string signature;

            HMACSHA1 hasher = new HMACSHA1(Encoding.ASCII.GetBytes(signingKey));

            signature = Convert.ToBase64String(hasher.ComputeHash(Encoding.ASCII.GetBytes(baseString)));

            return signature;
        }

        private string GenerateBaseString(string nonce, string timestamp, List<string> parameterlist)
        {
            string basestring = "";
            List<string> baseformat = new List<string>();
            baseformat.Add("oauth_consumer_key=" + consumerKey);
            baseformat.Add("oauth_nonce=" + nonce);
            baseformat.Add("oauth_signature_method=" + signatureMethod);
            baseformat.Add("oauth_timestamp=" + timestamp);
            baseformat.Add("oauth_token=" + accessToken);
            baseformat.Add("oauth_version=" + version);

            if (parameterlist != null)
            {
                baseformat.AddRange(parameterlist);
            }

            baseformat.Sort();

            foreach (string value in baseformat)
            {
                basestring += value + "&";
            }

            basestring = basestring.TrimEnd('&');

            return basestring;
        }

        private string TwitterWebRequest(string resourceurl, string authheader, List<string> parameterlist)
        {
            ServicePointManager.Expect100Continue = false;

            string postBody;

            postBody = parameterlist != null ? GetPostBody(parameterlist) : "";

            resourceurl += "?" + postBody;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(resourceurl);
            request.Headers.Add("Authorization", authheader);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";

            WebResponse response = request.GetResponse();

            string responseData = new StreamReader(response.GetResponseStream()).ReadToEnd();

            return responseData;

        }

        private string GetPostBody(List<string> parameterlist)
        {
            string stringtoreturn = "";

            foreach (string item in parameterlist)
            {
                stringtoreturn += item + "&";

            }
            stringtoreturn = stringtoreturn.TrimEnd('&');
            return stringtoreturn;
        }
    }
}
