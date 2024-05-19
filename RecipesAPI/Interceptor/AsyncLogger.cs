using Castle.DynamicProxy;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Threading.Tasks;
using ILogger = Serilog.ILogger;

namespace RecipesAPI.Interceptor
{
    public class AsyncLogger : IInterceptor
    {
        private readonly ILogger _logger;
        private readonly IOptionsMonitor<LoggingOptions> _options;

        public IIdentity? identity { get; set; }

        public AsyncLogger(ILogger logger, IOptionsMonitor<LoggingOptions> options)
        {
            _logger = logger;
            _options = options;
        }

        public void Intercept(IInvocation invocation)
        {
            LogMethodInvocation(invocation);

            try
            {
                invocation.Proceed();
                var method = invocation.MethodInvocationTarget;
                var isAsync = method.GetCustomAttribute(typeof(AsyncStateMachineAttribute)) != null;
                if (isAsync && typeof(Task).IsAssignableFrom(method.ReturnType))
                {
                    invocation.ReturnValue = InterceptAsync((dynamic)invocation.ReturnValue, invocation);
                }
                else
                {
                    LogMethodReturn(invocation, invocation.ReturnValue);
                }
            }
            catch (Exception ex)
            {
                LogError(invocation, ex);
                throw;
            }
        }

        private async Task InterceptAsync(Task task, IInvocation invocation)
        {
            try
            {
                await task.ConfigureAwait(false);
                LogMethodCompletion(invocation);
            }
            catch (Exception ex)
            {
                LogError(invocation, ex);
                throw;
            }
        }

        private async Task<T> InterceptAsync<T>(Task<T> task, IInvocation invocation)
        {
            try
            {
                T result = await task.ConfigureAwait(false);
                LogMethodReturn(invocation, result);
                return result;
            }
            catch (Exception ex)
            {
                LogError(invocation, ex);
                throw;
            }
        }

        private void LogMethodInvocation(IInvocation invocation)
        {
            if (_options.CurrentValue.EnableLogging)
            {
                var user = GetCurrentUser();
                _logger.Information($"User: {user.Name}, Rights: {user.Rights}, Time: {DateTime.UtcNow}, Calling method: {invocation.Method.DeclaringType?.Name}.{invocation.Method.Name} with parameters {JsonConvert.SerializeObject(invocation.Arguments)}");
            }
        }

        private void LogMethodReturn(IInvocation invocation, object returnValue)
        {
            if (_options.CurrentValue.EnableLogging)
            {
                var serializationSettings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };
                var user = GetCurrentUser();
                _logger.Information($"User: {user.Name}, Rights: {user.Rights}, Time: {DateTime.UtcNow}, Method {invocation.Method.DeclaringType?.Name}.{invocation.Method.Name} returned {returnValue.GetType().Name}");
            }
        }

        private void LogMethodCompletion(IInvocation invocation)
        {
            if (_options.CurrentValue.EnableLogging)
            {
                var user = GetCurrentUser();
                _logger.Information($"User: {user.Name}, Rights: {user.Rights}, Time: {DateTime.UtcNow}, Method {invocation.Method.DeclaringType?.Name}.{invocation.Method.Name} completed");
            }
        }

        private void LogError(IInvocation invocation, Exception ex)
        {
            var user = GetCurrentUser();
            _logger.Error($"User: {user.Name}, Rights: {user.Rights}, Time: {DateTime.UtcNow}, Error happened in method: {invocation.Method.DeclaringType?.Name}.{invocation.Method.Name}. Error: {JsonConvert.SerializeObject(ex)}");
        }

        private (string Name, string Rights) GetCurrentUser()
        {
            if (identity != null)
            {
                return (identity.Name ?? "Empty", "Admin");
            }
            else
            {
                return (string.Empty, string.Empty);
            }
        }
    }
}
