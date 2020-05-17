using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineModUtil.UtilAPI
{
    public static class FileManager
    {
        public static string SelectImage()
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Title = "Select Texture",
                InitialDirectory = @"c:\",
                Filter = "Image files (*.jpg, *.png) | *.jpg; *.png",
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    return dialog.FileName;
                } catch (Exception error) { Output.FatalError(error, "select image"); }
            }

            return null;
        }

        public static string SelectFile(string Type = "*.txt")
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Title = "Select File",
                InitialDirectory = @"c:\",
                Filter = Type + " files (" + Type + ") | " + Type,
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    return dialog.FileName;
                }  catch (Exception error) { Output.FatalError(error, "select file"); }
            }

            return null;
        }

        public static string SelectFolder()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    return dialog.SelectedPath;
                }  catch (Exception error) { Output.FatalError(error, "select folder"); }
            }

            return null;
        }

        public static void SafeFile(List<string> Data, string type = ".txt")
        {
            SaveFileDialog dialog = new SaveFileDialog
            {
                Filter = type + " files (*" + type + ")|*" + type,
                RestoreDirectory = true,
                Title = "Safe File",
                FilterIndex = 2
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string path = dialog.FileName;
                Console.WriteLine("Saving data to " + path);

                try
                {
                    File.WriteAllLines(path, Data);
                    Output.Success("Saved file as " + dialog.FileName + type + " at " + path);
                } catch (Exception error) { Output.FatalError(error, "save file"); }
            }

            return;
        }

        public static List<string> OpenFile(string type = ".txt")
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Title = "Load File",
                InitialDirectory = @"c:\",
                Filter = type + " files (" + type + ") | " + type,
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string path = dialog.FileName;
                Console.WriteLine("Loading file " + path);

                try
                {
                    string[] rawData = File.ReadAllLines(path);
                    List<string> Data = new List<string>();

                    foreach (string i in rawData)
                    {
                        Data.Add(i);
                        Console.WriteLine("Fetched '" + i + "'");
                    }

                    Output.Success("Loaded file " + path);
                    return Data;
                } catch (Exception error) { Output.FatalError(error, "load file"); }
            }

            return new List<string>();
        }

        public static List<string> GetFiles(string path, string type = "*.txt")
        {
            Console.WriteLine("Checking for folder " + path);

            if (Directory.Exists(path))
            {
                Console.WriteLine("Getting files from " + path);

                try
                {
                    List<string> files = new List<string>();
                    string[] dir = Directory.GetFiles(path, type);

                    foreach (string i in dir)
                    {
                        files.Add(i);
                        Console.WriteLine("Found '" + i + "'");
                    }

                    Output.Success("Retrieved " + files.Count + " files from " + path);

                    return files;

                }  catch (Exception error) { Output.FatalError(error, "get files from " + path); }
            } else
            {
                Output.Error("Could not find " + path);
                Console.WriteLine("Creating " + path);

                try
                {
                    System.IO.Directory.CreateDirectory(path);

                    if (Directory.Exists(path))
                    {
                        Output.Success("Created " + path);
                    }
                } catch(Exception error) { Output.FatalError(error, "create folder " + path); }
            }

            return new List<string>();
        }
    }
}
