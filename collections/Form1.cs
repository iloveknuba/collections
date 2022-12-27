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
using System.Xml.Serialization;

namespace collections
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public class one : IComparable
        {
            public string type;
            public string tovar;
            public int cost;
            public int amount;
            public one(string t, string t1, int c, int a)
            {
                type = t;
                tovar = t1;
                cost = c;
                amount = a;
            }
            public one()
            {
                Random r = new Random();
                type = "Fruits";
                tovar = "Cherry";
                cost = r.Next(60, 80);
                amount = r.Next(150, 300);
            }
            public int CompareTo(object obj)
            {
                one b;
                b = (one)obj;
                return amount.CompareTo(b.amount);
            }

        }
        class SortByAmount : IComparer<one>
        {
            public int Compare(one x, one y)
            {
                one t1 = x;
                one t2 = y;
                if (t1.amount < t2.amount) return 1;
                if (t1.amount > t2.amount) return -1;
                return 0;
            }
        }

        class Tovar_list
        {

            int cout = 0;

            public XmlSerializer ser = new XmlSerializer(typeof(List<one>));
            public List<one> sklad = new List<one>();


            public void add(string t1, string t2, int c1, int a2, DataGridView dg)
            {
                sklad.Add(new one(t1, t2, c1, a2));
                dg.Rows.Add(sklad[sklad.Count - 1].type, sklad[sklad.Count - 1].tovar, sklad[sklad.Count - 1].cost.ToString(), sklad[sklad.Count - 1].amount.ToString());
                cout++;
            }

            public void show(DataGridView dg)
            {
                dg.Rows.Clear();
                for (int i = 0; i < sklad.Count; i++)
                {
                    dg.Rows.Add(sklad[i].type, sklad[i].tovar, sklad[i].cost.ToString(), sklad[i].amount.ToString());

                }
            }


            public void del(int i)
            {
                sklad.RemoveAt(i);
            }

            public void zap(string f)
            {
                FileStream file = new FileStream(f, FileMode.Create, FileAccess.Write, FileShare.None);
                ser.Serialize(file, sklad);
                file.Close();
            }
            public void ct(string f)
            {
                FileStream file;
                file = new FileStream(f, FileMode.Open, FileAccess.Read, FileShare.None);
                sklad = (List<one>)ser.Deserialize(file);
                file.Close();
            }
            public void sort()
            {
                SortByAmount sa = new SortByAmount();
                sklad.Sort(sa);
            }

            public void sort1(int z, DataGridView dg)
            {
                sklad.Sort();
                dg.Rows.Clear();
                for (int i = 0; i < sklad.Count; i++)
                {
                    if (z < sklad[i].amount)
                        dg.Rows.Add(sklad[i].type, sklad[i].tovar, sklad[i].cost.ToString(), sklad[i].amount.ToString());

                }

            }

        }

        Tovar_list pl = new Tovar_list();

        private void button1_Click(object sender, EventArgs e)
        {
            string type = textBox1.Text;
            string tovar = textBox2.Text;
            int cost = Convert.ToInt32(textBox3.Text);
            int amount = Convert.ToInt32(textBox4.Text);
            pl.add(type, tovar, cost, amount, dataGridView2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pl.sort();
            pl.show(dataGridView3);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int y = Convert.ToInt32(textBox5.Text);
                pl.sort1(y, dataGridView1);
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string path = @"D:\недоУчоба\2 курс\ООП\collections\xml.txt";
            pl.ct(path);
            pl.show(dataGridView2);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string path = @"D:\недоУчоба\2 курс\ООП\collections\xml.txt";
            pl.zap(path);
            MessageBox.Show("Saved");
        }

        private void button6_Click(object sender, EventArgs e)
        {           
                int i = dataGridView2.CurrentRow.Index;
                pl.del(i);
                pl.show(dataGridView2);          
        }
    }
}
