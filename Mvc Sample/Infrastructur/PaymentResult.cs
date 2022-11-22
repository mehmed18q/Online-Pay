namespace Mvc_Sample.Infrastructur
{
    public class PaymentResult
    {
        #region نمایش پیغام های نتیجه پرداخت سامان
        public static string Saman(string resultId)
        {
            string result = "";
            switch (resultId)
            {
                case "OK":
                    result = "پرداخت با موفقیت انجام شده";
                    break;
                case "-100":
                    result = "پرداخت کنسل شده";
                    break;
                case "CanceledByUser":
                case "":
                    result = "تراكنش توسط خريدار كنسل شد";
                    break;
                case "InvalidAmount":
                    result = "مبلغ سند برگشتی، از مبلغ تراکنش اصلی بیشتر است";
                    break;
                case "InvalidTransaction":
                    result = "درخواست برگشت یک تراکنش رسیده است ، درحالی که تراکنش اصلی پیدا نمی شود";
                    break;
                case "InvalidCardNumber":
                    result = "شماره کارت اشتباه است";
                    break;
                case "NoSuchIssuer":
                    result = "چنین صادر کننده کارتی وجود ندارد";
                    break;
                case "ExpiredCardPickUp":
                    result = "از تاریخ انقضای کارت گذشته است و کارت دیگر معتبر نیست";
                    break;
                case "AllowablePINTriesExceededPickUp":
                    result = "رمز کارت 3 مرتبه اشتباه وارد شده است و در نتیجه کارت غیر فعال خواهد شد";
                    break;
                case "IncorrectPIN":
                    result = "خریدار رمز کارت را اشتباه وارد کرده است";
                    break;
                case "ExceedsWithdrawalAmountLimit":
                    result = "مبلغ بیش از سقف برداشت می باشد";
                    break;
                case "TransactionCannotBeCompleted":
                    result = "تراکنش Authorize شده است (شماره PIN و PAN درست هست) ولی امکان سند خوردن وجود ندارد";
                    break;
                case "ResponseReceivedTooLate":
                    result = "تراکنش در شبکه بانکی Timeout خورده است";
                    break;
                case "Suspected Fraud Pick Up":
                    result = "خریدار یا فیلد CVV2 و یا فیلد ExpDate را اشتباه وارد کرده است (یا اصلا وارد نکرده است)";
                    break;
                case "NoSufficientFunds":
                    result = "موجودی حساب خریدار، کافی نیست";
                    break;
                case "IssuerDownSlm":
                    result = "سیستم بانک صادر کننده کارت خریدار، در وضعیت عملیاتی نیست";
                    break;
                case "TMEError":
                    result = "کلیه خطاهای دیگر بانک باعث ایجاد چنین خطایی می گردد";
                    break;
                /*********************************************************/
                case "-1":
                    result = "خطای در پردازش اطلاعات ارسالی - مشکل در یکی از ورودی ها و ناموفق بودنفراخوانی متد برگشت تراکنش";
                    break;
                case "-3":
                    result = "ورودی ها حاوی کارکترهای غیرمجاز می باشند.";
                    break;
                case "-4":
                    result = "Merchant Authentication Failed  کلمه عبور یا کد فروشنده اشتباه است";
                    break;
                case "-6":
                    result = "سند قبلا برگشت کامل یافته است";
                    break;
                case "-7":
                    result = "رسید دیجیتالی تهی است";
                    break;
                case "-8":
                    result = "طو ورودی ها بیشتر از حد مجاز است";
                    break;
                case "-9":
                    result = "وجود کارکترهای غیرمجاز در مبلخ برگشتی";
                    break;
                case "-10":
                    result = "رسید دیجیتالی به صورت Base64 نیست)حاوی کاراکترهای غیرمجاز است)";
                    break;
                case "-11":
                    result = "طو ورودی ها کمتر از حد مجاز است";
                    break;
                case "-12":
                    result = "مبلخ برگشتی منفی است";
                    break;
                case "-13":
                    result = "مبلخ برگشتی برای برگشت جزئی بیش از مبلخ برگشت نخورده ی رسید دیجیتالی است";
                    break;
                case "-14":
                    result = "چنین تراکنشی تعریف نشده است";
                    break;
                case "-15":
                    result = "مبلخ برگشتی به صورت اعشاری داده شده است";
                    break;
                case "-16":
                    result = "خطای داخلی سیستم";
                    break;
                case "-17":
                    result = "برگشت زدن جزیی تراکنش مجاز نمی باشد";
                    break;
                case "-18":
                    result = "IP Address  فروشنده نا معتبر است";
                    break;
                default:
                    result = "خطای نا مشخص";
                    break;

            }
            return result;
        }

        #endregion
    }
}