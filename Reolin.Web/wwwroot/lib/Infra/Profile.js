var Reolin;
(function (Reolin) {
    var Web;
    (function (Web) {
        var Client;
        (function (Client) {
            class Profile {
                static get Current() {
                    return JSON.parse(window.localStorage.getItem("CurrentProfile"));
                }
                static set Current(profile) {
                    window.localStorage.setItem("CurrentProfile", JSON.stringify(profile));
                }
            }
            Client.Profile = Profile;
        })(Client = Web.Client || (Web.Client = {}));
    })(Web = Reolin.Web || (Reolin.Web = {}));
})(Reolin || (Reolin = {}));
