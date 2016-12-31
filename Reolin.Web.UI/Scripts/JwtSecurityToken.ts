module Reolin.Web.Client {
    export class JwtSecurityToken {

        private _token: string;
        private _tokenObject: any;

        constructor(jwt: string) {
            this._token = jwt;
            this._tokenObject = this.Decode(jwt);
        }

        get IsExpired(): boolean {
            
            return false;
        };

        Decode(jwt: string): any {
            var base64Url = this._token.split('.')[1];
            var base64 = base64Url.replace('-', '+').replace('_', '/');
            return JSON.parse(window.atob(base64));
        }

        GetToken(): string {
            return this._token;
        }

        public static Parse(jwt: string): JwtSecurityToken {
            if (!jwt) {
                throw Error("jwt can not be null");
            }

            return new JwtSecurityToken(jwt);
        };

        public static TryParse(jwt: string): JwtSecurityToken {
            try {
                return JwtSecurityToken.Parse(jwt);
            } catch (e) {
                return null;
            }
        };
    }
}