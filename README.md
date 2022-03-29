# BackOffice
 
PROJE AMACI
Ürünlerin ve Kullanıcıların yönetilebilmesi amacıyla bir backoffice projesi olarak tasarlanmıştır.

PROJE DETAYI
Proje çalıştılırıp incelenirken kolaylık olması adına Db Connection(MongoDb,Redis)'ların static kalması adına uzak sunucudan erişilebilir olarak ayarladım. 
 Projeyi run etmek için herhangi bir configuration ayarı yapılmasına gerek yoktur.

Admin rolü dışındaki rollere sahip kullanıcıların için yetki kısıtlamaları vardır.
Admin Panelinde hesaplar ve ürünler ile ilgili düzenleme yapabilmek için admin olmak gerektiğinden ve yeni üye olan kullanıcıların kayıt olurken admin rolünü seçemeyeceği için 
veritabanında kayıtlı bir admin hesabı mevcuttur.

email: serdarkaya@inveon.com
password: 123456

KULLANILAN TEKNOLOJİLER
•	.NET Core MVC
•	Redis
•	Mongo DB
•	Authentication(Kimlik Kontrolü)
•	Authorization(Yetkilendirme)	•	AutoFac
•	AutoMapper  
•	Action Filter

