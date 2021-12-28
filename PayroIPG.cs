using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Payro
{
    public class PayroIPG
    {
        private string BaseURL = "https://api.payro24.ir/v1.0/";
        private bool IsSandBox = false;
        private string Token = "5ff45e647432213b542d5937-xboag34zvq7fhihhhq80cuses";
        private PayroMessage? errorMessage = null;

        public PayroIPG(string p_token,bool is_sandbox = false)
        {
            this.Token = p_token;
            this.IsSandBox = is_sandbox;
        }

        public PayroCreated? payment(string callback_url, string order_id, long amount, string name, string email, string mobile, string description)
        {
            PayroCreated? result = null;
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), this.BaseURL + "payment"))
                {
                    request.Headers.TryAddWithoutValidation("P-TOKEN", this.Token);
                    request.Headers.TryAddWithoutValidation("P-SANDBOX", (this.IsSandBox ? "1" : "0"));

                    request.Content = new StringContent("{  \"order_id\": \"" + order_id + "\",  \"amount\": " + amount + ",  \"name\": \"" + name + "\",  \"phone\": \"" + mobile + "\",  \"mail\": \"" + email + "\",  \"desc\": \"" + description + "\",\n  \"callback\": \"" + callback_url + "\"}");
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    HttpResponseMessage response = httpClient.Send(request);
                    if (response.Content != null)
                    {
                        Stream receiveStream = response.Content.ReadAsStream();
                        StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                        string jsonString = readStream.ReadToEnd();
                        if (response.StatusCode == System.Net.HttpStatusCode.Created)
                        {
                            result = JsonSerializer.Deserialize<PayroCreated>(jsonString);
                        }
                        else
                        {
                            this.errorMessage = JsonSerializer.Deserialize<PayroMessage>(jsonString);
                        }
                    }


                }
                return result;
            }
        }

        public PayroPaymentInfo? verify(string id, string order_id)
        {
            PayroPaymentInfo? result = null;
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), this.BaseURL + "payment/verify"))
                {
                    request.Headers.TryAddWithoutValidation("P-TOKEN", this.Token);
                    request.Headers.TryAddWithoutValidation("P-SANDBOX", (this.IsSandBox ? "1" : "0"));

                    request.Content = new StringContent("{  \"order_id\": \"" + order_id + "\", \"id\": \"" + id + "\"}");
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    HttpResponseMessage response = httpClient.Send(request);
                    if (response.Content != null)
                    {
                        Stream receiveStream = response.Content.ReadAsStream();
                        StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                        string jsonString = readStream.ReadToEnd();
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            result = JsonSerializer.Deserialize<PayroPaymentInfo>(jsonString);
                        }
                        else
                        {
                            this.errorMessage = JsonSerializer.Deserialize<PayroMessage>(jsonString);
                        }
                    }


                }
                return result;
            }
        }

        public PayroMessage getError()
        {
            if(this.errorMessage == null)
            {
                PayroMessage error = new PayroMessage();
                error.error_code = "55";
                error.error_code = "Connection Error!";
                return error;
            }
            else
            {
                return this.errorMessage;
            }
        }
    }
    public class PayroMessage
    {
        public string error_code { get; set; }
        public string error_message { get; set; }
    }

    public class PayroCreated
    {
        public string id { get; set; }
        public string link { get; set; }
    }

    public class PayroPaymentInfo
    {
        public string status { get; set; }
        public string track_id { get; set; }
        public string id { get; set; }
        public string order_id { get; set; }
        public string amount { get; set; }
        public Payment payment { get; set; }

        public class Payment
        {
            public string track_id { get; set; }
            public string amount { get; set; }

        }
    }

}
