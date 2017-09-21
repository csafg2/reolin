module Reolin.Web.Client
{
    export class User
    {
        public get Id(): number
        {
            return parseInt(store.Get().GetKey("Id"));
        }

        public UserName: string;
        public Email: string;
    }
}