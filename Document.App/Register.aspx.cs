using Document.Library.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Document.App
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void button_register(object sender, EventArgs e)
        {
            try
            {
                string message = "";
                string namefield = name.Value.Trim();
                string emailField = email.Value.Trim();
                string companyField = company.Value;
                string phoneField = phone.Value;
                string passwordField = password.Value;
                string confirmPassField = confirmpassword.Value;

                if (!passwordField.Equals(confirmPassField, StringComparison.OrdinalIgnoreCase))
                {
                    message = "Invalid Password Combination. Please check your password";
                    errorMessage.InnerText = message;
                    labelDiv.Attributes.Add("display", "block");
                    return;
                }

                UserServices.Add(namefield, emailField, phoneField, passwordField, confirmPassField, companyField);


            }
            catch
            {
                Response.Redirect(FormsAuthentication.LoginUrl);

            }

        }

    }
}