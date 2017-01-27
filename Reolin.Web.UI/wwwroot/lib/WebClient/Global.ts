import Reo = Reolin.Web.Client;

var source: Reo.IJwtSource = new Reo.RemoteJwtSource(Reo.LocalURLs.ExhangeTokenUrl, Reo.LocalURLs.GetTokenUrl);
var store: Reo.LocalJwtStore = new Reo.LocalJwtStore();
var manager: Reo.IJwtManager = new Reo.DefaultJwtManager(source, store);
var service: Reo.AccountService = new Reo.AccountService(manager);
