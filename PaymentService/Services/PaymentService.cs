using System;
using System.Threading.Tasks;
using PaymentService.Services;

namespace PaymentService.Services
{
    public class PaymentService : IPaymentService
    {
        public Task<Tuple<bool, string>> DoPaymentAsync(int walletId, int userId, decimal totalAmount)
        {
            throw new NotImplementedException();
        }
    }
}