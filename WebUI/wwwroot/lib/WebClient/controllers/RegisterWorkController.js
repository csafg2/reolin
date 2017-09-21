var Reolin;
(function (Reolin) {
    var Web;
    (function (Web) {
        var Client;
        (function (Client) {
            var Controllers;
            (function (Controllers) {
                var UI = Reolin.Web.UI.Services;
                class RegisterWorkController {
                    constructor() {
                        this.FullNameTextBox = $("#FullName");
                        this.MajorCategoryDropdown = $("#MainCategory");
                        this.SubCategoryDropdown = $("#SubCategory");
                        //CityDropDown: JQuery = $("#CityList");
                        //CountryDropdown: JQuery = $("#Country");
                        this.PhoneNumberTextBox = $('#PhoneNumber');
                        this.Latitude = $("#us3-lat");
                        this.Longitude = $("#us3-lon");
                        this._profileService = new UI.ProfileService();
                        this._accountController = new Controllers.AccountController(() => {
                            var model = new UI.RegisterProfileModel();
                            model.Name = this.FullNameTextBox.val();
                            model.PhoneNumber = this.PhoneNumberTextBox.val();
                            model.JobCategoryId = this.MajorCategoryDropdown.find("option:selected").val();
                            model.SubJobCategoryID = this.SubCategoryDropdown.find("option:selected").val();
                            model.Latitude = parseFloat(this.Latitude.val());
                            model.Longitude = parseFloat(this.Longitude.val());
                            var geoCodeUrl = "http://maps.googleapis.com/maps/api/geocode/json?latlng=" + this.Latitude.val() + "," + this.Longitude.val() + "& sensor=true";
                            var me = this;
                            //get city and country name
                            $.getJSON(geoCodeUrl, null, function (d) {
                                if (d.status !== 'OK') {
                                    return;
                                }
                                var handler = new Reo.HttpServiceHandler();
                                handler.HandleError = (r) => {
                                    alert("damn! error!");
                                };
                                handler.HandleResponse = (r) => {
                                    window.location.href = "/User/ChooseYourProfile";
                                };
                                var info = me.GetLocationInfo(d.results);
                                model.City = info.city;
                                model.Country = info.country;
                                me._profileService.CreateWorkProfile(model, handler);
                            });
                        });
                    }
                    GetLocationInfo(results) {
                        var country, city = "";
                        if (results[0]) {
                            var add = results[0].formatted_address;
                            var value = add.split(",");
                            var count = value.length;
                            country = value[count - 1];
                            var state = value[count - 2];
                            city = value[count - 3];
                            console.log("city: " + city);
                            console.log("country: " + country);
                        }
                        else {
                            alert("address not found");
                        }
                        return { country: country, city: city };
                    }
                }
                Controllers.RegisterWorkController = RegisterWorkController;
            })(Controllers = Client.Controllers || (Client.Controllers = {}));
        })(Client = Web.Client || (Web.Client = {}));
    })(Web = Reolin.Web || (Reolin.Web = {}));
})(Reolin || (Reolin = {}));
new Reolin.Web.Client.Controllers.RegisterWorkController();
