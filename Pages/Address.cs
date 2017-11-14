using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest.Android;

public class Address
{   
    private AndroidApp app;

    private String name;
    private String address;
    private String floorAptNr;
    private String phone;

    public Address(AndroidApp app, string name, string address, string floorAptNr, string phone)
    {
        this.app = app;
        this.name = name;
        this.address = address;
        this.floorAptNr = floorAptNr;
        this.phone = phone;
    }

   

    public void InsertAddress()
    {


        app.ClearText(x => x.Id("delivery_address_name_et"));
        app.Tap(x => x.Id("delivery_address_name_et"));
        app.EnterText(x => x.Id("delivery_address_name_et"), name);
        app.DismissKeyboard();

        app.Tap(x => x.Id("delivery_address_address_et"));
        app.ClearText("delivery_areas_addresses");
        app.EnterText(x => x.Id("delivery_areas_addresses"), address);
        app.Tap(x => x.Id("addresses_name_row"));

        app.ClearText("delivery_address_floor_et");
        app.EnterText(x => x.Id("delivery_address_floor_et"), floorAptNr);
        app.DismissKeyboard();

        app.ClearText(x => x.Id("delivery_address_telephone_et"));
        app.EnterText(x => x.Id("delivery_address_telephone_et"), phone);
        app.DismissKeyboard();

        /*
         * Old version
         * 
        app.ClearText(x => x.Id("delivery_address_address_postcode_mode_et"));
        app.Tap(x => x.Id("delivery_address_address_postcode_mode_et"));
        app.EnterText(x => x.Id("delivery_address_address_postcode_mode_et"), address); 
        app.Tap(x => x.Id("delivery_address_postcode_et"));
        app.ClearText(x => x.Id("delivery_areas_postcode"));
        app.EnterText(x => x.Id("delivery_areas_postcode"), postCode);
        app.Tap(x => x.Class("AppCompatTextView").Text("1221"));

        app.ClearText(x => x.Id("delivery_address_telephone_postcode_mode_et"));
        app.EnterText(x => x.Id("delivery_address_telephone_postcode_mode_et"), phone);
        app.Back();
        */

        app.Tap(x => x.Id("delivery_address_validate_tv"));

    }
}
