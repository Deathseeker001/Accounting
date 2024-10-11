using Accounting.DataLayer;
using Accounting.DataLayer.context;
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
using ValidationComponents;

namespace Accounting.App
{
    public partial class frmAddOrEditCustomer : Form
    {
        public int ID = 0;
        public frmAddOrEditCustomer()
        {
            InitializeComponent();
        }

        private void frmAddOrEditCustomer_Load(object sender, EventArgs e)
        {
            if (ID != 0)
            {
                this.Text = "ویرایش شخص ";
                btnSubmit.Text = "ویرایش";
                using (UnitOfWork db = new UnitOfWork())
                {
                    var customer = db.CustomerRepository.GetCustomerById(ID);
                    txtName.Text = customer.FullName;
                    txtMobile.Text = customer.Mobile;
                    txtEmail.Text = customer.Email;
                    txtAddress.Text = customer.Address;
                    pcCustomer.ImageLocation = Application.StartupPath + "/Images/" + customer.Image;

                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {


            if (BaseValidator.IsFormValid(this.components))
            {
                string imageName = Guid.NewGuid().ToString() + Path.GetExtension(pcCustomer.ImageLocation);
                string path = Application.StartupPath + "/Images/";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                pcCustomer.Image.Save(path + imageName);
                using (UnitOfWork db = new UnitOfWork())
                {

                    Customers customer = new Customers()
                    {
                        FullName = txtName.Text,
                        Mobile = txtMobile.Text,
                        Email = txtEmail.Text,
                        Address = txtAddress.Text,
                        Image = imageName

                    };

                    if( ID == 0)
                    {
                        db.CustomerRepository.InsertCustomer(customer);
                    }
                    else
                    {
                        customer.CustomerID = ID;
                        db.CustomerRepository.UpdateCustomer(customer);
                    }
                    


                    db.Save();

                    DialogResult = DialogResult.OK;

                }


            }


        }


        private void btnSelectPhoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                pcCustomer.ImageLocation = openFile.FileName;
            }
        }
    }
}
