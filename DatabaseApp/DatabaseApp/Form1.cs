using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatabaseApp
{
    public partial class Form1 : Form
    {
        Connect con = new Connect();
        DataTable dt = new DataTable();
        public Form1()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            radioButton1.Select();
            radioButton9.Select();
            label2.ForeColor = Color.Red;
            label2.Text = "Bağlantı Yok!";
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {       
            bool stat = con.Baglan();
            if (stat)
            {
                label2.ForeColor = Color.Green;
                label2.Text = "Bağlantı Kuruldu!";
            }
            else
            {
                label2.ForeColor = Color.Red;
                label2.Text = "Hata!";
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {            
            dt.Clear();
            dataGridView1.DataSource = dt;
            String komut = "SELECT *FROM ogrenciler";
            if (radioButton5.Checked)
            {
                komut += " ORDER BY ogr_ID;";
            }
            else if (radioButton6.Checked)
            {
                komut += " ORDER BY ad;";
            }
            else if (radioButton7.Checked)
            {
                komut += " ORDER BY soyad;";
            }
            else if (radioButton8.Checked)
            {
                komut += " ORDER BY sinif;";
            }
            con.getTable(komut, dt);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.Columns[dataGridView1.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.DataSource = dt;       
        }

        private void button3_Click(object sender, EventArgs e)
        {  
            int numara = Int32.Parse(textBox1.Text);
            int sinif = Int32.Parse(textBox4.Text);
            String komut = "INSERT INTO ogrenciler VALUES(" + numara + ",'" + textBox2.Text + "','" + textBox3.Text + "'," + sinif + ");";
            con.addRow(komut);
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            button2.PerformClick();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            String komut="SELECT *FROM ogrenciler WHERE ";
            dt.Clear();
            dataGridView1.DataSource = dt;
            if (radioButton1.Checked)
            {
                try
                {
                    long numara = long.Parse(textBox5.Text);
                    komut += "ogr_ID=" + numara;
                }
                catch(Exception ERR)
                {
                    MessageBox.Show("Sayı girmelisiniz!");
                }
                
            }
            else if (radioButton2.Checked)
            {
                komut += "ad LIKE '%" + textBox5.Text + "%'";
            }
            else if (radioButton3.Checked)
            {
                komut += "soyad LIKE '%" + textBox5.Text + "%'";
            }
            else if (radioButton4.Checked)
            {
                int sinif = Int32.Parse(textBox5.Text);
                komut += "sinif=" + sinif;
            }
            if (radioButton5.Checked)
            {
                komut += " ORDER BY ogr_ID;";
            }
            else if (radioButton6.Checked)
            {
                komut += " ORDER BY ad;";
            }
            else if (radioButton7.Checked)
            {
                komut += " ORDER BY soyad;";
            }
            else if (radioButton8.Checked)
            {
                komut += " ORDER BY sinif;";
            }
            else
            {
                komut += ";";
            }
            con.searchRow(komut, dt);
            textBox5.Text = "";
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.Columns[dataGridView1.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.DataSource = dt;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            long id = long.Parse(textBox6.Text);
            con.deleteRow(id);
            textBox6.Text = "";
            button2.PerformClick();
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Tablo Temizlenecek Emin Misiniz?","DİKKAT!",MessageBoxButtons.YesNo);
            if(dialog == DialogResult.Yes)
            {
                con.truncateTable();
            }
            else
            {
                //do nothing
            }
            button2.PerformClick();
        }
    }
}
