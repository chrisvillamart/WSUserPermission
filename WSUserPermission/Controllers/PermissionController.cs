using Microsoft.AspNetCore.Mvc;
using Nest;
using Serilog;
using System.Web.Http;
using WSUserPermission.Entities;
using WSUserPermission.Kafka;
using WSUserPermission.Services;
using NC = Microsoft.AspNetCore.Mvc;


namespace WSUserPermission.Controllers
{
    [ApiController]
    [NC.Route("api/[controller]")]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _service;
        private readonly IElasticClient _elasticClient;
        private readonly IKafkaProducer _kafkaProducer;
        public PermissionController(IPermissionService srv, IElasticClient elasticClient, IKafkaProducer kafkaProducer)
        {
            _service = srv;
            _elasticClient = elasticClient;
            _kafkaProducer = kafkaProducer;
        }



        [NC.HttpPost]
        [NC.Route("requestPermission")]
        public PermissionResponse Insert([NC.FromBody] Permission permission)
        {
            Log.Information("POST Permission");
            var elasticResponse = _elasticClient.IndexDocument(permission);
            Log.Information($"elastic Indexing: {elasticResponse}");
            _kafkaProducer.SendKafka("POST");
            return (_service.RequestPermission(permission)); 
        }


        [NC.HttpGet]
        [NC.Route("getPermission")]
        public PermissionResponse Get()
        {
            Log.Information("GET Permission");
            var response = _service.GetPermission();
            _kafkaProducer.SendKafka("GET");
            return response;
        }

        [NC.HttpPut]
        [NC.Route("modifyPermission")]
        public PermissionResponse Put([NC.FromBody] Permission permission)
        {
            Log.Information("PUT Permission");
            var elasticResponse = _elasticClient.IndexDocument(permission);
            Log.Information($"elastic Indexing: {elasticResponse}");

            _kafkaProducer.SendKafka("PUT");
            return (_service.ModifyPermission(permission));
        }
    }
}
