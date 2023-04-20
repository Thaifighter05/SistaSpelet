

using System.Collections.Generic;
//En generisk samling är starkt typad så att vi kan eliminera körtidstypsfel,den förbättrar prestandan genom att undvika boxning och unboxing.
using System;
//Tillhandahåller grundläggande klasser och basklasser som definierar vanliga värde- och referensdatatyper, händelse- och händelsehanterare, gränssnitt, attribut och undantag.



public class Game
{
    int[,] map = new int[4, 4]; // Detta är en 2D-array som används för att representera spelplanen. Varje cell i arrayen motsvarar en bricka på spelplanen. Heltalsvärdet i varje cell representerar värdet på brickan. Måtten på arrayen är 4 x 4 för att matcha storleken på spelplanen.
    int yDir = 0; //En heltalsvariabel som initierar den till 0 vilket är rörelseriktningen i y-axeln.
    int xDir = 0; //En heltalsvariabel som initierar den till 0 vilket är rörelseriktningen i x-axeln.
    Dictionary<int, ConsoleColor> colors = new Dictionary<int, ConsoleColor>(); // En ordbok som används för att associera en färg med varje brickvärde.Värdena är ConsoleColor-värden som representerar brickans färg.
    public static void Main(string[] args)
    {
        new Game();
    }
    public void PrintMap()
    {
        Console.Clear();
        ConsoleColor defualt = Console.BackgroundColor;
        int startX = (Console.WindowWidth - 4 * 5) / 2; // 4 är storleken på kartan, 5 är avståndet mellan elementen
        int startY = (Console.WindowHeight - 4) / 2;

        for (int y = 0; y < 4; y++)
        {
            Console.SetCursorPosition(startX, startY + y);
            for (int x = 0; x < 4; x++)
            {
                Console.BackgroundColor = colors[map[x, y]];
                Console.Write(Spacing(map[x, y] + "", "2048"));
            }
        }
        Console.BackgroundColor = defualt;
    }
    public Game()
    {
        colors.Add(0, ConsoleColor.DarkGreen); //Lägger till en färg på respektive nummer
        colors.Add(2, ConsoleColor.DarkGray);
        colors.Add(4, ConsoleColor.Black);
        colors.Add(8, ConsoleColor.DarkBlue);
        colors.Add(16, ConsoleColor.Blue);
        colors.Add(32, ConsoleColor.DarkRed);
        colors.Add(64, ConsoleColor.Red);
        colors.Add(128, ConsoleColor.DarkMagenta);
        colors.Add(256, ConsoleColor.Magenta);
        colors.Add(512, ConsoleColor.DarkCyan);
        colors.Add(1024, ConsoleColor.Cyan);
        colors.Add(2048, ConsoleColor.Yellow);
        Console.Clear();
        AddNew();
        while (true)
        {
            PrintMap();
            ReadKey();
            UpdateMap();
            if (Enden())
            { //Enden-metoden kontrollerar om det finns några möjliga drag kvar genom att simulera alla möjliga drag och kontrollera om något av dem resulterar i en annan spelplan. Om det inte finns några möjliga drag kvar är spelet över.
                PrintMap();
                if (Won())
                {
                    Console.WriteLine("You won!");
                }
                else
                {
                    Console.WriteLine("You Lost");
                }
            }
            if (Won())
            {
                PrintMap();
                Console.WriteLine("You won!");

            }
            AddNew();
        }
    }
    public void ReadKey()
    {
        ConsoleKeyInfo key = Console.ReadKey();
        if (key.Key == ConsoleKey.W || key.Key == ConsoleKey.UpArrow)
        {
            yDir = -1;
        }
        else
        if (key.Key == ConsoleKey.S || key.Key == ConsoleKey.DownArrow)
        {
            yDir = 1;
        }
        else
        if (key.Key == ConsoleKey.A || key.Key == ConsoleKey.LeftArrow)
        {
            xDir = -1;
        }
        else
        if (key.Key == ConsoleKey.D || key.Key == ConsoleKey.RightArrow)
        {
            xDir = 1;
        }
        else
        if (key.Key == ConsoleKey.R)
        {
            map = new int[4, 4];
            AddNew();
        }
        //Kontroller för hur du styr och kör A eller vänster pil flyttar du vänster, D eller högerpil flyttar du höger, W eller uppåt pil flyttar du uppåt, S eller neråt pil flyttar du neråt och en reset funktion genom "R"
    }
    public void AddNew()
    {
        int fullSpaces = 0;
        for (int y = 0; y < 4; y++)
        {
            for (int x = 0; x < 4; x++)
            {
                if (map[x, y] != 0) fullSpaces++;
            }
        }
        if (fullSpaces != 16)
        {
            int x = (int)rnd(3);
            int y = (int)rnd(3);
            while (map[x, y] != 0)
            {
                x = (int)rnd(3);
                y = (int)rnd(3);
            }
            map[x, y] = (((int)rnd(4)) == 1 ? 4 : 2);
            //Denna kod lägger till en ny slumpmässig bricka till spelplanen. Den kontrollerar först om det finns några tomma platser kvar på tavlan. Om det finns, genererar den två slumpmässiga tal för att representera raden och kolumnen i den nya brickan. Den fortsätter sedan att generera nya slumpmässiga tal tills den hittar ett tomt utrymme. Slutligen tilldelar den slumpmässigt ett värde på antingen 2 eller 4 till den nya brickan.
        }
    }
    public bool Enden()
    {
        int posibleMoves = 0;
        for (int j = 0; j < 4; j++)
        {
            int xDir = 0;
            int yDir = 0;
            switch (j)
            {
                case 0:
                    yDir = 1;
                    break;
                case 1:
                    yDir = -1;
                    break;
                case 2:
                    xDir = 1;
                    break;
                default:
                    xDir = -1;
                    break;
            }
            int i = 0;
            int iMax = ((xDir == -1 || yDir == -1) ? 4 : 1);
            while (i < iMax)
            {
                for (int x = 0; x < 4; x++)
                {
                    for (int y = 0; y < 4; y++)
                    {
                        try
                        {
                            if (map[x + xDir, y + yDir] == 0)
                            {
                                posibleMoves++;
                            }
                            else if (map[x + xDir, y + yDir] == map[x, y])
                            {
                                posibleMoves++;
                            }
                            else
                            {

                            }
                        }
                        catch (Exception e) { } //undantagshanteringskod, satser som anger vad som ska göras om ett undantag kastas i try -blocket. Om någon sats inom try -blocket (eller i en funktion som anropas från try -blocket) ger ett undantag, flyttas kontrollen omedelbart till catch -blocket.
                    }
                }
                i++;
            }
        }
        return !(posibleMoves > 0);
        // kollar igenom alla möjliga drag (upp, ner, vänster, höger) och kontrollera för varje position på spelplanen om det finns ett tomt utrymme intill den eller en bricka med samma värde bredvid den. Om det finns några sådana mellanslag, returnerar metoden falskt (vilket betyder att spelet inte har avslutats ännu), annars returnerar det sant (vilket betyder att det inte finns några möjliga drag kvar och spelet har avslutats).
    }
    public bool Won()
    {
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                if (map[x, y] == 2048) return true;
            }
        }
        return false;
        // iterera genom varje element i ett 4x4-rutnät och kontrollera om någon av dem är lika med 2048. Om den hittar en bricka med värdet 2048, så blir det "sant" , vilket indikerar att spelaren har vunnit.
    }
    public void UpdateMap()
    {

        int i = 0;
        int iMax = ((xDir == -1 || yDir == -1) ? 4 : 1);
        while (i < iMax)
        {
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    try
                    {
                        if (map[x + xDir, y + yDir] == 0)
                        {
                            map[x + xDir, y + yDir] = map[x, y];
                            map[x, y] = 0;
                        }
                        else if (map[x + xDir, y + yDir] == map[x, y])
                        {
                            map[x + xDir, y + yDir] *= 2;
                            map[x, y] = 0;
                        }
                        else
                        {

                        }
                    }
                    catch (Exception e) { } //undantagshanteringskod, satser som anger vad som ska göras om ett undantag kastas i try -blocket. Om någon sats inom try -blocket (eller i en funktion som anropas från try -blocket) ger ett undantag, flyttas kontrollen omedelbart till catch -blocket.
                }
            }
            i++;
        }
        xDir = 0;
        yDir = 0;
        //Metoden går igenom kartan 4 gånger, en gång för varje rad eller kolumn. Inom varje slinga, itererar den genom brickorna i den raden/kolumnen och utför en av dessa tre åtgärder, om brickan är tom (värdet 0), försöker den flytta brickan till nästa icke-tomma bricka i angiven riktning.
        //Om nästa icke-tomma bricka i den angivna riktningen har samma värde som den aktuella brickan, slår den samman de två brickorna genom att dubbla värdet på nästa bricka och sätta värdet på den aktuella brickan till 0.
        //Om nästa icke-tomma bricka har ett annat värde gör den ingenting.
        //När alla iterationer är klara, återställs xDir- och yDir-variablerna till 0.
    }

   
    public static double rnd(int max)
    {
        return new Random().Next(max + 1);
    // För att generera slumpmässiga tal skapar metoden en ny instans av klassen Random och anropar dess next-metod med argumentet max+1, vilket skickar tillbaka ett icke-negativt slumpmässigt heltal som är mindre än det angivna maxvärdet.
    }
    public static string Spacing(string inData, string Max)
    {
        int spacing = Max.Length - inData.Length;
        string spacingString = "";
        for (int i = 0; i < spacing; i++)
        {
            spacingString += " ";
        }
        return "[" + inData + spacingString + "]";
        //Tar in två strängparametrar inData och Max. Den beräknar skillnaden mellan längden på Max och inData, skapar en strängspacingString som består av så många mellanslag och sammanfogar sedan inData och spacingString inom hakparenteser []. Den resulterande strängen returneras sedan.
    }
}


