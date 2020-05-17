using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineModUtil.UtilAPI
{
    public class Util
    {
        public static void CreateJSON(string path, List<string> Data)
        {
            if (Directory.Exists(path) && Data[2] != "" && Data[3] != "")
            {
                Console.WriteLine("Fetching template");

                List<string> files = FileManager.GetFiles(@"Templates\", "*.json");
                path = path + @"\" + Data[2] + ".json";

                try
                {
                    foreach (string i in files)
                    {
                        if (i.Contains(Data[3]))
                        {
                            files.Clear();
                            files.Add(i);
                            break;
                        }
                    }

                    Output.Success("Fetched template " + files[0]);
                }
                catch (Exception error) { Output.FatalError(error, "fetch template"); }

                Console.WriteLine("Preparing file...");

                File.Copy(files[0], path, true);
                WriteLines(path, ReadLines(path, Data));

                Output.Success("Succesfully created " + path + "!");
                Output.Notify("Succesfully created " + path + "!", "Succes!");
            } else
            {
                Output.Error("Not enough data!");
            }

            return;
        }

        public static List<string> ReadLines(string path, List<string> Data)
        {
            Console.WriteLine("Scanning file...");
            List<string> list = new List<string>();

            if (Directory.Exists(path))
            {
                try
                {
                    using (StreamReader reader = new StreamReader(path))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            try { line = line.Replace("{directory}", Data[0]); } catch { Output.Warning("No item was found for directory."); }
                            try { line = line.Replace("{folder:sub}", Data[1]); } catch { Output.Warning("No item was found for folder:subfolder."); }
                            try { line = line.Replace("{item_name}", Data[2]); } catch { Output.Warning("No item was found for item name."); }
                            try { line = line.Replace("{item_type}", Data[3]); } catch { Output.Warning("No item was found for category."); }
                            list.Add(line);
                        }
                        reader.Close();
                    }
                    Output.Success("Succesfully scanned file!");
                }
                catch (Exception error) { Output.FatalError(error, "read file"); }
            } else
            {
                Output.Error("Directory " + path + " does not exist!");
            }

            return list;
        }

        public static void WriteLines(string path, List<string> Data)
        {
            Console.WriteLine("Writing to file...");

            if (Directory.Exists(path))
            {
                try
                {
                    using (StreamWriter file = new StreamWriter(path))
                    {
                        foreach (string line in Data)
                        {
                            try { file.WriteLine(line); } catch { Output.Warning("No item was found to write line."); }
                        }
                    }
                    Output.Success("Succesfully written to file!");
                }
                catch (Exception error) { Output.FatalError(error, "write to file"); }
            } else
            {
                Output.Error("Directory " + path + " does not exist!");
            }

            return;
        }
    }
}