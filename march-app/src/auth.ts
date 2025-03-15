import NextAuth, { User } from "next-auth";
import Credentials from "next-auth/providers/credentials"
import { ApiResPayload, LoginRes } from "@/types";
import { CustomAuthError } from "@/utils";

export const { handlers, signIn, signOut, auth } = NextAuth({
    providers: [
        Credentials({
            // You can specify which fields should be submitted, by adding keys to the `credentials` object.
            credentials: {
                userId: {},
                password: {},
            },
            authorize: async (credentials) => {
                const response = await fetch(`${process.env.MARCH_API}/auth/login`, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify(credentials),
                });

                const data: ApiResPayload<LoginRes> = await response.json();

                if (data.isSuccess)
                    return { ...data.payload };
                else
                    throw new CustomAuthError(data.message);
            },
        }),
    ],
    pages: {
        signIn: "/auth/login",
    },
    callbacks: {
        jwt({ token, user }) {
            // console.log('jwt callbacks', token, user);
            if (user) { // User is available during sign-in
                token.user = (user as User);
            }
            return token
        },
        session({ session, token }) {
            // console.log('session callbacks', session, token);
            (session.user as User) = token.user as User;
            return session
        },
        authorized: async ({ auth }) => {
            // Logged in users are authenticated, 
            //otherwise redirect to login page
            return !!auth
            // return true
        },
    },
})