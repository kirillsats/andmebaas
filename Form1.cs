using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Andmebaas
{
    public partial class Form1 : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Andmebas;Integrated Security=True");

        SqlCommand cmd;
        SqlDataAdapter adapter;

        public Form1()
        {
            
            InitializeComponent();
            NaitaAndmed();
        }

        public void NaitaAndmed()
        { 
            conn.Open();
            DataTable dt = new DataTable();
            cmd = new SqlCommand("SELECT * FROM Toode",conn);
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            dataGridView1.DataSource = dt; //связывает грид с таблицей
            conn.Close();
        }
       










        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'andmebasDataSet.Toode' table. You can move, or remove it, as needed.
            this.toodeTableAdapter.Fill(this.andmebasDataSet.Toode);

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (NimetusBox.Text.Trim() != string.Empty && KogusBox.Text.Trim() != string.Empty && HindBox.Text.Trim() != string.Empty)
            {
                try
                {
                    conn.Open();
                    cmd = new SqlCommand("INSERT INTO Toode(Nimetus,Kogus,Hind) VALUES (@toode, @kogus,@hind)", conn);
                    cmd.Parameters.AddWithValue("@toode", NimetusBox.Text);
                    cmd.Parameters.AddWithValue("@kogus", KogusBox.Text);
                    cmd.Parameters.AddWithValue("@hind", HindBox.Text);

                    cmd.ExecuteNonQuery();

                    conn.Close();
                    NaitaAndmed();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Andmebaasiga viga!");
                }
            }
            else
            {
                MessageBox.Show("Sisesta andmeid!");
            }
        }
        public void kustuta_btn_click(object sender, EventArgs e)
        {

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {

                    int selectedID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID"].Value);
                    conn.Open();
                    cmd = new SqlCommand("DELETE FROM Toode WHERE ID= @id", conn);
                    cmd.Parameters.AddWithValue("@id", selectedID);

                    cmd.ExecuteNonQuery();

                    conn.Close();
                    NaitaAndmed();

                    MessageBox.Show("Toote on kustutanud!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Viga andmete kustutamisel! " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Palun, valige kustutavad andmed!");
            }

        }
    }
}
