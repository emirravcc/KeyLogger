# ⌨️ Keyboard Event Logger (C# WinForms)

Bu proje, C# programlama dili ve Windows API (User32.dll) kullanılarak klavye olaylarının (keyboard hooks) nasıl yakalandığını ve bir dosyaya nasıl kaydedildiğini gösteren bir **eğitim projesidir.**

## ⚠️ Önemli Uyarı
Bu yazılım tamamen **akademik ve eğitim amaçlı** geliştirilmiştir. Yazılımın amacı, işletim sistemi seviyesindeki klavye dinleme mekanizmalarını anlamaktır. Kötü niyetli kullanım geliştiricinin sorumluluğunda değildir.

## 🚀 Projenin Özellikleri
* **Düşük Seviyeli Hooking:** `SetWindowsHookEx` API'si ile sistem genelindeki tuş vuruşları yakalanır.
* **Akıllı Loglama:** Sadece harfler değil; `Enter`, `Space` ve `Backspace` gibi özel tuşlar da okunaklı formatta kaydedilir.
* **Dosya Kaydı:** Veriler projenin yürütülebilir klasöründeki `log.txt` dosyasına otomatik olarak eklenir.
* **Dinamik Kaynak Yönetimi:** Program kapatıldığında `UnhookWindowsHookEx` ile sistem kaynakları güvenli bir şekilde serbest bırakılır.

## 🛠️ Kullanılan Teknolojiler
* **Dil:** C#
* **Platform:** .NET Framework / Windows Forms
* **API:** Win32 (user32.dll, kernel32.dll)

## 🎮 Nasıl Çalıştırılır?
1. Visual Studio ile `.sln` dosyasını açın.
2. Projeyi `Build` edin ve başlatın.
3. Herhangi bir pencerede tuşlara basın.
4. `bin/Debug` klasörü altındaki `log.txt` dosyasından kayıtları kontrol edin.

## 📁 Geliştirici
* **Emir**
