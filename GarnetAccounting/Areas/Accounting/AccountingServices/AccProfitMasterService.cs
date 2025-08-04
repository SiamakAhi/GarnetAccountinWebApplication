using GarnetAccounting.Areas.Accounting.AccountingInterfaces;
using GarnetAccounting.Areas.Accounting.Dto;
using GarnetAccounting.Models;
using Microsoft.EntityFrameworkCore;

namespace GarnetAccounting.Areas.Accounting.AccountingServices
{
    public class AccProfitMasterService : IAccProfitMasterService
    {
        private readonly AppDbContext _db;
        public AccProfitMasterService(AppDbContext db)
        {
            _db = db;
        }

        //مانده گیری
        public async Task<List<AccountBalanceDto>> AccountsBalanceAsync(long sellerId, int periodId, List<int> AccountsIds, string? startDate = null, string? endDate = null, bool justPeriod = false)
        {
            DateTime? sDate = null;
            DateTime? eDate = null;
            if (!string.IsNullOrEmpty(startDate)) sDate = startDate.PersianToLatin();
            if (!string.IsNullOrEmpty(endDate)) eDate = endDate.PersianToLatin();

            var query = _db.Acc_Articles.Include(n => n.Moein).Where(n =>
            n.Doc.SellerId == sellerId
            && n.Doc.PeriodId == periodId
            && !n.IsDeleted && !n.Doc.IsDeleted
            && AccountsIds.Contains(n.MoeinId)).AsQueryable();
            if (sDate.HasValue)
                query = query.Where(n => n.Doc.DocDate >= sDate.Value);
            if (eDate.HasValue)
                query = query.Where(n => n.Doc.DocDate <= eDate.Value);


            var arts = await query.ToListAsync();

            var balances = arts.GroupBy(n => n.MoeinId).Select(n => new AccountBalanceDto
            {
                AccountId = n.Key,
                AccountCode = n.Max(x => x.Moein.MoeinCode),
                AccountName = n.Max(x => x.Moein.MoeinName),
                AccountNature = n.Max(x => x.Moein.Nature),
                TotalBed = n.Sum(x => x.Bed),
                TotalBes = n.Sum(x => x.Bes),
                TotalBalance = n.Sum(x => x.Bed) > n.Sum(x => x.Bes) ? n.Sum(x => x.Bed) - n.Sum(x => x.Bes) : n.Sum(x => x.Bes) - n.Sum(x => x.Bed),
                BalanceNature = n.Sum(x => x.Bed) > n.Sum(x => x.Bes) ? 1 : 2,

            }).ToList();

            return balances;
        }


        //محاسبه درآمد و فروش خالص برای صورت سود و زیان
        //دریافت حساب های فروش و درآمد
        public async Task<List<int>> GetIncomeAccountsAsync(long sellerId, List<int> incomKols)
        {
            List<int>? incomeAccounts = await _db.Acc_Coding_Moeins.Where(n =>
               n.SellerId == sellerId
               && incomKols.Contains(n.KolId)
               && n.Nature == 2).Select(n => n.Id).ToListAsync<int>();

            return incomeAccounts;
        }
        public async Task<List<int>> GetIncomeAccountsByGroupsAsync(long sellerId, List<int> incomeGroups)
        {
            List<int>? incomeAccounts = await _db.Acc_Coding_Moeins.Where(n =>
               n.SellerId == sellerId
               && incomeGroups.Contains(n.MoeinKol.GroupId)
               && n.MoeinKol.Nature == 2
               && n.Nature == 2).Select(n => n.Id).ToListAsync<int>();
            return incomeAccounts;
        }
        public async Task<List<int>> GetIncomeDiscountAccountsByGroupsAsync(long sellerId, List<int> incomeGroups)
        {
            List<int>? incomeAccounts = await _db.Acc_Coding_Moeins.Where(n =>
               n.SellerId == sellerId
               && incomeGroups.Contains(n.MoeinKol.GroupId)
               && n.Nature == 1).Select(n => n.Id).ToListAsync<int>();
            return incomeAccounts;
        }
        //دریافت حساب های تخفیفات و برگشت از فروش
        public async Task<List<int>> GetIncomeDiscountAndReturnAccountsAsync(long sellerId, List<int> DiscountKols)
        {
            List<int>? acounts = await _db.Acc_Coding_Moeins.Where(n =>
               n.SellerId == sellerId
               && DiscountKols.Contains(n.KolId)
               && n.Nature == 1).Select(n => n.Id).ToListAsync<int>();

            return acounts;
        }
        public async Task<List<ProfitMasterDto>> NetIncomeAsync(long sellerId, int periodId, List<int>? incomeGroups, string? startDate = null, string? endDate = null)
        {
            List<ProfitMasterDto> netIncomeSection = new List<ProfitMasterDto>();
            long sumIncome = 0;
            long sumDiscount = 0;
            //----------------------------------------- محاسبه فروش و درآمد ناخالص
            if (incomeGroups != null)
            {
                List<int> income_accounts = await GetIncomeAccountsByGroupsAsync(sellerId, incomeGroups);
                var income = await AccountsBalanceAsync(sellerId, periodId, income_accounts, startDate, endDate);
                foreach (var x in income.OrderByDescending(n => n.TotalBalance))
                {
                    ProfitMasterDto n = new ProfitMasterDto();
                    n.Title = x.AccountName;
                    n.Amount = x.TotalBalance;
                    n.SumAmounts = null;
                    n.TotalSection = null;
                    n.IsAdd = true;
                    n.cssClassName = "profit-amount";
                    netIncomeSection.Add(n);
                }
                sumIncome = income.Sum(n => n.TotalBalance);
                ProfitMasterDto nakhales = new ProfitMasterDto();
                nakhales.Title = "جمع فروش و درآمدها";
                nakhales.SumAmounts = sumIncome;
                nakhales.TotalSection = null;
                nakhales.Amount = null;
                nakhales.IsAdd = true;
                nakhales.cssClassName = "profit-sum";
                netIncomeSection.Add(nakhales);

                //------------------------------------------- تخفیفات و برگشت
                List<int> disAccounts = await GetIncomeDiscountAccountsByGroupsAsync(sellerId, incomeGroups);
                var discount = await AccountsBalanceAsync(sellerId, periodId, disAccounts, startDate, endDate);
                foreach (var x in discount.OrderByDescending(n => n.TotalBalance))
                {
                    ProfitMasterDto n = new ProfitMasterDto();
                    n.Title = x.AccountName;
                    n.Amount = x.TotalBalance;
                    n.SumAmounts = null;
                    n.TotalSection = null;
                    n.IsAdd = false;
                    n.cssClassName = "profit-amount";
                    netIncomeSection.Add(n);
                }
                sumDiscount = discount.Sum(n => n.TotalBalance);
                ProfitMasterDto totalDiscount = new ProfitMasterDto();
                totalDiscount.Title = "جمع تخفیفات و برگشت از فروش";
                totalDiscount.SumAmounts = sumDiscount;
                totalDiscount.IsAdd = false;
                totalDiscount.cssClassName = "profit-sum";
                netIncomeSection.Add(totalDiscount);
            }

            //------------------------------------------------ خالص فروش و درآمد
            ProfitMasterDto total = new ProfitMasterDto();
            total.Title = "خالص فروش و درآمد";
            total.TotalSection = sumIncome - sumDiscount;
            total.IsAdd = true;
            total.cssClassName = "profit-total";
            netIncomeSection.Add(total);

            return netIncomeSection;

        }

        //=================================================
        // فروش ناخالص طی دوره
        public async Task<long> CalcSaleAsync(long sellerId, int periodId, List<int>? SaleKols, string? startDate = null, string? endDate = null)
        {
            long totalSale = 0;
            if (SaleKols == null) return totalSale;

            List<int> saleAccounts = await GetIncomeAccountsAsync(sellerId, SaleKols);
            var saleMande = await AccountsBalanceAsync(sellerId, periodId, saleAccounts, startDate, endDate);
            totalSale = saleMande.Sum(n => n.TotalBalance);
            return totalSale;
        }
        //تخفیفات و برگشت از فروش طی دوره
        public async Task<long> CalcSaleDiscountAsync(long sellerId, int periodId, List<int>? SaleDiscountKols, string? startDate = null, string? endDate = null)
        {
            long totalSaleDiscount = 0;
            if (SaleDiscountKols == null) return totalSaleDiscount;

            List<int> saleDiscountAccounts = await GetIncomeDiscountAndReturnAccountsAsync(sellerId, SaleDiscountKols);
            var saleMande = await AccountsBalanceAsync(sellerId, periodId, SaleDiscountKols, startDate, endDate);
            totalSaleDiscount = saleMande.Sum(n => n.TotalBalance);
            return totalSaleDiscount;
        }
        //فروش خالص
        // فروش خالص = فروش - تخفیفات و برگشت از فروش
        public async Task<long> CalcNetSaleAsync(long sellerId, int periodId, List<int>? SaleKols, List<int>? SaleDiscountKols, string? startDate = null, string? endDate = null)
        {
            long sale = await CalcSaleAsync(sellerId, periodId, SaleKols);
            long discount = await CalcSaleDiscountAsync(sellerId, periodId, SaleDiscountKols);

            return (sale - discount);

        }
        // گزارش فروش برای صورت سود و زیان
        public async Task<List<ProfitMasterDto>> ProfitReport_SaleAsync(long sellerId, int periodId, List<int>? SaleKols, List<int>? SaleDiscountKols, string? startDate = null, string? endDate = null)
        {
            List<ProfitMasterDto> netIncomeSection = new List<ProfitMasterDto>();
            long sumSale = 0;
            long sumDiscount = 0;
            //----------------------------------------- محاسبه فروش ناخالص
            if (SaleKols != null)
            {
                List<int> sale_accounts = await GetIncomeAccountsAsync(sellerId, SaleKols);
                var sale = await AccountsBalanceAsync(sellerId, periodId, sale_accounts, startDate, endDate);
                foreach (var x in sale.OrderByDescending(n => n.TotalBalance))
                {
                    ProfitMasterDto n = new ProfitMasterDto();
                    n.Title = x.AccountName;
                    n.Amount = x.TotalBalance;
                    n.SumAmounts = null;
                    n.TotalSection = null;
                    n.IsAdd = true;
                    n.cssClassName = "profit-amount";
                    netIncomeSection.Add(n);
                }
                sumSale = sale.Sum(n => n.TotalBalance);
                ProfitMasterDto nakhales = new ProfitMasterDto();
                nakhales.Title = "جمع فروش";
                nakhales.SumAmounts = sumSale;
                nakhales.TotalSection = null;
                nakhales.Amount = null;
                nakhales.IsAdd = true;
                nakhales.cssClassName = "profit-sum";
                netIncomeSection.Add(nakhales);

                //------------------------------------------- تخفیفات و برگشت
                List<int> disAccounts = await GetIncomeDiscountAndReturnAccountsAsync(sellerId, SaleDiscountKols);
                var discount = await AccountsBalanceAsync(sellerId, periodId, disAccounts, startDate, endDate);
                foreach (var x in discount.OrderByDescending(n => n.TotalBalance))
                {
                    ProfitMasterDto n = new ProfitMasterDto();
                    n.Title = x.AccountName;
                    n.Amount = x.TotalBalance;
                    n.SumAmounts = null;
                    n.TotalSection = null;
                    n.IsAdd = false;
                    n.cssClassName = "profit-amount";
                    netIncomeSection.Add(n);
                }
                sumDiscount = discount.Sum(n => n.TotalBalance);
                ProfitMasterDto totalDiscount = new ProfitMasterDto();
                totalDiscount.Title = "جمع تخفیفات و برگشت از فروش";
                totalDiscount.SumAmounts = sumDiscount;
                totalDiscount.IsAdd = false;
                totalDiscount.cssClassName = "profit-sum minez";
                netIncomeSection.Add(totalDiscount);
            }

            //------------------------------------------------ خالص فروش
            ProfitMasterDto total = new ProfitMasterDto();
            total.Title = "خالص فروش";
            total.TotalSection = sumSale - sumDiscount;
            total.IsAdd = true;
            total.cssClassName = "profit-total";
            netIncomeSection.Add(total);

            return netIncomeSection;

        }
        //
        //
        public async Task<long> CalcIncomeAsync(long sellerId, int periodId, List<int>? IncomeKols, string? startDate = null, string? endDate = null)
        {
            long totalIncome = 0;
            if (IncomeKols == null) return totalIncome;

            List<int> incomeAccounts = await GetIncomeAccountsAsync(sellerId, IncomeKols);
            var incomeMande = await AccountsBalanceAsync(sellerId, periodId, incomeAccounts, startDate, endDate);
            totalIncome = incomeMande.Sum(n => n.TotalBalance);
            return totalIncome;
        }
        // گزارش درآمد برای صورت سود و زیان
        public async Task<List<ProfitMasterDto>> ProfitReport_IncomeAsync(long sellerId, int periodId, List<int>? IncomeKols, string? startDate = null, string? endDate = null)
        {
            List<ProfitMasterDto> netIncomeSection = new List<ProfitMasterDto>();
            long sumSale = 0;
            //----------------------------------------- محاسبه درآمدها
            if (IncomeKols != null)
            {
                List<int> income_accounts = await GetIncomeAccountsAsync(sellerId, IncomeKols);
                var income = await AccountsBalanceAsync(sellerId, periodId, income_accounts, startDate, endDate);
                foreach (var x in income.OrderByDescending(n => n.TotalBalance))
                {
                    ProfitMasterDto n = new ProfitMasterDto();
                    n.Title = x.AccountName;
                    n.Amount = x.TotalBalance;
                    n.SumAmounts = null;
                    n.TotalSection = null;
                    n.IsAdd = true;
                    n.cssClassName = "profit-amount";
                    netIncomeSection.Add(n);
                }
                sumSale = income.Sum(n => n.TotalBalance);
                ProfitMasterDto nakhales = new ProfitMasterDto();
                nakhales.Title = "جمع درآمدها";
                nakhales.SumAmounts = sumSale;
                nakhales.TotalSection = null;
                nakhales.Amount = null;
                nakhales.IsAdd = true;
                nakhales.cssClassName = "profit-sum";
                netIncomeSection.Add(nakhales);
            }
            return netIncomeSection;
        }
        //نمایش جمع فروش و درآمدها
        public async Task<ProfitMasterDto> ProfitReport_NetSaleAndIncomeAsync(long sellerId, int periodId, List<int>? IncomeKols, List<int>? saleKols, List<int>? saleDiscountKols, string? startDate = null, string? endDate = null)
        {
            long netSale = await CalcNetSaleAsync(sellerId, periodId, saleKols, saleDiscountKols);
            long incomes = await CalcIncomeAsync(sellerId, periodId, IncomeKols);

            ProfitMasterDto total = new ProfitMasterDto();
            total.Title = "جمع فروش و درآمدها";
            total.TotalSection = netSale + incomes;
            total.IsAdd = false;
            total.cssClassName = "profit-total";

            return total;
        }


        //====================================================
        //=== خالص خرید
        //====================================================
        //دریافت حساب های خرید
        public async Task<List<int>> GetBuyAccountsAsync(long sellerId, List<int> buyKols, string? startDate = null, string? endDate = null)
        {
            List<int>? incomeAccounts = await _db.Acc_Coding_Moeins.Where(n =>
               n.SellerId == sellerId
               && buyKols.Contains(n.KolId)
               && n.Nature == 1).Select(n => n.Id).ToListAsync<int>();

            return incomeAccounts;
        }
        public async Task<List<int>> GetBuyAccountsByGroupsAsync(long sellerId, List<int> buyGroups, string? startDate = null, string? endDate = null)
        {
            List<int>? buyAccounts = await _db.Acc_Coding_Moeins.Where(n =>
               n.SellerId == sellerId
               && buyGroups.Contains(n.MoeinKol.GroupId)
               && n.Nature == 1).Select(n => n.Id).ToListAsync<int>();

            return buyAccounts;
        }
        public async Task<List<int>> GetDiscountBuyAccountsByGroupsAsync(long sellerId, List<int> buyGroups, string? startDate = null, string? endDate = null)
        {
            List<int>? buyAccounts = await _db.Acc_Coding_Moeins.Where(n =>
               n.SellerId == sellerId
               && buyGroups.Contains(n.MoeinKol.GroupId)
               && n.Nature == 2).Select(n => n.Id).ToListAsync<int>();

            return buyAccounts;
        }
        public async Task<List<int>> GetBuyDiscountAndReturnAccountsAsync(long sellerId, List<int> DiscountKols, string? startDate = null, string? endDate = null)
        {
            List<int>? acounts = await _db.Acc_Coding_Moeins.Where(n =>
               n.SellerId == sellerId
               && DiscountKols.Contains(n.KolId)
               && n.Nature == 2).Select(n => n.Id).ToListAsync<int>();

            return acounts;
        }
        //محاسبه خالص خرید
        public async Task<long> GetNetBuyAsync(long sellerId, int periodId, List<int>? buyGroups, string? startDate = null, string? endDate = null)
        {
            long sumBuy = 0;
            long sumDiscount = 0;
            if (buyGroups != null)
            {
                //----------------------------------------- محاسبه خرید ناخالص
                List<int> buye_accounts = await GetBuyAccountsByGroupsAsync(sellerId, buyGroups);
                var buy = await AccountsBalanceAsync(sellerId, periodId, buye_accounts, startDate, endDate);
                sumBuy = buy.Sum(n => n.TotalBalance);

                //------------------------------------------- تخفیفات و برگشت
                List<int> disAccounts = await GetDiscountBuyAccountsByGroupsAsync(sellerId, buyGroups);
                var discount = await AccountsBalanceAsync(sellerId, periodId, disAccounts, startDate, endDate);
                sumDiscount = discount.Sum(n => n.TotalBalance);
            }
            return sumBuy - sumDiscount;
        }
        public async Task<List<ProfitMasterDto>> NetBuyAsync(long sellerId, int periodId, List<int>? buyGroups, string? startDate = null, string? endDate = null)
        {
            List<ProfitMasterDto> netBuySection = new List<ProfitMasterDto>();
            long sumBuy = 0;
            long sumDiscount = 0;
            //----------------------------------------- محاسبه خرید ناخالص
            if (buyGroups != null)
            {
                List<int> buye_accounts = await GetBuyAccountsByGroupsAsync(sellerId, buyGroups);
                var buy = await AccountsBalanceAsync(sellerId, periodId, buye_accounts, startDate, endDate);
                foreach (var x in buy.OrderByDescending(n => n.TotalBalance))
                {
                    ProfitMasterDto n = new ProfitMasterDto();
                    n.Title = x.AccountName;
                    n.Amount = x.TotalBalance;
                    n.SumAmounts = null;
                    n.TotalSection = null;
                    n.IsAdd = false;
                    n.cssClassName = "profit-amount";
                    netBuySection.Add(n);
                }
                sumBuy = buy.Sum(n => n.TotalBalance);
                ProfitMasterDto nakhales = new ProfitMasterDto();
                nakhales.Title = "جمع خــرید";
                nakhales.SumAmounts = sumBuy;
                nakhales.TotalSection = null;
                nakhales.Amount = null;
                nakhales.IsAdd = false;
                nakhales.cssClassName = "profit-sum";
                netBuySection.Add(nakhales);

                //------------------------------------------- تخفیفات و برگشت
                List<int> disAccounts = await GetDiscountBuyAccountsByGroupsAsync(sellerId, buyGroups);
                var discount = await AccountsBalanceAsync(sellerId, periodId, disAccounts, startDate, endDate);
                foreach (var x in discount.OrderByDescending(n => n.TotalBalance))
                {
                    ProfitMasterDto n = new ProfitMasterDto();
                    n.Title = x.AccountName;
                    n.Amount = x.TotalBalance;
                    n.SumAmounts = null;
                    n.TotalSection = null;
                    n.IsAdd = true;
                    n.cssClassName = "profit-amount";
                    netBuySection.Add(n);
                }
                sumDiscount = discount.Sum(n => n.TotalBalance);
                ProfitMasterDto totalDiscount = new ProfitMasterDto();
                totalDiscount.Title = "جمع تخفیفات خرید و برگشت از خرید";
                totalDiscount.SumAmounts = sumDiscount;
                totalDiscount.IsAdd = true;
                totalDiscount.cssClassName = "profit-sum minez";
                netBuySection.Add(totalDiscount);
            }

            //------------------------------------------------ خالص فروش و درآمد
            ProfitMasterDto total = new ProfitMasterDto();
            total.Title = "خالص خــرید";
            total.TotalSection = sumBuy - sumDiscount;
            total.IsAdd = false;
            total.cssClassName = "profit-total";
            netBuySection.Add(total);

            return netBuySection;

        }


        //====================================================
        //=== هزینه ها
        //====================================================
        //دریافت حساب های هزینه
        public async Task<List<int>> GetCostAccountsAsync(long sellerId, List<int> Kols, string? startDate = null, string? endDate = null)
        {
            List<int>? costAccounts = await _db.Acc_Coding_Moeins.Where(n =>
               n.SellerId == sellerId
               && Kols.Contains(n.KolId)
               && n.Nature == 1).Select(n => n.Id).ToListAsync<int>();

            return costAccounts;
        }
        public async Task<List<int>> GetCostAccountsByGroupsAsync(long sellerId, List<int> groups, string? startDate = null, string? endDate = null)
        {
            List<int>? costAccounts = await _db.Acc_Coding_Moeins.Where(n =>
               n.SellerId == sellerId
               && groups.Contains(n.MoeinKol.GroupId)
               && n.Nature == 1).Select(n => n.Id).ToListAsync<int>();

            return costAccounts;
        }
        // هزینه ها
        public async Task<List<ProfitMasterDto>> CostAsync(long sellerId, int periodId, List<int>? costGroup, string? startDate = null, string? endDate = null)
        {
            List<ProfitMasterDto> costSection = new List<ProfitMasterDto>();
            long sumCost = 0;
            long sumGeneral = 0;
            if (costGroup != null)
            {
                List<int> cost_accounts = await GetCostAccountsByGroupsAsync(sellerId, costGroup);
                var costs = await AccountsBalanceAsync(sellerId, periodId, cost_accounts, startDate, endDate);
                foreach (var x in costs.OrderByDescending(n => n.TotalBalance))
                {
                    ProfitMasterDto n = new ProfitMasterDto();
                    n.Title = x.AccountName;
                    n.Amount = x.TotalBalance;
                    n.SumAmounts = null;
                    n.TotalSection = null;
                    n.IsAdd = true;
                    n.cssClassName = "profit-amount";
                    costSection.Add(n);
                }
                sumCost = costs.Sum(n => n.TotalBalance);
                ProfitMasterDto totalCost = new ProfitMasterDto();
                totalCost.Title = "جمع هزینه ها";
                totalCost.SumAmounts = sumCost;
                totalCost.TotalSection = null;
                totalCost.Amount = null;
                totalCost.IsAdd = true;
                totalCost.cssClassName = "profit-sum";
                costSection.Add(totalCost);
            }

            //------------------------------------------------ plu
            ProfitMasterDto total = new ProfitMasterDto();
            total.Title = "جمع هزینه های عملیاتی و غیرعملیاتی";
            total.TotalSection = sumCost;
            total.IsAdd = false;
            total.cssClassName = "profit-total minez";
            costSection.Add(total);

            return costSection;
        }
        public async Task<long> Calc_CostAsync(long sellerId, int periodId, List<int>? costGroup, string? startDate = null, string? endDate = null)
        {
            List<int> cost_accounts = await GetCostAccountsByGroupsAsync(sellerId, costGroup);
            var costs = await AccountsBalanceAsync(sellerId, periodId, cost_accounts, startDate, endDate);
            long costAmount = costs.Sum(n => n.TotalBalance);
            return costAmount;

        }

        //====================================================
        //=== موجودی کالا و مواد
        //====================================================
        //موجودی اول دوره
        public async Task<long> GetMojoodiAvalDoreAsync(long sellerId, int periodId, int MojoodiKalaMoeinId, string? startDate = null, string? endDate = null)
        {
            var efteth = await _db.Acc_Articles.Where(n =>
            n.Doc.SellerId == sellerId
            && n.Doc.PeriodId == periodId
            && n.IsDeleted == false
            && (n.Doc.TypeId == 2 || n.Doc.DocNumber == 1)
            && n.MoeinId == MojoodiKalaMoeinId).FirstOrDefaultAsync();
            if (efteth == null)
                return 0;
            else
                return efteth.Bed;
        }
        public async Task<ProfitMasterDto> GetMojoodiAvalDoreArticleAsync(long sellerId, int periodId, int MojoodiKalaMoeinId, string? startDate = null, string? endDate = null)
        {
            long mojoodi = await GetMojoodiAvalDoreAsync(sellerId, periodId, MojoodiKalaMoeinId);

            ProfitMasterDto total = new ProfitMasterDto();
            total.Title = "موجودی در ابتدای دوره";
            total.TotalSection = mojoodi;
            total.IsAdd = true;
            total.cssClassName = "profit-total";

            return total;
        }

        //موجودی مواد و کالا
        public async Task<long> GetMojoodiKalaAsync(long sellerId, int periodId, int MojoodiKalaMoeinId, string? startDate = null, string? endDate = null)
        {
            var mojoodiKala = await AccountsBalanceAsync(sellerId, periodId, new() { MojoodiKalaMoeinId }, startDate, endDate);
            if (mojoodiKala.Count == 0)
                return 0;
            else
                return mojoodiKala.FirstOrDefault().TotalBed;
        }
        public async Task<ProfitMasterDto> GetMojoodiKalaArticleAsync(long sellerId, int periodId, int MojoodiKalaMoeinId, string? startDate = null, string? endDate = null)
        {
            long mojoodi = await GetMojoodiKalaAsync(sellerId, periodId, MojoodiKalaMoeinId);

            ProfitMasterDto total = new ProfitMasterDto();
            total.Title = "موجودی مواد و کالا";
            total.TotalSection = mojoodi;
            total.IsAdd = true;
            total.cssClassName = "profit-total";

            return total;
        }
        //موجودی پایان دوره
        // موجودی پایان دوره = اول دوره + خالص خرید - خالص فروش
        public async Task<long> CalcPayanDoreAsync(long sellerId, int periodId, int MojoodiKalaMoeinId, List<int>? SaleKols, List<int>? SaleDiscountKols, List<int>? incomeKols, List<int>? buyGroups, long? manualAmount = null, string? startDate = null, string? endDate = null)
        {
            if (manualAmount != null) return manualAmount.Value;

            long payanDore = 0;
            long avalDore = await GetMojoodiAvalDoreAsync(sellerId, periodId, MojoodiKalaMoeinId);
            long netSale = await CalcNetSaleAsync(sellerId, periodId, SaleKols, SaleDiscountKols);
            long income = await CalcIncomeAsync(sellerId, periodId, incomeKols);
            long netIncome = netSale + income;
            long netBuy = await GetNetBuyAsync(sellerId, periodId, buyGroups);
            payanDore = (avalDore + netBuy) - netIncome;
            return payanDore;
        }
        public async Task<ProfitMasterDto> ProfitReport_PayanDoreAsync(long sellerId, int periodId, int MojoodiKalaMoeinId, List<int>? SaleKols, List<int>? SaleDiscountKols, List<int>? incomeKols, List<int>? buyGroups, long? manualAmount = null, string? startDate = null, string? endDate = null)
        {
            long payanDore = 0;
            if (manualAmount != null)
                payanDore = manualAmount.Value;
            else
            {
                payanDore = await CalcPayanDoreAsync(sellerId, periodId, MojoodiKalaMoeinId, SaleKols, SaleDiscountKols, incomeKols, buyGroups, null);
            }

            ProfitMasterDto total = new ProfitMasterDto();
            total.Title = "موجودی پایان دوره";
            total.TotalSection = payanDore;
            total.IsAdd = false;
            total.cssClassName = "profit-total minez";

            return total;

        }
        //آماده برای فروش
        // ------------------  خرید طی دوره + موجودی اول دوره = آماده برای فروش
        public async Task<long> GetAmadehForooshAsync(long sellerId, int periodId, int MojoodiKalaMoeinId, List<int> buyGroups, string? startDate = null, string? endDate = null)
        {
            long avalDore = await GetMojoodiAvalDoreAsync(sellerId, periodId, MojoodiKalaMoeinId);
            long netBuy = await GetNetBuyAsync(sellerId, periodId, buyGroups);
            return avalDore + netBuy;
        }
        public async Task<ProfitMasterDto> AmadehForooshArticleAsync(long sellerId, int periodId, int MojoodiKalaMoeinId, List<int> buyGroups, string? startDate = null, string? endDate = null)
        {
            long mojoodi = await GetAmadehForooshAsync(sellerId, periodId, MojoodiKalaMoeinId, buyGroups);
            ProfitMasterDto total = new ProfitMasterDto();
            total.Title = "آماده برای فروش";
            total.TotalSection = mojoodi;
            total.IsAdd = true;
            total.cssClassName = "profit-total";
            return total;
        }

        // بهای تمام شده کالای فروش رفته
        //----------------------------------- موجودی اول دوره + خرید های دوره - موجودی پایان دوره = بهای تمام شده کالای فروش رفته
        public async Task<long> GetBahayeTamamShodeForooshAsync(long sellerId, int periodId, int MojoodiKalaMoeinId, long payanDore, List<int> buyGroups, string? startDate = null, string? endDate = null)
        {
            long avalDore = await GetMojoodiAvalDoreAsync(sellerId, periodId, MojoodiKalaMoeinId);
            long netBuy = await GetNetBuyAsync(sellerId, periodId, buyGroups);
            //long mojoodi = await GetMojoodiKalaAsync(sellerId, periodId, MojoodiKalaMoeinId);
            return (avalDore + netBuy) - payanDore;
        }
        public async Task<ProfitMasterDto> BahayeTamamShodeForooshArticleAsync(long sellerId, int periodId, int MojoodiKalaMoeinId, long payanDore, List<int> buyGroups, string? startDate = null, string? endDate = null)
        {
            long mojoodi = await GetBahayeTamamShodeForooshAsync(sellerId, periodId, MojoodiKalaMoeinId, payanDore, buyGroups);
            ProfitMasterDto total = new ProfitMasterDto();
            total.Title = "بهای تمام شده کالای فروش رفته";
            total.TotalSection = mojoodi;
            total.IsAdd = true;
            total.cssClassName = "profit-total";
            return total;
        }

        //============
        //سود ناویژه
        public async Task<long> profit_NavizheAsync(ProfitReportSetting profitSetting)
        {
            long PayandoreAmount = await CalcPayanDoreAsync(profitSetting.SellerId, profitSetting.PeriodId, profitSetting.MojoodiKalaMoeinId, profitSetting.SaleKolAccounts, profitSetting.SaleDiscountKolAccounts, profitSetting.IncomeKolAccounts, profitSetting.BuyGroups, profitSetting.PayanDore, profitSetting.startDate, profitSetting.endDate);
            long bahayeTamamShode = await GetBahayeTamamShodeForooshAsync(profitSetting.SellerId, profitSetting.PeriodId, profitSetting.MojoodiKalaMoeinId, PayandoreAmount, profitSetting.BuyGroups, profitSetting.startDate, profitSetting.endDate);
            long sale = await CalcNetSaleAsync(profitSetting.SellerId, profitSetting.PeriodId, profitSetting.SaleKolAccounts, profitSetting.SaleDiscountKolAccounts, profitSetting.startDate, profitSetting.endDate);
            long income = await CalcIncomeAsync(profitSetting.SellerId, profitSetting.PeriodId, profitSetting.IncomeKolAccounts, profitSetting.startDate, profitSetting.endDate);
            long totalIncome = sale + income;

            long profit = totalIncome - bahayeTamamShode;
            return profit;
        }
        public async Task<ProfitMasterDto> profit_Navizhe_ArticleAsync(ProfitReportSetting profitSetting)
        {
            long profit = await profit_NavizheAsync(profitSetting);
            string title = "سـود ناویـژه";
            string cssClass = "profit-total";
            if (profit < 0)
            {
                title = "زیان ناویژه";
                cssClass = "profit-total minez";
            }

            ProfitMasterDto total = new ProfitMasterDto();
            total.Title = title;
            total.TotalSection = profit;
            total.IsAdd = true;
            total.cssClassName = cssClass;
            return total;
        }

        //======== سود و زیان ویژه
        public async Task<long> profit_VizhehAsync(ProfitReportSetting profitSetting)
        {
            long navicheh = await profit_NavizheAsync(profitSetting);
            long costs = await Calc_CostAsync(profitSetting.SellerId, profitSetting.PeriodId, profitSetting.CostGroup, profitSetting.startDate, profitSetting.endDate);
            long profit = navicheh - costs;
            return profit;
        }
        public async Task<ProfitMasterDto> profit_Vizheh_ArticleAsync(ProfitReportSetting profitSetting)
        {
            long profit = await profit_VizhehAsync(profitSetting);
            string title = "سـود ویـژه";
            string cssClass = "profit-total";
            if (profit < 0)
            {
                title = "زیان ویژه";
                cssClass = "profit-total minez";
            }

            ProfitMasterDto total = new ProfitMasterDto();
            total.Title = title;
            total.TotalSection = profit;
            total.IsAdd = true;
            total.cssClassName = cssClass;
            return total;
        }

        //=====================================================
        //--------Profit Report 
        //=====================================================
        public async Task<List<ProfitMasterDto>> ProfitReportAsync(ProfitReportSetting profitSetting)
        {
            List<ProfitMasterDto> report = new List<ProfitMasterDto>();
            long PayandoreAmount = await CalcPayanDoreAsync(profitSetting.SellerId, profitSetting.PeriodId, profitSetting.MojoodiKalaMoeinId, profitSetting.SaleKolAccounts, profitSetting.SaleDiscountKolAccounts, profitSetting.IncomeKolAccounts, profitSetting.BuyGroups, profitSetting.PayanDore, profitSetting.startDate, profitSetting.endDate);
            //--1-- فروش
            var sale = await ProfitReport_SaleAsync(profitSetting.SellerId, profitSetting.PeriodId, profitSetting.SaleKolAccounts, profitSetting.SaleDiscountKolAccounts, profitSetting.startDate, profitSetting.endDate);
            //--2-- درآمدها
            var income = await ProfitReport_IncomeAsync(profitSetting.SellerId, profitSetting.PeriodId, profitSetting.IncomeKolAccounts, profitSetting.startDate, profitSetting.endDate);
            //--3-- جمع فروش و درآمدها
            var totalSaleAndIncome = await ProfitReport_NetSaleAndIncomeAsync(profitSetting.SellerId, profitSetting.PeriodId, profitSetting.IncomeKolAccounts, profitSetting.SaleKolAccounts, profitSetting.SaleDiscountKolAccounts, profitSetting.startDate, profitSetting.endDate);
            //--4-- موجودی کالا و مواد
            var MojoodiKala = await GetMojoodiKalaArticleAsync(profitSetting.SellerId, profitSetting.PeriodId, profitSetting.MojoodiKalaMoeinId, profitSetting.startDate, profitSetting.endDate);
            //--5-- خالص خرید
            var buy = await NetBuyAsync(profitSetting.SellerId, profitSetting.PeriodId, profitSetting.BuyGroups, profitSetting.startDate, profitSetting.endDate);
            //--6-- آماده برای فروش
            var AmadehForoosh = await AmadehForooshArticleAsync(profitSetting.SellerId, profitSetting.PeriodId, profitSetting.MojoodiKalaMoeinId, profitSetting.BuyGroups, profitSetting.startDate, profitSetting.endDate);
            //--7-- موجودی پایان دوره
            var payanDore = await ProfitReport_PayanDoreAsync(profitSetting.SellerId, profitSetting.PeriodId, profitSetting.MojoodiKalaMoeinId, profitSetting.SaleKolAccounts, profitSetting.SaleDiscountKolAccounts, profitSetting.IncomeKolAccounts, profitSetting.BuyGroups, profitSetting.PayanDore, profitSetting.startDate, profitSetting.endDate);
            //--8-- بهای تمام شده کالای فروش رفته
            var bahayeTamamShodeFroosh = await BahayeTamamShodeForooshArticleAsync(profitSetting.SellerId, profitSetting.PeriodId, profitSetting.MojoodiKalaMoeinId, PayandoreAmount, profitSetting.BuyGroups, profitSetting.startDate, profitSetting.endDate);
            //--9-- سود(زیان) ناویژه
            var Navizheh = await profit_Navizhe_ArticleAsync(profitSetting);
            //--10-- هزینه ها
            var cost = await CostAsync(profitSetting.SellerId, profitSetting.PeriodId, profitSetting.CostGroup, profitSetting.startDate, profitSetting.endDate);
            //--11-- سود و زیان ویژه
            var vizhe = await profit_Vizheh_ArticleAsync(profitSetting);

            // Add to report
            report.AddRange(sale);
            report.AddRange(income);
            report.Add(totalSaleAndIncome);
            report.Add(MojoodiKala);
            report.AddRange(buy);
            report.Add(AmadehForoosh);
            report.Add(payanDore);
            report.Add(bahayeTamamShodeFroosh);
            report.Add(Navizheh);
            report.AddRange(cost);
            report.Add(vizhe);

            return report;
        }
    }
}
