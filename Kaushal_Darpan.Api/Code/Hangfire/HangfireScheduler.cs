using Hangfire;
using Kaushal_Darpan.Models.CommonModel;

namespace Kaushal_Darpan.Api.Code.Hangfire
{
    public class HangfireScheduler
    {
        public static void RegisterJobs()
        {            
            RecurringJob.RemoveIfExists("sidh-data-export");

            RecurringJob.AddOrUpdate<HangfireJob>(
                "sidh-data-export",
                x => x.SidhDataExport(),
                "0 * * * *"
            );


        }
    }
}
