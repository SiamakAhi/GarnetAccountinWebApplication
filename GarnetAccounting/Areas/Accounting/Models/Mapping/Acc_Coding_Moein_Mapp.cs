using GarnetAccounting.Areas.Accounting.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarnetAccounting.Areas.Accounting.Models.Mapping
{
    public class Acc_Coding_Moein_Mapp : IEntityTypeConfiguration<Acc_Coding_Moein>
    {
        public void Configure(EntityTypeBuilder<Acc_Coding_Moein> builder)
        {
            builder.HasOne(n => n.MoeinKol)
                .WithMany(n => n.Moeins)
                .HasForeignKey(f => f.KolId);

            //
            //builder.HasData(


            //    new Acc_Coding_Moein { Id = 1, KolId = 1, MoeinCode = "1010001", MoeinName = "زمین", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 2, KolId = 1, MoeinCode = "1010002", MoeinName = "ساختمان", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 3, KolId = 1, MoeinCode = "1010003", MoeinName = "ماشین آلات و تجهیزات", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 4, KolId = 1, MoeinCode = "1010004", MoeinName = "تاسیسات", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 5, KolId = 1, MoeinCode = "1010005", MoeinName = "وسایل نقلیه", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 6, KolId = 1, MoeinCode = "1010006", MoeinName = "اثاثه و منصوبات", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 7, KolId = 1, MoeinCode = "1010007", MoeinName = "ابزارآلات", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 8, KolId = 1, MoeinCode = "1010008", MoeinName = "قالب ها", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 9, KolId = 1, MoeinCode = "1010009", MoeinName = "پیش پرداخت اقلام سرمایه ایی", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 10, KolId = 1, MoeinCode = "1010051", MoeinName = "دارایی های ثابت در جریان تکمیل", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 11, KolId = 1, MoeinCode = "1010052", MoeinName = "ساختمان های در دست تکمیل", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 12, KolId = 1, MoeinCode = "1010053", MoeinName = "ماشین آلات و تجهیزات در دست تکمیل", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 13, KolId = 1, MoeinCode = "1010054", MoeinName = "تاسیسات در دست تکمیل", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 14, KolId = 1, MoeinCode = "1010055", MoeinName = "وسایل نقلیه در دست تکمیل", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 15, KolId = 1, MoeinCode = "1010056", MoeinName = "اثاثه و منصوبات در دست تکمیل", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 16, KolId = 1, MoeinCode = "1010057", MoeinName = "ابزارآلات در دست تکمیل", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 17, KolId = 1, MoeinCode = "1010058", MoeinName = "قالب های در دست تکمیل", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 18, KolId = 1, MoeinCode = "1010101", MoeinName = "ساختمانهای بلااستفاده", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 19, KolId = 1, MoeinCode = "1010102", MoeinName = "ماشین آلات و تجهیزات بلااستفاده", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 20, KolId = 1, MoeinCode = "1010103", MoeinName = "تاسیسات بلااستفاده", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 21, KolId = 1, MoeinCode = "1010104", MoeinName = "وسایط نقلیه بلااستفاده", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 22, KolId = 1, MoeinCode = "1010105", MoeinName = "اثاثه و منصوبات بلااستفاده", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 23, KolId = 1, MoeinCode = "1010106", MoeinName = "ابزارآلات بلااستفاده", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 24, KolId = 1, MoeinCode = "1010107", MoeinName = "قالب های بلااستفاده", Nature = 1, IsCurrencyAccount = false, IsEditable = true },


            //    new Acc_Coding_Moein { Id = 25, KolId = 2, MoeinCode = "1020001", MoeinName = "استهلاک انباشته ساختمان", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 26, KolId = 2, MoeinCode = "1020002", MoeinName = "استهلاک انباشته ماشین آلات و تجهیزات", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 27, KolId = 2, MoeinCode = "1020003", MoeinName = "استهلاک انباشته تاسیسات", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 28, KolId = 2, MoeinCode = "1020004", MoeinName = "استهلاک انباشته وسایط نقلیه", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 29, KolId = 2, MoeinCode = "1020005", MoeinName = "استهلاک انباشته اثاثیه و منصوبات", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 30, KolId = 2, MoeinCode = "1020006", MoeinName = "استهلاک انباشته ابزار آلات", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 31, KolId = 2, MoeinCode = "1020007", MoeinName = "استهلاک انباشته قالب ها", Nature = 2, IsCurrencyAccount = false, IsEditable = true },


            //    new Acc_Coding_Moein { Id = 32, KolId = 3, MoeinCode = "1030001", MoeinName = "سرمایه گذاری در املاک", Nature = 1, IsCurrencyAccount = false, IsEditable = true },


            //    new Acc_Coding_Moein { Id = 33, KolId = 4, MoeinCode = "1040001", MoeinName = "حق الامتیاز برق", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 34, KolId = 4, MoeinCode = "1040002", MoeinName = "حق الامتیاز تلفن", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 35, KolId = 4, MoeinCode = "1040003", MoeinName = "حق الامتیاز گاز", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 36, KolId = 4, MoeinCode = "1040004", MoeinName = "حق الامتیاز آب", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 37, KolId = 4, MoeinCode = "1040005", MoeinName = "سرقفلی", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 38, KolId = 4, MoeinCode = "1040006", MoeinName = "دانش فنی", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 39, KolId = 4, MoeinCode = "1040007", MoeinName = "نرم افزارهای رایانه ای", Nature = 1, IsCurrencyAccount = false, IsEditable = true },


            //    new Acc_Coding_Moein { Id = 40, KolId = 5, MoeinCode = "1050001", MoeinName = "استهلاک انباشه نرم افزار های رایانه ای", Nature = 2, IsCurrencyAccount = false, IsEditable = true },


            //    new Acc_Coding_Moein { Id = 41, KolId = 6, MoeinCode = "1060001", MoeinName = "سرمایه گذاری در شرکت های تابعه", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 42, KolId = 6, MoeinCode = "1060002", MoeinName = "سرمایه گذاری در شرکت های وابسته", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 43, KolId = 6, MoeinCode = "1060003", MoeinName = "سپرده سرمایه گذاری در سهام سایر شرکتها", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 44, KolId = 6, MoeinCode = "1060004", MoeinName = "سپرده سرمایه گذاری در اوراق مشارکت", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 45, KolId = 6, MoeinCode = "1060005", MoeinName = "سپرده سرمایه گذاری در سایر اوراق بهادار", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 46, KolId = 6, MoeinCode = "1060006", MoeinName = "سپرده سرمایه گذاری بلند مدت بانكی", Nature = 1, IsCurrencyAccount = false, IsEditable = true },


            //    new Acc_Coding_Moein { Id = 47, KolId = 7, MoeinCode = "1070001", MoeinName = "چک های دریافتنی تجاری بلند مدت", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 48, KolId = 7, MoeinCode = "1070002", MoeinName = "اسناد دریافتنی تجاری بلند مدت ", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 49, KolId = 7, MoeinCode = "1070003", MoeinName = "حسابهای دریافتنی تجاری بلند مدت", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 50, KolId = 7, MoeinCode = "1070004", MoeinName = "سایر اسناد دریافتنی بلند مدت", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 51, KolId = 7, MoeinCode = "1070005", MoeinName = "سایر حسابهای دریافتنی بلند مدت", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 52, KolId = 7, MoeinCode = "1070006", MoeinName = "ذخیره کاهش ارزش دریافتنی های بلند مدت", Nature = 1, IsCurrencyAccount = false, IsEditable = true },


            //    new Acc_Coding_Moein { Id = 53, KolId = 8, MoeinCode = "1080001", MoeinName = "وجوه بانكی مسدود شده", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 54, KolId = 8, MoeinCode = "1080002", MoeinName = "سپرده نزد صندوق دادگستری", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 55, KolId = 8, MoeinCode = "1080003", MoeinName = "سایر داراییهای غیر جاری بلند مدت", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 56, KolId = 8, MoeinCode = "1080004", MoeinName = "هزینه های انتقالی به دوره های آتی", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 57, KolId = 8, MoeinCode = "1080005", MoeinName = "هزینه های تاسیس و قبل از بهره برداری", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 58, KolId = 8, MoeinCode = "1080006", MoeinName = "هزینه افزایش سرمایه", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 59, KolId = 8, MoeinCode = "1080007", MoeinName = "هزینه سود و کارمزد وامهای بلند مدت", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 60, KolId = 8, MoeinCode = "1080008", MoeinName = "هزینه تبلیغات فروش آتی", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 61, KolId = 8, MoeinCode = "1080009", MoeinName = "هزینه پروژه های تحقیقاتی", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 62, KolId = 8, MoeinCode = "1080010", MoeinName = "هزینه های طرح و توسعه", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 63, KolId = 8, MoeinCode = "1080011", MoeinName = "سایر هزینه های سنوات آتی", Nature = 1, IsCurrencyAccount = false, IsEditable = true },

            //    //-----------------------------------------------------------------------------------------------------------------
            //    new Acc_Coding_Moein { Id = 64, KolId = 9, MoeinCode = "2010001", MoeinName = "پیش پرداخت خرید کالا و مواد اولیه", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 65, KolId = 9, MoeinCode = "2010002", MoeinName = "پیش پرداخت خرید خارجی", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 66, KolId = 9, MoeinCode = "2010003", MoeinName = "پیش پرداخت هزینه های گمرکی", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 67, KolId = 9, MoeinCode = "2010004", MoeinName = "پیش پرداخت هزینه های جاری", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 68, KolId = 9, MoeinCode = "2010005", MoeinName = "پیش پرداخت خرید خدمات", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 69, KolId = 9, MoeinCode = "2010006", MoeinName = "پیش پرداخت به حق العمل کاران گمرکی", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 70, KolId = 9, MoeinCode = "2010007", MoeinName = "پیش پرداخت بیمه", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 71, KolId = 9, MoeinCode = "2010008", MoeinName = "پیش پرداخت حسابرسی", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 72, KolId = 9, MoeinCode = "2010009", MoeinName = "پیش پرداخت مالیات", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 73, KolId = 9, MoeinCode = "2010010", MoeinName = "پیش پرداخت هزیته های طرح و توسعه", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 74, KolId = 9, MoeinCode = "2010011", MoeinName = "پیش پرداخت اجاره", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 75, KolId = 9, MoeinCode = "2010012", MoeinName = "پیش پرداخت هزینه سود و کارمزد وامها", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 76, KolId = 9, MoeinCode = "2010013", MoeinName = "پیش پرداخت پیمانکاران", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 77, KolId = 9, MoeinCode = "2010014", MoeinName = "پیش پرداخت بهره وری تولید", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 78, KolId = 9, MoeinCode = "2010015", MoeinName = "پیش پرداخت پروژه های مشاوره ای", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 79, KolId = 9, MoeinCode = "2010016", MoeinName = "(پیش پرداخت عوارض شهرداری-آموزش و پرورش(قدیم", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 80, KolId = 9, MoeinCode = "2010017", MoeinName = "(پیش پرداخت عوارض آموزش و پرورش(قدیم", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 81, KolId = 9, MoeinCode = "2010018", MoeinName = "پیش پرداخت صدور فاکتور", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 82, KolId = 9, MoeinCode = "2010019", MoeinName = "سایر پیش پرداختها", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 83, KolId = 9, MoeinCode = "2010051", MoeinName = "پیش پرداخت مالیات بر ارزش افزوده", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 84, KolId = 9, MoeinCode = "2010052", MoeinName = "پیش پرداخت عوارض ارزش افروده", Nature = 1, IsCurrencyAccount = false, IsEditable = true },


            //    new Acc_Coding_Moein { Id = 85, KolId = 10, MoeinCode = "2020001", MoeinName = "سفارشات اقلام در راه", Nature = 1, IsCurrencyAccount = false, IsEditable = true },


            //    new Acc_Coding_Moein { Id = 86, KolId = 11, MoeinCode = "2030001", MoeinName = "ودیعه اجاره", Nature = 1, IsCurrencyAccount = false, IsEditable = true },


            //    new Acc_Coding_Moein { Id = 87, KolId = 12, MoeinCode = "2040001", MoeinName = "موجودی انبار", Nature = 1, IsCurrencyAccount = false, IsEditable = true },


            //    //new Acc_Coding_Moein { Id = 88, KolId = 13, MoeinCode = "2050001", MoeinName = "موجودی آخر دوره انبار", Nature = 1, IsCurrencyAccount = false, IsEditable = true },


            //    new Acc_Coding_Moein { Id = 89, KolId = 14, MoeinCode = "2060001", MoeinName = "دریافتنی های تجاری", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 90, KolId = 14, MoeinCode = "2060002", MoeinName = "ذخیره مطالبات مشکوک الوصول", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 91, KolId = 14, MoeinCode = "2060003", MoeinName = "حساب های واخواست شده", Nature = 1, IsCurrencyAccount = false, IsEditable = true },


            //    new Acc_Coding_Moein { Id = 92, KolId = 15, MoeinCode = "2070001", MoeinName = "چکهای دریافتنی نزد صندوق", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 93, KolId = 15, MoeinCode = "2070002", MoeinName = "چکهای در جریان وصول نزد بانک", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 94, KolId = 15, MoeinCode = "2070051", MoeinName = "اسناد دریافتنی نزد صندوق", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 95, KolId = 15, MoeinCode = "2070052", MoeinName = "اسناد در جریان وصول نزد بانک", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 96, KolId = 15, MoeinCode = "2070053", MoeinName = "اسناد خرید دین", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 97, KolId = 15, MoeinCode = "2070054", MoeinName = "چک های واخواست شده", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 98, KolId = 15, MoeinCode = "2070055", MoeinName = "اسناد دریافتنی واخواست شده", Nature = 1, IsCurrencyAccount = false, IsEditable = true },


            //    new Acc_Coding_Moein { Id = 99, KolId = 16, MoeinCode = "2080001", MoeinName = "جاری شرکا/سهامداران", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 100, KolId = 16, MoeinCode = "2080002", MoeinName = "حساب های دریافتنی غیر تجاری", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 101, KolId = 16, MoeinCode = "2080003", MoeinName = "وام کارکنان", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 102, KolId = 16, MoeinCode = "2080004", MoeinName = "مساعده کارکنان", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 103, KolId = 16, MoeinCode = "2080005", MoeinName = "جاری کارکنان", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 104, KolId = 16, MoeinCode = "2080006", MoeinName = "سود سهام دریافتنی", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 105, KolId = 16, MoeinCode = "2080007", MoeinName = "سایر حساب های دریافتنی غیر تجاری", Nature = 1, IsCurrencyAccount = false, IsEditable = true },


            //    new Acc_Coding_Moein { Id = 302, KolId = 17, MoeinCode = "2090001", MoeinName = "ذخیره کاهش ارزش دریافتنی های غیر تجاری", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 106, KolId = 17, MoeinCode = "2090002", MoeinName = "سهام شرکتهای پذیرفته شده در بورش و فرابورس", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 107, KolId = 17, MoeinCode = "2090003", MoeinName = "سایر اوراق بهادر", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 108, KolId = 17, MoeinCode = "2090004", MoeinName = "سرمایه گذاری درسهام سایر شرکتها", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 109, KolId = 17, MoeinCode = "2090005", MoeinName = "سرمایه گذاری در سایر اوراق بهادر", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 110, KolId = 17, MoeinCode = "2090006", MoeinName = "سپرده های سرمایه گذاری کوتاه مدت نزد بانکها", Nature = 1, IsCurrencyAccount = false, IsEditable = true },


            //    new Acc_Coding_Moein { Id = 111, KolId = 18, MoeinCode = "2100001", MoeinName = "موجودی نقد بانكها - ریالی", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 112, KolId = 18, MoeinCode = "2100002", MoeinName = "موجودی نقد بانكها - ارزی", Nature = 1, IsCurrencyAccount = true, IsEditable = true },


            //    new Acc_Coding_Moein { Id = 113, KolId = 19, MoeinCode = "2110001", MoeinName = "موجودی نقذ صندوق ریالی", Nature = 1, IsCurrencyAccount = true, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 114, KolId = 19, MoeinCode = "2110002", MoeinName = "موجودی نقد صندوق ارزی", Nature = 1, IsCurrencyAccount = true, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 115, KolId = 19, MoeinCode = "2110003", MoeinName = "تنخواه گردانها", Nature = 1, IsCurrencyAccount = true, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 116, KolId = 19, MoeinCode = "2110004", MoeinName = "وجوه در راه", Nature = 1, IsCurrencyAccount = true, IsEditable = true },



            //    new Acc_Coding_Moein { Id = 117, KolId = 20, MoeinCode = "2120001", MoeinName = "زمین و ساختمان انبار", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 118, KolId = 20, MoeinCode = "2120002", MoeinName = "دارایی های مرتبط با کارخانه تولید محصولات", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //     new Acc_Coding_Moein { Id = 119, KolId = 20, MoeinCode = "2120003", MoeinName = "بدهی های مرتبط با دارایی های غیر جاری نگهداری شده برای فروش", Nature = 1, IsCurrencyAccount = false, IsEditable = true },

            //    //----- 3 - حقوق مالکانه --------------------------------------------------------------------------------------
            //    new Acc_Coding_Moein { Id = 120, KolId = 21, MoeinCode = "3010001", MoeinName = "سرمایه ثبت شده", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 121, KolId = 21, MoeinCode = "3010002", MoeinName = "سرمایه تعهد شده", Nature = 2, IsCurrencyAccount = false, IsEditable = true },

            //    new Acc_Coding_Moein { Id = 122, KolId = 22, MoeinCode = "3020001", MoeinName = "افزایش سرمایه از محل مطالبات سهامداران", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 123, KolId = 22, MoeinCode = "3020002", MoeinName = "افزایش سرمایه از محل آورده نقدی سهامداران", Nature = 2, IsCurrencyAccount = false, IsEditable = true },


            //    new Acc_Coding_Moein { Id = 124, KolId = 23, MoeinCode = "3030001", MoeinName = "صرف سهام افزایش سرمایه", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 125, KolId = 23, MoeinCode = "3030002", MoeinName = "صرف سهام خزانه", Nature = 2, IsCurrencyAccount = false, IsEditable = true },


            //    new Acc_Coding_Moein { Id = 126, KolId = 24, MoeinCode = "3040001", MoeinName = "اندوخته قانونی", Nature = 2, IsCurrencyAccount = false, IsEditable = true },


            //    new Acc_Coding_Moein { Id = 127, KolId = 25, MoeinCode = "3050001", MoeinName = "اندوخته ی سرمایه ای", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 128, KolId = 25, MoeinCode = "3050002", MoeinName = "اندوخته ی توسعه و تکمیل", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 129, KolId = 25, MoeinCode = "3050003", MoeinName = "اندوخته تجدید ارزیابی دارایی ها", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 130, KolId = 25, MoeinCode = "3050004", MoeinName = "اندوخته جایگزین دارایی ها", Nature = 2, IsCurrencyAccount = false, IsEditable = true },


            //    new Acc_Coding_Moein { Id = 131, KolId = 26, MoeinCode = "3060001", MoeinName = "مازاد تجدید ارزیابی دارایی ها-ساختمان", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 132, KolId = 26, MoeinCode = "3060002", MoeinName = "مازاد تجدید ارزیابی دارایی ها-ماشین آلات و تجهیزات", Nature = 2, IsCurrencyAccount = false, IsEditable = true },


            //    new Acc_Coding_Moein { Id = 133, KolId = 27, MoeinCode = "3070001", MoeinName = "تفاوت تسعیر ارز عملیات خارجی ", Nature = 3, IsCurrencyAccount = false, IsEditable = true },


            //    new Acc_Coding_Moein { Id = 134, KolId = 28, MoeinCode = "3080001", MoeinName = "سهام خزانه ", Nature = 2, IsCurrencyAccount = false, IsEditable = true },


            //    new Acc_Coding_Moein { Id = 135, KolId = 29, MoeinCode = "3090001", MoeinName = "سود(زیان)انباشته", Nature = 3, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 136, KolId = 29, MoeinCode = "3090002", MoeinName = "تعدیلات سنواتی طی دوره", Nature = 3, IsCurrencyAccount = false, IsEditable = true },


            //    new Acc_Coding_Moein { Id = 137, KolId = 30, MoeinCode = "3100001", MoeinName = "سود (زیان) جاری", Nature = 3, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 138, KolId = 30, MoeinCode = "3100002", MoeinName = "عملکرد", Nature = 3, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 139, KolId = 30, MoeinCode = "3100003", MoeinName = "تقسیم سود", Nature = 3, IsCurrencyAccount = false, IsEditable = true },


            //    new Acc_Coding_Moein { Id = 140, KolId = 31, MoeinCode = "3110001", MoeinName = "سرمایه ابتدای دوره", Nature = 3, IsCurrencyAccount = false, IsEditable = true },


            //    new Acc_Coding_Moein { Id = 141, KolId = 32, MoeinCode = "3120001", MoeinName = "کنترل سربار-دستمزد", Nature = 3, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 142, KolId = 32, MoeinCode = "3120002", MoeinName = "کنترل سربار-ثابت", Nature = 3, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 143, KolId = 32, MoeinCode = "3120003", MoeinName = "کنترل سربار-متغیر", Nature = 3, IsCurrencyAccount = false, IsEditable = true },


            //    //------- 4 - بدهیهای غیرجاری -------------------------------------------------------------------
            //    new Acc_Coding_Moein { Id = 144, KolId = 33, MoeinCode = "4010001", MoeinName = "اسناد پرداختنی تجاری بلند مدت", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 145, KolId = 33, MoeinCode = "4010002", MoeinName = "حسابهای پرداختنی تجاری بلند مدت", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 146, KolId = 33, MoeinCode = "4010003", MoeinName = "سایر اسناد پرداختنی بلند مدت", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 147, KolId = 33, MoeinCode = "4010004", MoeinName = "سایر حسابهای پرداختنی بلند مدت", Nature = 2, IsCurrencyAccount = false, IsEditable = true },


            //    new Acc_Coding_Moein { Id = 148, KolId = 34, MoeinCode = "4020001", MoeinName = "تسهیلات بلند مدت دریافتی از بانكها", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 149, KolId = 34, MoeinCode = "4020002", MoeinName = "دریافتی های بلند مدت از اوراق مشارکت", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 150, KolId = 34, MoeinCode = "4020003", MoeinName = "دریافتی های بلند مدت از اوراق خرید دین", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 151, KolId = 34, MoeinCode = "4020004", MoeinName = "تعهدات اجاره سرمایه ای", Nature = 2, IsCurrencyAccount = false, IsEditable = true },


            //    new Acc_Coding_Moein { Id = 152, KolId = 35, MoeinCode = "4030001", MoeinName = "ذخیره مزایای پایان خدمت کارکنان", Nature = 2, IsCurrencyAccount = false, IsEditable = true },


            //    new Acc_Coding_Moein { Id = 153, KolId = 36, MoeinCode = "4040001", MoeinName = "ذخیره مالیات", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 154, KolId = 36, MoeinCode = "4040002", MoeinName = "دخیره مرخصی استفاده نشده", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 155, KolId = 36, MoeinCode = "4040003", MoeinName = "ذخیره سود و کارمزد وام ها", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 156, KolId = 36, MoeinCode = "4040004", MoeinName = "ذخیره هزینه های معوق", Nature = 2, IsCurrencyAccount = false, IsEditable = true },


            //    //------- 5 - بدهیهای جاری -------------------------------------------------------------------------------------------
            //    new Acc_Coding_Moein { Id = 157, KolId = 37, MoeinCode = "5010001", MoeinName = "پرداختنی تجاری", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 158, KolId = 37, MoeinCode = "5010051", MoeinName = "کالای امانی دیگران نزد ما ", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 159, KolId = 37, MoeinCode = "5010052", MoeinName = "معلق خرید", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 160, KolId = 37, MoeinCode = "5010053", MoeinName = "سایر حساب های پرداختنی تجاری", Nature = 2, IsCurrencyAccount = false, IsEditable = true },


            //    new Acc_Coding_Moein { Id = 161, KolId = 38, MoeinCode = "5020001", MoeinName = "چک های پرداختنی", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 162, KolId = 38, MoeinCode = "5020002", MoeinName = "سفته های پرداختنی", Nature = 2, IsCurrencyAccount = false, IsEditable = true },


            //    new Acc_Coding_Moein { Id = 163, KolId = 39, MoeinCode = "5030001", MoeinName = "حسابهای پرداختنی غیر تجاری", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 164, KolId = 39, MoeinCode = "5030002", MoeinName = "حقوق ودستمزد پرداختنی", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 165, KolId = 39, MoeinCode = "5030003", MoeinName = "حق بیمه پرداختنی", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 166, KolId = 39, MoeinCode = "5030004", MoeinName = "اداره امورمالیاتی(مالیات حقوق )", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 167, KolId = 39, MoeinCode = "5030005", MoeinName = "اداره امورمالیاتی(مالیات تکلیفی )", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 168, KolId = 39, MoeinCode = "5030006", MoeinName = "اداره امورمالیاتی(مالیات بر ارزش افروده )", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 169, KolId = 39, MoeinCode = "5030007", MoeinName = "جاری شرکاء/سهامداران", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 170, KolId = 39, MoeinCode = "5030008", MoeinName = "ذخیره هزینه های تحقق یافته پرداخت نشده", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 171, KolId = 39, MoeinCode = "5030009", MoeinName = "سپرده های دریافتی از دیگران", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 172, KolId = 39, MoeinCode = "5030010", MoeinName = "رند حقوق", Nature = 2, IsCurrencyAccount = false, IsEditable = true },

            //    new Acc_Coding_Moein { Id = 173, KolId = 40, MoeinCode = "5040001", MoeinName = "مالیات پرداختنی", Nature = 2, IsCurrencyAccount = false, IsEditable = true },


            //    new Acc_Coding_Moein { Id = 174, KolId = 41, MoeinCode = "5050001", MoeinName = "سود سهام مصوب مجمع عمومی", Nature = 2, IsCurrencyAccount = false, IsEditable = true },


            //    new Acc_Coding_Moein { Id = 175, KolId = 42, MoeinCode = "5060001", MoeinName = "تسهیلات دریافتی از بانكها", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 176, KolId = 42, MoeinCode = "5060002", MoeinName = "تسهیلات دریافتی از اشخاص", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 177, KolId = 42, MoeinCode = "5060003", MoeinName = "دریافتی ها از اوراق مشارکت", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 178, KolId = 42, MoeinCode = "5060004", MoeinName = " دریافتی ها از اوراق خرید دین", Nature = 2, IsCurrencyAccount = false, IsEditable = true },


            //    new Acc_Coding_Moein { Id = 179, KolId = 43, MoeinCode = "5070001", MoeinName = "ذخیره جاری", Nature = 2, IsCurrencyAccount = false, IsEditable = true },


            //    new Acc_Coding_Moein { Id = 180, KolId = 44, MoeinCode = "5080001", MoeinName = "پیش دریافت های تجاری", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 181, KolId = 44, MoeinCode = "5080002", MoeinName = "پیش دریافت های غیر تجاری", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 182, KolId = 44, MoeinCode = "5080003", MoeinName = "پیش دریافت بابت پیش فاکتور", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 183, KolId = 44, MoeinCode = "5080004", MoeinName = "سایر پیش دریافت ها", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 184, KolId = 44, MoeinCode = "5080051", MoeinName = "پیش دریافت مالیات بر ارزش افروده", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 185, KolId = 44, MoeinCode = "5080052", MoeinName = "پیش دریافت عوارض ارزش افزوده", Nature = 2, IsCurrencyAccount = false, IsEditable = true },




            //    new Acc_Coding_Moein { Id = 186, KolId = 45, MoeinCode = "5090001", MoeinName = "بدهی های مرتبط با دارایی های غیرجاری نگهداری شده برای فروش", Nature = 2, IsCurrencyAccount = false, IsEditable = true },



            //    //------- 6 - درآمدها ---------------------------------------------------------------------------------------------------
            //    new Acc_Coding_Moein { Id = 187, KolId = 46, MoeinCode = "6010001", MoeinName = "فروش", Nature = 2, IsCurrencyAccount = false, IsEditable = true },


            //    new Acc_Coding_Moein { Id = 188, KolId = 47, MoeinCode = "6020001", MoeinName = "برگشت از فروش", Nature = 1, IsCurrencyAccount = false, IsEditable = true },


            //    new Acc_Coding_Moein { Id = 189, KolId = 48, MoeinCode = "6030001", MoeinName = "تخفیفات نقدی فروش", Nature = 1, IsCurrencyAccount = false, IsEditable = true },


            //    new Acc_Coding_Moein { Id = 190, KolId = 49, MoeinCode = "6040001", MoeinName = "تخفیفات نقدی برگشت از فروش", Nature = 1, IsCurrencyAccount = false, IsEditable = true },


            //    new Acc_Coding_Moein { Id = 191, KolId = 50, MoeinCode = "6050001", MoeinName = "درآمد", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 192, KolId = 50, MoeinCode = "6050002", MoeinName = "درآمد دریافت تخفیف بابت تعدیل حساب", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 193, KolId = 50, MoeinCode = "6050003", MoeinName = "درآمد حق العمل", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 194, KolId = 50, MoeinCode = "6050004", MoeinName = "درآمد تخلیه بار", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 195, KolId = 50, MoeinCode = "6050005", MoeinName = "درآمد حاصل از عملیات پیمانکاری", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 196, KolId = 50, MoeinCode = "6050006", MoeinName = "درآمد حاصل از فروش مواد اولیه", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 197, KolId = 50, MoeinCode = "6050007", MoeinName = "درآمد حاصل از خدمات", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 198, KolId = 50, MoeinCode = "6050008", MoeinName = "درآمد حاصل از فروش دارایی ثابت منقول", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 199, KolId = 50, MoeinCode = "6050009", MoeinName = "درآمد حاصل از فروش دارایی ثابت غیر منقول", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 200, KolId = 50, MoeinCode = "6050010", MoeinName = "درآمد حاصل از اجاره", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 201, KolId = 50, MoeinCode = "6050011", MoeinName = "درآمد حاصل از سود سپرده بانکی", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 202, KolId = 50, MoeinCode = "6050012", MoeinName = "درآمد حاصل از سود و کارمزد وامهای پرداختی", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 203, KolId = 50, MoeinCode = "6050013", MoeinName = "درآمد حاصل از خوش حسابی تسهیلات مالی دریافتی", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 204, KolId = 50, MoeinCode = "6050014", MoeinName = "درآمد حاصل از دیرکرد تسهیلات مالی پرداختی ", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 205, KolId = 50, MoeinCode = "6050015", MoeinName = "درآمد حاصل از خوش حسابی عوارض قانونی", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 206, KolId = 50, MoeinCode = "6050016", MoeinName = "درآمد حاصل از سود سهام دریافتی از شرکتها", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 207, KolId = 50, MoeinCode = "6050017", MoeinName = "درآمد حاصل از معاملات ارزی و تسعیر ارز", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 208, KolId = 50, MoeinCode = "6050018", MoeinName = "درآمد حاصل از سرمایه گذاری ها", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 209, KolId = 50, MoeinCode = "6050019", MoeinName = "درآمد غیر مترقبه", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 210, KolId = 50, MoeinCode = "6050020", MoeinName = "درآمد حاصل از سود و زیان فروش ضایعات", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 211, KolId = 50, MoeinCode = "6050021", MoeinName = "درآمد حاصل از بازسازی محصولات", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 212, KolId = 50, MoeinCode = "6050022", MoeinName = "مغایرت انبارگردانی", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 213, KolId = 50, MoeinCode = "6050023", MoeinName = "درآمد حق العمل کاری", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 214, KolId = 50, MoeinCode = "6050024", MoeinName = "درآمد حاصل از معاوضه دارایی ها", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 215, KolId = 50, MoeinCode = "6050025", MoeinName = "سایر درآمد ها", Nature = 2, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 216, KolId = 50, MoeinCode = "6050026", MoeinName = "درآمد حاصل از اشانتیون", Nature = 2, IsCurrencyAccount = false, IsEditable = true },




            //    //------- 7 - هزینه ها--------------------------------------------------------------------------------------



            //    new Acc_Coding_Moein { Id = 217, KolId = 51, MoeinCode = "7010001", MoeinName = "هزینه آب", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 218, KolId = 51, MoeinCode = "7010002", MoeinName = "هزینه برق", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 219, KolId = 51, MoeinCode = "7010003", MoeinName = "هزینه تلفن", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 220, KolId = 51, MoeinCode = "7010004", MoeinName = "هزینه کرایه", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 221, KolId = 51, MoeinCode = "7010005", MoeinName = "هزینه مطالبات مشکوک الوصول-سوخت چک", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 222, KolId = 51, MoeinCode = "7010006", MoeinName = "هزینه ضایعات کالا", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 223, KolId = 51, MoeinCode = "7010007", MoeinName = "هزینه تخفیفات-تعدیل حساب", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 224, KolId = 51, MoeinCode = "7010008", MoeinName = "هزینه کمیسیون واسطه", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 225, KolId = 51, MoeinCode = "7010009", MoeinName = "هزینه کارمزد بانک", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 226, KolId = 51, MoeinCode = "7010010", MoeinName = "هزینه حمل", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 227, KolId = 51, MoeinCode = "7010011", MoeinName = "هزینه گرد نمودن مبلغ فاکتور", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 228, KolId = 51, MoeinCode = "7010012", MoeinName = "هزینه عوارض شهرداری", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 229, KolId = 51, MoeinCode = "7010013", MoeinName = "هزینه مالیات بر ارزش افزوده", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 230, KolId = 51, MoeinCode = "7010014", MoeinName = "هزینه بابت کد اقتصادی", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 231, KolId = 51, MoeinCode = "7010015", MoeinName = "هزینه مالیات", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 232, KolId = 51, MoeinCode = "7010016", MoeinName = "هزینه حقوق پرسنل", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 233, KolId = 51, MoeinCode = "7010017", MoeinName = "هزینه عواض شهرداری(قدیم(", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 234, KolId = 51, MoeinCode = "7010018", MoeinName = "هزینه عوارض آموزش و پرورش(قدیم(", Nature = 1, IsCurrencyAccount = false, IsEditable = true },



            //    new Acc_Coding_Moein { Id = 235, KolId = 52, MoeinCode = "7020001", MoeinName = "هزینه آگهی و تبلیغات", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 236, KolId = 52, MoeinCode = "7020002", MoeinName = "تخفیفات تجاری و پرداختی", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 237, KolId = 52, MoeinCode = "7020003", MoeinName = "هزینه شرکت در مزایده و مناقصه", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 238, KolId = 52, MoeinCode = "7020004", MoeinName = "هزینه نمونه های ارسالی", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 239, KolId = 52, MoeinCode = "7020005", MoeinName = "ضایعات محصول", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 240, KolId = 52, MoeinCode = "7020006", MoeinName = "هزینه دو در هزار فروش", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 241, KolId = 52, MoeinCode = "7020007", MoeinName = "هزینه های نمایشگاه", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 242, KolId = 52, MoeinCode = "7020008", MoeinName = "هزینه بازاریابی", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 243, KolId = 52, MoeinCode = "7020009", MoeinName = "هزینه های حمل و انبارداری", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 244, KolId = 52, MoeinCode = "7020010", MoeinName = "سایر هزینه های توزیع و فروش", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 245, KolId = 52, MoeinCode = "7020011", MoeinName = "هزینه اشانتیون", Nature = 1, IsCurrencyAccount = false, IsEditable = true },


            //    new Acc_Coding_Moein { Id = 246, KolId = 53, MoeinCode = "7030001", MoeinName = "دستمزد مستقیم", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 247, KolId = 53, MoeinCode = "7030002", MoeinName = "سربار ساخت", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 248, KolId = 53, MoeinCode = "7030003", MoeinName = "دستمزد غیر مستقیم", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 249, KolId = 53, MoeinCode = "7030004", MoeinName = "مواد غیر مستقیم", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 250, KolId = 53, MoeinCode = "7030005", MoeinName = "سایر هزینه های عملیاتی", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 251, KolId = 53, MoeinCode = "7030006", MoeinName = "هزینه های جذب نشده", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 252, KolId = 53, MoeinCode = "7030007", MoeinName = "ضایعات غیر عادی", Nature = 1, IsCurrencyAccount = false, IsEditable = true },


            //    new Acc_Coding_Moein { Id = 253, KolId = 54, MoeinCode = "7040001", MoeinName = "هزینه سود و کارمزد وامهای دریافتی", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 254, KolId = 54, MoeinCode = "7040002", MoeinName = "هزینه تمبر و سفته", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 255, KolId = 54, MoeinCode = "7040003", MoeinName = "هزینه های واخواست", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 256, KolId = 54, MoeinCode = "7040004", MoeinName = "هزینه کارمزد خدمات بانک", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 257, KolId = 54, MoeinCode = "7040005", MoeinName = "هزینه جریمه دیرکرد وامهای دریافتی", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 258, KolId = 54, MoeinCode = "7040006", MoeinName = "هزینه خوش حسابی وامهای دریافتی", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 259, KolId = 54, MoeinCode = "7040007", MoeinName = "سایر هزینه های مالی", Nature = 1, IsCurrencyAccount = false, IsEditable = true },



            //    new Acc_Coding_Moein { Id = 260, KolId = 55, MoeinCode = "7050001", MoeinName = "هزینه ناشی از ارزیابی سرمایه گذاری ها جاری سریع العمل به ارزش بازار", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 261, KolId = 55, MoeinCode = "7050002", MoeinName = "هزینه استهلاک ساختمان تولیدی", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 262, KolId = 55, MoeinCode = "7050003", MoeinName = "هزینه استهلاک ماشین آلات و تجهیرات", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 263, KolId = 55, MoeinCode = "7050004", MoeinName = "هزینه استهلاک تاسیسات", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 264, KolId = 55, MoeinCode = "7050005", MoeinName = "هزینه استهلاک واسط نقلیه", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 265, KolId = 55, MoeinCode = "7050006", MoeinName = "هزینه استهلاک اثاثه و منصوبات", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 266, KolId = 55, MoeinCode = "7050007", MoeinName = "هزینه استهلاک ابزار آلات", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 267, KolId = 55, MoeinCode = "7050008", MoeinName = "هزینه استهلاک قالب ها", Nature = 1, IsCurrencyAccount = false, IsEditable = true },


            //    new Acc_Coding_Moein { Id = 268, KolId = 56, MoeinCode = "7060001", MoeinName = "ضایعات غیر عادی تولید", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 269, KolId = 56, MoeinCode = "7060002", MoeinName = "هزینه های جذب نشده در تولید", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 270, KolId = 56, MoeinCode = "7060003", MoeinName = "زیان کاهش ارزش موجودی ها", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 271, KolId = 56, MoeinCode = "7060004", MoeinName = "زیان ناشی از تسعیر بدهی های ارزی عملیاتی", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 272, KolId = 56, MoeinCode = "7060005", MoeinName = "خالص کسری انبارها", Nature = 1, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 273, KolId = 56, MoeinCode = "7060006", MoeinName = "هزینه کاهش ارزش دریافتنی ها", Nature = 1, IsCurrencyAccount = false, IsEditable = true },



            //    //------- 8 - بهای تمام شده -------------------------------------------


            //    new Acc_Coding_Moein { Id = 274, KolId = 57, MoeinCode = "8010001", MoeinName = "خرید", Nature = 1, IsCurrencyAccount = false, IsEditable = true },


            //    new Acc_Coding_Moein { Id = 275, KolId = 58, MoeinCode = "8020001", MoeinName = "برگشت از خرید", Nature = 2, IsCurrencyAccount = false, IsEditable = true },


            //    new Acc_Coding_Moein { Id = 276, KolId = 59, MoeinCode = "8030001", MoeinName = "تخفیفات نقدی خرید", Nature = 2, IsCurrencyAccount = false, IsEditable = true },


            //    new Acc_Coding_Moein { Id = 277, KolId = 60, MoeinCode = "8030001", MoeinName = "قیمت تمام شده کالای ساخنه شده", Nature = 1, IsCurrencyAccount = false, IsEditable = true },


            //    new Acc_Coding_Moein { Id = 278, KolId = 61, MoeinCode = "8030001", MoeinName = "قیمت تمام شده کالای فروش رفته", Nature = 1, IsCurrencyAccount = false, IsEditable = true },

            //    //-------- 9 - حسابهای انتظامی -----------------------------------------------------------

            //    new Acc_Coding_Moein { Id = 279, KolId = 62, MoeinCode = "9010001", MoeinName = "چکهای امانی دیگران نزد ما", Nature = 3, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 280, KolId = 62, MoeinCode = "9010002", MoeinName = "ضمانت نامه های انجام مناقصه و مزایده دیگران", Nature = 3, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 281, KolId = 62, MoeinCode = "9010003", MoeinName = "ضمانت نامه های حسن اجرای تعهدات دیگران نزد ما", Nature = 3, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 282, KolId = 62, MoeinCode = "9010004", MoeinName = "ضمانت نامه های پیش پرداخت دیگران نزد ما", Nature = 3, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 283, KolId = 62, MoeinCode = "9010005", MoeinName = "ضمانت نامه های استرداد کسور وجه الضمان", Nature = 3, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 284, KolId = 62, MoeinCode = "9010006", MoeinName = "چک های تضمینی نزد صندوق دیگران نزد ما", Nature = 3, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 285, KolId = 62, MoeinCode = "9010007", MoeinName = "اسناد تضمینی نزد صندوق دیگران نزد ما", Nature = 3, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 286, KolId = 62, MoeinCode = "9010008", MoeinName = "سایر اسناد تضمینی به نفع شرکت", Nature = 3, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 287, KolId = 62, MoeinCode = "9010009", MoeinName = "کالای امانی ما نزد دیگران", Nature = 3, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 288, KolId = 62, MoeinCode = "9010010", MoeinName = "موجودی ضایعات", Nature = 3, IsCurrencyAccount = false, IsEditable = true },



            //    new Acc_Coding_Moein { Id = 289, KolId = 63, MoeinCode = "9020001", MoeinName = "چک های امانی ما نزد دیگران", Nature = 3, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 290, KolId = 63, MoeinCode = "9020002", MoeinName = "ضمانت نامه های شرکت در مناقصه و مزایده ما نزد", Nature = 3, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 291, KolId = 63, MoeinCode = "9020003", MoeinName = "ضمانت نامه های حسن اجرای تعهدات ما نزد", Nature = 3, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 292, KolId = 63, MoeinCode = "9020004", MoeinName = "ضمانت نامه های پیش پرداخت ما نزد دیگران", Nature = 3, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 293, KolId = 63, MoeinCode = "9020005", MoeinName = "ضمانت نامه های استرداد کسور وجه الضمان ما نزد", Nature = 3, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 294, KolId = 63, MoeinCode = "9020006", MoeinName = "اسناد تضمینی بابت سفارشات ما نزد دیگران", Nature = 3, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 295, KolId = 63, MoeinCode = "9020007", MoeinName = "اسناد تضمینی بابت ضمانت نامه های گمرکی ما", Nature = 3, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 296, KolId = 63, MoeinCode = "9020008", MoeinName = "کالای امانی دیگران نزد ما", Nature = 3, IsCurrencyAccount = false, IsEditable = true },


            //    new Acc_Coding_Moein { Id = 297, KolId = 64, MoeinCode = "9030001", MoeinName = "چک های صادره تحویل نشده", Nature = 3, IsCurrencyAccount = false, IsEditable = true },


            //    new Acc_Coding_Moein { Id = 298, KolId = 65, MoeinCode = "9040001", MoeinName = "طرف حساب های انتظامی به عهده شرکت", Nature = 3, IsCurrencyAccount = false, IsEditable = true },
            //    new Acc_Coding_Moein { Id = 299, KolId = 65, MoeinCode = "9040002", MoeinName = "طرف حساب های انتظامی به نفع شرکت", Nature = 3, IsCurrencyAccount = false, IsEditable = true },


            //     //-------- 10- تراز افتتاحیه و اختتامیه -----------------------------------------------------------


            //     new Acc_Coding_Moein { Id = 300, KolId = 66, MoeinCode = "0010001", MoeinName = "تراز افتتاحیه", Nature = 3, IsCurrencyAccount = false, IsEditable = true },
            //      new Acc_Coding_Moein { Id = 301, KolId = 67, MoeinCode = "0010002", MoeinName = "تراز اختتامیه", Nature = 3, IsCurrencyAccount = false, IsEditable = true }

            //    );
        }
    }
}
