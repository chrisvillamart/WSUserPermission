using Microsoft.AspNetCore.Mvc.Filters;
using Nest;
using Serilog;
using WSUserPermission.Utils;

namespace WSUserPermission.Filters
{
    public class ElasticSearchFilter : IActionFilter
    {
        private readonly IElasticClient _elasticClient;

        public ElasticSearchFilter(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.Request.Method.ToString() == "GET" )
            {

                var elasticSave = UtilClass.ProcessElasticSave(context.Result);
                var elasticResponse =_elasticClient.IndexDocument(elasticSave);
                Log.Information($"elastic Indexing: {elasticResponse}");
            }

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
