var Reolin;
(function (Reolin) {
    var Web;
    (function (Web) {
        var Client;
        (function (Client) {
            var Controllers;
            (function (Controllers) {
                var UI = Reolin.Web.UI.Services;
                class RegisterCVController {
                    constructor() {
                        this.FullNameTextBox = $("#FullName");
                        this.MajorCategoryDropdown = $("#MajorCategory");
                        this.SubCategoryDropdown = $("#SubCategory");
                        this.CityDropDown = $("#CityList");
                        this.CountryDropdown = $("#Country");
                        this.PhoneNumberTextBox = $('#PhoneNumber');
                        this._accountController = new Controllers.AccountController(() => {
                            var model = new UI.RegisterProfileModel();
                            model.Name = this.FullNameTextBox.val();
                            model.City = this.CityDropDown.find("option:selected").text();
                            model.Country = this.CountryDropdown.find("option:selected").text();
                            model.PhoneNumber = this.PhoneNumberTextBox.val();
                            model.JobCategoryId = this.MajorCategoryDropdown.find("option:selected").val();
                            model.SubJobCategoryID = this.SubCategoryDropdown.find("option:selected").val();
                            model.Latitude = parseFloat(this.CityDropDown.find("option:selected").attr("data-lat"));
                            model.Longitude = parseFloat(this.CityDropDown.find("option:selected").attr("data-long"));
                            var handler = new Reo.HttpServiceHandler();
                            handler.HandleResponse = (r) => {
                                window.location.href = "/User/chooseYourProfile";
                            };
                            handler.HandleError = (r) => {
                                alert(r.ResponseText);
                            };
                            this._profileService.CreatePersonal(model, handler);
                        });
                        this._profileService = new UI.ProfileService();
                    }
                }
                Controllers.RegisterCVController = RegisterCVController;
            })(Controllers = Client.Controllers || (Client.Controllers = {}));
        })(Client = Web.Client || (Web.Client = {}));
    })(Web = Reolin.Web || (Reolin.Web = {}));
})(Reolin || (Reolin = {}));
var registerCVController = new Reolin.Web.Client.Controllers.RegisterCVController();
