using HomeWork25_07_19.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HomeWork25_07_19
{
    public partial class Authors : Form
    {
        private AuthorInfoEntities db;
        private AuthorsInfo authorInput;
        private AuthorsInfo author_update_delete;

        public Authors()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           db = new AuthorInfoEntities();
            FilldGv();
           CmbCountry.DataSource= db.Country.Select(country => new ComboClass { Id=country.Id,Name=country.CountryName }).ToList();
        }

        private void FilldGv()
        {
            var authors = db.AuthorsInfo.Where(authorInput=>authorInput.Deleted==false).Select(auth => new
            {
                auth.Id,
                auth.AuthorName,
                auth.AuthorSurname,
                City = auth.City.CityName,
                Country = auth.City.Country.CountryName
            }).OrderByDescending(auth => auth.Id).ToList();
            dGv.DataSource = authors;


            

        }

        private  void CmbCountry_SelectedValueChanged(object sender, EventArgs e)
        {
            int cityId = (CmbCountry.SelectedItem as ComboClass).Id;
            CmbCity.DataSource = db.City.Where( city => city.CountryId==cityId).Select(city=>city.CityName).ToList();
        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            CheckInput();
            db.AuthorsInfo.Add(authorInput);
            UpdateDgV();
        }

        private void CheckInput()
        {
            string name = txtName.Text.Trim();
            string surname = txtSurname.Text.Trim();
            int? countryId = (CmbCountry.SelectedItem as ComboClass).Id;

            if (name == "" || surname == "" || countryId == null)
            {

                MessageBox.Show("Don't empty ", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                authorInput = new AuthorsInfo { AuthorName = name, AuthorSurname = surname, CityId = countryId };
            }


        }

       

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            CheckInput();
            author_update_delete.AuthorName = authorInput.AuthorName;
            author_update_delete.AuthorSurname = authorInput.AuthorSurname;
            UpdateDgV();
          
        }

        private void DGv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = (int)dGv.Rows[e.RowIndex].Cells[0].Value;
            author_update_delete = db.AuthorsInfo.Find(id);
            txtName.Text = author_update_delete.AuthorName;
            txtSurname.Text = author_update_delete.AuthorSurname;
            CmbCity.Text = author_update_delete.City.CityName;
            CmbCountry.Text = author_update_delete.City.Country.CountryName;
            BtnVisible(false);

        }

        private void UpdateDgV()
        {
            db.SaveChanges();
            dGv.DataSource = null;
            FilldGv();

        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            var response = MessageBox.Show($"Are you sure delete {author_update_delete.AuthorName}?","Warning",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
            if (response == DialogResult.Yes)
            {
                author_update_delete.Deleted = true;
                db.SaveChanges();
                UpdateDgV();
            }

        }

        


        private void BtnVisible( bool b)
        {
            if (b)
            {
                btnCreate.Visible = true;
                btnDelete.Visible = false;
                btnUpdate.Visible = false;
            }
            else
            {
                btnCreate.Visible = false;
                btnDelete.Visible = true;
                btnUpdate.Visible = true;

            }
        }

        private void CreateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BtnVisible(true);
        }

        
    }

}
