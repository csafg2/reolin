module Reolin.Web.Client
{
    export class JwtSecurityToken
    {
        private _tokenValues: { [key: string]: string } = {};
        private _token: string;

        constructor(token: string)
        {
            this._token = token;
        }

        get IsExpired(): boolean
        {
            return false;
        };

        public static Decode(jwt: string): any
        {
            var base64Url = jwt.split('.')[1];
            var base64 = base64Url.replace('-', '+').replace('_', '/');
            return JSON.parse(window.atob(base64));
        }

        get Token(): string
        {
            return this._token;
        }
        
        GetKey(key: string): string
        {
            return this._tokenValues[key];
        }

        public static Parse(jwt: string): JwtSecurityToken
        {
            if (!jwt)
            {
                throw Error("jwt can not be null");
            }
            var result: JwtSecurityToken = new JwtSecurityToken(jwt);
            var jst = JwtSecurityToken.Decode(jwt);

            for (var p in jst)
            {
                result._tokenValues[p] = jst[p];
            }

            return result;
        };

        public static TryParse(jwt: string): JwtSecurityToken
        {
            try
            {
                return JwtSecurityToken.Parse(jwt);
            } catch (e)
            {
                if (console)
                {
                    console.log(e.Message);
                }
                return null;
            }
        };
    }
}