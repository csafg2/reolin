
module Reolin.Web.Client
{
    export class DefaultJwtManager implements IJwtManager
    {
        private _source: IJwtSource;
        private _store: IJwtStore;

        constructor(source: IJwtSource, store: IJwtStore)
        {
            this._source = source;
            this._store = store;
        }

        GetLocalJwt(): JwtSecurityToken
        {
            if (!this._store.HasJwt())
            {
                return null;
            }
            return this._store.Get();
        }

        Save(jwt: JwtSecurityToken): void
        {
            this._store.Save(jwt);
        }

        IssueJwt (info: LoginInfo): JwtSecurityToken
        {
            var jwt: JwtSecurityToken = this._source.IssueJwt(info);
            this.Save(jwt);
            return jwt;
        }

        ProvideJwtbyOldJwt(oldJwt: JwtSecurityToken): JwtSecurityToken
        {
            if (oldJwt === null)
            {
                throw new Error("oldJwt can not be null");
            }

            var newToken: JwtSecurityToken = this._source.ExchangeJwt(oldJwt);
            this._store.Save(newToken);

            return newToken;
        }
    }
}