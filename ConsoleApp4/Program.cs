using System;
using System.Collections.Generic;

static int ParseEntry(string entry)
{
    if (int.TryParse(entry, out int v) && v > 0)
    {
        return v;
    }
    else
    {
        Console.WriteLine("Veuillez entrer un nombre supérieur à 0.");
        return -1;
    }
}

static List<int> AddBills()
{
    List<int> bills = new List<int>();

    int billsCount = -1;
    while (billsCount < 0)
    {
        Console.WriteLine("Combien voulez-vous rentrer de factures ?");
        billsCount = ParseEntry(Console.ReadLine());
    }

    for (int i = 0; i < billsCount; i++)
    {
        int billValue = -1;
        while (billValue < 0)
        {
            Console.WriteLine("Entrez le total TTC de votre facture n°" + (i + 1) + ":");
            billValue = ParseEntry(Console.ReadLine());
        }
        bills.Add(billValue);
    }
    return bills;
}

List<int> bills = new List<int>();
bills.AddRange(AddBills());

double annualTotalBrut = 0;
foreach (var bill in bills)
{
    annualTotalBrut += bill;
}

double plafond = 36800;
double netMultiplicator = 0.75;

double annualTotalNet = annualTotalBrut * netMultiplicator;
double monthlySalaryBrut = annualTotalBrut / 12;
double monthlySalaryNet = monthlySalaryBrut * netMultiplicator;
string ceiling = "Plafond OK";
if (annualTotalBrut > plafond)
{
    ceiling = "Plafond dépassé de " + Convert.ToString(annualTotalBrut - plafond);
}

Console.WriteLine(
    "\nFacture annuelle" +
    "\n brut: " + annualTotalBrut +
    "\n net: " + annualTotalNet +
    "\n\nFacture mensuelle" +
    "\n brut: " + monthlySalaryBrut +
    "\n net: " + monthlySalaryNet +
    "\n\n" + ceiling);
