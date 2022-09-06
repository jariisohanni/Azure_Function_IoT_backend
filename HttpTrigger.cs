using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data.SqlCommand;

namespace Saranen.Backend
{
    public static class HttpTrigger
    {
        [FunctionName("SimpleFunction")]
         public static void SimpleFunction(
        [MqttTrigger("my/topic/in")] IMqttMessage message,
        [Mqtt] out IMqttMessage outMessage,
        ILogger logger)
        {
            var body = message.GetMessage();
            var bodyString = Encoding.UTF8.GetString(body);
            logger.LogInformation($"{DateTime.Now:g} Message for topic {message.Topic}: {bodyString}");
            outMessage = new MqttMessage("testtopic/out", new byte[] { }, MqttQualityOfServiceLevel.AtLeastOnce, true);
        }

}

//Server=tcp:saranenio.database.windows.net,1433;Initial Catalog=saranenSample;Persist Security Info=False;User ID=saranen;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;