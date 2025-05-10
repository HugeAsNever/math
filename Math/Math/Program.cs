using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Text = DocumentFormat.OpenXml.Wordprocessing.Text;

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
    var dateString = DateTime.Now.Date.ToString("yyyy-MM-dd");
    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    string fileName = Path.Combine(desktopPath, $"math-{dateString}.docx");

    using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(fileName, DocumentFormat.OpenXml.WordprocessingDocumentType.Document))
    {
        // Add a main document part
        MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
        mainPart.Document = new Document();
        Body body = new Body();

        // Add the date
        body.Append(new Paragraph(new Run(new Text(dateString))));
        body.Append(new Paragraph(new Run(new Text("")))); // Empty line

        // Add questions
        foreach (var question in questions)
        {
            body.Append(new Paragraph(new Run(new Text(question))));
        }

        // Add a placeholder for the mark
        body.Append(new Paragraph(new Run(new Text("")))); // Empty line
        body.Append(new Paragraph(new Run(new Text("")))); // Empty line
        body.Append(new Paragraph(new Run(new Text("Mark: ___________"))));

        // Finalize the document
        mainPart.Document.Append(body);
        mainPart.Document.Save();
    }
}

