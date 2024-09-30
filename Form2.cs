using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace databaseLogin
{
    public partial class Form2 : Form
    {
        private MySqlConnection koneksi;
        private MySqlDataAdapter adapter;
        private MySqlCommand perintah;
        private DataSet ds = new DataSet();
        private string alamat, query;

        public Form2()
        {
            alamat = "server=localhost; database=db_login; username=root; password=;";
            koneksi = new MySqlConnection(alamat);

            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                koneksi.Open();
                query = string.Format("select * from tbl_user ");
                perintah = new MySqlCommand(query, koneksi);
                adapter = new MySqlDataAdapter(perintah);
                perintah.ExecuteNonQuery();
                ds.Clear();
                adapter.Fill(ds);
                koneksi.Close();
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Width = 100;
                dataGridView1.Columns[0].HeaderText = "ID User";
                dataGridView1.Columns[1].Width = 150;
                dataGridView1.Columns[1].HeaderText = "Username";
                dataGridView1.Columns[2].Width = 120;
                dataGridView1.Columns[2].HeaderText = "Password";
                dataGridView1.Columns[3].Width = 120;
                dataGridView1.Columns[3].HeaderText = "Email";
                dataGridView1.Columns[4].Width = 120;
                dataGridView1.Columns[4].HeaderText = "No Handphone";

                textBoxIDUser.Clear();
                textBoxUsername.Clear();
                textBoxPassword.Clear();
                textBoxEmail.Clear();
                textBoxNoHandphone.Clear();
                //textBoxIDUser.Focus(); 
                buttonUpdate.Enabled = true;
                buttonDelete.Enabled = true;
                buttonClear.Enabled = true;
                buttonSave.Enabled = true;
                buttonSearch.Enabled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxUsername.Text != "" && textBoxPassword.Text != "" && textBoxEmail.Text != "")
                {

                    query = string.Format("insert into tbl_user  values ('{0}','{1}','{2}','{3}','{4}');", textBoxIDUser.Text, textBoxUsername.Text, textBoxPassword.Text, textBoxEmail.Text, textBoxNoHandphone.Text);


                    koneksi.Open();
                    perintah = new MySqlCommand(query, koneksi);
                    adapter = new MySqlDataAdapter(perintah);
                    int res = perintah.ExecuteNonQuery();
                    koneksi.Close();
                    if (res == 1)
                    {
                        MessageBox.Show("Insert Data Suksess ...");
                        Form2_Load(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Gagal insert Data . . . ");
                    }
                }
                else
                {
                    MessageBox.Show("Data Tidak lengkap !!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxUsername.Text != "")
                {
                    query = string.Format("select * from tbl_user where username = '{0}'", textBoxUsername.Text);
                    ds.Clear();
                    koneksi.Open();
                    perintah = new MySqlCommand(query, koneksi);
                    adapter = new MySqlDataAdapter(perintah);
                    perintah.ExecuteNonQuery();
                    adapter.Fill(ds);
                    koneksi.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow kolom in ds.Tables[0].Rows)
                        {
                            textBoxIDUser.Text = kolom["id_user"].ToString();
                            textBoxPassword.Text = kolom["password"].ToString();
                            textBoxEmail.Text = kolom["email"].ToString();
                            textBoxNoHandphone.Text = kolom["no_handphone"].ToString();

                        }
                        textBoxUsername.Enabled = true;
                        dataGridView1.DataSource = ds.Tables[0];
                        buttonSave.Enabled = false;
                        buttonUpdate.Enabled = true;
                        buttonDelete.Enabled = true;
                        buttonSearch.Enabled = false;
                        buttonClear.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Data Tidak Ada !!");
                        Form2_Load(null, null);
                    }

                }
                else
                {
                    MessageBox.Show("Data Yang Anda Pilih Tidak Ada !!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxPassword.Text != "" && textBoxEmail.Text != "" && textBoxUsername.Text != "" && textBoxIDUser.Text != "")
                {

                    query = string.Format("update tbl_user set username = '{0}', password = '{1}', email = '{2}', no_handphone = '{3}' where id_user = '{4}' ", textBoxUsername.Text, textBoxPassword.Text, textBoxEmail.Text, textBoxNoHandphone.Text, textBoxIDUser.Text);


                    koneksi.Open();
                    perintah = new MySqlCommand(query, koneksi);
                    adapter = new MySqlDataAdapter(perintah);
                    int res = perintah.ExecuteNonQuery();
                    koneksi.Close();
                    if (res == 1)
                    {
                        MessageBox.Show("Update Data Suksess ...");
                        Form2_Load(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Gagal Update Data . . . ");
                    }
                }
                else
                {
                    MessageBox.Show("Data Tidak lengkap !!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxIDUser.Text != "")
                {
                    if (MessageBox.Show("Anda Yakin Menghapus Data Ini ??", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        query = string.Format("Delete from tbl_user where id_user = '{0}'", textBoxIDUser.Text);
                        
                        koneksi.Open();
                        perintah = new MySqlCommand(query, koneksi);
                        adapter = new MySqlDataAdapter(perintah);
                        int res = perintah.ExecuteNonQuery();
                        ds.Clear();
                        adapter.Fill(ds);
                        koneksi.Close();
                        if (res == 1)
                        {
                            MessageBox.Show("Delete Data Suksess ...");
                        }
                        else
                        {
                            MessageBox.Show("Gagal Delete data");
                        }
                    }
                    //Form2_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Data Yang Anda Pilih Tidak Ada !!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            try
            {
                Form2_Load(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
        }

        private void buttonLogOut_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }
    }
}
    

