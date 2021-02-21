using System;
using System.Threading.Tasks;

namespace PaymentService.Services
{
    public interface IPaymentService
    {
        Task<Tuple<bool, string>> DoPaymentAsync(int walletId, int userId, decimal totalAmount);
    }
}