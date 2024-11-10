using Accounting.DataLayer.context;
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
    public partial class frmLogin : Form
    {
        public bool isEdit = false;
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            if (isEdit)
            {
                this.Text = "تنظیمات ورود به برنامه";
                btnLogin.Text = "ذخیره تغییرات";
                using(UnitOfWork db = new UnitOfWork())
                {
                    var login = db.LoginRepository.Get().First();
                    txtUserName.Text = login.UserName;
                    txtPassWord.Text = login.PassWord;
                }
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            if (BaseValidator.IsFormValid(this.components))
            {
                using (UnitOfWork db = new UnitOfWork())
                {
                    if (isEdit)
                    {
                        var login = db.LoginRepository.Get().First();
                        login.UserName = txtUserName.Text;
                        login.PassWord = txtPassWord.Text;
                        db.LoginRepository.update(login);
                        db.Save();
                        Application.Restart();
                    }

                    else
                    {
                        if (db.LoginRepository.Get(l => l.UserName == txtUserName.Text && l.PassWord == txtPassWord.Text).Any())
                        {
                            DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            RtlMessageBox.Show("کاربری یافت نشد!");
                        }
                    }
                }

            }
        }
    }
}
