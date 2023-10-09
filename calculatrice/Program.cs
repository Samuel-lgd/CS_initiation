using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

Console.WriteLine("Quel est votre calcul ?");
string calcul = Console.ReadLine();

List<Operator> operators = parseCalcul(calcul);

string sum = operators[0].Value;
for (int j = 0; j < operators.Count; j++)
{
    var op = operators[j];
    if (op.Type == OperatorsEnum.OPERATOR)
    {
        if (j + 1 < operators.Count)
        {
            int int1 = Convert.ToInt32(sum);
            int int2 = Convert.ToInt32(operators[j + 1].Value);

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

Console.WriteLine(sum);

static List<Operator> parseCalcul(string line)
{
    List<Operator> operators = new List<Operator>();
    string partialNumber = "";
    int i = 0;

    // on parcours la string pour y extraire les valeurs des opérateurs
    foreach (var character in line.ToCharArray())
    {
        string c = Convert.ToString(character);
        bool isNumber = int.TryParse(c, out int number);
        if (isNumber) // cas ou c'est un nombre
        {
            partialNumber += c;
        }
        else if (c == "*" || c == "+" || c == "-" || c == "/") //cas ou c'est un opérateur
        {
            if (partialNumber.Length > 0)
            {
                operators.Add(new Operator(partialNumber, OperatorsEnum.NUMBER));
                partialNumber = "";
            }
            if (operators.Count <= 0 || operators.Last().Type == OperatorsEnum.OPERATOR)
            {
                Console.WriteLine("Erreur de syntaxe");
            }
            operators.Add(new Operator(c, OperatorsEnum.OPERATOR));
        }
        i++;
    }

    // après avoir parcouru toute la liste, on ajoute alors le dernier nombre
    if (partialNumber.Length > 0)
    {
        operators.Add(new Operator(partialNumber, OperatorsEnum.NUMBER));
    }

    // si justement ce n'est pas un nombre alors y'a une erreur
    if (operators.Last().Type == OperatorsEnum.OPERATOR)
    {
        Console.WriteLine("Erreur de syntaxe");
    }

    return operators;
}

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

