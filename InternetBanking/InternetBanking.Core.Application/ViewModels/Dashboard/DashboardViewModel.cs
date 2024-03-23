

namespace InternetBanking.Core.Application.ViewModels.Dashboard
{
    public class DashboardViewModel
    {
        public int Pays { get; set; }
        public int PaysToday {  get; set; }
        public int Transactions {  get; set; }
        public int TransactionsToday {  get; set; }
        public int ActiveUsers {  get; set; }
        public int DisabledUsers {  get; set; }
        public int QuantityProducts {  get; set; }
    }
}
