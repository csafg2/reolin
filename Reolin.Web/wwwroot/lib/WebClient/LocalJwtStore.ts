
module Reolin.Web.Client
{


    export class LocalJwtStore implements IJwtStore
    {
        private key: string = "jwt";

        HasJwt(): boolean
        {
            return (window.localStorage.getItem(this.key) !== null);
        }

        Get(): JwtSecurityToken
        {
            return JwtSecurityToken.Parse(window.localStorage.getItem(this.key));
        }


        Save(jwt: JwtSecurityToken): void
        {
            if (jwt === null)
            {
                throw Error("jwt can not be null");
            }

            window.localStorage.clear();

            window.localStorage.setItem(this.key, jwt.Token);
        }
    }
}