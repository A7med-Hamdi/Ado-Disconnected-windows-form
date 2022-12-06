using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ado
{
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {


            DGV_course.DataSource = Businesslayer.getall();

            SqlCommand cmd1 = new SqlCommand("select * from topic");
            cmb_topic.DataSource = DBlayers.select(cmd1);

            
              cmb_topic.DisplayMember = "top_Name";
              cmb_topic.ValueMember = "top_id";

           
            btn_add.Visible = true;
            btn_update.Visible = false;
            btn_delete.Visible = false;

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btn_add_Click(object sender, EventArgs e)
        {
           SqlCommand cmd = new SqlCommand("insert into course(crs_id ,crs_name,crs_duration,top_id) values(@id,@name,@duaration,@topic) ");

            cmd.Parameters.AddWithValue("id", txt_id.Text);
            cmd.Parameters.AddWithValue("name", txt_name.Text);
            cmd.Parameters.AddWithValue("duaration", txt_duaration.Text);
            cmd.Parameters.AddWithValue("topic", cmb_topic.SelectedValue);

            int roweffect = DBlayers.dml(cmd);

          

            if (roweffect > 0)
            {
                txt_duaration.Text = txt_id.Text = txt_name.Text = "";
                Form1_Load(null, null); 
            }

        }

        private void btn_search_Click(object sender, EventArgs e)
        {
          SqlCommand  cmd = new SqlCommand("select * from course where crs_name like @search");
            cmd.Parameters.AddWithValue("search", txt_search.Text);

            DGV_course.DataSource = DBlayers.select(cmd);

           


        }

        private void btn_update_Click(object sender, EventArgs e)
        {
           SqlCommand cmd = new SqlCommand("update course set Crs_Name=@name , Crs_Duration=@duration , Top_Id=@topic  where Crs_Id=@id");
            cmd.Parameters.AddWithValue("id", txt_id.Text);
            cmd.Parameters.AddWithValue("name", txt_name.Text);
            cmd.Parameters.AddWithValue("duration", txt_duaration.Text);
            cmd.Parameters.AddWithValue("topic", cmb_topic.SelectedValue);
           
            int rowEffect = DBlayers.dml(cmd);
           
            if (rowEffect > 0)
            {
                txt_duaration.Text = txt_id.Text = txt_name.Text = cmb_topic.SelectedText = "";
                Form1_Load(null, null);
            }
            btn_add.Visible = true;
            btn_update.Visible = false;
            btn_delete.Visible = false;
            lb_update.Text = "";

        }

        private void DGV_course_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {


        }

        private void DGV_course_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txt_id.Text = DGV_course.SelectedRows[0].Cells[0].Value.ToString();
            txt_name.Text = DGV_course.SelectedRows[0].Cells[1].Value.ToString();
            txt_duaration.Text = DGV_course.SelectedRows[0].Cells[2].Value.ToString();
            cmb_topic.DisplayMember = DGV_course.SelectedRows[0].Cells[3].Value.ToString();

            lb_update.Text = "don't change the id";
            btn_add.Visible = false;
            btn_update.Visible = true;
            btn_delete.Visible = true;
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            lb_update.Text = "";
            if (MessageBox.Show("Are you sure you want to delete", "confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                int id = (int)DGV_course.SelectedRows[0].Cells[0].Value;
               SqlCommand cmd = new SqlCommand("delete from course where crs_id =@id");
                cmd.Parameters.AddWithValue("id", id);
                
                int roweffect = DBlayers.dml(cmd);

                if (roweffect > 0)
                {
                    txt_duaration.Text = txt_id.Text = txt_name.Text = cmb_topic.SelectedText = "";

                    Form1_Load(null, null);
                }
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
