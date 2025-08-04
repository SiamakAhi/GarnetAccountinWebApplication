using Kavenegar;
using GarnetAccounting.Areas.Support.Dtos;
using GarnetAccounting.Areas.Support.SuportInterfaces;


namespace GarnetAccounting.Areas.Support.SupportServices
{
    public class SmsSenderService : ISmsSenderService
    {

        public async Task<SmsResultDto> Send_KavenegarAsync(string Reciptor, string Message)
        {

            KavenegarApi api = new Kavenegar.KavenegarApi("36704B2F6550656D77635667653046524173494C4265315A69766848687352685375714F38656F486D366B3D");
            try
            {
                //Message += " لغو11";
                var result = await api.Send("10008663", Reciptor, Message);
                return new() { Status = result.Status, StatusText = result.StatusText };

            }
            catch (Kavenegar.Core.Exceptions.ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                return new() { Status = ex.HResult, StatusText = ex.Message };
            }
            catch (Kavenegar.Core.Exceptions.HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                return new() { Status = ex.HResult, StatusText = ex.Message };
            }

        }
    }
}
