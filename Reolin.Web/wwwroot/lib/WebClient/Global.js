var Reo = Reolin.Web.Client;
var UrlSource = Reo.URLs;
var source = new Reo.RemoteJwtSource(UrlSource.ExhangeTokenUrl, UrlSource.GetTokenUrl);
var store = new Reo.LocalJwtStore();
var manager = new Reo.DefaultJwtManager(source, store);
function IsNullOrEmpty(...input) {
    for (var item of input) {
        if (item === undefined || item === "" || item === null) {
            return true;
        }
    }
    return false;
}
