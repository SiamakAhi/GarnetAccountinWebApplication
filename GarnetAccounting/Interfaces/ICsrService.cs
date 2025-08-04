using GarnetAccounting.ViewModels.Tax;

namespace GarnetAccounting.Interfaces
{
    public interface ICsrService
    {
        CsrResult GenerateCsrForHoghooghi(CsrInfoHoghooghi csrInfo);
        CsrResult GenerateCsrForHaghighi(CsrInfoHaghighi csrInfo);
        string LoadPrivateKey(string privateKeyPEM);
    }
}
