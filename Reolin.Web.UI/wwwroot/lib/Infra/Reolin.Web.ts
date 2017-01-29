module Reolin.Web.Client
{
    export class User
    {
        private static _current: User;

        static get Current(): User
        {
            return User._current;
        }

        static set Current(user: User)
        {
            User._current = user;
        }

        public UserName: string;
        public Email: string;
    }
}