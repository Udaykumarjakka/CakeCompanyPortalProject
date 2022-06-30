using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeCompanyPortal.Helper
{
    enum Cake
    {
        Chocolate,
        Vanilla,
        RedVelvet
    }

    enum PaymentIn
    {
        IsSuccessful,
        HasCreditLimit
    }

    static class OrderStatus
    {
        public const string Created  = "Created";
        public const string Preparing  = "Preparing";
        public const string Shipped  = "Shipped";
        public const string Delivered  = "Delivered";
        public const string Rejected  = "Rejected";
        public const string Completed  = "Completed";
        
    }

    static class BillStatus
    {
        public const string NotPaid = "NotPaid";
        public const string Paid = "Paid";
    }

    public enum LogType
    {
        Information = 0,
        Success = 1,
        Error = 2,
        Warning = 3,
    }

}
