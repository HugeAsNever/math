// See https://aka.ms/new-console-template for more information
using System.Xml.Linq;

Console.WriteLine("Enter the max value of addend/minuend (eg. 20): ");
int maxNumber = int.Parse(Console.ReadLine()) + 1;
Console.WriteLine("Enter number of questions: ");
int questionQuantity = int.Parse(Console.ReadLine());
Random rm = new Random();
int i = 0;
List<string> questions = new List<string>();
List<string> operations = new List<string>() { "-", "x", "÷" };
int minNumber = 0;
int mulNumber = 0;
int divNumber = 0;
while (i < questionQuantity)
{
    string operation = operations[rm.Next(operations.Count)];
    int number1 = rm.Next(1, maxNumber);
    int number2;
    
    if (operation.Equals("-"))
    {
        number2 = rm.Next(1, number1);
    }
    else if (operation.Equals("+"))
    {
        number2 = rm.Next(1, maxNumber);
    }
    else if (operation.Equals("÷"))
    {
        number2 = rm.Next(1, 11);
    }
    else
    {
        number1 = rm.Next(10, maxNumber);
        number2 = rm.Next(2, 30);
    }
    string question = $"{number1} {operation} {number2} =";
    if(!questions.Contains(question))
    {
        if (operation == "-" && minNumber < 3)
        {
            minNumber++;
            questions.Add(question);
            i++;
        }

        else if(operation == "x" && mulNumber < 4)
        {
            mulNumber++;
            questions.Add(question);
            i++;
        }

        else if (operation == "÷" && divNumber < 3)
        {
            divNumber++;
            questions.Add(question);
            i++;
        }
    }

    
}

SaveToFile(ref questions);


void SaveToFile(ref List<string> questions)
{
    var dateString = DateTime.Now.Date.ToString("d");
    string fileName = $"math-{dateString}.doc";
    using (StreamWriter writer = new StreamWriter(fileName))
    {
        writer.WriteLine(dateString);
        writer.WriteLine();
        writer.WriteLine();
        for (int idx = 0; idx < questions.Count; idx++)
        {
            writer.WriteLine(questions[idx]);
            writer.WriteLine();
        }
        writer.WriteLine();
        writer.WriteLine("Mark: ___________");
    }
}

