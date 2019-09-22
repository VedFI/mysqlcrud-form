using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;

namespace DatabaseApp
{
    class Connect
    {
        MySqlConnection baglanti;
        MySqlDataAdapter adapter;
        MySqlDataReader reader;
        MySqlCommand komut;
         
        public bool Baglan()
        {
            try
            {
                baglanti =
                    new MySqlConnection("Server=sql7.freesqldatabase.com;Database=sql7287836;Uid=sql7287836;Pwd=nK57F73wT4");
                baglanti.Open();            
                if(baglanti.State != System.Data.ConnectionState.Closed)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception err)
            {

            }
            return false;
        }

        public void getTable(String command,DataTable dt)
        {
            try
            {
                adapter = new MySqlDataAdapter(command, baglanti);
                adapter.Fill(dt);
            }
            catch(Exception e)
            {
                MessageBox.Show("Önce Bağlantı Kurmalısınız!","HATA!");
            }
        }

        public void addRow(String command)
        {
            try
            {
                komut = new MySqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = command;
                komut.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString(),"HATA!");
            }
        }

        public void deleteRow(long id)
        {
            try
            {
                komut = null;
                komut = new MySqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "DELETE FROM ogrenciler WHERE ogr_ID="+id+";";
                komut.ExecuteNonQuery();
            }
            catch(Exception a)
            {
                MessageBox.Show(a.ToString());
            }
        }

        public void truncateTable()
        {
            komut = null;
            komut = new MySqlCommand();
            komut.Connection = baglanti;
            komut.CommandText = "TRUNCATE TABLE ogrenciler";
            komut.ExecuteNonQuery();
        }

        public void searchRow(String command,DataTable dt)
        {
            try
            {
                komut = null;
                komut = new MySqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = command;
                reader = komut.ExecuteReader();
                while (reader.Read())
                {                    
                    long num = reader.GetInt64(0);
                    String ad = reader.GetString(1);
                    String soyad = reader.GetString(2);
                    int sinif = reader.GetInt32(3);
                    dt.Rows.Add(num, ad, soyad, sinif);
                }
                reader.Close();
                reader = null;
            }
            catch(Exception e)
            {
                MessageBox.Show("Tabloyu Gösterdikten Sonra Arama Yaptığınızdan Ve Bağlantı Kurduğunuzdan Emin Olun.","Uyarı!");
            }
        }
    }
}
