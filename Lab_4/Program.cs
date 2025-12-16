
using Lab_4;

PaymentGateway term =  new PaymentGateway();
term.providerName = "test";
term.getInfo();
Thread.Sleep(3000);
term.switchSandbox();
term.getInfo();
Thread.Sleep(3000);

CardGateway card = new CardGateway();
card.providerName = "card";
card.maskedPan = "1232 1541 1254 5313";
card.getInfo();
Thread.Sleep(3000);
card.switchSandbox();
card.getInfo();
Thread.Sleep(3000);
card.maskedPan = "123215411255313";
card.getInfo();