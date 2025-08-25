using Kaushal_Darpan.Core.Interfaces;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Kaushal_Darpan.Models.SMSService;
using Kaushal_Darpan.Models.SMSConfigurationSetting;
using AutoMapper;
using System.Data;
using Kaushal_Darpan.Api.Email;
using Kaushal_Darpan.Core.Helper;
public interface IMyService
{
    Task FetchDataAsync();
}

public class MyService : IMyService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly SMSConfigurationSettingModel _sMSConfigurationSetting;
    public MyService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _sMSConfigurationSetting = unitOfWork.SMSMailRepository.GetSMSConfigurationSetting().Result;
    }

    public async Task FetchDataAsync()
    {
       

        var smsList = new List<SmsNotification>();


        DataTable dataTable = await _unitOfWork.SMSSchedulerRepository.GetAllUnsentMsgs();

        //while (await data.ReadAsync())
        //{
        //    smsList.Add(new SmsNotification
        //    {
        //        Id = data.GetInt32(0),
        //        MobileNumber = data.GetString(1),
        //        Message = data.GetString(2)
        //    });
        //}

        foreach (DataRow row in dataTable.Rows)
        {
            smsList.Add(new SmsNotification
            {
                Id = row["Id"] != DBNull.Value ? Convert.ToInt32(row["Id"]) : 0,
                MobileNumber = row["mobileno"] != DBNull.Value ? row["mobileno"].ToString() : string.Empty,
                Message = row["MessageText"] != DBNull.Value ? row["MessageText"].ToString() : string.Empty
            });
        }

        foreach (var sms in smsList)
        {
            var result = await CommonFuncationHelper.SendSMS(_sMSConfigurationSetting, sms.MobileNumber, sms.Message, "33333");

            if (result == "Request submitted successfully")
            {

                await _unitOfWork.SMSSchedulerRepository.MarkAsSentAsync(sms.Id);
            }
        }


    }
}















//// SmsSchedulerService.cs
//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Configuration;
//using System.Data;
//using System.Data.SqlClient;
//using Kaushal_Darpan.Models.SMSService;
//using Kaushal_Darpan.Core.Helper;
//using Kaushal_Darpan.Models.SMSConfigurationSetting;
//using Kaushal_Darpan.Core.Interfaces;
//using AutoMapper;
//using Kaushal_Darpan.Api.Email;
//using Kaushal_Darpan.Infra.Repositories;
//public class SchedulerService : BackgroundService
//{
//    private readonly IServiceScopeFactory _scopeFactory;
//    private readonly SMSConfigurationSettingModel _sMSConfigurationSetting;
//    public SchedulerService(IServiceScopeFactory scopeFactory)
//    {
//        _scopeFactory = scopeFactory;
//        using var scope = _scopeFactory.CreateScope();
//        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

//        _sMSConfigurationSetting = unitOfWork.SMSMailRepository.GetSMSConfigurationSetting().Result;
//    }

//    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//    {


//        // Use unitOfWork here...

//        while (!stoppingToken.IsCancellationRequested)
//        {
//            await ProcessSmsAsync();
//            await Task.Delay(TimeSpan.FromHours(3), stoppingToken);
//        }
//    }
//    //private readonly IMapper _mapper;
//    //private readonly EmailService _emailService;
//    //private readonly IUnitOfWork _unitOfWork;
//    //private readonly DataTable _dataTable_Master = new DataTable();
//    //private readonly SMSConfigurationSettingModel _sMSConfigurationSetting;
//    //public SchedulerService(IMapper mapper, IUnitOfWork unitOfWork, EmailService emailService)
//    //{
//    //    _mapper = mapper;
//    //    _emailService = emailService;
//    //    _unitOfWork = unitOfWork;
//    //    _sMSConfigurationSetting = _unitOfWork.SMSMailRepository.GetSMSConfigurationSetting().Result;
//    //}

//    //protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//    //{
//    //    while (!stoppingToken.IsCancellationRequested)
//    //    {
//    //        await ProcessSmsAsync();
//    //        await Task.Delay(TimeSpan.FromHours(3), stoppingToken);
//    //    }
//    //}

//    private async Task ProcessSmsAsync()
//    {
//        var smsList = new List<SmsNotification>();

//        using var scope = _scopeFactory.CreateScope();
//        var _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

//        DataTable dataTable = await _unitOfWork.SMSSchedulerRepository.GetAllUnsentMsgs();

//        //while (await data.ReadAsync())
//        //{
//        //    smsList.Add(new SmsNotification
//        //    {
//        //        Id = data.GetInt32(0),
//        //        MobileNumber = data.GetString(1),
//        //        Message = data.GetString(2)
//        //    });
//        //}

//        foreach (DataRow row in dataTable.Rows)
//        {
//            smsList.Add(new SmsNotification
//            {
//                Id = row["Id"] != DBNull.Value ? Convert.ToInt32(row["Id"]) : 0,
//                MobileNumber = row["mobileno"] != DBNull.Value ? row["mobileno"].ToString() : string.Empty,
//                Message = row["MessageText"] != DBNull.Value ? row["MessageText"].ToString() : string.Empty
//            });
//        }

//        foreach (var sms in smsList)
//        {
//            CommonFuncationHelper.SendSMS(_sMSConfigurationSetting, sms.MobileNumber, sms.Message, "33333");

//            //await MarkAsSentAsync(sms.Id);
//        }
//    }

//    //private async Task MarkAsSentAsync(int id)
//    //{
//    //    using SqlConnection conn = new(_connectionString);
//    //    await conn.OpenAsync();

//    //    using SqlCommand cmd = new("GetPendingSmsNotifications", conn)
//    //    {
//    //        CommandType = CommandType.StoredProcedure
//    //    };
//    //    cmd.Parameters.AddWithValue("@Id", id);
//    //    cmd.Parameters.AddWithValue("@Action", "update");
//    //    await cmd.ExecuteNonQueryAsync();
//    //}

//    private Task<bool> SendSmsAsync(string mobileNumber, string message)
//    {
//        Console.WriteLine($"Sending SMS to {mobileNumber}: {message}");
//        return Task.FromResult(true);
//    }
//}

