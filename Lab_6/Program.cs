
using Lab_8;

var ctx1 = new LoanContext();
Console.WriteLine(InterestCalculatorEnum.CalculateRate(CreditBand.A, ctx1));

var ctx2 = new LoanContext { HasCollateral = true };
Console.WriteLine(InterestCalculatorEnum.CalculateRate(CreditBand.B, ctx2));

var ctx3 = new LoanContext { IsFirstTime = true };
Console.WriteLine(InterestCalculatorEnum.CalculateRate(CreditBand.C, ctx3));

var ctx4 = new LoanContext { HasPromo = true, HasCollateral = true };
Console.WriteLine(InterestCalculatorEnum.CalculateRate(CreditBand.D, ctx4));

var ctx5 = new LoanContext { HasCollateral = true, IsFirstTime = true, HasPromo = true };
Console.WriteLine(InterestCalculatorEnum.CalculateRate(CreditBand.GovGuaranteed, ctx5)); 

Console.WriteLine(InterestCalculatorOop.CalculateRate(new ABand(), ctx1)); 
Console.WriteLine(InterestCalculatorOop.CalculateRate(new GovGuaranteedBand(), ctx5));