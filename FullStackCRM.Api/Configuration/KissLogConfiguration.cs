using FullStackCRM.Shared;
using KissLog.AspNetCore;
using KissLog.CloudListeners.RequestLogsListener;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullStackCRM.Api.Configuration
{
    public class KissLogConfiguration
    {
        public void ConfigureKissLog(IOptionsBuilder options)
        {
            options.InternalLog = (message) =>
            {
                Debug.WriteLine(message);
            };

            RegisterKissLogListeners(options);
        }

        public void RegisterKissLogListeners(IOptionsBuilder options)
        {
            options.Listeners.Add(new RequestLogsApiListener(new KissLog.CloudListeners.Auth.Application(
                ConfigurationHelper.KissLogConfiguration.OrganizationId,
                ConfigurationHelper.KissLogConfiguration.ApplicationId)
            )
            {
                ApiUrl = ConfigurationHelper.KissLogConfiguration.ApiUrl
            });
        }
    }
}
