using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using Stimulsoft.Controls;
using Stimulsoft.Base.Drawing;
using Stimulsoft.Report;
using Stimulsoft.Report.Dialogs;
using Stimulsoft.Report.Components;

namespace Reports
{
    public class Report : Stimulsoft.Report.StiReport
    {
        public Report()        {
            this.InitializeComponent();
        }

        #region StiReport Designer generated code - do not modify
        public string CompanyName;
        public Stimulsoft.Report.Components.StiPage Page1;
        public Stimulsoft.Report.Components.StiPageHeaderBand PageHeaderBand1;
        public Stimulsoft.Report.Components.StiText Text1;
        public Stimulsoft.Report.Components.StiText Text2;
        public Stimulsoft.Report.Components.StiText Text3;
        public Stimulsoft.Report.Components.StiText Text4;
        public Stimulsoft.Report.Components.StiText Text5;
        public Stimulsoft.Report.Components.StiText Text6;
        public Stimulsoft.Report.Components.StiText Text7;
        public Stimulsoft.Report.Components.StiText Text8;
        public Stimulsoft.Report.Components.StiText Text9;
        public Stimulsoft.Report.Components.StiText Text10;
        public Stimulsoft.Report.Components.StiText Text11;
        public Stimulsoft.Report.Components.StiText Text12;
        public Stimulsoft.Report.Components.StiPageFooterBand PageFooterBand1;
        public Stimulsoft.Report.Components.StiColumnHeaderBand ColumnHeaderBand1;
        public Stimulsoft.Report.Components.StiText Text13;
        public Stimulsoft.Report.Components.StiText Text15;
        public Stimulsoft.Report.Components.StiText Text16;
        public Stimulsoft.Report.Components.StiText Text17;
        public Stimulsoft.Report.Components.StiText Text28;
        public Stimulsoft.Report.Components.StiGroupHeaderBand GroupHeaderBand1;
        public Stimulsoft.Report.Components.StiText Text20;
        public Stimulsoft.Report.Components.StiText Text21;
        public Stimulsoft.Report.Dictionary.StiSumIntFunctionService Text21_SumI;
        public Stimulsoft.Report.Components.StiText Text22;
        public Stimulsoft.Report.Dictionary.StiSumIntFunctionService Text22_SumI;
        public Stimulsoft.Report.Components.StiText Text33;
        public Stimulsoft.Report.Components.StiText Text32;
        public Stimulsoft.Report.Components.StiDataBand DataBand1;
        public Stimulsoft.Report.Components.StiText Text23;
        public Stimulsoft.Report.Components.StiText Text29;
        public Stimulsoft.Report.Components.StiText Text30;
        public Stimulsoft.Report.Components.StiText Text31;
        public Stimulsoft.Report.Components.StiText Text18;
        public Stimulsoft.Report.Components.StiColumnFooterBand ColumnFooterBand1;
        public Stimulsoft.Report.Components.StiText Text25;
        public Stimulsoft.Report.Dictionary.StiSumDecimalFunctionService Text25_Sum;
        public Stimulsoft.Report.Components.StiText Text26;
        public Stimulsoft.Report.Dictionary.StiSumDecimalFunctionService Text26_Sum;
        public Stimulsoft.Report.Components.StiText Text27;
        public Stimulsoft.Report.Components.StiReportSummaryBand ReportSummaryBand1;
        public Stimulsoft.Report.Components.StiText Text35;
        public Stimulsoft.Report.Components.StiText Text36;
        public headerDataSource header;
        public articlesDataSource articles;
        
        public void Text1__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            // CheckerInfo: Text Text1
            e.Value = "سنــد حسابداری";
        }
        
        public void Text2__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            // CheckerInfo: Text Text2
            e.Value = ToString(sender, CompanyName, true);
        }
        
        public void Text3__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            // CheckerInfo: Text Text3
            e.Value = "شماره : ";
        }
        
        public void Text4__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            // CheckerInfo: Text Text4
            e.Value = "تاریخ :";
        }
        
        public void Text5__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            // CheckerInfo: Text Text5
            e.Value = ToString(sender, header.DocNumber, true);
        }
        
        public void Text6__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            // CheckerInfo: Text Text6
            e.Value = ToString(sender, header.DocDate_Sh, true);
        }
        
        public void Text7__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            // CheckerInfo: Text Text7
            e.Value = "#%#صفحه {PageNumber} از {TotalPageCount}";
            e.StoreToPrinted = true;
        }
        
        public string Text7_GetValue_End(Stimulsoft.Report.Components.StiComponent sender)
        {
            // CheckerInfo: Text Text7
            return "صفحه " + ToString(sender, PageNumber, true) + " از " + ToString(sender, TotalPageCount, true);
        }
        
        public void Text8__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            // CheckerInfo: Text Text8
            e.Value = "عطف :";
        }
        
        public void Text9__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            // CheckerInfo: Text Text9
            e.Value = ToString(sender, header.DocAtf, true);
        }
        
        public void Text10__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            // CheckerInfo: Text Text10
            e.Value = "دوره مالی " + ToString(sender, header.FinancePeriodName, true);
        }
        
        public void Text11__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            // CheckerInfo: Text Text11
            e.Value = "شرح سند :";
        }
        
        public void Text12__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            // CheckerInfo: Text Text12
            e.Value = ToString(sender, header.Description, true);
        }
        
        public void Text13__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            // CheckerInfo: Text Text13
            e.Value = "ردیف";
        }
        
        public void Text15__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            // CheckerInfo: Text Text15
            e.Value = "شــرح";
        }
        
        public void Text16__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            // CheckerInfo: Text Text16
            e.Value = "بدهکار";
        }
        
        public void Text17__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            // CheckerInfo: Text Text17
            e.Value = "بستانکار";
        }
        
        public void Text28__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            // CheckerInfo: Text Text28
            e.Value = "جزء";
        }
        
        public void GroupHeaderBand1__GetValue(object sender, Stimulsoft.Report.Events.StiValueEventArgs e)
        {
            // CheckerInfo: Condition GroupHeaderBand1
            e.Value = articles.Nature + articles.MoeinCode;
        }
        
        public void Text20__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            // CheckerInfo: Text Text20
            e.Value = ToString(sender, articles.MoeinCode, true) + " - " + ToString(sender, articles.MoeinName, true);
        }
        
        public void Text20_BeforePrint(object sender, System.EventArgs e)
        {
            // CheckerInfo: BeforePrintEvent Text20
            if(articles.Nature == 2){
	Text20.Margins=new Stimulsoft.Report.Components.StiMargins(10, 130, 0, 0);
};
        }
        
        public void Text21__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            // CheckerInfo: Text Text21
            e.Value = ("#%#" + this.Text21.TextFormat.Format(((long)(StiReport.ChangeType(this.Text21_SumI.GetValue(), typeof(long), true)))));
            e.StoreToPrinted = true;
        }
        
        public string Text21_GetValue_End(Stimulsoft.Report.Components.StiComponent sender)
        {
            // CheckerInfo: Text Text21
            return this.Text21.TextFormat.Format(CheckExcelValue(sender, ((long)(StiReport.ChangeType(this.Text21_SumI.GetValue(), typeof(long), true)))));
        }
        
        public void Text22__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            // CheckerInfo: Text Text22
            e.Value = ("#%#" + this.Text22.TextFormat.Format(((long)(StiReport.ChangeType(this.Text22_SumI.GetValue(), typeof(long), true)))));
            e.StoreToPrinted = true;
        }
        
        public string Text22_GetValue_End(Stimulsoft.Report.Components.StiComponent sender)
        {
            // CheckerInfo: Text Text22
            return this.Text22.TextFormat.Format(CheckExcelValue(sender, ((long)(StiReport.ChangeType(this.Text22_SumI.GetValue(), typeof(long), true)))));
        }
        
        public void Text23__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            // CheckerInfo: Text Text23
            e.Value = ToString(sender, articles.Tafsil4, true) + "  " + ToString(sender, articles.Comment, true) + "\r\n";
        }
        
        public void Text23_BeforePrint(object sender, System.EventArgs e)
        {
            // CheckerInfo: BeforePrintEvent Text23
            if(articles.Nature == 2){
	Text23.Margins=	new Stimulsoft.Report.Components.StiMargins(0, 130, 0, 0);
};
        }
        
        public void Text29__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            // CheckerInfo: Text Text29
            e.Value = this.Text29.TextFormat.Format(CheckExcelValue(sender, articles.Amount));
        }
        
        public void Text18__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            // CheckerInfo: Text Text18
            e.Value = ToString(sender, LineThrough, true);
        }
        
        public void Text25__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            // CheckerInfo: Text Text25
            e.Value = ("#%#" + this.Text25.TextFormat.Format(((decimal)(StiReport.ChangeType(this.Text25_Sum.GetValue(), typeof(decimal), true)))));
            e.StoreToPrinted = true;
        }
        
        public string Text25_GetValue_End(Stimulsoft.Report.Components.StiComponent sender)
        {
            // CheckerInfo: Text Text25
            return this.Text25.TextFormat.Format(CheckExcelValue(sender, ((decimal)(StiReport.ChangeType(this.Text25_Sum.GetValue(), typeof(decimal), true)))));
        }
        
        public void Text26__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            // CheckerInfo: Text Text26
            e.Value = ("#%#" + this.Text26.TextFormat.Format(((decimal)(StiReport.ChangeType(this.Text26_Sum.GetValue(), typeof(decimal), true)))));
            e.StoreToPrinted = true;
        }
        
        public string Text26_GetValue_End(Stimulsoft.Report.Components.StiComponent sender)
        {
            // CheckerInfo: Text Text26
            return this.Text26.TextFormat.Format(CheckExcelValue(sender, ((decimal)(StiReport.ChangeType(this.Text26_Sum.GetValue(), typeof(decimal), true)))));
        }
        
        public void Text27__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            // CheckerInfo: Text Text27
            e.Value = ToString(sender, header.strTotal, true);
        }
        
        public void Text35__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            // CheckerInfo: Text Text35
            e.Value = "مدیر امور مالی";
        }
        
        public void Text36__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            // CheckerInfo: Text Text36
            e.Value = "مدیرعامل";
        }
        
        public void GroupHeaderBand1__BeginRender(object sender, System.EventArgs e)
        {
            this.Text21_SumI.Init();
            this.Text21.TextValue = "";
            this.Text22_SumI.Init();
            this.Text22.TextValue = "";
        }
        
        public void DataBand1__BeginRender(object sender, System.EventArgs e)
        {
            this.Text25_Sum.Init();
            this.Text25.TextValue = "";
            this.Text26_Sum.Init();
            this.Text26.TextValue = "";
        }
        
        public void GroupHeaderBand1__EndRender(object sender, System.EventArgs e)
        {
            this.Text21.SetText(new Stimulsoft.Report.Components.StiGetValue(this.Text21_GetValue_End));
            this.Text22.SetText(new Stimulsoft.Report.Components.StiGetValue(this.Text22_GetValue_End));
        }
        
        public void DataBand1__EndRender(object sender, System.EventArgs e)
        {
            this.Text25.SetText(new Stimulsoft.Report.Components.StiGetValue(this.Text25_GetValue_End));
            this.Text26.SetText(new Stimulsoft.Report.Components.StiGetValue(this.Text26_GetValue_End));
        }
        
        public void GroupHeaderBand1__Rendering(object sender, System.EventArgs e)
        {
            // CheckerInfo: Text Text21
            try
            {
                this.Text21_SumI.CalcItem(articles.Bed);
            }
            catch (System.Exception ex)
            {
                StiLogService.Write(this.GetType(), "GroupHeaderBand1 RenderingEvent Text21_SumI ...ERROR");
                StiLogService.Write(this.GetType(), ex);
                this.WriteToReportRenderingMessages("Text21_SumI " + ex.Message);
            }
            // CheckerInfo: Text Text22
            try
            {
                this.Text22_SumI.CalcItem(articles.Bes);
            }
            catch (System.Exception ex)
            {
                StiLogService.Write(this.GetType(), "GroupHeaderBand1 RenderingEvent Text22_SumI ...ERROR");
                StiLogService.Write(this.GetType(), ex);
                this.WriteToReportRenderingMessages("Text22_SumI " + ex.Message);
            }
        }
        
        public void DataBand1__Rendering(object sender, System.EventArgs e)
        {
            // CheckerInfo: Text Text25
            try
            {
                this.Text25_Sum.CalcItem(articles.Bed);
            }
            catch (System.Exception ex)
            {
                StiLogService.Write(this.GetType(), "DataBand1 RenderingEvent Text25_Sum ...ERROR");
                StiLogService.Write(this.GetType(), ex);
                this.WriteToReportRenderingMessages("Text25_Sum " + ex.Message);
            }
            // CheckerInfo: Text Text26
            try
            {
                this.Text26_Sum.CalcItem(articles.Bes);
            }
            catch (System.Exception ex)
            {
                StiLogService.Write(this.GetType(), "DataBand1 RenderingEvent Text26_Sum ...ERROR");
                StiLogService.Write(this.GetType(), ex);
                this.WriteToReportRenderingMessages("Text26_Sum " + ex.Message);
            }
        }
        
        public void ReportWordsToEnd__EndRender(object sender, System.EventArgs e)
        {
            this.Text7.SetText(new Stimulsoft.Report.Components.StiGetValue(this.Text7_GetValue_End));
        }
        
        public void CheckEndRenderRuntimes__EndRender(object sender, System.EventArgs e)
        {
            Stimulsoft.Report.Components.StiSimpleText.CheckEndRenderRuntimes(this);
            Stimulsoft.Report.Components.StiSimpleText.ProcessEndRenderSetText(this);
        }
        
        private void InitializeComponent()
        {
            this.articles = new articlesDataSource();
            this.header = new headerDataSource();
            this.Dictionary.Variables.Add(new Stimulsoft.Report.Dictionary.StiVariable("", "CompanyName", "CompanyName", "", typeof(string), "", false, Stimulsoft.Report.Dictionary.StiVariableInitBy.Value, false, new Stimulsoft.Report.Dictionary.StiDialogInfo(Stimulsoft.Report.Dictionary.StiDateTimeType.Date, "", true, new string[0], new string[0]), null, false, Stimulsoft.Report.Dictionary.StiSelectionMode.FromVariable));
            this.Dictionary.Resources.Add(new Stimulsoft.Report.Dictionary.StiResource("YekanBakhFaNum-Light", "YekanBakhFaNum-Light", Stimulsoft.Report.Dictionary.StiResourceType.FontTtf, Stimulsoft.Base.Helpers.StiPacker.UnpackFromString("H4sIAAAAAAAEAMR9B3wcRbJ3d8/ubFDWKgdL2l0FK0srrZItL5acZFnOCRwkB2wZ29jGZI7zA0zOmTuOA85HvCPDkc0RfJgDHtgc8WyMCQbjOzAZjHe/f9XMrlayhH3fe7/v21b39MxUV1d1V1dX9fSMhBRCJCOxiPz26RPG4UwKWZuBo31c+5ixw1rz1uJSOc4fGTdl8vSyW3JuxPlHQlSdO276zNEFC9qvEaJmpRDxyZOnV9We9/xHJwsxagPguxev6llzzfsZt+N8lxDWmxeftD7fujauXkj3z7jfdeyaZatu8ie/LKSnE+VTl/WcsAbXE3H/G6p/2cpTj/3DxQ+/I8QY4Lv/o+VLe5Z4d/3lBiG+X4L7/uW44BhrfQ3noEl4l69af8rdLbNB2/fPoPjNK49f3HO+a74XHHWDwWmrek5ZY2lOuEpIy0OAz1/ds2rpn29vvwLnbwjh2LXm+BPWf9h7NPiPx/2U1jXrlq5ZMOrA+ULWUH0PCGqrWEG/M4QSycIjvse1H0RIKBSiPyU15C3SgrxV6sjbZDzyCTIZeZfMQD5TFgpNFsli5EtkCfLD5XDkS2Up8mWyAvlKWYf8zfJm5G+Xr6MWJRwomQksWTILV7NlHvL5Mh/5AulG3iM9yHulF/lC1GFgUdyjQjhFN6C09rGdM0RKz7qeRcK7smf9ajEWPY9fKCQ0QVJgB6ScOa09X6SZV6WwCoeZV0LH/b4SmrCJGBF73NJ1q0UHpzM4XcDp8lU9644T6zg9k9MLOb121XGrjhO3cnoXpw9w+jjQakyvOmzOgrwFlOlHmFeg1D7k0SFSRL4oFyNEu+gUM8Q8sQR37Cg/R/7bcqyRtybHr0n8jnpCWF0qJT7dn350+rPcGjL9E+OY8bhxzCxBvTh6hgmdJMO7WugkJN4/CVk4jWESRSlqWwRZuk08KT6RsXKYLJfNMiDHynnyJHk+SjaHfpQtoXdkb2iPvDj0prwytENeHTogbLI+tB93X8TdN+Xy0IeA2C9XhTbJU" +
"0NfoFw9l3sPV59HuS9Qbh/KHRRO3NmDcrtxdx/K/QiIHSj3D5TbD8jvAPkjQ1plW+igHBO6FBAP485e3NmL8qMA3wYc7cA+BvUtR5ne0DajPGT2SsSr0W4OQH0BqO8AtRwQj6OWg2YtRM8XzIcFUD8C6gCg3gfUxyjdgjJtqGlMaB2fHcSZDWdNkOjjg68iXYPUCi724e6PDNse2gWId5jzN3HnCy43ClQb+L9ATfWgnMrQVbqi4coBXDlI9Qvl2UW9UvhsUbmIZ9nfHboq9HHo2dB5oX+G9oV+DL0Z+iK0A+HH0A2hJaFHcW27iPxC3wkttD30Xmi/ecEJuYK0hg6GtlJdONfCkKHdSP8Reh+tLMyrmgm9NbTHLM/XUfeO0J5QB2DXhf6Omn8X2hR6IfR2aD8o2w+KNoU2h2408d4N+g6GhuPORaE3GdfW0N7Qj4SZOdgK6i4xYd8MPYErfwG+t8Uv/IhCYH0BNf8FdR08LOybaJcP0Q5PoCcP8wtdjxb+IuQLvRym6hdgDxA+tMKOI8D7YOh1wK0KvRq65XCwgOa6w234i5BoQaQXHa4dGHZ7aBd4Ox+98+ovwh0UquAUkjtvt/dS9BRml9CJJDHg4JLQNaG/oee2I/dgpP6/H4Ljx9AOEYd+Hnj9dchSSkSeouF3A37/wOtMjSGxB6OuUHqAdVn/n8x/k7VYkZgl/iT2yhTZKi+Wt8r7VLwKqHVqk9qrBbR12suWIssZlnetiVa/dY51nfVP1p/0RL1UP0d/Vv/SVmlbabvJ9rw9y77e/qojX6TKbRilH4Xulh8jfhLaLveELpSfhm6Rn0Fn7Q0dLz+HBtkHmP24901os/wW+e9Dr8hQaKsSoR1Khu5WKvSwwmhUFuSt4DTBxPoJsO4Bxs3AuAcYtwLjw8C4D5j2A9N+lN6B0ntQegdK7+DSTpR+D6V2o8R+lNhDkDIoEgC9H9D7TWiC/AiaZw/ufgaofch/g1LfhnYB8hVAvgvItwD5qXAB54PyPdylEgRNe" +
"A3etoC3L+RXoU9ReheVlkHkQ6Hd4O9zYPo7ML0FTK8B01ug8B/QhXvA315uEYMH0A79RlwbV/aLdJxtR407UePLaIcdaN1XudynzBmV3xFp2a9AyTc4fos2+h6UBRFDoc2gYKvZwneDgs1mC+8RscD8BbCuAsZeYLzf5GkGsO0Bph0ovccsuZVKocS20H0o9bTZZttRagdKvc587EOfGP1BLbwddX3FJYiDfdyTRi/uNuvZjBIG90Z/7EYde0QSSjyLEp+gxE5Q9w+UegmldqLUKyj1LFp8J0oSr8TjflBJdW43ZYAkaDvL5N3AcpeBBa39qdnDRn9dDR53ocU2A8uHLBvfh74GtjuA7RW02APAeC0w3gCMVwPjVWixu0xuwu3+ObeSIYOCadgTRcMelsM0lDgVdNwIOl5C731httieCIZ9GCdG791n9t5m0AJZhZQb42M7MN8CzLcA871m721mzLcA8/PA/B7aab8pF/v70bcPWAzsWyPYg4jUs4ZcbAbmzaZcbGbM1Ha7gXkrMD8Gmmn07WG6Daw7uN8MrB8A690DpI3ofdqUtvB43s6yfDywng+s95hjeofZGtGYTwPm24H5WlNTPA3s7wH7zcD+LLDfavbMdcB+LbBfC+zXijhg32PK2J4orEZ/f8OSudvUFDtQcj9LiQN0PIISD5vcXQKonVGytIeh7IDax9R+FnqbtE9UT+9hmdVZf+xlTbYnShftEfGg62FwfTdrL8wG4O5tcPc6aNqK2p5jHSFDr6LUPrMXXgVH+6Bt3mP9tw+1/xG9EO5fkr+HUdcDZnu9bY77V4CNewHY9pnYeCwwNiuwfB4le9Tz+0y6P2Ut/Qa1EvNuwVyTxu35Hmr7CGU+Dt0LyLu59r2hh1DzdpOXHaid9NWjqP0e9NRuUEB6Ywd6ao8pXdujKNkDvd7X99tNarYzJtI4Yd1lyOdADJtFDOiiFiHd/rypSz4BhjvlV+DmG4yvb0U2Sr2DEvt4DtnMrf8x8O5B7JOMe8z2I12/I" +
"0oy+uaQBJTcg5L3mxKy3Zx9rmPtY+jIT0y5D2ugCK/QFcaYJI23FSWp5R6V+2HTMpVcItxPeyLS+IRJJUHfQvUMkKgdEc32MKBvMkfnLSixGXXcjVLXgi/SrM+avbM50q5BHp17TIzRvbKd+3sz9/fHoRtMabvbxPow02JoE+qpfcC6JwrrdvC/LwrzDlOOjf5OM2eAzZCk/cB+lWkh3Gti327SvJ2xf8UzAmHeaWLeY1oI4dbdY/bPDpFijq1buCWMPtpqtsJ1JsbNoHe/iXUP2x3Aamrr7dEjNWJzEI0PmP1GsrKZ9cI+k+tvWReF9X0/u0Oo9AfIusq6LWuXSBCpbM1D44deCb0FK34zbH3IDGzE7YPZcabt9jXb7vuQ7qEI6P2HWoND/1Dj9sNDMeQXR4zzi/8Admdo1xHBbUY4Ir7gc71+hHXvH7pdB0C+Erop9MARQb4Vui30fOjrw2NGD2+BDAiKh4Fk3o+kTSExF6P+fYfHSe1kZlTqHJLCjC0Z34k4kcLt8gVqvD10CyjcHDqb6gbOPSQrCFtJ2kwcQcSbQg+z/3odl/wEYY8hi1F1HYYeAxZ1HIZHE+5t6PXDcfc5UXM4qAj0vsPDMNyRysvWw9zfY2A63Ngz+d1++DHKkrTviMfykfArXYvYI4wVblEtWsVYMUMsEmvEmeJccbW4VbwrlDxF+IRPXiJyEfqfSXls6N7Qvbi6JPRC6AW5lNL/4IzKPxx6+BfrcIjhkNcUsZDTbk57RAHCYkrl03RFbub0Gb7yLN+1o0QCQjenPZwuphQlKN3M6TOcPst3VfI6aou0QNrFqDWN2ycVoRDhf/uXiEA/F0ISAtURhzAcgWj5v/11INAP7ScqERoQqhHoV4HQjLASIQbBhmBBoPxhfrRaJiaLyXwUhraK3Hu9TxeHfo+wIyrsRaDjU31hUPy0wmXe4/zWvtEVehlhiHL9cABuSHqjyoce6NPyh9D7L4QjoTfq3kC40OMIR0IvwXgQjuAX+vjwq2X/m7+hZ" +
"k2+rhJOoXGS+mXqN/xEgX7ZCCS3VgR6lqASlvBYKkmj5xaGfGUiOBGIZy28hnqE9FCfPIrwG4S5CLsRDtBab0Is07IkbTxocTAsLJPQJgT0LdUXOh+B5jSSRVoRk/G7478CVbFiAbSGFGcIegokB4likPxQsOHoxGjzixGIo8V40QV9KsTRiN2IxyKuFOuYpVP4WQ49AzkzdCPSk7SFSK/j/L7gc9CNqepkpK2hxUh7VQWuZ4qXqZRUnKYKGTooJ1A+uIWv1IM3Pb4yirc5qGEOHwdGMUh+KNi+GCuSw3TLNaFGStWTQsZdHvd0X62W84VAlINEMUh+KFgzyjjLCMtoy3hLl2WG5WhLt+VYy0rLOsspiCstZ+J4Jo7n4HiO5UJcu5yvX4vzay03Wm7leIflHstDlsctzyBPcQvOX8b5NsvbHHdaPrLstXyJ/Hccd1p+tuy1Kss2q53jTms8zlOsWdZ8a5G13FprbeQYPm9FvjXqvB35dj7vwHkH8lM4zrLOQ1yEuBxxNeJ6xNOsG6znmvFiM15pxuvNeBPHTda7EO+zPmJ9kuMm67McH7G+yHGT9VXEN6zvWndx3GT9hOO71n0cv7L+wDGoWzh+pTs5BvVEinqanoPo1kv0Sr1Ob0Y+gDgW5504n4b8HMQFOF+C8xX6Gv0kxDP0s/Tz9Uv1q5H/DeLNOL8N53+ybtIfoBi5/6j+NMfn9Zc4Djx/TX9T/6e+2zpL/5Qi7v2b4/P6NxSB6yeKmLh0W6xNt26yJVO0zLBl4HyYzWsr5Vht81PEvREULTttoy17beOR76IY7n/kZ1C0HW3rNuOxZlzJcR2urePrp0TimbZzbBfaLrdda7vRdqvtDpzfg/OHbI/bnrFtsb2M8222t207I3Af2fbavrR9FwV/Du7/bFd2uz3ensLl37Zn2fPtRYjl9lpDVgc5b0Rstey0t1v22juQn4I4y15rn2dfZF8ehofs3gNZfty+2r7efpp9A47n4nixEPYrKdqvt98EGb+Vor7Gv" +
"gnxLv0s+336pfZHrJvsT1IE3LP2F2mQR46v2t9A+q59l/0TxH32rxB/QAza9zksfH2fw2l/15GIa7scaY4chxuxxFGJWIfYjBhwjHV08r0SxzQc5+CY41hgPdeMF5vxSjNeb8abOC5xrOC4xnESxzMcZ3E833EpxyWOqzmucfyGYkS71rJGrZULkE7h/BTOX8v5aykvnZz/iXWsoY3PVJ9GtPF/aT6kp2onIN0YnId0E6fnaGNR9sHgTqQPyONw5TjKix3yXqqF83+mPGBWIj+J0ymUyj+HLqHrqhRXHqO8eEJ1IO3l/J2Ux93f8vUn+QpS+Zi6H/ltahvSuyiV84IzkTYqK9IZnK+ivPiWqUoy81SjxjW2qD8gTaRUfE3cyWQ1F2kFp7HqB8bwNdJ47Uyk9drpgDwh+Hek+5mvr5jOr5iqr5iSG0KYieRymrO09QSjnchcv8P43+UrZzD971BqSVaYvywuSuUWglEdfCWH+FVdlMq13PKPUS/IRZzfyPm9fPdJTtuY37HKhtTPaTnXWEd58TrnXXx9nKLdF9XqVoa8la//nq5ryXRdc9F1Tl1aItI7qa3UPeoeTjfhShy3XhblxXucTzbuavmcptFdTpP5yjCeu+9lyt+XHyP9jvP/Ujtw/RrOP0/X5W85/xLDfK6NRLpTrUV6mdZCPSJXIG/A/JZlcgnztUjBexLXMPy1nK6jPpJ/l3vIcmCYPPk+0m75Je7u5l7bxHI1Wy1CuliOi1ga6fJvzHspWx3fUiovj1ggVsbmVPORytArJHVqNMp+E3yDIQ8gLWOYWTSCUOp7pJ3EqfiMKd+vbaQdNVzWo1UjdWi3Ie0I/ZnkVovvb+fIkxjzbyXJ4Wq5FRjeCj5OqaRx3RNED4qbWMbmqiVI1wfvxpXtEqNSXij+ymONRt8r6nZc2ahikf+EqBK3c/qqwliWZ8h3iEJuySnBXTzKyCfNZV7ccgvlg49Qn4o/Iq3RsrhV9wNyNVlrlg0kCZbniRL1ZwVZUncqB2CGc" +
"ZrGV/KJC2ii6ygN9bKdZmebDWNNnMJ0/iyv4TzphxdDxN0BNY3T53hUUs9m8JXv1Susqe5Aeh6P8RQuex6X/btZtoNTGnFW9SJxwVdup3aTWQQPDUB9oRjPCUHK72WuZ7Bs3MB5jfP7OV+j3kJ6B/fmF5QX3yq0jLieUu1K7ZuI9SfEPpIrPg6McpD8ULDhGCuyhBeebCPs6U4xC3b0CrFenCkuRo3nIr1SXI/cGeIscb64FLl1sKwvFOcgt1ysFhvEacgtEEsAsQa5GbDF1wnaBdQhpgDbauQCYiww0y6hbtjtx8JqV7hTK+aJVngpC2D3rsDdk1BeQ5kOXJ8F3IsidvCfWDPdyW1zIo/R9Zxfx7K2TjNmjF6C1K4iGEq101l2HGoDZGQy65u9LKehEPX2cBph6gH1ANvcw/j5fE6/aBGJIg1Hd2SmE6ylRWgS57dxHiNLnaJ+w1i6eM3h6P9RjAEtXlFq+jhhD+foPhrkCM4/y3kP53/m9CmkFsmQsobzx/D17/hKnjA9GpEo62WTbJYtcpRsk+1yjFwuV8nj5Rp5qrxIXiGvQu8owIwH/Cq5ir0cB2COl2sZ4hJ5mQnlkNskrBb5pnxbaPI9uUPo8gP5Aa5/KPcJp/y3/EKkyP3yK5Emv5Hfiwz5M+bLHOBLoZLyHyj5lnwHM9g/5Q65U74vd8ndKPux3CM/lZ/JvfJz1GKV/5L/Qi1fsrb6DtrQKg/KoIhREtjiuOWHo8Vmix7IzGJI0lPiJ3FAniwv5R1rxZBtIeZD7uzi1wjJuP+UcPFexBTehZgjn5QvoN3Bt6iCXAreoWjBQZcroR8vlBfLK+XVgubTXtghgluI9iYK3jdINDhRslq8KLbSbkU5XJbJctqdiBI38444D1NRI/4Gal8S34s/cB2v8/7H7bzz8Q3e8/hP3sO4G2U0plAwhXbeIenkXs2S+dKDuqllaJej4P2Nxs5Gnfc02pkqaEgRA4hsmSNz5TDoVUDgfoksRdkk9HC9iJN+6QcvDbIRm" +
"JtkE8o2y2bgaZEt6MERkDZNtspW4B8lR4lYGZABwB8lR9N+NN5t1i7bQckYOQb9PlaOBfx4yI5k2YlBTTlcUzJqasI9wu5k7ImM3SpHypE0O6EOxXUkoY7RgCTsMYzdwdgTGLtVjqN5letQkMrloKBX9op49NRK0ES1utBnq5E/Xh4PTtbKtUhPlaeC+ovlxeD5UkgG5l/MwKno1yvB89Xo3RSSFsYoGVqDrF8ELJfIS1DrZfIypFfIK1DrVZB+nWw3lInhlRYKOlqcZEbx/jVjv6cVPW9ck3Ib9Gx4X6bF2PHI41MXNjPHjgAirbI4aa8oJCueS5J97IJ2cXEwjso8M84paqzFrIzRwJPCa59JjDmFcdL8kYyQZEbjFy8SuDbMRiKVZc+gh+jso43wGjQ6TPqItjpAu5g+V+QoIrRJM4p+tFF5HkXcIvSLMXcLE23KjMYvjqlLBLXKHOlKlIlyYJ2P+cSKUb8EFCzF+Lby+Lbx+LaTDgDkz+Ig2bNyM2TgGfkc+vN5jHUdd1aKIoQY6IdilKd1YSfwlqE8reOSJqhCa9E6bwx0sR/101pvjGhBiMHM1QosoxBioKVHI9+GEIOZbizy4xBSea04RkxESIUW70J+MkKqmIYQI6YjpGKmm438XIQYcQxCDLiaD24Xgrd48NYDPhcjSHC4FNeXIcRgvjwO6WpxPNJ14gSkJ4mTkZ4mTkd6JkIMt0UCZu2zkD8bIQGz+bnIn4eQgBn8IuQvFZchvRIhRlyFECuuRYiBLXUd+us3CBbxWwRN3CR+j/yt4g9Ib0OwiNsRNHGXuBvpvQgx4gGEGPEgQqx4BCFG/AUhVjyBEMu9Eic2I8TBgvwr0ucQ4sQLCHHQi39DuhW6MUa8Il5F+hpCjNiGECPeQIgRbyLEiHfEu0h3IsSIXQix4iOEWPEpQqz4HEGHVUN2zZcIseJr8Q0gf0DQIBM/If0ZQRNBBI2FW4OOhczLRXIpxvixEjOFXCaXIX+BvADah/SAg8e+g8e+A97oA" +
"9BoD8qHIVWPykehcR6TjyH/hHwCOutJ+SS029OQuXjI3DOQv2cl5mtI3vO4vgV2b4zcKsGpfEW+wjqSxkI8y5SFpcnCcmThftC4H4we0LgHNO4BjXm1MNWSqVZMtWS6LEwR7a1faY5m0kIpLOcay5iFZUwdImMW1s4O1s421s5W1s4662In62I762Ib62Ir62KdubYw1+oQri2mtrSJvp/OdKnIOe2nt0auGfvwf6Oe6bcPv5b34a+L7Kp3muXiMDpTRabIFQWiMLIr32vCGLvy46FR0qCDh8GiK+I7MeYe/ViMB5dIF9kiD/N0cRR2Y89+IijPgN1CGEvE8J6elevFD5TCVaLUzmnisb2re2QGp8M49XJa2ru6d72s5tTP6QhOR688fvFKOX7V0iW9sovTGZwezWn3upW9y+SxnJ7C6YWc3ngC1XUPpw9x+jinz5xwQnWN3MLpy5xu4/RtpLVyJ6cfcbqX0y+R+uR3nP5MqVKc2tevPnGViuc0hdMsTvNZRo1+GeoozVnt0FQbIk3k1DJoaouaK4zZ4f/uiuT5b0Cq0bOEOO0kTtdwuoJTWueP157h9HFOH+L0baQJ2jZOX+aUIBNE+B2LgakLklINnyoAC34KPJxueDRr4DltEJdjJN8m7oF+fFpsMdoNGLk9LLPMdrzWPL5hHreZx9fM9nkVRwePbQvbmKeY52exFSLll3y0yp9UrBqmqtVowzpR7WqWWq5OM7Cps8zjP81j0KTGYh7JiwY2vU4ofaXIl51ykuySk+UUOVVOk9PlDDlTzpKz5Rw5Vx4tj5Hz5Hy5QC6U3bJHJaoklaxcKkWlqjSVrjJUJjD5aXzr1WQP6RuIchv1Vgpm1zmYs87HPHQjOHGanlcZWdk0F8ssaC0lszEjK1kQBVHOEJUMUcoQ1QwBixmjPUfS09cP0RO3c25POAeNjRzL2WuMgXKvR3KfRnKfGTl9Hc7sbH2T1rpTwA/Ul0SuWQEFe1CnHSDx6PNFsC2s/CQng725H8FvCVsjp" +
"bBGFKgOIa2UJCfV/SB/YMifGPJnhgwypGBIqktjCMopzrHcWP+N+AP3WjK1sBU9qp9jjspkA4Z40M/qd6Ub8aR+V7oQz+jDo9+MeEcUHqmXIm4agPlKxMp+Vzaw1PSVAk79+kgpOij9clEOz8BPvofpj47AbNOK/AhT0hr7SdoUHAeDHwSWFg1QQzvPtn+TL2K2DR9fkn+XL2PefVX+t3wNnhY961O6Fz33jfiWo9LdsAUmYJ7rQD4fPuLhcPSVtGPWLkHflUMmq+FJzoHnPk8swOhfBCv1WLO8kRo4DAxPQhc8A59+i/hvyOF28Q/xPvAhSLuMj4y7qdwOQ7XNYO0gdR3WxIUYV7+B5XAX7LPHUctL0CfvwsfcK74SP8H2ccpkeJVuWSpr0aZjgeFoWBUr5Dp5mjwL3u+V8jdyk/yTfAhz/BZw/QY89l3yE7lPfiV/kEFlUU6M9zSVo9yqBNqmUbVC03SoKdA289QiaJzVkAK/qGdfoVk0YYxosHVGCniXoO8oaEkNLTdGtOPYISZAZ2qwlyeJThynianQoBos5ZliBo4rRC90qQbrd5Ug22adWMsrRyeJE8V6HE8Tp0LPatAnvxJn4HiW+C9oXQ2270ZxDo4XigugaTTYvpeIi3G8UlwBnaxB514jrmar9wZxPWw0C/T078SNnLtV3CJuhsVGtu8fxSb5Klu+d4o7cLxH/Fn8CccHxP3iPhwfEQ+Lh3B8XDwmHsXxFfEy9IUGu/Vt8RaOO8Q/xXusl3aLDyA3GvTSJ+Jjtuf2QuNo4gvxb/EvQHyN/tlPEDJWxkgntMwUOQHpNFhgCj0M+YT+rUc6m734ObIB6Vz48go92IT0GHjbCnq5Bel82HMK+nkk0oXsbXczTA/7+Fshk3/D8RVI5d+FUomEVyURXpVMeJWL8KoUKqNSCa9KI7wqnfCqDMKrMgmvJUvkyjtgI98JS/QuSM3d8mFIzlNkG8Iy/CvswufIC2OYv8g/w4KmeA8s1nvl4+bqUwepCJUALS+h+" +
"33wvFrga7XBr5oIH2o6vKDj4P2cAK/ndPg5Z8OnuQi+zFWwl38LO/kP0O53wyd5EP7HS/Aa3oWf8BH0+j7xpXRAhl8C1nZYesMxx1RgVqkZpIbZ8MmOgZ28EPbxYtjFR17jE/B0NsPDeQF1v4oZZhvmcaJhZx8V4htpAyVxJi20SlWCul6CpHwkPmaf+y5Oi3keuhNnf4akKfZ6j2VNOglWrMFBH/2t4GA0eBiLUTSNfUviYRn7i+Qtkq9IniL5iWdiZJzLfiB5gVdC/q+D7P+W/QzyMm5DnfeAowcg0eSXvRLFyzvMzefsW32HWesn8bOklaotrB1pBV3HOH8csv4hpPsL5qSSV8n6dOFTkIa/QhLo6UAmRnwnRvkMjNRzMDqvxgh6NKIVn4defBGj6C2MoA/QOp9hdOxnK6uHV4mt4Hs0eP6ca7JCvzyGsfUJRhGtDY7E3aNwfwwgHgYvewH3bxNyJO6MwZV/05qHHEVShxHVI2Ihe5mYk6m9r0NLGDbpLtT+JdesYQZqIbnHaDXu071Evkv7N8b2k62BknQ9NMxvYOn8Dm19MzTLrWITtMptUXITbumdwLqfZSUuMvc186z3H9ld4P5/dwQx92ivAK0EmqvIY+ENjse82UEyjfazKbtyYGaIgQ0ap+IxliX8sybIRXSPzIdEkB8aXruldZzHWHJ2Q3Y+gfSE+wuenpovy2lNTRUp8uAajSO8zFpIoLnERL8NMFbNLK2DiZkTZ3dBkvJDP2ufHnwXDv+nVA402Km0ojfW87lweH0uxfSZU4TFejGOLwOlDgmtAv0BzOnUl6dhfrmZx8mdGCf387h/S3yvO/U4PVXP1vP0Zn2WPkfv1dfqp+pn6ufq5+tX6Nfo1+u36Jv0u/Q/6ffqD+uP60/rz+lb9Jf0l/X/1rfr/9B/zFf5cfmp+Zn5ufnu/KL8yvyqfF9+c357/sT87vw/FbgLHnYPc493d7uXep2Fdxc+V/hy4ScHph6Ye2DVgV8duOjANQd+f+DOA/cfe" +
"OTAYwfePfDhgc8OhH5efHDUwa8O/hz8JvhT8OdgKPQzvFviuRq9cRRkkyTzdHBzK+TwTvT7fRgnBjcOPdbkpkAPgJtufcUAbm4GN3dGuHlKfwbcbAU3r+rbwM2X+SJfy4/Pz8jPYW5KwE1tftMg3CwBN3cVPsvcTDkw68CyA6cduODAJQduOrDpwN0HHgA37xzYfeDTAz//vOBgK3OzIfht8ECYG/km4hWIrfIiyMax8mzkTzalYDt66bwDPUIccB3oOEBfbhA/bfvpfSF+fOnHx3584cftP2778bUft+L8c8Tvf/zyx+9+Ygn68QAgUzgX+mkMhrlNiA/mf3DMB//1Qdf7+9//5IPAB7M+qPhgxvt/+eDkD8TOKe/3vt/0fv77Be/99N7373343hvvPW35oG/Jw5rLaY91oeXT8HXLRM0FT0soeqqRzk9HaJflp4hfMUBf+hVr2vDvG/P4pXTTQXaIfj/WZZFrGKFjw9dkZ+R+p/gPf7ARqzHyRzO2GXJB5PoiuUSuMPOrowpocpk8QZ6qvPBDc2FRXiQvlsvlaSpLno4yx8slKlueLX8tz4H1mCevkmvk1fIKeY28UhXLa+UlsD9vVAXyfHmGvEm45WJJq7akWTdDt34KLeuDriS9Ni4yZ9PYHKjbvNBtV0Gv3c4a7SXo2S/FR/w8xMNPRvJlrsyR2TITdnCh7JWr5SpQSVrXC/19nnSI62AH/gV68DVowm3Qy8+Jv0EXvsDznw1esl0UQOmcDkv5Bnm9vE7+Fvq2kW3ddrZsL4ONSZbmRGgRsmznscZbAm17PVucv+fRdwvwv44a3mMr8S22Gcmz+E58z9YgzTKwG2Gn+2SlrEJf1HF7lPKK+GbouArMs59ihiRbkmwKH2xusrxfhWfRAnubrO5R0GFkdY/DrEvW9mTMvGRlT8f8S1b2bPgtc1nLGbp5GextsrqPg71NVvfxsLfJ6j4B9jZZ3SfD3iar+3TY22R1/xrakqzuszGXk9V9EexssravwrxOVvZvM" +
"feRbX077Geyou+FviHr+UHM+mQ9kxVEVvNTmP9pRiWLiGxkRTso4W9cwOtypYFiki9MwctgMUo1G/anXGihD4tMFsKmW+mTFlqSVU8v8yUVJBUWJBXMkbcFr5BNwb+pCw6e3IiZUbAeNH7K8F1De0PnijTVgbOkQDxZLhtx+aSkRGXPKEv3JPnSystPU8+zoIeuBewKtQFef0FgWFxsjNNhp4o1JeNEB3xQNA89XIlX9rQyW5KtuKG4Ib0h3ZZuK05Ka8zp7MydNCm3szOn8TS1pDG7swP53I7O7IaDvzN2r2tXqst57TJOuEQeetmP/huDHpvEz4S7IT/L1V0T73NOmROIL5GZKVUyK61SxiZondkT74vpu5oQvjoQbO5co/RJJQ7Y3TFKrMuT2c6cnOx1dqumSVuxXmQp1GEGKXrskOSKSepNBG+u1DhXL2bHzLSUzN50mVqaMVxLy0pNWyayRKwzK3ZZskyIl87sBGevp2CYlpObmzObMzm53ZXeCnd5fpktNyd3iknlif8LtSdQ7bHOhNjZWcnqSCuP+//IeuCUcMUxG7hmZ3bOhv8nVc+dOzdwwtKlPT3HHDNzZkfH2LHx8U6nrguxdPnS5cuO7VnSs2TxomO6j+leuGD+vJlHzzx67pzZs6ZPndLVMaljUufEsRPGThg/rn30UaNGtjQ11NfVVleUFXrc+bnZ6anxrnhXclJigjPOieEA6wEDgvasJyVV6RllrlRPvS/VZ8b0ejoTusddVF/n99WmpaZ5ML5qU1NslHUXN6T5ausB4vW4U1OQ9Xs8qThYcamAL/OpJHR0od5nKzBRV1RcvubytZe9sfiKg593tLePG9fe3nHwtON6jy+uu2VhUXp+THJpfbDy6Dlzjt4/oaSi/eCWCq2hMvhwZ23DuIOPVSxWaxdXyoPHA4NasviycvotXlx+WfDHmTMmT54xs+t3smVGcUaqf3GqO2bk22/ee/LJJ6+bVXrp5fg1LvYFf7h8cUXF4ssr8BMqdI96RyzWe" +
"jBDxIqWQCM9cpKdClqqw2pRYoKyKNqZMZuOUiy0auRzTHY4hHDEOqBRUM6eptvTy1zQHdAgnnobGKx0J48Zk+wundWilbtd7e2p7hz8SIuFfqfeFdOi60OFnVRVB1U6gSuQYjYdhVzI1cvB6tOotnSbL7UBvRZVX6I7pa092WPWJ0NPoL4x2gqUSwkk2W2k/HR+SeakVEloiGpgsI0ZMwZUujXv2LHJ7qPnFhCt16Ft1jCt8aI8MDzOKTVqnXg7Wic2RlMWwbvv5WwieSF5YZOTUkgRa0ZT+Iw2Kc5sb3d5urs9c9rHJNfWaj3cINwuVTVUzxVqr1ih0dtH8aIuUBNjVVxPXKxFs8gOpwPVTqDmp36Qkap0XY/X49M14oLbAi1hNEoGULtqHuzxutrbUrzqIJ/GZXvoSN/Qmhw6V1aps+HvVgRKHVIJuw2NrzqdEi2DXjgXRCnMszgspAWPyUlJSZo9s8xl8yV5kriS7U3XFB3flN3bq471H5ylft2Y3btcCBN3dRg3EDLBqtNhV7wJDbjRsTwbEhsKLZaUzLjRYEBOTfa3nKbVxdc05S6XD2Q3HvwvdZc/dznPhfOlVdwLK8smFwkr8o77hXxUWu+rLzPmu++QroZd4xR5gZxDG0zQMzu7T8OU609N0T0ed32dz7faal3idC5pzE1yZuf24bkZeBwiM5AWRgDPuUvQrgVbrWZjDCaCm53XOp3XNuZmoziXlX4uOxQNjAE0eIGhgDAUSL8z+IbTKcsNJNyOc0I/yCfln+DlVwcq7CifmuKA7KrOjPQ0TdE6mzqXF5LnEjg9I5VTvZ5CzZZZ5ieFBY2VbivyuPXUFOgpf0O6tbjhlgmVnWU1tcmlRbWBsSNS20alB5/OOHPNjBE1k8uzqlxttTVHZRfU3FrTCmyLgPYGtmVcAZrtkybRRkjZBUvDBlUJQVjU0ECGigErEwEbRzwDLGmSBnGCCRlutDgR46q1oJyXW62oPsmXlOLzyURnyrgJDcckg+/ZU+TuYP5a1" +
"hGPAV+m+gqTS4KoCVRizEJNwBmA9YSBscwqNY0EUyWrrthYIWITYhPiUYVwFuvomWTdBgFFA/gwHiBSGZl6dmFuXGHhvn0rG9RfExJak8fkpddcf/AtNVwMqC+TdFK4Pk1apGbZAJfIqFeXFovqiao3MzY9NSUpnmu2Dag5uR+nUTR8aPLcR8nBk5n9s4Ib1hqWHn3l7AMtl58H/ysQ50qG6ZiWmqJZMEYn3ueAVZAp+NGA7KVV0ElWEKYtFJqWrHVlGxZTDn+8ThH1BuAAmPjDIUk4AiSpv4zkl8vPpV8gLj09PSs9K9Nb4NVtWWXJPNei+cJy62cNSt1Y6W2pqqksKBmeE+cudiS86Jo4vkEt9OdVNRcW5Q4rik+1uo91F/YGk+QXZhvKRG5Dr8wetA0LhEVZlcW6oR8HurRaofKj29ITYcMCBqxnhuEHgMYfKc6EI8eZemQ4jwid0eDxaHBvutvVjBa32bKHavH+4nto228OS/EhPXBwF0vz+ODja0lnZwXPF99CN7jh7wlo7nKxMXQNNLeUj4auIc0NmKbg+TKFYUpMmE39YYTGb40f1CrNPQ/uQB48GZrLZUe0grbb7TH2mExS0C6bMfvaIDtffZPwTRbizAYtZpl7+e2IT7CgMF7IiYE3VZQECiN4aSbcGK3IgDzV7nI1kyILY+/fTuF63GbrRGo7eGlfo1BNtPn6Z1hANMJPeFhImDmdhr3vFUqDmllnOo+9NvoCpW6Ry+1S160LhdWabEWHFxKYZgytoeHmBlLT0+PieIRlxKXFpRYmed0OjDOXny1amgTrfWn++jqaKWw+Gmcl2WMKPTUjWs4uuHj4sLSc5owp8dDz/pG1xaX1G3uG5WTkttSdaowyYfKSaPLiFRv78VIe5kVHp+q2CK32MK0OabNZe8I8VUR4OhL4uYF05g3CnJ+Xkxnmzjk0d/376lA+N4ZlegC3B1/r6zy2Q+aE/FqS9Am3tLG8psgHQyea8nqiKdMEcwnD2IeCCX0X8surCIbWz" +
"whGHDcojGKYr02YKYfWFXyuHz0Z4bpUf5hoegaBCb0WfK4fPRli0iEw2wETTU9GmJ7outBvSfKlCD2p8kHal0Y0y6uiYC5hGPtQMGxLXSX3RuhJFU/IB02YB6NgihjmaxPmzoEwBj1qTISe9HBdagA9DGMfCsagRy2N0JMepkcNoIdhvjZh7hwIY9KzOkJPiXxsCHpWR+gZBMak59IIPSVi26D0FDDM1ybMXYfSE/yDGikvEcWQ0ltDz2PmYDuVrOvQ81EwN5owa0L3AWZKBOa+PllVlwPGLaaasvrtYPKsChlm2uAw9KVWkx5jx1tHYFx6LGZAssiEgnJeZoftb9GtlmVCh0YQum0ZW8LksEh6i1pOzs/Pycn35HvcBTl5OXmZBcMLCpyY7NLSfD6b4dA3NFQqj6cBeiIt3abrqYVwAtPhul627ITX9dcuOHvx+lfj83xln9n3toyvSvx+6TS5Yd+swNuTGnuXzz3bv7C8KrXRM73jxKpl41bU1Pyhri5MN7XRcFFBz9UCMwq9mXaLVQPlFqFZLdqyGCLXbtOXCXusdEi7Yxk9KJRqsRMTdXgSq6wsLRWisqayprqqtKK0orwMGIvBw/DaODBh2tBFxUR2yhAcMacmS+pG5/jSxtKUJUunDcrc8tTaqnF/+XRmK8zwjOb6KvA0fQCf1S2ly6qqbqypEdyHwef69XNGuA+j9ATBRPfzITDk40T6eXigCH0szD6G1j+kR6kLLdDtv9CFQ3bdYL0Wrt/or6pA+fCS3FjafNUJN9wiSLiIDm2xSQL5PtwFNbotp8wLKmqPpB/kXrSt8zDtv6h06IY3/VFub6+oD9TmJVktFmmVnbokz0RYl8EtkqqHLBU4JlJ4CnIy01MSYp02mL6w8dLKZEQkTJqJZNN7hWvYUFQEeuXc5k6f6xiXj+id0NqWs6Dl+Fmjnc62zsX+p1xto/5tl5cUTh/X0FCSm10/vqkwLzs3K+/40b7GiM/M/Z1OK99xGq1gdArevSxAIBw203ECc" +
"LpI9RcUWslTDVce7kOTqMvCVX5BNH1oEtJo1AciLqTazbmYZEhNhJxNZjlzyRYVa+gTFRutuxhmylAwRhszzFQTJnFQmEKGmTY4DBhuBUyi/Byea0YgNSFekR0ZMU8LvOQ8SzZNCooLamGaFBc3yLeaZnYF39Jlla+58oViaTtqxKiJjeXe4fU7i0UY50TMcdnCE8hPtema6LDw+lgP2b4QTEFf4Mjy+pqpUb1k8ACty+xwnXuc2rbYJxf4ClrSa09OrixJdlqtroqRSckjq/br3zcW5qQuvbU1PfjnurLcJKfb63U3cvsyP+pm8Dzb5DkWXWnw3GO2L9OnfguYOSZMlZpnwsyLgknUWiJ4SmS86jb0geqOxqM1RPCUyHoDjzLxkJyFHDIN9kesGBbIJtW5kZfg6PVlstOlmOoqcNMqiMXgGG0Me0+mjV4/IVvP7V7lH3F6oOGEpTI7+LEI47sI+DJFZaAsCaYNLTRIsRFulIA1SpvXo0Q3U2QU1fpc5KqmEWIe9+FBhabmKk9Y5fdVxDidMXn+GKPaZ6jCTSVVGOautOmnBhqEoUdRd678CvzON9pNfHWofcP0EcyCX4Q5nm2O+abN8dWhdgDhYZgFvwjTy7bCfNNWGArPpRE8g8IEBfdRGM9wcUNQGDQHRRTMRQyzYHAYXgNzWC6X9IywJdAYD1WXQtshOx02Bf23kRbuN8LPt+iWpXbasxlWgb5aT4rP5fd6XJj080hXQwgwD6bo5kCI9BX0db3Zh/5wdxqw2nW1lZX1/hljqgPxSWMqC+ti20/trDp9Y8OKReWt3fX++uraZj+mxLJbfjd2iic/35PqSu06te2ysx5+aMXa+u5RMm9TcXXN8FtuMfmwxoGPgBgdGJWVooQtn3bKdsY46L8KbKQvMW0UdhgG+lIn3E2h9Vgtin32Ua0tTf56YsYLsSuI7cdOlOjp/yFrFX5/TXma05lWGYhLGlX2y0wSG+WVudkF+P0yq8Y8oK0Er42iMVBfNdxDDxQ6LfSCv" +
"9gY47RqmKz0Hofdpul6sk5DqlH462qYPQ8CcegPs1d7SGZQhiPTxq+bGnw1IDnMX0xMP/58o+OSx1R2Op2djQZLlJYdwp67IN9Ds4sxp/0bvJSL2kCVJ1dFseKwa5qU1h6brtgbBSPlorTO62EunLZhQ3Pxy5RHCd0Q9PbJXJhSw9/EWOHxvcwY39oZh9r5JIcMs3woGKP/GKbX9IPeGQxG/ZthVgwOQ8/CQc878jJRJKrFuEA7BF1LkNKmYF7pmm7VNggNE5mNVqp0q9B7YXMpi8JAttnC5lZxcXF1cVURxnJ9vddhy4Xop7Nnb8yf7qEk3RL2+AvU5e7Mnj9M882sXjUv+P2cvP7iLbdMmtg0+vyZMnFtqfX+G+vmNc9ZNTy167R+oj2786ijpl94nTB5so4FTz4xUkwKdMQo6KGcdEWr8sJmt9pt1g2wyOxWSVzZbcLea6onp0NFmWh1dXUj60YwX77aBm8MD+oIZ/3stP+UzfzE2jIa1g0jM+pnHgnHR5Unx+dmp2RXDc24Ypk4GXyXiXrqS3eSHdZHrtQU+BY2ZRP0Fju9J2rrddodOoZInwYrL8fIqC+vq61G8eEgvqHIFQMLOjw8foHvyOjIj3A6rdk/LHvxrdNNVlOdzlRidVbw8+Nyk9sr853OfLWsa2Lz6PNmyUQaNb93l9kf6sfo6IKC7Fzi7YLrw7zFgLc8eAGtgZY0u7LwxiOrBW7ABt6GKa29dt1GK1nhHszPFyJ/eH5JkRflcn3+Ihctqh3Ckn4EbNTMG3l44pvn1vYj2xzv76g2jMHjDbvAkgPKHfcr2FyWqPE+lmHWDAVj9C3DrDVgVNegMDEMs25wGIwNst3+qC6A3ZtJz2wSpLLw/i2LEpZeoWlJk3Sroi0wxthOTEzMTMwoSkoqLKBVaNE3rIt9sNf6ZFvmXTDGGL/L9fl+OaVrYlPbeRBgmb2eB2yjmhpM7In0J9GhJYOOHGie5kBDjl1Jnd6jJ1EVcJmgaazWpEn0JDhqSObm5hblF" +
"oIcT21DAakaMYRUDk5d/2E3GKER8RtAL+vSXaCX5O+oQKsTpOpp/ei1SyLY0Z/gfhKY58aw8tAChytpyOFEEpgkIpTfPOmCsdGjqJUon+V0rpDnhnWEuiCYeFY/4rNz1dSD95qKwaB9PWh3iVya7WW/PtcldbotutNTUoRIyU3Jycqgl6/dfo8ddnofyfrQZNIoGYI4jI1+ZJl+xB/5ecLJpj+y95CxEZYTtzhlKJhI37jFqSbMk4PCrGeY0waHMdcZ4jApeOjpvg2Oil3SP3mCk9wZeaZDSw7WxXA0ws/HCgoLvB4vLTb4Bz7OpZWGAWsOl1XP9E8YkxJobprY5n9Ef6ZywvC5y5/Sn/z1+nEjFixqmFVTWp4/sbF5YkPphIqO9sbuJaNXZhhzGmg7Wq4ThaJCjAqMKPV6bLy6kALPcmJkiYFe3tcs9IWe8INXKYYXD8vJzkyKj3WIQllorjNQL0aLHtk4/QgtbrDA4YeKrJcb6tozRwdSA2PnOJ1z2ruqUye3zV3+nP7shhMmTr7eLccVez+6dt5Iua66KrN6dF1ibG62MyOtZlFT55jGnmXt6xv+OWJK9tsdo5kP7odu8OGmfWgBX3GsppiPTOJDmGzoluh5WAiMHrfIr69t9NpYf4O6gsF5MFW416T9tAV517mlZSADNWtmVzmdNfJ6g/TGZNc7I/pRn5eZnbvXJNrYK7BFniQyaP0rLBcqIhMW2ScNbhIGK0b4IMJgDJruKpIA16gRjZ1t/r/Ygkc5ndcvmd4wu6asLG9iY9PERliK5lpInBoPvn9tyus45TB9dUfUesnRqgMwG0yYahVjynRMlNx3M8x/mTD+wWDkFjUOMGeZMC5lN+uyR9UVp56I0JMepkcNpOepCD3pYXrUQHqeitCTHqZHDaTn8Qg96WF6VBQ90AvL1Szhlg5zrrtHpZt8pUfBbEM/uaUzAhNnwsRF0dPCMDEm71mDwchXua5YEyZ50LqWq60RetLD9KiB9GyO0JMepkcNpGdzhJ70M" +
"D1qID1bI/Skh+mJrivkUNtw1S3jjOctapjcZdAsd/XBaGMZJn4oGJOe9gg9qUO2T1eEntSB7SP4q6XaSHUzrI7igJf3t1qjH932bdzJFOmuVt6Dwqtu9YXp1nTTkTAsa1+Bur+58ju5NCn4t49oRW7jrJkNp8qTGsu9VVU/1FxRf+YnTe2jJh4z8ZKNJ5l7dDDVWVB3IfmHBXnsH9L2wngr7TCjb9ZB+fSZwihTKDzuilpeP6oN11rbR88wxTqHVI5B2p+ZAl+YrF1LFvpgJhbVjx0lp3cENzItEfI+WDorMzc7uWmU1VE/Luy/3gz6CqhtMtK4dWyDrB0WiLwGd0V47XAoWoytSpOaK7+VS5KCL76/dKEvr4+WT5xOmYWmqq48lJaofVXyHdCTSvuRw301YH9VqnC10uP7Q3upf/UfUg8NqPSTxjGjJlJl5pgguVDiuvA595US14uw/N3M5zeEz5k2JX5jzItUXsvCxJcrygIlvHtaU7ZBZMtiseRasjMzUpMtNAemW+uTh5QwzR4XfOjTwaRMyyq6uOzgH4eQNWXQz/QMJyui0MPSBvSuZJA1qLyBsOGWYshbOskbESaOROaYxuBHvyx5BrmrfkkAjTn5Zqa5kPZP5mSZregYKIOgtNDiKRiWnupKsh7aiEOI4iajLYcWx0ibDimUyuh3pjFTeAMFff0cLZegL9OSnpqsDdnBhxLUJ6ADyYiIabScuklOeX13p3zPXN99r0+/s+y6SXYHhzHl+UPA3GDCXKY2mXPtpih9+g7D/GZwGGOtUl0OezZVFAbcghbYhYQbbJGa1JbSziNljtIUn4s0RtqAZYkkXqzYEVlyaD9lUtX3kXUFdeLBK6ed0W7Uo1nZbm4I1OVICz0kFYq2e6A2HZablWubBPPH0gP2ky1UL6y1Wh9qJp/x0JoPWYvvI6O2LN7pjBtWH0MLItH0BLWiytzsNFda16ltYR1VDrqyyCZL4t3JvPvROsB0BEBGnfko4NDqIwbjuPqGuiI4WGkVr" +
"c7EQFmO05nTWF0ifwzG1pTn5Hjz870sCMae0wdRbzzvoaQaqSra2Gkq6XgR63PR/BVu5r4aZrT14XW0d2XnmvP05dy+txvr63KRvM+Umfui5mmjD+4YHMYcH8XqC6HDe4FdncFPSOglEoupc1gfsm+v9dCDPigem83msRV4a30ZSdRR0uahF0vU4K1k7D+Tqju/u2Dy5N8PaC8NaHPU+1u2JN5++8EJVSVyevDBcMvFxSfER8awzjSmkp7pT50mN0Y/fwRtqTYXKCPh7U9ZuEWlbtByU7hl+9d/b1uXoTu4jS+UZ8N+L6Y5guo6V8DtEFYl+m29y8zMLM4sair0udzGakcDGe9JniRiHQ6HsShcX5/mryfHl26SYMuy1Iz6MZMb1i58YeK0ebNmzvks0FhVWuVrbZa6L2VCIDhGPjF7+eT2qZ3Tp08Yc6evdlKFv7Eq4G80aQuBNo+oFSMCTbQ9EJKshDpXaLqk8dbrsCmLRfTYQS0pOa9XCG+tt6Z8OAoV1PlcRV5aUsg3F6vryXsbSLbZl0X96VYPNMYlTWqYOSeK/BnpTmf6S2H63V6PByQX9uNiWW52YzQPrNOOgt9M3//EnJedRvvyIusiNruVV0QwlZhjsqKsuBDAbp+3tr7QQc4S0eU3SB3w8KBviIrw1sf61vIFM2cdU1KS6XRmljfEuSbVriiOb6ym9RF51umBEW1NpzcWFVZ3H7dkoacWWsNb5C0aX+TOzpWJEzob284Oy+KroDkdVlVVoDxeauaaCDzoXquOmTnK8soflpVBj8N9XvjPmVHkpvwihQunD0nX3EXRBIX1gLweY/wJ08folnPNMT43Wg8wzJNDwZh7sb6K7DVyid2D7sVSDPO1CbPp0L1YrJdiIvSky9lGXWoAPQzz5FAw/zt71cLzHWBS+Y09jI+NYb2h8UNlVmpSm/o/mPRkXvBTmvTMff3U1kRTZaAsxphfrJp1I70NuNF8CGLu6OclM5/Lf8iWk6GeCKjL+01zfev+x0amuCiib" +
"mcd1hVyyJfkejCXL4oC9J85oD2txneKxWKyO8wFH09hobvATZREnqw3GNrbGGBEn7FqlypfOurErrW2ExesUSctmVs1rW5GT/DlkZPXjj7K3950xuiG5XOmzhoxv25tzwXT6xZOnzFltEGHGgk6sgxvCi1hs1psvfSNAyUX63ZF+6M0aboJsBVzAJpJJLkd4U1Rg9LliyJMjXQeZ7toTX/SeoOLTdoac7NPOKePuhV1JnUkk9xO6g3021MsS2XwRgf6vcwDwzz9SzDA80gEz/Ah8TwSwXMIDH1/hvH8EXNeDmaf+kAtpmBpMZ5dWARJEqZBfYXZfgJzkbmSxP1YCAsqF9NfcYMv3ZZ82P5M7SpYiCnx2KG7VZ18R9ILLyQd/GHQ7jXoBU9Eb4Eop5VuptdqPGsR9DDfrlvtK2j7l6YW2xxK1/l1WRoENltpSSEsCls+9XehywkhDNN+RP0eZuCdX+x+k4dg71Bi0Nd/eZH+cw3Zf3mR/hsCBnjSI3hSh8STHsFzCAw97wjdrv4q92Laq6Y1+GoptFx+4yo+TqmO7CwlJ9DHuKhZe6O2sglRNhxlPBOLrDApBS+/23zmhiTDqeGZ0lbsbygyGjXZXJSnJ1xyyrTqxronaxqHv1n8VsOcietGjnTNODlt6szW9tpZ9Y1jJzQ3dQRa5SUVU2t8s3LLvZWlu0s+aWkb0WHRfdU1WRVZS5p906uDW6dWN48d21w7Odu0p76T/5A/iQSRLeofSaa94LJj4n21U+YEYsz9PTT8swOJA/f8zH2otEyxbdxQ3DBMsX6ELHjDy89gbE7b7IqiJYu8tUc3TTtWjmvumtQyvmXE1LfXz64cvnjd1IraqZXzO5rndU5cVF0XGFNn+tyqRf4LnqFH+ALVMTDsaCunZiFtrVs1fZmgj6tZVW+Uxva4kwq9tTU2fl49YMMCt6Ov3pdk4c0ALJpbystdTqervs2ZOqt2yuzqmf5f+YNbTxw16Vcjx62BRvLXZ+fMnt54TN344F9HVxXe0" +
"9Fm2LyhRHWKvBhjv4bscvrOpGbhB86wkZY6nDZN00SPVSlTY1ZVosOLRZEr1eP2eN0xRB751Lpu46X5esPUiNgfuo3araFV+Yq401NBsDrF6R9dkTJu2MrTVjidKxZOn3xMx7mzjp7VlN00NbCztbFl1OqzQHFS2fCLTlwVn53768Xr5ncFjm2ScbNnr/P503eOmjrp13MnRtbrn4Hclog6esOtTkprHq9+p6Yofj+TFux7eWvjQtPpKnTXtRQV8hOR8DAfSmzDlpM/Smavra+ZXV9DcvuYr7HqtZLtLbM6Txg9Ijm5bfQ5TuezV4/taGqeEGidMqcKohkW3Ira90p2jxoV6LDabXp27rA+gWU7BfKqrsP4fCk8PnlPG61Pdvf57qpF/QowfzdhOtVYE2ZseL8z4dHoP7/liTKxKOBwp7iSed+wIfzDzCcCy/Twm2/wi63WsF+cHcgOA9DeVatlaR+c1jWXXt8bXpxL/6Ar3pOUSoYmv/TT4LN5dDLhbR419LC5oyo972zbse6m12z/febgQ0j1lLgzGyvKa9OLKoInDzmeNG6LZvUpCCGpnRToyJZ2h+yMEK+cUrMpbRl9ssJucyyjPVFWO+0NsYTf+oAgV5SWFHnzcjPTUpJMlmIGYSl58LGX2jfynqrKGGZwpr/2Lg/ClLqjHGmz67tmVU2vfyT4t1NGTTpz5ASVX+LOaKw0uJvSlJPd5M/NnTmt6Zi679uqiu7rHB3ef0N9ODUiC2UyRs03+3l+lCzMVo8B5j0TZrZ8wbSPX4jYvoYOVIQnLD/QQYrkh55bmvpeia3C0P+J6mt5Gc+qzYGGEiksCfH05jCmAPoIu0XBmNPINV1K7y/2vZRZ6KFvgqSXeN307CtaFxRjao3SA/20QKuqV1+bWmDh2tjfpdyzcErn0U1n9Mb91tACI/yNo2YtrWzMySE1MG/uKF/744tOmtfVfMzk0itICeweNXPSbe6RY8rCeyANPabouzLmOtYzzN8r4pD2MNbc1Wy5RCTRX" +
"thETABJ5jMzNtojezVrfUm0CbRB+tKjH4QXa3OkaA3EVZckO53JTRNcjV9UyIY3Kktzs5taHwjuMtepQ0I+Bxs9j3wC2ndoaCZS9la12EJzT49mmCaueo/LtM/94WrQTJ6kqLUN3veVJGNnjyTHcvpsf+r8kWm0sJE2bX6DXpudu+iYNjm/vIbWMxbMbQ9uMvZHvs2+S7FcHPqM8uTDGO9bBN+OlpUj0D+zlSUic6lyrnzFlLlXonyyB/k/CHwZ3luiZYj/xvl+7pPXQq39fK20sK8V9V7HdsBE+1ppg72n9P+yHjUNMF8a40z8t1xg8rwgysfMkH8BzH5zLJ4vnhN2wNz3HKvlyPs1t0boKROfyXtMPPdE+bwJDPO1CXPXQBiTnroIPcPFq0PQ85sIPcPlGeJ5g57nw/Rw+6jzDHogGdw+jO8zeb9IjrQBG4j3I8PtFZCZXOZrs8yU0Mlc5s5ImZOjy5g+MfVTIfkVwgqDnPbrSfoIlKKtAELXxDKhWyz6bKHrloW0x22yD0OhwFPAdk+Rx8OSn86DwVQkAx1W9auyiqe7p56ub6wbfflR7ePPmNDFHuvEkYsaE4v/NHmRv913avuI1hUjDae1YlKV6T+TzOTS6kqU07yY9irIhVZjWEqRk5WekpQY57RqIlfm0u6EQh6dg5BSX+fxNGhwnNekHHXaVJOIpWtHPmuDMeMqapxVbRAwbkRrQ+Q5FslpvzVi2jP+y2vEh25hNYyUOxr9tVXQRylVgbiktopTnM5TGquG/+HWssqcnIKCfHfkuwIk9/H0TQJz33vk+QRUEGk7b99LHVSZVDPajnM6jyuv93d1ZedWlv7hlshzS22EXC4SSYf2zb5KW2gu4Ar6D7UJhUkptNhRCy3GEyubBAW2gofnr9pte65weNsIVWk/+EbHUQvP9BdPLyytaeT+saoHRaNw0Tu65stNhDXqEZ9LJBcUuvlxI30CIYKX1GVDgXrQGXSlZowaIWfZg3+aMadDUTcMa00vLm04qnXCc" +
"eYzkxHqecj0N+bY9cnXzPH0WmQ8gQ55I2C+NWFolZvH0x3meOLvFryqLgCNCeFzNZ7PE9lGfYHvP87fBRgdGAV/BOLVaZMYBjD6N9A6pLRS09FOJ7SfpdswyMyPA2RmpLmSE4wPE9gP+TCB+WYCWaxJ3gwbfR6gyPvii903ldGy8z/U+fGJI5PG5GXU3hCcEF7Z55dumS7QSXQVis7ABAs9r8qXyqY6HeBUSRvRZpfSFqaNdpJYF9GKNEwopq4wttDrHpad2Uehc0gKB8jsIdTWFaU5nekVrY6kQNkgdMfWlOdm03q9/AL66AC1qTZMuGWSqS/XqlLzGVSpqVMPEH8MkzwUjKgLns9956a+43fGf3/Ie+WA4f50U38ODkP/fRv0VEboqRCrjbpUHz17mJ7KCD2DwBiypC2O0DNcJqpLTJov6YMBnsUReg6BgSqDftOL+PlbLX8PszswvzRHWWyyMyFWOejfGqFzacut7cxEGS/iZHzcshipOSUsvGNJByZNoj3Ulh761gJkMRDw+QJtgbbRR/lG+UaNaG5qqK3xul3eIp+rLjHJloexkzSIci5kT6pYj3phujY9bMsUJKX0vVwjUz316mhe8owsOKrN09tWwgWxpoyb4J+XbFGaZWVZvV/GjV7fkWXL7V4VnH1AXcCPAiMLkme2d1mt5ldArFYoq+DHM8Kv6oxuaDBtGatLo/0p9IUMjyi2nMZzWi797yy0p1ft1dIxp2nm+1j3W+SjGm8UMZ6fu5WVv5E5MTDeJR3OWKk7VHj5yWLdSK/QOx0W5zLhEJp0aL0xdlqF6luub6ivrQaCCpcPLn6dzxVrG0b7FMLPjsIPtAb4+uYkY44kb8T/0B7x+6e3VRkPuqbUjXamzq7rml0103+mv7x8Sqnf7y8sP6idRAsB4xswkk4KXtzWVZiX723w5+TOIgdkQvCZkU01xTI5+HV1RV114T0TR0f0O70Pnkz7LBT9NwN+BNa39oM7iS5jKdsbfikhahvMH0kDBS/gjQnrG" +
"qtKNt3a1mVsRZDme9s+Y1dEIs99Fvp+Lj0C7D/55Ypsl6/WeFbrHTD7Fev9Kry1saGGZkBXT3ZyW0XwCq55BddcVpmbPbqgwNwJYezLWC5vw9RaQs+ESpzKmH7pP1VJjAy2BuifrWkLreHJrCCPZuIS2neTGbXvpqFSRVYLqQGIyDpzZ2VaOm+9WVOS0TnNXT2jfnb3mpXBk1VG+jmeFrd33lymx5ZRNbKidnrNyqWeOUd31xQMu9ndVFDsq8gx6dwGv8wDgQGdVVJazG29Fp2+ZUAfN9AtQu+19l/icNXVFhUaRlT/NY4+DdzXcNHLG6q8rmamr256la/uRafznMAIl6s9EPyYm3LadYExLY0T2wLTZ9bUzqiumOqrn5aXm01rGsxI/tTqprHjG2qnZkfWZlp4T3wZ7eUuxCwX3pJMT+YHrsvk5+eX5Ze6DcKzD1mcGWjuJOvGHnki/Zpoms8OjCSaz3M6n8sDrQHQfCi92bkynynNJZqF6HvmdptIQ2tDKj20b5KXFfjrETT59Yl+ThbAUupLaNnTO0ACzDWjaCmQesYkCMBMCMDalfudzp2PneNpdnuPmduIzh9RGe787NyGtr7eDz8b3ibXi2FiOO1wEJpu1TUrWdG6JnX+XAdZraLvtW9oskJ3PgrkuPyFHjtsRou//5L2AD2toh5sOOf71Mm0ol0/o7vtlM4TWBWvrWztWjf6KP+YZ2pnN4TX5KeeYT4EkuV1C6fN5HVtg15tLOj1Ql5bAo3Cald2K7/XYbdKe69D8pssi2288SnyHKairKSInri6fG5PoYfW5QfSbPR/vDoSwmlRUR9zwoShqV+Rmz3x+JGH0i8jtlwc6Tz6ypuESWZ8wMzUR3EizuMqsPKW82jj64awxXXBwXvDhhYQGTjZfsgkqUrhL+OxlkuaRB/Fi2x34M2FmfyCno334A9lON1wqLUUrjNiJFHN/fkpCRTSI0ghNbFBhNnqPmK25BcD7MdovmoDVSm8qs3jGsasUQOzt+h/zl6k7" +
"igbEL7MP4CyDNaNJoru15e00/Jl5GV/+mQafSGVv45KGwHv10U7fYmxbNaoUVQ29FdRxrePsKzNLDvKWNs5G+mvRANAirn84N9oi0ZAHxj81U8NDYbvd3ZoK8qL/6C8zyxv6Cqj/gf4yzmNjKHCIs335K30SpU2O/x9M01Ntlgws9ksNt3KKPUoktIRveNu/umB5ubTGTlwh7pM2myigXGXM250J9w7IJ3NTh5Vow2F2md+NOJXPx09tqEByMHzNdCwayB+qcLHWEuSkxRtA0pNcQ35KboCL3mkhLFo4Bem+MNJbZFPSiV4yxK3Jk9s9+dXNRcW5w4rTEwvWOYuonqDX9ObS1DYZr38FS/e9XgE9eqH1Dug4ti83DSqeXOk5riEGK4ada8Az1eLtXCNyrjufNIpEwYTNnKewn1dpOu24lZFtRbn5GTn5EEr3nxzZ0xBqat2eFwh+uhSlFgnPkKpdFHKmPPCExV/J3iuMHZ9KfDi9fInLxhxZFtIUX0aL8wb+nNdW1VVG0VvZX5+RUV+fuUtbcYlPquohA8emo8a/iBexNQS4BrrY2GNyE5N9pm+uoKkWObS0cIWr0XSlodhIjfJ44Kpa4dqJzr8YXMxav+WqdXp46aziotaWmxxiQ5HYpytpaWwpCS7KLuwpKZjXPtI+Jb6yPZxHTUlV1xhjMUz0cb0JWZN6A9qUtaWNaB/zvom8ZsFy91ojdBJuH+RzBTxZO0x5blJtLZt9kNUt2ekeVW4C4oia9fkzBaFTQ00VsmwkRXlLVX+WxLOyvLk5g/35hUU9viyygItZUUj0tvdDePLY2KLc3JLS3NM+n4N+px99KWb9GUu9ywHfaeCvvPoOwH0P70etllhDxk8ENwZ5rfAdi93L7/N4Kce8JdL+s8N3rBEZ6enOexWMYH/wwM9oh3wYNOdP7wg3xIeSfQ9K5pUG/ozWe+PYvKClMq69Ixj8/NGVFY0Vzbc4lrpBauFYPUKb3r2gtKkuj6GF+dEMQx+gj+L88z/KxfhJ" +
"30AP8Q7fdIM7fMv8ONiXfaf6FJX0QNPPsnt+y/oKxfX95+Vd5rlp8EnvF9cjLHkOdz41NPLiqIWN6Zk2LIKc+KKvPv2HR1er4jWz4/y98yW9vuaWcGAL7NZaUudZTb52bKbdshOyQ64B36W7VCguYE4/Myvlel6Vlnx4F8rmzfg+2Tpk8fUt9aUhD9LNsJn0EvmwsniPaa3nVuxiXf3dVqlsZlJaBDQXt2gQ++jIy4OtgPIcCWhbKzXY4NBTE1cnBpFTx85MzNbCzw1LS2n3DI8Nx3UTHlvQ3kNkfPeX0xyZOhy1LZGvCl0kRxIiOzCFCclSTR/g7HXco2xt3LLli2Jd9yBMlcDYCV0U5KoZOp5X/vg+6+S4LR62GklOiOKJ/Javd9fVFDodRf5/c2BrklVxcVVk7oCNH9dGNojlopvMH+VHencpWeWDT5vdfXNW4Vl9CXE+l+at8qOdM5Cff+T+eo/1EX/p7o3AY+ruBJGq6r31tqbWlJrub2otbVard619yK1bEmWZcmL5FVtS7ZkbMnIK7uDWc2+mSVMYAiEJSRpG2MIMIQQwpCEySQwkAxJmISEJWSSMAzJ5Ce49U7VrdtqyTKQzP997z3Lt6vq3lOnTp1z6tReNTcn1gn4ImTD5Wwsis4xfyKOj34izTcwGGLKwFRKMCQLBtL9Mr4AObGA6LwCXRn7GCo8jnCKTSsAvscW4MNzGXxWCP9VxPdXCSbO0jRmYGxg/RfDbJr7C8CoUTW2U4rEVHEE6Y4jkTI2mRFh4PPp/lMGpwN+c/h4cY50XgHaif8Nv8nm4D1imww6kQQKsAyK0Chtk1GTLEOrCgsRKqwsrCguotPPTtpwQnz7qi2zVIaWG6fUi/ThxEgwOOLxsF9rpKYmYhUitbURAb8ZXOt2rw2Kv+lvV3cI1vbq6nar0FEt9gPossGvkUuQhpYpaB6yEgV9LzOm47XO6hAkrbPrDhgr/IIphGdvmjTuuG1/+kM6DMnj4zshvoXOZRIsqn6OVk43ssjow" +
"D9bvCIOzFtQaY2JLjHX040WdJ2vk66qEJsbkBLL47tNubIQpObWNtmqw4ag0dlVi5cXF7lbaaLbIP1vVNf6HO7p3JlQTczJaSAHgIYGujq71MhGbBpcdltujoxuR2KHRMvoJljW36gx2euq6FZHJO5izk7cxg/OWII6fIeqoL7EETYCRdGaLldBm7NyEamkqF1fYiz1OTy7ROICxeVLES7RbAeaa9GGSJFdDTTXWkwaqA2K2L5dB0Z8ZRK9hkkmZ1W3uEIJOmh8WUYRn1uZyoCwD6NPmBxVdpZF/eKjmkU7szCPRZBJ8wdmz5Cvpb0w6An3lDaUOldX6ybGWjdbWAYtOkVDI97bYW0cdFc0FXd5bS12k9HrtpaW+NN/FjO3dby8KVfs64OsyXaoG8Ae0lWoTmh4FIAQSH8xrbtkvfTcs8vl7ORTLPb0HYY6Rz3bfrpgpUj1metFqClWMd/Azgu89oPXdtb2uJxtel27PbEKvzV+7u4pr8cd9GnLWlwt/+fpC121yS9esyww7C41F1lG+ib+cXzy0VWh5mDjsq6qCmc8JK6Z/Rh/F2RRidZETAbQ4coKI/RmlBgUpp+WieWiKNj9jvSybb5xQkdACMWIl2T2cRRlyvHoKYejqoY2sBFd5hYMzQ/beouyzs4OmfGl17W7nbt8Jkdx/3r3oLsyKBgbm7xdHa2DQxZc9cxMSfc9YbVipLdprT9f5zL2+kOtLqdY/lrY+p/LoI41RfTQ1CNYIRVhCyvCslCR3hzUg9Bl9t9+wzU4bS45Z2XDV+JNeAuW2Q6l/3jVtbhwv+2j9M0ivrmP2XqiXGR7gqOD3NcNjjzOasgBy2O0xh19jCFHBpWTVCsJZM/gw9ekXCtnMtiF9F+th3DhVdemP9hn/QhPi3qBHwc+0/b2yoi2FNoR+RjJSb+k6dBlJHJEy6nI3xXiKATVdJAFVNRTGRD2YfSJKruhSmCFOWtQiOm1tIXdG8LdwyvXNA1712wMtcesYWtLaK0XmweXh7Y0X31J+" +
"mrsf2RndXet1yfQOYADYKc/BDtNcKCChiX7SHAQ8TCzdwSHpDCzPQSHM+tpQizcbJTCe6AsENyipWHgL36QfW99i4W5/AhuQ/w74z/B7eJ6UM4zgjuWxL8gvrgGit8RQ++8DUZ8Gjq/zlbBQjFEdH4djOE6ymK6/FWGWUddrZIr5Uqxo85vjME6jK26zJ0xuJkexk5WpT34R6ffxT+iad+LdpIykVfLIIzr2HqGSyCFIHoHsboRnQT6NCK/9tBwN4QPivzCNDwD4QExP2wOawjCO0V+yWn4IeCHSeTXIA1vpmNuYn4dLAzft4j8YuGvwne9yK9NC2nsrPvbaVyJFtIYNy+ksevjhTR2T/89NC7mwYL44pwIegTe5rC5ecT3NZaxmRbofTjoFQ3e+R6b/RyDocisNxQVlZaYTCWlRfM4XoMiXBGxQDecGYnsTbrQ+rXQbhBDFAyZ8olJwqYRaop1ilITYCxPNJdo65R5JVaKWNRH9AjeAG0leqL/aTCe1FXT9tlpaR5chHmFrjVaGoaND8/hE6C3FlSNJsQD1B3sGjrZLKJTL3J65hFbfcXqP0WSzzbTo6oZGNtFeHa40Uh+WVlZdZmzSldXIp4JI81sQHVjh9ZUNW9ceU12ksVMfGFLR7CzpNJZqQmqiivqBNeA72vBO4HFRfAQzfIOrxAos9d6fE6nraw20tDY7/pBOmoSeW/ieUOPQN40UCN29aUaIW9F0uqKcmrjxKHeMnrSuhFlrZeQXo9GcrVarUlrrDIaFGq2GJF1SVR2ZRalk/JWudIpZ8LXm7Gwd9Odd26cHfvOPCl8fZsc/wO1wRFzPr+0ifZtc9FyxG4tAR0I0Zuaqn0m8bIm1V3TqfwUbvpGQWq6etd05bG2xo6nn+5obDtW+cUv0ulNqD/Jbwm9XZbe09SIWtE1jxNM55T7UpWQ2Wq11NqlV40ghQxNaaFeUaoUyqkcNpkstWgaPh1SLleNShFU8lWjEZvHIwhGo1zuafW0BP1Co+CucRorjRWlx" +
"XKDXF+XC5nBuBrLFzWgi+Yntnyf9o1cmN7fgK9NH++oquqw28Vfv99u9/lt+KGslza/30Y/kO2nby0nu83ODpu1o7q6w2rrcP48YLMGAlZrcD+8hbD4+4sgvKEPNUIqykN5exYPO1EfWocepnzUSHxsyJW4kydxp0CVL1NosVqjUE/pcgplGo08qcScm4HPAS+Xa0azImooV739/ZGIxNn+df1rhwYjfZHeRJen09MRDp7BZf3fwWVhUVieBSv8PRLAB7MC6R9yeeADf7NglhRSlrA0VFaKmxbIaiW7Z3Yv+mcqrxxJXr5Cif86if9GjUGlVyjyc4k2R6GdMhcU5ZkUOTnypJqOjDKptX/eWHJ5zuhCBDlUerHJyY0bBwfnJTi5d3J2etfGHRu3bx0b3DC4fu3qyMrIwLLEWaRZ/H9Bmp9Xuv97SS8InD5/sdj/l/L/VF3I0gnah7gab8OK+bHcKlm1AW/7hfvN99P3lOExEeZxqIOPQlXriFhpHcxvXRplA5LigGxWTRwU10cHOkhAXOl7h15TWWPWy2lNfJRWxaWaTFUsrkkTaSB4F5L6NW8yPeU0UbHiN9MPuSHJqdM3WcjMGXBQ9XjrDViFr8AjDekHyWUSGMMvwhHM4+FL0Q20LYWCYq2WK42M4nK6jUZ/5njraEQrDbnSS71M9sAN0Jo9fVpsA+BLcR7Dp0IJEWMBEZddyujpZAypmfkpWvEl2sJG9ACzfvGUE0/Bx1J5PEjTOX1abI/g96A9ZYd2yD2QMri0dY1Yu4R+xydpfQ1MgPYK+MV7oE6KTRWWb/YdBNV2kokxK/esRWXPyn22cMXcM/kCbfpPky+52lwmCji3xCa1tfBJ7IF0q0W6GH1XAC36DH1syOlkpk0FMI2MTiDoVEmxSimXaBX5mhRZaF/A11HO1yTjK9Cs1+uhP2nX2yrKGe3KpWj3flpmvpjVhDwjY4uCrL30Mfke6Bk9n38mkqOWQ5+6t0hPFEC8GYi35KiJnJ6MKlds0+Zq6E6kL" +
"fQgCVTB7qqhXvZajmQ7KKhCIV9HXbliixgLlAUaxZUV5WWQhkVnMOh09CdPXVlvCPjotW/s6jc7e6pDdvG2tRB4fLNfrfRVnITnxjsc9z1S6St/DPw33en47SnHKXy//1H1/Y+qHvD/wAe/8N/3A/wvSCpjrN2Vw9dMKTAjD5qBdH2zvEw+oKP/CpXqsvoQm+sy2UFv7SNPB5+G/++eOhU6dYq131LQ/30XFLw4YsqajmCH2IvXdIasJusIfjS9+t0QyvQZ5aQG9OYCCA+C3tB1bdfQ8Uz85NwgH0a9Zl6/UT5+FV8NtCofUzJ7wK/tpJfNXXL7LdW33l597DbnLcfw1bffWn3L7dW33+K89RgS+59zH0I+H+D36Iz3pRwgsHLl/DQxkkGHdIcKWuVSxkFkS30Hca2TwBRyKjK1GnoNOWposmbuN1WDLtIVgirIM52xxqtXfo1m/ZOV+IHe3mdD+3p7xTGpETQEdHUzumxsTpS3sjPT1nRUit5EVyhXl9YbAJ9J4iN7uoNXhJgcTwGe/4Can96kpyUyetHpcmin00W7MrJDKZo8JTN5KmaZqHLBo1crK+rNoibZA1ka9kuP/9r7PK+83erx3/iA57Xf9uqPhq4I3qW7mqYo0u6DNHcjM7Q1+iPLzZAqKqKnwzbaiEruthPgBluOhBGb4QNgMpXZ4axQKhWjSKFQJpFSoaSzvvSflVKjZ0NRRWwQShxwo4PKquqis7yfbSmucAmVHmudX13SUgxNhzJPZZ1fY2kxi+9rgurSFrP43uXV4E2FkG+9xZdfqAePxfcNFi7l4VKfpG8BIDkKPQmoFxdfEEgHkQmdZ2P3aMpBvw0Bq84eAMkEsTaU/ug1Xr7+B/8A34mUtK6jOquooufnKPCp9H14syn9LI5pvp74etO996Kl4Q0Kk6K6qmo33py+z4RjEOHODHymDHmhDB1BJehOKEOsLLEylLqTm15WliR9Q0QN8EqkpStsxGtxxdukFlWJKpVKq9IW0ynEUFVVCMhQm" +
"KyB2ygJJkoMXp9e/e177/ECNeuDQXGMRya7Ad8K+iCt/rObc6CM0Is1gTB6RRc9HgRYNkp7vyjJZuIXSN4xf++oOTg/91/tzLrSL2sdgAo/tSOyZvv2NZEdLc2Sr7l/LNC2rKc9MNY4ORZoX7a8DXzLE9Mb1k8n1q5NTK/fMJ1Yd173iu5Ef9fAQFd/ArwrxHppBz4X6nm6D/ez1ihUOW20PNIZOX3WQZ60YZm9TmHEGal2Rpzwvzri9PmqnX6/s9pHSiNVVZHqavEXXtNHkv8vcAl5FOWB/DUykH+QjsmyTX5metdHR7+wtq9vrdDf8ZNbu629D+/b/3Cvtfu23UvG5fMkRjbOG/h1xwrr2t7etdYVHfj4bd3Wvof273uoz9p9627WhhLjEnz10nR8XlysnvwFvoXYIK4FqR8vLtQSAvHFDBjF7AQXhEZCbXZ/fb3f3hZ6YN6L110TEZoPDA8fbBYi165bGEJnSycoEugXyS1aEPoDRe5yZdIRvQ9fC4gPDg9RxNesW7cgJPEC/YGlo3xMS/DivIycleSl4gbPRtHDixKmcd/C4+RB5ETbn3Aa9DJ6t4h4N56Jz6JiOz0oig0r2Qi9OK8EPhr4/SNT2Z/YfAB/C7XKKP8mI6tGR09ZG61Oqs1s/o4t32abIvOJylRB6DFk9AVVQqez+lv23AJnVVmVr6UU4/wa+JePcWmLD145C3LtNjxc09XuaTKEKhKRjpggDygqYx2RREXQ2ORp76rJXyJfSDafL06yfYWUQxuez1fWfn7pkzgPJb5dOL2cnS8qLLpqx8Qu+usgoYCbVNPrakxiyS0qMp/+rHwFWb6MQZavSkVALrB8hQwsXwU8X3rIl1iTQx8Z0/1idBnuOn6CLCaZmhzbdVDH6nxYf+TIU0+RPacfCpLvSjYgG8+Zt6lTCzTfIvBBpQPPyFNPHTlCHjzdGiQjIg7oAz0IVJjp2B0/JO9AVifHrPPpRkIV5LunW1lZkuBVbIcZvXR0UUNk6fVztDMDmGQMW" +
"yiEr8YV6bfSo7g8/WuxDk3JI5+jXSh775Ni1i7kbcn+z9OW/CidO9+WFOMQfEcmLGfhC6Qwo4Pg28/8zvh1GsL1n2dNjlpck1MFtSGRp1fXu93i+CU+yeqObrEnU0KPIAH0h2k0egZJ+Qq5ODoOfZrMNxk9/A3t4F9Gn9A5QawKtYWqh10nDVOAePHh7f6W1pLSzo7v+smy00/iWFEs3tVVevoLEs/OSJ+KC5PDiG9CK2fbocoIS1/6JjbOdvAvkL5OVyTQ9A2ZKk3H/kZe7OgsKWlt8e3wk0tK493xWFH6WUoItCRom+JK/AlvWxdAG9SMxiPJPFkupEG7FGgHG9+j43gK+Q42rKdWa9aJ43vqLXTcT73SaCwszM1Vq6mCGc1Gc5Gp0FBo0OvEa27F9nWW4umVYgvbSlvZi1wc2oRdmz7aRBo3fkR9+JMN8O/H6+Hfj6iP8UuDb8QRQKWLUJuE6ZKhA6BcyuL6KqvOim9Mv4TDGhxibSYJVkV3PvKJVXq9E7BvnbTLT4agbFAC9SBBZQnDIgvYTSKmptcBV/rj115jujaH78e9nzpvUpQ1gH5y48bevg0b+vAG5vRu4PMSIg5zlpVYei2lkm7DXHod5YiIuXejN+L3RzoD/sgfWQobev2dnf5AZ8TP6P0Q34d3QVtN+ZiC1mFYp4JWLL4vN/087sAT6VtVpwoe5+3LDKwa6mQ1PSBPglepzDxK1fe+J8Xqm53l++GD+GfsnsYUEu/HO3TmHXrp7yyAKZZgsu9fgy8/o2dxcRgT+it+mq95eTobhuzOwJglGLIY5soMTA3OxY9ymEcXpPVxBsaAi89Ma65jAc1FS+WLpXUwA1OPPobWvIjnSDYeslOEYfuZD/E90B9Di3vRHuhb2X5mTh+94wm0tvBT9uuWILPvLPt15/evfMC364qblZ7Tap/L7NZllzSgjBwvZ/l9msvxyiXlmA1TLMEskuPl9H5LDkPluNS5a5fT+1g4jFmCIYthLsvA1GAFfpjDPLwgrQ8zM" +
"AZcfmZa6Z8ymukYV5CdUfA0WnQuAZNRdr6Klso7o+dgBqYeUltK1pczWT/NZX2lKGtI9dNkfTnbJ83O0SSIndWbtfUtc46mY34pMzv1sdQfWh3/g1b7hzDdKd01IN4B9Orff99YhzJy5n1jr2buG4NeJz//TY7YiULQCM0++s1gdXqb6NKJM09+48mYcFH2RWNiejvmbxljt46xMwD4PWOYn++3xB1ir0r3jOF/+lQYds8Yx7PUHWKvSveMcTxng2H3jHE8S90h9qp0zxjHsySMdM8Yx7PUPWOvSveMcTxL3jMGLblqshEZ6WooFdvno1Gzm/uy7i+c30hkRAaoTjJX7dmtdCBDvDeA3uon+1pXbOj0bjxUnv4tiWzfktz+yZxKpg43xxPhwJ/cL67dvGE0LLVV5F+CdK305BaabiE0NcvY+rJM+nTQbeHpqoge2C3YvUGHg2+tXEDDGTeWnEGQvsDboNNqdaGY3jexmLZmq6Gg3GIubpy/11EeBRodfyuN9BzOz0sjOz5ZNtgVHzo9jYfK0u+Tzh1bDEAmPaUlyMg8rdXKZFl0huyczvnzc2XfBzrNn0+GZmTyn1WGS5IDYlxMw4ZRmjjh+vM8bafT/ZI5LPW8XBm9pm5JAqB9VCw3Oxw17BBaejD3UoqEf1qU/jn59ZLaRJ53/Lgp3bJYpwjXqefZmdvtkRZKC701T2ASy6bpTKGxU7gdVLFq2LG9S9B1pnItSeSZGnYGvQv1jHA9o3TX0HuYlqAbKT+F8Bq50ycRbgBqP4twJuLdOF8k/rP0DqdfcPw4fc7S2ke47lHaLXRHXZb8JZqXUACLvMQvKsBS9H46iZIunkGXqJF8PBR0cjPYvRe5Pf8TqeRnDVRmnSv+JQbzz2eD4XKhMC9xmDuWgoH8U5jvLQ0DehmBn5VkHHoOWnqyuJZtrKCrBbEcTQJTLCtkYtufzbLQnlfIavbJ7DJfldXwT1Cftvy65+023DeHvnDiBBk/ffd/4GHa14AfIgDePGRCFSgei" +
"cgxuyqaqDE9+2qHSjp7xEKXG+EtSjGVoqL8fISKKorKy0rzTflGgw4w5NZqoMdB57zFkSk79FzoRBrdmKGXdmjg763b4awdWpf+CJfXh2d/8tJgoj32H6+sGT2vSNflXbnq9N0k1NI+/Xb6A3+8u+Njaucjc38mcmhnm+hYcD40OfPoJYJ0zbOsXzqzykoHekSNtsnEI9aNdrBQamhZhALKzHCwOeC0C3TvJT456Bip3b9nxwWbehPb07+95lptSSA3//DBq26qCFrSJ6+9h9kDyh8X8Ecrno2tyGIHbRbxDndODvS4TDmUDVqkcSoZG8Q17HYd5J2fmqPz4RvPafLuWveb3yxfV1a/pueeezov27nnqk7IdFPH9MjE7jqCxH44pIt/CenmsJ6i2M2nh5GwSSULzaxiC1IoyhRZ82pV1gD/g37nL9Nfw03pf8XD6b34Os/vGt/3nL6b430F8GrY2TWYjs+gHdK1SUzGfKqO6ZBBwue7DtvSb+Lu9BdasEXCReXiArmU0vNI9Vgm12Ekoweo0ONd5PPn0vEj6K2UZeKCNZtCPN6nxA5MMaggJXkwpLObfCYqqYygxKF6/OUp4WcPC2tq39p5wfoVfdtuuhJkZTFioe613ML/ufLmkoD5G//AxCXmjxQzvlkjFdLYGR2WsKxQzBcQkV8GyJfOrrPCA/lbHQqlH21pwWawwePp93GRxC90HeCTIQM9CJDKXcTCho8MNGZLCy1PAFs592fcw8oSyEwLbdMc6T4iFkvsKdn4ruNcu4FO9tBrcO06dl61qcj34ZpwS6KzKZDQDg3j76UjkeYivzmTL8LyJUTKVSA0OTWStL9PSz5CZWhAp9fr6UheKGTwyUJ2zLI1+aP2937dnX6tpeWNmfQTeM9HtN8M1Eo69jrgVLDxA4oJ8R3fZQTYVChTs9EH4BB+Pf1ECz7SRHwsKqfnUoibS9vhahltfrMbCNh9BzLGb142dHr4Y8pk8NERPrtMRgn7Xv33Q9HXfhptIZr0Vvyl0" +
"/8DTO/Cz2Tj9zA9hX6DghaAM3JLZ8tK6w2YyhAzEe4ONqdvCgBGig0eDcVGmJ6OgZ7miGWYzr3RjQbWFbQ04ySl04bFc31MuUZdARs8oWU4e1mPUyeOoxTa8RtXHLn22suvuvonO/bMTP5E+4/HHvjiFx+47T4/lt18wYU3pU9LdkPJ7IaBnccsU2A2Qsd2kG0Rmc34nAPVXY4hR68r0KhVCqTFWpWaHrbFkoZcBbjlBNsR2zC1JfnfLR/39PV84+Q7EyPrd5y++x3cvzwR7cWlLJ+bWD6LkICmIzkGyUrSBehmtgOP3sHIjCWdeBbHvkW7SU+8FOjqggp6RgWIc2IpWDwwGjGaQSHNgrmSnQRhcujtam5jadMg+0QNaDI45pn2Q6dgb7DiwxdT3u27iPhj6R8z/n1ZW6f1dijuvYly8ar7NQMr/iXDyHk+GsBatEbCGsyumJcrMa342DQ7U9hM9Wc0YmQsNZYUF+l1hfkKOTJgg5oxtMhs9lU7JRIpZxGtmujIua+uYKBlKMdkiHaNbEq34Oh1J7/1YYntiecspeq89cvWJU/f/SGOXHEVvY9JtHv4V8Bndh9dIVAiFACrrZkL6SivrPPrXsX6yI5stQ4Xq49YxVDNzs5gdm6eYfQUSWoH8PRe++rlsZ6KtWvXRIs3NkzvHppIrvr62jWJKe3q4cZoY2GBUG9rby72aPLXDcR6i4taQofKPWVi2Wlkc7m0rWCJFIsLjESFY4ttMxMHvoDV5IPsB/41Xf/DMPF7PKdfZvEHoLy0EnpGpLiWQzRf2ZnhWwnFzHD75WQeoP1bFzZv3bBha8tF52sPzOIr0gfWjYysw0fT58/ye5+ktoxSpA+0tB9jXqVSy6NnlgdTQ2HwkZWtL7/c9jbY1dOvkXqx3oH4f2F2lt2DpJBR4yCj2wSp0RGtgwHMAxv/BqNDEZkYMvzlq6PXhM8/5r7h/Obrovg/00ZA+z4p6kzXkqIM7hpmZysjZRqlTHaG4QErywwPYJYx1AaZD/tPR" +
"l79cedjJyI/eiWKSTqNG3Eg/WesTf8g/eNMnp2AV43KI6VKGR1lXZhrwMrsrYjS5MMrf9L65JOtPwm/jS9Jn8K96S+8zfHgj5hdhDqG1rQqLB5VSiQh82kCkDQYXibpKisGUVsx/P8ofQxPpJN4NH07PtlENHW+0//t4DYSP4dfZmc190WW0Xs0c0ChBUKUCtBqGb12WLFD2hnkoMmUrVBjpVKeVIFJs8kHSkuhRreX2irL6TXvDrvdoFHTM1hCZh/brqIyidPb0gZNvl0TKnxcvLZC0TiycXNrZ3+4rfiBgXXruiY6Wu6u6PdrrbZmXLt2RUltsdnrampfFuusHQ1Uz9oFahuA5t+Tw/RMPzQbyQXTinSFOTIF23HTyAwexuW0TcPsvYNxmpkz0eDZqMGzioP2nwIDNq+gAKGCkoJiox4Sy9Nxm0dPPrSpAmBC6FoCZ7Uus6b+ih0vbm5r8SiEfu94ILh61UD/kPbgoXcbfnX6sLeiwtOKczzrklu2cFtyL/CdtW1zoZGQc2bb1rFE2xb4q5i3u9DVyTIkPjwZbm7bPHyqzW8J1u4+b3qPtsRTsmHs6WJPXs6B/eeez+X9BvCuEPpcwDuQISkpJkvxbp6EpXhHGO/OCgO80+nAXlh0pUVGeigiNPdE3lG1sJsyJEuLNXTAvZZ+Qe4J2eKNOyZ3r79uzar6trLwGm15pZcc/lWJ8cjM/hnPya1jxcXpP7UiSXdfAh7qQA+gjqDr+sRqj14+jBTQAs2mMEMbQiXFJgO1ZXa9XZVViVlZHcbW7IqH8FjxdEd7y/g6OR7atPFfZ8470TPcFU3/SgtaObhuYHj8vfP245WblkXqRPvmgZ8ouRHElIxoNSBENVAk8bVckqp9BTstQjwNqGKF2JinZ1GLjf2pJb6ORgqY9E06nQFqLjW0YXHInt1cNtmpSddhh+yCem9ZWYUr0hWPN+ObPf31NTnu3O6OPk96Gt+MON9G8XvAN7qj4a5IQXkuwRoXVioasEopA3ItdI13D" +
"tYAPRo8hdRq1oxnRYSZmIr5bsgKpFSqklqsUgkqyIRnPpYSXipVE58am+as2ANsE7c5AEFuR7NVZ3T6c9XlvBXG7z/l21V9IbuoOWZ+P6p06QtUngt2t0xsi8YOuR3u6orKjWsb15K764SxTStrXG3RQ+Ga5Su3LWs7d1lnpLc3Elv2YFOnz2M0l1TIlQMre2rqtCuHd5VrXOXVDou91lJcZ8Ybmjui4eZIl2++j0bLkAEtP2XIZ9typd0vdJoW8Y06jvkJ2yK2NfSMD6NPgDwNVlpf6cUdfqLJVDLjontkwLUsGO4fCq+2Q//kPHsgmn4D164ZECo+PH0Yb2CyjMLvc+RcMNQa1NOXcgIVpdIeHFbzs2ZSxQppSWJxVu9swSc2d8t7f7RDZleBQj3X2XmhrCdMzm2fBf2h401zczb0A5aeHo1FtAVYLivEovVwMi2HJppCrphYggbZghPXJbj5r6ORfDndK6Cz6Yy6QhUnhF4yRWnhLaQfdHbG4xfIezaUlTtqgK5P/tDVSqyetH51jjuHy+YpkE0O9E01mf6SKB4dpFNOmzQVUp1uoe/EM2Ez70ZPzXeozLRDpWIdqp6H259/ouvNlpZHu/8L61M/xjecPkzTm/sz+hmkl4d6IlrRAhFEMgkyW0rFzruANEE+WZx5NxrJEXuGBjvrGXLTLraqXjQVlBp1LfV1WnWDwlxOnKd/6nfzftJvoQzb6K57W3mxVs6mRJTKCnYKlXiOpWOFip7oxTpjUC9XGaxqdUWWqvEjNLjWqcTFkFTzHh6obnYFVaXLa9uD4eX9W/vym73rVii8IXzI4e9K/xzb64rKygOR9L/jhlUDEUHoDKQ78AapbABdBto/5IVDKvQLlf9sql89r/rLV7WsdSq8Gd2vX73CWiGmJNn/30FadA47q3/nWKJ/Z84t0hcu3b/LthqXb98xPT05Ob162fLhNct7V2v3H9h/4MD+/Qc8G8bGNtCH9fHmAvj3kC5d57czkmvQa6H+LKDXl3HjKUjli" +
"jeaxNWKQJqNlgbW3xLoIoQrzgqHaGkoLCy0FJY6gVFWWhqCog2UeBQIZrU8vpZomJz4Z19/pcLTsqGzLLhmuG9oKM98eOZdqD5P7/0jKTHj3JbRLclNjG+lJMjafFVoZaTfZtXKlAoTlinpmZcYWlN0DIeetCefYAUDTDUXHW34yejlbFCNsnZfValDqKDtPlAvO2v3ZbSXqRNfckkdxAfi6BHvxGLRFTiKPfLKft9L2yfd3Ta3L/3i4PLh1c3lnfXvgKoL1aTJW1n+/vTFZvMdrSGcuzG5ZX1L+iNzybwNbmKyh/Z6jpLQg97oyUx0WXSFNFZjpmM1+iAdB+R6nayuH7EqAi34nBp1k8ZpbgNlWif17S4BfFW0PQayxLZ8UCf7gr6dY3Hfrgo5Guz1rD22qGvHTyUwm9mef965u0RoD3lri8KdwbA/sWbQ1hA2NdQWtUQSbS1D2otqPJW6Qn25pcahayhobat2m0yqAr1gqa8uajCK/TugMUyWIyO9sUa0NQqwNbnQdOydP1lSrF+zhtuMyKAz6GhTx8LFE9DRpdoq1migzcb8usqidReRS8Ld3ZFWba47Zw0u9lxZcMiTfrud378FfUP8On4erD6dJyLsbBba9a4QG3/0FGdmcmxy8ZxUncGmMygzHQG7LrunWL+ySuENR1d1RVrWDGqFygB+Pv1ANBbFJelfr94g9Zvwm5Deor5ixZJ9Rfxmy223tH09Dmhm8fXpDt7vugjin9lXrPisvmJypnU2tPlQ/e6x8L52vCn9ZUB7Mb60Kf0NfCnDHQbavgu4z+grVizuK0LW2TYLu1nlSx9tf/De9iNHOr78UPszz7z9ixMnfvE27xfPjYL+Ut4uj+QWAEKCCumRdtya6DMZF3WP2Y8CuhoelHOCfaTmIlfku0NvpApZFAyGxC5lPhHV7ztt9ZZnWx75ctt3jOU1Fdbi8toHG/o78fH0RfhIOtbgLde6czL9dHw30HOWvmbFZ/Q1706/j3XQdVWmf483evDOmqb0b" +
"c7/P5xB8P/5ff8gm+jc9eg61PCpaxuhuXSdr6Sm4R7aPpx7BN2A93zu/SFgElQQ/wa/nwAKvCfwX/dK7Uz8ANkNqmaj45p5kHC+OMpN6Hk49J4DzEdXpdkKOqGgtzhqaRHrIKEOwtfcmzK7qvKJyeQLmCHNd0wV9TZnydYtvtVlXasHlhULtVZrsVfe0vKd+jJruTNoH1yrL2ywR9oFd4VQavM5zMZzmyBX0bk/k2VAlwC9r2bUG+kJekrz5EgWylXQbhS95FJOLbiMXiR5WDqBw7qCjscSsFisBdbUhFBTc1O4rgbwVNYWq4CL1G6FqumZ0yajKnt8s4h3PQjfWS9lhvBF9nbcf44waB2qdOkfb/X3xNpXFtscPUN7h7RqubPS7XG7TNXVgbWRxNO+i7TuuiFhKNBVZG6s/mZtoqm9J9FR4XE66kajvSvwrFrmbLDV+rx1Qr1Qkv6nYMjfVfMtal8VLN872Nh2ASpFTtSEXulL5YodN4UWQ2+O9sGQWqNU72TjpNJIB5NP2Yo8rNGokrnQcbOp6ALpfIjauERUGos2KJeM9nclNjoacVZXWyz0PKjqpmqPq46edWitLCwtLDEZ6CU8rJ2WT3fjZbXTsB37yHxTDWU34eZfn+7s7OiIRDrwgC/9rH+izu2uq3PjyUhnR2dnpGO03t1QD+EtPa2x3t5Yaw9WnL4bX7Y54PUG3J70LLzu64PXVfwNKz9HyCP4v+ltsGhHZLwI+jtm6Lg0Y6gD6TQ19HcV/UiNsExND2JCcoVMPgXy0RKFlu76UuUQ1RQ0QAFOM5GHc3LodiWlTTngg86kr83XGg4C7iaH1e6w240Oe766cn5wR8pVYNE4mnj+C1+FyWY1swbVXl3WsuOCW66/8eKD/nB0YMAb6vB6dFfesLx1PFAyOHDTDeknvnrjRY3tXyiJNFykq9ZfeP3NV07vXt17X2+nyWo01NdUzYb8rRObL7w8/dx919/cW1e9WWD30Itl7Rugc/Ssr2Y0ztY5jzZihdJtJ" +
"3KFDatYSUNEiQhVB4VcqZhCci1WyeQqevABxrJRDWuaq+kJLasEgeqA0CyEfU30Dtqs08Fy+CJp+aITwTIHT4szq8HgojnezPE4+OUbVq+W/vde3Jv5b4+XFEcr1m85Z3W7vyRc0znROrtdu+amtWtvXLPmxrVrb1rj7Lukv/+SPvHXX5DnyMvfsmlDstSen++bWD51iPJCvoAXXtSKdjNubK3FcjW76kIuk9PLXzLcgCKgVtKruXKxXKGW07sHKDtyGDvYLQSrfD4rY4iv1dfSHGryNDZUOQSv1ZvFlvzPx5bq+UkPac4jmJmHPxtjcpyV2qAzrC7I3xKNpKpPuf3ei684O2d+pyu+5HKjXqnb3hJNb3roZ3VNXtc3xfGlKB4ha9g8hJGufirMUcvk4no9KB+yw2oFVD6XqZRshmIddTObnemcoZ7WRRplZb3BF6ADET5oRIWgoWpnTfiXt/5+a+29o/p11XWbNtc5SXt19ds/ePLJH+Dcb387/ZHYfgmDbBQgm3q0JjJUAvpZjOmtcHTQMgf6NioqI3pg1w4NJnS6ls6wi/sIlWw5QQ6YqpwcWw61tPWorram2qnTV9FRCb2D7qWVB/mAIRDILnkJ2MXDr0T9UwHVdIQKHNDD25ZX9wyt2VT4uw/l5KM/5I1ubFxW5VzuPXdvQXNQJg+1Of5Fm5fbWNK33FFeX1zjGN2UX+DJzzswDRahutzxHbHO/zr+FTmF6tB0xJQP+XDkERmqq4IsyPqN0O7s60vpxJM2oROwB9EtpudmTUjxeXgbHf+pFMc7FHLFFUtCjEY0OoNbZ6yjE+WOTK9GLFzVIBB+wPt8v8YMVTj+lX3dis5ExfHhqHFVg3fDRsdQb3S5dd3w5uHodq/2FnfUo8sXXLZDxQ3qPNzbp1rT426r0xfYPFXt7RXuUry8j+fzHmKEBvS5EZ0hDwjNpYPiJZgoEB2XpHm0se1HexDd/HzugoHyTJeHda7pcJtcIb9iaRDoXNNuCyor1oHNne8YZdkRK" +
"asse+f4Q7HkwPr1UV9pU1Xj+omN2xrVZnfp1o0btpW4c3OW92kOTO08oO5bzvQvyvZS0LZSZ6QNWkkyaC1Bax66aApaZYjrLmgv2rpCQ6/KUG1BKlWZiqpbBarQQcfJYdcZtOqK+hBda8tHeouyhnrpBKbJGkhZXiAvtjWb3XlCvHtNPN5Sive6jeETDZc3+TSq+sLe7ssbHl/O9pcATaQIymQRao4Etex2bSVmizZkZAeYYw1GWnaUNltlotWWaQfAYrD9vWxcMAf0vgoUPQCP2K9gg4S0fOKe22+//ejz4cdOhL999I477gjtdvRW4fH0Y3hF+u6qXgfbWwjJ+3z3bPlj85aCtj8hlew9WlR/6b/3hOj+o2oukEZKp4zezqdC4moYFk/23uk3EFJePBeYW6l0MkxZ/0gNWYtGZDcDHB3c/lPWpyfha574yEvFB9rBCKvEb7Ja7j/CH/Zv7n14jklhsozjzfyb+zo8/wDP0/DQnUA34YvRSnwJPDa0CY3P/Rkdg56yCyyQEfonf0VbsRJtRX+d+yZWzn0TwkoIK8EtBbcZ3v8R3v8RwioIq2RTkJcjaARfDTjWz/05K/wjCL/6Kd+XDJPb0Ai5GZ4rwL/l7whPog4ShWeRi6cB/xfg+QxX/h4agZ73iGwr0NZzZljhhXAbhHdDeNuZYZJGHTI1pPk6+F9eIjwBtA7Csxb8FZ8dln0bcL8E4SYIO5cI10O4LSt8BYQ/hjDIlNj+9jD5b/D/FeQBfmzKCpfz8FsQ/hM8ITE98lv+XZLffJjJXwoTGxogPng+pysrAZ410HzBA7KRFQNuKUxdD3dXcBfeszi/hScbHr7jb3LaeFiWC09+lo6ePSzq6Gd9vxXwPgzvuAu0bSUd8Hjmvks64PHM/RXcv8J7P7z3Q/hdCL8rwSkvBh3aj0bk5wMOXh7OKucMbtGF8vwaf44sem7jz054rocHOuNzF8NzgLvnwRPg7u/5M8TjXg3PjfDcCs9Rjuc8jiP7iUt+4gJ+FKAu/" +
"C7qIi/Acy48J0GWBqB5P2phTx6EJ9GBBXCXw3fIJ57jMBLc5Twe2EryDLqXPIPrwD0JTzc8M/AMwfMQPJv589W/AY6+34yhXGc/JIe7G4HXUZAH2BYF6C9+msM8TfV+/kHvwrvt4I9m4blKfGRWeL8p66lfFJYeO3+ofwziUZzfg2d20fM7/iT48wx/dooPpYU+8jsAz/382cTDm8Q8kVz+/BLivMefT/jzMX9+DDr9eZ5f/A3P9NyrYFtf/SxXfgnQC2VIDrZFDvbns8LkRhQBng2TLnB3gWtGw/gv4M6IYfRLCB9DlSCfYWiPDZPX4TkCz73w3c/dZeBawfWiRnIU9A5wypohPAnPdng88BTA4wZc32NuhGhQBI8jDykDfy28N0D/QZibA/kMYzUaloUBJ7yHMsHikTqAXwPfngN3C6RTC+lUwrdnIZxGYbBdEfwmS/PzlY3PqefoX6EN9WOg7R0UJZfC8xA8v4PnA/4cg6cd0rcBzK/g+ROER9j8bF3Wn/esf8vQAfGPrGd/N5Pvk/dkZbLVsktlKdlzst/Iidwp3y6/UH6//OcKh+JF5VHl3cqvK99QGVXnqz5SK9XF6jp1m3pAPaaeVV+mPqZ+SP2U+mX1m+oPNERj1Dg1YU2vZqNmWnNYc7Pmfi3S7tNeob1D+9McV87FOU/lvJzzZs4HuSTXlbsz98Lc63PvzT2R+0ZecV5dXlveQN5Y3mzeZXnH8h7Kz82/NP+9go6CwYKtBfsKrij4oHBX4Ru6oG6Zbr1ul+5i3fP6Uv0h/Qv61/Xv6T825BrWG541FhuHjOPGA8arjHcZHzU+a/yR8S3jRyalqdhUZ2ozDZjGTLOmy0zHTA+ZnjL9pWhZ0Y1FLxb9sOj1ojeL3jG7zVeZ3yguLXYVdxQPFm8tPlb8VklFSaJkpGRnyTMlfylNlD5kIZZey0bLtOWw5WbL/ZZTlpcsb5Tpy9aXfansD+WoXF/uKA+WX1/+YYW8oqiipqKl4t6K9yqDlccq/yJohTLhR" +
"iFtLbTarHVWr7XFepf1Pusj1hPW39ictrCt17bRNm7bZbvYdqPtDtu9thO2F2yv296zfWzPtVfYPfZe+7j9hP0jh9ux2nGp46TjRcdPHb90vFflr7q56v6qU1UvVb1R9Z9VaWeh0+ZMOX9Y3VU9W32iRlkzXvNMrbJ2c+2zte/VOetW191ad6LulboP6/X1kfpd9Q/Vv+PSu/pdd7gecr3n+ktDYcNYw6GGOxqebHjL3eI+7L7L/XKjvLGjcW3jZOP5jTc23td4svHlxjc9dZ5ez7Wed5pWNz3q1Xq93jHvCe9T3ue93/e+4n3D+5b3fe+H3o99xKf16X3LfIO+9b6tvp2+Wd/5vkt9R303++7y3ed71HfS94zvBd/Lvn/z/dz3G99/+j7yfeKv8Pf6N/qn/T8P2AL9gT8EXwj9MPTLcGG4OOwKR8LLwnvC94dPhl9oRs3Fzfua32whLWUt/pahli+1PN/ySeuu1utbv9L6bOvrbfK28bavtL3Q9lbb++3q9rb2Pe3Xtz/aUdxxWcdDHd/veKszv7OmM9bZ2znUub5za+elnc9HUGR9ZE/kjshTkQ+iyqgt6o8moiPRndELo9dH742eiL4cU8aKY3WxtthAbCw2G7ssdiz2UOz52Ptxfbw/vjm+J35p/Nb4V+JPxr8f/3lXcdfOrme7PumOdX+p+5NEIjGS2Jm4MHF94qme3J7enit6TvT8dJltWe+ysWXXL3ty2evLlctdy7cvv7dX27u69/7eF3o/6jP2xfqm+27ue7a/uP/8/lMrylaMr3hyYGTgSwOvr3Su3LzyrpXvDJYNjgw+uap0VWzVrateGdIO1Q2NDB0eemnoo6FPhuXDucPG4bJhx7Br2D/cNtw13D+8FjrK24enhw8MXzx8xfD1w8eGvz78yuqK1XetGVjzb2vXr31vXc26G0fkI5MjJ0Y+FPtFpAbtQ4UoSW/XRpvRA+g6eJ2Xb6IX+SF69tsjmb5ShxiD/RaiDu4nEHOA+2WoEiy86JdDH3GW+" +
"xUoD13F/UpkRLdxvwp50MPcr0Z69O/crwH/77gfupPoNPfnoBIs+XNRMSni/jy0gri5Px+VkwvombdyDYQuJHdwP0YumZH7CcqXRbhfBn2BAe6XA8y13K9ApbInuV+JamSvcL8KjUHbXPSrkUO+nvs14L+Y+7UEye/j/hzkVUr+XORR/hv356G7VHLuz0ctqi9xfyESVD/lfh3Sqv4i+iFzWjXhfoz06lzuJ6hYXRGf2XPe7NSOyX1CzbZawevx+oSt5wlDE+cnhVjynMl9U8nZqe3J2XG3EN21S2CQe4XZib0Tswcmxt2jE+ckpxmgkEgO7N/dT7973V6Pp3VN37qBVvadfmZfG9jnxXEE9nbtxOzeqZlpgUX+jGhTe4WksG82OT6xOzl7jjCzfSl6z3x15pv/XeaF+Mz0vonde2Zmk7PnCYM0A8nphuhscuvUNmHfeXsmtie3TUBU8cWuiX37AIRSK2ZlO8SmWRmf2Du1Y3pinCa9Irl3Zv+4MDyxZ2JyVkhOjwtT+4SDSZpoNtiZFDLY6eRu+D7PKLfQl5wGBFJK+/fC5+0zs0L39I5dU3sn3ZP79u3Z29LYePDgQTcFmppNTru3zexupB/g/ezWBVyYml09Q5EI+yYBG43govSBf3pi28TevZQN+2aEma37klPTADQh7JraNjENEbbPzuwWFqcipb4gZR5jL4qjGbQHnQeWYArtQJNgawRUg7ahWnC9UPq9yAe+rQAhoCE0gc4HSySgGPyew6CnwEfjbmfuOHLD1yjaBX9CFs69LDQB7gS4B+CXQo6Cew7Em87CKKAE+AbQfkTn+vozGGjaO+DtLpaOF2JT6jyoFa1hJw0PgG8en4RtHldDFq6/Jd21jOK9EJ5h8PMp/+9Sm2I8obzcB/iTwI8JiEfzdg68mwF+fl5+fx6ozwPz/6YmCCz1afhOubAH/LMMC01rMCMBysMGwEm/bYXwNsa98wB+gqW6DVwx1WyIXfB2H8M8y1IXeZstle08bUkq44w+SvE0o" +
"0/K9QqA3wuw+9m7YfhGU54EvALDNM4wUDwHGaSY07Nh+zw8nMc7Db7dPP5SGkXz3cfeiRQsztN+xvFx/oVS3A3fdwB36Hcaf5JxaQ+EWlAj/B1kf+4MpinG1Wl4sw3e7AYIKYYIPwv5Orsu0NirIZ5ECZXcJKdNSsGV4Z/4nvJrG+Pe3ow27GM4ZiCtffBuivGAYppgkp5i8NM8he0QZ4aV58/Ky+K8nz3PC9PY+5m2ZN4OzL+jctrONEjUk0mmCwe4hs7LeFIaH58bQVVoiX/QNqRtPXY3HpZhOVZgJVZhNdZgLc7BuTgP5+MCXIh1WI8N2IhNuAibcTEuwaXYgstwOa7Aleg7WMBWbMN27MBV2ImrcQ2uxXW4HrtwA3bjRuzBTdiLfdiPAziIQziMm3ELbsVtuB134E4cwVEcw3HchbtxAvfgZXg57sV9uB+vwAN4JR7Eq/AQHsar8Rq8Fq/DI3gUr8cb8Ea8CW/GW/AYTuKteBsexxN4O96BJ/EU3onPwbvwbjyNZ/AefC6exXvxPrwfH8AH8SF8Hj4fX4AvxBfhi/El+DD+Ar4UH8GX4cvxFfhKfBW+Gh/F1+Br8XX4enwDvhHfhG/Gt+Bb8W34GL4d34HvxHfhL+K78T/gL+F78L34H/F9+Mv4fvwA/gp+ED+EH8aP4K/iR/HX8NfxN3AKH8cn8GP4JH4cn8JP4CfxN/FT+Gn8DP4n/Cz+Fn4Ofxs/j7+DX8DfxS/if8Yv4e/h7+Mf4Jfxv+Af4n/FP8I/xq/gV/G/4dfw6/gn+Kf43/Eb+Gf45/gX+E38H/iX+Ff4Lfxr/Bv8Nn4Hv4vfw7/F7+Pf4f/Ev8d/wH/EH+D/wh/i/8Yf4T/hP+P/wX/B/wd/jP+KP8GncRrPETrZR4iMyImCKImKqImGaEkOySV5JJ8UkEKiI3piIEZiIkXETIpJCSklFlJGykkFqSQCsRIbsRMHqSJOUk1qSC2pI/XERRqImzQSD2kiXuIjfhIgQRIiYdJMWkgraSPtpIN0k" +
"giJkhiJky7STRKkhywjy0kv6SP9ZAUZICvJIFlFhsgwWU3WkLVkHRkho2Q92UA2kk1kM9KRLdAnGCNJaN/nka1kGxknE2Q72QG9jE70DNJCW/+r6BR6Ap1Ej0NbPx/a4LegD9CTUE5uQA+i+6FXY0W3Qq/lZnSETJIpaNPnQr/lEfQs+hb0mcagLxRBW9B76DF0guwk55BdZDeU1qvINJkhe8i56EoyS/aSfWQ/OUAOkkPkPHI+uYBcSC4iF5NLyGHyBXIpOUIuI5eTK8iV5CpyNTlKriHXkuvI9eQGciO5idxMbiG3ktvIMXI7uYPcSe4iXyR3k38gXyL3IAeUXieqhtq0FtWherBzDWBRGqFObWK1qh8FUBCFUBg1g/1pRW2oHXWBfU6gHrQMLUe9YDH6ofYZQCuhNlwF1mIYbOkaaJusQyNgKdajDWgj2oQM0HMzQa/OjIpRCSpFFlRG7iX/SO4jXyb3kwfIV8iD5CHyMCpgq7to78iOrkFH0bXkEeg1XYYuR8dU+6envH5PNH9bcnZ2KrljYnZi3/7Zafra44l62GePR3K93G3iro+7fu4GuBvkboi7Ye42czfK3Rh349ztEl1vgrqJ7kRCDAdZ+tCp53Bejs/L8Xk5Pi/H55XgJHzdHD/H5+nOp/kL+j3uacj2zEEVD+ZKrw9OjU+IL71N3A2JNIW7edjLw14e9vGwn4cDPBzOT+6a2L49uW8yuS8JyRkWBt3bp6aTYpQQSyoRC0hJhkUUzR7u8iSb+fdQN4cPcbeZu2EeP8rhg9yV8IX49xgPS3Bx7sY4fo4vyPEHpbCEX4IX6eiOSnQmxO9hMd1YKMbdLu7GefwuHo/nK+rnro9/l/By+qOc/qhEP1eTaFQM+3j6UY4/LMLHws38exP/zkXo4+nGPGK4WYLn+W/m8gh38XAgl7tZQvNxucckZDwTMZ4JnxTmmYjxTMR4JnxcT2JcCDHO1BgnwifF48yI80zGOJN9XC/jPP04Ty8upc/Ti3M8cSksxWvmL" +
"k8/ztOPS+lL73n6XTz9OE8/ygtN1MOYkglAl3kqE5iC7jIvY1wGXZzcLk5uFyeX60AsyHUmyHUmyGXql+Lx7HTx7HRxdka5joZF3YhJMmzmMvZzcXXxbHXx7Hbx7EaleD7uBrjLi3U0zjMVz85uPDu78ezsStnj3Ovm3Ovi3ItxlYoFstBBYB4dBLLQcWXp5lzo5ui7Off8nCvdksu50825E+viSLuyk+vKTq4rKzmukzGpQPsldJIN5MmLBd0T5DoW6+IFkutWTCIvnsjlblYBikcLDiYP7js4Mz6zb6/4fnJiMjmb3MoT4wrazSXWzSXWzSXm5d8lCfECEosGeNjLE/VmJdrF+d6VXZR5EYtFue5Fue5J9iTOdSnK7aGka1FuD+M+npIvO3siubEER5bgCpbgPPTzbCW4SBOcpwmpPuHZC3O4MM++ZKTi3TzR7iyRQmBepBDI0iCOLsG51C2JiOeBl8uYVA5iHC7Gy22C1xkJTkaCa2SCa1yCm5UEV5WElF5Uvyu5e8vCus985iuWicJszadVbml2uaAvmCfD5UQXJ6cr4S3g5YuWNUlpPMEg51KwO0s00SZeYpuyy3JTdlluymJdkGtvUNTeHM4P997Zbdwfp36N9F7yxd3bOA85b2PctvCqIhbjNoZXEbEYr68CXKMDXJMDvBgFeJNDaif4JVXixTQg2TquIn7JdvJ6LMS/B4LKc2Z2JSfdioPJvbs4U5oCyr27k7t2HeAwYSWIZvf5SeVW5mj2SbJitMZ50ywebOKuN4fTJqWfiAe6uZvgMDwfCU5HgucjwfOVkL7z/Cd4/hPczCS4NUtwnU1wfgU5viDHF+T4pPSCHF+Q4wtyfEGOT2rXBJvV22f2z+6fHd+v2TtxYGKa+/ZMzCb3zczmcuLd+6b37+aBpuyANzvgyw74swOB7EAwOxDKDoSzA81ZgWA2BcFsCoLZFASzKQhmUxDMpiCYTUEwm4JgNgVBkYI8iUEslJ/hEguKXPRwrWviWtjEtTPIi6SHS" +
"80jhbl2SjU9bwF0+bq5m9Bun9qxf3ZiPLl3kr7yepp4P6RJFKgnytv7ouJ5A6Lt9QbEBiK4Me3Mnonprft37ZrYx0G83PWJXQJv2H2QGQAxGOpeEPRGFwZjC4GbFwT9wYXBRXEXfo0vTMi/EBWjilqiLKrmg4yqrGBsIXDzgiCjaj4Yb1r4dRGqRcAL0/VLmDUs6ANUosmLdos+2sSW3sU80rvmMH8HDe3Mu6bMu67Mu4D7YL7koxbXzftp0Si3ztFsux3NtttRRhcPJNx7D+6d1GUHWMSFb2jshW+yUMSzUcTPQBE/A0V8HoXm/InZGVYw1DPTE6IHmjvMo903OTshvtPQIsV9Uwc43N6pQyIcK2Gid4LOl4mA01Mcobx7/6x4xxmem0MFS40d8n9BNvIQIlez8UT2i8Lw50H0dC5EDuPnEZrrIbvpyiH88tyf53oQSiGXkEJrRrpHBaHvSZS/qi+lHF4/kvJbUjWjY9uFo2tGUqQq+U01UqNt2+xbLVZrCo2mUNzedQJoio/FGlLYlRLGtjekiMtutVsbUjKXMP6YzGhCsXjKEBfGxmLHiTEeO14li6dIfPUhIZVrB088OZ6SDx46QQgBNCnrRJmVvj2Rb8KxMgG89tgJAzbAN3sKDY5MjJ4owoQlKHelZPUpU3yEppcqisc5gEUYF1LPDabkzvUnanBevHtbd0rZPWJNyapGhzaMALDl6IiQGhyEVxGAToWpLzw6KhwXoYGiGnjFQ0LKQ797KORzgyMCcONoUkhpB0fG4I1Av2mpL0h9wTHL2OjoqAW4lcqNb0uhoZEU6qPAVghb+lIV1FfRl3yyEG2jEE8q0NbR0fHkaArXj47yHIwK45Afe2y0IaVwCUCBvCoJeVLFB0dSKnsspbbHQAIQZawhpWTsBk4I48dVW2MC/UizaxHJp78pMta9LaWos8LHuHBUOAppHfcoqoBDq0bGBi3JodER+6h1VEhFhkfgm4XyhZPSkFK5Upp4/QlERDGrIWiP2UFd7LFki" +
"mzdnsLbgJCUqq4hpXEJlNp8yJYcbRUohlRkbJSCjHUxarWuE5p8FO+O1VkzipPjWqhIuSIWXA8kxCHrY0L3UXuSCpUxG1moQFKCBYiUqATR2pNdYhJ5Z4lOL/6ikSNLRcp3sQw9lpeLZN2QisVuHa0DJS5wHSekOzWe7GpIFboAVBBSBfFeigA8IKFUIQ0NQaiQyUsHiAoZUwTgwTZIOaWLjwlHx4SUDtjWkNK7+laPHJePd406UnkT9kMNKYOrb9VI37D40mKF9wb23ug6jvTxNSPH9fp4CidjKV09LXKgWrHjBfSnEH5SuAhkIasaHDlO2Qf5jR0FCUOyhXVWO0ST/BbxO40CJZm+GYWc9AD9PfB2obDOIsLjCBnswK94CnWcwBgzaZlc6Dgi3atHUnp7TOhO5YP65UHBHgNVNMKbMaDhieJijHTIgGKxGOWEEQiBb8eN6vrUNfUWG/CtCDJrqm9ImV3HMXWLgfHULXEdl1G31HVcTl2L67iCumWu40rqlruOq6hb4Tqupm6l67iGuvUuuySIlHIMWG4X3Cm8iRabhpQr62NR5uO54seGrI/OzMdZ8aPgQqmC+rNlmOb1cTGvNKPZ+bNC/gSgywb5o64d8kddB+SPulWQP+o6IX/UrYb8UbcG8kfdWsgfdesgf9R1u4Q2prmNLki2eEwA64fH4ky2UBrdVHk9rlRjfaoRCmYTlIke4SxitSfDdmrhPxXCQnPvlWR9PF/ZTVUv1VR3XIFN3SNgHWkufVnsORuM3yUEGOUBwCbCdJ+ZJpTfJWmh71HRSVatdXXYw8f92ETzGgR+QAaWph9KTTLckAq53Oa2hlT4s0BBw7cBeDOICBVVCW6hh9oGYO3yo0d77D1gTEagBgTzC1VTGGOTETjcAkasKGUGMDnY1SoGdjwXxVI58fqJo267ILQdBZytC8EEt4gvpYTSwKGF1Bg1LpFVI4/JBYVgeUzuVJSOxqjJ1YL1trMY9sRYShlfXG7HqNkTqyd5fGzcnlJA7Qqf5" +
"fGkBfxj1OQtjpME0qAisCdAxnZIIUGrLm2cpQL4lkjELhpXJRRiEIYCFE5xBlbASImookTI4Jeb1Pm0QBHaJF4I8Fbh5LywtwGb2jOfUlr2PWHvoYlSKXZkWEgzI3I6hVaPuIU2qNkp9fylQOniokgpqyC0PLsRIwpxKW3n0rJTle/MoiQuiWuMtnQWZ1kScQTsh5tyMZEyx0cGLVC5Cm2j7uMebIRyG13wdcgyuOBrbMm4nxYj7kq11H9agl2uVGv9UaCN6hhk6qygIFB3ygMxulmWqX46Rc4noaUWE7NOFdQOxccNJU/En3Ad10KlI0X5G1W65/+WFtM8UTvWZgdTlaUv1lFOZw8Y4JZ6iSvLINRab7VzvvDcZFiwHFhgEos9NEughBvcqSCU8t6zvO8DdNhoSIXA3+9KNYOzgnKxG9gtJKAGlrg14KIKnVoB3pWuEwglwDMIHkw9q1wnMHszBB72ZpjC9IBnNYWhnjUUhnrWUhjqWed6DGxhHHwj4MPMN+p6DIvv1oNPfLeBwmHq20jhmG8ThWO+zRSO+bbQNLvBM0bTpJ4kTZN6ttI0qWcbhVkGnnEKQz0TFIZ6tlMY6tnB6OoC3ySji/qmGF3Ut5PRRX3nMLqobxeji/p2M7qob5rRRX0zwOO2jAD3sFAqAt5zRW8UvLOU6SwUg9BeqGs5zD7RS2H2MxjMYQ5A5PYM1oMsxGIcEr00xnmil4KfD3g4wAWilwJcKHopwEUA25HBdzELMfBLRC8FPyx6KfgXICYHuFT0UoAjopcCXAawnRl8l7MQA79C9FLwK0UvBb8KYnKAq0UvBTgqeinANa4TOayJm1JaTsiJrBt6T2AGR2P1KfVESuYYPCRV1g20ioXO4EtD0LTE10N1J1rOPceRKvaYA/oDddT3eIm6QEG0ot+qNMqYXxN7WnlYTtcq50AoJ/Y0isAfDX2TnvHdddyBr1oFpfeqERoe7zpeQ8NPqpH4AkFD+Hg1ffWU+jDC8shV21ZLH+i/xy1KnYzk1" +
"z2J5y5Pya+DerrrMcW4EnV18dXS8O//AbtI+DQgMgEAWklQ"), false));
            this.Dictionary.Resources.Add(new Stimulsoft.Report.Dictionary.StiResource("YekanBakhFaNum-Regular", "YekanBakhFaNum-Regular", Stimulsoft.Report.Dictionary.StiResourceType.FontTtf, Stimulsoft.Base.Helpers.StiPacker.UnpackFromString("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"), false));
            this.Dictionary.Resources.Add(new Stimulsoft.Report.Dictionary.StiResource("YekanBakhFaNum-Black", "YekanBakhFaNum-Black", Stimulsoft.Report.Dictionary.StiResourceType.FontTtf, Stimulsoft.Base.Helpers.StiPacker.UnpackFromString("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" +
"QBaSVA="), false));
            this.NeedsCompiling = false;
            this.Text26_Sum = new Stimulsoft.Report.Dictionary.StiSumDecimalFunctionService();
            this.Text25_Sum = new Stimulsoft.Report.Dictionary.StiSumDecimalFunctionService();
            this.Text22_SumI = new Stimulsoft.Report.Dictionary.StiSumIntFunctionService();
            this.Text21_SumI = new Stimulsoft.Report.Dictionary.StiSumIntFunctionService();
            // 
            // Variables init
            // 
            // CheckerInfo: Value CompanyName
            this.CompanyName = "";
            this.Culture = "fa-IR";
            this.EngineVersion = Stimulsoft.Report.Engine.StiEngineVersion.EngineV2;
            this.Key = "01bbc7b735df4d58ae8affdaad12d138";
            this.PreviewMode = Stimulsoft.Report.StiPreviewMode.StandardAndDotMatrix;
            this.ReferencedAssemblies = new string[] {
                    "System.Dll",
                    "System.Drawing.Dll",
                    "System.Windows.Forms.Dll",
                    "System.Data.Dll",
                    "System.Xml.Dll",
                    "Stimulsoft.Controls.Dll",
                    "Stimulsoft.Base.Dll",
                    "Stimulsoft.Report.Dll"};
            this.ReportAlias = "Report";
            // 
            // ReportChanged
            // 
            this.ReportChanged = new DateTime(2024, 8, 25, 0, 4, 59, 297);
            // 
            // ReportCreated
            // 
            this.ReportCreated = new DateTime(2024, 8, 16, 19, 47, 22, 0);
            this.ReportFile = "F:\\GitLab Projects\\garnetaccounting\\GarnetAccounting\\wwwroot\\Reports\\acc\\DocPrint.mrt";
            this.ReportGuid = "37b52cfd91cb41f8b3d68a8ca70812de";
            this.ReportName = "Report";
            this.ReportUnit = Stimulsoft.Report.StiReportUnitType.Inches;
            this.ReportVersion = "2020.2.1.0";
            this.ScriptLanguage = Stimulsoft.Report.StiReportLanguageType.CSharp;
            // 
            // Page1
            // 
            this.Page1 = new Stimulsoft.Report.Components.StiPage();
            this.Page1.Guid = "15284e619ca24b18a92c70f830c7141f";
            this.Page1.Name = "Page1";
            this.Page1.PageHeight = 11.69;
            this.Page1.PageWidth = 8.27;
            this.Page1.PaperSize = System.Drawing.Printing.PaperKind.A4;
            this.Page1.RightToLeft = true;
            this.Page1.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.FromArgb(255, 216, 216, 216), 2, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Page1.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            // 
            // PageHeaderBand1
            // 
            this.PageHeaderBand1 = new Stimulsoft.Report.Components.StiPageHeaderBand();
            this.PageHeaderBand1.CanShrink = true;
            this.PageHeaderBand1.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 0.2, 7.87, 1.3);
            this.PageHeaderBand1.Name = "PageHeaderBand1";
            this.PageHeaderBand1.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.Bottom, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.PageHeaderBand1.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            // 
            // Text1
            // 
            this.Text1 = new Stimulsoft.Report.Components.StiText();
            this.Text1.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(3.08, 0.1, 1.7, 0.3);
            this.Text1.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Center;
            this.Text1.Name = "Text1";
            this.Text1.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text1__GetValue);
            this.Text1.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.Text1.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text1.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Text1.Font = Stimulsoft.Base.StiFontCollection.CreateFont("Yekan Bakh FaNum Light", 14F, System.Drawing.FontStyle.Bold);
            this.Text1.Indicator = null;
            this.Text1.Interaction = null;
            this.Text1.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Text1.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text1.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text2
            // 
            this.Text2 = new Stimulsoft.Report.Components.StiText();
            this.Text2.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(2.64, 0.4, 2.6, 0.2);
            this.Text2.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Center;
            this.Text2.Name = "Text2";
            this.Text2.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text2__GetValue);
            this.Text2.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.Text2.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text2.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Text2.Font = Stimulsoft.Base.StiFontCollection.CreateFont("Yekan Bakh FaNum Light", 11F, System.Drawing.FontStyle.Bold);
            this.Text2.Indicator = null;
            this.Text2.Interaction = null;
            this.Text2.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Text2.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text2.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text3
            // 
            this.Text3 = new Stimulsoft.Report.Components.StiText();
            this.Text3.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(7.2, 0.2, 0.6, 0.2);
            this.Text3.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Center;
            this.Text3.Name = "Text3";
            this.Text3.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text3__GetValue);
            this.Text3.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.Text3.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text3.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Text3.Font = Stimulsoft.Base.StiFontCollection.CreateFont("Yekan Bakh FaNum Light", 10F, System.Drawing.FontStyle.Regular);
            this.Text3.Indicator = null;
            this.Text3.Interaction = null;
            this.Text3.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Text3.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text3.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(true, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text4
            // 
            this.Text4 = new Stimulsoft.Report.Components.StiText();
            this.Text4.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(7.2, 0.4, 0.6, 0.2);
            this.Text4.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Center;
            this.Text4.Name = "Text4";
            this.Text4.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text4__GetValue);
            this.Text4.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.Text4.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text4.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Text4.Font = Stimulsoft.Base.StiFontCollection.CreateFont("Yekan Bakh FaNum Light", 10F, System.Drawing.FontStyle.Regular);
            this.Text4.Indicator = null;
            this.Text4.Interaction = null;
            this.Text4.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Text4.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text4.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(true, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text5
            // 
            this.Text5 = new Stimulsoft.Report.Components.StiText();
            this.Text5.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(6.3, 0.2, 0.9, 0.2);
            this.Text5.Name = "Text5";
            this.Text5.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text5__GetValue);
            this.Text5.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Text5.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.Text5.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text5.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Text5.Font = Stimulsoft.Base.StiFontCollection.CreateFont("Yekan Bakh FaNum Light", 10F, System.Drawing.FontStyle.Bold);
            this.Text5.Indicator = null;
            this.Text5.Interaction = null;
            this.Text5.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Text5.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text5.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(true, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text6
            // 
            this.Text6 = new Stimulsoft.Report.Components.StiText();
            this.Text6.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(6.3, 0.4, 0.9, 0.2);
            this.Text6.Name = "Text6";
            this.Text6.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text6__GetValue);
            this.Text6.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Text6.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.Text6.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text6.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Text6.Font = Stimulsoft.Base.StiFontCollection.CreateFont("Yekan Bakh FaNum Light", 10F, System.Drawing.FontStyle.Bold);
            this.Text6.Indicator = null;
            this.Text6.Interaction = null;
            this.Text6.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Text6.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text6.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(true, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text7
            // 
            this.Text7 = new Stimulsoft.Report.Components.StiText();
            this.Text7.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0.1, 0.9, 0.9, 0.2);
            this.Text7.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Center;
            this.Text7.Name = "Text7";
            this.Text7.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text7__GetValue);
            this.Text7.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Text7.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.Text7.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text7.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Text7.Font = Stimulsoft.Base.StiFontCollection.CreateFont("Yekan Bakh FaNum Light", 10F, System.Drawing.FontStyle.Regular);
            this.Text7.Indicator = null;
            this.Text7.Interaction = null;
            this.Text7.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Text7.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text7.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(true, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text8
            // 
            this.Text8 = new Stimulsoft.Report.Components.StiText();
            this.Text8.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(7.2, 0.6, 0.6, 0.2);
            this.Text8.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Center;
            this.Text8.Name = "Text8";
            this.Text8.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text8__GetValue);
            this.Text8.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.Text8.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text8.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Text8.Font = Stimulsoft.Base.StiFontCollection.CreateFont("Yekan Bakh FaNum Light", 10F, System.Drawing.FontStyle.Regular);
            this.Text8.Indicator = null;
            this.Text8.Interaction = null;
            this.Text8.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Text8.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text8.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(true, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text9
            // 
            this.Text9 = new Stimulsoft.Report.Components.StiText();
            this.Text9.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(6.3, 0.6, 0.9, 0.2);
            this.Text9.Name = "Text9";
            this.Text9.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text9__GetValue);
            this.Text9.Type = Stimulsoft.Report.Components.StiSystemTextType.DataColumn;
            this.Text9.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.Text9.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text9.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Text9.Font = Stimulsoft.Base.StiFontCollection.CreateFont("Yekan Bakh FaNum Light", 10F, System.Drawing.FontStyle.Bold);
            this.Text9.Indicator = null;
            this.Text9.Interaction = null;
            this.Text9.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Text9.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text9.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(true, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text10
            // 
            this.Text10 = new Stimulsoft.Report.Components.StiText();
            this.Text10.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(2.64, 0.6, 2.6, 0.3);
            this.Text10.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Center;
            this.Text10.Name = "Text10";
            this.Text10.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text10__GetValue);
            this.Text10.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Text10.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.Text10.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text10.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Text10.Font = Stimulsoft.Base.StiFontCollection.CreateFont("Yekan Bakh FaNum Light", 10F, System.Drawing.FontStyle.Regular);
            this.Text10.Indicator = null;
            this.Text10.Interaction = null;
            this.Text10.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Text10.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text10.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text11
            // 
            this.Text11 = new Stimulsoft.Report.Components.StiText();
            this.Text11.CanGrow = true;
            this.Text11.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(7, 0.9, 0.8, 0.3);
            this.Text11.GrowToHeight = true;
            this.Text11.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Center;
            this.Text11.Name = "Text11";
            this.Text11.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text11__GetValue);
            this.Text11.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.Text11.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text11.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Text11.Font = Stimulsoft.Base.StiFontCollection.CreateFont("Yekan Bakh FaNum Light", 10F, System.Drawing.FontStyle.Bold);
            this.Text11.Indicator = null;
            this.Text11.Interaction = null;
            this.Text11.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Text11.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text11.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(true, false, true, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text12
            // 
            this.Text12 = new Stimulsoft.Report.Components.StiText();
            this.Text12.CanGrow = true;
            this.Text12.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(2.2, 0.9, 4.8, 0.3);
            this.Text12.GrowToHeight = true;
            this.Text12.Name = "Text12";
            this.Text12.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text12__GetValue);
            this.Text12.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Text12.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.Text12.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text12.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Text12.Font = Stimulsoft.Base.StiFontCollection.CreateFont("Yekan Bakh FaNum Light", 10F, System.Drawing.FontStyle.Bold);
            this.Text12.Indicator = null;
            this.Text12.Interaction = null;
            this.Text12.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Text12.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text12.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(true, false, true, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            this.PageHeaderBand1.Interaction = null;
            // 
            // PageFooterBand1
            // 
            this.PageFooterBand1 = new Stimulsoft.Report.Components.StiPageFooterBand();
            this.PageFooterBand1.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 10.99, 7.87, 0.3);
            this.PageFooterBand1.Name = "PageFooterBand1";
            this.PageFooterBand1.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.Top, System.Drawing.Color.FromArgb(255, 216, 216, 216), 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.PageFooterBand1.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.PageFooterBand1.Interaction = null;
            // 
            // ColumnHeaderBand1
            // 
            this.ColumnHeaderBand1 = new Stimulsoft.Report.Components.StiColumnHeaderBand();
            this.ColumnHeaderBand1.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 1.9, 7.87, 0.3);
            this.ColumnHeaderBand1.Name = "ColumnHeaderBand1";
            this.ColumnHeaderBand1.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.ColumnHeaderBand1.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            // 
            // Text13
            // 
            this.Text13 = new Stimulsoft.Report.Components.StiText();
            this.Text13.CanGrow = true;
            this.Text13.CanShrink = true;
            this.Text13.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(7.4, 0, 0.5, 0.3);
            this.Text13.GrowToHeight = true;
            this.Text13.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Center;
            this.Text13.Name = "Text13";
            this.Text13.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text13__GetValue);
            this.Text13.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.Text13.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.FromArgb(255, 191, 191, 191), 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text13.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.FromArgb(255, 242, 242, 242));
            this.Text13.Font = Stimulsoft.Base.StiFontCollection.CreateFont("Yekan Bakh FaNum Light", 10F, System.Drawing.FontStyle.Bold);
            this.Text13.Indicator = null;
            this.Text13.Interaction = null;
            this.Text13.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Text13.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text13.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(true, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text15
            // 
            this.Text15 = new Stimulsoft.Report.Components.StiText();
            this.Text15.CanGrow = true;
            this.Text15.CanShrink = true;
            this.Text15.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(3.5, 0, 3.9, 0.3);
            this.Text15.GrowToHeight = true;
            this.Text15.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Center;
            this.Text15.Name = "Text15";
            this.Text15.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text15__GetValue);
            this.Text15.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.Text15.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.FromArgb(255, 191, 191, 191), 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text15.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.FromArgb(255, 242, 242, 242));
            this.Text15.Font = Stimulsoft.Base.StiFontCollection.CreateFont("Yekan Bakh FaNum Light", 10F, System.Drawing.FontStyle.Bold);
            this.Text15.Indicator = null;
            this.Text15.Interaction = null;
            this.Text15.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Text15.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text15.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(true, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text16
            // 
            this.Text16 = new Stimulsoft.Report.Components.StiText();
            this.Text16.CanGrow = true;
            this.Text16.CanShrink = true;
            this.Text16.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(1.2, 0, 1.2, 0.3);
            this.Text16.GrowToHeight = true;
            this.Text16.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Center;
            this.Text16.Name = "Text16";
            this.Text16.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text16__GetValue);
            this.Text16.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.Text16.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.FromArgb(255, 191, 191, 191), 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text16.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.FromArgb(255, 242, 242, 242));
            this.Text16.Font = Stimulsoft.Base.StiFontCollection.CreateFont("Yekan Bakh FaNum Light", 10F, System.Drawing.FontStyle.Bold);
            this.Text16.Indicator = null;
            this.Text16.Interaction = null;
            this.Text16.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Text16.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text16.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(true, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text17
            // 
            this.Text17 = new Stimulsoft.Report.Components.StiText();
            this.Text17.CanGrow = true;
            this.Text17.CanShrink = true;
            this.Text17.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 0, 1.2, 0.3);
            this.Text17.GrowToHeight = true;
            this.Text17.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Center;
            this.Text17.Name = "Text17";
            this.Text17.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text17__GetValue);
            this.Text17.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.Text17.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.FromArgb(255, 191, 191, 191), 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text17.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.FromArgb(255, 242, 242, 242));
            this.Text17.Font = Stimulsoft.Base.StiFontCollection.CreateFont("Yekan Bakh FaNum Light", 10F, System.Drawing.FontStyle.Bold);
            this.Text17.Indicator = null;
            this.Text17.Interaction = null;
            this.Text17.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Text17.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text17.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(true, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text28
            // 
            this.Text28 = new Stimulsoft.Report.Components.StiText();
            this.Text28.CanGrow = true;
            this.Text28.CanShrink = true;
            this.Text28.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(2.4, 0, 1.1, 0.3);
            this.Text28.GrowToHeight = true;
            this.Text28.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Center;
            this.Text28.Name = "Text28";
            this.Text28.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text28__GetValue);
            this.Text28.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.Text28.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.FromArgb(255, 191, 191, 191), 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text28.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.FromArgb(255, 242, 242, 242));
            this.Text28.Font = Stimulsoft.Base.StiFontCollection.CreateFont("Yekan Bakh FaNum Light", 10F, System.Drawing.FontStyle.Bold);
            this.Text28.Indicator = null;
            this.Text28.Interaction = null;
            this.Text28.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Text28.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text28.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(true, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            this.ColumnHeaderBand1.Interaction = null;
            // 
            // GroupHeaderBand1
            // 
            this.GroupHeaderBand1 = new Stimulsoft.Report.Components.StiGroupHeaderBand();
            this.GroupHeaderBand1.CanBreak = true;
            this.GroupHeaderBand1.CanShrink = true;
            this.GroupHeaderBand1.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 2.6, 7.87, 0.3);
            this.GroupHeaderBand1.GetValue += new Stimulsoft.Report.Events.StiValueEventHandler(this.GroupHeaderBand1__GetValue);
            this.GroupHeaderBand1.KeepGroupHeaderTogether = false;
            this.GroupHeaderBand1.Name = "GroupHeaderBand1";
            this.GroupHeaderBand1.PrintOnAllPages = true;
            this.GroupHeaderBand1.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.GroupHeaderBand1.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            // 
            // Text20
            // 
            this.Text20 = new Stimulsoft.Report.Components.StiText();
            this.Text20.CanGrow = true;
            this.Text20.CanShrink = true;
            this.Text20.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(3.5, 0, 3.9, 0.3);
            this.Text20.GrowToHeight = true;
            this.Text20.Name = "Text20";
            this.Text20.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text20__GetValue);
            this.Text20.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Text20.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.Text20.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.Right, System.Drawing.Color.FromArgb(255, 216, 216, 216), 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text20.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Text20.Font = Stimulsoft.Base.StiFontCollection.CreateFont("Yekan Bakh FaNum Light", 9F, System.Drawing.FontStyle.Bold);
            this.Text20.Indicator = null;
            this.Text20.Interaction = null;
            this.Text20.Margins = new Stimulsoft.Report.Components.StiMargins(0, 10, 10, 0);
            this.Text20.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text20.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(true, false, true, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            this.Text20.BeforePrint += new System.EventHandler(this.Text20_BeforePrint);
            // 
            // Text21
            // 
            this.Text21 = new Stimulsoft.Report.Components.StiText();
            this.Text21.CanGrow = true;
            this.Text21.CanShrink = true;
            this.Text21.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(1.2, 0, 1.2, 0.3);
            this.Text21.GrowToHeight = true;
            this.Text21.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Right;
            this.Text21.Name = "Text21";
            // 
            // Text21_SumI
            // 
            this.Text21.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text21__GetValue);
            this.Text21.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Text21.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.Text21.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.Right, System.Drawing.Color.FromArgb(255, 216, 216, 216), 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text21.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Text21.Font = Stimulsoft.Base.StiFontCollection.CreateFont("Yekan Bakh FaNum Light", 9F, System.Drawing.FontStyle.Bold);
            this.Text21.Indicator = null;
            this.Text21.Interaction = null;
            this.Text21.Margins = new Stimulsoft.Report.Components.StiMargins(8, 0, 10, 0);
            this.Text21.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text21.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiNumberFormatService(1, ".", 0, ",", 3, true, false, " ");
            this.Text21.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(true, false, true, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text22
            // 
            this.Text22 = new Stimulsoft.Report.Components.StiText();
            this.Text22.CanGrow = true;
            this.Text22.CanShrink = true;
            this.Text22.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 0, 1.2, 0.3);
            this.Text22.GrowToHeight = true;
            this.Text22.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Right;
            this.Text22.Name = "Text22";
            // 
            // Text22_SumI
            // 
            this.Text22.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text22__GetValue);
            this.Text22.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Text22.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.Text22.Border = new Stimulsoft.Base.Drawing.StiBorder((Stimulsoft.Base.Drawing.StiBorderSides.Left | Stimulsoft.Base.Drawing.StiBorderSides.Right), System.Drawing.Color.FromArgb(255, 216, 216, 216), 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text22.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Text22.Font = Stimulsoft.Base.StiFontCollection.CreateFont("Yekan Bakh FaNum Light", 9F, System.Drawing.FontStyle.Bold);
            this.Text22.Indicator = null;
            this.Text22.Interaction = null;
            this.Text22.Margins = new Stimulsoft.Report.Components.StiMargins(8, 0, 10, 0);
            this.Text22.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text22.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiNumberFormatService(1, ".", 0, ",", 3, true, false, " ");
            this.Text22.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(true, false, true, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text33
            // 
            this.Text33 = new Stimulsoft.Report.Components.StiText();
            this.Text33.CanGrow = true;
            this.Text33.CanShrink = true;
            this.Text33.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(2.4, 0, 1.1, 0.3);
            this.Text33.GrowToHeight = true;
            this.Text33.Name = "Text33";
            this.Text33.Type = Stimulsoft.Report.Components.StiSystemTextType.DataColumn;
            this.Text33.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.Text33.Border = new Stimulsoft.Base.Drawing.StiBorder((Stimulsoft.Base.Drawing.StiBorderSides.Left | Stimulsoft.Base.Drawing.StiBorderSides.Right), System.Drawing.Color.FromArgb(255, 216, 216, 216), 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text33.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Text33.Font = Stimulsoft.Base.StiFontCollection.CreateFont("Yekan Bakh FaNum Light", 10F, System.Drawing.FontStyle.Regular);
            this.Text33.Indicator = null;
            this.Text33.Interaction = null;
            this.Text33.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Text33.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text33.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(true, false, true, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text32
            // 
            this.Text32 = new Stimulsoft.Report.Components.StiText();
            this.Text32.CanGrow = true;
            this.Text32.CanShrink = true;
            this.Text32.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(7.4, 0, 0.5, 0.3);
            this.Text32.GrowToHeight = true;
            this.Text32.Name = "Text32";
            this.Text32.Type = Stimulsoft.Report.Components.StiSystemTextType.DataColumn;
            this.Text32.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.Text32.Border = new Stimulsoft.Base.Drawing.StiBorder((Stimulsoft.Base.Drawing.StiBorderSides.Left | Stimulsoft.Base.Drawing.StiBorderSides.Right), System.Drawing.Color.FromArgb(255, 216, 216, 216), 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text32.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Text32.Font = Stimulsoft.Base.StiFontCollection.CreateFont("Yekan Bakh FaNum Light", 10F, System.Drawing.FontStyle.Regular);
            this.Text32.Indicator = null;
            this.Text32.Interaction = null;
            this.Text32.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Text32.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text32.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(true, false, true, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            this.GroupHeaderBand1.Interaction = null;
            // 
            // DataBand1
            // 
            this.DataBand1 = new Stimulsoft.Report.Components.StiDataBand();
            this.DataBand1.CanBreak = true;
            this.DataBand1.CanShrink = true;
            this.DataBand1.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 3.3, 7.87, 0.2);
            this.DataBand1.DataSourceName = "articles";
            this.DataBand1.Name = "DataBand1";
            this.DataBand1.Sort = new string[0];
            this.DataBand1.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.DataBand1.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.DataBand1.BusinessObjectGuid = null;
            // 
            // Text23
            // 
            this.Text23 = new Stimulsoft.Report.Components.StiText();
            this.Text23.CanGrow = true;
            this.Text23.CanShrink = true;
            this.Text23.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(3.5, 0, 3.9, 0.2);
            this.Text23.GrowToHeight = true;
            this.Text23.Name = "Text23";
            this.Text23.ShrinkFontToFit = true;
            this.Text23.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text23__GetValue);
            this.Text23.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Text23.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.Text23.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text23.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Text23.Font = Stimulsoft.Base.StiFontCollection.CreateFont("Yekan Bakh FaNum Light", 10F, System.Drawing.FontStyle.Regular);
            this.Text23.Indicator = null;
            this.Text23.Interaction = null;
            this.Text23.Margins = new Stimulsoft.Report.Components.StiMargins(0, 10, 0, 0);
            this.Text23.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text23.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(true, false, true, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            this.Text23.BeforePrint += new System.EventHandler(this.Text23_BeforePrint);
            // 
            // Text29
            // 
            this.Text29 = new Stimulsoft.Report.Components.StiText();
            this.Text29.CanGrow = true;
            this.Text29.CanShrink = true;
            this.Text29.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(2.4, 0, 1.1, 0.2);
            this.Text29.GrowToHeight = true;
            this.Text29.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Right;
            this.Text29.Name = "Text29";
            this.Text29.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text29__GetValue);
            this.Text29.Type = Stimulsoft.Report.Components.StiSystemTextType.DataColumn;
            this.Text29.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.Text29.Border = new Stimulsoft.Base.Drawing.StiBorder((Stimulsoft.Base.Drawing.StiBorderSides.Left | Stimulsoft.Base.Drawing.StiBorderSides.Right), System.Drawing.Color.FromArgb(255, 216, 216, 216), 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text29.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Text29.Font = Stimulsoft.Base.StiFontCollection.CreateFont("Yekan Bakh FaNum Light", 9F, System.Drawing.FontStyle.Regular);
            this.Text29.Indicator = null;
            this.Text29.Interaction = null;
            this.Text29.Margins = new Stimulsoft.Report.Components.StiMargins(8, 0, 0, 0);
            this.Text29.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text29.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiNumberFormatService(1, ".", 0, ",", 3, true, false, " ");
            this.Text29.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(true, false, true, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text30
            // 
            this.Text30 = new Stimulsoft.Report.Components.StiText();
            this.Text30.CanGrow = true;
            this.Text30.CanShrink = true;
            this.Text30.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(1.2, 0, 1.2, 0.2);
            this.Text30.GrowToHeight = true;
            this.Text30.Name = "Text30";
            this.Text30.Type = Stimulsoft.Report.Components.StiSystemTextType.DataColumn;
            this.Text30.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.Text30.Border = new Stimulsoft.Base.Drawing.StiBorder((Stimulsoft.Base.Drawing.StiBorderSides.Left | Stimulsoft.Base.Drawing.StiBorderSides.Right), System.Drawing.Color.FromArgb(255, 216, 216, 216), 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text30.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Text30.Font = Stimulsoft.Base.StiFontCollection.CreateFont("Yekan Bakh FaNum Light", 9F, System.Drawing.FontStyle.Regular);
            this.Text30.Indicator = null;
            this.Text30.Interaction = null;
            this.Text30.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Text30.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text30.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(true, false, true, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text31
            // 
            this.Text31 = new Stimulsoft.Report.Components.StiText();
            this.Text31.CanGrow = true;
            this.Text31.CanShrink = true;
            this.Text31.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 0, 1.2, 0.2);
            this.Text31.GrowToHeight = true;
            this.Text31.Name = "Text31";
            this.Text31.Type = Stimulsoft.Report.Components.StiSystemTextType.DataColumn;
            this.Text31.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.Text31.Border = new Stimulsoft.Base.Drawing.StiBorder((Stimulsoft.Base.Drawing.StiBorderSides.Left | Stimulsoft.Base.Drawing.StiBorderSides.Right), System.Drawing.Color.FromArgb(255, 216, 216, 216), 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text31.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Text31.Font = Stimulsoft.Base.StiFontCollection.CreateFont("Yekan Bakh FaNum Light", 9F, System.Drawing.FontStyle.Regular);
            this.Text31.Indicator = null;
            this.Text31.Interaction = null;
            this.Text31.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Text31.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text31.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(true, false, true, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text18
            // 
            this.Text18 = new Stimulsoft.Report.Components.StiText();
            this.Text18.CanBreak = true;
            this.Text18.CanGrow = true;
            this.Text18.CanShrink = true;
            this.Text18.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(7.4, 0, 0.5, 0.2);
            this.Text18.GrowToHeight = true;
            this.Text18.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Center;
            this.Text18.Name = "Text18";
            this.Text18.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text18__GetValue);
            this.Text18.Type = Stimulsoft.Report.Components.StiSystemTextType.SystemVariables;
            this.Text18.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.Text18.Border = new Stimulsoft.Base.Drawing.StiBorder((Stimulsoft.Base.Drawing.StiBorderSides.Left | Stimulsoft.Base.Drawing.StiBorderSides.Right), System.Drawing.Color.FromArgb(255, 216, 216, 216), 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text18.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Text18.Font = Stimulsoft.Base.StiFontCollection.CreateFont("Yekan Bakh FaNum Light", 10F, System.Drawing.FontStyle.Regular);
            this.Text18.Indicator = null;
            this.Text18.Interaction = null;
            this.Text18.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Text18.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text18.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(true, false, true, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            this.DataBand1.DataRelationName = null;
            this.DataBand1.Interaction = null;
            // 
            // ColumnFooterBand1
            // 
            this.ColumnFooterBand1 = new Stimulsoft.Report.Components.StiColumnFooterBand();
            this.ColumnFooterBand1.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 3.9, 7.87, 0.3);
            this.ColumnFooterBand1.Name = "ColumnFooterBand1";
            this.ColumnFooterBand1.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.ColumnFooterBand1.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            // 
            // Text25
            // 
            this.Text25 = new Stimulsoft.Report.Components.StiText();
            this.Text25.CanBreak = true;
            this.Text25.CanGrow = true;
            this.Text25.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(1.2, 0, 1.2, 0.3);
            this.Text25.GrowToHeight = true;
            this.Text25.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Center;
            this.Text25.Name = "Text25";
            // 
            // Text25_Sum
            // 
            this.Text25.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text25__GetValue);
            this.Text25.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Text25.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.Text25.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.FromArgb(255, 216, 216, 216), 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text25.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.FromArgb(255, 242, 242, 242));
            this.Text25.Font = Stimulsoft.Base.StiFontCollection.CreateFont("Yekan Bakh FaNum Light", 9F, System.Drawing.FontStyle.Bold);
            this.Text25.Indicator = null;
            this.Text25.Interaction = null;
            this.Text25.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Text25.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text25.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiNumberFormatService(1, ".", 0, ",", 3, true, false, " ");
            this.Text25.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(true, false, true, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text26
            // 
            this.Text26 = new Stimulsoft.Report.Components.StiText();
            this.Text26.CanBreak = true;
            this.Text26.CanGrow = true;
            this.Text26.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 0, 1.2, 0.3);
            this.Text26.GrowToHeight = true;
            this.Text26.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Center;
            this.Text26.Name = "Text26";
            // 
            // Text26_Sum
            // 
            this.Text26.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text26__GetValue);
            this.Text26.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Text26.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.Text26.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.FromArgb(255, 216, 216, 216), 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text26.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.FromArgb(255, 242, 242, 242));
            this.Text26.Font = Stimulsoft.Base.StiFontCollection.CreateFont("Yekan Bakh FaNum Light", 9F, System.Drawing.FontStyle.Bold);
            this.Text26.Indicator = null;
            this.Text26.Interaction = null;
            this.Text26.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Text26.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text26.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiNumberFormatService(1, ".", 0, ",", 3, true, false, " ");
            this.Text26.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(true, false, true, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text27
            // 
            this.Text27 = new Stimulsoft.Report.Components.StiText();
            this.Text27.CanBreak = true;
            this.Text27.CanGrow = true;
            this.Text27.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(2.4, 0, 5.5, 0.3);
            this.Text27.GrowToHeight = true;
            this.Text27.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Center;
            this.Text27.Name = "Text27";
            this.Text27.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text27__GetValue);
            this.Text27.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Text27.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.Text27.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.FromArgb(255, 216, 216, 216), 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text27.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.FromArgb(255, 242, 242, 242));
            this.Text27.Font = Stimulsoft.Base.StiFontCollection.CreateFont("Yekan Bakh FaNum Light", 10F, System.Drawing.FontStyle.Regular);
            this.Text27.Indicator = null;
            this.Text27.Interaction = null;
            this.Text27.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Text27.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text27.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(true, false, true, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            this.ColumnFooterBand1.Interaction = null;
            // 
            // ReportSummaryBand1
            // 
            this.ReportSummaryBand1 = new Stimulsoft.Report.Components.StiReportSummaryBand();
            this.ReportSummaryBand1.CanShrink = true;
            this.ReportSummaryBand1.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 4.6, 7.87, 1.1);
            this.ReportSummaryBand1.Name = "ReportSummaryBand1";
            this.ReportSummaryBand1.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.FromArgb(255, 216, 216, 216), 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.ReportSummaryBand1.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            // 
            // Text35
            // 
            this.Text35 = new Stimulsoft.Report.Components.StiText();
            this.Text35.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(4.9, 0.5, 2.1, 0.2);
            this.Text35.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Center;
            this.Text35.Name = "Text35";
            this.Text35.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text35__GetValue);
            this.Text35.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.Text35.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text35.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Text35.Font = Stimulsoft.Base.StiFontCollection.CreateFont("Yekan Bakh FaNum Light", 10F, System.Drawing.FontStyle.Bold);
            this.Text35.Indicator = null;
            this.Text35.Interaction = null;
            this.Text35.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Text35.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text35.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text36
            // 
            this.Text36 = new Stimulsoft.Report.Components.StiText();
            this.Text36.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0.7, 0.5, 2.1, 0.2);
            this.Text36.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Center;
            this.Text36.Name = "Text36";
            this.Text36.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text36__GetValue);
            this.Text36.VertAlignment = Stimulsoft.Base.Drawing.StiVertAlignment.Center;
            this.Text36.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text36.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Text36.Font = Stimulsoft.Base.StiFontCollection.CreateFont("Yekan Bakh FaNum Light", 10F, System.Drawing.FontStyle.Bold);
            this.Text36.Indicator = null;
            this.Text36.Interaction = null;
            this.Text36.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Text36.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text36.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            this.ReportSummaryBand1.Interaction = null;
            this.Page1.Interaction = null;
            this.Page1.Margins = new Stimulsoft.Report.Components.StiMargins(0.2, 0.2, 0.2, 0.2);
            this.Page1.Report = this;
            this.PageHeaderBand1.Page = this.Page1;
            this.PageHeaderBand1.Parent = this.Page1;
            this.Text1.Page = this.Page1;
            this.Text1.Parent = this.PageHeaderBand1;
            this.Text2.Page = this.Page1;
            this.Text2.Parent = this.PageHeaderBand1;
            this.Text3.Page = this.Page1;
            this.Text3.Parent = this.PageHeaderBand1;
            this.Text4.Page = this.Page1;
            this.Text4.Parent = this.PageHeaderBand1;
            this.Text5.Page = this.Page1;
            this.Text5.Parent = this.PageHeaderBand1;
            this.Text6.Page = this.Page1;
            this.Text6.Parent = this.PageHeaderBand1;
            this.Text7.Page = this.Page1;
            this.Text7.Parent = this.PageHeaderBand1;
            this.Text8.Page = this.Page1;
            this.Text8.Parent = this.PageHeaderBand1;
            this.Text9.Page = this.Page1;
            this.Text9.Parent = this.PageHeaderBand1;
            this.Text10.Page = this.Page1;
            this.Text10.Parent = this.PageHeaderBand1;
            this.Text11.Page = this.Page1;
            this.Text11.Parent = this.PageHeaderBand1;
            this.Text12.Page = this.Page1;
            this.Text12.Parent = this.PageHeaderBand1;
            this.PageFooterBand1.Page = this.Page1;
            this.PageFooterBand1.Parent = this.Page1;
            this.ColumnHeaderBand1.Page = this.Page1;
            this.ColumnHeaderBand1.Parent = this.Page1;
            this.Text13.Page = this.Page1;
            this.Text13.Parent = this.ColumnHeaderBand1;
            this.Text15.Page = this.Page1;
            this.Text15.Parent = this.ColumnHeaderBand1;
            this.Text16.Page = this.Page1;
            this.Text16.Parent = this.ColumnHeaderBand1;
            this.Text17.Page = this.Page1;
            this.Text17.Parent = this.ColumnHeaderBand1;
            this.Text28.Page = this.Page1;
            this.Text28.Parent = this.ColumnHeaderBand1;
            this.GroupHeaderBand1.Page = this.Page1;
            this.GroupHeaderBand1.Parent = this.Page1;
            this.Text20.Page = this.Page1;
            this.Text20.Parent = this.GroupHeaderBand1;
            this.Text21.Page = this.Page1;
            this.Text21.Parent = this.GroupHeaderBand1;
            this.Text22.Page = this.Page1;
            this.Text22.Parent = this.GroupHeaderBand1;
            this.Text33.Page = this.Page1;
            this.Text33.Parent = this.GroupHeaderBand1;
            this.Text32.Page = this.Page1;
            this.Text32.Parent = this.GroupHeaderBand1;
            this.DataBand1.Page = this.Page1;
            this.DataBand1.Parent = this.Page1;
            this.Text23.Page = this.Page1;
            this.Text23.Parent = this.DataBand1;
            this.Text29.Page = this.Page1;
            this.Text29.Parent = this.DataBand1;
            this.Text30.Page = this.Page1;
            this.Text30.Parent = this.DataBand1;
            this.Text31.Page = this.Page1;
            this.Text31.Parent = this.DataBand1;
            this.Text18.Page = this.Page1;
            this.Text18.Parent = this.DataBand1;
            this.ColumnFooterBand1.Page = this.Page1;
            this.ColumnFooterBand1.Parent = this.Page1;
            this.Text25.Page = this.Page1;
            this.Text25.Parent = this.ColumnFooterBand1;
            this.Text26.Page = this.Page1;
            this.Text26.Parent = this.ColumnFooterBand1;
            this.Text27.Page = this.Page1;
            this.Text27.Parent = this.ColumnFooterBand1;
            this.ReportSummaryBand1.Page = this.Page1;
            this.ReportSummaryBand1.Parent = this.Page1;
            this.Text35.Page = this.Page1;
            this.Text35.Parent = this.ReportSummaryBand1;
            this.Text36.Page = this.Page1;
            this.Text36.Parent = this.ReportSummaryBand1;
            this.GroupHeaderBand1.BeginRender += new System.EventHandler(this.GroupHeaderBand1__BeginRender);
            this.DataBand1.BeginRender += new System.EventHandler(this.DataBand1__BeginRender);
            this.GroupHeaderBand1.EndRender += new System.EventHandler(this.GroupHeaderBand1__EndRender);
            this.DataBand1.EndRender += new System.EventHandler(this.DataBand1__EndRender);
            this.GroupHeaderBand1.Rendering += new System.EventHandler(this.GroupHeaderBand1__Rendering);
            this.DataBand1.Rendering += new System.EventHandler(this.DataBand1__Rendering);
            this.EndRender += new System.EventHandler(this.ReportWordsToEnd__EndRender);
            this.AggregateFunctions = new object[] {
                    this.Text21_SumI,
                    this.Text22_SumI,
                    this.Text25_Sum,
                    this.Text26_Sum};
            this.EndRender += new System.EventHandler(this.CheckEndRenderRuntimes__EndRender);
            // 
            // Add to PageHeaderBand1.Components
            // 
            this.PageHeaderBand1.Components.Clear();
            this.PageHeaderBand1.Components.AddRange(new Stimulsoft.Report.Components.StiComponent[] {
                        this.Text1,
                        this.Text2,
                        this.Text3,
                        this.Text4,
                        this.Text5,
                        this.Text6,
                        this.Text7,
                        this.Text8,
                        this.Text9,
                        this.Text10,
                        this.Text11,
                        this.Text12});
            // 
            // Add to ColumnHeaderBand1.Components
            // 
            this.ColumnHeaderBand1.Components.Clear();
            this.ColumnHeaderBand1.Components.AddRange(new Stimulsoft.Report.Components.StiComponent[] {
                        this.Text13,
                        this.Text15,
                        this.Text16,
                        this.Text17,
                        this.Text28});
            // 
            // Add to GroupHeaderBand1.Components
            // 
            this.GroupHeaderBand1.Components.Clear();
            this.GroupHeaderBand1.Components.AddRange(new Stimulsoft.Report.Components.StiComponent[] {
                        this.Text20,
                        this.Text21,
                        this.Text22,
                        this.Text33,
                        this.Text32});
            // 
            // Add to DataBand1.Components
            // 
            this.DataBand1.Components.Clear();
            this.DataBand1.Components.AddRange(new Stimulsoft.Report.Components.StiComponent[] {
                        this.Text23,
                        this.Text29,
                        this.Text30,
                        this.Text31,
                        this.Text18});
            // 
            // Add to ColumnFooterBand1.Components
            // 
            this.ColumnFooterBand1.Components.Clear();
            this.ColumnFooterBand1.Components.AddRange(new Stimulsoft.Report.Components.StiComponent[] {
                        this.Text25,
                        this.Text26,
                        this.Text27});
            // 
            // Add to ReportSummaryBand1.Components
            // 
            this.ReportSummaryBand1.Components.Clear();
            this.ReportSummaryBand1.Components.AddRange(new Stimulsoft.Report.Components.StiComponent[] {
                        this.Text35,
                        this.Text36});
            // 
            // Add to Page1.Components
            // 
            this.Page1.Components.Clear();
            this.Page1.Components.AddRange(new Stimulsoft.Report.Components.StiComponent[] {
                        this.PageHeaderBand1,
                        this.PageFooterBand1,
                        this.ColumnHeaderBand1,
                        this.GroupHeaderBand1,
                        this.DataBand1,
                        this.ColumnFooterBand1,
                        this.ReportSummaryBand1});
            // 
            // Add to Pages
            // 
            this.Pages.Clear();
            this.Pages.AddRange(new Stimulsoft.Report.Components.StiPage[] {
                        this.Page1});
            this.header.Columns.AddRange(new Stimulsoft.Report.Dictionary.StiDataColumn[] {
                        new Stimulsoft.Report.Dictionary.StiDataColumn("FinancePeriodName", "FinancePeriodName", "FinancePeriodName", typeof(string), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("DocDate_Sh", "DocDate_Sh", "DocDate_Sh", typeof(string), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("DocNumber", "DocNumber", "DocNumber", typeof(int), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("DocAtf", "DocAtf", "DocAtf", typeof(int), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("DocAutoNumber", "DocAutoNumber", "DocAutoNumber", typeof(int), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("Description", "Description", "Description", typeof(string), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("strTotal", "strTotal", "strTotal", typeof(string), null)});
            this.DataSources.Add(this.header);
            this.articles.Columns.AddRange(new Stimulsoft.Report.Dictionary.StiDataColumn[] {
                        new Stimulsoft.Report.Dictionary.StiDataColumn("MoeinCode", "MoeinCode", "MoeinCode", typeof(string), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("MoeinName", "MoeinName", "MoeinName", typeof(string), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("Tafsil4", "Tafsil4", "Tafsil4", typeof(string), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("Tafsil5", "Tafsil5", "Tafsil5", typeof(string), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("strAmount", "strAmount", "strAmount", typeof(string), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("Comment", "Comment", "Comment", typeof(string), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("Amount", "Amount", "Amount", typeof(long), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("Bed", "Bed", "Bed", typeof(long), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("Bes", "Bes", "Bes", typeof(long), null),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("Nature", "Nature", "Nature", typeof(int), null)});
            this.DataSources.Add(this.articles);
        }
        
        #region DataSource header
        public class headerDataSource : Stimulsoft.Report.Dictionary.StiBusinessObjectSource
        {
            
            public headerDataSource() : 
                    base("header", "header", "header", "e8aa92c5c6964cd382f44130b4b18957")
            {
            }
            
            public virtual string FinancePeriodName
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["FinancePeriodName"], typeof(string), true)));
                }
            }
            
            public virtual string DocDate_Sh
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["DocDate_Sh"], typeof(string), true)));
                }
            }
            
            public virtual int DocNumber
            {
                get
                {
                    return ((int)(StiReport.ChangeType(this["DocNumber"], typeof(int), true)));
                }
            }
            
            public virtual int DocAtf
            {
                get
                {
                    return ((int)(StiReport.ChangeType(this["DocAtf"], typeof(int), true)));
                }
            }
            
            public virtual int DocAutoNumber
            {
                get
                {
                    return ((int)(StiReport.ChangeType(this["DocAutoNumber"], typeof(int), true)));
                }
            }
            
            public virtual string Description
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["Description"], typeof(string), true)));
                }
            }
            
            public virtual string strTotal
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["strTotal"], typeof(string), true)));
                }
            }
        }
        #endregion DataSource header
        
        #region DataSource articles
        public class articlesDataSource : Stimulsoft.Report.Dictionary.StiBusinessObjectSource
        {
            
            public articlesDataSource() : 
                    base("articles", "articles", "articles", "5852112f94bf4323b3c37c68d9b7880e")
            {
            }
            
            public virtual string MoeinCode
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["MoeinCode"], typeof(string), true)));
                }
            }
            
            public virtual string MoeinName
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["MoeinName"], typeof(string), true)));
                }
            }
            
            public virtual string Tafsil4
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["Tafsil4"], typeof(string), true)));
                }
            }
            
            public virtual string Tafsil5
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["Tafsil5"], typeof(string), true)));
                }
            }
            
            public virtual string strAmount
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["strAmount"], typeof(string), true)));
                }
            }
            
            public virtual string Comment
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["Comment"], typeof(string), true)));
                }
            }
            
            public virtual long Amount
            {
                get
                {
                    return ((long)(StiReport.ChangeType(this["Amount"], typeof(long), true)));
                }
            }
            
            public virtual long Bed
            {
                get
                {
                    return ((long)(StiReport.ChangeType(this["Bed"], typeof(long), true)));
                }
            }
            
            public virtual long Bes
            {
                get
                {
                    return ((long)(StiReport.ChangeType(this["Bes"], typeof(long), true)));
                }
            }
            
            public virtual int Nature
            {
                get
                {
                    return ((int)(StiReport.ChangeType(this["Nature"], typeof(int), true)));
                }
            }
        }
        #endregion DataSource articles
        #endregion StiReport Designer generated code - do not modify
    }
}
