
module Reolin.Web.Client.Controllers
{
    export interface UserLoggedInCallBack
    {
        (): void;
    }

    import UI = Reolin.Web.UI.Services;
    export class RegisterCVController
    {
        public FullNameTextBox: JQuery = $("#FullName");
        public MajorCategoryDropdown: JQuery = $("#MajorCategory");
        public SubCategoryDropdown: JQuery = $("#SubCategory");
        public CityDropDown: JQuery = $("#CityList");
        public CountryDropdown: JQuery = $("#Country");
        public PhoneNumberTextBox: JQuery = $('#PhoneNumber');

        private _accountController: AccountController = new AccountController(() =>
        {
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

            handler.HandleResponse = (r: HttpResponse) =>
            {
                window.location.href = "/User/chooseYourProfile";
            }

            handler.HandleError = (r: HttpResponse): void =>
            {
                alert(r.ResponseText);
            };

            this._profileService.CreatePersonal(model, handler);
        });

        private _profileService: UI.ProfileService = new UI.ProfileService();
    }
}

var registerCVController: Reolin.Web.Client.Controllers.RegisterCVController =
    new Reolin.Web.Client.Controllers.RegisterCVController();