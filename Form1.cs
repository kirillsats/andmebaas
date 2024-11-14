using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.XPath;

namespace Andmebaas
{
    public partial class Form1 : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\opilane\source\repos\Andmebaas\Andmebas.mdf;Integrated Security=True");

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
                    cmd = new SqlCommand("INSERT INTO Toode(Nimetus,Kogus,Hind, Pilt) VALUES (@toode, @kogus,@hind, @pilt)", conn);
                    cmd.Parameters.AddWithValue("@toode", NimetusBox.Text);
                    cmd.Parameters.AddWithValue("@kogus", KogusBox.Text);
                    cmd.Parameters.AddWithValue("@hind", HindBox.Text);
                    //cmd.Parameters.AddWithValue("@pilt", NimetusBox.Text+extension);

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
        int ID = 0;
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ID = (int)dataGridView1.Rows[e.RowIndex].Cells["ID"].Value;
            NimetusBox.Text = dataGridView1.Rows[e.RowIndex].Cells["Nimetus"].Value.ToString();
            KogusBox.Text = dataGridView1.Rows[e.RowIndex].Cells["Kogus"].Value.ToString();
            HindBox.Text = dataGridView1.Rows[e.RowIndex].Cells["Hind"].Value.ToString();
        }
        OpenFileDialog open;
        SaveFileDialog save;
        private void Otsipilt_Click(object sender, EventArgs e)
        {
            open = new OpenFileDialog();
            open.InitialDirectory = @"C:\Users\opilane\Pictures\";
            open.Multiselect = false;
            open.Filter = "Images Files(*.jpeg;*.png;*.bmp;*jpg)|*.jpeg;*.png;*.bmp;*jpg";
            FileInfo opefile = new FileInfo(@"C:\Users\opilane\Pictures\"+open.FileName);
            if (open.ShowDialog() == DialogResult.OK && NimetusBox.Text != null)
            {
                save = new SaveFileDialog();
                save.InitialDirectory = Path.GetFullPath(@"..\..\..}Pildid");
                string extension = Path.GetExtension(open.FileName);
                save.FileName = NimetusBox.Text + extension;
                save.Filter = "Images" + Path.GetExtension(open.FileName) + "|" + Path.GetExtension(open.FileName);
                if (save.ShowDialog() == DialogResult.OK && NimetusBox != null)
                {
                    File.Copy(open.FileName, save.FileName);
                    Otsipilt.Image = Image.FromFile(save.FileName);
                }
            }
            else
            {
                MessageBox.Show("Puudub toode nimetus või ei ole Cancel vajutavad");
            }
        }

    }


}
