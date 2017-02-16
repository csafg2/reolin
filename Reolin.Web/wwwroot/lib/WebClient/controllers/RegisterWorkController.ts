module Reolin.Web.Client.Controllers
{
    import UI = Reolin.Web.UI.Services;

    export class RegisterWorkController
    {
        FullNameTextBox: JQuery = $("#FullName");
        MajorCategoryDropdown: JQuery = $("#MainCategory");
        SubCategoryDropdown: JQuery = $("#SubCategory");
        //CityDropDown: JQuery = $("#CityList");
        //CountryDropdown: JQuery = $("#Country");
        PhoneNumberTextBox: JQuery = $('#PhoneNumber');
        Latitude: JQuery = $("#us3-lat");
        Longitude: JQuery = $("#us3-lon");

        private _profileService: UI.ProfileService = new UI.ProfileService();


        private _accountController: AccountController = new AccountController(() =>
        {
            var model = new UI.RegisterProfileModel();
            model.Name = this.FullNameTextBox.val();
            model.PhoneNumber = this.PhoneNumberTextBox.val();
            model.JobCategoryId = this.MajorCategoryDropdown.find("option:selected").val();
            model.SubJobCategoryID = this.SubCategoryDropdown.find("option:selected").val();
            model.Latitude = parseFloat(this.Latitude.val());
            model.Longitude = parseFloat(this.Longitude.val());
            var geoCodeUrl = "http://maps.googleapis.com/maps/api/geocode/json?latlng=" + this.Latitude.val() + "," + this.Longitude.val() + "& sensor=true";
            var me = this;
            $.getJSON(geoCodeUrl, null, function (d)
            {
                if (d.status !== 'OK')
                {
                    return;
                }

                console.log(d.status);
                
                var handler = new Reo.HttpServiceHandler();

                handler.HandleError = (r: HttpResponse): void =>
                {
                    alert("damn! error!");
                };

                handler.HandleResponse = (r: HttpResponse): void =>
                {
                    console.log(r);
                }

                var info = me.GetLocationInfo(d.results);
                model.City = info.city;
                model.Country = info.country;
                me._profileService.CreateWorkProfile(model, handler);
            })
        });

        GetLocationInfo(results: any): any
        {
            var country, city = "";
            if (results[0])
            {
                var add = results[0].formatted_address;
                var value = add.split(",");

                var count = value.length;
                country = value[count - 1];
                var state = value[count - 2];
                city = value[count - 3];
                console.log("city: " + city);
                console.log("country: " + country);
            }
            else
            {
                alert("address not found");
            }
            return { country: country, city: city };
        }

    }

}


var rController: Reolin.Web.Client.Controllers.RegisterWorkController = new Reolin.Web.Client.Controllers.RegisterWorkController();