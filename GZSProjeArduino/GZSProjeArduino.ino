#include <LiquidCrystal.h>  //lcd kütüphanemizi ekledik

LiquidCrystal lcd(8, 9, 4, 5, 6, 7);    //lcdnin bağlı olduğu pinleri belirttik

int i=0;

int trigPin = 12; // Sensorun trig pini Arduinonun 6 numaralı ayağına bağlandı
int echoPin = 11; // Sensorun echo pini Arduinonun 7 numaralı ayağına bağlandı

long sure;
long uzaklik;

String gelenMesajUzun;

String gelenMesaj;

void setup() {
  lcd.begin(16, 2);               // LCD Kütüphanesi başlatılıyor
  lcd.setCursor(0, 0); 
  pinMode(trigPin, OUTPUT); // trig pini çıkış olarak ayarlandı 
  pinMode(echoPin, INPUT); // echo pini giriş olarak ayarlandı 
  Serial.begin(9600); /* Seri haberlesme baslatildi */
}
void loop()
{
  digitalWrite(trigPin, LOW); //sensör pasif hale getirildi
  delayMicroseconds(5);  //Delay atadık
  digitalWrite(trigPin, HIGH); //Sensore ses dalgasının üretmesi için emir verildi
  delayMicroseconds(10);   //Delay atadık
  digitalWrite(trigPin, LOW);  //Yeni dalgaların üretilmemesi için trig pini LOW konumuna getirildi
  sure = pulseIn(echoPin, HIGH); /* ses dalgasının geri dönmesi için geçen sure ölçülüyor */
  uzaklik = sure / 29.1 / 2; /* ölçülen sure uzaklığa çevriliyor */
  
  Serial.print(uzaklik);  // hesaplanan uzaklık bilgisayara aktarılıyor

  gelenMesaj=Serial.readString();  //gelen metni readString metodu ile char ile uğraşmadan direk aldık.

  if(Serial.available()>0)  //Eğer seri porttan veri geldiyse
  {
    i++;   //ilk defa veri gelmesi durumu
  }
  if(i>1) //ilk defa veri gelmemesi durumu
  {
    lcd.clear(); //daha önceden veri gelmiş ise lcdyi temizliyoruz. 
  }
  if(gelenMesaj.length()>16)//lgelen metin 16 karakterden fazla olup olmadığının kontrolu
  {
    lcd.print(gelenMesaj);    
    lcd.setCursor(1,0);    //faza ise bir alt satıra geç
    gelenMesajUzun= gelenMesaj.substring(16);   //kaldığı yerden devam et
    lcd.print(gelenMesajUzun);
  
  }
  else{
  lcd.print(gelenMesaj); //16 karakterden kısa ise normal olarak bastır
  }
  delay(500);  //1 sn delay
  }
  

  
 


