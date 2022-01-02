# csharp-package

payro24 payment gateway package for c# (ASP.NET)

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
