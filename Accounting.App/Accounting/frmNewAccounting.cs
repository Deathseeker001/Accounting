﻿using Accounting.DataLayer.context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ValidationComponents;

namespace Accounting.App
{
    public partial class frmNewAccounting : Form
    {
        public int AccountID = 0;
        public frmNewAccounting()
        {
            InitializeComponent();
        }

        private void frmNewAccounting_Load(object sender, EventArgs e)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                dgvCustomers.AutoGenerateColumns = false;
                dgvCustomers.DataSource = db.CustomerRepository.GetNamesCustomer();
                if (AccountID != 0)
                {
                    var account = db.AccountingRepository.GetById(AccountID);
                    txtAmount.Text = account.Amount.ToString();
                    txtDescription.Text = account.Description.ToString();
                    txtName.Text = db.CustomerRepository.GetCustomerNameById(account.CustomerID);
                    if(account.TypeID == 1)
                    {
                        rbReceive.Checked = true;
                    }
                    else
                    {
                        rbPay.Checked = true;
                    }
                    this.Text = "ویرایش";
                    btnSave.Text = "ویرایش";

                }
            }

        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            using (UnitOfWork db = new UnitOfWork())
            {

                dgvCustomers.AutoGenerateColumns = false;
                dgvCustomers.DataSource = db.CustomerRepository.GetNamesCustomer(txtFilter.Text);

            }
        }

        private void dgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtName.Text = dgvCustomers.CurrentRow.Cells[0].Value.ToString();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (BaseValidator.IsFormValid(this.components))
            {
                if (rbPay.Checked || rbReceive.Checked)
                {
                    using (UnitOfWork db = new UnitOfWork())
                    {
                        DataLayer.Accounting accounting = new DataLayer.Accounting()
                        {
                            Amount = int.Parse(txtAmount.Value.ToString()),
                            CustomerID = db.CustomerRepository.GetCustomerIdByName(txtName.Text),
                            TypeID = (rbReceive.Checked ?1 : 2),
                            DateTime = DateTime.Now,
                            Description = txtDescription.Text
                        };
                        if (AccountID == 0)
                        {
                            db.AccountingRepository.Insert(accounting);
                        }
                        else
                        {
                            accounting.ID = AccountID;
                            db.AccountingRepository.update(accounting);
                        }
                        db.Save();
                        DialogResult = DialogResult.OK;
                    }

                }


            }
            else
            {
                RtlMessageBox.Show("لطفا نوع تراکنش را انتخاب کنید!", "هشدار", MessageBoxButtons.OKCancel);
            }
        }
    }
}

