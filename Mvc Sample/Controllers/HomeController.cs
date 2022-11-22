using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using System.Web;
using Mvc_Sample.Helpers;
using System.Web.Mvc;
using Mvc_Sample.Models;

namespace Mvc_Sample.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(payment ent)
        {
            string terminal = ent.txtTid;
            string amount = ent.txtAmount;
            string resnum = ent.txtResnum;
            string url = ent.txtUrl;

            //int paymentId = InsertPayment(price);

            //if (paymentId > 0)

            NameValueCollection datacollection = new NameValueCollection();

            datacollection.Add("ResNum", resnum);//paymentId را برای پیدا کردن تراکنش از درون دیتابیس استفاده می نماییم

            datacollection.Add("MID", terminal);

            datacollection.Add("RedirectURL", url);

            datacollection.Add("Amount", amount);

            Response.Write(HttpHelper.PreparePOSTForm("https://sep.shaparak.ir/payment.aspx", datacollection));

            return View();
        }

        #region ثبت اطلاعات اولیه پرداخت در دیتابیس

        //private int InsertPayment(long price)
        //{
        //    int paymentId = 0;
        //    try
        //    {
       // در صورتی که اطلاعات سبد خرید نیز درون کوکی و یا هیدن فیلد وجود دارد قبل از ثبت مبلغ نهایی نیز باید با دیتابیس چک شود
        //        var payment = new Payment();

        //        // قیمت پرداخت
        //        payment.Amount = price;

        //        // شماره پیگیری را در ثبت اولیه 0 ثبت می کنیم
        //        payment.SaleReferenceId = 0;

        //        // فقط در صورتی که این فید ترو باشد پرداخت موفق بوده است
        //        payment.PaymentFinished = false;

        //        // آی دی کاربر درحال پرداخت که ما یک در نظر گرفتیم و شما باید آی دی کاربری که پرداخت را انجام می دهد ثبت کنید
        //        payment.UserId = 1;

        //        // ثبت اطلاعات در دیتابیس
        //        db.Payments.Add(payment);
        //        db.SaveChanges();

        //        // شماره پرداخت که همان آی دی جدول می باشد که به بانک ارسال می کنیم و هنگام بازگشت اطلاعات را از دیتابیس پیدا می کنیم
        //        paymentId = payment.PaymentId;

        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //    return paymentId;
        //}
        #endregion

        #region پیدا کردن مبلغ خرید
        //private long FindAmountPayment(long paymentId)
        //{
        //if(Session["UserId"]!=null){
        //    try 
        //{	        
		    //    long amount = db.Payments.Where(c => c.PaymentId == paymentId && c.UserId=Session["UserId"]).Select(c => c.Amount).FirstOrDefault();
            //    return amount;
        //}
        //catch (Exception)
        //{
        //    return -1;
        //}
        
         //}
       // else{
       //      throw new Exception("User Session Expire");
       // }
        //}
        #endregion

        #region متد ویرایش پرداخت

        //private void UpdatePayment(long paymentId, string vresult, long saleReferenceId, string refId, bool paymentFinished = false)
        //{

        //    var payment = db.Payments.Find(paymentId);

        //    if (payment != null)
        //    {
        //        payment.StatusPayment = vresult;
        //        payment.SaleReferenceId = saleReferenceId;
        //        payment.PaymentFinished = paymentFinished;

        //        if (refId != null)
        //        {
        //            payment.ReferenceNumber = refId;
        //        }

        //        db.Entry(payment).State = EntityState.Modified;
        //        db.SaveChanges();
        //    }
        //    else
        //    {
        //        // اطلاعاتی از دیتابیس پیدا نشد
        //    }
        //}
        #endregion

        public ActionResult Return()
        {
            ViewBag.BankName = "درگاه بانک سامان";
            try
            {
                //Request.Form["StateCode"] این فیلد مقدار عددی وضعیت تراکنش را بر می گرداند

                // بررسی وجود استیت ارسالی از درگاه
                // در صورت عدم وجود خطا را نمایش می دهیم
                if (Request.Form["state"].ToString().Equals(string.Empty))
                {
                    //ViewBag.Message = "خريد شما توسط بانک تاييد شده است اما رسيد ديجيتالي شما تاييد نگشت! مشکلي در فرايند رزرو خريد شما پيش آمده است";
                    ViewBag.Message = "پاسخی از درگاه بانکی دریافت نشد";
                    ViewBag.SaleReferenceId = "**************";
                }

                // بررسی وجود رف نام ارسالی از درگاه
                // در صورت عدم وجود خطا را نمایش می دهیم
                else if (Request.Form["RefNum"].ToString().Equals(string.Empty) && Request.Form["state"].ToString().Equals(string.Empty))
                {
                    ViewBag.Message = "فرايند انتقال وجه با موفقيت انجام شده است اما فرايند تاييد رسيد ديجيتالي با خطا مواجه گشت";
                    ViewBag.SaleReferenceId = "**************";
                }

                // بررسی وجود رس نام ارسالی از درگاه
                // در صورت عدم وجود خطا را نمایش می دهیم
                else if (Request.Form["ResNum"].ToString().Equals(string.Empty) && Request.Form["state"].ToString().Equals(string.Empty))
                {
                    ViewBag.Message = "خطا در برقرار ارتباط با بانک";
                    ViewBag.SaleReferenceId = "**************";
                }
                else
                {
                    // تغییر های مورد تعریف شده برای قرار دادن اطلاعات دریافتی از درگاه
                    string refrenceNumber = string.Empty;
                    string reservationNumber = string.Empty;
                    string transactionState = string.Empty;
                    string traceNumber = string.Empty;


                    // کد سفارش که به صورت عدد و حروف می باشد
                    refrenceNumber = Request.Form["RefNum"].ToString();

                    // کد ارسالی از طرف سایت که شماره آی دی همان اطلاعات ثبتی در هنگام اتصال به درگاه
                    reservationNumber = Request.Form["ResNum"].ToString();

                    // وضعیت پرداخت
                    transactionState = Request.Form["state"].ToString();

                    // شماره پیگیری
                    traceNumber = Request.Form["TraceNo"].ToString();
                    //آی دی که به ازای هر resnum برای مشتری در دیتابیس ذخیره شده است
                    //long paymentId = Convert.ToInt64(reservationNumber);

                    if (transactionState.Equals("OK"))
                    {
                        ///////////////////////////////////////////////////////////////////////////////////
                        //   *** IMPORTANT  ****   ATTENTION
                        // Here you should check refrenceNumber in your DataBase tp prevent double spending
                        ///////////////////////////////////////////////////////////////////////////////////

                        ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

                        var srv = new SepVerify.PaymentIFBinding();

                        var result = srv.verifyTransaction(Request.Form["RefNum"], Request.Form["MID"]);

                        if (result > 0)
                        {
                            //// پیدا کردن مبلغ پرداختی از درگاه
                            //long amount = FindAmountPayment(paymentId);

                            //// چک کردن مبلغ بازگشتی از سرویس با مبلغ تراکنش
                            //if ((long)result == amount)
                            //{

                            //    // در اینجا خرید موفق بوده و اطلاعات دریافتی از درگاه را در دیتابیس ذخیره می کنیم
                            //    UpdatePayment(paymentId, transactionState, Convert.ToInt64(traceNumber), refrenceNumber, true);
                            //    }
                            //else
                            //{
                            ////نام کاربری همان ام آی دی است
                            //string userName = Request.Form["MID"];

                            //// رمز عبور برای شما توسط سامان کیش ایمیل شده است
                            //string pass = "123456789";

                            //// فراخوانی متد ریورس تراکشن برای بازگشت دادن مبلغ به حساب خریدار
                            //srv.reverseTransaction(Request.Form["RefNum"], Request.Form["MID"], userName, pass);

                            //// پرداخت ناموفق بوده و اطلاعات دریافتی را در دیتابیس ثبت می کنیم
                            //UpdatePayment(paymentId, transactionState, 0, refrenceNumber, false);

                            //ViewBag.message = Infrast.PaymentResult.Saman(transactionState);
                            //ViewBag.SaleReferenceId = "**************";
                            //}
                            ViewBag.Message = "پرداخت با موفقیت انجام شد.";
                            ViewBag.SaleReferenceId = traceNumber;
                            ViewBag.RefrenceNumber = refrenceNumber;
                        }
                        else
                        {
                            //// پرداخت ناموفق بوده و اطلاعات دریافتی را در دیتابیس ثبت می کنیم
                            //UpdatePayment(paymentId, result.ToString(), 0, refrenceNumber, false);

                            ViewBag.message = Infrastructur.PaymentResult.Saman(transactionState);
                            ViewBag.SaleReferenceId = "**************";
                        }
                    }
                    else
                    {
                        //// پرداخت ناموفق بوده و اطلاعات دریافتی را در دیتابیس ثبت می کنیم
                        //UpdatePayment(paymentId, transactionState, 0, refrenceNumber, false);

                        if (!String.IsNullOrEmpty(Infrastructur.PaymentResult.Saman(transactionState)))
                        {
                            ViewBag.Message = Infrastructur.PaymentResult.Saman(transactionState);
                        }
                        else
                        {
                            ViewBag.Message = "متاسفانه بانک خريد شما را تاييد نکرده است";
                        }
                        ViewBag.SaleReferenceId = "**************";

                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.SaleReferenceId = "**************";
                ViewBag.Message = "مشکلی در پرداخت به وجود آمده است ، در صورتیکه وجه پرداختی از حساب بانکی شما کسر شده است آن مبلغ به صورت خودکار برگشت داده خواهد شد";
            }

            return View();
        }

        [HttpGet]
        public ActionResult Token()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Token(payment ent)
        {
            string terminal = ent.txtTid;
            string amount = ent.txtAmount;
            string resnum = ent.txtResnum;
            string url = ent.txtUrl;

            NameValueCollection datacollection = new NameValueCollection();

            var initPayment = new Sep.PaymentIFBinding();

            string token = initPayment.RequestToken(ent.txtTid, ent.txtResnum, long.Parse(ent.txtAmount), 0, 0, 0, 0, 0, 0, "", "", 0);


            if (!String.IsNullOrEmpty(token))
            {
                datacollection.Add("Token", token);

                datacollection.Add("RedirectURL", url);

                Response.Write(HttpHelper.PreparePOSTForm("https://sep.shaparak.ir/payment.aspx", datacollection));

            }

            return View();
        }

    }

}
