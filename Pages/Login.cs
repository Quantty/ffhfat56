using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest.Android;

public class Login
{
    private AndroidApp app;
    public Login(AndroidApp app)
    {
        this.app = app;
    }
    public void SingIn(String email, String password)
    {
        app.Tap(x => x.Id("user_login_fragment_create_acc_tv"));
        app.EnterText(x => x.Id("user_login_fragment_email_et"), email);
        app.EnterText(x => x.Id("user_login_fragment_password_et"), password);
        app.PressEnter();
        app.Tap(x => x.Id("old_fashioned_way_space"));
        app.Tap(x => x.Id("user_login_button"));
    }


    public void CreateUser(String name, String email, String password)
    {
        app.Tap(x => x.Id("user_login_fragment_name_et"));
        app.EnterText(x => x.Id("user_login_fragment_name_et"), name);
        app.EnterText(x => x.Id("user_login_fragment_email_et"), email);
        app.EnterText(x => x.Id("user_login_fragment_password_et"), password);
        app.DismissKeyboard();
       
        app.Tap(x => x.Id("user_login_button"));
    }
}
