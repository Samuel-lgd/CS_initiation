using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;


Calcul calcul1 = new Calcul();

Console.WriteLine("Quel est votre calcul ?");
calcul1.Value = Console.ReadLine();
calcul1.ParseCalcul();
Console.WriteLine(calcul1.Operate());

static class OperatorsEnum
{
    public static string NUMBER = "NUMBER";
    public static string OPERATOR = "OPERATOR";

}
class Operator
{
    public string Value { get; set; }
    public string Type { get; set; }

    public Operator(string value, string type)
    {
        this.Value = value;
        this.Type = type;
    }
}

class Calcul
{
    public string? Value { get; set; }

    public List<Operator>? Sequence { get; set; }

    public string Result { get; set; }

    public void ParseCalcul()
    {
        if (Value == null)
        {
            throw new Exception("Aucun calcul n'a été renseigné");
        }

        List<Operator> sequence = new List<Operator>();
        string partialNumber = "";
        int i = 0;

        // on parcours la string pour y extraire les valeurs des opérateurs
        foreach (var character in Value.ToCharArray())
        {
            string c = Convert.ToString(character);
            bool isNumber = int.TryParse(c, out int number);
            if (isNumber) // cas ou c'est un nombre
            {
                partialNumber += c;
            }
            else if (c == "*" || c == "+" || c == "-" || c == "/") // cas ou c'est un opérateur
            {
                if (partialNumber.Length > 0)
                {
                    sequence.Add(new Operator(partialNumber, OperatorsEnum.NUMBER));
                    partialNumber = "";
                }
                if (sequence.Count <= 0 || sequence.Last().Type == OperatorsEnum.OPERATOR)
                {
                    throw new ArgumentException("Erreur de syntaxe");
                }
                sequence.Add(new Operator(c, OperatorsEnum.OPERATOR));
            }
            else
            {
                throw new ArgumentException("Erreur de syntaxe");
            }
            i++;
        }

        // après avoir parcouru toute la liste, on ajoute alors le dernier nombre
        if (partialNumber.Length > 0)
        {
            sequence.Add(new Operator(partialNumber, OperatorsEnum.NUMBER));
        }

        // si justement ce n'est pas un nombre alors y'a une erreur
        if (sequence.Last().Type == OperatorsEnum.OPERATOR)
        {
            throw new ArgumentException("Erreur de syntaxe");
        }

        this.Sequence = sequence;
    }

    public string Operate()
    {
        if (Sequence == null || Sequence.Any())
        {
            throw new Exception("Aucun calcul n'a été renseigné");
        }

        string sum = Sequence[0].Value;
        for (int j = 0; j < Sequence.Count; j++)
        {
            var op = Sequence[j];
            if (op.Type == OperatorsEnum.OPERATOR)
            {
                if (j + 1 < Sequence.Count)
                {
                    int int1 = Convert.ToInt32(sum);
                    int int2 = Convert.ToInt32(Sequence[j + 1].Value);

                    switch (op.Value)
                    {
                        case "+":
                            sum = (int1 + int2).ToString();
                            break;

                        case "-":
                            sum = (int1 - int2).ToString();
                            break;

                        case "*":
                            sum = (int1 * int2).ToString();
                            break;

                        case "/":
                            sum = (int1 / int2).ToString();
                            break;
                    }
                    j++;
                }
            }
        }
        Result = sum;
        return sum;
    }
}
