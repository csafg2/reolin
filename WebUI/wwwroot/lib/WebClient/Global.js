var Reo = Reolin.Web.Client;
var UrlSource = Reo.LocalURLs;
var source = new Reo.RemoteJwtSource(UrlSource.ExhangeTokenUrl, UrlSource.GetTokenUrl);
var store = new Reo.LocalJwtStore();
var manager = new Reo.DefaultJwtManager(source, store);
function IsNullOrEmpty() {
    var input = [];
    for (var _i = 0; _i < arguments.length; _i++) {
        input[_i] = arguments[_i];
    }
    for (var _a = 0, input_1 = input; _a < input_1.length; _a++) {
        var item = input_1[_a];
        if (item === undefined || item === "" || item === null) {
            return true;
        }
    }
    return false;
}
