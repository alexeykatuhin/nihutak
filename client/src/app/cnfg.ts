import { AuthServiceConfig, GoogleLoginProvider } from 'angularx-social-login';

export let config = new AuthServiceConfig([
    {
       id: GoogleLoginProvider.PROVIDER_ID,
       provider: new GoogleLoginProvider('1023105569360-9opdg0vp5r8arc481fuevhglc61a7v0h.apps.googleusercontent.com')
    },

]);
export function provideConfig()
   {
      return config;
   }