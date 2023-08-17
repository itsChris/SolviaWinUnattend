using DiscUtils.Iso9660;

class Program
{
    static string isoFile = "SolviaAutoUnattend.iso";
    static string AutoUnattendXmlSource = "";
    static string AutoUnattendXmlDest = "autounattend.xml";
    static string readMeFile = "readme.txt";

    public static string IsoFileFullPath { get; } = CurrentDirectory() + @$"\{isoFile}";
    public static string ReadMeFileFullPath { get; } = CurrentDirectory() + @$"\{readMeFile}";

    static void Main()
    {
        // ask user for BIOS with MBR or UEFI with GPT with gpt.. (old ESX only supports BIOS -> MBR)
        Console.WriteLine("BIOS with MBR or UEFI with GPT?");
        Console.WriteLine("1. BIOS w/MBR");
        Console.WriteLine("2. UEFI w/GPT");
        Console.Write("Enter your choice (1 or 2): ");

        string input = Console.ReadLine();

        switch (input)
        {
            case "1":
                Console.WriteLine("You've chosen BIOS w/MBR");
                AutoUnattendXmlSource = "autounattend-BIOS-MBR.xml";
                break;
            case "2":
                Console.WriteLine("You've chosen UEFI w/PGT");
                AutoUnattendXmlSource = "autounattend-UEFI-GPT.xml";
                break;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }

        

        if (File.Exists(IsoFileFullPath))
        {
            Console.WriteLine($"File {IsoFileFullPath} exists. Do you want to (D)elete it, (R)ename it, or (E)xit the application? (D/R/E)");
            var choice = Console.ReadLine();

            switch (choice.ToUpper())
            {
                case "D":
                    try
                    {
                        File.Delete(IsoFileFullPath);
                        Console.WriteLine("File successfully deleted.");
                        CreateAutoUnattendIso();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error while deleting the file: {ex.Message}");
                    }
                    break;
                case "R":
                    try
                    {
                        string directory = Path.GetDirectoryName(IsoFileFullPath);
                        string extension = Path.GetExtension(IsoFileFullPath);
                        string fileName = Path.GetFileNameWithoutExtension(IsoFileFullPath);
                        string newFileName = $"{DateTime.Now:yyyyMMddHHmmss}_{fileName}{extension}";
                        string newFilePath = Path.Combine(directory, newFileName);

                        File.Move(IsoFileFullPath, newFilePath);
                        Console.WriteLine($"File successfully renamed to: {newFileName}");
                        CreateAutoUnattendIso();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error while renaming the file: {ex.Message}");
                    }
                    break;
                case "E":
                    Environment.Exit(-1);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Exiting...");
                    Environment.Exit(-1);
                    break;
            }
        }
        else
        {
            Console.WriteLine($"File {IsoFileFullPath} does not exist.");
            CreateAutoUnattendIso();
        }
        System.Diagnostics.Process.Start("explorer.exe", ".");
    }
    private static void CreateReadMe()
    {
        string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
        string[] content = {
            " _______  _______  _                _________ _______ ",
            "(  ____ \\(  ___  )( \\      |\\     /|\\__   __/(  ___  )",
            "| (    \\/| (   ) || (      | )   ( |   ) (   | (   ) |",
            "| (_____ | |   | || |      | |   | |   | |   | (___) |",
            "(_____  )| |   | || |      ( (   ) )   | |   |  ___  |",
            "      ) || |   | || |       \\ \\_/ /    | |   | (   ) |",
            "/\\____) || (___) || (____/\\  \\   /  ___) (___| )   ( |",
            "\\_______)(_______)(_______/   \\_/   \\_______/|/     \\|",
            Environment.NewLine,
            "Solution by Solvia",
            "https://www.solvia.ch", 
            "info@solvia.ch",
            $"Date: {currentDate} (YYYY-MM-DD)"
        };
        File.WriteAllLines(ReadMeFileFullPath, content);
    }
    private static void CreateAutoUnattendIso()
    {
        try
        {
            CreateReadMe();
            Console.WriteLine($"Trying to create ISO file: {IsoFileFullPath}");
            // create new ISO with the extracted files and additional XML file
            CDBuilder builder = new CDBuilder();
            builder.UseJoliet = true;

            Console.WriteLine($"Trying to add {AutoUnattendXmlSource} to {IsoFileFullPath}");
            // add additional XML file
            if (File.Exists(GetAutoUnattendFile()))
            {
                builder.AddFile(AutoUnattendXmlDest, GetAutoUnattendFile());
            }
            else
            {
                Console.WriteLine($"ERROR! -> File: {GetAutoUnattendFile()} does not exist!");
                Environment.Exit(-1);
            }
            if (File.Exists(ReadMeFileFullPath)) {
                builder.AddFile(readMeFile, ReadMeFileFullPath);
            }
            Console.WriteLine($"Trying to build/save ISO file {IsoFileFullPath}");
            // save the new ISO
            builder.Build(IsoFileFullPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occured \n{ex.Message}");
        }
    }
    private static string CurrentDirectory()
    {
        return Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
    }
    private static string GetAutoUnattendFile()
    {
        if (CurrentDirectory == null)
        {
            throw new Exception("Could not get the directory of the executable");
        }

        string filePath = Path.Combine(CurrentDirectory(), AutoUnattendXmlSource);

        if (!File.Exists(filePath))
        {
            throw new Exception($"File does not exist: {filePath}");
        }
        Console.WriteLine(File.ReadAllText(filePath));
        return filePath;
    }
}
