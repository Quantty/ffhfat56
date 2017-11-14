using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using Xamarin.UITest.Android;
using System.Threading;
using static Login;

namespace UITest3
{   

    [TestFixture]
    public class Tests
    {
        AndroidApp app;

        //Data used for Tests
        //
        private String email = "andreimoisaa@mail.ru";
        private String password = "12345";
        private String name = "Andrei";
        private String address = "Søren Kierkegaards Plads1 ";
        private String floorAptNr = "666";
        private String phoneNumber = "0758888888";
        //
        //

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
            Thread.Sleep(1000);
            Order1Item(1);

            Thread.Sleep(1000);
            new Login(app).SingIn(email,password);

            Thread.Sleep(1000);
            Delivery();

            //finishing and going to payment section
            app.Tap(x => x.Id("new_order_pay_bill_btn"));
        }


        [Test]
        public void CreateNewUserScenario()
        {
            Order1Item(4);

            new Login(app).CreateUser("Rasmus","ras@mail.ru", "12345");

            TakeAway();

            //finishing and going to payment section
            app.Tap(x => x.Id("new_order_pay_bill_btn"));
        }

        [Test]
        public void TakeAwaySuccessScenario()
        {
            Thread.Sleep(1000);

            Order1Item(4);

            new Login(app).SingIn(email, password);

            TakeAway();

            //finishing and going to payment section
            app.Tap(x => x.Id("new_order_pay_bill_btn"));
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
            //In case the store is closed
            CheckForMessage();
            
            app.Tap(x => x.Id("menu_item_plus_btn"));
            app.Tap(x => x.Id("select_dishes_shopping_basket_fab"));
            app.Tap(x => x.Id("new_order_pay_bill_btn"));
        }

        public void CheckForMessage()
        {
            if (app.Query("message").Any())
            {
                //tap Comfirm
                app.Tap(x => x.Id("button1"));
            }
        }
        public void TakeAway()
        {
            app.Tap(x => x.Id("takeaway_iv"));
            app.Tap(x => x.Id("asap_btn"));
            app.Tap(x => x.Id("pay_btn"));
            app.Tap(x => x.Id("new_order_pay_bill_btn"));
        }
        public void Delivery()
        {   // In case of messages about closed delivery hours

            CheckForMessage();

            app.Tap(x => x.Id("delivery_iv"));
            app.Tap(x => x.Id("asap_btn"));
            app.Tap(x => x.Id("pay_btn"));

            new Address(app,name,address,floorAptNr,phoneNumber).InsertAddress();
        }
 
    }
}

