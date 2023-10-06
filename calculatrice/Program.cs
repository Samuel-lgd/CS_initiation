using System.Text.RegularExpressions;

Console.WriteLine("Quel est votre calcul ?");
string calcul = Console.ReadLine();

List<Operator> operators = new List<Operator>();

string OPERATOR = "OPERATOR";
string NUMBER = "NUMBER";

string partialNumber = "";
int i = 0;
foreach (var character in calcul.ToCharArray())
{
    string c = Convert.ToString(character);
    bool isNumber = int.TryParse(c, out int number);
    if (isNumber)
    {
        partialNumber += c;
    }
    else if (c == "*" || c == "+" || c == "-" || c == "/")
    {
        if (partialNumber.Length > 0)
        {
            operators.Add(new Operator(partialNumber, NUMBER));
            partialNumber = "";
        }
        if (operators.Count <= 0 || operators.Last().Type == OPERATOR)
        {
            Console.WriteLine("Erreur de syntaxe");
            return;
        }
        operators.Add(new Operator(c, OPERATOR));
    }
    i++;
}

if (partialNumber.Length > 0)
{
    operators.Add(new Operator(partialNumber, NUMBER));
}
if (operators.Last().Type == OPERATOR)
{
    Console.WriteLine("Erreur de syntaxe");
    return;
}

int sum = 0;

int j = 0;
foreach (var op in operators)
{
    if (op.Type == OPERATOR)
        {
            switch (op.Value)
            {
            case "+":
                string val1 = operators[j - 1].Value;
                string val2 = operators[j + 1].Value;
                operators[j + 1] = new Operator(addValues(val1, val2), NUMBER );
                break;
            }
        }
    j++;
}
static string addValues(string val1, string val2)
{
    int int1 = Convert.ToInt32(val1);
    int int2 = Convert.ToInt32(val2);

    int sum = int1 + int2;

    return Convert.ToString(sum);
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