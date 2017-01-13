/// <reference path="typing/jquery.d.ts" />

import Reo = Reolin.Web.Client;
var exhangeUrl: string = "http://localhost:6987/account/ExchangeToken";
var getUrl: string = "http://localhost:6987/account/Login";
var source: Reo.IJwtSource = new Reo.RemoteJwtSource(exhangeUrl, getUrl);
var store: Reo.LocalJwtStore = new Reo.LocalJwtStore();
var manager: Reo.IJwtManager = new Reo.DefaultJwtManager(source, store);
$("#getButton").click(function (e)
{
    //var jwt: string = $("#jwt").val();
    
    var info: Reo.LoginInfo = new Reo.LoginInfo();
    info.UserName = "Nina3";
    info.Password = "Hassan@1";
    console.clear();
    //var newJwt: Reo.JwtSecurityToken = source.IssueJwt(info);
    var newJwt: Reo.JwtSecurityToken = manager.IssueJwt(info);
    console.log("newtoken: " + newJwt.Token);

    var exhangedToken: Reo.JwtSecurityToken = manager.ProvideJwtbyOldJwt(newJwt);
    console.log("exchanged: " + exhangedToken.Token);

});