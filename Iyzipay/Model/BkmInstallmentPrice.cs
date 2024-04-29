using System;

namespace Iyzicore.Model
{
  public  class BkmInstallmentPrice : RequestStringConvertible
    {
        public int? InstallmentNumber { get; set; }
        public string TotalPrice { get; set; }

        public string ToPKIRequestString()
        {
            return ToStringRequestBuilder.NewInstance()
                .Append("installmentNumber", InstallmentNumber)
                .AppendPrice("totalPrice", TotalPrice)
                .GetRequestString();
        }
    }
}
