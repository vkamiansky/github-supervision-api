using System;
using OpenTracing;
using Jaeger;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Fls.Supervision.Api
{
    public static class JaegerHelper
    {
        private const string JaegerServiceName = "supervision";
        private const string JaegerAgentHost = "84.201.135.181";
        private const string JaegerAgentPort = "6831";
        private const string JaegerSamplerType = "const";
        public static ITracer CreateTracer()
        {
            Environment.SetEnvironmentVariable("JAEGER_SERVICE_NAME", JaegerServiceName);
            Environment.SetEnvironmentVariable("JAEGER_AGENT_HOST", JaegerAgentHost);
            Environment.SetEnvironmentVariable("JAEGER_AGENT_PORT", JaegerAgentPort);
            Environment.SetEnvironmentVariable("JAEGER_SAMPLER_TYPE", JaegerSamplerType);
            var loggerFactory = new LoggerFactory();
            var config = Jaeger.Configuration.FromEnv(loggerFactory);
            return config.GetTracer();
        }

        private static void TraceError(Exception error, ISpan span)
        {
            try
            {
                span.SetTag("error", true);
                span.SetTag("error_type", error.GetType().Name);
                span.SetTag("error_text", error.Message);
                span.Log(new Dictionary<string, object> { { "exception", error } });
                return;
            }
            catch { }
        }

        public static T Trace<T>(this ITracer tracer, string operationName, Func<T> operation)
        {
            if (tracer == null)
            {
                return operation();
            }
            else
            {
                var builder = tracer.BuildSpan(operationName);
                using (var scope = builder.StartActive(true))
                {
                    var span = scope.Span;
                    try{
                        return operation();
                    }
                    catch(Exception ex)
                    {
                        TraceError(ex, span);
                        return default(T);
                        //throw ex;
                    }
                }
            }
        }
    }
}