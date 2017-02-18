//var source: Reo.IJwtSource = new Reo.RemoteJwtSource(Reo.LocalURLs.ExhangeTokenUrl, Reo.LocalURLs.GetTokenUrl);
//var store: Reo.LocalJwtStore = new Reo.LocalJwtStore();
//var manager: Reo.IJwtManager = new Reo.DefaultJwtManager(source, store);
//var service: Reo.AccountService = new Reo.AccountService(manager);
var authService = new Reo.AuthenticatedHttpServiceProvider(manager, () => {
    alert("authentication failed");
});
//$("#GetValueButton").click(function (e) {
//    var handler = new Reo.HttpServiceHandler();
//    //handler.HandleError = (r: Reo.HttpResponse) => console.log(r.Error);
//    handler.HandleResponse = (r: Reo.HttpResponse) => console.log(r.ResponseText);
//    authService.Get("http://178.63.55.123/Values/Get", {}, 2, true, handler);
//}); 
