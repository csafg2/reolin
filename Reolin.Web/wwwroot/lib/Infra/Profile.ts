module Reolin.Web.Client
{
    export class Profile
    {
        static get Current(): Profile
        {
            return JSON.parse(window.localStorage.getItem("CurrentProfile"));
        }

        static set Current(profile: Profile)
        {
            window.localStorage.setItem("CurrentProfile", JSON.stringify(profile));
        }

        public Id: number;
        public Name: string;
    }
}