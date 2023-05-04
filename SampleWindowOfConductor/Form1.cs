using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SampleWindowOfConductor
{
    public partial class Form1 : Form
    {

        List<Inf> infor = new List<Inf>();
        public string path = @"C:\";
        public Form1()
        {
            InitializeComponent();
            conductor();
        }
        public void conductor()
        {
            direc_one_time();
            files_one_time();
        }

        private void direc_one_time()
        {
            try
            {
                string[] dir = Directory.GetDirectories(path);
                foreach (string s in dir)
                {
                    FileInfo test = new FileInfo(s);
                    string name = test.Name, pathing = path + @"\" + name;
                    var item = new Inf();
                    item.name = name;
                    item.path = pathing;
                    infor.Add(item);
                    listBox1.Items.Add(name);
                }
            } catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void files_one_time()
        {
            string[] files = Directory.GetFiles(path);
            try
            {
                foreach (string s in files)
                {
                    FileInfo test = new FileInfo(s);
                    string name = test.Name, ext = test.Extension, pathing = path + @"\" + name;
                    var item = new Inf();
                    item.name = name;
                    item.path = pathing;
                    item.exten = ext;
                    item.fileis = true;
                    infor.Add(item);
                    listBox1.Items.Add(name);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void direc_reusable(int index)
        {
            infor[index].b = true;
            string selfpath = infor[index].path;
            int i = 1;
            try
            {
                string[] dir = Directory.GetDirectories(selfpath);
                foreach (string s in dir)
                {
                    FileInfo test = new FileInfo(s);
                    string name = test.Name, pathing = selfpath + @"\" + name;
                    var item = new Inf();
                    item.name = name;
                    item.path = pathing;
                    item.tabulation = infor[index].tabulation + 1;
                    infor.Insert(index + i, item);
                    string tab = "";
                    for (int j = 0; j < item.tabulation; j++)
                        tab += "  ";
                    listBox1.Items.Insert(index + i, tab + name);
                    i++;
                }
                files_reusable(selfpath, index + i - 1);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void files_reusable(string selfpath, int index)
        {
            string[] files = Directory.GetFiles(selfpath);
            int i = 1;
            try
            {
                foreach (string s in files)
                {
                    FileInfo test = new FileInfo(s);
                    string name = test.Name, ext = test.Extension, pathing = selfpath + @"\" + name;
                    var item = new Inf();
                    item.name = name;
                    item.path = pathing;
                    item.exten = ext;
                    item.tabulation = infor[index].tabulation;
                    if ((Directory.GetDirectories(selfpath)).Length==0)
                        item.tabulation++;
                    item.fileis = true;
                    infor.Insert(index + i, item);
                    string tab = "";
                    for (int j = 0; j < item.tabulation; j++)
                        tab += "  ";
                    listBox1.Items.Insert(index + i, tab + name);
                    i++;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void direc_of(int index)
        {
            int i = index + 1;
            while (infor[i].tabulation > infor[index].tabulation)
            {
                infor.RemoveAt(i);
                listBox1.Items.RemoveAt(i);
            }
            infor[index].b = false;
        }

        private void open_file(int index)
        {
            try
            {
                Form f = new Form();
                Label l = new Label();
                l.Location = new Point(20, 20);
                StreamReader read = new StreamReader(infor[index].path);
                l.Text = read.ReadToEnd();
                read.Close();
                f.Controls.Add(l);
                f.Show();
            } catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = listBox1.IndexFromPoint(e.Location);
            Console.WriteLine(index);
            if (!infor[index].fileis)
            {
                if (!infor[index].b)
                    direc_reusable(index);
                else if (infor[index].b)
                    direc_of(index);
            }
            else if ((infor[index].fileis) && (infor[index].exten == ".txt"))
                open_file(index);
        }
    }
    public class Inf{
            public string path, name, exten;
            public bool b = false, fileis = false;
            public int tabulation = 0;
            public override string  ToString()
                    {
 	                    return name;
                    }
        }
}
