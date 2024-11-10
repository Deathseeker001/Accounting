using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounting.DataLayer.context;
using Accounting.ViewModel.Accounting;

namespace Accounting.Business
{
    public class Account
    {
        public static ReportViewModel ReportFormMain()
        {
            ReportViewModel rp = new ReportViewModel();
            using (UnitOfWork db = new UnitOfWork())
            {
                DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 01);
                DateTime endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 30);

                var receive = db.AccountingRepository.Get(a => a.TypeID == 1 && a.DateTime >= startDate && a.DateTime <= endDate).Select(a => a.Amount).ToList();
                var pay = db.AccountingRepository.Get(a => a.TypeID == 2 && a.DateTime >= startDate && a.DateTime <= endDate).Select(a => a.Amount).ToList();

                rp.Receive = receive.Sum();
                rp.Pay = pay.Sum();
                rp.AccountBalance = (receive.Sum() - pay.Sum());
            }
            return rp;
        }
    }
}
