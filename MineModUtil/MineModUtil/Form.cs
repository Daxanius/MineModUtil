using MineModUtil.UtilAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;

namespace MineModUtil
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();

            string path = @"Templates\";
            List<string> files = FileManager.GetFiles(path, "*.json");

            Console.WriteLine("Initializing templates.");

            try
            {
                foreach (string i in files)
                {
                    string item = i.Remove(0, path.Length);
                    if (item.Contains("template_")) { item = item.Remove(0, 9); }

                    int length = item.Length;
                    item = item.Remove(length - 5, 5);
                    comboBox.Items.Add(item);

                    Console.WriteLine("Initialized " + item);
                }
                Output.Success("Initialized templates!");
            } catch (Exception error) { Output.FatalError(error, "loading components"); }

            Output.Success("User interface ready!");
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            string path = FileManager.SelectFolder();

            if (path != null)
            {
                textBox1.Text = path;
                Output.Success("Selected " + path + " as directory!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Collecting data...");

            List<string> Items = new List<string>();

            try { Items.Add(textBox1.Text); } catch { Items.Add(""); }
            try { Items.Add(textBox2.Text); } catch { Items.Add(""); }
            try { Items.Add(textBox3.Text); } catch { Items.Add(""); }
            try { Items.Add(comboBox.Text); } catch { Items.Add(""); }

            if (Items.TrueForAll(i => i.Equals("")))
            {
                Output.Warning("No data to collect!");
            } else
            {
                Output.Success("Collected data!");
                FileManager.SafeFile(Items, ".MMU");
            }

            return;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> Items = FileManager.OpenFile("*.MMU");

            if (!Items.TrueForAll(i => i.Equals(null)))
            {
                Console.WriteLine("Inserting items...");

                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                comboBox.Text = "";

                try { textBox1.Text = Items[0]; } catch { Output.Warning("No item was found for directory."); }
                try { textBox2.Text = Items[1]; } catch { Output.Warning("No item was found for folder:subfolder."); }
                try { textBox3.Text = Items[2]; } catch { Output.Warning("No item was found for item name."); }
                try { comboBox.SelectedItem = Items[3]; } catch { Output.Warning("No item was found for category."); }

                Output.Success("Inserted items!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Collecting data...");

            List<string> Items = new List<string>();

            try { Items.Add(textBox1.Text); } catch { Items.Add(""); }
            try { Items.Add(textBox2.Text); } catch { Items.Add(""); }
            try { Items.Add(textBox3.Text); } catch { Items.Add(""); }
            try { Items.Add(comboBox.Text); } catch { Items.Add(""); }

            if (Items.TrueForAll(i => i.Equals("")))
            {
                Output.Warning("No data to collect!");
            }
            else
            {
                Output.Success("Collected data!");
                Util.CreateJSON(Items[0],Items);
            }
        }
    }
}
