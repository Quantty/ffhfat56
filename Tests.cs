using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using Xamarin.UITest.Android;
using System.Threading;

namespace UITest3
{


    [TestFixture]
    public class Tests
    {
        AndroidApp app;

        [SetUp]
        public void BeforeEachTest()
        {
            // TODO: If the iOS app being tested is included in the solution then 
            // add a reference to the android project from the project containing this file
            app = ConfigureApp
                    .Android
                    // TODO: Update this path to point to your Android app and uncomment the
                    // code if the app is not included in the solution.
                    .ApkFile("C:\\Users\\Quanticus\\Downloads\\app-release.apk")
                    .StartApp();
        }

        [Test]
        public void AppLaunches()
        {
            app.Screenshot("First screen.");
        }

        [Test]
        public void DeliverySuccessScenario()
        {
            Order1Item(1);

            Login("andreimoisaa@mail.ru","12345");

            TryDeliveryOrCollect();

            //finishing and going to payment section
            app.Tap(x => x.Id("new_order_pay_bill_btn"));
        }


        [Test]
        public void CreateNewUserScenario()
        {
            Order1Item(2);

            CreateUser("Rasmus","ras@mail.ru", "12345");

            //TakeAway();

            //finishing and going to payment section
            //app.Tap(x => x.Id("new_order_pay_bill_btn"));
        }

        [Test]
        public void TakeAwaySuccessScenario()
        {
            Order1Item(4);

            Login("andreimoisaa@mail.ru", "12345");

            //TryDeliveryOrCollect();   //uncomment this line and comment the next 2 for deivery test
            app.Tap(x => x.Id("takeaway_iv"));
            TakeAway();

            //finishing and going to payment section
            app.Tap(x => x.Id("new_order_pay_bill_btn"));
        }

        [Test]
        public void NewTest()
        {
            app.Tap(x => x.Id("imageView").Index(1));
            app.Tap(x => x.Id("menu_item_plus_btn"));
            app.Tap(x => x.Id("select_dishes_shopping_basket_fab"));
            app.Tap(x => x.Id("new_order_pay_bill_btn"));
            app.Tap(x => x.Id("authButton"));
        }


        public void TryDeliveryOrCollect()
        {
            try
            {
                app.Tap(x => x.Id("delivery_iv"));
                Delivery();
            }
            catch (Exception e)
            {
                Console.WriteLine("Deliver button not found or Closed!");

                try
                {
                    app.Tap(x => x.Id("takeaway_iv"));
                    TakeAway();
                }
                catch(Exception e2)
                {
 
                    Console.WriteLine("Take Away buton not found");
  
                }
            }
        }
        public void Order1Item(int index)
        {   
            if(index == 0)
            {
                app.Tap(x => x.Id("imageView"));
            }
            else
            {
                app.Tap(x => x.Id("imageView").Index(index));
            }

            //In case the restaurant is closed
            try
            {
                app.Tap(x => x.Id("button1"));
            }
            catch(Exception e){}
            
            app.Tap(x => x.Id("menu_item_plus_btn"));
            app.Tap(x => x.Id("select_dishes_shopping_basket_fab"));
            app.Tap(x => x.Id("new_order_pay_bill_btn"));
        }
        public void TakeAway()
        {
          
            app.Tap(x => x.Id("asap_btn"));
            app.Tap(x => x.Id("pay_btn"));
            app.Tap(x => x.Id("new_order_pay_bill_btn"));
        }
        public void Delivery()
        {   // this try is in case of messages about closed delivery hours
           /* try
            {
                app.Tap(x => x.Id("button1"));
            }
            catch (Exception e)
            {

            }*/
            app.Tap(x => x.Id("asap_btn"));
            app.Tap(x => x.Id("pay_btn"));
            InsertAddress("Andrei", "Søren Kierkegaards Plads 1", "1221", "0758888888");

        }
        public void InsertAddress(String name,String address,String postCode,String phone)
        {
 

            app.ClearText(x => x.Id("delivery_address_name_et"));
            app.Tap(x => x.Id("delivery_address_name_et"));
            app.EnterText(x => x.Id("delivery_address_name_et"), name);

            app.ClearText(x => x.Id("delivery_address_address_postcode_mode_et"));
            app.Tap(x => x.Id("delivery_address_address_postcode_mode_et"));
            app.EnterText(x => x.Id("delivery_address_address_postcode_mode_et"), address);

            app.Tap(x => x.Id("delivery_address_postcode_et"));
            app.ClearText(x => x.Id("delivery_areas_postcode"));
            app.EnterText(x => x.Id("delivery_areas_postcode"), postCode);
            Thread.Sleep(2000);
            app.Tap(x => x.Class("AppCompatTextView").Text("1221"));

            app.ClearText(x => x.Id("delivery_address_telephone_postcode_mode_et"));
            app.EnterText(x => x.Id("delivery_address_telephone_postcode_mode_et"), phone);
            app.Back();

            app.Tap(x => x.Id("delivery_address_validate_tv"));

        }
        public void Login(String email, String password)
        {
            app.Tap(x => x.Id("user_login_fragment_create_acc_tv"));
            app.EnterText(x => x.Id("user_login_fragment_email_et"), email);
            app.EnterText(x => x.Id("user_login_fragment_password_et"), password);
            app.PressEnter();
            app.Tap(x => x.Id("old_fashioned_way_space"));
            app.Tap(x => x.Id("user_login_button"));
        }
        public void CreateUser(String name,String email,String password)
        {
            app.Tap(x => x.Id("user_login_fragment_name_et"));
            app.EnterText(x => x.Id("user_login_fragment_name_et"), name);
            //app.Tap(x => x.Id("user_login_old_way"));
            //app.PressEnter();
            //app.DoubleTap(x => x.Id("user_login_fragment_name_et"));
            //app.Tap(x => x.Id("user_login_fragment_email_et"));
            app.EnterText(x => x.Id("user_login_fragment_email_et"), email);
            //app.Tap(x => x.Id("user_login_fragment_password_et"));
            app.EnterText(x => x.Id("user_login_fragment_password_et"), password);
            app.DismissKeyboard();
            //app.Tap(x => x.Id("user_login_button"));
            app.Tap(x => x.Id("user_login_button"));
        }

      
    }
}

