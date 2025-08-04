using GarnetAccounting.Areas.Support.Dtos;

namespace GarnetAccounting.Areas.Support.SuportInterfaces
{
    public interface ISmsSenderService
    {
        Task<SmsResultDto> Send_KavenegarAsync(string Reciptor, string Message);
    }
}
