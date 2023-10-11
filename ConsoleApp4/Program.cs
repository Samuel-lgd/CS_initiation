using System;
using System.Collections.Generic;

class BillCalculator
{
    public List<int> Bills { get; set; }

    public BillCalculator()
    {
        Bills = new List<int>();
    }

    public int ParseEntry(string entry)
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

    public void AddBills()
    {
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
            Bills.Add(billValue);
        }
    }

    public double CalculateAnnualTotalBrut()
    {
        double annualTotalBrut = 0;
        foreach (var bill in Bills)
        {
            annualTotalBrut += bill;
        }
        return annualTotalBrut;
    }

    public string CheckCeiling(double annualTotalBrut, double plafond)
    {
        if (annualTotalBrut > plafond)
        {
            return "Plafond dépassé de " + Convert.ToString(annualTotalBrut - plafond);
        }
        return "Plafond OK";
    }

    public void CalculateAndDisplaySalary()
    {
        double annualTotalBrut = CalculateAnnualTotalBrut();
        double plafond = 36800;
        double netMultiplicator = 0.75;
        double annualTotalNet = annualTotalBrut * netMultiplicator;
        double monthlySalaryBrut = annualTotalBrut / 12;
        double monthlySalaryNet = monthlySalaryBrut * netMultiplicator;
        string ceiling = CheckCeiling(annualTotalBrut, plafond);

        Console.WriteLine(
            "\nFacture annuelle" +
            "\n brut: " + annualTotalBrut +
            "\n net: " + annualTotalNet +
            "\n\nFacture mensuelle" +
            "\n brut: " + monthlySalaryBrut +
            "\n net: " + monthlySalaryNet +
            "\n\n" + ceiling);
    }
}

class Program
{
    static void Main()
    {
        BillCalculator billCalculator = new BillCalculator();
        billCalculator.AddBills();
        billCalculator.CalculateAndDisplaySalary();
    }
}
