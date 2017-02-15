import Reo = Reolin.Web.Client;
var UrlSource = Reo.LocalURLs;
var source: Reo.IJwtSource = new Reo.RemoteJwtSource(UrlSource.ExhangeTokenUrl, UrlSource.GetTokenUrl);
var store: Reo.LocalJwtStore = new Reo.LocalJwtStore();
var manager: Reo.IJwtManager = new Reo.DefaultJwtManager(source, store);


function IsNullOrEmpty(...input: string[]): boolean
{
    for (var item of input)
    {
        if (item === undefined || item === "" || item === null)
        {
            return true;
        }
    }

    return false;
}