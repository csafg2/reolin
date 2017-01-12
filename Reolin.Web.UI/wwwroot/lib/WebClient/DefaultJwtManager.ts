
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
            
        }

        ProvideJwtbyOldJwt(oldJwt: JwtSecurityToken): JwtSecurityToken
        {
            if (oldJwt === null)
            {
                throw new Error("oldJwt can not be null");
            }

            return this._source.ExchangeJwt(oldJwt);
        }
    }
}