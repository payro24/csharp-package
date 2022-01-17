# csharp-package

payro24 payment gateway package for c# (ASP.NET)

مستندات کامل برای پیاده سازی درگاه پرداخت پیرو در لینک [اینجا](https://lab.payro24.ir/) وجود دارد.

در این قطعه کد یک کلاس بنام PayroIPG نوشته شده است که میتوانید آن را بصورت کامل در پروژه خود کپی کنید و یک کنترلر برای نحوه استفاده از این کلاس بنام HomeController نوشته شده است.

جهت ایجاد تراکنش پرداخت همانطور که در فایل HomeController.cs در پوشه Controllers وجود دارد، از قطعه کد زیر استفاده کنید:

```bash
Payro.PayroIPG payro = new Payro.PayroIPG(p_token, p_sandbox);
Payro.PayroCreated? payroCreatedResponse = payro.payment(callback_url,order_id,amount,name,email,mobile,description);
```

و برای تایید تراکنش هنگامی که کاربر از درگاه اینترنتی باز میگردد میتوانید از قطعه کد زیر استفاده نمایید:

```bash
Payro.PayroIPG payro = new Payro.PayroIPG(p_token, p_sandbox);
Payro.PayroPaymentInfo? payroVerifyResponse = payro.verify(transaction_id, order_id);
```
