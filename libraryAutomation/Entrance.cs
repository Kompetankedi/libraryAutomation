using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace libraryAutomation
{
    public partial class Entrance : Form
    {
        public Entrance()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=.\\SQLExpress;Initial Catalog=DbKutuphane;Integrated Security=True;Encrypt=False");
        SqlConnection baglanti = new SqlConnection("Data Source=.\\SQLExpress;Initial Catalog=DbKutuphane;Integrated Security=True;Encrypt=False");

        private void bul(string sorgu)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                { con.Open(); }
                SqlDataAdapter da = new SqlDataAdapter(sorgu, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                bunifuDataGridView1.DataSource = dt;
                con.Close();


            }
            catch (Exception hata) { MessageBox.Show(hata.Message); }
        }
        private void tbul(string sorgu)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                { con.Open(); }
                SqlDataAdapter da = new SqlDataAdapter(sorgu, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                bunifuDataGridView2.DataSource = dt;
                con.Close();


            }
            catch (Exception hata) { MessageBox.Show(hata.Message); }
        }
        private void Obul(string sorgu)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                { con.Open(); }
                SqlDataAdapter da = new SqlDataAdapter(sorgu, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                bunifuDataGridView3.DataSource = dt;
                con.Close();


            }
            catch (Exception hata) { MessageBox.Show(hata.Message); }
        }
        private void list()
        {
            if (con.State == ConnectionState.Closed)
            { con.Open(); }
            SqlDataAdapter da = new SqlDataAdapter("select * from YabanciRoman", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            bunifuDataGridView1.DataSource = dt;
            con.Close();
        }
        private void tlist()
        {
            if (con.State == ConnectionState.Closed)
            { con.Open(); }
            SqlDataAdapter da = new SqlDataAdapter("select * from TrRoman", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            bunifuDataGridView2.DataSource = dt;
            con.Close();
        }
        private void btnYabanci_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        private void btnTrkitap_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 3;
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void Entrance_Load(object sender, EventArgs e)
        {
            VeriGoster();
            tlist();
            list();
            CenterToScreen();
            tabControl1.Enabled = true;
            button4.FlatAppearance.BorderSize = 0;
            button4.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button4.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button4.BackColor = Color.Transparent;
        }

        private void btnYupdatelist_Click(object sender, EventArgs e)
        {
            list();
        }

        private void btnYadd_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into YabanciRoman (KitapNo,KitapAdi,KitapYazar) values (@p1,@p3,@p4)", con);
            cmd.Parameters.AddWithValue("@p1", txtBookid.Text);
            cmd.Parameters.AddWithValue("@p3", txtYbookName.Text);
            cmd.Parameters.AddWithValue("@p4", txtYbookAuthor.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            list();
        }
        int selectedid = 0;
        private void btnYdel_Click(object sender, EventArgs e)
        {
            if (bunifuDataGridView1.CurrentRow != null)
            {
                selectedid = Convert.ToInt32(bunifuDataGridView1.CurrentRow.Cells[0].Value.ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM YabanciRoman WHERE id=" + selectedid + "", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Kayıt Silindi");
                con.Close();
                list();
            }
        }

        private void btnYupdate_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand komutguncelle = new SqlCommand("Update YabanciRoman set KitapNo=@a1,KitapAdi=@a3,KitapYazar=@a4 where id=@a5", con);
            komutguncelle.Parameters.AddWithValue("@a1", txtBookid.Text);
            komutguncelle.Parameters.AddWithValue("@a3", txtYbookName.Text);
            komutguncelle.Parameters.AddWithValue("@a4", txtYbookAuthor.Text);
            komutguncelle.Parameters.AddWithValue("@a5", txtSQLid.Text);
            komutguncelle.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Kayıt Güncellendi.", MessageBoxButtons.OK.ToString());
            list();
        }

        private void atxtKitapNo_TextChanged(object sender, EventArgs e)
        {
            bul("select * from YabanciRoman where KitapNo like '%" + atxtKitapNo.Text + "%'");
        }

        private void VeriGoster()
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                    baglanti.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from Oislemler", baglanti);
                DataTable dTable = new DataTable();
                sqlDataAdapter.Fill(dTable);
                bunifuDataGridView3.DataSource = dTable;
                baglanti.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void atxtKitapAdi_TextChanged(object sender, EventArgs e)
        {
            bul("select * from YabanciRoman where KitapAdi like '%" + atxtKitapAdi.Text + "%'");
        }

        private void atxtKitapYazar_TextChanged(object sender, EventArgs e)
        {
            bul("select * from YabanciRoman where KitapYazar like '%" + atxtKitapYazar.Text + "%'");
        }

        private void bunifuDataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            txtSQLid.Text = bunifuDataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtYbookName.Text = bunifuDataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtYbookAuthor.Text = bunifuDataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtBookid.Text = bunifuDataGridView1.CurrentRow.Cells[3].Value.ToString();
        }




        private void btnTlistupdate_Click(object sender, EventArgs e)
        {
            tlist();
        }

        private void btnTadd_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into TrRoman (KitapNo,KitapAdi,KitapYazar) values (@p1,@p3,@p4)", con);
            cmd.Parameters.AddWithValue("@p1", txtTbookid.Text);
            cmd.Parameters.AddWithValue("@p3", txtTBookName.Text);
            cmd.Parameters.AddWithValue("@p4", txtTBookAuthor.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            tlist();
        }

        int trselected = 0;
        private void btnTdel_Click(object sender, EventArgs e)
        {
            if (bunifuDataGridView2.CurrentRow != null)
            {
                trselected = Convert.ToInt32(bunifuDataGridView2.CurrentRow.Cells[0].Value.ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM TrRoman WHERE id=" + trselected + "", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Kayıt Silindi");
                con.Close();
                tlist();
            }
        }

        private void btnTupdate_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand komutguncelle = new SqlCommand("Update TrRoman set KitapNo=@a1,KitapAdi=@a3,KitapYazar=@a4 where id=@a5", con);
            komutguncelle.Parameters.AddWithValue("@a1", txtTbookid.Text);
            komutguncelle.Parameters.AddWithValue("@a3", txtTBookName.Text);
            komutguncelle.Parameters.AddWithValue("@a4", txtTBookAuthor.Text);
            komutguncelle.Parameters.AddWithValue("@a5", txtTSQLid.Text);
            komutguncelle.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Kayıt Güncellendi.");
            tlist();
        }

        private void txtsearchBookİd_TextChanged(object sender, EventArgs e)
        {
            tbul("select * from TrRoman where KitapNo like '%" + txtsearchBookİd.Text + "%'");
        }

        private void txtsearchBookname_TextChanged(object sender, EventArgs e)
        {
            tbul("select * from TrRoman where KitapAdi like '%" + txtsearchBookname.Text + "%'");
        }

        private void txtsearchBookAuthor_TextChanged(object sender, EventArgs e)
        {
            tbul("select * from TrRoman where KitapYazar like '%" + txtsearchBookAuthor.Text + "%'");
        }

        private void bunifuDataGridView2_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            txtTSQLid.Text = bunifuDataGridView2.CurrentRow.Cells[0].Value.ToString();
            txtTBookName.Text = bunifuDataGridView2.CurrentRow.Cells[1].Value.ToString();
            txtTBookAuthor.Text = bunifuDataGridView2.CurrentRow.Cells[2].Value.ToString();
            txtTbookid.Text = bunifuDataGridView2.CurrentRow.Cells[3].Value.ToString();
        }
        private void delete()
        {
            if (DialogResult.OK == MessageBox.Show("Seçili Kayıt Silinecektir!", "Kayıt Siliniyor", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
            {
                try
                {
                    if (baglanti.State == ConnectionState.Closed)
                        baglanti.Open();
                    string SorguSil = "delete from Oislemler where id=" + txtOSQLid.Text;
                    SqlCommand komut = new SqlCommand(SorguSil, baglanti);
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    VeriGoster();
                    MessageBox.Show("Kayıt Başarıyla Silinmiştir");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void Guncelle()
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                    baglanti.Open();
                string sorgu = "update  Oislemler set Oisim=@adsoyad,Okitap=@kitabinadi,Okitapid=@kitapno," +
                    "Osınıf=@sinif,Oalındımı=@aciklamalar,Oalınmatarihi=@alindigitarih,Oiadetarih=@iadetarih where id=" + txtOSQLid.Text ;
                SqlCommand komut = new SqlCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@adsoyad", txtOName.Text);
                komut.Parameters.AddWithValue("@kitabinadi", txtOBookName.Text);
                komut.Parameters.AddWithValue("@kitapno", txtOBookid.Text);
                komut.Parameters.AddWithValue("@sinif", txtOClass.Text);
                komut.Parameters.AddWithValue("@aciklamalar", txtOdelivery.Text);
                komut.Parameters.AddWithValue("@alindigitarih", bunifuDatePicker1.Text);
                komut.Parameters.AddWithValue("@iadetarih", bunifuDatePicker2.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                VeriGoster();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Kaydet()
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                    baglanti.Open();
                string sorgu = "insert into Oislemler(Oisim,Okitap,Okitapid,Osınıf,Oalındımı,Oalınmatarihi,Oiadetarih) values (@adsoyad,@kitabinadi,@kitapno,@sinif,@aciklamalar,@alindigitarih,@iadetarih)";
                SqlCommand komut = new SqlCommand(sorgu, baglanti);
                //        komut.Parameters.AddWithValue("@id",txtOSQLid.Text);
                komut.Parameters.AddWithValue("@adsoyad", txtOName.Text);
                komut.Parameters.AddWithValue("@kitabinadi", txtOBookName.Text);
                komut.Parameters.AddWithValue("@kitapno", txtOBookid.Text);
                komut.Parameters.AddWithValue("@sinif", txtOClass.Text);
                komut.Parameters.AddWithValue("@aciklamalar", txtOdelivery.Text);
                komut.Parameters.AddWithValue("@alindigitarih", bunifuDatePicker1.Text);
                komut.Parameters.AddWithValue("@iadetarih", bunifuDatePicker2.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                VeriGoster();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void obul(string sorgu)
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                    baglanti.Open();
                SqlDataAdapter dAdapter = new SqlDataAdapter(sorgu, baglanti);
                DataTable dTable = new DataTable();
                dAdapter.Fill(dTable);
                bunifuDataGridView3.DataSource = dTable;
                baglanti.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnOadd_Click(object sender, EventArgs e)
        {
            Kaydet();
        }

        private void btnOdel_Click(object sender, EventArgs e)
        {
            delete();
        }

        private void btnOupdate_Click(object sender, EventArgs e)
        {
            Guncelle();
        }

        private void btnOupdatelist_Click(object sender, EventArgs e)
        {
            VeriGoster();
        }

        private void txtOSearchName_TextChanged(object sender, EventArgs e)
        {
            obul("select * from Oislemler where Oisim like '%" + txtOSearchName.Text + "%'");
        }

        private void txtOSearchid_TextChanged(object sender, EventArgs e)
        {
            obul("select * from Oislemler where Okitapid like '%" + txtOSearchid.Text + "%'");
        }

        private void txtOSearchBookName_TextChanged(object sender, EventArgs e)
        {
            obul("select * from Oislemler where Okitap like '%" + txtOSearchBookName.Text + "%'");
        }

        private void txtOSearchClass_TextChanged(object sender, EventArgs e)
        {
            obul("select * from Oislemler where Osınıf like '%" + txtOSearchClass.Text + "%'");
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void bunifuButton7_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void bunifuDataGridView3_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            txtOSQLid.Text = bunifuDataGridView3.CurrentRow.Cells[0].Value.ToString();
            txtOName.Text = bunifuDataGridView3.CurrentRow.Cells[1].Value.ToString();
            txtOBookName.Text = bunifuDataGridView3.CurrentRow.Cells[2].Value.ToString();
            txtOBookid.Text = bunifuDataGridView3.CurrentRow.Cells[3].Value.ToString();
            bunifuDatePicker1.Text = bunifuDataGridView3.CurrentRow.Cells[4].Value.ToString();
            bunifuDatePicker2.Text = bunifuDataGridView3.CurrentRow.Cells[5].Value.ToString();
            txtOdelivery.Text = bunifuDataGridView3.CurrentRow.Cells[6].Value.ToString();
            txtOClass.Text = bunifuDataGridView3.CurrentRow.Cells[7].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState=FormWindowState.Minimized;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #region kaydırma işlemleri
        private bool isDragging = false;
        private Point mouseOffset;
        private Point formPosition;
        private void button4_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            mouseOffset = e.Location;
            formPosition = Location;
        }

        private void button4_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging && e.Button == MouseButtons.Left)
            {
                Point currentPosition = PointToScreen(e.Location);
                Location = new Point(currentPosition.X - mouseOffset.X, currentPosition.Y - mouseOffset.Y);
            }
        }

        private void Entrance_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void Entrance_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isDragging)
            {
                Cursor = Cursors.Default;
            }
            else
            {
                Cursor = Cursors.SizeAll;
            }
        }

        private void button4_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }
        #endregion
    }
}